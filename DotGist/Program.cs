using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using var httpClient = new HttpClient();
var retriever = new WebStringRetriever(httpClient);

var url = "https://gist.github.com/atawLee/472ecaaa4654a2f7d23a8e09f9161f5f";

// GitHub Gist에서 코드만 추출
var extractedCode = await retriever.ExtractGistCodeAsync(url);

Console.WriteLine("추출된 코드:");
Console.WriteLine(extractedCode);

var fileName = $"{Guid.NewGuid()}.cs";
var filePath = Path.Combine(Environment.CurrentDirectory, fileName);

try
{
    await File.WriteAllTextAsync(filePath, extractedCode);
    Console.WriteLine($"\n파일이 저장되었습니다: {fileName}");
    Console.WriteLine($"전체 경로: {filePath}");
    
    // dotnet run으로 생성된 파일 실행
    await RunDotnetCommand(fileName);
}
catch (Exception ex)
{
    Console.WriteLine($"파일 저장 중 오류가 발생했습니다: {ex.Message}");
}

Console.ReadLine();

static async Task RunDotnetCommand(string fileName)
{
    try
    {
        Console.WriteLine($"\n실행 중: dotnet run {fileName}");
        
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run {fileName}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(processStartInfo);
        if (process != null)
        {
            // 표준 출력 읽기
            var outputTask = process.StandardOutput.ReadToEndAsync();
            var errorTask = process.StandardError.ReadToEndAsync();
            
            await process.WaitForExitAsync();
            
            var output = await outputTask;
            var error = await errorTask;
            
            Console.WriteLine("\n=== 실행 결과 ===");
            if (!string.IsNullOrEmpty(output))
            {
                Console.WriteLine("출력:");
                Console.WriteLine(output);
            }
            
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("오류:");
                Console.WriteLine(error);
            }
            
            Console.WriteLine($"프로세스 종료 코드: {process.ExitCode}");
        }
        else
        {
            Console.WriteLine("프로세스를 시작할 수 없습니다.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"dotnet 명령 실행 중 오류 발생: {ex.Message}");
    }
}