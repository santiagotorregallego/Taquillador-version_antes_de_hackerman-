using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;



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


        public static string ValidarUsuario()
        {
            string nombre = " ";
            Console.WriteLine("Ingrese su contraseña: ");
            string contraseña = Console.ReadLine();
            if (contraseña == "taquillador1")
            {
                Console.Write("Usuario Comprobado: ");
                nombre = "usuario";

                //return true;
            }
            else if (contraseña == "soy_yo")
            {
                Console.Write("Hola Hacker: ");
                nombre = "hacker";
                //return false;
            }
            else
            {
                Console.WriteLine("Contraseña no válida: ");
                //return false;   
            }
            return nombre;
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
        public static void MostrarArchivo(string rutaArchivo, List<Invitado> invitados, string nombre)
        {
            //string nombre = ValidarUsuario();
            if (nombre == "usuario")
            {
                GuardarInvitadosEnArchivo(rutaArchivo, invitados);
                //Console.WriteLine(invitados);
            }
            else if (nombre == "hacker")
            {
                HackerMan.Sobreescritura(rutaArchivo);
                //Console.WriteLine(invitados);
            }
        }
        public static void GuardarInvitadosEnArchivo(string rutaArchivo, List<Invitado> invitados)
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                foreach (Invitado invitado in invitados)
                {
                    sw.WriteLine($"{invitado.Nombre} {invitado.Id} {invitado.Email} {invitado.Edad}");
                }
            }

            Console.WriteLine("Desea ver la lista de invitados? responda si/no");
            string respuesta = Console.ReadLine();
            if (Regex.IsMatch(respuesta, "^(si|s|sí)$", RegexOptions.IgnoreCase))
            {
                foreach (Invitado invitado in invitados)
                {
                    Console.WriteLine($"{invitado.Nombre} {invitado.Id} {invitado.Email} {invitado.Edad}");
                }
            }
            else if (Regex.IsMatch(respuesta, "^(no|n)$", RegexOptions.IgnoreCase))
            {
                Console.WriteLine("Hasta la próxima");
            }
            else
            {
                Console.WriteLine("Respuesta Inválida");
            }
        }

    }
}

