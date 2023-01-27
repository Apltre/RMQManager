using Refit;

namespace RMQDel
{
    [Headers("Authorization: Basic")]
    public interface IRMQManagement
    {
        [Get("/api/queues")]
        Task<ApiResponse<IEnumerable<RMQObj>>> GetQueues();

        [Get("/api/exchanges")]
        Task<ApiResponse<IEnumerable<RMQObj>>> GetExchanges();

        [Delete("/api/queues/%2f/{**queueName}")]
        Task DeleteQueue(string queueName);

        [Delete("/api/exchanges/%2f/{**exchangeName}")]
        Task DeleteExchange(string exchangeName);
    }
}
