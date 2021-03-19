#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Server/ReviewService.Web.Server/ReviewService.Web.Server.csproj", "src/Server/ReviewService.Web.Server/"]
COPY ["src/Shared/ReviewService.Shared/ReviewService.Shared.csproj", "src/Shared/ReviewService.Shared/"]
COPY ["src/Server/ReviewService.Infrastructure/ReviewService.Infrastructure.csproj", "src/Server/ReviewService.Infrastructure/"]
COPY ["src/Server/ReviewService.Application/ReviewService.Application.csproj", "src/Server/ReviewService.Application/"]
COPY ["src/Server/ReviewService.Domain/ReviewService.Domain.csproj", "src/Server/ReviewService.Domain/"]
COPY ["src/Client/ReviewService.Blazor.Client/ReviewService.Blazor.Client.csproj", "src/Client/ReviewService.Blazor.Client/"]
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