using SocialVeterinarySurgeon.DAL;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using SocialVeterinarySurgeon.BL.Services;
using SocialVeterinarySurgeon.Domain.Entities;
using Xunit;
using SocialVeterinarySurgeon.Mapper;

namespace SocialVeterinarySurgeon.UnitTests
{
    public class EmployeeServiceTests 
    {
        [Fact]
        public async Task Upsert_NewEmployee_WorksCorrectly()
        {
            //arrange 
            var loggerMock = new Mock<ILogger<EmployeeService>>();
            var mapperMock = new Mock<IMapper>();

            SocialVeterinarySurgeonDbContext db = CreateDb();
            db.Employees.AddRange(new Employee
                {
                    Id = 15,
                    Name = "Sergio",
                    LastName = "Ramos",
                    FromMediaInteractiva = false
                },
                new Employee
                {
                    Id = 20,
                    Name = "Marco",
                    LastName = "Asensio",
                    FromMediaInteractiva = false
                });
            db.SaveChanges();

            var employeeService = new EmployeeService(db, mapperMock.Object, loggerMock.Object);

            var employee = new Employee
            {
                Name = "Daniel",
                LastName = "Carvajal"
            };


            //act
            await employeeService.Upsert(employee);

            //assert
            var result = db.Employees.FirstOrDefault(e => e.LastName == "Carvajal");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Upsert_ExistingEmployee_WorksCorrectly()
        {
            //arrange
            var loggerMock = new Mock<ILogger<EmployeeService>>();
            
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            var mapper = config.CreateMapper();

            await using SocialVeterinarySurgeonDbContext db = CreateDb();

            db.Employees.AddRange(new Employee
                {
                    Id = 19,
                    Name = "Diego",
                    LastName = "Kosta",
                    FromMediaInteractiva = false
                },
                new Employee
                {
                    Id = 22,
                    Name = "Isco",
                    LastName = "Alarcon",
                    FromMediaInteractiva = false
                });
            db.SaveChanges();

            var employee = new Employee
            {
                Id = 19,
                Name = "Diego",
                LastName = "Devil Kosta",
            };

            var employeeService = new EmployeeService(db, mapper, loggerMock.Object);
            
            //act
            await employeeService.Upsert(employee);

            //assert
            var result = db.Employees.FirstOrDefault(e => e.Id == 19);

            Assert.Equal(employee.Name, result.Name);
            Assert.Equal(employee.LastName, result.LastName);
        }

        [Fact]
        public async Task GetById_ExistingEmployee_WorksCorrectly()
        {
            //arrange
            var loggerMock = new Mock<ILogger<EmployeeService>>();
            var mapperMock = new Mock<IMapper>();
            SocialVeterinarySurgeonDbContext db = CreateDb();
            var employeeService = new EmployeeService(db, mapperMock.Object, loggerMock.Object);

            db.Employees.AddRange(new Employee
                {
                    Id = 18,
                    Name = "Jordi",
                    LastName = "Alba",
                    FromMediaInteractiva = false
                },
                new Employee
                {
                    Id = 5,
                    Name = "Sergio",
                    LastName = "Busquets",
                    FromMediaInteractiva = false
                });
            db.SaveChanges();
            //act
            var result = await employeeService.GetById(18);

            //assert

            Assert.Equal("Jordi", result.Name);
            Assert.Equal("Alba", result.LastName);
        }

        [Fact]
        public async Task GetById_NotExistingEmployee_ThrowsException()
        {
            //arrange
            var loggerMock = new Mock<ILogger<EmployeeService>>();
            var mapperMock = new Mock<IMapper>();
            SocialVeterinarySurgeonDbContext db = CreateDb();
            var employeeService = new EmployeeService(db, mapperMock.Object, loggerMock.Object);

            db.Employees.AddRange(new Employee
                {
                    Id = 1,
                    Name = "David",
                    LastName = "De Gea",
                    FromMediaInteractiva = false
                },
                new Employee
                {
                    Id = 3,
                    Name = "Gerard",
                    LastName = "Piqué",
                    FromMediaInteractiva = false
                });
            db.SaveChanges();

            var employeeId = 14;
            //act
            var result = await Record.ExceptionAsync(async () => await employeeService.GetById(employeeId));

            //assert

            Assert.NotNull(result);
            Assert.Equal(result.Message, $"There is no {typeof(Employee).ShortDisplayName()} with id: {employeeId}");
        }

        private SocialVeterinarySurgeonDbContext CreateDb()
        {
            var options = new DbContextOptionsBuilder<SocialVeterinarySurgeonDbContext>()
                .UseInMemoryDatabase(databaseName: $"SocialVeterinarySurgeon")
                .Options;

            return new SocialVeterinarySurgeonDbContext(options);
        }
    }
}
