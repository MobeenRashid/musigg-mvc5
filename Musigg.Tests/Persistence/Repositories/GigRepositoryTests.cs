using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musicly.Core.Repositories;
using Moq;
using Musicly.Persistence;
using Musicly.Persistence.Repositories;
using System.Data.Entity;
using Musicly.Core.Models;
using System.Collections;
using Musigg.Tests.Extensions;
using FluentAssertions;

namespace Musigg.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigsRepository _repository;
        private Mock<DbSet<Gig>> _mockSet;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockSet = new Mock<DbSet<Gig>>();
            var mockContext = new Mock<IApplicationDbContext>();

            mockContext.SetupGet(c => c.Gigs).Returns(_mockSet.Object);

            _repository = new GigsRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_IsPastGig_ShouldNotBeReturned()
        {
            var gig = new Gig()
            {
                DateTime = DateTime.Today.AddDays(-1),
                ArtistId = "1"
            };

            _mockSet.SetupSource<Gig>(new[] { gig });

            var result = _repository.GetUpcomingGigsByArtist("1");
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShoudNotBeReturned()
        {

            var gig = new Gig()
            {
                DateTime = DateTime.Today.AddDays(1),
                ArtistId = "1"
            };
            gig.Cancel();

            _mockSet.SetupSource<Gig>(new[] { gig });

            var result = _repository.GetUpcomingGigsByArtist("1");
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShoudNotBeReturned()
        {

            var gig = new Gig()
            {
                DateTime = DateTime.Today.AddDays(1),
                ArtistId = "1"
            };
            
            _mockSet.SetupSource<Gig>(new[] { gig });

            var result = _repository.GetUpcomingGigsByArtist("2");
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForSameArtistAndIsInFuture_ShoudNotBeReturned()
        {

            var gig = new Gig()
            {
                DateTime = DateTime.Today.AddDays(1),
                ArtistId = "1"
            };

            _mockSet.SetupSource<Gig>(new[] { gig });

            var result = _repository.GetUpcomingGigsByArtist("1");
            result.Should().Contain(gig);
        }
    }
}
