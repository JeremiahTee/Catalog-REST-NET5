using Catalog.Api.Dtos;
using Catalog.Api.Models;

namespace Catalog.Api
{
    public static class Extensions {
        public static ItemDto AsDto(this Item item){
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate)
            {
                Id = item.Id,
                Description = item.Description,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}