FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY insuranceonaspdotnet/Api.csproj insuranceonaspdotnet/
RUN dotnet restore insuranceonaspdotnet/Api.csproj

COPY insuranceonaspdotnet/ insuranceonaspdotnet/

RUN dotnet publish insuranceonaspdotnet/Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]