#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/services/Social.IdentidadeAPI/Social.IdentidadeAPI.csproj", "src/services/Social.IdentidadeAPI/"]
RUN dotnet restore "src/services/Social.IdentidadeAPI/Social.IdentidadeAPI.csproj"
COPY . .
WORKDIR "/src/src/services/Social.IdentidadeAPI"
RUN dotnet build "Social.IdentidadeAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Social.IdentidadeAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Social.IdentidadeAPI.dll"]