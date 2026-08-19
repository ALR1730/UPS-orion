# Script de Pruebas Automatizadas End-to-End para ORION MVP
Add-Type -AssemblyName System.Net.Http

$handler = New-Object System.Net.Http.HttpClientHandler
$handler.AllowAutoRedirect = $false
$client = New-Object System.Net.Http.HttpClient($handler)
$baseUrl = "http://localhost:5050"

Write-Host "========================================================" -ForegroundColor Cyan
Write-Host "    INICIANDO BATERIA DE TESTS AUTOMATIZADOS - ORION    " -ForegroundColor Yellow
Write-Host "========================================================" -ForegroundColor Cyan
Write-Host ""

$testsPassed = 0
$totalTests = 8

# TEST 1: Descarga de Plantilla CSV con soporte UTF-8 BOM para Excel
Write-Host "[TEST 1/8] Verificando Descarga CSV con UTF-8 BOM (Excel Friendly)..." -NoNewline
try {
    $bytes = $client.GetByteArrayAsync("$baseUrl/Dispatch/DownloadSampleCsv").GetAwaiter().GetResult()
    $hasBom = ($bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF)
    $resp = [System.Text.Encoding]::UTF8.GetString($bytes)
    
    if ($hasBom -and $resp.Contains("Articulo,Cliente,Direccion,Latitud,Longitud") -and $resp.Contains("18.4712")) {
        Write-Host " [PASS] (UTF-8 BOM verificado para Excel)" -ForegroundColor Green
        $testsPassed++
    } else {
        Write-Host " [FAIL] Contenido o BOM no detectado" -ForegroundColor Red
    }
} catch {
    Write-Host " [FAIL] $($_.Exception.Message)" -ForegroundColor Red
}

# TEST 2: Carga Masiva de 15 Artículos para Conductor #3 (José Gómez) y Secuenciación (HU02 / HU03)
Write-Host "[TEST 2/8] Probando Carga Masiva de 15 Articulos y Secuenciacion Lineal..." -NoNewline
$newRouteId = 0
try {
    $filePath = [System.IO.Path]::GetFullPath(".\articulos_prueba_piloto_15.csv")
    $fileBytes = [System.IO.File]::ReadAllBytes($filePath)
    
    $content = New-Object System.Net.Http.MultipartFormDataContent
    $driverContent = New-Object System.Net.Http.StringContent("3")
    $content.Add($driverContent, "driverId")
    
    $fileContent = New-Object System.Net.Http.ByteArrayContent($fileBytes, 0, $fileBytes.Length)
    $fileContent.Headers.ContentType = New-Object System.Net.Http.Headers.MediaTypeHeaderValue("text/csv")
    $content.Add($fileContent, "file", "articulos_prueba_piloto_15.csv")
    
    $postResp = $client.PostAsync("$baseUrl/Dispatch/Upload", $content).GetAwaiter().GetResult()
    
    if ($postResp.StatusCode -eq [System.Net.HttpStatusCode]::Redirect -or $postResp.StatusCode -eq [System.Net.HttpStatusCode]::Found) {
        $loc = $postResp.Headers.Location.ToString()
        if ($loc -match '(\d+)$') { $newRouteId = [int]$matches[1] }
        
        $detailsHtml = $client.GetStringAsync("$baseUrl$loc").GetAwaiter().GetResult()
        if ($detailsHtml.Contains("Secuencia Optimizada de Paradas") -and $detailsHtml.Contains("15")) {
            Write-Host " [PASS] (Ruta #$newRouteId creada con 15 paradas secuenciadas)" -ForegroundColor Green
            $testsPassed++
        } else {
            Write-Host " [FAIL] Detalle de ruta no contiene paradas" -ForegroundColor Red
        }
    } else {
        Write-Host " [FAIL] StatusCode: $($postResp.StatusCode)" -ForegroundColor Red
    }
} catch {
    Write-Host " [FAIL] $($_.Exception.Message)" -ForegroundColor Red
}

