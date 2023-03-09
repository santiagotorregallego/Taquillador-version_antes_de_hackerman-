namespace Taquillador
{
    public class Evento
    {
        static void Main(string[] args)
        {
            //Validador taquillador = new Validador();
            string rutaArchivo = Validador.CargaRuta();
            string nombre = Validador.ValidarUsuario();
            List<Invitado> invitados = Validador.LeerArchivo(rutaArchivo);
            Console.Write("Ingrese el email del invitado a validar: ");
            string email = Console.ReadLine();
            Validador.ValidarInvitado(invitados, email);
            Validador.MostrarArchivo(rutaArchivo, invitados, nombre);
        }
    }
}







