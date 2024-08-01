namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession document) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Categories = request.Categories,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
            };


            document.Store(product);
            await document.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
