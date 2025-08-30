using ConsoleAppFramework;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

var app = ConsoleApp.Create();

// 기본 명령어 등록
app.Add<GistCommands>();

app.Run(args);

/// <summary>
/// GitHub Gist에서 코드를 추출하고 파일로 저장하는 도구
/// </summary>
public class GistCommands
{
    /// <summary>
    /// GitHub Gist URL에서 코드를 추출하고 파일로 저장합니다.
    /// </summary>
    /// <param name="url">-u, GitHub Gist URL</param>
    /// <param name="output">-o, 출력 파일명 (미지정시 GUID로 생성)</param>
    /// <param name="execute">-e, 저장 후 dotnet publish 명령 실행</param>
    /// <param name="showCode">-s, 추출된 코드를 콘솔에 표시</param>
    /// <param name="format">-f, 출력 형식 (기본값: cs)</param>
    [Command("")]
    public async Task ExtractAsync(
        string url,
        string? output = null,
        bool execute = false,
        bool showCode = false,
        string format = "cs")
    {
        try
        {
            using var httpClient = new HttpClient();
            var retriever = new GitGistRetriever(httpClient);

            Console.WriteLine($"🔄 Gist URL에서 코드 추출 중: {url}");
            var extractedCode = await retriever.ExtractGistCodeAsync(url);

            if (showCode)
            {
                Console.WriteLine("\n📄 추출된 코드:");
                Console.WriteLine(new string('=', 60));
                Console.WriteLine(extractedCode);
                Console.WriteLine(new string('=', 60));
            }

            // 파일명 결정
            var fileName = !string.IsNullOrWhiteSpace(output) 
                ? output 
                : $"{Guid.NewGuid()}.{format}";
            
            // 확장자가 없으면 format 확장자 추가
            if (!Path.HasExtension(fileName))
            {
                fileName += $".{format}";
            }

            var filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            await File.WriteAllTextAsync(filePath, extractedCode);
            Console.WriteLine($"\n✅ 파일이 저장되었습니다: {fileName}");
            Console.WriteLine($"📁 전체 경로: {filePath}");

            if (execute)
            {
                await PublishDotnetCommand(fileName);
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"❌ 네트워크 오류: {ex.Message}");
            Environment.ExitCode = 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ 오류가 발생했습니다: {ex.Message}");
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 빠른 추출: 기본 설정으로 Gist에서 코드를 추출합니다.
    /// </summary>
    /// <param name="url">-u, GitHub Gist URL</param>
    public async Task QuickAsync(string url)
    {
        await ExtractAsync(url, execute: true, showCode: true);
    }

    /// <summary>
    /// Gist URL의 유효성을 검증합니다.
    /// </summary>
    /// <param name="url">-u, 검증할 GitHub Gist URL</param>
    public async Task Validate(string url)
    {
        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"✅ URL이 유효합니다: {url}");
                Console.WriteLine($"📊 상태 코드: {response.StatusCode}");
                Console.WriteLine($"📏 컨텐츠 길이: {response.Content.Headers.ContentLength ?? 0} bytes");
            }
            else
            {
                Console.WriteLine($"❌ URL에 접근할 수 없습니다: {url}");
                Console.WriteLine($"📊 상태 코드: {response.StatusCode}");
                Environment.ExitCode = 1;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ URL 검증 중 오류: {ex.Message}");
            Environment.ExitCode = 1;
        }
    }

    static async Task PublishDotnetCommand(string fileName)
    {
        try
        {
            Console.WriteLine($"\n🚀 dotnet publish 명령 실행 중: {fileName}");
            
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"publish {fileName}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processStartInfo);
            if (process != null)
            {
                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();
                
                await process.WaitForExitAsync();
                
                var output = await outputTask;
                var error = await errorTask;
                
                Console.WriteLine("\n📋 실행 결과");
                Console.WriteLine(new string('=', 50));
                
                if (!string.IsNullOrEmpty(output))
                {
                    Console.WriteLine("📤 출력:");
                    Console.WriteLine(output);
                }
                
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("⚠️  오류:");
                    Console.WriteLine(error);
                }
                
                var exitCodeEmoji = process.ExitCode == 0 ? "✅" : "❌";
                Console.WriteLine($"{exitCodeEmoji} 프로세스 종료 코드: {process.ExitCode}");
            }
            else
            {
                Console.WriteLine("❌ 프로세스를 시작할 수 없습니다.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ dotnet 명령 실행 중 오류 발생: {ex.Message}");
        }
    }
}