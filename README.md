# DotGist ??

GitHub Gist에서 C# 코드를 추출하여 .NET 10 단일 파일 앱으로 빌드하는 명령줄 도구입니다.

## ?? 주요 기능

- GitHub Gist URL에서 C# 코드 추출
- 추출된 코드를 로컬 파일로 저장
- .NET 10 단일 파일 앱 빌드 (csproj 없이)
- Gist URL 유효성 검증

## ?? 필수 요구사항

- **.NET 10 SDK** (필수)

## ?? 사용법

### 기본 명령어

```bash
# 기본 추출
dotgist -u "https://gist.github.com/username/gist-id"

# 파일명 지정하여 추출
dotgist -u "https://gist.github.com/username/gist-id" -o "MyApp.cs"

# 코드 미리보기
dotgist -u "https://gist.github.com/username/gist-id" -s

# 추출 후 자동 빌드
dotgist -u "https://gist.github.com/username/gist-id" -e
```

### 빠른 명령어

```bash
# 코드 미리보기 + 자동 빌드
dotgist quick -u "https://gist.github.com/username/gist-id"
```

### URL 검증

```bash
# Gist URL 유효성 확인
dotgist validate -u "https://gist.github.com/username/gist-id"
```

## ?? 옵션 설명

| 옵션 | 설명 | 기본값 |
|------|------|--------|
| `-u` | GitHub Gist URL (필수) | - |
| `-o` | 출력 파일명 | GUID 자동생성 |
| `-e` | 저장 후 dotnet publish 실행 | false |
| `-s` | 추출 코드 콘솔 표시 | false |
| `-f` | 파일 확장자 | cs |

## ?? 사용 예제

```bash
# 1. 간단한 추출
dotgist -u "https://gist.github.com/example/123abc"

# 2. 미리보기와 함께 추출
dotgist -u "https://gist.github.com/example/123abc" -s -o "HelloWorld.cs"

# 3. 빠른 추출 및 빌드
dotgist quick -u "https://gist.github.com/example/123abc"
```

## ?? 빌드 방법

```bash
git clone https://github.com/atawLee/DotGist.git
cd DotGist
dotnet publish -c Release
```

---

**Note**: .NET 10 SDK가 설치되어 있어야 합니다.