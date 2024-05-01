
# Simple Location API

An API to track the locations of Users


## Tech Stack

- .NET 8 (Requires .NET 8 SDK)
- Application Insights
- MediatR
- ASP.NET Integration Tests


## Run Locally

Clone the project

```bash
  git clone https://github.com/ReeceNoone/SampleApi
```

Go to the project directory

```bash
  cd SampleApi
```

Build

```bash
  dotnet build
```

Run the Tests

```bash
  dotnet test
```


Go to the API directory

```bash
cd src/Locator.WebApi
```

Run the API

```bash
  dotnet run
```


## API Reference

The requests can be run from the swagger page when the app is in development mode. These endpoints are available:

### Users
#### Get a User

```http
  GET /api/users/{userId}
```

Gets a user by ID

#### Create User

```http
  POST /api/users
```

Creates a user

### Locations

#### Get Current Location

```http
  GET /api/locations/{userId}/current
```

Gets the current location of a user by User Id

#### Set Current Location

```http
  PUT /api/locations/{userId}/current
```

Sets the current location of a user

#### Get Location History

```http
  GET /api/locations/{userId}/history
```

Gets the location history of a user

#### Get Current Location (All Users)

```http
  GET /api/locations/current
```

Gets the current location of all users


### Suggested Usage to Test

The following requests are suggested to test the functionality of the API

1. ```POST /api/users```
2. ```PUT /api/locations/{userId1}/current```
3. ```GET /api/locations/{userId1}/current```
4. ```PUT /api/locations/{userId1}/current```
5. ```GET /api/locations/{userId1}/history```
6. ```POST /api/users```
2. ```PUT /api/locations/{userId2}/current```
7. ```GET /api/locations/current```