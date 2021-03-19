#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Server/ReviewService.Web.Server/ReviewService.Web.Server.csproj", "src/Server/ReviewService.Web.Server/"]
RUN dotnet restore "src/Server/ReviewService.Web.Server/ReviewService.Web.Server.csproj"
COPY . .
WORKDIR "/src/src/Server/ReviewService.Web.Server"
RUN dotnet build "ReviewService.Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReviewService.Web.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReviewService.Web.Server.dll"]