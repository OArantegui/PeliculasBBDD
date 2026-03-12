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
                Console.WriteLine("0.- Salir");

                while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 0 || opcion > 3)
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo:");
                }

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("---Añadir Pelicula---");
                        pr.InsertarPelicula();
                        break;
                    case 2:
                        Console.WriteLine("---Eliminar Pelicula---");
                        pr.EliminarPelicula(pr.PedirString("ID"));
                        break;
                    case 3:
                        pr.ImprimirLista();
                        break;
                    case 0:
                        Console.WriteLine("Adios");
                        break;
                }

            } while (opcion != 0);

        }
    }
}
