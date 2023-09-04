using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJitu_ProductsService.Models;
using TheJitu_ProductsService.Models.Dtos;
using TheJitu_ProductsService.Services;

namespace TheJitu_ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductInterface _productInterface;
        private readonly ResponseDto _responseDto;
        public ProductController(IMapper mapper, IProductInterface productInterface)
        {
            _mapper = mapper;
            _productInterface = productInterface;
            _responseDto = new ResponseDto();
        }
        //add product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> AddProducts(ProductRequestDto newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);
            var res = await _productInterface.AddProductAsync(product);
            if (string.IsNullOrWhiteSpace(res))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "something went wrong";

                return BadRequest(_responseDto);
            }

            return CreatedAtAction(nameof(AddProducts),_responseDto);
        }
        [HttpGet]
        //get products
        public async Task<ActionResult<ResponseDto>> getAllProducts()
        {
            var products = await _productInterface.GetProductsAsync();
            if(products == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Could not fetch products";

                return NotFound(_responseDto);
            }
            _responseDto.Result = products;
            return Ok(products);
        }
        //get product by id
        [HttpGet("GetById{id}")]
        public async Task<ActionResult<ResponseDto>> GetProductById(Guid id)
        {
            var product = await _productInterface.GetProductByIdAsync(id);
            if(product == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Could not fetch product";

                return NotFound(_responseDto);
            }
            _responseDto.Result = product;
            return Ok(product);
        }
        //update product
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(Guid id, ProductRequestDto productRequestDto)
        {
            var productToUpdate = await _productInterface.GetProductByIdAsync(id);
            if (productToUpdate == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Could not Update";
                return BadRequest(_responseDto);
            }
            var updatedProduct = _mapper.Map(productRequestDto, productToUpdate);
            var res = await _productInterface.UpdateProductAsync(updatedProduct);
            _responseDto.Result = res;
            return Ok(_responseDto);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(Guid id)
        {
            var productToDelete = await _productInterface.GetProductByIdAsync(id);
            if(productToDelete == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Occured";
                return BadRequest(_responseDto);
            }
            var res = await _productInterface.DeleteProductAsync(productToDelete);
            _responseDto.Result = res;
            return Ok(_responseDto);
        }
    }
  
}
