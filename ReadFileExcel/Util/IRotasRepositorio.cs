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
        bool Add(Rota rota);
        List<Rota> GetAll();
    }
}
