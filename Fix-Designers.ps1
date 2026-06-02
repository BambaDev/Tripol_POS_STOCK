# Script de nettoyage des fichiers Designer.cs
# Commente toutes les lignes resources.GetObject() qui causent NullReferenceException

param(
    [switch]$DryRun = $false
)

Write-Host @"
========================================================
   Fix Designer.cs Files - Ezzipos POS
   Comments out problematic resources.GetObject() calls
========================================================
"@ -ForegroundColor Cyan

$projectPath = "Pos\Pos\Forms"
$designerFiles = Get-ChildItem -Path $projectPath -Filter "*.Designer.cs" -Recurse

Write-Host "`nFound $($designerFiles.Count) Designer.cs files`n" -ForegroundColor Yellow

$totalFiles = 0
$totalLinesCommented = 0

foreach ($file in $designerFiles) {
    Write-Host "Processing: $($file.Name)" -ForegroundColor Cyan

    $content = Get-Content $file.FullName -Raw
    $lines = Get-Content $file.FullName

    $modifiedLines = @()
    $linesCommentedInFile = 0
    $inCommentBlock = $false

    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]

        # Patterns qui causent NullReferenceException
        $shouldComment = $false

        if ($line -match 'resources\.GetObject\(' -and
            $line -match '\.(Appearance|AppearanceGroup|Properties\.Appearance|Properties\.Buttons|Properties\.Columns|Properties\.AutoHeight)') {
            $shouldComment = $true
        }

        if ($line -match 'resources\.GetString\(' -and
            $line -match '\.(Properties\.NullText|Properties\.Columns)') {
            $shouldComment = $true
        }

        # Options.Use* qui suivent un GetObject
        if ($line -match '\.Options\.Use(BackColor|BorderColor|Font|ForeColor)' -and
            $i -gt 0 -and
            $lines[$i-1] -match '//.*resources\.GetObject') {
            $shouldComment = $true
        }

        if ($shouldComment -and $line -notmatch '^\s*//') {
            # Commenter la ligne en preservant l'indentation
            if ($line -match '^(\s*)(.+)$') {
                $indent = $matches[1]
                $code = $matches[2]
                $modifiedLines += "$indent// FIXED: $code"
                $linesCommentedInFile++
            } else {
                $modifiedLines += $line
            }
        } else {
            $modifiedLines += $line
        }
    }

    if ($linesCommentedInFile -gt 0) {
        Write-Host "  -> $linesCommentedInFile lines commented" -ForegroundColor Green

        if (-not $DryRun) {
            $modifiedLines | Set-Content $file.FullName -Encoding UTF8
        }

        $totalFiles++
        $totalLinesCommented += $linesCommentedInFile
    } else {
        Write-Host "  -> No changes needed" -ForegroundColor Gray
    }
}

Write-Host "`n========================================================" -ForegroundColor Cyan
Write-Host "SUMMARY" -ForegroundColor Yellow
Write-Host "========================================================" -ForegroundColor Cyan
Write-Host "Files modified: $totalFiles" -ForegroundColor Green
Write-Host "Total lines commented: $totalLinesCommented" -ForegroundColor Green

if ($DryRun) {
    Write-Host "`n[DRY RUN] No files were actually modified." -ForegroundColor Yellow
    Write-Host "Run without -DryRun to apply changes." -ForegroundColor Yellow
} else {
    Write-Host "`n[SUCCESS] All Designer files fixed!" -ForegroundColor Green
    Write-Host "`nNext steps:" -ForegroundColor Cyan
    Write-Host "  1. Rebuild the project in Visual Studio" -ForegroundColor White
    Write-Host "  2. Test the application" -ForegroundColor White
}
