﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Services.Common.Messages;
using WebShop.Services.Products.Application.Responses;

namespace WebShop.Services.Products.Application.Messages.Commands
{
    public class UpdateProductCommand : IRequest<UpdateProductResponse> , ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Vendor { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        public UpdateProductCommand(Guid id, string name,
            string description, string vendor,
            decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Vendor = vendor;
            Price = price;
            Quantity = quantity;
        }
    }
}
