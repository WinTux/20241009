FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/SDK:8.0 AS build
WORKDIR /src
COPY ["PreGrado.csproj",""]
RUN dotnet restore "./PreGrado.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PreGrado.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PreGrado.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","PreGrado.dll"]
