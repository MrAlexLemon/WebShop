using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Services.Products.Application.Messages.Commands;
using WebShop.Services.Products.Application.Queries;
using WebShop.Services.Products.Application.Responses;
using WebShop.Services.Products.Core.Repositories;
using WebShop.Services.Products.Core.Utils;
using WebShop.Services.Products.Infrastructure.ErrorMiddleware;

namespace WebShop.Services.Products.Api.Controllers
{
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("search")]
        public async Task<IActionResult> Get([FromQuery] BrowseProductsQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(GetProductQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Accepted();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateProductCommand command)
        {
            command.Bind(c => c.Id, id);
            var result = await _mediator.Send(command);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Accepted();
        }
    }
}
