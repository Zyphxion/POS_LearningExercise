using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PointOfSaleSystem.Models
{
    public class InventoryInitializer : DropCreateDatabaseIfModelChanges<InventoryContext>
    {
        protected override void Seed(InventoryContext context)
        {

            var categories = new List<Category>
            {
                new Category{Name = "Wii U"},
                new Category{Name = "PlayStation Vita"},
                new Category{Name = "Nintendo 3DS"},
                new Category{Name = "Xbox One"},
            };

            foreach (var temp in categories)
            {
                context.Categories.Add(temp);
            }
            context.SaveChanges();

            var items = new List<Item>
            {
                new Item{CategoryId = 1,
                    Description = "Mario Kart 8",
                    Price = 59.99m,
                    Cost = 30.00m,
                    UPC = "045496903667",
                    Quantity = 9,
                    Taxable = true
                },
                new Item{CategoryId = 1,
                    Description = "Monster Hunter 3 Ultimate",
                    Price = 39.99m,
                    Cost = 20.00m,
                    UPC = "013388390014",
                    Quantity = 14,
                    Taxable = true
                },
                new Item{CategoryId = 2,
                    Description = "Persona 4 Golden",
                    Price = 19.99m,
                    Cost = 10.00m,
                    UPC = "730865200009",
                    Quantity = 32,
                    Taxable = true
                },
                new Item{CategoryId = 3,
                    Description = "Pokemon X",
                    Price = 39.99m,
                    Cost = 20.00m,
                    UPC = "045496742485",
                    Quantity = 48,
                    Taxable = true
                },
                new Item{CategoryId = 4,
                    Description = "Titanfall",
                    Price = 59.99m,
                    Cost = 30.00m,
                    UPC = "885370771275",
                    Quantity = 6,
                    Taxable = true
                },
            };

            foreach (var temp in items)
            {
                context.Items.Add(temp);
            }
            context.SaveChanges();
        }
    }
}