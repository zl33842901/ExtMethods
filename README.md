# ExtMethods

�����õ���һЩ��չ����

### Ч��

һЩ��������ú�˳��

### ��װ�������

dotnet add package xLiAd.ExtMethods

### ʹ�÷���������

1���ַ���ת������
```csharp
string s = "2";
int i = s.ToInt(0); //0��ת��ʧ��ʱ��Ĭ��ֵ
```
2���ַ���תʱ�䣺
```csharp
string s = "2020-07-16 22:31:00";
DateTime? t = s.ToDateTime();
```
3���ж��ַ����Ƿ�Ϊ�գ�
```csharp
string s = null;
bool b = s.NullOrEmpty();
```
4���������ַ�����ת��
```csharp
int[] arr = new int[] { 1, 2, 3, 4, 5 };
string s = arr.ToStringBy(",");// s ֵ�� "1,2,3,4,5"
List<int> list = s.ToListIntByDot(); // list ֵ�� new List<int>() { 1, 2, 3, 4, 5 }
```

�������⣬���У�

ʱ�����ʱ�以ת�ķ������ַ�����ȡ���ַ���ת float decimal��ö�����Ͳ��� �ȵȡ���