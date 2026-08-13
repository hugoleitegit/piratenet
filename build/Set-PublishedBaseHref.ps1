param(
    [Parameter(Mandatory = $true)]
    [string]$PublishedIndexHtml,

    [Parameter(Mandatory = $true)]
    [string]$BaseHref
)

if (-not (Test-Path -LiteralPath $PublishedIndexHtml)) {
    Write-Host "Published index not found: $PublishedIndexHtml"
    exit 0
}

$content = Get-Content -LiteralPath $PublishedIndexHtml -Raw
$pattern = '<base\s+href=(?:"[^"]*"|[^\s>]+)\s*/?>'
$replacement = '<base href="{0}" />' -f $BaseHref
$updated = [System.Text.RegularExpressions.Regex]::Replace(
    $content,
    $pattern,
    $replacement,
    [System.Text.RegularExpressions.RegexOptions]::IgnoreCase
)

Set-Content -LiteralPath $PublishedIndexHtml -Value $updated -NoNewline
Write-Host "Base href updated to '$BaseHref' in $PublishedIndexHtml"
