using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Services.Common.Authentication;
using WebShop.Services.Common.ErrorMiddleware;
using WebShop.Services.Products.Application.Messages.Commands;
using WebShop.Services.Products.Application.Queries;

namespace WebShop.Services.Products.Api.Controllers
{
    [AdminAuth]
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
        public async Task<IActionResult> Get([FromQuery] BrowseProductsQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(GetProductQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Accepted();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProductCommand command)
        {
            command.Bind(c => c.Id, id);
            var result = await _mediator.Send(command);
            return Accepted();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(DeleteProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Accepted();
        }
    }
}
