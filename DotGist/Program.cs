using ConsoleAppFramework;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

var app = ConsoleApp.Create();

// 기본 명령어 등록
app.Add<GistCommands>();

app.Run(args);
