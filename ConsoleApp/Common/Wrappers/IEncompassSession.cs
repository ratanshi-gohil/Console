using EllieMae.Encompass.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Common.Wrappers
{
    public interface IEncompassSession
    {
        ISession Session { get; }
    }
}
