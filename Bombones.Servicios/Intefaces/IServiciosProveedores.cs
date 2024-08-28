﻿using Bombones.Entidades.Entidades;

namespace Bombones.Servicios.Intefaces
{
    public interface IServiciosProveedores
    {
        void Borrar(int ProveedorId);
        bool Existe(Proveedor proveedor);
        List<Proveedor> GetLista();
        void Guardar(Proveedor proveedor);
    }
}
