using AccesoDatos.Interfaces;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class RepositorioCliente : IRepositorio<Cliente>
    {
        private StarCRMContext _db;
        public RepositorioCliente(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(Cliente cli)
        {
            if (cli == null) throw new ClienteException("No se recibió al cliente.");

            try
            {
                cli.validar();
                _db.Clientes.Add(cli);
                _db.SaveChanges();
            }
            catch (ClienteException ce)
            {
                throw new ClienteException(ce.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Cliente> FindAll()
        {
            throw new NotImplementedException();
        }

        public Cliente FindById(int? id)
        {
            try
            {
                if (id == null) throw new ArgumentNullException("Id cliente no puede ser nulo");
                return _db.Clientes.SingleOrDefault(cli => cli.id == id);
            }
            catch (ClienteException cliExc)
            {
                throw new ClienteException($"Error: {cliExc.Message}");
            }
            catch(Exception ex)
            {
                throw new ClienteException(ex.Message);
            }
        }

        public void Remove(Cliente obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(Cliente obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Cliente obj)
        {
            throw new NotImplementedException();
        }
    }
}
