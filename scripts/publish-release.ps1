<#
.SYNOPSIS
    GitHub Releases向けに、自己完結版とFramework-dependent版の両方のexeをビルドし dist/ に出力する。
.DESCRIPTION
    - BatteryWatcher.exe      : 自己完結・単一exe（.NETランタイム不要、約70MB）
    - BatteryWatcher-fx.exe   : Framework-dependent・単一exe（.NET 8 Desktop Runtime必須、数百KB）
#>

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$csproj = Join-Path $repoRoot "src/BatteryWatcher/BatteryWatcher.csproj"
$distDir = Join-Path $repoRoot "dist"

if (Test-Path $distDir) {
    Remove-Item $distDir -Recurse -Force
}
New-Item -ItemType Directory -Path $distDir | Out-Null

$selfContainedOut = Join-Path $distDir "_sc"
Write-Host "== 自己完結版をビルド中 ==" -ForegroundColor Cyan
dotnet publish $csproj -c Release -o $selfContainedOut
Copy-Item (Join-Path $selfContainedOut "BatteryWatcher.exe") (Join-Path $distDir "BatteryWatcher.exe")

$frameworkDependentOut = Join-Path $distDir "_fx"
Write-Host "== Framework-dependent版をビルド中 ==" -ForegroundColor Cyan
dotnet publish $csproj -c Release -o $frameworkDependentOut `
    -p:SelfContained=false `
    -p:PublishSingleFile=true `
    -p:EnableCompressionInSingleFile=false
Copy-Item (Join-Path $frameworkDependentOut "BatteryWatcher.exe") (Join-Path $distDir "BatteryWatcher-fx.exe")

Remove-Item $selfContainedOut -Recurse -Force
Remove-Item $frameworkDependentOut -Recurse -Force

Write-Host ""
Write-Host "完了。dist/ の内容:" -ForegroundColor Green
Get-ChildItem $distDir | Format-Table Name, @{Label = "Size(MB)"; Expression = { [Math]::Round($_.Length / 1MB, 2) } }
