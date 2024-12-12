﻿using AccesoDatos.Interfaces;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class RepositorioComercial : IRepositorioComercial
    {
        private StarCRMContext _db;

        public RepositorioComercial(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(Comercial comercial)
        {
            if (comercial == null) throw new ComercialException("No se receibió el comercial.");
            if (_db.Comerciales.Where(com => com.nombre == comercial.nombre).Any()) throw new ComercialException("Ya hay un comercial registrado con ese nombre.");
            if (_db.Comerciales.Where(com => com.correo == comercial.correo).Any()) throw new ComercialException("Ya hay un comercial registrado con ese correo.");

            try
            {
                comercial.validar();
                _db.Comerciales.Add(comercial);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    // Muestra el mensaje de la excepción interna
                    throw new Exception($"Inner Exception: {ex.InnerException.Message}");
                }
                else
                {
                    throw new Exception("Error al agregar el comerciall: " + ex.Message, ex);
                }
            }
        }

        public IEnumerable<Comercial> FindAll()
        {
            return _db.Comerciales.ToList();
        }

        public IEnumerable<Cliente> FindAllClientes()
        {
            return _db.Comerciales.OfType<Cliente>().ToList(); // Retornar todos los comerciales de tipo Cliente
        }

        public IEnumerable<Proveedor> FindAllProveedores()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comercial> FindByCondition(Expression<Func<Comercial, bool>> expression)
        {
            return _db.Comerciales.Where(expression).ToList();
        }

        public Comercial FindById(int? id)
        {
            try
            {
                if (id == null) throw new ArgumentNullException("Id comercial no puede ser nulo");
                return _db.Comerciales.SingleOrDefault(com => com.id == id);
            }
            catch (ComercialException comExc)
            {
                throw new ComercialException($"Error: {comExc.Message}");
            }
            catch (Exception ex)
            {
                throw new ComercialException(ex.Message);
            }
        }

        

        public void Remove(Comercial obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            try
            {
                var comercial = FindById(id);
                if (comercial != null)
                {
                    _db.Comerciales.Remove(comercial);
                    _db.SaveChanges();
                }
            }
            catch(ArgumentNullException ex)
            {
                throw new ComercialException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ComercialException(ex.Message);
            }
            
        }

        public void Update(Comercial obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Comercial comercial)
        {
            throw new NotImplementedException();
        }

        public void UpdateCliente(int id, Cliente cliente)
        {
            if (cliente == null)
                throw new ClienteException("Cliente recibido por parámetro nulo");

            var clienteExistente = _db.Comerciales.OfType<Cliente>().FirstOrDefault(c => c.id == id);

            if (clienteExistente == null)
                throw new ClienteException("No se encontró cliente con el id especificado.");

            try
            {
                clienteExistente.nombre = cliente.nombre;
                clienteExistente.telefono = cliente.telefono;
                clienteExistente.correo = cliente.correo;
                clienteExistente.credito = cliente.credito;
                clienteExistente.razonSocial = cliente.razonSocial; 
                clienteExistente.rut = cliente.rut;
                clienteExistente.direccion = cliente.direccion;
                clienteExistente.sitioWeb = cliente.sitioWeb;
                clienteExistente.TipoComercial = cliente.TipoComercial;
                clienteExistente.zafras = cliente.zafras;
                clienteExistente.notas = cliente.notas;
                clienteExistente.esInactivo = cliente.esInactivo;
                clienteExistente.estado = cliente.estado;

                _db.SaveChanges();
            }catch(ClienteException ex)
            {
                throw new ClienteException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProveedor(int id, Proveedor proveedor)
        {
            throw new NotImplementedException();
        }
    }
}
