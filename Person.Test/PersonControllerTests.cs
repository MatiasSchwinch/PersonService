using Microsoft.AspNetCore.Mvc;
using Moq;
using Person.API.Controllers;
using Person.Domain.PersonAggregate;
using Person.Domain.PersonAggregate.DTO;

namespace Person.Test
{
    public class PersonControllerTests
    {
        private readonly Mock<IPersonRepository> _repository;

        public PersonControllerTests()
        {
            _repository = new Mock<IPersonRepository>();
        }

        [Fact]
        public async void GetAllPerson_IfValidFilters()
        {
            //Arrange
            var persons = GetSamplePersonBasicData();
            var filters = new string[]
            {
                "filters=age>35",
                "filters=nationality=AU"
            };

            _repository.Setup(setup => setup.GetAllAsync(filters)).ReturnsAsync(persons);

            var controller = new PersonController(null!, _repository.Object);

            //Act
            var actionResult = await controller.GetAll(filters);
            var result = actionResult.Result as OkObjectResult;
            var actual = result!.Value;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actual);
        }

        [Fact]
        public async void GetAllPerson_IfNotValidFilters()
        {
            //Arrange
            var persons = GetSamplePersonBasicData();
            var filters = new string[] { };

            _repository.Setup(setup => setup.GetAllAsync(filters));

            var controller = new PersonController(null!, _repository.Object);

            //Act
            var actionResult = await controller.GetAll(filters);
            var result = actionResult.Result as NotFoundObjectResult;
            var actual = result!.Value;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            //Assert.NotNull(actual);
        }

#pragma warning disable
        private IEnumerable<PersonEntityDto> GetSamplePerson()
        {
            return new List<PersonEntityDto>()
            {
                new PersonEntityDto
                {
                    PersonId = 77,
                    Title = "Ms",
                    FirstName = "Liliana",
                    LastName = "Schrader",
                    Age = 67,
                    Email = "liliana.schrader@example.com",
                    Phone = "0520-9215544",
                    Cell = "0173-5972968",
                    Nationality = "DE",
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Gender = 1,
                    Location = new LocationDto
                    {
                        LocationId = 77,
                        StreetNumber = 9666,
                        StreetName = "Heideweg",
                        City = "Kitzingen",
                        Country = "Germany",
                        Postcode = "34212",
                        State = "Bayern",
                        Coordinates = new CoordinateDto() { },
                        Timezone = new TimezoneDto() { },
                    },
                    Login = new LoginDto
                    {
                        LoginId = 2048,
                        Username = "silverfish810",
                        Password = "fellow"
                    },
                    Picture = new PictureDto
                    {
                        PictureId = 1392,
                        Thumbnail = "https://randomuser.me/api/portraits/med/women/86.jpg"
                    },
                    Registered = new RegisteredDto { RegisteredId = 4611, Age = 10 }
                },
                new PersonEntityDto
                {
                    PersonId = 45,
                    Title = "Miss",
                    FirstName = "Isobel",
                    LastName = "Welch",
                    Age = 55,
                    Email = "isobel.welch@example.com",
                    Phone = "01-5260-2990",
                    Cell = "0446-996-148",
                    Nationality = "AU",
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Gender = 1,
                    Location = new LocationDto
                    {
                        LocationId = 45,
                        StreetNumber = 9666,
                        StreetName = "Heideweg",
                        City = "Kitzingen",
                        Country = "Germany",
                        Postcode = "34212",
                        State = "Bayern",
                        Coordinates = new CoordinateDto() { },
                        Timezone = new TimezoneDto() { },
                    },
                    Login = new LoginDto
                    {
                        LoginId = 2048,
                        Username = "silverfish810",
                        Password = "fellow"
                    },
                    Picture = new PictureDto
                    {
                        PictureId = 1392,
                        Thumbnail = "https://randomuser.me/api/portraits/med/women/86.jpg"
                    },
                    Registered = new RegisteredDto { RegisteredId = 4611, Age = 10 }
                },
                new PersonEntityDto
                {
                    PersonId = 54,
                    Title = "Mr",
                    FirstName = "Duane",
                    LastName = "Gonzales",
                    Age = 52,
                    Email = "duane.gonzales@example.com",
                    Phone = "0520-9215544",
                    Cell = "0173-5972968",
                    Nationality = "AU",
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Gender = 0,
                    Location = new LocationDto
                    {
                        LocationId = 77,
                        StreetNumber = 9666,
                        StreetName = "Heideweg",
                        City = "Kitzingen",
                        Country = "Germany",
                        Postcode = "34212",
                        State = "Bayern",
                        Coordinates = new CoordinateDto() { },
                        Timezone = new TimezoneDto() { },
                    },
                    Login = new LoginDto
                    {
                        LoginId = 2048,
                        Username = "silverfish810",
                        Password = "fellow"
                    },
                    Picture = new PictureDto
                    {
                        PictureId = 1392,
                        Thumbnail = "https://randomuser.me/api/portraits/med/women/86.jpg"
                    },
                    Registered = new RegisteredDto { RegisteredId = 4611, Age = 10 }
                }
            };
        }

        private IEnumerable<BasicDataDto> GetSamplePersonBasicData()
        {
            return new List<BasicDataDto>()
            {
                new BasicDataDto
                {
                    PersonId = 77,
                    Title = "Ms",
                    FirstName = "Liliana",
                    LastName = "Schrader",
                    Age = 67,
                    Email = "liliana.schrader@example.com",
                    Phone = "0520-9215544",
                    Cell = "0173-5972968",
                    Nationality = "DE",
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Gender = 1
                },
                new BasicDataDto
                {
                    PersonId = 45,
                    Title = "Miss",
                    FirstName = "Isobel",
                    LastName = "Welch",
                    Age = 55,
                    Email = "isobel.welch@example.com",
                    Phone = "01-5260-2990",
                    Cell = "0446-996-148",
                    Nationality = "AU",
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Gender = 1
                },
                new BasicDataDto
                {
                    PersonId = 54,
                    Title = "Mr",
                    FirstName = "Duane",
                    LastName = "Gonzales",
                    Age = 52,
                    Email = "duane.gonzales@example.com",
                    Phone = "0520-9215544",
                    Cell = "0173-5972968",
                    Nationality = "AU",
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Gender = 0
                }
            };
        }
    }
}
