using System.Data.SqlClient;
using System.Runtime;

namespace ProyectoFinal
{

    class Program
    {

        static void Main(string[] args)
        {

            // Variable
            byte opcionMenuPrincipal;

            // Clase creada para tener las funciones del menu contacto
            Opciones_contacto op_contactos = new Opciones_contacto();

            // Clase creada para tener las funciones del menu eventos
            Opciones_eventos op_eventos = new Opciones_eventos();

            // Clase creada para tener las conversiones de los grados
            Conversor_unidades c_unidades = new Conversor_unidades();

            // Clase creada para tener las opciones de una calculadora


            // Conexion de SQL SERVER
            SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true");



            do
            {
                // Menu principal del programa
                Console.Clear();
                Console.WriteLine("PROYECTO FINAL");
                Console.WriteLine("[1] Agenda Electronica");
                Console.WriteLine("[2] Modulo Utilitario");
                Console.WriteLine("[3] Salir");
                Console.Write("Ingrese su opcion: ");
                opcionMenuPrincipal = byte.Parse(Console.ReadLine()!);

                switch (opcionMenuPrincipal)
                {

                    // Agenda Electronica
                    case 1:

                        // Variables
                        byte opcion;

                        do
                        {
                            // Menu Agenda Electronica
                            Console.Clear();
                            Console.WriteLine("Agenda Electronica");
                            Console.WriteLine("[1] Contactos");
                            Console.WriteLine("[2] Eventos");
                            Console.WriteLine("[3] Salir");
                            Console.Write("Selecciona una opcion: ");
                            opcion = byte.Parse(Console.ReadLine()!);

                            switch (opcion)
                            {

                                // Contactos
                                case 1:

                                    // Variables
                                    int cantidadContactoAgregar = 0;
                                    byte opcionMenuContactos;

                                    // Listas
                                    List<string[]> contactos = new List<string[]>();

                                    // Pasar los datos de sql a la lista de los contactos
                                    DatosAListaContact(contactos, connection);

                                    do
                                    {
                                        // Menu de los contactos
                                        Console.Clear();
                                        Console.WriteLine("Menu del Contacto");
                                        Console.WriteLine("[1] Agregar contacto");
                                        Console.WriteLine("[2] Buscar contacto");
                                        Console.WriteLine("[3] Ver lista de contactos");
                                        Console.WriteLine("[4] Borrar todos los contactos");
                                        Console.WriteLine("[5] Salir");
                                        Console.Write("Selecciona una opcion: ");
                                        opcionMenuContactos = byte.Parse(Console.ReadLine()!);

                                        switch (opcionMenuContactos)
                                        {

                                            // Agregar contacto
                                            case 1:

                                                // Metodo para agregar contactos
                                                op_contactos.AgregarContacto(contactos, cantidadContactoAgregar);

                                                break;


                                            // Buscar Contacto
                                            case 2:

                                                // Metodo para buscar un contacto
                                                op_contactos.BuscarContacto(contactos);

                                                break;


                                            // Mostrar lista de contactos
                                            case 3:

                                                // Metodo para mostrar la lista de contactos
                                                op_contactos.MostrarContactos(contactos);

                                                break;


                                            // Eliminar todos los contactos
                                            case 4:

                                                // Metodo para eliminar todos los contactos
                                                op_contactos.BorrarTodosContactos(contactos);

                                                break;



                                           // Saliendo
                                            case 5:

                                                Console.Clear();
                                                Console.WriteLine("Saliendo....");
                                                Thread.Sleep(1000);
                                                Console.Clear();

                                                break;


                                            // Error!
                                            default:

                                                Console.Clear();
                                                Console.WriteLine("Opcion no valida....");
                                                Thread.Sleep(1000);
                                                Console.Clear();

                                                break;


                                        }

                                    } while (opcionMenuContactos != 5);

                                    break;


                                // Eventos
                                case 2:

                                    // Variables
                                    int cantidadEventoAgregar = 0;
                                    byte opcionMenuEventos;

                                    // Listas
                                    List<string[]> eventos = new List<string[]>();

                                    // Pasar los datos de sql a la lista de los eventos
                                    DatosAListaEvent(eventos, connection);


                                    do
                                    {
                                        // Menu de los eventos
                                        Console.Clear();
                                        Console.WriteLine("Menu de Eventos");
                                        Console.WriteLine("[1] Agregar Evento");
                                        Console.WriteLine("[2] Buscar Evento");
                                        Console.WriteLine("[3] Mostrar lista de eventos");
                                        Console.WriteLine("[4] Borrar todos los eventos");
                                        Console.WriteLine("[5] Salir");
                                        Console.Write("Selecciona una opcion: ");
                                        opcionMenuEventos = byte.Parse(Console.ReadLine()!);

                                        switch (opcionMenuEventos)
                                        {

                                            // Agregar evento
                                            case 1:

                                                // Metodo para agregar eventos
                                                op_eventos.AgregarEvento(eventos, cantidadEventoAgregar);
                                                break;


                                            // Buscar evento
                                            case 2:

                                                // Metodo para buscar un eventos
                                                op_eventos.BuscarEvento(eventos);

                                                break;


                                            // Mostrar la lista de los eventos
                                            case 3:

                                                // Metodo para mostrar la lista de eventos
                                                op_eventos.MostrarEventos(eventos);

                                                break;


                                            // Borrar todos los eventos
                                            case 4:
                                                
                                                // Metodo para eliminar todos los eventos
                                                op_eventos.BorrarTodosEventos(eventos);

                                                break;

                                            
                                            // Saliendo
                                            case 5:

                                                Console.Clear();
                                                Console.WriteLine("Saliendo....");
                                                Thread.Sleep(1000);
                                                Console.Clear();

                                                break;


                                            // Error!
                                            default:

                                                Console.Clear();
                                                Console.WriteLine("Opcion no valida....");
                                                Thread.Sleep(1000);
                                                Console.Clear();

                                                break;


                                        }

                                    } while (opcionMenuEventos != 5);

                                    break;


                                // Saliendo
                                case 3:

                                    Console.Clear();
                                    Console.WriteLine("Saliendo....");
                                    Thread.Sleep(1000);
                                    Console.Clear();

                                    break;


                                // Error!
                                default:

                                    Console.Clear();
                                    Console.WriteLine("Opcion no valida....");
                                    Thread.Sleep(1000);
                                    Console.Clear();

                                    break;
                            }

                        } while (opcion != 3);

                        break;



                    // Modulo Utilitario
                    case 2:

                        // Variables
                        byte opcionModoUtilitario;

                        do
                        {
                            // Menu del modulo utilitario
                            Console.Clear();
                            Console.WriteLine("Modo Utilitario");
                            Console.WriteLine("[1] Conversor de Unidades");
                            Console.WriteLine("[2] Calculadora simple");
                            Console.WriteLine("[3] Salir");
                            Console.Write("Selecciona una opcion: ");
                            opcionModoUtilitario = byte.Parse(Console.ReadLine()!);

                            switch (opcionModoUtilitario)
                            {

                                // Conversor de Unidades
                                case 1:

                                    // Variables
                                    byte opcionConversorUnidad;

                                    do
                                    {
                                        // Menu del convesor de unidades
                                        Console.Clear();
                                        Console.WriteLine("Modo Conversor Unidades\n");
                                        Console.WriteLine("[1] Grados Fahrenheit a Celsius");
                                        Console.WriteLine("[2] Grados Celsius a Fahrenheit");
                                        Console.WriteLine("[3] Grados Fahrenheit a Kelvin");
                                        Console.WriteLine("[4] Grados Kelvin a Fahrenheit");
                                        Console.WriteLine("[5] Grados Celsius a Kelvin");
                                        Console.WriteLine("[6] Grados Kelvin a Celsius");
                                        Console.WriteLine("[7] Salir\n");
                                        Console.Write("Selecciona una opcion: ");
                                        opcionConversorUnidad = byte.Parse(Console.ReadLine()!);

                                        switch (opcionConversorUnidad)
                                        {

                                            // Convertir Grados Fahrenheit a Celsius
                                            case 1:

                                                break;


                                            // Convertir Grados Celsius a Fahrenheit
                                            case 2:

                                                break;


                                            // Convertir Grados Fahrenheit a Kelvin
                                            case 3:
                                                
                                                break;


                                            // Convertir Grados Kelvin a Fahrenheit
                                            case 4:
                                            
                                                break;


                                            // Convertir Grados Celsius a Kelvin
                                            case 5:

                                                break;


                                            // Convertir Grados Kelvin a Celsius
                                            case 6:

                                                break;


                                            // Saliendo
                                            case 7:

                                                Console.Clear();
                                                Console.WriteLine("Saliendo....");
                                                Thread.Sleep(1000);
                                                Console.Clear();

                                                break;


                                            // Error!
                                            default:

                                                Console.Clear();
                                                Console.WriteLine("Opcion no valida....");
                                                Thread.Sleep(1000);
                                                Console.Clear();

                                                break;
                                        }




                                    } while (opcionConversorUnidad != 7);

                                    break;


                                // Calculadora simple
                                case 2:
                                    
                                    break;


                                // Saliendo
                                case 3:

                                    Console.Clear();
                                    Console.WriteLine("Saliendo....");
                                    Thread.Sleep(1000);
                                    Console.Clear();

                                    break;


                                // Error!
                                default :

                                    Console.Clear();
                                    Console.WriteLine("Opcion no valida....");
                                    Thread.Sleep(1000);
                                    Console.Clear();

                                    break;
                            }


                        } while (opcionModoUtilitario != 3);

                        break;


                    // Salir
                    case 3:

                        Console.Clear();
                        Console.WriteLine("Saliendo....");
                        Thread.Sleep(1000);
                        Console.Clear();

                        break;


                    // Error!
                    default:

                        Console.Clear();
                        Console.WriteLine("Opcion no valida....");
                        Thread.Sleep(1000);
                        Console.Clear();

                        break;
                }



            } while (opcionMenuPrincipal != 3);
        }

        // Metodo para pasar todos los datos de la tabla contactos a la lista creada para los contactos
        static void DatosAListaContact(List<string[]> lista, SqlConnection connection)
        {
            connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true");

            connection.Open();
            string query = "select * from Contactos";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string nombre = reader.GetString(1);
                string apellido = reader.GetString(2);
                string direccion = reader.GetString(3);
                string telefono = reader.GetString(4);
                string[] datosContactos = new string[] { nombre, apellido, direccion, telefono };
                lista.Add(datosContactos);
            }

            reader.Close();
            connection.Close();

        }

        // Metodo para pasar todos los datos de la tabla eventos a la lista creada para los eventos
        static void DatosAListaEvent(List<string[]> lista, SqlConnection connection)
        {
            connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();
            string query = "select * from Eventos";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string nombre = reader.GetString(1);
                string lugar = reader.GetString(2);
                string fecha = reader.GetDateTime(3).ToString();
                string hora = reader.GetTimeSpan(4).ToString();
                string[] datosEventos = new string[] { nombre, lugar, fecha, hora };
                lista.Add(datosEventos);
            }

            reader.Close();
            connection.Close();

        }

    }
}