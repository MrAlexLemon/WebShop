﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Products.Application.Responses
{
    public class CreateProductResponse
    {
		public Guid Id { get; }
		public string Name { get; }
		public string Description { get; }
		public string Vendor { get; }
		public decimal Price { get; }
		public int Quantity { get; }

		public CreateProductResponse(Guid id, string name, string description, string vendor, decimal price, int quantity)
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