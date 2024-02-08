using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.API.DTOS;
using Stock.API.Error;
using Stock.Core;
using Stock.Core.Entites;
using Stock.Core.Entites.Items;
using Stock.Repository;

namespace Stock.API.Controllers
{
    public class SuppliersController : APIBaseController
    {
        private readonly IMapper mapper;
        private readonly IUnitofwork unitofwork;

        public SuppliersController(IMapper mapper,IUnitofwork unitofwork)
        {
            this.mapper = mapper;
            this.unitofwork = unitofwork;
        }

        #region EndPoint AddSupplier
        [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status400BadRequest)]
        [HttpPost("AddSupplier")]
        public async Task<ActionResult<SupplierDto>> Create(SupplierDto supplierDto)
        {
            var mappedSupplier = mapper.Map<SupplierDto, Supplier>(supplierDto);
            await unitofwork.Repository<Supplier>().AddAsync(mappedSupplier);
            await unitofwork.CompleteAsync();
            var response = new
            {
                response = new APIResponse(200, "Supplier successfully created.")
            };
            return Ok(new {response, mappedSupplier });
        }
        #endregion


        #region EndPoint GetAllSuppliers 
        [ProducesResponseType(typeof(IReadOnlyList<Supplier>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Supplier), StatusCodes.Status404NotFound)]
        [HttpGet("GetAllSuppliers")]
        public async Task<ActionResult<IReadOnlyList<Supplier>>> GetAllSuppliers()
        {
            var Suppliers = await unitofwork.Repository<Supplier>().GatAllAsync();
            if (Suppliers == null || !Suppliers.Any())
            {
                return NotFound();
            }
            return Ok(Suppliers);
        } 
        #endregion
    }
}
