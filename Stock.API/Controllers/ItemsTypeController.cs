using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.API.DTOS;
using Stock.API.Error;
using Stock.Core;
using Stock.Core.Entites;
using Stock.Core.Entites.Items;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsTypeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitofwork unitofwork;

        public ItemsTypeController(IMapper mapper,IUnitofwork unitofwork)
        {
            this.mapper = mapper;
            this.unitofwork = unitofwork;
        }

        #region EndPoint AddItemsType
        [ProducesResponseType(typeof(ItemTypeDTo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ItemTypeDTo), StatusCodes.Status400BadRequest)]
        [HttpPost("AddItemType")]
        public async Task<ActionResult<ItemTypeDTo>> Create(ItemTypeDTo itemsTypeDTo)
        {
            var mappedItemType = mapper.Map<ItemTypeDTo, ItemType>(itemsTypeDTo);
            await unitofwork.Repository<ItemType>().AddAsync(mappedItemType);
            await unitofwork.CompleteAsync();
            
            var response = new
            {
                response = new APIResponse(200, "Supplier successfully created.")
            };
            return Ok(new { response, mappedItemType });
         
        }
        #endregion


        #region EndPoint GetAllItemsType
        [ProducesResponseType(typeof(IReadOnlyList<ItemTypeDTo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ItemTypeDTo), StatusCodes.Status404NotFound)]
        [HttpGet("GetAllItemsType")]
        public async Task<ActionResult<IReadOnlyList<ItemTypeDTo>>> GetAllSuppliers()
        {
            var ItemsType = await unitofwork.Repository<ItemType>().GatAllAsync();
            if (ItemsType == null || !ItemsType.Any())
            {
                return NotFound();
            }
            return Ok(ItemsType);
        }
        #endregion
    }
}
