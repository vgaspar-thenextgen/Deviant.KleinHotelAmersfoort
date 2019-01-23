using AutoMapper;
using Deviant.KleinHotelAmersfoort.DAL.Models;
using Deviant.KleinHotelAmersfoort.Services;
using Deviant.KleinHotelAmersfoort.Services.Models;
using Deviant.KleinHotelAmersfoort.WebApi.Controllers;
using Deviant.KleinHotelAmersfoort.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace Deviant.KleinHotelAmersfoort.WebApi.UnitTests
{
    public class ReactionsControllerTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IReactionsService> _reactionsServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        public ReactionsControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _reactionsServiceMock = _mockRepository.Create<IReactionsService>();
            _mapperMock = _mockRepository.Create<IMapper>();
        }

        [Fact]
        public void Get_WhenServiceReturnsResponse_ThenReturnsOk()
        {
            // Arrange
            var sut = new ReactionsController(_reactionsServiceMock.Object, _mapperMock.Object);

            var expected = new List<ReactionView>();
            _reactionsServiceMock
                .Setup(s => s.List("MHZ52UYA1ZL", 2, SortType.Score, OrderType.Asc))
                .Returns(expected);

            // Act
            var actual = sut.Get("MHZ52UYA1ZL", 2, SortType.Score, OrderType.Asc).Result as OkObjectResult;

            // Assert
            actual.Value.ShouldBe(expected);

            _mockRepository.VerifyAll();
        }

        [Fact]
        public void Post_WhenSavesSuccessfuly_ThenReturnsOk()
        {
            // Arrange
            var sut = new ReactionsController(_reactionsServiceMock.Object, _mapperMock.Object);

            var request = new ReactionSaveRequest
            {
                Token = "MHZ52UYA1ZL"
            };
            var mappedRequest = new Reaction();

            _mapperMock
                .Setup(m => m.Map<Reaction>(request))
                .Returns(mappedRequest);
            _reactionsServiceMock
                .Setup(s => s.Save(request.Token, mappedRequest));

            // Act
            var actual = sut.Post(request) as OkObjectResult;

            // Assert
            actual.Value.ShouldBe("Reaction saved!");

            _mockRepository.VerifyAll();
        }

        [Fact]
        public void Post_WhenServiceThrowsUnauthorized_ThenReturnsUnauthorized()
        {
            // Arrange
            var sut = new ReactionsController(_reactionsServiceMock.Object, _mapperMock.Object);

            var request = new ReactionSaveRequest
            {
                Token = "MHZ52UYA1ZL"
            };
            var mappedRequest = new Reaction();

            _mapperMock
                .Setup(m => m.Map<Reaction>(request))
                .Returns(mappedRequest);
            _reactionsServiceMock
                .Setup(s => s.Save(request.Token, mappedRequest))
                .Throws<UnauthorizedException>();

            // Act
            var actual = sut.Post(request);

            // Assert
            actual.ShouldBeOfType<UnauthorizedResult>();

            _mockRepository.VerifyAll();
        }

        [Fact]
        public void Delete_WhenDeletesSuccessfuly_ThenReturnsOk()
        {
            // Arrange
            var sut = new ReactionsController(_reactionsServiceMock.Object, _mapperMock.Object);

            var request = new ReactionDeleteRequest
            {
                Id = Guid.Parse("5304F61E-F365-4013-8F89-D2BEC135B7CA"),
                Token = "MHZ52UYA1ZL"
            };

            _reactionsServiceMock
                .Setup(s => s.Delete(request.Token, request.Id));

            // Act
            var actual = sut.Delete(request) as OkObjectResult;

            // Assert
            actual.Value.ShouldBe("Reaction deleted!");

            _mockRepository.VerifyAll();
        }

        [Fact]
        public void Delete_WhenServiceThrowsUnauthorized_ThenReturnsUnauthorized()
        {
            // Arrange
            var sut = new ReactionsController(_reactionsServiceMock.Object, _mapperMock.Object);

            var request = new ReactionDeleteRequest
            {
                Id = Guid.Parse("5304F61E-F365-4013-8F89-D2BEC135B7CA"),
                Token = "MHZ52UYA1ZL"
            };

            _reactionsServiceMock
                .Setup(s => s.Delete(request.Token, request.Id))
                .Throws<UnauthorizedException>();

            // Act
            var actual = sut.Delete(request);

            // Assert
            actual.ShouldBeOfType<UnauthorizedResult>();

            _mockRepository.VerifyAll();
        }
    }
}
