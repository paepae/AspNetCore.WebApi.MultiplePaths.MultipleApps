FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

COPY ./ .
RUN dotnet restore
RUN dotnet publish -c release -o /release --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /release
COPY --from=build /release ./
ENTRYPOINT ["dotnet", "PaePae.Test.AspNetCore.WebApi.MultiplePaths.MultipleApps.V1.dll"]
