using System;
using System.Collections.Generic;
using System.Text;

namespace DotGist.Core;

public interface IWebStringRetriever
{
    Task<string> GetStringAsync(string url, string querySelector);
    Task<string> GetStringAsync(Uri uri);
}
