using System;\nusing System.Net.Http;\nusing System.Text;\nusing System.Threading.Tasks;\nusing Newtonsoft.Json;\nusing DonorApp.Services.DTO;\n\nnamespace DonorApp.Services.ApiClient\n{\n    public class DonationApiClient\n    {\n        private readonly HttpClient _httpClient;\n        private readonly string _baseUrl;\n        private bool _simulateError;\n\n        public DonationApiClient(string baseUrl)\n        {\n            _baseUrl = baseUrl;\n            _httpClient = new HttpClient();\n        }\n\n        public void SimulateError(bool simulate)\n        {\n            _simulateError = simulate;\n        }\n\n        public async Task<ApiResponse<ContentResult>> ConfirmDonationAsync(DonationDto donationDto)\n        {\n            if (_simulateError)\n            {\n                return new ApiResponse<ContentResult>\n                {\n                    StatusCode = System.Net.HttpStatusCode.InternalServerError,\n                    Content = new ContentResult\n                    {\n                        Content = \"Simulated server error\",\n                        ContentType = \"application/json\",\n                        StatusCode = 500\n                    }\n                };\n            }\n\n            var json = JsonConvert.SerializeObject(donationDto);\n            var content = new StringContent(json, Encoding.UTF8, \"application/json\");\n\n            var response = await _httpClient.PutAsync($\"{_baseUrl}/api/v1/donation/confirm\", content);\n\n            var responseContent = await response.Content.ReadAsStringAsync();\n