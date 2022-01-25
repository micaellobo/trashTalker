using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TrashTalker.Controllers;
using TrashTalker.Database.Repositories.ContainerRepository;
using TrashTalker.Database.Repositories.MeasurementsRepository;
using TrashTalker.Dto.Container;
using TrashTalker.Models;
using TrashTalker.Services;
using Xunit;

namespace XUnitTests
{
    public class ContainerControllerTests
    {
        private readonly ContainerController _containerController;
        private readonly Mock<IContainerRepository> _containerRepoMock = new Mock<IContainerRepository>();
        private readonly Mock<IMeasurementRepository> _measurementRepository = new Mock<IMeasurementRepository>();
        private readonly IQrCodeService _qrCodeService = new QrCodeService();
        

        public ContainerControllerTests()
        {
            _containerController = new ContainerController(_containerRepoMock.Object, _measurementRepository.Object, _qrCodeService);
        }


        [Fact]
        public async Task GetContainerByIdAsync_ShouldReturnContainer_WhenContainerExists()
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

            _containerRepoMock.Setup(x => x.GetContainer(containerId))
                .ReturnsAsync(container);

            //Act
            var containerResult = await _containerController.getContainer(containerId);

            //Assert
            //Assert.Equal(containerId, containerResult.Value.id);
        }

        [Fact]
        public async Task GetContainerByIdAsync_ShouldReturnNull_WhenContainerNotExists()
        {
            //Arrange
            _containerRepoMock.Setup(x => x.GetContainer(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            //Act
            ActionResult<ContainerDTO> containerResult = await _containerController.getContainer(Guid.NewGuid());

            //Assert
            Assert.IsType<NotFoundResult>(containerResult.Result);
        }

        [Fact]
        public async Task UpdateContainer_ShouldReturnNotFound_WhenContainerNotExists()
        {
            //Arrange
            var containerId = Guid.NewGuid();

            var containerToBeUpdated = new UpdateContainerDTO()
            {
                height = 2.7f,
                width = 2.7f,
                depth = 2.7f
            };

            var containerUpdated = new Container()
            {
                id = containerId,
                status = TrashTalker.Models.Enumerations.Status.ACTIVE,
                typeOfWaste = TrashTalker.Models.Enumerations.TypeOfWaste.PAPER,
                height = 2.5f,
                width = 2.5f,
                depth = 2.5f
            };

            _containerRepoMock.Setup(x => x.disableContainer(Guid.NewGuid()))
                .ReturnsAsync(() => null);

            //Act
            ActionResult<ContainerDTO> containerResult = await _containerController.UpdateContainer(containerToBeUpdated, containerId);

            //Assert
            //Assert.IsType<NotFoundResult>(containerResult);
        }

        [Fact]
        public async Task DisableContainer_ShouldReturnNotFound_WhenContainerNotExists()
        {
            //Arrange
            _containerRepoMock.Setup(x => x.GetContainer(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            //Act
            IActionResult containerResult = await _containerController.disableContainer(Guid.NewGuid());

            //Assert
            Assert.IsType<NotFoundResult>(containerResult);
        }

        //TODO
        // Um contentor depois de atualizado deve ser igual ao contentorDTO utilizado para atualizar
        [Fact]
        public async Task UpdatedContainer_ShouldBeUpdated()
        {
            //Arrange
            var containerId = Guid.NewGuid();

            var newContainerDTO = new UpdateContainerDTO()
            {
                height = 2.7f,
                width = 2.7f,
                depth = 2.7f
            };

            var containerToBeUpdated = new Container()
            {
                id = containerId,
                status = TrashTalker.Models.Enumerations.Status.ACTIVE,
                typeOfWaste = TrashTalker.Models.Enumerations.TypeOfWaste.PAPER,
                height = 2.5f,
                width = 2.5f,
                depth = 2.5f
            };

            _containerRepoMock.Setup(x => x.UpdateContainer(containerToBeUpdated))
                .ReturnsAsync(containerToBeUpdated);

            //Act
            var containerResult = await _containerController.UpdateContainer(newContainerDTO, containerId);

            //Assert
            //Assert.Equal(2.7f, containerToBeUpdated.depth);
            //Assert.Equal(2.7f, containerToBeUpdated.height);
            //Assert.Equal(2.7f, containerToBeUpdated.width);
        }

        //TODO
        // Um contentor não deve ter height, width e depth com valores 0 ou negativos
        [Fact]
        public void Container_ShouldNotHave_0OrNegativeNumbers()
        {
            //Arrange


            //Act


            //Assert
        }
    }
}