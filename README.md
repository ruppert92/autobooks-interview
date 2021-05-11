# autobooks-interview
This project is for a simple grocery store api. Current functionality allows you to interact with the grocery store customers.

## Configuration required
In the appsettings.json set GroceryStore.GroceryStoreDbFilePath to the file path of a json file that you would like to use as the database.

Database schema:
```json 
{
  "customers": [ {
    "id": 0,
    "name": "string"
  } ]
}
```

## How to run
Option 1: Open in Visual Studio 2019 and run using IIS Express or by running the GroceryStore.API

Option 2: Navigate to the GroceryStore.API project folder. Enter the following command in the terminal

`dotnet run --project GroceryStore.API.csproj`
