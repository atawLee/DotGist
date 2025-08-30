using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text;
using DotGist.Core;

public class WebStringRetriever : IWebStringRetriever
{
    private readonly HttpClient _httpClient;

    public WebStringRetriever(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<string> GetStringAsync(string url, string querySelector)
    {
        var html = await _httpClient.GetStringAsync(url);
        return ExtractBySelector(html, querySelector);
    }

    public async Task<string> GetStringAsync(Uri uri)
    {
        return await _httpClient.GetStringAsync(uri);
    }

    // GitHub Gist에서 코드만 추출하는 전용 메서드
    public async Task<string> ExtractGistCodeAsync(string url)
    {
        var html = await _httpClient.GetStringAsync(url);
        return ExtractCodeFromGistHtml(html);
    }

    public string ExtractCodeFromGistHtml(string html)
    {
        var codeLinePattern = @"<td[^>]*class[^>]*blob-code blob-code-inner js-file-line[^>]*>(.*?)</td>";
        var matches = Regex.Matches(html, codeLinePattern, RegexOptions.Singleline);

        var codeBuilder = new StringBuilder();
        foreach (Match match in matches)
        {
            var codeContent = match.Groups[1].Value;
            // HTML 엔티티 디코딩 (확장된 버전)
            codeContent = DecodeHtmlEntities(codeContent);
            
            codeBuilder.AppendLine(codeContent);
        }

        return codeBuilder.ToString().Trim();
    }

    private static string DecodeHtmlEntities(string text)
    {
        return text
            // 기본 HTML 엔티티
            .Replace("&lt;", "<")
            .Replace("&gt;", ">")
            .Replace("&amp;", "&")
            .Replace("&quot;", "\"")
            .Replace("&apos;", "'")
            
            // 숫자 HTML 엔티티 (자주 사용되는 것들)
            .Replace("&#39;", "'")    // 작은따옴표
            .Replace("&#34;", "\"")   // 큰따옴표
            .Replace("&#38;", "&")    // 앰퍼샌드
            .Replace("&#60;", "<")    // 작다
            .Replace("&#62;", ">")    // 크다
            .Replace("&#32;", " ")    // 공백
            .Replace("&#160;", " ")   // 줄바꿈하지 않는 공백
            
            // 추가적인 일반적인 엔티티들
            .Replace("&nbsp;", " ")   // 줄바꿈하지 않는 공백
            .Replace("&copy;", "©")   // 저작권
            .Replace("&reg;", "®")    // 등록상표
            .Replace("&trade;", "™"); // 상표
    }

    // 기존 메서드들...
    private static string ExtractBySelector(string html, string selector)
    {
        // 기존 구현...
        if (selector.StartsWith('#'))
        {
            var id = selector[1..];
            var pattern = $@"<[^>]*id\s*=\s*[""']{Regex.Escape(id)}[""'][^>]*>(.*?)</[^>]*>";
            var match = Regex.Match(html, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return match.Success ? match.Groups[1].Value.Trim() : string.Empty;
        }
        
        if (selector.StartsWith('.'))
        {
            var className = selector[1..];
            var pattern = $@"<[^>]*class\s*=\s*[""'][^""']*{Regex.Escape(className)}[^""']*[""'][^>]*>(.*?)</[^>]*>";
            var match = Regex.Match(html, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return match.Success ? match.Groups[1].Value.Trim() : string.Empty;
        }

        var tagPattern = $@"<{Regex.Escape(selector)}[^>]*>(.*?)</{Regex.Escape(selector)}>";
        var tagMatch = Regex.Match(html, tagPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        return tagMatch.Success ? tagMatch.Groups[1].Value.Trim() : string.Empty;
    }
}