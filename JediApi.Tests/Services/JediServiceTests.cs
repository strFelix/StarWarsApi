using JediApi.Data;
using JediApi.Models;
using JediApi.Repositories;
using JediApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        // não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }


        [Fact]
        public async Task GetAll()
        {
            
            List<Jedi> jedisSpected = new List<Jedi>()
            {
                new Jedi { Id = 1, Name = "test1", Strength = 10, Version = 1 },
                new Jedi { Id = 2, Name = "test2", Strength = 10, Version = 1 },
            };

            _repositoryMock.Setup(repository => repository.GetAllAsync()).ReturnsAsync(jedisSpected);
            var result = await _service.GetAllAsync();

            Assert.Equal(jedisSpected, result);
        }

        
        [Fact]
        public async Task GetById_Success()
        {
           Jedi jediSpected = new Jedi() {
               Id = 1, 
               Name = "test1", 
               Strength = 10, 
               Version = 1 
           };

            _repositoryMock.Setup(repository => repository.GetByIdAsync(jediSpected.Id)).ReturnsAsync(jediSpected);
            var result = await _service.GetByIdAsync(jediSpected.Id);

            Assert.Equal(jediSpected, result);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            Jedi jediSpected = new Jedi()
            {
                Id = 1,
                Name = "test1",
                Strength = 10,
                Version = 1
            };

            _repositoryMock.Setup(repository => repository.GetByIdAsync(2)).ReturnsAsync(jediSpected);
            var result = await _service.GetByIdAsync(2);

            Assert.Equal(jediSpected, result);
        }
    }
}
