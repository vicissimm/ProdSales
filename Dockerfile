FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProdSales/ProdSales.csproj", "ProdSales/"]
RUN dotnet restore "ProdSales/ProdSales.csproj"
COPY . .
WORKDIR  "/src/ProdSales"
RUN dotnet build "ProdSales.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "ProdSales.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProdSales.dll", "--urls", "http://127.0.0.1:80"]