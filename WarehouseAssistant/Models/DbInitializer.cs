using System;

namespace WarehouseAssistant.Models
{
    public static class DbInitializer
    {
        public static void Initialize(AppContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            #region Product
            var products = new Product[]
                {
                    new Product{Name="Product 1"},
                    new Product{Name="Product 2"},
                    new Product{Name="Product 3"},
                    new Product{Name="Product 4"},
                    new Product{Name="Product 5"},
                    new Product{Name="Product 6"},
                    new Product{Name="Product 7"},
                    new Product{Name="Product 8"},
                    new Product{Name="Product 9"},
                    new Product{Name="Product 10"}
                };

            foreach (Product product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();
            #endregion

            #region Warehouse
            var warehouses = new Warehouse[]
                {
                    new Warehouse{Name="From outside"},
                    new Warehouse{Name="To outside"},
                    new Warehouse{Name="Warehouse 1"},
                    new Warehouse{Name="Warehouse 2"},
                    new Warehouse{Name="Warehouse 3"}
                };

            foreach (Warehouse warehouse in warehouses)
            {
                context.Warehouses.Add(warehouse);
            }
            context.SaveChanges();
            #endregion

            #region Transaction
            var transactions = new Transaction[]
                {
                    new Transaction{WarehouseInId=3, WarehouseFromId=1, ProductId=1, Count=100, DateTime=DateTime.Parse("2022-04-10")},
                    new Transaction{WarehouseInId=3, WarehouseFromId=1, ProductId=2, Count=200, DateTime=DateTime.Parse("2022-04-10")},
                    new Transaction{WarehouseInId=3, WarehouseFromId=1, ProductId=3, Count=300, DateTime=DateTime.Parse("2022-04-10")},
                    new Transaction{WarehouseInId=3, WarehouseFromId=1, ProductId=4, Count=400, DateTime=DateTime.Parse("2022-04-10")},
                    new Transaction{WarehouseInId=3, WarehouseFromId=1, ProductId=1, Count=10, DateTime=DateTime.Parse("2022-04-10")},
                    new Transaction{WarehouseInId=4, WarehouseFromId=3, ProductId=2, Count=20, DateTime=DateTime.Parse("2022-04-11")},
                    new Transaction{WarehouseInId=3, WarehouseFromId=4, ProductId=2, Count=-20, DateTime=DateTime.Parse("2022-04-11")}
                };

            foreach (Transaction transaction in transactions)
            {
                context.Transactions.Add(transaction);
            }
            context.SaveChanges(); 
            #endregion
        }
    }
}
