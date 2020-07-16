# ExtMethods

经常用到的一些扩展方法

### 效果

一些操作将变得很顺手

### 安装程序包：

dotnet add package xLiAd.ExtMethods

### 使用方法举例：

1，字符串转整数：
```csharp
string s = "2";
int i = s.ToInt(0); //0是转换失败时的默认值
```
2，字符串转时间：
```csharp
string s = "2020-07-16 22:31:00";
DateTime? t = s.ToDateTime();
```
3，判断字符串是否为空：
```csharp
string s = null;
bool b = s.NullOrEmpty();
```
4，数组与字符串互转：
```csharp
int[] arr = new int[] { 1, 2, 3, 4, 5 };
string s = arr.ToStringBy(",");// s 值是 "1,2,3,4,5"
List<int> list = s.ToListIntByDot(); // list 值是 new List<int>() { 1, 2, 3, 4, 5 }
```

除上述外，还有：

时间戳与时间互转的方法、字符串截取、字符串转 float decimal、枚举类型操作 等等……