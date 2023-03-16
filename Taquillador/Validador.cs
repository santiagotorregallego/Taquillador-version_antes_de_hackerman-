using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;



namespace Taquillador
{
    class Validador
    {
        
        public  int counterhacker = 0;
        public  string CargaRuta()
        {
            Console.Write("Ingrese la ruta del archivo de invitados: ");
            string rutaArchivo = Console.ReadLine();
            return rutaArchivo;
        }


        public string ValidarUsuario()
        {

            string nombre = " ";
            Console.WriteLine("Ingrese su contraseña: ");
            string contraseña = Console.ReadLine();
            if (contraseña == "taquillador1")
            {
                Console.Write("Usuario Comprobado: ");
                nombre = "usuario1";
                this.counterhacker = 0;

                //return true;
            }
            else if (contraseña == "soy_yo")
            {
                Console.Write("Hola Hacker: ");
                this.counterhacker++;
                nombre = "usuario2";
                //return false;
            }
            else
            {
                Console.WriteLine("Contraseña no válida");
                nombre = " ";
            }
            return nombre;

        }
        public  List<Invitado> LeerArchivo(string rutaArchivo)
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
        /*public static void  ValidarInvitado(List<Invitado> invitados, string email)
        {
            //string llave = " ";
            if (IsValidInvitado(invitados, email))
            {
                counter++;
            }
            else
            {
                Console.WriteLine("Usuario no válido");
            }
        }*/
         bool EsEmailValido(string email)
        {
            string pattern = @"^[a-zA-Z][\w.-]*@[a-zA-Z]+\.(com|co|edu\.co|org)$";
            return Regex.IsMatch(email, pattern);
        }

        public  bool IsValidInvitado(List<Invitado> invitados, string email)
        {
            //string llave = " ";
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
                //llave = "usuarioVálido";
                
                return true;
            }
        }
         void CargarInvitadosDesdeTxt(string rutaArchivo, List<Invitado> invitados)
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

         void CargarInvitadosDesdeCsv(string rutaArchivo, List<Invitado> invitados)
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
        public  void MostrarArchivo(string rutaArchivo, List<Invitado> invitados, string extension)
        {
            //string nombre = ValidarUsuario();
            if (counterhacker == 0)
            {
                VerInvitadosEnArchivo(rutaArchivo, invitados);
                //Console.WriteLine(invitados);
            }
            else if (counterhacker == 1)
            {
                HackerMan.Sobreescritura(rutaArchivo, extension,this);
                //Console.WriteLine("Cambios guardados con éxito...");
                //VerInvitadosEnArchivo(rutaArchivo, invitados);
                //Console.WriteLine(invitados);
            }
        }
        public  void VerInvitadosEnArchivo(string rutaArchivo, List<Invitado> invitados)
        {
            Console.WriteLine("Desea ver la lista de invitados? responda si/no");
            string verLista = Console.ReadLine();
            if (Regex.IsMatch(verLista, "^(si|s|sí)$", RegexOptions.IgnoreCase))
            {
                foreach (Invitado invitado in invitados)
                {
                    Console.WriteLine($"{invitado.Nombre} {invitado.Id} {invitado.Email} {invitado.Edad}");
                }
            }
            else if (Regex.IsMatch(verLista, "^(no|n)$", RegexOptions.IgnoreCase))
            {
                //Console.WriteLine("Hasta la próxima");
            }
            else
            {
                Console.WriteLine("Respuesta Inválida");
            }
        }
        public  void GuardarFormato(string rutaArchivo, List<Invitado> invitados, string extension)
        {

            extension = Path.GetExtension(rutaArchivo);
            Console.WriteLine("¿Desea guardar?");
            string resp = Console.ReadLine();
            if (Regex.IsMatch(resp, "^(si|s|sí)$", RegexOptions.IgnoreCase))
            {
                if (extension == ".txt")
                {
                    using (StreamWriter sw = new StreamWriter(rutaArchivo))
                        foreach (Invitado invitado in invitados)
                        {
                            sw.WriteLine($"{invitado.Nombre} {invitado.Id} {invitado.Email} {invitado.Edad}");
                        }
                }
                else if (extension == ".csv")
                {
                    using (StreamWriter sw = new StreamWriter(rutaArchivo))
                        foreach (Invitado invitado in invitados)
                        {
                            //sw.WriteLine($"\"{invitado.Nombre}\",\"{invitado.Id}\",\"{invitado.Email}\",{invitado.Edad}");
                            sw.WriteLine($"{invitado.Nombre},{invitado.Id},{invitado.Email},{invitado.Edad}");
                        }

                }
                Console.WriteLine("Se ha guardado la lista");
            }
            else if (Regex.IsMatch(resp, "^(no|n)$", RegexOptions.IgnoreCase))
            {
                Console.WriteLine("Chao");
            }


        }


    }

}


