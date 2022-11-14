namespace ApplicationCore.Interfaces
{
    public interface IBasketQueryService
    {
        Task<int> CountTotalBasketItems(Guid userId);
    }
}
