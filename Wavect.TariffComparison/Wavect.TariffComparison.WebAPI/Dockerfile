#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Wavect.TariffComparison.WebAPI/Wavect.TariffComparison.WebAPI.csproj", "Wavect.TariffComparison.WebAPI/"]
COPY ["Wavect.TariffComparison.Persistence/Wavect.TariffComparison.Persistence.csproj", "Wavect.TariffComparison.Persistence/"]
COPY ["Wavect.TariffComparison.Entities/Wavect.TariffComparison.Entities.csproj", "Wavect.TariffComparison.Entities/"]
COPY ["Wavect.TariffComparison.Interfaces/Wavect.TariffComparison.Interfaces.csproj", "Wavect.TariffComparison.Interfaces/"]
COPY ["Wavect.TariffComparison.DTOs/Wavect.TariffComparison.DTOs.csproj", "Wavect.TariffComparison.DTOs/"]
COPY ["Wavect.TariffComparison.Services/Wavect.TariffComparison.Services.csproj", "Wavect.TariffComparison.Services/"]
COPY ["Wavect.TariffComparison.GlobalConstants/Wavect.TariffComparison.GlobalConstants.csproj", "Wavect.TariffComparison.GlobalConstants/"]
RUN dotnet restore "Wavect.TariffComparison.WebAPI/Wavect.TariffComparison.WebAPI.csproj"
COPY . .
WORKDIR "/src/Wavect.TariffComparison.WebAPI"
RUN dotnet build "Wavect.TariffComparison.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wavect.TariffComparison.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wavect.TariffComparison.WebAPI.dll"]