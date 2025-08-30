# DotGist�� ���� dotnet-suggest �ڵ��ϼ� ��� ��ũ��Ʈ
# �� ��ũ��Ʈ�� �����ϸ� PowerShell���� DotGist ��ɾ� �ڵ��ϼ��� Ȱ��ȭ�˴ϴ�.

Write-Host "DotGist dotnet-suggest �ڵ��ϼ��� ����մϴ�..." -ForegroundColor Green

# ���� ������Ʈ ����
Write-Host "������Ʈ ���� ��..." -ForegroundColor Yellow
dotnet build

if ($LASTEXITCODE -eq 0) {
    Write-Host "? ���� �Ϸ�" -ForegroundColor Green
    
    # dotnet-suggest ���
    $exePath = "dotnet run --project `"$PWD\DotGist.csproj`" --"
    Write-Host "����� ��ɾ�: $exePath" -ForegroundColor Cyan
    
    # PowerShell���� �ڵ��ϼ� ���
    dotnet-suggest register --command-path $exePath --suggestion-command "dotnet run --project `"$PWD\DotGist.csproj`" -- [suggest]"
    
    Write-Host "? dotnet-suggest ��� �Ϸ�!" -ForegroundColor Green
    Write-Host ""
    Write-Host "����:" -ForegroundColor Yellow
    Write-Host "  $exePath --help" -ForegroundColor Cyan
    Write-Host "  $exePath --url `"https://gist.github.com/username/gistid`" --output `"myfile.cs`"" -ForegroundColor Cyan
    Write-Host "  $exePath quick-async --url `"https://gist.github.com/username/gistid`"" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "�ڵ��ϼ��� ����Ϸ��� PowerShell�� �ٽ� �����ϰų� ������ �����ϼ���:" -ForegroundColor Yellow
    Write-Host "  dotnet-suggest script | Out-String | Invoke-Expression" -ForegroundColor Cyan
}
else {
    Write-Host "? ���� ����" -ForegroundColor Red
    exit 1
}