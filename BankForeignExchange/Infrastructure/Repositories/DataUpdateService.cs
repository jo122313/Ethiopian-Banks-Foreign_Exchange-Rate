using BankForeignExchange.Domain.Interfaces;
using BankForeignExchange.Util;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace BankForeignExchange.Infrastructure.Repositories
{
    public class DataUpdateService : BackgroundService
    {
        private readonly IHubContext<MyHub> _hubContext;
        private readonly IServiceProvider _serviceProvider;

        public DataUpdateService(IHubContext<MyHub> hubContext, IServiceProvider serviceProvider)
        {
            _hubContext = hubContext;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dataService = scope.ServiceProvider.GetService<IDataService>();
                    if (dataService == null)
                    {
                        // Log the error or handle it as needed
                        continue; // Skip this iteration if dataService is null
                    }

                    var data = await dataService.GetUpdatedDataAsync();
                    await _hubContext.Clients.All.SendAsync("ReceiveUpdatedData", JsonSerializer.Serialize(data));
                }
            }
        }
    }
}
