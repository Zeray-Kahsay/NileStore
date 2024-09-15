using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
  public static async Task SeedDataAsync(StoreContext context)
  {
    if (!context.Products.Any())
    {
      var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

      // deserialize the json into products 
      var products = JsonSerializer.Deserialize<List<Product>>(productsData);

      // check if the conversion was successfull and there is a product
      if (products is null) return;

      //add the products into the products table in the database 
      context.Products.AddRange(products);

      // save the changes 
      await context.SaveChangesAsync();
    }
  }

}
