#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["tiendung99.SignalR.MVC/tiendung99.SignalR.MVC.csproj", "tiendung99.SignalR.MVC/"]
RUN dotnet restore "tiendung99.SignalR.MVC/tiendung99.SignalR.MVC.csproj"
COPY . .
WORKDIR "/src/tiendung99.SignalR.MVC"
RUN dotnet build "tiendung99.SignalR.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tiendung99.SignalR.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "tiendung99.SignalR.MVC.dll"]