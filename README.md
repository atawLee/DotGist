# DotGist ??

GitHub Gist���� C# �ڵ带 �����Ͽ� .NET 10 ���� ���� ������ �����ϴ� ����� �����Դϴ�.

## ?? �ֿ� ���

- GitHub Gist URL���� C# �ڵ� ����
- ����� �ڵ带 ���� ���Ϸ� ����
- .NET 10 ���� ���� �� ���� (csproj ����)
- Gist URL ��ȿ�� ����

## ?? �ʼ� �䱸����

- **.NET 10 SDK** (�ʼ�)

## ?? ����

### �⺻ ��ɾ�

```bash
# �⺻ ����
dotgist -u "https://gist.github.com/username/gist-id"

# ���ϸ� �����Ͽ� ����
dotgist -u "https://gist.github.com/username/gist-id" -o "MyApp.cs"

# �ڵ� �̸�����
dotgist -u "https://gist.github.com/username/gist-id" -s

# ���� �� �ڵ� ����
dotgist -u "https://gist.github.com/username/gist-id" -e
```

### ���� ��ɾ�

```bash
# �ڵ� �̸����� + �ڵ� ����
dotgist quick -u "https://gist.github.com/username/gist-id"
```

### URL ����

```bash
# Gist URL ��ȿ�� Ȯ��
dotgist validate -u "https://gist.github.com/username/gist-id"
```

## ?? �ɼ� ����

| �ɼ� | ���� | �⺻�� |
|------|------|--------|
| `-u` | GitHub Gist URL (�ʼ�) | - |
| `-o` | ��� ���ϸ� | GUID �ڵ����� |
| `-e` | ���� �� dotnet publish ���� | false |
| `-s` | ���� �ڵ� �ܼ� ǥ�� | false |
| `-f` | ���� Ȯ���� | cs |

## ?? ��� ����

```bash
# 1. ������ ����
dotgist -u "https://gist.github.com/example/123abc"

# 2. �̸������ �Բ� ����
dotgist -u "https://gist.github.com/example/123abc" -s -o "HelloWorld.cs"

# 3. ���� ���� �� ����
dotgist quick -u "https://gist.github.com/example/123abc"
```

## ?? ���� ���

```bash
git clone https://github.com/atawLee/DotGist.git
cd DotGist
dotnet publish -c Release
```

---

**Note**: .NET 10 SDK�� ��ġ�Ǿ� �־�� �մϴ�.