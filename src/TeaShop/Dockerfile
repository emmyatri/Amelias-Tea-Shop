
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY TeaShop.sln .
COPY TeaShop/TeaShop.csproj TeaShop/
COPY TeaShop.UnitTest/TeaShop.UnitTest.csproj TeaShop.UnitTest/
RUN dotnet restore

COPY . .
RUN dotnet test TeaShop.UnitTest --configuration Release --no-restore

RUN dotnet publish TeaShop/TeaShop.csproj --configuration Release --output /app --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LC_ALL=en_US.UTF-8

ENTRYPOINT ["dotnet", "TeaShop.dll"]