using InventoryApi.Repositories;
using MediatR;

namespace InventoryApi.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository repository)
    : IRequestHandler<DeleteProductCommand, bool>
{
    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken) =>
        repository.DeleteAsync(request.Id);
}
