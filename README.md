# paylocityapi

Paylocity API is an ASP.NET Web API that displays a user grid to input employees and their dependents. It will also calculate their benefit costs. 

## Prerequisites 

1. Visual Studio 2017 or later
2. Sql Server Management Studio or Azure Data Studio for Mac users

## Deployment
Will need to restore the database on local server using the PaylocityDatabase bak file.

To build code, run Paylocity DB on local server and run API locally in VS.

To test API calls, run API locally on VS and add "/swagger" on the end of the localhost url (ex. http://127.0.0.1:8080/swagger). This will open Swagger UI which allows the user to see the API calls and their parameters as well as run each call. 

The API call GetAllDependents will return a list of data objects.
Ex. 

```json
{
    "EmployeeId": 1,
    "DependentId": 1,
    "LastName": "Scott",
    "FirstName": "Joe",
    "BenefitCost": 500,
    "Discount": 0
  }
 ```
## Built With
* Docker
* Azure Data Studio
* Visual Studio 2019 for Mac
* Kendo UI for jQuery

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
