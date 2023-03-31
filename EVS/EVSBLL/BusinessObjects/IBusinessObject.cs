using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSBLL.BusinessObjects
{
    public interface IBusinessObject<T>
    {
        T Create();
    }

    public interface IBusinessObject<T, U> : IBusinessObject<T>
        where T : class
        where U : IBusinessObject<T>
    {
        U GetFrom(T entity);
    }
}
