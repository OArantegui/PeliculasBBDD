using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacionConsola
{
    public class Menu
    {
        PeliculasRepository pr = new PeliculasRepository();
        public void MenuPrincipal()
        {
            
            int opcion;
            do
            {
                Console.WriteLine("\nElige una opción:");
                Console.WriteLine("1.- Añadir pelicula");
                Console.WriteLine("2.- Eliminar pelicula");
                Console.WriteLine("3.- Ver Catalogo");
                Console.WriteLine("4.- Actualizar pelicula");
                Console.WriteLine("0.- Salir");

                while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 0 || opcion > 4)
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo:");
                }

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("---Añadir Pelicula---");
                        string id = pr.PedirID();
                        string titulo = pr.PedirTitulo();
                        string director = pr.PedirDirector();
                        int anio = pr.PedirAnio();
                        pr.InsertarPelicula(pr.InstanciaPelicula(id, titulo, director, anio));
                        break;
                    case 2:
                        Console.WriteLine("---Eliminar Pelicula---");
                        pr.EliminarPelicula(pr.PedirID());
                        break;
                    case 3:
                        pr.ImprimirLista();
                        break;
                    case 4:
                        Console.WriteLine("---Actualizar Pelicula---");
                        string peliculaID = pr.PedirID();
                        string columna = pr.PedirColumna();
                        string nuevoDato = pr.PedirDatoString();
                        pr.ActualizarPelicula(peliculaID, columna, nuevoDato);
                        break;
                    case 0:
                        Console.WriteLine("Adios");
                        break;
                }

            } while (opcion != 0);
        }
    }
}
