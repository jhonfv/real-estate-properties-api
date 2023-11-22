using AutoMapper;
using Moq;
using properties.Application.DTOs;
using properties.Application.Services;
using properties.Domain.Entities;
using properties.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace properties.Test
{
    [TestFixture]
    public class OwnerServiceTest
    {
        private Mock<IOwnerRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private OwnerService _ownerService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IOwnerRepository>();
            _mockMapper = new Mock<IMapper>();
            _ownerService = new OwnerService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CreateAsync_ShouldReturnSuccess_WhenOwnerIsCreated()
        {
            // Arrange
            var ownerDTO = new OwnerDTO { IdOwner=1, Name="Jhon"};
            var owner = new Owner { IdOwner=1, Name="Jhon" };

            _mockMapper.Setup(m => m.Map<Owner>(It.IsAny<OwnerDTO>())).Returns(owner);
            _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Owner>())).ReturnsAsync(owner);
            _mockMapper.Setup(m => m.Map<OwnerDTO>(It.IsAny<Owner>())).Returns(ownerDTO);

            // Act
            var result = await _ownerService.CreateAsync(ownerDTO);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ownerDTO, result.data);
            _mockRepository.Verify(r => r.CreateAsync(It.IsAny<Owner>()), Times.Once);
            _mockMapper.Verify(m => m.Map<Owner>(It.IsAny<OwnerDTO>()), Times.Once);
            _mockMapper.Verify(m => m.Map<OwnerDTO>(It.IsAny<Owner>()), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnOwners_WhenOwnersExist()
        {
            // Arrange
            var owners = new List<Owner> { new Owner() { IdOwner=1, Name = "Mr John" } };
            var ownerDTOs = new List<OwnerDTO> { new OwnerDTO() { IdOwner = 1, Name = "Mr John" } };

            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(owners);
            _mockMapper.Setup(m => m.Map<IEnumerable<OwnerDTO>>(owners)).Returns(ownerDTOs);

            // Act
            var result = await _ownerService.getAllAsync();

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ownerDTOs, result.data);
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<IEnumerable<OwnerDTO>>(owners), Times.Once);
        }


    }
}
