﻿# QingShan 青杉基础NuGet包

## Cache 缓存

  `ICache`.cs 所有需要实现的缓存都继承自该接口

## Converts 转换

- `Base64Convert.cs `Base64转换

  - `IsBase64(string base64Str, out byte[] bytes)`判断是否是base64字符串转换为一个等效的8位无符号整数数组。  

    ```
    if(Base64Convert.IsBase64(base64String,out byte[] bytes)){
    	
    }
    ```

    

  - `FileToBase64(Stream fs)`文件转换成Base64字符串

    ```
    ```

    

  - `Base64ToFileAndSave(byte[] bytes, string url)`Base64字符串转换成文件

    ````
    string url = FileHelper.GetDirectory("Uploads")+Guid.NewGuid()+".jpg".ToLocalBinDirectory();
    Base64ToFileAndSave(bytes,url);
    ````

    

## Data 数据

  该文件夹中用于基础数据整合

## DatabaseAccessor 数据访问

  数据访问基础文件

## DependencyInjection 依赖注入

  自动依赖注入实现

## Exceptions 异常

## Extensions 拓展

## Utilities  工具类

