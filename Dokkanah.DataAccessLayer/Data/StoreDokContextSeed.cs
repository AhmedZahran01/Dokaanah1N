using Dokaanah.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dokaanah.DataAccessLayer.Data
{
    public static class StoreDokContextSeed
    {
        #region MyRegion

        public async static Task SeedAsync(DokkanahDBContex dbcontext)
        {
           
            if (!dbcontext.Categories.Any()) // Check if the database is empty
            {
                var categoryData = File.ReadAllText("../Dokkanah.DataAccessLayer/Data/DataSeeding/Categories.json");

                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);
                 
                if (categories?.Count() > 0)
                {
                    foreach (var caategory in categories)
                    {
                        dbcontext.Set<Category>().Add(caategory);
                    }
                    await dbcontext.SaveChangesAsync();
                    
                    //if (categories != null && categories.Any())
                    //{
                    //    dbcontext.Categories.AddRange(categories);
                    //    await dbcontext.SaveChangesAsync();
                    //}
                }
            } 
            
            if (!dbcontext.Products.Any()) // Check if the database is empty
            {
                var productData = File.ReadAllText("../Dokkanah.DataAccessLayer/Data/DataSeeding/Products.json");

                var Products = JsonSerializer.Deserialize<List<Product>>(productData);
 
                if (Products?.Count() > 0)
                {
                    foreach (var Product in Products)
                    {
                        dbcontext.Set<Product>().Add(Product);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
          
            if (!dbcontext.Roles.Any()) // Check if the database is empty
            {
                var productData = File.ReadAllText("../Dokkanah.DataAccessLayer/Data/DataSeeding/Roleseed.json");

                var Products = JsonSerializer.Deserialize<List<IdentityRole>>(productData);
 
                if (Products?.Count() > 0)
                {
                    foreach (var Product in Products)
                    {
                        dbcontext.Set<IdentityRole>().Add(Product);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
             
        }
        #endregion

    }
}
