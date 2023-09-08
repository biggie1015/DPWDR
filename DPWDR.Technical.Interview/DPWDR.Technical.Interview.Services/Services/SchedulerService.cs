using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPWDR.Technical.Interview.Services.Services
{
    public class SchedulerService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly ProductService _productService;
        private readonly double _intervalInMinutes = 1;

        public SchedulerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async state => await ExecuteScheduledTaskAsync(state), null, TimeSpan.Zero, TimeSpan.FromMinutes(_intervalInMinutes));
            return Task.CompletedTask;
        }

        private async Task ExecuteScheduledTaskAsync(object state)
        {
            try
            {
                
                using (var scope = _serviceProvider.CreateScope())
                {
                    var productService = scope.ServiceProvider.GetRequiredService<ProductService>();
                    var productsFromApi = await productService.FetchExternalApiDataAsync();

                    foreach (var product in productsFromApi)
                    {
                        bool productExists = await productService.ProductExists(product.Id);

                        if (!productExists)
                        {
                            product.Stock = 0;
                            await productService.AddProduct(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja las excepciones aquí
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
