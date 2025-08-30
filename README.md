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
dotgist quick-async -u "https://gist.github.com/username/gist-id"
```

### URL ����

```bash
# Gist URL ��ȿ�� Ȯ��
dotgist validate-async -u "https://gist.github.com/username/gist-id"
```

## ?? ��ɾ� ���

| ��ɾ� | ���� | �ɼ� |
|--------|------|------|
| (�⺻) | �ڵ� ���� �� ���� | `-u`, `-o`, `-e`, `-s`, `-f` |
| `quick-async` | ���� ���� (�̸����� + ����) | `-u` |
| `validate-async` | URL ��ȿ�� ���� | `-u` |

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
dotgist quick-async -u "https://gist.github.com/example/123abc"

# 4. URL ���� �� �۾�
dotgist validate-async -u "https://gist.github.com/example/123abc"
# ? URL�� ��ȿ�մϴ�: https://gist.github.com/example/123abc
# ?? ���� �ڵ�: OK
# ?? ������ ����: 1234 bytes
```

## ?? ���� ���

```bash
git clone https://github.com/atawLee/DotGist.git
cd DotGist
dotnet publish -c Release
```

---

**Note**: .NET 10 SDK�� ��ġ�Ǿ� �־�� �մϴ�.