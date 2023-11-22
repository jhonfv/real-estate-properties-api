using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using properties.Application.DTOs.Properties;
using properties.Application.Services;
using properties.Domain.Entities;
using properties.Domain.Filters;
using properties.Domain.Interfaces;

namespace properties.Test
{
    [TestFixture]
    public class PropertyServicesTest
    {
        private Mock<IPropertyRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<IFakeCDNExternalService> _mockFakeCDNExternalService;
        private Mock<IConfiguration> _mockConfiguration;
        private PropertyServices _propertyService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IPropertyRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockFakeCDNExternalService = new Mock<IFakeCDNExternalService>();
            _mockConfiguration = new Mock<IConfiguration>();

            _propertyService = new PropertyServices(
                _mockRepository.Object,
                _mockMapper.Object,
                _mockFakeCDNExternalService.Object,
                _mockConfiguration.Object); 
        }
        [Test]
        public async Task CreateAsync_ShouldReturnSuccess_WhenPropertyIsCreated()
        {
            // Arrange
            var createPropertyDTO = new CreatePropertyDTO { IdOwner=1, Name="Super Home 1",  Year=2023, Address="voul 1", Price=1500000};
            var property = new Property {IdProperty=1, CodeInternal="P-0001", IdOwner = 1, Name = "Super Home 1", Year = 2023, Address = "voul 1", Price = 1500000 };
            var propertyDTO = new PropertyDTO {IdProperty=1, CodeInternal="P-0001", IdOwner = 1, Name = "Super Home 1", Year = 2023, Address = "voul 1", Price = 1500000 };

            _mockMapper.Setup(m => m.Map<Property>(It.IsAny<CreatePropertyDTO>())).Returns(property);
            _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Property>())).ReturnsAsync(property);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Property>())).ReturnsAsync(property);
            _mockMapper.Setup(m => m.Map<PropertyDTO>(It.IsAny<Property>())).Returns(propertyDTO);

            // Act
            var result = await _propertyService.createAsync(createPropertyDTO);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(propertyDTO, result.data);
            _mockRepository.Verify(r => r.CreateAsync(It.IsAny<Property>()), Times.Once);
            _mockMapper.Verify(m => m.Map<Property>(It.IsAny<CreatePropertyDTO>()), Times.Once);
            _mockMapper.Verify(m => m.Map<PropertyDTO>(It.IsAny<Property>()), Times.Once);
        }

        [Test]
        public async Task ChangePriceAsync_ShouldReturnUpdatedProperty_WhenPriceIsChanged()
        {
            // Arrange
            int idProperty = 1;
            double newPrice = 2000000;
            var property = new Property { IdProperty = idProperty, Price = 1500000 }; 
            var updatedProperty = new Property { IdProperty = idProperty, Price = newPrice }; 
            var propertyDTO = new PropertyDTO { IdProperty = idProperty, Price = newPrice }; 

            _mockRepository.Setup(r => r.ChangePriceAsync(It.Is<Property>(p => p.IdProperty == idProperty && p.Price == newPrice)))
                           .ReturnsAsync(updatedProperty);
            _mockMapper.Setup(m => m.Map<PropertyDTO>(updatedProperty)).Returns(propertyDTO);

            // Act
            var result = await _propertyService.changePriceAsync(newPrice, idProperty);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(newPrice, result.data.Price);
            _mockRepository.Verify(r => r.ChangePriceAsync(It.IsAny<Property>()), Times.Once);
            _mockMapper.Verify(m => m.Map<PropertyDTO>(updatedProperty), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnUpdatedProperty_WhenPropertyIsUpdated()
        {
            // Arrange
            var propertyToUpdate = new Property { IdProperty = 1, Name = "Updated Name", Price = 2000000, IdOwner=1, CodeInternal="P-0001", Address="Address", Year=2023 }; 
            var propertyDTO = new PropertyDTO { IdProperty = 1, Name = "Updated Name", Price = 2000000, IdOwner = 1, CodeInternal = "P-0001", Address = "Address", Year = 2023 }; 

            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Property>())).ReturnsAsync(propertyToUpdate);
            _mockMapper.Setup(m => m.Map<PropertyDTO>(It.IsAny<Property>())).Returns(propertyDTO);

            // Act
            var result = await _propertyService.UpdateAsync(propertyDTO);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(propertyDTO, result.data);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Property>()), Times.Once);
            _mockMapper.Verify(m => m.Map<PropertyDTO>(It.IsAny<Property>()), Times.Once);
        }

        [Test]
        public async Task AddImageAsync_ShouldReturnSuccess_WhenImageIsAdded()
        {
            // Arrange
            int idProperty = 1;
            var fakeImageFile = new Mock<IFormFile>();
            var pathImage = "path/to/image.jpg";
            var propertyImageDTO = new PropertyImageDTO
            {
                IdProperty = idProperty,
                FilePath = pathImage,
                Enabled = true
            };
            var propertyImage = new PropertyImage
            {
                IdProperty = idProperty,
                FilePath = pathImage,
                Enabled = true
            };

            _mockFakeCDNExternalService.Setup(s => s.saveImageAsync(It.IsAny<IFormFile>())).ReturnsAsync(pathImage);
            _mockMapper.Setup(m => m.Map<PropertyImageDTO>(It.IsAny<PropertyImage>())).Returns(propertyImageDTO);
            _mockRepository.Setup(r => r.AddImageAsync(It.IsAny<PropertyImage>())).ReturnsAsync(propertyImage);
            _mockConfiguration.Setup(c => c.GetSection("FakeCDN").Value).Returns("https://localhost:7281/home/UploadImage");



            // Act
            var result = await _propertyService.addImageAsync(fakeImageFile.Object, idProperty);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(propertyImageDTO, result.data);
            _mockFakeCDNExternalService.Verify(s => s.saveImageAsync(It.IsAny<IFormFile>()), Times.Once);
            _mockMapper.Verify(m => m.Map<PropertyImageDTO>(It.IsAny<PropertyImage>()), Times.Once);
            _mockRepository.Verify(r => r.AddImageAsync(It.IsAny<PropertyImage>()), Times.Once);
        }

        [Test]
        public async Task GetByFiltersAsync_ShouldReturnProperties_WhenFiltersAreApplied()
        {
            // Arrange
            var filterPropertyDTO = new FilterPropertyDTO { Year=2023, PageNumber=20, PageSize=1 };
            var filters = new FilterProperty { Year = 2023, PageNumber = 20, PageSize = 1 };
            var properties = new List<Property> { new Property() { IdOwner = 1, Year = 2023 } };
            var propertyDTOs = new List<PropertyDTO> { new PropertyDTO() { IdOwner=1, Year=2023} };

            _mockMapper.Setup(m => m.Map<FilterProperty>(filterPropertyDTO)).Returns(filters);
            _mockRepository.Setup(r => r.getByFiltersAsync(filters)).ReturnsAsync(properties);
            _mockMapper.Setup(m => m.Map<IEnumerable<PropertyDTO>>(properties)).Returns(propertyDTOs);

            // Act
            var result = await _propertyService.getByFiltersAsync(filterPropertyDTO);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(propertyDTOs, result.data);
            _mockRepository.Verify(r => r.getByFiltersAsync(filters), Times.Once);
            _mockMapper.Verify(m => m.Map<FilterProperty>(filterPropertyDTO), Times.Once);
            _mockMapper.Verify(m => m.Map<IEnumerable<PropertyDTO>>(properties), Times.Once);
        }



    }
}
