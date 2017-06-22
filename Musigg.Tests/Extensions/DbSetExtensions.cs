using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musigg.Tests.Extensions
{
    public static class DbSetExtensions
    {
        public static void SetupSource<T>(this Mock<DbSet<T>> mockSet,IList<T> source) where T:class
        {
            var qSource = source.AsQueryable();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(qSource.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(qSource.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(qSource.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(qSource.GetEnumerator());
            
        }
    }
}
