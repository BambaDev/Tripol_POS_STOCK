# Script de correction des fichiers .fr.resx
# Copie les proprietes DevExpress manquantes depuis .resx vers .fr.resx
# Preserve les traductions francaises existantes

param(
    [string]$FormPath = "",
    [switch]$TestMode = $false,
    [switch]$AllForms = $false
)

function Merge-ResxFiles {
    param(
        [string]$SourceResx,
        [string]$TargetResx
    )

    Write-Host "`n=== Processing: $TargetResx ===" -ForegroundColor Cyan

    if (-not (Test-Path $SourceResx)) {
        Write-Host "ERROR: Source file not found: $SourceResx" -ForegroundColor Red
        return $false
    }

    if (-not (Test-Path $TargetResx)) {
        Write-Host "ERROR: Target file not found: $TargetResx" -ForegroundColor Red
        return $false
    }

    # Charger les fichiers XML
    [xml]$sourceXml = Get-Content $SourceResx -Encoding UTF8
    [xml]$targetXml = Get-Content $TargetResx -Encoding UTF8

    # Compteurs
    $added = 0
    $preserved = 0

    # Parcourir toutes les entrees du fichier source
    foreach ($sourceData in $sourceXml.root.data) {
        $dataName = $sourceData.name

        # Verifier si c'est une propriete DevExpress a copier
        $isDevExpressProperty = $dataName -match '\.(Properties\.|Appearance|Columns|ButtonsStyle|LookAndFeel|OptionsView|OptionsBehavior)'

        # Verifier si c'est une propriete technique (pas de traduction)
        $isTechnicalProperty = $dataName -match '\.(Size|Location|Anchor|Dock|TabIndex|Margin|Padding)'

        # Ne PAS copier les textes/labels (on garde les traductions francaises)
        $isTextProperty = $dataName -match '\.(Text|Caption|ToolTip|Hint|EmptyText)$' -and
                         $dataName -notmatch '\.(Properties\.|Appearance)'

        if ($isTextProperty) {
            # Garder la traduction francaise existante
            $preserved++
            continue
        }

        # Verifier si l'entree existe deja dans le fichier cible
        $existingData = $targetXml.root.data | Where-Object { $_.name -eq $dataName }

        if (-not $existingData -and ($isDevExpressProperty -or $isTechnicalProperty)) {
            # Creer une nouvelle entree
            $newData = $targetXml.ImportNode($sourceData, $true)
            $targetXml.root.AppendChild($newData) | Out-Null
            $added++

            Write-Host "  [+] Added: $dataName" -ForegroundColor Green
        }
    }

    # Sauvegarder le fichier modifie
    $targetXml.Save($TargetResx)

    Write-Host "`nResults:" -ForegroundColor Yellow
    Write-Host "  Added properties: $added" -ForegroundColor Green
    Write-Host "  Preserved translations: $preserved" -ForegroundColor Cyan

    return $true
}

# === MAIN SCRIPT ===

Write-Host ""
Write-Host "=========================================================" -ForegroundColor Magenta
Write-Host "   Fix French .resx Files - Ezzipos POS" -ForegroundColor Magenta
Write-Host "   Corrects invisible LookUpEdit controls in French" -ForegroundColor Magenta
Write-Host "=========================================================" -ForegroundColor Magenta

$projectRoot = "Pos\Pos\Forms"

if ($TestMode) {
    Write-Host "`n[TEST MODE] Processing OpenRegister only..." -ForegroundColor Yellow

    $sourceFile = "$projectRoot\Register\OpenRegister.resx"
    $targetFile = "$projectRoot\Register\OpenRegister.fr.resx"

    $result = Merge-ResxFiles -SourceResx $sourceFile -TargetResx $targetFile

    if ($result) {
        Write-Host "`n[SUCCESS] Test completed successfully!" -ForegroundColor Green
        Write-Host "`nNext steps:" -ForegroundColor Cyan
        Write-Host "  1. Rebuild the project"
        Write-Host "  2. Run the app and test OpenRegister in French"
        Write-Host "  3. If OK, run: .\Fix-FrenchResx.ps1 -AllForms"
    }
}
elseif ($AllForms) {
    Write-Host "`n[FULL MODE] Processing ALL French .resx files..." -ForegroundColor Yellow

    $frenchFiles = Get-ChildItem -Path $projectRoot -Filter "*.fr.resx" -Recurse
    $totalFiles = $frenchFiles.Count
    $processed = 0
    $success = 0
    $failed = 0

    Write-Host "Found $totalFiles French resource files`n" -ForegroundColor Cyan

    foreach ($frFile in $frenchFiles) {
        $processed++
        Write-Host "[$processed/$totalFiles] " -NoNewline

        $sourceFile = $frFile.FullName -replace '\.fr\.resx$', '.resx'
        $targetFile = $frFile.FullName

        $result = Merge-ResxFiles -SourceResx $sourceFile -TargetResx $targetFile

        if ($result) {
            $success++
        } else {
            $failed++
        }
    }

    Write-Host "`n=========================================================" -ForegroundColor Magenta
    Write-Host "SUMMARY" -ForegroundColor Yellow
    Write-Host "=========================================================" -ForegroundColor Magenta
    Write-Host "Total files processed: $processed" -ForegroundColor Cyan
    Write-Host "Successful: $success" -ForegroundColor Green
    Write-Host "Failed: $failed" -ForegroundColor $(if($failed -gt 0){"Red"}else{"Green"})
    Write-Host "`n[SUCCESS] All files processed!" -ForegroundColor Green
    Write-Host "`nNext steps:" -ForegroundColor Cyan
    Write-Host "  1. Rebuild the project"
    Write-Host "  2. Test the application in French"
}
elseif ($FormPath -ne "") {
    Write-Host "`n[SINGLE FORM MODE] Processing: $FormPath..." -ForegroundColor Yellow

    $sourceFile = "$FormPath.resx"
    $targetFile = "$FormPath.fr.resx"

    $result = Merge-ResxFiles -SourceResx $sourceFile -TargetResx $targetFile

    if ($result) {
        Write-Host "`n[SUCCESS] Form processed successfully!" -ForegroundColor Green
    }
}
else {
    Write-Host ""
    Write-Host "USAGE:" -ForegroundColor White
    Write-Host "------" -ForegroundColor White
    Write-Host ""
    Write-Host "Test on OpenRegister only:" -ForegroundColor Yellow
    Write-Host "  .\Fix-FrenchResx.ps1 -TestMode" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Process single form:" -ForegroundColor Yellow
    Write-Host "  .\Fix-FrenchResx.ps1 -FormPath 'Pos\Pos\Forms\Customer\AddEditCustomer'" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Process ALL forms:" -ForegroundColor Yellow
    Write-Host "  .\Fix-FrenchResx.ps1 -AllForms" -ForegroundColor Cyan
    Write-Host ""
}
