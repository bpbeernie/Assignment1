FROM microsoft/aspnetcore-build:2.1 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/aspnetcore:2.1
WORKDIR /app
COPY --from=build-env /app/out .
COPY entrypoint.sh .
ENTRYPOINT ["dotnet", "aspnetapp.dll"]

EXPOSE 80/tcp
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh