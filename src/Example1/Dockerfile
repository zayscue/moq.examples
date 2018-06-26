FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY src/Example1/Example1.csproj src/Example1/
RUN dotnet restore src/Example1/Example1.csproj
COPY . .
WORKDIR /src/src/Example1
RUN dotnet build Example1.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Example1.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Example1.dll"]
