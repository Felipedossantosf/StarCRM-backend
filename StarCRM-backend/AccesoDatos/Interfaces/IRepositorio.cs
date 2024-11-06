using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaces
{
    public  interface IRepositorio<T> where T : class
    {
        void Add(T obj);
        void Update(T obj);
        void Update(int id, T obj);
        void Remove(T obj);
        void Remove(int? id);
        IEnumerable<T> FindAll();
        T FindById(int? id);
    }
}
