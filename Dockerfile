FROM microsoft/dotnet:2.1-sdk-alpine AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY TraktDl.Business/*.csproj ./TraktDl.Business/
COPY TraktDl.Web/*.csproj ./TraktDl.Web/
RUN dotnet restore

# copy everything else and build app
COPY TraktDl.Business/. ./TraktDl.Business/
COPY TraktDl.Web/. ./TraktDl.Web/
WORKDIR /app/TraktDl.Web
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS runtime
WORKDIR /app

EXPOSE 5000

COPY --from=build /app/TraktDl.Web/out ./
ENTRYPOINT ["dotnet", "TraktDl.Web.dll"]

