using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DataBase
    {
        public static string ConnectionString
        {
            get
            {
                string cadenaConexion = @"Data Source=PC21WTTF\SQLEXPRESS;Initial Catalog=Cine;Integrated Security=True";
                //Tenemos que Hardcodear la conexion porque al no ser un proyecto del forms no tiene el archivo .config
                SqlConnectionStringBuilder conexionBuilder = 
                    new SqlConnectionStringBuilder(cadenaConexion);

                return cadenaConexion;
            }
        }
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection conexion =
                new SqlConnection(ConnectionString);
            conexion.Open();
            return conexion;
        }
    }
}
