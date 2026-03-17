using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace AccesoDatos
{
    public class PeliculasRepository
    {
        //Método para listar todas las películas
        public List<Pelicula> ObtenerTodas()
        {
            using (var conexion =
                DataBase.GetSqlConnection())
            {
                String sql = "";
                sql = sql + "SELECT [PeliculaID] " + "\n";
                sql = sql + "      ,[Titulo] " + "\n";
                sql = sql + "      ,[Director] " + "\n";
                sql = sql + "      ,[Anio] " + "\n";
                sql = sql + "  FROM [dbo].[Peliculas]";

                using (SqlCommand comando =
                    new SqlCommand(sql, conexion))
                {
                    SqlDataReader reader = 
                        comando.ExecuteReader();
                    List<Pelicula> peliculas = 
                        new List<Pelicula>();
                    while (reader.Read())
                    {
                        var pelicula = LeerDelDataReader(reader);
                        peliculas.Add(pelicula);
                    }
                    return peliculas;
                }
            }
        }
        public void ImprimirPelicula(Pelicula p)
        {
            Console.WriteLine($"ID: {p.PeliculaID} | Titulo: {p.Titulo} | Director: {p.Director} | Año publicación: {p.Anio}");
            Console.WriteLine();
        }
        public void ImprimirLista()
        {
            var listaPeliculas = ObtenerTodas();
            foreach(Pelicula p in listaPeliculas)
            {
                ImprimirPelicula(p);
            }
        }

        public Pelicula ObtenerPorId(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String sql = "";
                sql = sql + "SELECT [PeliculaID] " + "\n";
                sql = sql + "      ,[Titulo] " + "\n";
                sql = sql + "      ,[Director] " + "\n";
                sql = sql + "      ,[Anio] " + "\n";
                sql = sql + "  FROM [dbo].[Peliculas]";
                sql = sql + $"WHERE PeliculaID = @peliculaId";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("peliculaId", id);
                    var reader = comando.ExecuteReader();
                    Pelicula pelicula = null;
                    if (reader.Read())
                    {
                        pelicula = LeerDelDataReader(reader);
                    }
                    return pelicula;
                }
            }
        }
        public Pelicula LeerDelDataReader(SqlDataReader reader)
        {
            Pelicula pelicula = new Pelicula();
            pelicula.PeliculaID = (string)reader["PeliculaID"];
            pelicula.Titulo = (string)reader["Titulo"];
            pelicula.Director = (string)reader["Director"];
            pelicula.Anio = (int)reader["Anio"];

            return pelicula;
        }

        public void InsertarPelicula(Pelicula pelicula)
        {
            
            using (var conexion = DataBase.GetSqlConnection())
            {
                String sql = "";
                sql = sql + "INSERT INTO [dbo].[Peliculas] " + "\n";
                sql = sql + "           ([PeliculaID] " + "\n";
                sql = sql + "           ,[Titulo] " + "\n";
                sql = sql + "           ,[Director] " + "\n";
                sql = sql + "           ,[Anio]) " + "\n";
                sql = sql + "     VALUES " + "\n";
                sql = sql + "           (@peliculaId " + "\n";
                sql = sql + "           ,@titulo " + "\n";
                sql = sql + "           ,@director " + "\n";
                sql = sql + "           ,@anio)";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("peliculaId", pelicula.PeliculaID);
                    comando.Parameters.AddWithValue("titulo", pelicula.Titulo);
                    comando.Parameters.AddWithValue("director", pelicula.Director);
                    comando.Parameters.AddWithValue("anio", pelicula.Anio);

                    var insertados = comando.ExecuteNonQuery();

                    if(insertados > 0)
                    {
                        Console.WriteLine("Pelicula insertada con exito");
                        ImprimirPelicula(pelicula);
                    }
                }
            }
        }
        public Pelicula InstanciaPelicula(string id, string titulo, string director, int anio) //Igual al constructor de pelicula para tenerlo en la misma lase
        {
            Pelicula pelicula = new Pelicula(id, titulo, director, anio);
            return pelicula;
        }
        public virtual string PedirID()
        {
            string id;
            do
            {
                Console.WriteLine("ID: ");
                id = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(id))
                    Console.WriteLine("El ID no puede estar vacío.");
            }
            while (string.IsNullOrWhiteSpace(id));
            return id;
        }
        public virtual string PedirTitulo()
        {
            string titulo;
            do
            {
                Console.WriteLine("Titulo: ");
                titulo = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(titulo))
                    Console.WriteLine("El ID no puede estar vacío.");
            }
            while (string.IsNullOrWhiteSpace(titulo));
            return titulo;
        }
        public virtual string PedirDirector()
        {
            string director;
            do
            {
                Console.WriteLine("Director: ");
                director = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(director))
                    Console.WriteLine("El ID no puede estar vacío.");
            }
            while (string.IsNullOrWhiteSpace(director));
            return director;
        }
        public void EliminarPelicula(string id)
        {
            int eliminados = 0;
            using (var conexion = DataBase.GetSqlConnection())
            {
                String sql = "";
                sql = sql + "DELETE FROM [dbo].[Peliculas] " + "\n";
                sql = sql + "      WHERE PeliculaID = @peliculaId";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@peliculaId", id);
                    eliminados = comando.ExecuteNonQuery();

                    if (eliminados > 0)
                    {
                        Console.WriteLine("Pelicula eliminada con exito");
                    }
                    else Console.WriteLine("No existe pelicula con este ID");
                }
            }
        }

        
        public virtual int PedirAnio()
        {
            int anio;
            Console.WriteLine("Año: ");
            while (!int.TryParse(Console.ReadLine(), out anio) || anio < 0 || anio > 2050)
            {
                Console.WriteLine("Año no válido. Introduce un año entre 0 y 2050:");
            }
            return anio;
        }

        public void ActualizarPelicula (string id, string columna, string nuevoDato)
        {
            
            // Validar columna destino
            
            SqlDbType tipoParam;
            object valor;
            switch (columna)
            {
                case "Titulo":
                    tipoParam = SqlDbType.NVarChar;
                    valor = nuevoDato; // valida longitud si procede
                    break;
                case "Director":
                    tipoParam = SqlDbType.NVarChar;
                    valor = nuevoDato;
                    break;
                case "Anio":
                    tipoParam = SqlDbType.Int;
                    int anio;
                    // Bucle hasta que el usuario meta un número de verdad
                    while (!int.TryParse(nuevoDato, out anio))
                    {
                        Console.WriteLine("El campo Año debe ser numérico.");
                        nuevoDato = PedirDatoString(); // Ahora sí actualizamos la variable
                    }
                    valor = anio;
                    break;
                default:
                    throw new ArgumentException("Campo no válido. Usa: Titulo, Director o Año.");
            } //Esto lo voy a mover a su propio metodo
            using (var conexion = DataBase.GetSqlConnection())
            {
                String sql = "";
                sql = sql + "UPDATE [dbo].[Peliculas] " + "\n";
                sql = sql + $"   SET {columna} = @nuevoDato " + "\n";
                sql = sql + "WHERE PeliculaID = @id";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    comando.Parameters.Add("@nuevoDato", tipoParam).Value = valor;

                    var actualizadas = comando.ExecuteNonQuery();

                    if (actualizadas > 0)
                    {
                        Console.WriteLine("Pelicula actualizada con exito");
                        ImprimirPelicula(ObtenerPorId(id));
                    }
                    else
                    {
                        Console.WriteLine("No se encontró la película con ese ID.");
                    }
                }
            }
        }

        public virtual string PedirColumna()
        {
            // TODO: Cambiar opcion a elegir con numero
            string columna = "";
            bool columnaValida = false;

            do
            {
                Console.WriteLine("Dato a cambiar (Titulo, Director, Año): ");
                columna = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(columna))
                {
                    Console.WriteLine("El dato no puede estar vacío.");
                    continue;
                }

                switch (columna)
                {
                    case "Titulo":
                    case "Director":
                        columnaValida = true;
                        break;
                    case "Año":
                    case "Anio":
                        columna = "Anio"; // Lo estandarizamos para la BD
                        columnaValida = true;
                        break;
                    default:
                        Console.WriteLine("Campo no válido. Usa: Titulo, Director o Año.");
                        break;
                }
            }
            while (!columnaValida);

            return columna;
        }

        public virtual string PedirDatoString()
        {
            string dato;
            do
            {
                Console.WriteLine("Dato: ");
                dato = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(dato))
                    Console.WriteLine("El Dato no puede estar vacío.");
            }
            while (string.IsNullOrWhiteSpace(dato));
            return dato;
        }
        //TODO: Añadir funcionalidad elegir ids duplicadas sacar lista y dar a elegir
    }
}
