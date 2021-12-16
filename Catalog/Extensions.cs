using Catalog.DTOs;
using Catalog.Entities;

namespace Catalog
{
    public static class Entensions
    {
        public static ItemDTO AsDTO(this Item item)
        {
            return new ItemDTO(){
             Id=item.Id,
             Name = item.Name,
             Price = item.Price,
             CreatedDate = item.CreatedDate 
            };

        }
    }

}