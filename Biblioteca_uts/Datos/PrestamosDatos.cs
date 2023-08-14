using Biblioteca_uts.Models;
using System.Data;
using System.Data.SqlClient;

namespace Biblioteca_uts.Datos
{
    public class PrestamosDatos
    {
     /*   public List<PrestamosModels> Listar()
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
                            IdPrestamo = Convert.ToInt32(dr["Id_Prestamo"]),
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
        */
        public  List<CMT_PrestamoModels> Listar2()
        {
            List<CMT_PrestamoModels> Lista = new List<CMT_PrestamoModels>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Sp_Prestamo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Lista.Add(new CMT_PrestamoModels()
                        {
                            Pre_Id_Prestamo= Convert.ToInt32(dr["Id_Prestamo"]),
                            Us_Identificador = Convert.ToInt32(dr["Identificador"]),
                            Us_Nombres = dr["Nombres"].ToString(),
                            Us_ApePa = dr["ApePa"].ToString(),
                            Pre_Fecha_prestamo = dr.GetDateTime(dr.GetOrdinal("Fecha_prestamo")),
                            Pre_Fecha_devolucion = dr.GetDateTime(dr.GetOrdinal("Fecha_devolucion")),
                            Lib_Titulo = dr["Titulo"].ToString(),
                            Lib_Clasificacion = dr["Clasificacion"].ToString(),
                            Lib_IBSN = dr["IBSN"].ToString(),
                        });
                    }
                }
            }
            return Lista;
        }



        public PrestamosModels ObtenerPrestamo(int IdPrestamo)
        {
            PrestamosModels _Prestamos = new PrestamosModels();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Sp_Buscar_Prestamos", conexion);
                cmd.Parameters.AddWithValue("Id_Prestamo", IdPrestamo);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _Prestamos.IdPrestamo = Convert.ToInt32(dr["Id_Prestamo"]);
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
        public bool GuardarPrestamo(PrestamosModels model)
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
                    cmd.Parameters.AddWithValue("Fecha_devolucion", model.Fecha_devolucion);
                    cmd.Parameters.AddWithValue("No_Adquisicion", model.No_Adquisicion);
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

        public bool EditarPrestamo(PrestamosModels model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Sp_Modificar_Prestamos", conexion);
                    cmd.Parameters.AddWithValue("Id_Prestamo", model.IdPrestamo);
                    cmd.Parameters.AddWithValue("Identificador", model.Identificador);
                    cmd.Parameters.AddWithValue("Fecha_prestamo", model.Fecha_prestamo);
                    cmd.Parameters.AddWithValue("Fecha_devolucion", model.Fecha_prestamo);
                    cmd.Parameters.AddWithValue("No_Adquisicion", model.No_Adquisicion);
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
        public bool EliminarPrestamo(int IdPrestamo)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Eliminar_Prestamos", conexion);
                    cmd.Parameters.AddWithValue("Id_Prestamo", IdPrestamo);
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