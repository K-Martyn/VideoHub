FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["VideoHub/VideoHub.csproj", "VideoHub/"]
RUN dotnet restore "VideoHub/VideoHub.csproj"
COPY . .
WORKDIR "/src/VideoHub"
RUN dotnet build "VideoHub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VideoHub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VideoHub.dll"]
