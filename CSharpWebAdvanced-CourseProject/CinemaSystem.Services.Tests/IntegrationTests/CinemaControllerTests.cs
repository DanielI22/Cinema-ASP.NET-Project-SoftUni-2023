namespace CinemaSystem.Services.Tests.IntegrationTests
{
    using Microsoft.AspNetCore.Mvc.Testing;

    [TestFixture]
    public class CinemaControllerTests
    {
        private WebApplicationFactory<Program> factory;
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            factory = new WebApplicationFactory<Program>();

            client = factory.CreateClient();
        }

        [TearDown]
        public void Teardown()
        {
            client.Dispose();
            factory.Dispose();
        }

        [Test]
        public async Task All_ReturnsViewWithCinemas()
        {
            // Arrange

            // Act
            var response = await client.GetAsync("/Cinema/All");

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Contains("Cinemania Varna"), Is.True);
        }
    }
}
