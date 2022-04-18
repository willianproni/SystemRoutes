using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ReadFileExcel.Util
{
    public interface IRotasRepositorio
    {
        bool Add(Route rota);
        List<Route> GetAll();
    }
}
