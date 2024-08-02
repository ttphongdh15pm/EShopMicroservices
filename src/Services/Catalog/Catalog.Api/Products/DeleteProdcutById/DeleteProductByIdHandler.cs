﻿namespace Catalog.Api.Products.DeleteProdcutById
{
    public record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByIdResult>;
    public record DeleteProductByIdResult(bool IsSuccess);
    public class DeleteProductByIdCommandValidator : AbstractValidator<DeleteProductByIdCommand>
    {
        public DeleteProductByIdCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
        }
    }

    internal class DeleteProductByIdCommandHandler
      (IDocumentSession session)
      : ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdResult>
    {
        public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductByIdResult(true);
        }
    }
}
