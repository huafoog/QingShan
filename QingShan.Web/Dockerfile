# ʹ������ʱ����
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
# ���ù���Ŀ¼
WORKDIR /app
# ��Ŀ¼�µ����ݶ����Ƶ���ǰĿ¼��
COPY . .
# ��¶9999�˿�
EXPOSE 9999
# ���о����������Ϳ�ִ���ļ�����
ENTRYPOINT ["dotnet", "QingShan.Core.Web.dll"]