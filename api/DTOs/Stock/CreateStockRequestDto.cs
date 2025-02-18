using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Stock
{
    public class CreateStockRequestDto
    {

    [Required(ErrorMessage = "Symbol is required")]
    [MinLength(1, ErrorMessage = "Symbol must be at least 1 character long")]
    [MaxLength(5, ErrorMessage = "Symbol must be at most 5 characters long")]
    public string Symbol { get; set; } = string.Empty;

    [Required(ErrorMessage = "CompanyName is required")]
    [MinLength(2, ErrorMessage = "CompanyName must be at least 2 characters long")]
    [MaxLength(50, ErrorMessage = "CompanyName must be at most 50 characters long")]
    public string CompanyName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Purchase price is required")]
    [Range(1, 1000000000, ErrorMessage = "Purchase price must be between 1 and 1000000000")]
    public decimal Purchase { get; set; }

    [Required(ErrorMessage = "LastDiv is required")]
    [Range(0.001, 100, ErrorMessage = "LastDiv must be between 0.001 and 100")]
    public decimal LastDiv { get; set; }

    [Required(ErrorMessage = "Industry is required")]
    [MaxLength(10, ErrorMessage = "Industry must be at most 10 characters long")]
    public string Industry { get; set; } = string.Empty;

    [Required(ErrorMessage = "MarketCap is required")]
    [Range(1, 5000000000, ErrorMessage = "MarketCap must be between 1 and 5000000000")]
    public long MarketCap { get; set; }
    }
}