using Catalog.Api.Exceptions;
using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.UpdateProduct
{

    public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Categories is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal sealed class UpdateProductCommandHandler(IDocumentSession document) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await document.LoadAsync<Product>(request.Id, cancellationToken);

            if(product is null)
            {
                throw new ProductNotFoundException(request.Id);
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
