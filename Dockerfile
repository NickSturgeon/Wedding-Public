FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
WORKDIR /app

RUN apk add --update npm
COPY package.json ./
COPY package-lock.json ./
RUN npm install

COPY Wedding.csproj ./
COPY tailwind.config.js ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5555

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Wedding.dll"]
