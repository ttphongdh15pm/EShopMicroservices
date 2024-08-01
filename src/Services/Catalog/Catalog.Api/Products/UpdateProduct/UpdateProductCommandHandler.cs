namespace Catalog.Api.Products.UpdateProduct
{

    public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductCommandHandler(IDocumentSession document) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await document.LoadAsync<Product>(request.Id, cancellationToken);

            if(product is null)
            {
                return new UpdateProductResult(false);
            }

            product.Name = request.Name;
            product.Categories = request.Categories;
            product.Description = request.Description;
            product.ImageFile = request.ImageFile;
            product.Price = request.Price;

            document.Update(product);
            await document.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
