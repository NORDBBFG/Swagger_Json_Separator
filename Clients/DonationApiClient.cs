using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class DonationApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private bool _simulateError;

    public DonationApiClient(string baseUrl)
    {
        _httpClient = new HttpClient();
        _baseUrl = baseUrl;
    }

    public void SimulateError(bool simulate)
    {
        _simulateError = simulate;
    }

    public async Task<ApiResponse<ContentResult>> ConfirmDonationAsync(DonationDto donation)
    {
        if (_simulateError)
        {
            return new ApiResponse<ContentResult>
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Data = new ContentResult
                {
                    Content = "Simulated server error",
                    ContentType = "application/json",
                    StatusCode = 500
                }
            };
        }

        var json = JsonSerializer.Serialize(donation);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(`${_baseUrl}/api/v1/donation/confirm`, content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var contentResult = JsonSerializer.Deserialize<ContentResult>(responseContent);

        return new ApiResponse<ContentResult>
        {
            StatusCode = response.StatusCode,
            Data = contentResult
        };
    }
}

public class ApiResponse<T>
{
    public System.Net.HttpStatusCode StatusCode { get; set; }
    public T Data { get; set; }
}