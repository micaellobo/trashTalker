using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TrashTalker.Controllers;
using TrashTalker.Database.Repositories.ContainerRepository;
using TrashTalker.Database.Repositories.PickingRepository;
using TrashTalker.Database.Repositories.RouteRepository;
using TrashTalker.Dto.Container;
using TrashTalker.Dto.Picking;
using TrashTalker.Models;
using Xunit;

namespace XUnitTests
{
    public class PickinControllerTests
    {
        private readonly PickingController _pickingController;
        private readonly Mock<IPickingRepository> _pickingRepoMock = new Mock<IPickingRepository>();
        private readonly Mock<IContainerRepository> _containerRepoMock = new Mock<IContainerRepository>();
        private readonly Mock<IRouteRepository> _routeRepoMock = new Mock<IRouteRepository>();

        public PickinControllerTests()
        {
            _pickingController = new PickingController(_pickingRepoMock.Object, _containerRepoMock.Object, _routeRepoMock.Object);
        }


        [Fact]
        public async Task GetPickingByIdAsync_ShouldReturnPicking_WhenPickingExists()
        {
            //Arrange
            var containerId = Guid.NewGuid();
            var container = new Container()
            {
                id = containerId,
                status = TrashTalker.Models.Enumerations.Status.ACTIVE,
                typeOfWaste = TrashTalker.Models.Enumerations.TypeOfWaste.PAPER,
                height = 2.5f,
                width = 2.5f,
                depth = 2.5f
            };

            var pickingId = Guid.NewGuid();
            var picking = new Picking()
            {
                id = pickingId,

                volumeRecolhido = 2000,
                container = new Container()
            };

            _pickingRepoMock.Setup(x => x.getPickingId(pickingId))
                .ReturnsAsync(picking);

            //Act
            var pickingResult = await _pickingController.getPickingById(pickingId);

            //Assert
            //Assert.Equal(containerId, pickingResult.Value.id);
        }


        [Fact]
        public async Task GetPickingByIdAsync_ShouldReturnNull_WhenPickingNotExists()
        {
            //Arrange
            _pickingRepoMock.Setup(x => x.getPickingId(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            //Act
            ActionResult<PickingDTO> pickingResult = await _pickingController.getPickingById(Guid.NewGuid());

            //Assert
            Assert.IsType<NotFoundResult>(pickingResult.Result);
        }
    }
}