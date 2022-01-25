using System;
using System.Collections.Generic;
using System.Linq;
using TrashTalker.Dto.Alert;
using TrashTalker.Dto.Container;
using TrashTalker.Dto.Picking;
using TrashTalker.Dto.RecycleBin;
using TrashTalker.Dto.Route;
using TrashTalker.Dto.User;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Helpers
{
    public static class Extensions
    {
        public static User asUser(this CreateUserDTO createUser)
        {
            return new User
            {
                id = Guid.NewGuid(),
                status = Status.ACTIVE,
                username = createUser.username,
                password = createUser.password,
                firstName = createUser.firstName,
                lastName = createUser.lastName,
                email = createUser.email,
                gender = createUser.gender.toGender(),
                city = createUser.city,
                street = createUser.street,
                country = createUser.country,
                role = createUser.role.toRole(),
                zipCode = createUser.zipCode
            };
        }

        public static UserDTO asDTO(this User user)
        {
            var dto = new UserDTO
            {
                id = user.id,
                status = user.status.ToString().capitalizeFirstLetter()
            };

            dto.username = user.username;
            dto.firstName = user.firstName;
            dto.lastName = user.lastName;
            dto.email = user.email;
            dto.gender = user.gender.ToString().capitalizeFirstLetter();
            dto.city = user.city;
            dto.street = user.street;
            dto.country = user.country;
            dto.role = user.role.ToString().capitalizeFirstLetter();
            dto.zipCode = user.zipCode;
            return dto;
        }

        public static RecycleBin asRecycleBin(this CreateRecycleBinDTO createBin)
        {
            return new RecycleBin
            {
                id = Guid.NewGuid(),
                status = Status.ACTIVE,
                // longit = createBin.longit,
                // latit = createBin.latit,
                containers = new List<Container>(),
                street = createBin.street,
                city = createBin.city,
                country = createBin.country,
                zipCode = createBin.zipCode
            };
        }

        public static RecycleBinDTO asDTO(this RecycleBin bin)
        {
            return new RecycleBinDTO
            {
                id = bin.id,
                Status = bin.status.ToString().capitalizeFirstLetter(),
                longit = bin.longit,
                latit = bin.latit,
                street = bin.street,
                city = bin.city,
                country = bin.country,
                zipCode = bin.zipCode,
                containerDtos = bin.containers.Select(cont => cont.asDTO()).ToList()
            };
        }

        public static Container asContainer(this CreateContainerDTO container)
        {
            return new Container
            {
                id = Guid.NewGuid(),
                status = Status.ACTIVE,
                typeOfWaste = container.typeOfWaste.asTypeOfWaste(),
                height = container.height,
                width = container.width,
                depth = container.depth,
                avgGrowthOccupiedVolumePerDay = 0.0,
                currentPercOccupied = 0.0f
            };
        }

        public static ContainerDTO asDTO(this Container container)
        {
            return new ContainerDTO
            {
                id = container.id,
                status = container.status.ToString().capitalizeFirstLetter(),
                typeOfWaste = container.typeOfWaste.ToString().capitalizeFirstLetter(),
                height = container.height,
                width = container.width,
                depth = container.depth,
                recyclerBinId = container.RecBin.id,
                avgGrowthOccupiedVolumePerDay = container.avgGrowthOccupiedVolumePerDay,
                dateFull = container.dateFull,
                previsionOrDaysFull = container.setDuration(),
                percentageOccupied = container.currentPercOccupied
            };
        }

        public static ContainerAlertDTO asAlertDTO(this Container container)
        {
            return new ContainerAlertDTO
            {
                id = container.id,
                status = container.status.ToString().capitalizeFirstLetter(),
                typeOfWaste = container.typeOfWaste.ToString().capitalizeFirstLetter(),
                height = container.height,
                width = container.width,
                depth = container.depth,
                recyclerBin = container.RecBin.asDTO(),
                avgGrowthOccupiedVolumePerDay = container.avgGrowthOccupiedVolumePerDay,
                percentageOccupied = container.currentPercOccupied,
                dateFull = container.dateFull,
                previsionOrDaysFull = container.setDuration()
            };
        }

        public static Picking asPicking(this CreatePickingDTO picking)
        {
            return new Picking
            {
                id = Guid.NewGuid(),
                volumeRecolhido = picking.volumeRecolhido,
                data = DateTime.UtcNow,
            };
        }

        public static PickingDTO asDTO(this Picking picking)
        {
            return new PickingDTO
            {
                id = picking.id,
                volumeRecolhido = picking.volumeRecolhido,
                container = picking.container.asDTO(),
                date = picking.data
            };
        }

        public static Route asRoute(this CreateRouteDto route)
        {
            return new Route
            {
                id = Guid.NewGuid(),
                name = route.name,
                dateBegin = route.dateBegin,
                status = StatusRoute.PLANNED,
                typeCreation = TypeCreation.MANUAL,
                dateCriation = DateTime.Now,
            };
        }

        public static Route asRoute(this CreateAutomaticRoute createAutomatic, string routeString)
        {
            return new Route
            {
                id = Guid.NewGuid(),
                name = routeString,
                dateBegin = createAutomatic.dateBegin,
                typeCreation = TypeCreation.AUTO,
                status = StatusRoute.PLANNED,
                dateCriation = DateTime.Now,
            };
        }
        
        public static Route asRoutePending(this CreateAutomaticRoute createAutomatic, string routeString)
        {
            return new Route
            {
                id = Guid.NewGuid(),
                name = routeString,
                dateBegin = createAutomatic.dateBegin,
                typeCreation = TypeCreation.AUTO,
                status = StatusRoute.PENDING,
                dateCriation = DateTime.Now,
            };
        }

        public static RouteDto asDto(this Route route)
        {
            return new RouteDto
            {
                id = route.id,
                name = route.name,
                dateBegin = route.dateBegin,
                status = route.status.ToString().capitalizeFirstLetter(),
                typeCreation = route.typeCreation.ToString().capitalizeFirstLetter(),
                dateCriation = route.dateCriation,
                duration = route.dateEnd == null ? null : (route.dateEnd - route.dateBegin).ToString(),
                dateEnd = route.dateEnd,
                distanceTravelledKm = route.distanceTravelledKm,
                estimatedDuration = route.estimatedDuration.ToString(),
                distanceEstimatedKm = route.distanceEstimatedKm,
                recycleBins = route.collectPoints.asRcycleBinDto(),
                employee = route.employee.asDTO(),
                startingPoint = Config.startingPoint
            };
        }

        public static Alert asAlert(this CreateAlertDTO createAlert)
        {
            return new Alert
            {
                id = Guid.NewGuid(),
                alertStatus = AlertStatus.UNRESOLVED,
                issue = createAlert.issue,
                alertType = createAlert.alertType.asAlertType(),
                date = DateTime.Now
            };
        }

        public static AlertDTO asDTO(this Alert alert)
        {
            return new AlertDTO
            {
                id = alert.id,
                issue = alert.issue,
                alertStatus = alert.alertStatus.ToString().capitalizeFirstLetter(),
                alertType = alert.alertType.ToString().capitalizeFirstLetter(),
                date = alert.date,
                employeeId = alert.employee.id
            };
        }

        public static IList<RecycleBinDTO> asListRcycleBinDto(this IList<RecycleBin> recycleBins) =>
            new List<RecycleBinDTO>(recycleBins.Select(recycleBin => recycleBin.asDTO()));

        public static IList<RecycleBinDTO> asRcycleBinDto(this List<CollectPoint> collectPoints) =>
            new List<RecycleBinDTO>(collectPoints.OrderBy(x => x.order).Select(x => x.recycleBin.asDTO()));

        public static TypeOfWaste asTypeOfWaste(this string typeOfWasteString)
        {
            return (TypeOfWaste) Enum.Parse(typeof(TypeOfWaste), typeOfWasteString.ToUpper());
        }

        public static AlertType asAlertType(this string alertType)
        {
            return (AlertType) Enum.Parse(typeof(AlertType), alertType.ToUpper());
        }

        public static StatusRoute asStatusRoute(this string statusRouteString)
        {
            return (StatusRoute) Enum.Parse(typeof(StatusRoute), statusRouteString.ToUpper());
        }

        public static Role toRole(this string role)
        {
            return (Role) Enum.Parse(typeof(Role), role.ToUpper());
        }

        public static Gender toGender(this string gender)
        {
            return (Gender) Enum.Parse(typeof(Gender), gender.ToUpper());
        }

        public static string capitalizeFirstLetter(this string str)
        {
            return $"{str[..1].ToUpper()}{str[1..].ToLower()}";
        }

        public static bool IsNullOrEmpty(this Guid guid) => guid == Guid.Empty;
    }
}