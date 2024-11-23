using AutoMapper;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using System.Linq.Expressions;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork<Products> unitOfWork;
        private readonly IMapper mapper;
        public ApiResponse response;

        public ProductController(IUnitOfWork<Products> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            response = new ApiResponse();
        }



        [HttpGet]  // Get https://localhost:7045/api/Product
        //[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        [ResponseCache(CacheProfileName = ("defaultCash"))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]
        public async Task<ActionResult<ApiResponse>> GetAllProducts([FromQuery] decimal? price = null, [FromQuery] string? categoryName = null, int PageSize = 2, int PageNumber = 1)
        {
            Expression<Func<Products, bool>> filter = null;
            if (!string.IsNullOrEmpty(categoryName))
            {
                filter = x => x.Category.Name.Contains(categoryName);
            }

            var model = await unitOfWork.productRepository.GetAll(includeProperty : "Category" , page_size : PageSize , page_number : PageNumber , filter : filter); 
            var check = model.Any();
            if (check)
            {
                response.StatusCode = 200;
                response.IsSuccess = check;
                var mappedProducts = mapper.Map<IEnumerable<Products>, IEnumerable<ProductDTO>>(model);
                response.Result = mappedProducts;
                return response;
            }
            else
            {
                response.Message = "no products found";
                response.StatusCode = 200;
                response.IsSuccess = false;
                return response;
            }
        }


        [HttpGet("getById")]   // Get https://localhost:7045/api/Product/getById
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> GetById([FromQuery]int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiValidationResponse(new List<string> { "Invalid ID", "Try Positive Integer" }, 400));
                }
                var model = await unitOfWork.productRepository.GetById(id);

                if (model == null)
                {
                    var x = model.ToString();
                    return NotFound(new ApiResponse(404, "Product Not Found"));
                }
                return Ok(new ApiResponse(200, result: model));
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new ApiValidationResponse(new List<string> { "internal server error", ex.Message } , 
                    StatusCodes.Status500InternalServerError));
            }
        }


        [HttpPost]  
        public async Task<ActionResult> CreateProduct(Products model)
        {
            await unitOfWork.productRepository.Create(model);
            await unitOfWork.save();
            return Ok(model);
        }
          

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(Products model)
        {
            unitOfWork.productRepository.Update(model);
            await unitOfWork.save();
            return Ok(model);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            unitOfWork.productRepository.Delete(id);
            await unitOfWork.save();
            return Ok();
        }


        [HttpGet("{Cat_Id}")]
        public async Task<ActionResult<Products>> GetAllProductByCatId(int Cat_Id)
        {
            var products = await unitOfWork.productRepository.GetAllProductByCategoryId(Cat_Id);
            var mappedProducts = mapper.Map<IEnumerable<Products>, IEnumerable<ProductDTO>>(products);
            return Ok(mappedProducts);
        }

    }
}
