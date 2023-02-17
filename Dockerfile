FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80

# copy csproj and restore as distinct layers
COPY *.props .
COPY *.sln .


COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY Contracts.DAL.Base/*.csproj ./Contracts.DAL.Base/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/

COPY Contracts.Domain/*.csproj ./Contracts.Domain/
COPY Domain.App/*.csproj ./Domain.App/
COPY Domain.Base/*.csproj ./Domain.Base/

COPY Mapper/*.csproj ./Mapper/
COPY WebDTO/*.csproj ./WebDTO/
COPY Tests/*.csproj ./Tests/

COPY WebApp/*.csproj ./WebApp/

RUN dotnet restore


# copy everything else and build app
COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY Contracts.DAL.Base/. ./Contracts.DAL.Base/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/

COPY Contracts.Domain/. ./Contracts.Domain/
COPY Domain.App/. ./Domain.App/
COPY Domain.Base/. ./Domain.Base/

COPY Mapper/. ./Mapper/
COPY WebDTO/. ./WebDTO/
COPY Tests/. ./Tests/

COPY WebApp/. ./WebApp/

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out



FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]
