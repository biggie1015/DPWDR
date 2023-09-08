using AutoMapper;
using DPWDR.Technical.Interview.Data.Entities;
using DPWDR.Technical.Interview.Data.Repositories;
using DPWDR.Technical.Interview.Services.DTOS;
using System.Text.Json;


namespace DPWDR.Technical.Interview.Services.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ProductService(ProductRepository productRepository, HttpClient httpClient, IMapper mapper)
        {
            _productRepository = productRepository;
            _httpClient = httpClient;
            _mapper = mapper;
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public async Task<IEnumerable<Product>> GetProductsInStockAsync(DateTime? startDate, int? productId)
        {
            return await _productRepository.GetProductsInStockAsync(startDate, productId);
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(DateTime? filterDate, int productId)
        {
            return await _productRepository.GetFilteredProductsAsync(filterDate, productId);
        }

        public async Task<bool> ProductExists(int productId)
        {

            return await _productRepository.ProductExistsAsync(productId);
        }

        public async Task<bool> UpdateStockIfZero(int productId, int newStock)
        {
            return await _productRepository.UpdateStockIfZeroAsync(productId, newStock);
        }


        public async Task<bool> AddProduct(Product product)
        {

            bool productExists = await ProductExists(product.Id);

            if (!productExists)
            {

                await _productRepository.AddNewProductWithZeroStockAsync(product);
                return true;
            }
            else
            {

                return false;
            }
        }

        public async Task<List<Product>> FetchExternalApiDataAsync()
        {
            try
            {
                string url = "https://fakestoreapi.com/products";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var products = JsonSerializer.Deserialize<List<Product>>(json);
                    return products;
                }
                else
                {
                    var errorMessage = $"La solicitud HTTP no fue exitosa. Código de estado: {response.StatusCode}";
                    throw new ApplicationException(errorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                var errorMessage = $"Error en la solicitud HTTP: {ex.Message}";
                throw new ApplicationException(errorMessage, ex);
            }
            catch (JsonException ex)
            {
                var errorMessage = $"Error al deserializar el JSON: {ex.Message}";
                throw new ApplicationException(errorMessage, ex);
            }
            catch (AutoMapperMappingException ex )
            {
                var errorMessage = $"Error inesperado: {ex.Message}";
                throw new ApplicationException(errorMessage, ex);
            }
        }
    }
}
