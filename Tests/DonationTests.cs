using NUnit.Framework;\nusing System;\nusing System.Net;\nusing System.Threading.Tasks;\n\n[TestFixture] \npublic class DonationTests \n{ \n    private DonationApiClient _client; \n\n    [SetUp] \n    public void Setup() \n    { \n        _client = new DonationApiClient(\"https://api.example.com\"); \n    } \n\n    [Test] \n    public async Task ConfirmDonation_SuccessfulRequest_ReturnsOk() \n    { \n        // Arrange \n        var donation = new DonationDto \n        { \n            Id = Guid.NewGuid(), \n            UserId = \"user123\", \n            FirstName = \"John\", \n            LastName = \"Doe\", \n            Date = DateTime.UtcNow, \n            BloodType = \"A+\", \n            DonationTypeId = 1, \n            CityId = 1, \n            DonationCenterId = 1, \n            DonationStatus = DonationStatuses.Status1, \n            BloodVolume = 450, \n            HealthFeeling = HealthFeeling.Feeling5, \n            ExperienceRate = 5 \n        }; \n\n        // Act \n        var response = await _client.ConfirmDonationAsync(donation); \n\n        // Assert \n        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode); \n        Assert.IsNotNull(response.Content); \n        Assert.AreEqual(\"Donation successfully confirmed.\", response.Content.Content); \n    } \n\n    [Test] \n    public async Task ConfirmDonation_BadRequest_ReturnsBadRequest() \n    { \n        // Arrange \n        _client.SimulateError(true); \n        var donation = new DonationDto(); // Invalid donation object \n\n        // Act \n        var response = await _client.ConfirmDonationAsync(donation); \n\n        // Assert \n        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode); \n        Assert.IsNotNull(response.Content); \n        Assert.AreEqual(\"Bad request! See json response for details.\", response.Content.Content); \n    } \n\n    [Test] \n    public async Task ConfirmDonation_ServerError_ReturnsInternalServerError() \n    { \n        // Arrange \n        _client.SimulateError(true); \n        var donation = new DonationDto \n        { \n            Id = Guid.NewGuid(), \n            UserId = \"user123\", \n            FirstName = \"John\", \n            LastName = \"Doe\", \n            Date = DateTime.UtcNow, \n            BloodType = \"A+\", \n            DonationTypeId = 1, \n            CityId = 1, \n            DonationCenterId = 1, \n            DonationStatus = DonationStatuses.Status1, \n            BloodVolume = 450, \n            HealthFeeling = HealthFeeling.Feeling5, \n            ExperienceRate = 5 \n        }; \n\n        // Act \n        var response = await _client.ConfirmDonationAsync(donation); \n\n        // Assert \n        Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); \n        Assert.IsNotNull(response.Content); \n        Assert.AreEqual(\"Simulated server error\", response.Content.Content); \n    } \n}