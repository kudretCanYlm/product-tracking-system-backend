using DAS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Infrastructure
{
    public interface IDatabaseFactory:IDisposable
    {
        DASContext Get();
    }
}
