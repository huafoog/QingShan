# 使用运行时镜像 5.0  SDK
FROM mcr.microsoft.com/dotnet/core/aspnet:5.0
# 设置工作目录
WORKDIR /app
# 把目录下的内容都复制到当前目录下
COPY . .
# 暴露9999端口
EXPOSE 9999
# 运行镜像入口命令和可执行文件名称
ENTRYPOINT ["dotnet", "QingShan.Core.Web.dll"]