using Basket.Api.Data;

namespace Basket.Api.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteBasketAsync(request.Username);
            return new DeleteBasketResult(result);
        }
    }
}
