using BankForeignExchange.Domain.Entities;
using BankForeignExchange.Domain.Interfaces;
using BankForeignExchange.Infrastructure.Repositories;
using BankForeignExchange.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BanksExchangeRates.Controllers
{
    public class ExchangeRateController : Controller
    {
        private readonly IOptions<List<XPathModel>> _options;
        private readonly IHubContext<MyHub> _hubContext;
        private readonly IDataService _dataService;

        public ExchangeRateController(IOptions<List<XPathModel>> options, IHubContext<MyHub> hubContext, IDataService dataService)
        {
            _options = options;
            _hubContext = hubContext;
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public async Task<string> FetchInitialData()
        {
            var data = await _dataService.GetUpdatedDataAsync();
            return JsonSerializer.Serialize(data);
        }
    }
}