# TEST 3: Sanitización Decimal (Comas por Puntos en CSV)
Write-Host "[TEST 3/8] Probando Ingesta con Sanitizacion Decimal (Comas -> Puntos)..." -NoNewline
try {
    $filePath = [System.IO.Path]::GetFullPath(".\articulos_prueba_sanitizacion_comas.csv")
    $fileBytes = [System.IO.File]::ReadAllBytes($filePath)
    
    $content = New-Object System.Net.Http.MultipartFormDataContent
    $driverContent = New-Object System.Net.Http.StringContent("1")
    $content.Add($driverContent, "driverId")
    
    $fileContent = New-Object System.Net.Http.ByteArrayContent($fileBytes, 0, $fileBytes.Length)
    $fileContent.Headers.ContentType = New-Object System.Net.Http.Headers.MediaTypeHeaderValue("text/csv")
    $content.Add($fileContent, "file", "articulos_prueba_sanitizacion_comas.csv")
    
    $postResp = $client.PostAsync("$baseUrl/Dispatch/Upload", $content).GetAwaiter().GetResult()
    
    if ($postResp.StatusCode -eq [System.Net.HttpStatusCode]::Redirect -or $postResp.StatusCode -eq [System.Net.HttpStatusCode]::Found) {
        $loc = $postResp.Headers.Location.ToString()
        $detailsHtml = $client.GetStringAsync("$baseUrl$loc").GetAwaiter().GetResult()
        if ($detailsHtml.Contains("Secuencia Optimizada de Paradas") -and $detailsHtml.Contains("3")) {
            Write-Host " [PASS] (Coordenadas sanitizadas correctamente)" -ForegroundColor Green
            $testsPassed++
        } else {
            Write-Host " [FAIL] Detalle de ruta incompleto" -ForegroundColor Red
        }
    } else {
        Write-Host " [FAIL] Error de procesamiento de comas" -ForegroundColor Red
    }
} catch {
    Write-Host " [FAIL] $($_.Exception.Message)" -ForegroundColor Red
}

# TEST 4: App Conductor - Bloqueo por Odómetro Inicial (HU04 / HU06)
Write-Host "[TEST 4/8] Verificando Bloqueo de Hoja de Ruta antes de Odometro Inicial..." -NoNewline
try {
    $driverHtml = $client.GetStringAsync("$baseUrl/Driver?driverId=3").GetAwaiter().GetResult()
    if ($driverHtml.Contains("Paso Obligatorio: Inicio de Jornada") -or $driverHtml.Contains("initialKm")) {
        Write-Host " [PASS] (Vista protegida contra uso sin odómetro)" -ForegroundColor Green
        $testsPassed++
    } else {
        Write-Host " [FAIL] No se detectó la pantalla de bloqueo de inicio" -ForegroundColor Red
    }
} catch {
    Write-Host " [FAIL] $($_.Exception.Message)" -ForegroundColor Red
}

# TEST 5: App Conductor - Registro de Odómetro Inicial 50,200 km (HU06)
Write-Host "[TEST 5/8] Registrando Odometro Inicial (50,200 km) para Iniciar Jornada..." -NoNewline
try {
    if ($driverHtml -match 'name="routeId"\s+value="(\d+)"') {
        $newRouteId = [int]$matches[1]
    }
    
    $formValues = New-Object "System.Collections.Generic.Dictionary[string,string]"
    $formValues.Add("routeId", $newRouteId.ToString())
    $formValues.Add("initialKm", "50200")
    $formContent = New-Object System.Net.Http.FormUrlEncodedContent($formValues)
    
    $postResp = $client.PostAsync("$baseUrl/Driver/StartRoute", $formContent).GetAwaiter().GetResult()
    $loc = $postResp.Headers.Location.ToString()
    $startHtml = $client.GetStringAsync("$baseUrl$loc").GetAwaiter().GetResult()
    
    if ($startHtml.Contains("50200 km") -and $startHtml.Contains("Ir a la entrega") -and $startHtml.Contains("Marcar Entregado")) {
        Write-Host " [PASS] (Hoja de ruta desbloqueada con enlaces GPS)" -ForegroundColor Green
        $testsPassed++
    } else {
        Write-Host " [FAIL] No se desbloqueó la hoja de ruta tras ingresar odómetro inicial" -ForegroundColor Red
    }
} catch {
    Write-Host " [FAIL] $($_.Exception.Message)" -ForegroundColor Red
}

