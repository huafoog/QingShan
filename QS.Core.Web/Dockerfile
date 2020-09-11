#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["QS.Core.Web/QS.Core.Web.csproj", "QS.Core.Web/"]
COPY ["QS.ServiceLayer/QS.ServiceLayer.csproj", "QS.ServiceLayer/"]
COPY ["QS.DataLayer/QS.DataLayer.csproj", "QS.DataLayer/"]
COPY ["QS.Core/QS.Core.csproj", "QS.Core/"]
RUN dotnet restore "QS.Core.Web/QS.Core.Web.csproj"
COPY . .
WORKDIR "/src/QS.Core.Web"
RUN dotnet build "QS.Core.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QS.Core.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QS.Core.Web.dll"]