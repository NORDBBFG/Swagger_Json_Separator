using NUnit.Framework;\nusing System;\nusing System.Net;\nusing System.Net.Http;\nusing System.Threading.Tasks;\n\n[TestFixture]\npublic class DonationApiClientTests\n{\n    private DonationApiClient _client;\n    private const string BaseUrl = \"http://example.com\";\n\n    [SetUp]\n    public void Setup()\n    {\n        var httpClient = new HttpClient();\n        _client = new DonationApiClient(httpClient, BaseUrl);\n    }\n\n    [Test]\n    public async Task ConfirmDonation_SuccessfulRequest_ReturnsOkResponse()\n    {\n        // Arrange\n        var donation = new DonationDto\n        {\n            Id = Guid.NewGuid(),\n            UserId = \"user123\",\n            FirstName = \"John\",\n            LastName = \"Doe\",\n            Date = DateTime.Now,\n