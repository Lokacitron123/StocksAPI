# .NET Web API with Entity Framework and SQL Database

## Overview

This project is a **.NET Web API** using **ASP.NET Core 8.0**, **Entity Framework Core (EF Core)** and a local **SQL database**. The API follows a structured design with **Models, DTOs, Mappers, Repositories, and Controllers**.

## Project Structure

```
/my-api
│── Controllers/        # Handles HTTP requests and responses
│── Models/            # Defines database entities
│── DTOs/              # Data Transfer Objects for request/response
│── Mappers/           # Converts between Models and DTOs
│── Data/              # Database context and migrations
│── Interfaces/        # Defines repository contracts
│── Repository/        # Handles database operations
│── Program.cs         # Entry point of the application
│── appsettings.json   # Configuration file (DB connection, logging, etc.)
```

## Key Concepts

### 1. **Models (Database Entities)**

Defines the **data structure** used in the database.

**Example:**

```csharp
public class Stock {
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
}
```

### 2. **DTOs (Data Transfer Objects)**

Used to transfer structured data between layers.

**Example:**

```csharp
public class StockDto {
    public int Id { get; set; }
    public string Symbol { get; set; }
}
```

### 3. **Mappers (Model <-> DTO Conversion)**

Handles transformation between **Models** and **DTOs**.

**Example:**

```csharp
public static class StockMappers {
    public static StockDto ToStockDto(this Stock stock) {
        return new StockDto { Id = stock.Id, Symbol = stock.Symbol };
    }
}
```

### 4. **Data (Database Context & Migrations)**

Manages database connections and queries using EF Core.

**Example:**

```csharp
public class AppDbContext : DbContext {
    public DbSet<Stock> Stocks { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
}
```

### 5. **Interfaces (Repository Contracts)**

Defines methods for interacting with the database.

**Example:**

```csharp
public interface IStockRepository {
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> FindAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
    Task<Stock?> DeleteAsync(int id);
}
```

### 6. **Repositories (Database Logic)**

Implements database operations using EF Core.

**Example:**

```csharp
public class StockRepository : IStockRepository {
    private readonly AppDbContext _context;
    public StockRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<List<Stock>> GetAllAsync() {
        return await _context.Stocks.ToListAsync();
    }
}
```

### 7. **Controllers (Handling API Requests)**

Defines HTTP endpoints and interacts with repositories.

**Example:**

```csharp
[ApiController]
[Route("api/stocks")]
public class StocksController : ControllerBase {
    private readonly IStockRepository _stockRepository;
    public StocksController(IStockRepository stockRepository) {
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetStocks() {
        var stocks = await _stockRepository.GetAllAsync();
        return Ok(stocks.Select(s => s.ToStockDto()));
    }
}
```

### Data Validation

#### Validation in HTTP Requests

To ensure that API endpoints receive properly formatted data, route constraints are used in controller method attributes. For example, specifying {id:int} ensures that only integer values are accepted for the id parameter.

```csharp

[HttpGet("{id:int}")]
public IActionResult GetComment(int id)
{
return Ok($"Requested ID: {id}");
}
```

If a non-integer value (e.g., /api/comments/abc) is passed, the request will not reach the method, preventing unnecessary processing and errors.

#### Validation in DTOs

In this project, data validation is handled using data annotations in DTOs to ensure that incoming requests contain valid data before reaching the business logic layer. This approach keeps validation concerns separate from database models and improves maintainability.

Example DTO validation:

```csharp
public class CommentDto
{
[Required(ErrorMessage = "Title is required")]
[MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]
[MaxLength(255, ErrorMessage = "Title must be at most 255 characters long")]
public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Content is required")]
    [MinLength(3, ErrorMessage = "Content must be at least 3 characters long")]
    [MaxLength(500, ErrorMessage = "Content must be at most 500 characters long")]
    public string Content { get; set; } = string.Empty;

}
```

#### Validation in Controllers

When handling API requests, validation is enforced using ModelState.IsValid in controllers. If validation fails, a 400 Bad Request response is returned with error details.

Example controller method:

```csharp
[HttpPost]
public IActionResult CreateComment([FromBody] CommentDto dto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Proceed with creating the comment...
    return Ok("Comment created successfully");
}

```

By leveraging ModelState, we ensure that only valid requests are processed, improving API reliability and security.

### Happy Coding! 🚀
