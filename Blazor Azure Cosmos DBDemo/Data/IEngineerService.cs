namespace Blazor_Azure_Cosmos_DBDemo.Data
{
    public interface IEngineerService
    {
       // Task AddEngineer(Engineer engineer);
        Task DeleteEngineer(string? id, string? PartitionKey);
        Task<List<Engineer>> GetEngineerDetails();
        Task<Engineer> GetEngineerDetailsbyid(string? id, string? PartitionKey);
        Task UpsertEngineer(Engineer engineer);
    }
}