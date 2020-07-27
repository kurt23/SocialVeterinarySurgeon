FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# copy csproj and restore as distinct layers
COPY . /app

WORKDIR /app/sources/SocialVeterinarySurgeon

RUN dotnet restore && \
    dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

COPY --from=build /app/sources/SocialVeterinarySurgeon/out /out

WORKDIR /out

ENTRYPOINT ["dotnet", "SocialVeterinarySurgeon.dll"]

