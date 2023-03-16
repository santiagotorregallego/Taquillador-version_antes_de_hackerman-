using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Taquillador
{
    public class Evento
    {
        //private static int counterhacker = 0;
        static void Main(string[] args)
        {
            EjecucionPrograma();
        }
        public static void EjecucionPrograma()
        {
            
            Validador validador=new Validador();
            string usuario = validador.ValidarUsuario();
            //string usuario = "usuario1";
            if ((usuario != " "))
            {
                string rutaArchivo = validador.CargaRuta();
                
                do
                {
                    //string usuario = Validador.ValidarUsuario();
                    string extension = Path.GetExtension(rutaArchivo);

                    List<Invitado> invitados = validador.LeerArchivo(rutaArchivo);
                    Console.Write("Ingrese el email del invitado a validar: ");
                    string email = Console.ReadLine();
                    validador.IsValidInvitado(invitados, email);

                    validador.MostrarArchivo(rutaArchivo, invitados, extension);

                    if ((usuario == "usuario2"))
                    {
                        Console.WriteLine("Desea ingresar como taquillador?");
                        string resp = Console.ReadLine();
                        if (Regex.IsMatch(resp, "^(si|s|sí)$", RegexOptions.IgnoreCase))
                        {

                            usuario = validador.ValidarUsuario();
                            //usuario = "usuario1";
                            //Validador.counterhacker = 0;

                            //counterhacker = 0;
                            //List<Invitado> invitados = Validador.LeerArchivo(rutaArchivo);
                        }
                    }
                    else
                    {
                        //Console.WriteLine("Proceso terminado");
                    }
                    Console.WriteLine("¿Desea validar otro invitado?: si/no ");
                } while (Console.ReadLine().ToLower() == "si");
                Console.WriteLine("Proceso terminado");
            }
            else
            {
                Console.WriteLine("No puede ingresar al programa");

            }


        }
    }
}







