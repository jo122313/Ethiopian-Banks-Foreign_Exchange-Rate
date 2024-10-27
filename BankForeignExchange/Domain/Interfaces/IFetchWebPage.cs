namespace BankForeignExchange.Domain.Interfaces
{
    public interface IFetchWebPage
    {
        public Task<string> FecthWebPage(string url);
    }
}
