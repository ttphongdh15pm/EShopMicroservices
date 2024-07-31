using BuildingBlocks.CQRS;
using Catalog.Api.Models;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(int Total, List<Product> Products);

    public class GetProductQueryHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetProductsResult(5, new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product A",
                    Category = new List<string> { "Electronics", "Gadgets" },
                    Description = "High quality electronic gadget.",
                    ImageFile = "productA.jpg",
                    Price = 199.99m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product B",
                    Category = new List<string> { "Home Appliances" },
                    Description = "Durable home appliance.",
                    ImageFile = "productB.jpg",
                    Price = 89.99m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product C",
                    Category = new List<string> { "Books" },
                    Description = "Informative book on technology.",
                    ImageFile = "productC.jpg",
                    Price = 29.99m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product D",
                    Category = new List<string> { "Clothing", "Fashion" },
                    Description = "Stylish and comfortable clothing.",
                    ImageFile = "productD.jpg",
                    Price = 49.99m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product E",
                    Category = new List<string> { "Sports", "Equipment" },
                    Description = "High performance sports equipment.",
                    ImageFile = "productE.jpg",
                    Price = 149.99m
                }
            }));
        }
    }
}
