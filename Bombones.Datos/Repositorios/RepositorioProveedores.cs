using Bombones.Datos.Interfaces;
using Bombones.Entidades.Entidades;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombones.Datos.Repositorios
{
    public class RepositorioProveedores : IRepositorioProveedores
    {
        public RepositorioProveedores()
        {
        }

        public void Agregar(Proveedor proveedor, SqlConnection conn, SqlTransaction? tran = null)
        {
            string insertQuery = @"INSERT INTO Proveedores (NombreProveedor, Telefono, Email) 
                    VALUES(@NombreProveedor, @Telefono, @Email); SELECT CAST(SCOPE_IDENTITY() as int)";

            var primaryKey = conn.QuerySingle<int>(insertQuery, proveedor, tran);
            if (primaryKey > 0)
            {

                proveedor.ProveedorId = primaryKey;
                return;
            }

            throw new Exception("No se pudo agregar el proveedor");
        }

        public void Borrar(int proveedorId, SqlConnection conn, SqlTransaction? tran = null)
        {
            string deleteQuery = @"DELETE FROM Proveedores 
                    WHERE ProveedorId=@proveedorId";
            int registrosAfectados = conn
                .Execute(deleteQuery, new { proveedorId }, tran);
            if (registrosAfectados == 0)
            {
                throw new Exception("No se pudo borrar la forma de venta");
            }
        }

        public void Editar(Proveedor proveedor, SqlConnection conn, SqlTransaction? tran = null)
        {
            string updateQuery = @"UPDATE Proveedores 
                    SET NombreProveedor=@NombreProveedor, 
                        Telefono= @Telefono, 
                        Email= @Email
                    WHERE ProveedorId=@ProveedorId";

            int registrosAfectados = conn.Execute(updateQuery, proveedor, tran);
            if (registrosAfectados == 0)
            {
                throw new Exception("No se pudo editar el proveedor");
            }
        }

        public bool Existe(Proveedor proveedor, SqlConnection conn, SqlTransaction? tran = null)
        {
            try
            {
                string selectQuery = @"SELECT COUNT(*) FROM Proveedores ";
                string finalQuery = string.Empty;
                string conditional = string.Empty;
                if (proveedor.ProveedorId == 0)
                {
                    conditional = "WHERE NombreProveedor = @NombreProveedor";
                }
                else
                {
                    conditional = @"WHERE NombreProveedor = @NombreProveedor
                                AND ProveedorId<>@ProveedorId";
                }
                finalQuery = string.Concat(selectQuery, conditional);
                return conn.QuerySingle<int>(finalQuery, proveedor) > 0;

            }
            catch (Exception)
            {

                throw new Exception("No se pudo comprobar si existe el proveedor");
            }
        }

        public List<Proveedor> GetLista(SqlConnection conn, SqlTransaction? tran = null)
        {
            var selectQuery = @"SELECT ProveedorId, NombreProveedor, Telefono, Email FROM 
                        Proveedores p ORDER BY NombreProveedor";
            return conn.Query<Proveedor>(selectQuery).ToList();
        }
    }
}
