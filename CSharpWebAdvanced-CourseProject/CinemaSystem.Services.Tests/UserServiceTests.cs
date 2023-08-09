namespace CinemaSystem.Services.Tests
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data;
    using CinemaSystem.Web.ViewModels.User;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Moq;

    [TestFixture]
    public class UserServiceTests
    {
        private UserService userService;
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private Mock<ILogger<UserService>> loggerMock;

        [SetUp]
        public void Setup()
        {
            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            loggerMock = new Mock<ILogger<UserService>>();

            userService = new UserService(userManagerMock.Object, loggerMock.Object);
        }

        [Test]
        public async Task AddUserAsync_ValidModel_ReturnsSuccessResult()
        {
            // Arrange
            var model = new UserAddViewModel
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "P@ssw0rd",
                IsAdmin = true
            };

            userManagerMock.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            userManagerMock.Setup(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await userService.AddUserAsync(model);

            // Assert
            Assert.IsTrue(result.Succeeded);
            userManagerMock.Verify(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task DeleteUserAsync_ValidId_DeletesUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            userManagerMock.Setup(u => u.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            // Act
            await userService.DeleteUserAsync(userId.ToString());

            // Assert
            userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(u => u.DeleteAsync(It.IsAny<ApplicationUser>()), Times.Once);
        }

        [Test]
        public async Task EditUserAsync_ValidIdAndModel_ReturnsSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = new UserEditViewModel
            {
                Username = "newusername",
                Email = "newemail@example.com",
                IsAdmin = true,
                Password = "NewP@ssw0rd"
            };

            var user = new ApplicationUser { Id = userId };
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            userManagerMock.Setup(u => u.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(u => u.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            userManagerMock.Setup(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(u => u.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            userManagerMock.Setup(u => u.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("resetToken");
            userManagerMock.Setup(u => u.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await userService.EditUserAsync(userId.ToString(), model);

            // Assert
            Assert.IsTrue(result.Succeeded);
            userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(u => u.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Once);
            userManagerMock.Verify(u => u.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public async Task GetEditUserModelAsync_ValidId_ReturnsUserEditViewModel()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new ApplicationUser
            {
                Id = userId,
                UserName = "testuser",
                Email = "test@example.com"
            };
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            userManagerMock.Setup(u => u.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(false); // Simulate role checking

            // Act
            var result = await userService.GetEditUserModelAsync(userId.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Username, Is.EqualTo(user.UserName));
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.IsFalse(result.IsAdmin);
            userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(u => u.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
        }
    }
}
