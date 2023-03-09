using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taquillador
{
    class HackerMan
    {

        public static void Sobreescritura(string rutaArchivo)
        {
            List<Invitado> invitados = Validador.LeerArchivo(rutaArchivo);
            Console.WriteLine("Ingrese el ID del invitado que desea editar o escribe 'nuevo' para añadir un invitado:");
            string id = Console.ReadLine();

            if (id.ToLower() == "nuevo")
            {
                Console.WriteLine("Ingrese el nombre del nuevo invitado:");
                string nombre = Console.ReadLine();

                Console.WriteLine("Ingrese el ID del nuevo invitado:");
                string nuevoId = Console.ReadLine();

                Console.WriteLine("Ingrese el correo electrónico del nuevo invitado:");
                string email = Console.ReadLine();

                Console.WriteLine("Ingrese la edad del nuevo invitado:");
                int edad = int.Parse(Console.ReadLine());

                Invitado nuevoInvitado = new Invitado
                {
                    Nombre = nombre,
                    Id = nuevoId,
                    Email = email,
                    Edad = edad
                };
                invitados.Add(nuevoInvitado);
            }
            else
            {
                int indice = invitados.FindIndex(i => i.Id == id);
                if (indice == -1)
                {
                    Console.WriteLine("El invitado no existe en la lista.");
                }
                else
                {
                    Console.WriteLine("Ingrese el nuevo nombre del invitado:");
                    string nombre = Console.ReadLine();

                    Console.WriteLine("Ingrese el nuevo correo electrónico del invitado:");
                    string email = Console.ReadLine();

                    Console.WriteLine("Ingrese la nueva edad del invitado:");
                    int edad = int.Parse(Console.ReadLine());

                    invitados[indice].Nombre = nombre;
                    invitados[indice].Email = email;
                    invitados[indice].Edad = edad;
                }
            }

            Console.WriteLine("Guardando cambios en el archivo...");
            Validador.GuardarInvitadosEnArchivo(rutaArchivo, invitados);
        }
    }
}
