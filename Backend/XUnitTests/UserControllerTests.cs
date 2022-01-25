using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TrashTalker.Controllers;
using TrashTalker.Database.Repositories;
using TrashTalker.Database.Repositories.UserRepository;
using TrashTalker.Dto.User;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;
using Xunit;

namespace XUnitTests
{
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUserRepository> _funRepoMock = new Mock<IUserRepository>();
        public UserControllerTests()
        {
            _userController = new UserController(_funRepoMock.Object);
        }
        
        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var user = new User()
            {
                id = userId,
                username = "Tiago",
                password = "pass",
                role = Role.EMPLOYEE,
                firstName = "Joao",
                lastName = "Costa",
                email = "sampaio.micael@gmail.com",
                gender = Gender.FEMALE,
                city = "Vizela",
                zipCode = "Joao",
                status = Status.ACTIVE,
                country = "Portugal"
            };

            _funRepoMock.Setup(x => x.getUser(userId))
                    .ReturnsAsync(user);

            //Act
            var userResult = await _userController.getUser(userId);

            //Assert
            Assert.Equal(userId, userResult.Value.id);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserNotExists()
        {
            //Arrange
            _funRepoMock.Setup(x => x.getUser(It.IsAny<Guid>()))
                    .ReturnsAsync(() => null);

            //Act
            ActionResult<UserDTO> userResult = await _userController.getUser(Guid.NewGuid());

            //Assert
            Assert.IsType<NotFoundResult>(userResult.Result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnNotFound_WhenUserNotExists()
        {
            //Arrange
            var userId = Guid.NewGuid();

            var userToBeUpdated = new UpdateUserDTO()
            {
                password = "pass",
                email = "sampaio.micael@gmail.com",
                city = "Vizela",
                zipCode = "Joao",
                country = "Portugal"
            };

            var userUpdated = new User()
            {
                id = userId,
                username = "Tiago",
                password = "pass",
                role = Role.EMPLOYEE,
                firstName = "Pedro",
                lastName = "Costa",
                email = "sampaio.micael@gmail.com",
                gender = Gender.FEMALE,
                city = "Vizela",
                zipCode = "Joao",
                status = Status.ACTIVE,
                country = "Portugal"
            };

            _funRepoMock.Setup(x => x.disableUser(userId))
                    .ReturnsAsync(() => null);

            //Act
            IActionResult userResult = await _userController.updateUser(userToBeUpdated, userId);

            //Assert
            Assert.IsType<NotFoundResult>(userResult);
        }

        [Fact]
        public async Task DisableUser_ShouldReturnNotFound_WhenUserNotExists()
        {
            //Arrange
            _funRepoMock.Setup(x => x.getUser(It.IsAny<Guid>()))
                    .ReturnsAsync(() => null);

            //Act
            IActionResult userResult = await _userController.disabeUser(Guid.NewGuid());

            //Assert
            Assert.IsType<NotFoundResult>(userResult);
        }
    }
}
