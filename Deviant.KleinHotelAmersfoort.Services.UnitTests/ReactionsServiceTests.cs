using AutoMapper;
using Deviant.KleinHotelAmersfoort.DAL;
using Deviant.KleinHotelAmersfoort.DAL.Models;
using Deviant.KleinHotelAmersfoort.Services.Models;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Deviant.KleinHotelAmersfoort.Services.UnitTests
{
    public class ReactionsServiceTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IReactionsRepository> _reactionsRepositoryMock;
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public ReactionsServiceTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _reactionsRepositoryMock = _mockRepository.Create<IReactionsRepository>();
            _usersRepositoryMock = _mockRepository.Create<IUsersRepository>();
            _mapperMock = _mockRepository.Create<IMapper>();
        }

        [Fact]
        public void List_WhenIsNotAdmin_ThenDoesntShowEmails()
        {
            // Arrange
            var sut = new ReactionsService(_reactionsRepositoryMock.Object, _usersRepositoryMock.Object, _mapperMock.Object);

            var dummyQueryResults = new List<Reaction>();
            _reactionsRepositoryMock
                .Setup(r => r.List(3, SortType.Score, OrderType.Asc))
                .Returns(dummyQueryResults);

            var dummyMappedResults = new List<ReactionView>
            {
                new ReactionView
                {
                    Email = "Etiam@maurisMorbi.org"
                },
                new ReactionView
                {
                    Email = "in@elit.com"
                },
                new ReactionView
                {
                    Email = "facilisis@a.org"
                }
            };
            _mapperMock
                .Setup(m => m.Map<List<ReactionView>>(dummyQueryResults))
                .Returns(dummyMappedResults);

            var dummyUser = new User();
            _usersRepositoryMock
                .Setup(r => r.GetByToken("KIT20IWP3LQ"))
                .Returns(dummyUser);

            // Act
            var actual = sut.List("KIT20IWP3LQ", 3, SortType.Score, OrderType.Asc);

            // Assert
            actual.Count().ShouldBe(3);
            actual.ShouldAllBe(r => r.Email == null);

            _mockRepository.VerifyAll();
        }

        [Fact]
        public void List_WhenIsAdmin_ThenShowEmails()
        {
            // Arrange
            var sut = new ReactionsService(_reactionsRepositoryMock.Object, _usersRepositoryMock.Object, _mapperMock.Object);

            var dummyQueryResults = new List<Reaction>();
            _reactionsRepositoryMock
                .Setup(r => r.List(3, SortType.Score, OrderType.Asc))
                .Returns(dummyQueryResults);

            var dummyMappedResults = new List<ReactionView>
            {
                new ReactionView
                {
                    Email = "Etiam@maurisMorbi.org"
                },
                new ReactionView
                {
                    Email = "in@elit.com"
                },
                new ReactionView
                {
                    Email = "facilisis@a.org"
                }
            };
            _mapperMock
                .Setup(m => m.Map<List<ReactionView>>(dummyQueryResults))
                .Returns(dummyMappedResults);

            var dummyUser = new User
            {
                Rights = new List<Right> { Right.AllowRemoveReactions }
            };
            _usersRepositoryMock
                .Setup(r => r.GetByToken("KIT20IWP3LQ"))
                .Returns(dummyUser);

            // Act
            var actual = sut.List("KIT20IWP3LQ", 3, SortType.Score, OrderType.Asc);

            // Assert
            actual.Count().ShouldBe(3);
            actual.ShouldAllBe(r => r.Email != null);

            _mockRepository.VerifyAll();
        }

        [Fact]
        public void Save_WhenUserNotKnown_ThenThrowUnauthorizedException()
        {
            // Arrange
            var sut = new ReactionsService(_reactionsRepositoryMock.Object, _usersRepositoryMock.Object, _mapperMock.Object);

            _usersRepositoryMock
                .Setup(r => r.GetByToken("KIT20IWP3LQ"))
                .Returns((User)null);

            // Act & Assert
            Should.Throw<UnauthorizedException>(() => sut.Save("KIT20IWP3LQ", new Reaction()));

            _mockRepository.VerifyAll();
        }

        [Fact]
        public void Save_WhenUserKnown_ThenSavesReaction()
        {
            // Arrange
            var sut = new ReactionsService(_reactionsRepositoryMock.Object, _usersRepositoryMock.Object, _mapperMock.Object);

            var dummyReaction = new Reaction
            {
                Name = "Old name"
            };
            var dummyUser = new User
            {
                Name = "New name"
            };
            _usersRepositoryMock
                .Setup(r => r.GetByToken("KIT20IWP3LQ"))
                .Returns(dummyUser);

            _reactionsRepositoryMock
                .Setup(rep => rep.Save(It.Is<Reaction>(rct => rct.Name == "New name")));

            // Act & Assert
            sut.Save("KIT20IWP3LQ", dummyReaction);

            _mockRepository.VerifyAll();
        }

        [Fact]
        public void Delete_WhenUserNotAdmin_ThenThrowUnauthorizedException()
        {
            // Arrange
            var sut = new ReactionsService(_reactionsRepositoryMock.Object, _usersRepositoryMock.Object, _mapperMock.Object);

            var dummyUser = new User();
            _usersRepositoryMock
                .Setup(r => r.GetByToken("KIT20IWP3LQ"))
                .Returns(dummyUser);

            // Act & Assert
            Should.Throw<UnauthorizedException>(() => sut.Delete("KIT20IWP3LQ", It.IsAny<Guid>()));

            _mockRepository.VerifyAll();
        }

        [Fact]
        public void Delete_WhenUserKnown_ThenSavesReaction()
        {
            // Arrange
            var sut = new ReactionsService(_reactionsRepositoryMock.Object, _usersRepositoryMock.Object, _mapperMock.Object);

            var id = Guid.Parse("58018B2E-15BD-42D4-8A16-80465956BEF8");
            var dummyUser = new User
            {
                Rights = new List<Right> { Right.AllowRemoveReactions }
            };
            _usersRepositoryMock
                .Setup(r => r.GetByToken("KIT20IWP3LQ"))
                .Returns(dummyUser);

            _reactionsRepositoryMock
                .Setup(rep => rep.Delete(id));

            // Act & Assert
            sut.Delete("KIT20IWP3LQ", id);

            _mockRepository.VerifyAll();
        }
    }
}
