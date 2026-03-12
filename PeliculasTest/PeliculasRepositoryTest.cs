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
    }
}
