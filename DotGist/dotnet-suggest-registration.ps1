# DotGist를 위한 dotnet-suggest 자동완성 등록 스크립트
# 이 스크립트를 실행하면 PowerShell에서 DotGist 명령어 자동완성이 활성화됩니다.

Write-Host "DotGist dotnet-suggest 자동완성을 등록합니다..." -ForegroundColor Green

# 현재 프로젝트 빌드
Write-Host "프로젝트 빌드 중..." -ForegroundColor Yellow
dotnet build

if ($LASTEXITCODE -eq 0) {
    Write-Host "? 빌드 완료" -ForegroundColor Green
    
    # dotnet-suggest 등록
    $exePath = "dotnet run --project `"$PWD\DotGist.csproj`" --"
    Write-Host "등록할 명령어: $exePath" -ForegroundColor Cyan
    
    # PowerShell에서 자동완성 등록
    dotnet-suggest register --command-path $exePath --suggestion-command "dotnet run --project `"$PWD\DotGist.csproj`" -- [suggest]"
    
    Write-Host "? dotnet-suggest 등록 완료!" -ForegroundColor Green
    Write-Host ""
    Write-Host "사용법:" -ForegroundColor Yellow
    Write-Host "  $exePath --help" -ForegroundColor Cyan
    Write-Host "  $exePath --url `"https://gist.github.com/username/gistid`" --output `"myfile.cs`"" -ForegroundColor Cyan
    Write-Host "  $exePath quick-async --url `"https://gist.github.com/username/gistid`"" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "자동완성을 사용하려면 PowerShell을 다시 시작하거나 다음을 실행하세요:" -ForegroundColor Yellow
    Write-Host "  dotnet-suggest script | Out-String | Invoke-Expression" -ForegroundColor Cyan
}
else {
    Write-Host "? 빌드 실패" -ForegroundColor Red
    exit 1
}