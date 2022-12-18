#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
ENV ConnectionStringsDockerConStr="Server=db;Database=DefaultDatabase;User=ENVDBU;Password=ENVDBPW;"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DefaultDatabase.csproj", "."]
RUN dotnet restore "./DefaultDatabase.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DefaultDatabase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DefaultDatabase.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DefaultDatabase.dll"]