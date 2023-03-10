using System.Text.RegularExpressions;

namespace Taquillador
{
    public class Evento
    {
        static void Main(string[] args)
        {
            EjecucionPrograma();
        }
        public static void EjecucionPrograma()
        {
            string usuario = Validador.ValidarUsuario();
            if ((usuario != " "))
            {
                string rutaArchivo = Validador.CargaRuta();
                do
                {
                    //string usuario = Validador.ValidarUsuario();
                    if ((usuario != " "))
                    {
                        string extension = Path.GetExtension(rutaArchivo);

                        List<Invitado> invitados = Validador.LeerArchivo(rutaArchivo);
                        Console.Write("Ingrese el email del invitado a validar: ");
                        string email = Console.ReadLine();
                        Validador.IsValidInvitado(invitados, email);

                        Validador.MostrarArchivo(rutaArchivo, invitados, extension);
                        Console.WriteLine("Proceso terminado");
                    }
                    else
                    {
                        Console.WriteLine("No puede ingresar al programa");
                        break;
                    }
                    Console.WriteLine("¿Desea validar otro invitado?: si/no ");
                } while (Console.ReadLine().ToLower() == "si");
            }
            else
            {
                Console.WriteLine("No puede ingresar al programa");

            }


        }
    }
}







