using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Stock.API.DTOS;
using Stock.API.Error;
using Stock.API.Helpers;
using Stock.Core;
using Stock.Core.Entites.Items;
using Stock.Core.Specifications;
using Stock.Repository;

namespace Stock.API.Controllers
{
    public class ItemsController : APIBaseController
    {
        private readonly IUnitofwork unitofwork;
        private readonly IMapper mapper;

        public ItemsController(IUnitofwork unitofwork, IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;
        }

        #region EndPoint GetAllItems
        [ProducesResponseType(typeof(IReadOnlyList<ItemToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ItemToReturnDto), StatusCodes.Status404NotFound)]
        [HttpGet("GetAllItems")]
        public async Task<ActionResult<IReadOnlyList<ItemToReturnDto>>> GetAllSuppliers()
        {
            var spec = new ItemsWithItemTypesandSupplierSpecification();
            var Items = await unitofwork.Repository<Item>().GetAllWithSpecAsync(spec);
           
            if (Items == null || !Items.Any())
            {
                return NotFound();
            }
            return Ok(Items);
        }
        #endregion

        #region EndPoint AddItem
        [ProducesResponseType(typeof(ItemToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ItemToReturnDto), StatusCodes.Status400BadRequest)]
        [HttpPost("AddItem")]
        public async Task<ActionResult<ItemToReturnDto>> Add([FromForm] ItemToReturnDto itemDto)
        {
            try
            {
                if (itemDto.Image == null)
                {
                    ModelState.AddModelError("Image", "The Image field is required.");
                    return BadRequest(ModelState);
                }
                var item = mapper.Map<ItemToReturnDto, Item>(itemDto);

                // Save the image file
                item.ImageName = DocumentSettings.UploadFile(itemDto.Image, "Images");

                await unitofwork.Repository<Item>().AddAsync(item);
                await unitofwork.CompleteAsync();

                // Update the itemDto with the saved ImageName
                itemDto.ImageName = item.ImageName;

                var returnItem = mapper.Map<Item, ItemDto>(item);

                var response = new
                {
                    response = new APIResponse(200, "Item Successfully created.")
                };

                return Ok(new { returnItem, response });
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                return StatusCode(500, new APIResponse(500, $"Internal Server Error: {ex.Message}"));
            }
        }
        #endregion

        #region EndPoint UpdateItem
        [ProducesResponseType(typeof(ItemToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ItemToReturnDto), StatusCodes.Status400BadRequest)]
        [HttpPut("UpdateItem/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ItemToReturnDto itemToReturnDto)
        {
            try
            {
                var existingitem = await unitofwork.Repository<Item>().GetByIdAsync(id);

                if (existingitem == null)
                {
                    return NotFound(new APIResponse(404, "Item Not Found"));
                }

                if (itemToReturnDto.Image is not null)
                {
                    // Upload the new image
                    itemToReturnDto.ImageName = DocumentSettings.UploadFile(itemToReturnDto.Image, "Images");

                    // Delete the image 
                    if (!string.IsNullOrEmpty(existingitem.ImageName))
                    {
                        DocumentSettings.DeleteFile(existingitem.ImageName, "Images");

                    }
                }
                unitofwork.Repository<Item>().Detach(existingitem);

                // Update  Item details
                var mappedItem = mapper.Map<ItemToReturnDto, Item>(itemToReturnDto);
                unitofwork.Repository<Item>().Update(mappedItem);
                await unitofwork.CompleteAsync();



                var returnItem = mapper.Map<Item, ItemDto>(mappedItem);

                var response = new
                {
                    response = new APIResponse(200, "Item Successfully Updated.")
                };

                return Ok(new { returnItem, response });

      
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                return StatusCode(500, new APIResponse(500, $"Internal Server Error: {ex.Message}"));
            }
        }
        #endregion

        #region EndPoint DeleteItem
        [ProducesResponseType(typeof(ItemToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ItemToReturnDto), StatusCodes.Status400BadRequest)]
        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item =await unitofwork.Repository<Item>().GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound(new APIResponse(404, "Item Not Found"));
                }
                
                unitofwork.Repository<Item>().Delete(item);
                int result = await unitofwork.CompleteAsync();
                if (result > 0)
                {
                    DocumentSettings.DeleteFile(item.ImageName, "Images");
                }
                var response = new
                {
                    resonse = new APIResponse(202, $"Item {id} Successfully Deleted."),

                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                return StatusCode(500, new APIResponse(500, $"Internal Server Error: {ex.Message}"));
            }
        }
        #endregion

    }
}