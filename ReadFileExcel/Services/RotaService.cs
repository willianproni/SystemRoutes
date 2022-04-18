using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ReadFileExcel.Util;

namespace ReadFileExcel.Services
{
    public class RotaService
    {
        private IRotasRepositorio _rotasRepositorio;

        public RotaService()
        {
            _rotasRepositorio = new RotasRepositorio();
        }

        public bool Add(Route rota)
        {
            return _rotasRepositorio.Add(rota);
        }
    }
}
