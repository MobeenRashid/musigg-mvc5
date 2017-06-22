using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace Musigg.IntegrationTests
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public class Isolated : Attribute, IActionFilter
    {
        private TransactionScope _transaction;
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _transaction = new TransactionScope();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _transaction.Dispose();
        }
    }
}
