namespace MVCOrderSystem.Services
{
    public interface IQueueSender
    {
        //Abstraction for pushing JSON to a queue
        Task SendAsync(string queueName, string json);
    }
}
