using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.Exceptions
{
    //When we are inheriting from Exception, then we can come up with our own exception type
    public class OrderingDomainException :Exception
    {
        public OrderingDomainException() { }

        /*if we are constructing our own exception by passing a parameter, that parameter 
         * will get passed to the base(Exception) and then base will take care to display the message to the client*/
        public OrderingDomainException(string message) : base(message){ }

        /*In the below parameter we even want to define what kind of exception we are going to throw
        through innerException*/
        /*client may also give the actual exception in this Exception innerException parameter*/
        public OrderingDomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
