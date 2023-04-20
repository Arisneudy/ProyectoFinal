using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Contracts;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoFinal
{

    public class Opciones_eventos
    {

        // Metodo para agregar un evento
        public void AgregarEvento(List<string[]> eventos, int cantidadEventosAgregar)
        {
            Console.Clear();
            Console.WriteLine("Cuantos eventos desea agregar: ");
            cantidadEventosAgregar = int.Parse(Console.ReadLine()!);

            Console.Clear();
            for (int i = 0; i < cantidadEventosAgregar; i++)
            {
                Console.Clear();
                Console.WriteLine("Ingrese el nombre: ");
                string nombre = Console.ReadLine()!;

                Console.WriteLine("Ingrese el lugar: ");
                string lugar = Console.ReadLine()!;

                Console.WriteLine("Ingrese la fecha (Formato = Año-mes-dia): ");
                string fecha = Console.ReadLine()!;

                Console.WriteLine("Ingrese la hora: ");
                string hora = Console.ReadLine()!;

                // Insertando los datos obtenidos a la lista creada
                eventos.Add(new string[] { nombre, lugar, fecha, hora });

                // Insertando los datos obtenidos a SQL SERVER
                // Se utiliza la clausula using para manejar correctamente la conexión y el comando en SQL Server. 
                using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
                {
                    connection.Open();

                    // Consulta para insertar los datos
                    string query = "insert into Eventos (nombre, lugar, fecha, hora)" +
                                   "values (@nombre, @lugar, @fecha, CONVERT(time, @hora ,108))";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametros para indicar que datos son los datos a insertar
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@lugar", lugar);
                        command.Parameters.AddWithValue("@fecha", fecha);
                        command.Parameters.AddWithValue("@hora", hora);

                        command.ExecuteNonQuery();
                    }
                }
            }


            Console.Clear();
            Console.WriteLine("Contactos agregados......");
            Thread.Sleep(1000);
            Console.WriteLine("Presiona cualquier tecla.....");
            Console.ReadKey();
        }


        // Metodo para buscar un evento y dentro de este se pueden editar o borrar
        public void BuscarEvento(List<string[]> eventos)
        {

            // Variable de comprobacion
            bool comprobacion = false;

            Console.Clear();
            Console.WriteLine("Ingrese el nombre del evento: ");
            string eventoBuscar = Console.ReadLine()!;

            for (int i = 0; i < eventos.Count; i++)
            {

                if ((eventoBuscar == eventos[i][0]) || (eventos[i][0].StartsWith(eventoBuscar)))
                {

                    Console.Clear();
                    Console.WriteLine("Evento encontrado....");
                    comprobacion = true; // Variable de comprobacion
                    Thread.Sleep(2000);
                    Console.Clear();

                    Console.WriteLine("Datos del evento: ");
                    Console.WriteLine($"Nombre: {eventos[i][0]}");
                    Console.WriteLine($"Lugar: {eventos[i][1]}");
                    Console.WriteLine($"Fecha: {eventos[i][2]}");
                    Console.WriteLine($"Hora: {eventos[i][3]}\n");

                    Console.WriteLine("Menu de opciones");
                    Console.WriteLine("[1] Editar evento");
                    Console.WriteLine("[2] Borrar evento");
                    Console.WriteLine("[3] Salir");
                    Console.WriteLine("Elija una opcion: ");
                    byte opcionMenuBuscar = byte.Parse(Console.ReadLine()!);

                    switch (opcionMenuBuscar)
                    {
                        // Editar contacto
                        case 1:

                            EditarEvento(eventos, eventoBuscar);
                            break;

                        //Borrar contacto
                        case 2:

                            BorrarEvento(eventos, eventoBuscar);
                            break;

                        case 3:

                            Console.Clear();
                            Console.WriteLine("Saliendo....");
                            Thread.Sleep(1000);
                            Console.Clear();

                            break;

                        default:

                            Console.Clear();
                            Console.WriteLine("Opcion no valida!");

                            break;
                    }

                    break;
                }
            }

            // Contacto no existe
            if (comprobacion != true)
            {
                Console.Clear();
                Thread.Sleep(2000);
                Console.WriteLine("El evento no existe....");
                Thread.Sleep(1000);
                Console.WriteLine("Presione una tecla para continuar....");
                Console.ReadKey();
            }

        }


        // Metodo para borrar un evento
        public void BorrarEvento(List<string[]> eventos, string nombreEvento)
        {

            bool encontrado = false;

            // Buscar y eliminar el contacto de la lista
            eventos.RemoveAll(c => c[0] == nombreEvento);
            encontrado = true; // Variable de comprobacion

            // Eliminar el contacto de SQL Server
            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
            {
                connection.Open();

                // Consulta para eliminar los datos
                string query = "delete from Eventos where nombre = @nombreEvento";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreEvento", nombreEvento);

                    command.ExecuteNonQuery();
                }
            }

            Console.Clear();
            Console.WriteLine("El evento ha sido removido con exito....");
            Thread.Sleep(1000);
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            // Contacto no existe
            if (encontrado != true)
            {
                Console.Clear();
                Thread.Sleep(1000);
                Console.WriteLine("El evento no existe....");
                Thread.Sleep(1000);
                Console.WriteLine("Presione una tecla para continuar....");
                Console.ReadKey();
            }

        }


        // Metodo para borrar todos los eventos
        public void BorrarTodosEventos(List<string[]> eventos)
        {
            // Comprobacion si la lista esta vacia
            if (eventos.Count == 0)
            {
                Console.Clear();
                Thread.Sleep(1000);
                Console.WriteLine("Lista vacia......");
                Console.WriteLine("Presione una tecla para continuar....");
                Console.ReadKey();
                return;
            }

            eventos.Clear();

            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
            {
                connection.Open();

                // Consulta para seleccionar los datos
                string query = "delete from Eventos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            Console.Clear();
            Console.WriteLine("Eventos eliminados correctamente......");
            Thread.Sleep(1000);
            Console.WriteLine("Presione una tecla para salir.....");
            Console.ReadKey();
        }

        // Metodo para editar un evento
        public void EditarEvento(List<string[]> eventos, string nombreEvento)
        {

            // Variable de comprobacion
            bool comprobacion = false;

            // Variable para consultas
            string query;

            for (int i = 0; i < eventos.Count; i++)
            {
                // Contacto encontrado y editado
                if (nombreEvento == eventos[i][0] || (eventos[i][0].StartsWith(nombreEvento)))
                {
                    comprobacion = true;
                    Console.Clear();
                    Console.WriteLine("Elija una opcion: ");
                    Console.WriteLine("[1] Nombre");
                    Console.WriteLine("[2] Lugar");
                    Console.WriteLine("[3] Fecha");
                    Console.WriteLine("[4] Hora");
                    Console.Write("Que desea editar: ");
                    byte opcionEditar = byte.Parse(Console.ReadLine()!);

                    switch (opcionEditar)
                    {
                        //Nuevo nombre
                        case 1:

                            Console.Clear();
                            Console.Write("Ingrese el nuevo nombre del evento: ");
                            string nuevoNombre = Console.ReadLine()!;

                            comprobacion = true; // Variable de comprobacion

                            // Crear la consulta SQL para actualizar el nombre en la base de datos
                            query = "UPDATE Eventos SET nombre = @nuevoNombre WHERE nombre = @nombreAntiguo";

                            // Crear el objeto SqlCommand y asignar los valores de los parametros
                            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
                            {
                                connection.Open();

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                                    command.Parameters.AddWithValue("@nombreAntiguo", eventos[i][0]);

                                    // Ejecutar la consulta SQL
                                    command.ExecuteNonQuery();
                                }

                                connection.Close();
                            }


                            // Actualizar la lista de eventos con la nuevo nombre
                            eventos[i][0] = nuevoNombre;

                            Console.Clear();
                            Console.WriteLine("Su evento se ha actualizado correctamente!");
                            Thread.Sleep(1000);
                            Console.Clear();

                            Console.WriteLine($"Nombre: {eventos[i][0]}");
                            Console.WriteLine($"Lugar: {eventos[i][1]}");
                            Console.WriteLine($"Fecha: {eventos[i][2]}");
                            Console.WriteLine($"Hora: {eventos[i][3]}\n");

                            Thread.Sleep(1000);
                            Console.WriteLine("Presione una tecla para salir.....");
                            Console.ReadKey();

                            break;


                        //Nuevo lugar
                        case 2:

                            Console.Clear();
                            Console.Write("Ingrese el nuevo lugar del evento: ");
                            string nuevoLugar = Console.ReadLine()!;

                            comprobacion = true; // Variable de comprobacion

                            // Crear la consulta SQL para actualizar el nombre en la base de datos
                            query = "UPDATE Eventos SET lugar = @nuevoLugar WHERE lugar = @lugarAntiguo";

                            // Crear el objeto SqlCommand y asignar los valores de los parametros
                            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
                            {
                                connection.Open();

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@nuevoApellido", nuevoLugar);
                                    command.Parameters.AddWithValue("@lugarAntiguo", eventos[i][1]);

                                    // Ejecutar la consulta SQL
                                    command.ExecuteNonQuery();
                                }

                                connection.Close();
                            }


                            // Actualizar la lista de eventos con el nuevo lugar
                            eventos[i][1] = nuevoLugar;

                            Console.Clear();
                            Console.WriteLine("Su evento se ha actualizado correctamente!");
                            Thread.Sleep(1000);
                            Console.Clear();

                            Console.WriteLine($"Nombre: {eventos[i][0]}");
                            Console.WriteLine($"Lugar: {eventos[i][1]}");
                            Console.WriteLine($"Fecha: {eventos[i][2]}");
                            Console.WriteLine($"Hora: {eventos[i][3]}\n");

                            Thread.Sleep(1000);
                            Console.WriteLine("Presione una tecla para salir.....");
                            Console.ReadKey();

                            break;


                        //Nueva fecha
                        case 3:

                            Console.Clear();
                            Console.Write("Ingrese la nueva fecha del evento: ");
                            string nuevaFecha = Console.ReadLine()!;

                            comprobacion = true; // Variable de comprobacion

                            // Crear la consulta SQL para actualizar el nombre en la base de datos
                            query = "UPDATE Eventos SET fecha = @nuevaFecha WHERE fecha = @fechaAntigua";

                            // Crear el objeto SqlCommand y asignar los valores de los parametros
                            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
                            {
                                connection.Open();

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@nuevaDireccion", nuevaFecha);
                                    command.Parameters.AddWithValue("@fechaAntigua", eventos[i][2]);

                                    // Ejecutar la consulta SQL
                                    command.ExecuteNonQuery();
                                }

                            }


                            // Actualizar la lista de eventos con la nueva fecha
                            eventos[i][2] = nuevaFecha;

                            Console.Clear();
                            Console.WriteLine("Su evento se ha actualizado correctamente!");
                            Thread.Sleep(1000);
                            Console.Clear();

                            Console.WriteLine($"Nombre: {eventos[i][0]}");
                            Console.WriteLine($"Lugar: {eventos[i][1]}");
                            Console.WriteLine($"Fecha: {eventos[i][2]}");
                            Console.WriteLine($"Hora: {eventos[i][3]}\n");

                            Thread.Sleep(1000);
                            Console.WriteLine("Presione una tecla para salir.....");
                            Console.ReadKey();

                            break;

                        //Nueva hora
                        case 4:

                            Console.Clear();
                            Console.Write("Ingrese la nueva hora del evento: ");
                            string nuevaHora = Console.ReadLine()!;

                            comprobacion = true; // Variable de comprobacion

                            // Crear la consulta SQL para actualizar el nombre en la base de datos
                            query = "UPDATE Eventos SET hora = @nuevaHora WHERE nombre = @horaAntigua";

                            // Crear el objeto SqlCommand y asignar los valores de los parametros
                            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
                            {
                                connection.Open();

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@nuevaHora", nuevaHora);
                                    command.Parameters.AddWithValue("@horaAntigua", eventos[i][3]);

                                    // Ejecutar la consulta SQL
                                    command.ExecuteNonQuery();
                                }

                                connection.Close();
                            }


                            // Actualizar la lista de eventos con la nueva hora
                            eventos[i][3] = nuevaHora;

                            Console.Clear();
                            Console.WriteLine("Su contacto se ha actualizado correctamente!");
                            Thread.Sleep(1000);
                            Console.Clear();

                            Console.WriteLine($"Nombre: {eventos[i][0]}");
                            Console.WriteLine($"Lugar: {eventos[i][1]}");
                            Console.WriteLine($"Fecha: {eventos[i][2]}");
                            Console.WriteLine($"Hora: {eventos[i][3]}\n");

                            Thread.Sleep(1000);
                            Console.WriteLine("Presione una tecla para salir.....");
                            Console.ReadKey();

                            break;

                    }

                    break;
                }
            }

            // Contacto no existe
            if (comprobacion != true)
            {
                Console.Clear();
                Thread.Sleep(2000);
                Console.WriteLine("El contacto no existe....");
                Thread.Sleep(1000);
                Console.WriteLine("Presione una tecla para continuar....");
                Console.ReadKey();
            }

        }


        // Metodo para mostrar la lista de los eventos
        public void MostrarEventos(List<string[]> eventos)
        {
            // Comprobacion si la lista esta vacia
            if (eventos.Count == 0)
            {
                Console.Clear();
                Thread.Sleep(1000);
                Console.WriteLine("Lista vacia......");
                Console.WriteLine("Presione una tecla para continuar....");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Lista de Eventos:\n");

            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
            {
                connection.Open();

                // Consulta para seleccionar los datos
                string query = "SELECT id, nombre, lugar, FORMAT(fecha, 'dd-MM-yy', 'es-es' ) as Fecha, hora FROM Eventos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"Nombre: {reader["nombre"]}");
                        Console.WriteLine($"Lugar: {reader["lugar"]}");
                        Console.WriteLine($"Fecha: {reader["Fecha"]}");
                        Console.WriteLine($"Hora: {reader["hora"]}\n");
                    }
                }
            }

            Thread.Sleep(1000);
            Console.WriteLine("Presione una tecla para salir.....");
            Console.ReadKey();
        }



    }
}