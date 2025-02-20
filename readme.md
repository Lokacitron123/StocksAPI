# ASP.NET 8 Web API

## Summary

This is a web API built with ASP.NET 8 using Entity Framework Core and JWT authentication. I built it as an introduction to the .NET ecosystem with guidance from **Teddy Smith**. Object was to take a quick dive into programming with C# in .NET to explore further ways of creating applications for the web.

## Technologies Used 🚀

- ⚡ **.NET 8** – Core framework for building the API
- 🗄️ **Entity Framework Core 9** – ORM for database interactions
- 🔑 **JWT Authentication** – Secure user authentication
- 📜 **Swashbuckle (Swagger)** – API documentation
- 🛢 **SQL Server** – SQL Server Management Studio (SSMS), Database management tool

## How to Run

1. **Clone the repository**
   ```sh
   git clone https://github.com/Lokacitron123/StocksAPI.git
   cd api
   ```
2. **Restore dependencies**
   ```sh
   dotnet restore
   ```
3. **Prepare DB**
   ```sh
   dotnet ef migrations add CreateDB
   ```
4. **Update the database (if using migrations)**
   ```sh
   dotnet ef database update
   ```
5. **Run the application**
   ```sh
   dotnet watch run
   ```
6. **Access Swagger UI** (if enabled)
   - Navigate to `http://localhost:<your-port>/swagger`

## Shoutout

Special thanks to [teddysmithdev](https://github.com/teddysmithdev) for a a great tutorial on .NET and ASP.NET & Entity Framework.
