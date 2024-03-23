using JobManagement.UIApp.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace JobManagement.UIApp.Data
{
	public class JobOrderService
	{
        private readonly HttpClient _httpClient;
        private string jsonFilePath = "Data/joborders.json";
        public JobOrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<JobOrder>> GetJobOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<JobOrder>>("joborders.json");
        }

        public async Task AddJobOrderAsync(JobOrder jobOrder)
        {
            var jobOrders = await GetJobOrdersAsync();
            jobOrder.Id = Guid.NewGuid(); // Ensure the job order has a unique ID
            jobOrders.Add(jobOrder);
            await WriteJobOrdersAsync(jobOrders);
        }

        public async Task EditJobOrderAsync(JobOrder jobOrder)
        {
            var jobOrders = await GetJobOrdersAsync();
            var jobOrderIndex = jobOrders.FindIndex(jo => jo.Id == jobOrder.Id);
            if (jobOrderIndex != -1)
            {
                jobOrders[jobOrderIndex] = jobOrder;
                await WriteJobOrdersAsync(jobOrders);
            }
        }

        public async Task DeleteJobOrderAsync(Guid jobId)
        {
            var jobOrders = await GetJobOrdersAsync();
            var jobOrder = jobOrders.FirstOrDefault(jo => jo.Id == jobId);
            if (jobOrder != null)
            {
                jobOrders.Remove(jobOrder);
                await WriteJobOrdersAsync(jobOrders);
            }
        }

        public async Task UpdateJobStatusAsync(Guid jobId, string newStatus)
        {
            var jobOrders = await GetJobOrdersAsync();
            var jobOrder = jobOrders.FirstOrDefault(jo => jo.Id == jobId);
            if (jobOrder != null)
            {
                jobOrder.Status = newStatus;
                if (newStatus == "Done")
                {
                    jobOrder.FinishedDate = DateTime.Now;
                }
                await WriteJobOrdersAsync(jobOrders);
            }
        }

        private async Task WriteJobOrdersAsync(List<JobOrder> jobOrders)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            using (var outputStream = File.Create(jsonFilePath))
            {
                await JsonSerializer.SerializeAsync(outputStream, jobOrders, options);
                await outputStream.FlushAsync();
            }
        }
    }

}
