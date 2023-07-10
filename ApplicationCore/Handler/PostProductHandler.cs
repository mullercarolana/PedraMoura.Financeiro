using ApplicationCore.Command;
using ApplicationCore.Entities.Products;
using ApplicationCore.Seedwork.Exceptions;
using MediatR;

namespace ApplicationCore.Handler
{
    public sealed class PostProductHandler : IRequestHandler<PostProductCommand, Result<Product>>
    {
        private readonly IProductsRepository _productsRepository;
        public PostProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<Result<Product>> Handle(PostProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (Product.Create(request.Name, request.Price, request.MassMeasure, request.Value) is var product && product.IsError)
                {
                    return product.Error;
                }

                _productsRepository.Add(product.Success);
                await _productsRepository.SaveChangesAsync();
                return product.Success;

            }
            catch (Exception ex)
            {
                return Error.New("ErrorPostProduct", $"Error to post the product. Error: {ex.Message}");
            }
        }
    }
}
