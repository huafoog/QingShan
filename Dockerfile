#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["QingShan.Web/QingShan.Web.csproj", "QingShan.Web/"]
COPY ["QingShan.ServiceLayer/QingShan.Services.csproj", "QingShan.ServiceLayer/"]
COPY ["QingShan.DataLayer/QingShan.DataLayer.csproj", "QingShan.DataLayer/"]
COPY ["QingShan.Core/QingShan.Core.csproj", "QingShan.Core/"]
COPY ["QingShan/QingShan.csproj", "QingShan/"]
RUN dotnet restore "QingShan.Web/QingShan.Web.csproj"
COPY . .
WORKDIR "/src/QingShan.Web"
RUN dotnet build "QingShan.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QingShan.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QingShan.Web.dll"]