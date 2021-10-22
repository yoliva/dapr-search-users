# dapr-search-users

Note: ideally each project will be in a separate repo, but for this exercise I think it is best to submit everything within the same repository

## Migrations

### Create New Migrations

dotnet ef migrations add Initial -o .\Migrations --project ..\Oiga.SearchService.Data
dotnet ef migrations add Initial -o .\Migrations --project ..\Oiga.UserService.Data

### Apply changes

dotnet ef database update

## Startup services

`UserService`
dapr run --app-id user-service --components-path ../../../components dotnet run

`SearchService`
dapr run --app-id search-service --components-path ../../../../components dotnet run
`UI`
npm start

## Solution structure

components (custom dapr components)
src

- nugets (custom nugets for solution)

- search-service (microservice to process user search actions)

- user-service (microservice to process register/profile actions)

- ui (ui project with options for search,create,see profile)
