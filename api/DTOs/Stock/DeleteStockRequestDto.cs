using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class DeleteStockRequestDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
    }
}