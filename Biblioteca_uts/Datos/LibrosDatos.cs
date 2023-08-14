using System.Data.SqlClient;
using System.Data;

using Biblioteca_uts.Models;

namespace Biblioteca_uts.Datos
{
    public class LibrosDatos
    {
        public List<LibrosModel> Listar()
        {
            List<LibrosModel> Lista = new List<LibrosModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_Libro", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Lista.Add(new LibrosModel()
                        {
                            No_Adquisicion = Convert.ToInt32(dr["No_Adquisicion"]),
                            Titulo = dr["Titulo"].ToString(),
                            Fecha_adquisicion = dr.GetDateTime(dr.GetOrdinal("Fecha_adquisicion")),
                            Ibsn = dr["IBSN"].ToString(),
                            Clasificacion = dr["Clasificacion"].ToString(),
                            No_Estante = Convert.ToInt32(dr["No_Estante"]),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            Estatus = dr["Estatus"].ToString(),
                            Procedencia = dr["Procedencia"].ToString(),
                            No_factura = dr["No_factura"].ToString()
                        }
                        );
                    }
                }
            }
            return Lista;
        }
        public LibrosModel ObtenerLibro(int IdContacto)
        {
            LibrosModel _Contacto = new LibrosModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Sp_Buscar_Libro", conexion);
                cmd.Parameters.AddWithValue("No_Adquisicion", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _Contacto.No_Adquisicion = Convert.ToInt32(dr["No_Adquisicion"]);
                        _Contacto.Titulo = dr["Titulo"].ToString();
                        _Contacto.Fecha_adquisicion = dr.GetDateTime(dr.GetOrdinal("Fecha_adquisicion"));
                        _Contacto.Ibsn = dr["IBSN"].ToString();
                        _Contacto.Clasificacion = dr["Clasificacion"].ToString();
                        _Contacto.No_Estante = Convert.ToInt32(dr["No_Estante"]);
                        _Contacto.Cantidad = Convert.ToInt32(dr["Cantidad"]);
                        _Contacto.Estatus = dr["Estatus"].ToString();
                        _Contacto.Procedencia = dr["Procedencia"].ToString();
                        _Contacto.No_factura = dr["No_factura"].ToString();
                    }
                }
            }
            return _Contacto;
        }
        //############################################################################
        public bool GuardarLibro(LibrosModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Registrar_Libro", conexion);

                    cmd.Parameters.AddWithValue("No_Adquisicion", model.No_Adquisicion);
                    cmd.Parameters.AddWithValue("Titulo", model.Titulo);
                    cmd.Parameters.AddWithValue("Fecha_adquisicion", model.Fecha_adquisicion);
                    cmd.Parameters.AddWithValue("IBSN", model.Ibsn);
                    cmd.Parameters.AddWithValue("Clasificacion", model.Clasificacion);
                    cmd.Parameters.AddWithValue("No_Estante", model.No_Estante);
                    cmd.Parameters.AddWithValue("Cantidad", model.Cantidad);
                    cmd.Parameters.AddWithValue("Estatus", "Disponible");
                    cmd.Parameters.AddWithValue("Procedencia", model.Procedencia);
                    cmd.Parameters.AddWithValue("No_factura", model.No_factura);
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

        public bool EditarLibro(LibrosModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Sp_Modificar_Libro", conexion);
                    cmd.Parameters.AddWithValue("No_Adquisicion", model.No_Adquisicion);
                    cmd.Parameters.AddWithValue("Titulo", model.Titulo);
                    cmd.Parameters.AddWithValue("Fecha_adquisicion", model.Fecha_adquisicion);
                    cmd.Parameters.AddWithValue("IBSN", model.Ibsn);
                    cmd.Parameters.AddWithValue("Clasificacion", model.Clasificacion);
                    cmd.Parameters.AddWithValue("No_Estante", model.No_Estante);
                    cmd.Parameters.AddWithValue("Cantidad", model.Cantidad);
                    cmd.Parameters.AddWithValue("Estatus", model.Estatus);
                    cmd.Parameters.AddWithValue("Procedencia", model.Procedencia);
                    cmd.Parameters.AddWithValue("No_factura", model.No_factura);
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
        public bool EliminarLibro(int No_Adquisicion)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Eliminar_Libro", conexion);
                    cmd.Parameters.AddWithValue("No_Adquisicion", No_Adquisicion);
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