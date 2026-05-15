# Inventory App

A simple product inventory manager. The backend is an ASP.NET Core Web API using CQRS with MediatR, Entity Framework Core for persistence, and SQLite as the database. The frontend is an Angular 18 app.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org)

## Running Locally

You need two terminals — one for the API, one for the frontend.

### 1. Start the API

```bash
cd src/InventoryApi
dotnet run --launch-profile http
```
You should see:

```
Now listening on: http://localhost:5000
```

Swagger UI is at `http://localhost:5000/swagger` if you want to test the API directly.

### 2. Start the Frontend

In a second terminal:

```bash
cd src/inventory-web
npm install       # only needed the first time
npm start
```

You should see:

```
Local: http://localhost:4200/
```

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/products` | List all products |
| GET | `/api/products/{id}` | Get a single product |
| POST | `/api/products` | Create a product |
| PUT | `/api/products/{id}` | Update a product |
| DELETE | `/api/products/{id}` | Delete a product |

**Product payload**

```json
{
  "name": "Wireless Mouse",
  "sku": "WM-001",
  "price": 29.99,
  "stockQuantity": 50,
  "category": "Electronics"
}
```

**Validation rules**

- `name` and `sku` are required and cannot be blank
- `price` must be greater than zero
- `sku` must be unique across all products

Validation errors return HTTP 400 with a field-level breakdown. SKU conflicts return HTTP 409.
