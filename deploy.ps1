<#
deploy.ps1
Builds the project (dotnet publish) and deploys publish/wwwroot to the
gh-pages branch using a temporary git worktree.

Usage: Run from the repo root:
  .\deploy.ps1

This script is a local alternative to the GitHub Action.
#>

$ErrorActionPreference = 'Stop'

# repoRoot = script directory (works when executed from repo root)
$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $repoRoot

Write-Output "Publishing project..."
dotnet publish -c Release -o publish

$publishWww = Join-Path $repoRoot 'publish\wwwroot'
if (!(Test-Path $publishWww)) {
    Write-Error "publish/wwwroot not found. Build failed or wrong folder."; exit 1
}

Write-Output "Ensuring .nojekyll in publish/wwwroot..."
if (!(Test-Path (Join-Path $publishWww '.nojekyll'))) { New-Item -Path (Join-Path $publishWww '.nojekyll') -ItemType File -Force | Out-Null }

$worktreePath = Join-Path $repoRoot 'Piratenet-gh-pages'

Write-Output "Preparing gh-pages worktree at $worktreePath..."
git worktree prune
if (Test-Path $worktreePath) { Remove-Item -Recurse -Force $worktreePath -ErrorAction SilentlyContinue }
git worktree add -B gh-pages $worktreePath origin/gh-pages

Write-Output "Cleaning worktree and copying published files..."
# remove everything except .git in worktree
Get-ChildItem -Path $worktreePath -Force | Where-Object { $_.Name -ne '.git' } | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
Copy-Item -Path (Join-Path $publishWww '*') -Destination $worktreePath -Recurse -Force

Push-Location $worktreePath
Write-Output "Committing and pushing to gh-pages..."
git add -A
git commit -m "Deploy: $(Get-Date -Format o)" 2>$null
if ($LASTEXITCODE -eq 0) {
    git push origin gh-pages
} else {
    Write-Output "No changes to commit"
}
Pop-Location

Write-Output "Cleaning up worktree..."
git worktree remove $worktreePath -f
git worktree prune

Write-Output "Deploy complete."
