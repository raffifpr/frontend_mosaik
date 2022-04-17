#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Mosaik.idAPI/Mosaik.idAPI.csproj", "Mosaik.idAPI/"]
RUN dotnet restore "Mosaik.idAPI/Mosaik.idAPI.csproj"
COPY . .
WORKDIR "/src/Mosaik.idAPI"
RUN dotnet build "Mosaik.idAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mosaik.idAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Mosaik.idAPI.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Mosaik.idAPI.dll