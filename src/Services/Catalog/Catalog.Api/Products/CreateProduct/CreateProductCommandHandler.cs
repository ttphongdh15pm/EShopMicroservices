using FluentValidation;

namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Categories is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }


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
