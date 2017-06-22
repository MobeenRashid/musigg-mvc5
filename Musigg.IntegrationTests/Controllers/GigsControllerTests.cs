using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musicly.Controllers;
using Musicly.Persistence;
using Musicly.Persistence.Repositories;
using System.Linq;
using Musicly.Core.Models;
using System.Collections.Generic;
using FluentAssertions; 

namespace Musigg.IntegrationTests.Controller
{
    [TestClass]
    public class GigsControllerTest
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            GlobalSetUp.SetUp();
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TestCleanup]
        public void Cleanup()
        {
            //_context.Dispose();
        }

        [TestMethod]
        [Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            //arrange
            var user = _context.Users.First();
            var genre = new Genre()
            {
                Name = "__"
            };

            var gig = new Gig()
            {
                Artist = user,
                Genre = genre,
                Venue = "_",
                DateTime=DateTime.Today
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //act
            var result = _controller.Mine();

            //assert
            (result.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }
    }
}
