FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY src/Assignment.Domain/Assignment.Domain.csproj ./src/Assignment.Domain/
COPY src/Assignment.Application/Assignment.Application.csproj ./src/Assignment.Application/
COPY src/Assignment.Infrastructure/Assignment.Infrastructure.csproj ./src/Assignment.Infrastructure/
COPY src/Assignment.Api/Assignment.Api.csproj ./src/Assignment.Api/
RUN dotnet restore ./src/Assignment.Api/Assignment.Api.csproj

COPY . ./
RUN dotnet publish ./src/Assignment.Api -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 5432
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Assignment.Api.dll"]
