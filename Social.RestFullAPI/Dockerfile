#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Social.RestFullAPI/Social.RestFullAPI.csproj", "Social.RestFullAPI/"]
COPY ["Social.Service/Social.Service.csproj", "Social.Service/"]
COPY ["Social.Model/Social.Model.csproj", "Social.Model/"]
COPY ["Social.DAL.Contas/Social.DAL.Contas.csproj", "Social.DAL.Contas/"]
RUN dotnet restore "Social.RestFullAPI/Social.RestFullAPI.csproj"
COPY . .
WORKDIR "/src/Social.RestFullAPI"
RUN dotnet build "Social.RestFullAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Social.RestFullAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Social.RestFullAPI.dll"]