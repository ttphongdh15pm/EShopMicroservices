using Marten.Schema;

namespace Catalog.Api.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if(await session.Query<Product>().AnyAsync())
            {
                return;
            }

            session.Store(GenerateProducts());
            await session.SaveChangesAsync();
        }

        private Product[] GenerateProducts()
        {
            var random = new Random();

            // Example real-world-like product names and categories
            var productData = new List<(string Name, string[] Categories)>
            {
                // Electronics
                ("Apple iPhone 14", new[] { "Electronics", "Mobile Phones" }),
                ("Samsung Galaxy S22", new[] { "Electronics", "Mobile Phones" }),
                ("Sony WH-1000XM4 Headphones", new[] { "Electronics", "Audio" }),
                ("Dell XPS 13 Laptop", new[] { "Electronics", "Computers" }),
                ("Apple MacBook Pro 16-inch", new[] { "Electronics", "Computers" }),
                ("Google Pixel 7", new[] { "Electronics", "Mobile Phones" }),
                ("Apple Watch Series 8", new[] { "Electronics", "Wearables" }),
                ("Sony PlayStation 5", new[] { "Electronics", "Gaming" }),
                ("Nintendo Switch OLED", new[] { "Electronics", "Gaming" }),

                // Clothing
                ("Nike Air Max 270", new[] { "Clothing", "Shoes" }),
                ("Adidas Ultraboost", new[] { "Clothing", "Shoes" }),
                ("Levi's 501 Original Fit Jeans", new[] { "Clothing", "Pants" }),
                ("North Face ThermoBall Jacket", new[] { "Clothing", "Jackets" }),
                ("Patagonia Better Sweater", new[] { "Clothing", "Jackets" }),
                ("Ray-Ban Wayfarer Sunglasses", new[] { "Clothing", "Accessories" }),
                ("Casio G-Shock Watch", new[] { "Clothing", "Accessories" }),

                // Home & Kitchen
                ("Instant Pot Duo 7-in-1", new[] { "Kitchen", "Appliances" }),
                ("Dyson V11 Vacuum", new[] { "Home", "Appliances" }),
                ("KitchenAid Artisan Stand Mixer", new[] { "Kitchen", "Appliances" }),
                ("Philips Hue Smart Bulbs", new[] { "Home", "Lighting" }),
                ("Roomba i7+ Robot Vacuum", new[] { "Home", "Appliances" }),
                ("Nespresso Vertuo Coffee Maker", new[] { "Kitchen", "Appliances" }),

                // Books
                ("The Catcher in the Rye", new[] { "Books", "Fiction" }),
                ("1984 by George Orwell", new[] { "Books", "Fiction" }),
                ("The Great Gatsby", new[] { "Books", "Fiction" }),
                ("To Kill a Mockingbird", new[] { "Books", "Fiction" }),
                ("Sapiens: A Brief History of Humankind", new[] { "Books", "Non-Fiction" }),
                ("Educated: A Memoir", new[] { "Books", "Non-Fiction" }),
                ("Becoming by Michelle Obama", new[] { "Books", "Biography" }),

                // Toys
                ("LEGO Star Wars Millennium Falcon", new[] { "Toys", "Building Sets" }),
                ("Barbie Dreamhouse", new[] { "Toys", "Dolls" }),
                ("Hot Wheels Monster Trucks", new[] { "Toys", "Vehicles" }),
                ("NERF Elite 2.0 Shockwave", new[] { "Toys", "Outdoor Play" }),
                ("Fisher-Price Laugh & Learn", new[] { "Toys", "Baby & Toddler" }),

                // Sports & Outdoors
                ("Fitbit Charge 5", new[] { "Electronics", "Wearables" }),
                ("GoPro HERO11 Black", new[] { "Electronics", "Cameras" }),
                ("Columbia Hiking Boots", new[] { "Clothing", "Shoes" }),
                ("Yeti Tundra 45 Cooler", new[] { "Sports", "Outdoors" }),
                ("Wilson Evolution Basketball", new[] { "Sports", "Equipment" }),
                ("Osprey Farpoint 40 Travel Backpack", new[] { "Sports", "Outdoors" }),
            };

            int numberOfProducts = 100;
            var products = new Product[numberOfProducts];
            for (int i = 0; i < numberOfProducts; i++)
            {
                var data = productData[random.Next(productData.Count)];
                products[i] = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = data.Name,
                    Categories = data.Categories.ToList(),
                    Description = $"This is a description for {data.Name}.",
                    ImageFile = $"{data.Name.Replace(" ", "_").ToLower()}.png",
                    Price = Math.Round((decimal)(random.NextDouble() * 100 + 10), 2)
                };
            }

            return products;
        }
    }
}
