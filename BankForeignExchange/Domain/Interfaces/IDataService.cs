using BankForeignExchange.Domain.Entities;

namespace BankForeignExchange.Domain.Interfaces
{
    public interface IDataService
    {
        Task<List<BanksExchangeRatesModel>> GetUpdatedDataAsync();
    }
}
