//Oscar Arantegui
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AccesoDatos
{
    [TestFixture]
    public class PeliculasRepositoryTest
    {   
        Mock<PeliculasRepository> mockRepo;
        List<Pelicula> peliculaList = new List<Pelicula>();
        [SetUp]
        public void Setup() 
        {
            mockRepo = new Mock<PeliculasRepository>();
            peliculaList = mockRepo.Object.ObtenerTodas();
        }

        [Test]
        public void Test_ListaPeliculas()
        {

            var peliculasEsperadas = new List<Pelicula>
            {
                new Pelicula { PeliculaID = "P001", Titulo = "Avatar", Director = "James Cameron", Anio = 2009 },
                new Pelicula { PeliculaID = "P002", Titulo = "Avengers: Endgame", Director = "Anthony Russo y Joe Russo", Anio = 2019 },
                new Pelicula { PeliculaID = "P003", Titulo = "Avatar: The Way of Water", Director = "James Cameron", Anio = 2022 },
                new Pelicula { PeliculaID = "P004", Titulo = "Titanic", Director = "James Cameron", Anio = 1997 },
                new Pelicula { PeliculaID = "P005", Titulo = "Star Wars: The Force Awakens", Director = "J. J. Abrams", Anio = 2015 },
                new Pelicula { PeliculaID = "P006", Titulo = "Avengers: Infinity War", Director = "Anthony Russo y Joe Russo", Anio = 2018 },
                new Pelicula { PeliculaID = "P007", Titulo = "Spider-Man: No Way Home", Director = "Jon Watts", Anio = 2021 },
                new Pelicula { PeliculaID = "P009", Titulo = "The Lion King", Director = "Jon Favreau", Anio = 2019 },
                new Pelicula { PeliculaID = "P010", Titulo = "The Avengers", Director = "Joss Whedon", Anio = 2012 }
            };
            //Assert longitud
            Xunit.Assert.Equal(peliculaList.Count(), peliculasEsperadas.Count());
            //Assert contenido
            Xunit.Assert.Equal(peliculasEsperadas.First().PeliculaID, peliculaList.First().PeliculaID);
        }

        //Testing metodo imprimir por pantalla una pelicula
        [Test]
        public void Test_ImprimirPelicula() 
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            Pelicula p = peliculaList.First();
            string esperado = $"ID: {p.PeliculaID} | Titulo: {p.Titulo} | Director: {p.Director} | Año publicación: {p.Anio}\r\n\r\n";
            mockRepo.Object.ImprimirPelicula(p);

            var resultado = sw.ToString();
            Xunit.Assert.Equal(esperado, resultado);
            //Limpiar para evitar conflictos con otros tests
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }

        //Test metodo obtener pelicula por id
        [Test]
        public void Test_ObtenerPeliculaPorId()
        {
            //Crear pelicula esperada
            Pelicula esperada = new Pelicula { PeliculaID = "P001", Titulo = "Avatar", Director = "James Cameron", Anio = 2009 };
            Pelicula resultado = mockRepo.Object.ObtenerPorId("P001");
            Xunit.Assert.Equal(esperada.Titulo, resultado.Titulo);
        }

        //Test eliminar pelicula
        [Test]
        public void Test_EliminarPelicula()
        {
            //Definimos ID a eliminar
            string idEliminar = "P010";
            //Guardamos pelicula para recuperarla despues
            Pelicula guardada = mockRepo.Object.ObtenerPorId(idEliminar);
            //Eliminamos
            mockRepo.Object.EliminarPelicula(idEliminar);
            //Confirmamos
            Pelicula eliminada = mockRepo.Object.ObtenerPorId(idEliminar);
            Xunit.Assert.Null(eliminada);
            //Volvemos a crear para que los tests no afecten a la BBDD real
            mockRepo.Object.InsertarPelicula(guardada);
        }

        //Tests pedir datos
        [Test]
        public void Test_PedirId()
        {
            string esperado = "ID";
            mockRepo.Setup(x => x.PedirID()).Returns("ID");
            string valor =mockRepo.Object.PedirID();
            Xunit.Assert.Equal(esperado, valor);
        }
        [Test]
        public void Test_PedirTitulo()
        {
            string esperado = "Prueba";
            mockRepo.Setup(x => x.PedirTitulo()).Returns("Prueba");
            string valor = mockRepo.Object.PedirTitulo();
            Xunit.Assert.Equal(esperado, valor);
        }
        [Test]
        public void Test_PedirDirector()
        {
            string esperado = "Prueba";
            mockRepo.Setup(x => x.PedirDirector()).Returns("Prueba");
            string valor = mockRepo.Object.PedirDirector();
            Xunit.Assert.Equal(esperado, valor);
        }
        [Test]
        public void Test_PedirAnio()
        {
            int esperado = 1;
            mockRepo.Setup(x => x.PedirAnio()).Returns(1);
            int valor = mockRepo.Object.PedirAnio();
            Xunit.Assert.Equal(esperado, valor);
        }

        [Test]
        public void Test_ActualizarPelicula()
        {
            String tituloAntiguo = mockRepo.Object.ObtenerPorId("P002").Titulo;
            mockRepo.Object.ActualizarPelicula("P002", "Titulo", "Avengers: Infinity War");
            Xunit.Assert.NotEqual(mockRepo.Object.ObtenerPorId("P002").Titulo, tituloAntiguo);
            //Revertimos los cambios para no afectar la bbdd real
            mockRepo.Object.ActualizarPelicula("P002", "Titulo", "Avengers: Endgame");

        }
        [Test]
        public void Test_PedirColumna()
        {   
            string esperado = "Titulo";
            mockRepo.Setup(x => x.PedirColumna()).Returns("Titulo");
            string valor = mockRepo.Object.PedirColumna();
            Xunit.Assert.Equal(esperado, valor);
        }
        [Test]
        public void Test_PedirDatoString()
        {
            string esperado = "Dato";
            mockRepo.Setup(x => x.PedirDatoString()).Returns("Dato");
            string valor = mockRepo.Object.PedirDatoString();
            Xunit.Assert.Equal(esperado, valor);
        }
    }
}
