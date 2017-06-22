using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using System.Security.Claims;
using Moq;
using Musicly.Core.Repositories;
using Musicly.Controllers.APIs;
using Musicly.Core.Models;
using Musigg.Tests.Extensions;
using FluentAssertions;
using System.Web.Http.Results;

namespace Musigg.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private const string userId = "1";
        public GigsController _controller;
        private Mock<IGigsRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
             _mockRepository = new Mock<IGigsRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUnitOfWork.Object);
            _controller.MockCurrentUser(userId, "user!@domain.com");
        }


        [TestMethod]
        public void Cancel_NoGigsWithGivenId_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();

            //Assert.AreSame(result.GetType(), typeof(NotFoundResult));
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShoudReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(g => g.GetGigWithAttandees(1)).Returns(gig);

            var result= _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCanceledAnotherUserGig_ShouldReturnUnathorized()
        {
            var gig = new Gig()
            {
                ArtistId = userId + "23"
            };

            _mockRepository.Setup(g => g.GetGigWithAttandees(1)).Returns(gig);

            var result =_controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOK()
        {
            var gig = new Gig()
            {
                ArtistId = userId 
            };

            _mockRepository.Setup(g => g.GetGigWithAttandees(1)).Returns(gig);

            var result = _controller.Cancel(1);
            
            gig.IsCancel.ShouldBeEquivalentTo<bool>(true);


            foreach (var attandee in gig.Attendances)
            {
                //way 1 to test notifications added//
                Assert.IsTrue(attandee.Artist.UserNotifications.Count > 0);

                //way 2 with fluent assertion to check notifications added
                attandee.Artist.ShouldRaisePropertyChangeFor<ApplicationUser>(u => u.UserNotifications);
            }

            result.Should().BeOfType<OkResult>();
        }
    }
}
