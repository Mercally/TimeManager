using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Common.Transaction;

namespace TimeManager.BL
{
    public class Transaction : IDisposable
    {
        public Transaction()
        {

        }

        public void Commit()
        {

        }

        public void Rollback()
        {

        }

        public T Execute<T>(Query query) {



            return (T)Convert.ChangeType("", typeof(T));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
