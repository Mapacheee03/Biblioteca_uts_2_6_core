﻿using Biblioteca_uts.Models;
using System.Data;
using System.Data.SqlClient;

namespace Biblioteca_uts.Datos
{
    public class PrestamosDatos
    {
        public List<PrestamosModels> Listar()
        {
            List<PrestamosModels> Lista = new List<PrestamosModels>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_Prestamos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Lista.Add(new PrestamosModels()
                        {
                            Identificador = Convert.ToInt32(dr["Identificador"]),
                            Fecha_prestamo = dr.GetDateTime(dr.GetOrdinal("Fecha_prestamo")),
                            Fecha_devolucion = dr.GetDateTime(dr.GetOrdinal("Fecha_devolucion")),
                            No_Adquisicion = Convert.ToInt32(dr["No_Adquisicion"])
                        }
                        );
                    }
                }
            }
            return Lista;
        }
        public PrestamosModels ObtenerLibro(int Identificador)
        {
            PrestamosModels _Prestamos = new PrestamosModels();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Sp_Buscar_Prestamos", conexion);
                cmd.Parameters.AddWithValue("No_Adquisicion", Identificador);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _Prestamos.Identificador = Convert.ToInt32(dr["Identificador"]);
                        _Prestamos.Fecha_prestamo = dr.GetDateTime(dr.GetOrdinal("Fecha_prestamo"));
                        _Prestamos.Fecha_devolucion = dr.GetDateTime(dr.GetOrdinal("Fecha_devolucion"));
                        _Prestamos.No_Adquisicion = Convert.ToInt32(dr["No_Adquisicion"]);
                    }
                }
            }
            return _Prestamos;
        }
        //############################################################################
        public bool GuardarLibro(PrestamosModels model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Registrar_Prestamos", conexion);

                    cmd.Parameters.AddWithValue("Identificador", model.Identificador);
                    cmd.Parameters.AddWithValue("Fecha_prestamo", model.Fecha_prestamo);
                    cmd.Parameters.AddWithValue("Fecha_devolucion", model.Fecha_prestamo);
                    cmd.Parameters.AddWithValue("No_Adquisicion", model.Fecha_prestamo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }

        public bool EditarLibro(PrestamosModels model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Sp_Modificar_Prestamos", conexion);
                    cmd.Parameters.AddWithValue("Identificador", model.Identificador);
                    cmd.Parameters.AddWithValue("Fecha_prestamo", model.Fecha_prestamo);
                    cmd.Parameters.AddWithValue("Fecha_devolucion", model.Fecha_prestamo);
                    cmd.Parameters.AddWithValue("No_Adquisicion", model.Fecha_prestamo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }
        /*####################################################*/
        public bool EliminarLibro(int Identificador)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Eliminar_Prestamos", conexion);
                    cmd.Parameters.AddWithValue("Identificador", Identificador);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}