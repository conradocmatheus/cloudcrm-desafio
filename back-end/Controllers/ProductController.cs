﻿using back_end.DTOs;
using back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // POST Product
    // POST: /api/product/post
    [HttpPost]
    [Route("/post")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var productDto = await _productService.CreateProductAsync(createProductDto);
            return Ok(productDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // Update Product by id
    // Update: /api/product/update/by-id/{id}
    [HttpPut]
    [Route("/update/by-id")]
    public async Task<IActionResult> UpdateProductById([FromBody] CreateProductDto createProductDto,
        [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var productDto = await _productService.UpdateProductAsync(createProductDto, id);
            return Ok(productDto);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }

    // GET Products
    // GET: /api/product/get-all
    [HttpGet]
    [Route("/get-all/")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var productDto = await _productService.GetAllProducts();
            return Ok(productDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}