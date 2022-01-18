using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controller
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }
        //Get /items
        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync())
            .Select(item => item.AsDTO());
            return items;

        }

        // GET /item/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return item.AsDTO();

        }
        //POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItemAsync(CreateItemDTO itemDTO)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDTO.Name,
                Price = itemDTO.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDTO());

        }
        // PUT / items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDTO itemDTO)
        {

            var existingItem = await repository.GetItemAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDTO.Name,
                Price = itemDTO.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();


        }
        // DELETE /item/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {

            var item = await repository.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}