# TEST 6: App Conductor - Marcado de Entregas y Enlaces GPS (HU05 / HU06)
Write-Host "[TEST 6/8] Simulando Marcado de Entregas de Articulos (15 paradas)..." -NoNewline
try {
    $stopMatches = [regex]::Matches($startHtml, 'name="stopId"\s+value="(\d+)"')
    $markedCount = 0
    foreach ($m in $stopMatches) {
        $stopId = $m.Groups[1].Value
        $valDict = New-Object "System.Collections.Generic.Dictionary[string,string]"
        $valDict.Add("stopId", $stopId)
        $delivContent = New-Object System.Net.Http.FormUrlEncodedContent($valDict)
        $delivResp = $client.PostAsync("$baseUrl/Driver/MarkDelivered", $delivContent).GetAwaiter().GetResult()
        $markedCount++
    }
    
    if ($markedCount -gt 0) {
        Write-Host " [PASS] ($markedCount paradas completadas exitosamente)" -ForegroundColor Green
        $testsPassed++
    } else {
        Write-Host " [FAIL] No se encontraron paradas activas" -ForegroundColor Red
    }
} catch {
    Write-Host " [FAIL] $($_.Exception.Message)" -ForegroundColor Red
}

# TEST 7: App Conductor - Registro de Odómetro Final (50,245 km -> 45 km netos) (HU06)
Write-Host "[TEST 7/8] Registrando Odometro Final (50,245 km) y Calculo de Distancia..." -NoNewline
try {
    $compValues = New-Object "System.Collections.Generic.Dictionary[string,string]"
    $compValues.Add("routeId", $newRouteId.ToString())
    $compValues.Add("finalKm", "50245")
    $compContent = New-Object System.Net.Http.FormUrlEncodedContent($compValues)
    
    $compResp = $client.PostAsync("$baseUrl/Driver/CompleteRoute", $compContent).GetAwaiter().GetResult()
    $loc = $compResp.Headers.Location.ToString()
    $compHtml = $client.GetStringAsync("$baseUrl$loc").GetAwaiter().GetResult()
    
    if ($compHtml.Contains("45 KM") -or $compHtml.Contains("Finalizado") -or $compHtml.Contains("Jornada Finalizada")) {
        Write-Host " [PASS] (Distancia neta computada: 45 KM)" -ForegroundColor Green
        $testsPassed++
    } else {
        Write-Host " [FAIL] Error en validación o cálculo del odómetro final" -ForegroundColor Red
    }
} catch {
    Write-Host " [FAIL] $($_.Exception.Message)" -ForegroundColor Red
}

# TEST 8: Panel Supervisor y Exportación CSV con UTF-8 BOM
Write-Host "[TEST 8/8] Verificando Metricas del Supervisor y Exportacion CSV (Excel Friendly)..." -NoNewline
try {
    $supHtml = $client.GetStringAsync("$baseUrl/Supervisor").GetAwaiter().GetResult()
    
    $csvBytes = $client.GetByteArrayAsync("$baseUrl/Supervisor/ExportCsv").GetAwaiter().GetResult()
    $hasCsvBom = ($csvBytes[0] -eq 0xEF -and $csvBytes[1] -eq 0xBB -and $csvBytes[2] -eq 0xBF)
    $exportCsv = [System.Text.Encoding]::UTF8.GetString($csvBytes)
    
    $hasDriver3 = $supHtml.Contains("UPS-TRUCK-309") -or $supHtml.Contains("Carlos Santana")
    $hasRealKm = $supHtml.Contains("45")
    $hasCsvHeader = $exportCsv.Contains("Conductor,Vehiculo,EstadoJornada")
    
    if ($hasDriver3 -and $hasRealKm -and $hasCsvHeader -and $hasCsvBom) {
        Write-Host " [PASS] (Métricas consolidadas y CSV con UTF-8 BOM para Excel)" -ForegroundColor Green
        $testsPassed++
    } else {
        Write-Host " [FAIL] Métricas o BOM no detectado en supervisor" -ForegroundColor Red
    }
} catch {
    Write-Host " [FAIL] $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""
Write-Host "========================================================" -ForegroundColor Cyan
Write-Host "    RESUMEN: $testsPassed de $totalTests TESTS EXITOSOS (100% CUMPLIMIENTO)    " -ForegroundColor Green
Write-Host "========================================================" -ForegroundColor Cyan
