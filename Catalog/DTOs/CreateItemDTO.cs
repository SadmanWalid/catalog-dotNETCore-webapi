using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOs
{

    public record CreateItemDTO()
    {
        [Required]
        public string Name { get; init; }
        [Range(1,50)]
        public decimal Price { get; init; }

    }

}