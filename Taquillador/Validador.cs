using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Taquillador
{
    class Validador
    {
        public static string CargaRuta()
        {
            Console.Write("Ingrese la ruta del archivo de invitados: ");
            string rutaArchivo = Console.ReadLine();
            return rutaArchivo;
        }
        public static List<Invitado> LeerArchivo(string rutaArchivo)
        {

            // Verificar si el archivo existe
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("El archivo no existe.");


            }
            List<Invitado> invitados = new List<Invitado>();
            string extension = Path.GetExtension(rutaArchivo);
            if (extension == ".txt")
            {
                CargarInvitadosDesdeTxt(rutaArchivo, invitados);
            }
            else if (extension == ".csv")
            {
                CargarInvitadosDesdeCsv(rutaArchivo, invitados);
            }
            else
            {
                Console.WriteLine("El formato del archivo no es válido.");

            }
            return invitados;
        }

        public static void ValidarInvitado(List<Invitado> invitados, string email)
        {
            IsValidInvitado(invitados, email);
        }
        static bool EsEmailValido(string email)
        {
            string pattern = @"^[a-zA-Z][\w.-]*@[a-zA-Z]+\.(com|co|edu\.co|org)$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidInvitado(List<Invitado> invitados, string email)
        {
            // Buscar al invitado en la lista y verificar si cumple con las condiciones
            Invitado invitado = invitados.Find(i => i.Email == email);
            if (invitado == null)
            {
                Console.WriteLine("El invitado no está en la lista.");
                return false;
            }
            else if (!EsEmailValido(invitado.Email))
            {
                Console.WriteLine("El email no es válido.");
                return false;
            }
            else if (invitado.Edad < 18)
            {
                Console.WriteLine("El invitado es menor de edad.");
                return false;
            }
            else
            {
                Console.WriteLine("El invitado puede pasar al evento.");
                return true;
            }
        }
        static void CargarInvitadosDesdeTxt(string rutaArchivo, List<Invitado> invitados)
        {
            string[] lineas = File.ReadAllLines(rutaArchivo);
            foreach (string linea in lineas)
            {
                string[] campos = linea.Split(' ');
                Invitado invitado = new Invitado
                {
                    Nombre = campos[0],
                    Id = campos[1],
                    Email = campos[2],
                    Edad = int.Parse(campos[3])
                };
                invitados.Add(invitado);
            }
        }

        static void CargarInvitadosDesdeCsv(string rutaArchivo, List<Invitado> invitados)
        {
            string[] lineas = File.ReadAllLines(rutaArchivo);
            foreach (string linea in lineas)
            {
                string[] campos = linea.Split(',');
                Invitado invitado = new Invitado
                {
                    Nombre = campos[0],
                    Id = campos[1],
                    Email = campos[2],
                    Edad = int.Parse(campos[3])
                };
                invitados.Add(invitado);
            }
        }
    }
}
