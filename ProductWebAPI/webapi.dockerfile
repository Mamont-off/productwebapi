FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProductWebAPI.csproj", "ProductWebAPI/"]
RUN dotnet restore "ProductWebAPI/ProductWebAPI.csproj"
WORKDIR /src
COPY . ProductWebAPI/
RUN dotnet build "ProductWebAPI/ProductWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src
RUN dotnet publish "ProductWebAPI/ProductWebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductWebAPI.dll"]