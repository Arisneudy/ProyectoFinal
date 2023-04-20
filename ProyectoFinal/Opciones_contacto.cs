using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Contracts;

namespace ProyectoFinal
{

    public class Opciones_contacto
    {

        // Metodo para agregar un contacto
        public void AgregarContacto(List<string[]> contactos, int cantidadContactoAgregar)
        {
            Console.Clear();
            Console.WriteLine("Cuantos contactos desea agregar: ");
            cantidadContactoAgregar = int.Parse(Console.ReadLine()!);

            Console.Clear();
            for (int i = 0; i < cantidadContactoAgregar; i++)
            {
                Console.Clear();
                Console.WriteLine("Ingrese un nombre: ");
                string nombre = Console.ReadLine()!;

                Console.WriteLine("Ingrese un apellido: ");
                string apellido = Console.ReadLine()!;

                Console.WriteLine("Ingrese su direccion: ");
                string direccion = Console.ReadLine()!;

                Console.WriteLine("Ingrese su numero telefonico: ");
                string telefono = Console.ReadLine()!;

                // Insertando los datos obtenidos a la lista creada
                contactos.Add(new string[] { nombre, apellido, direccion, telefono });

                // Insertando los datos obtenidos a SQL SERVER
                // Se utiliza la clausula using para manejar correctamente la conexión y el comando en SQL Server. 
                using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
                {
                    connection.Open();

                    // Consulta para insertar los datos
                    string query = "insert into Contactos (nombre, apellido, direccion, telefono)" +
                                   "values (@nombre, @apellido, @direccion, @telefono)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametros para indicar que datos son los datos a insertar
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@apellido", apellido);
                        command.Parameters.AddWithValue("@direccion", direccion);
                        command.Parameters.AddWithValue("@telefono", telefono);

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


        // Metodo para buscar un contacto y dentro de este se pueden editar o borrar
        public void BuscarContacto(List<string[]> contacto)
        {

            // Variable de comprobacion
            bool comprobacion = false;

            Console.Clear();
            Console.WriteLine("Ingrese el nombre del contacto: ");
            string nombreBuscar = Console.ReadLine()!;

            Console.WriteLine("Ingrese el apellido del contacto: ");
            string apellidoBuscar = Console.ReadLine()!;

            for (int i = 0; i < contacto.Count; i++)
            {

                if ((nombreBuscar == contacto[i][0] && apellidoBuscar == contacto[i][1] || (contacto[i][0].StartsWith(nombreBuscar)) && (contacto[i][1].StartsWith(apellidoBuscar))))
                {

                    Console.Clear();
                    Console.WriteLine("Usuario encontrado....");
                    comprobacion = true; // Variable de comprobacion
                    Thread.Sleep(2000);
                    Console.Clear();

                    Console.WriteLine("Datos del contacto: ");
                    Console.WriteLine($"Nombre: {contacto[i][0]}");
                    Console.WriteLine($"Apellido: {contacto[i][1]}");
                    Console.WriteLine($"Direccion: {contacto[i][2]}");
                    Console.WriteLine($"Telefono: {contacto[i][3]}\n");

                    Console.WriteLine("Menu de opciones");
                    Console.WriteLine("[1] Editar contacto");
                    Console.WriteLine("[2] Borrar contacto");
                    Console.WriteLine("[3] Salir");
                    Console.WriteLine("Elija una opcion: ");
                    byte opcionMenuBuscar = byte.Parse(Console.ReadLine()!);

                    switch (opcionMenuBuscar)
                    {
                        // Editar contacto
                        case 1:

                            EditarContacto(contacto, nombreBuscar, apellidoBuscar);
                            break;

                        //Borrar contacto
                        case 2:

                            BorrarContacto(contacto, nombreBuscar, apellidoBuscar);
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
                Console.WriteLine("El contacto no existe....");
                Thread.Sleep(1000);
                Console.WriteLine("Presione una tecla para continuar....");
                Console.ReadKey();
            }

        }


        // Metodo para borrar un contacto
        public void BorrarContacto(List<string[]> contactos, string nombreEliminar, string apellidoEliminar)
        {

            bool encontrado = false;

            // Buscar y eliminar el contacto de la lista
            contactos.RemoveAll(c => c[0] == nombreEliminar && c[1] == apellidoEliminar);
            encontrado = true; // Variable de comprobacion
  
            // Eliminar el contacto de SQL Server
            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
            {
                connection.Open();

                // Consulta para eliminar los datos
                string query = "delete from Contactos where nombre = @nombreEliminar and apellido = @apellidoEliminar";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreEliminar", nombreEliminar);
                    command.Parameters.AddWithValue("@apellidoEliminar", apellidoEliminar);

                    command.ExecuteNonQuery();
                }
            }

            Console.Clear();
            Console.WriteLine("El usuario ha sido removido con exito....");
            Thread.Sleep(1000);
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
 
            // Contacto no existe
            if (encontrado != true)
            {
                Console.Clear();
                Thread.Sleep(1000);
                Console.WriteLine("El contacto no existe....");
                Thread.Sleep(1000);
                Console.WriteLine("Presione una tecla para continuar....");
                Console.ReadKey();
            }

        }


        // Metodo para borrar todos los contactos
        public void BorrarTodosContactos(List<string[]> contactos)
        {
            // Comprobacion si la lista esta vacia
            if (contactos.Count == 0)
            {
                Console.Clear();
                Thread.Sleep(1000);
                Console.WriteLine("Lista vacia......");
                Console.WriteLine("Presione una tecla para continuar....");
                Console.ReadKey();
                return;
            }

            contactos.Clear();

            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
            {
                connection.Open();

                // Consulta para seleccionar los datos
                string query = "delete from Contactos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            Console.Clear();
            Console.WriteLine("Contactos eliminados correctamente......");
            Thread.Sleep(1000);
            Console.WriteLine("Presione una tecla para salir.....");
            Console.ReadKey();
        }


        // Metodo para editar un contacto
        public void EditarContacto(List<string[]> contactos, string nombreEditar, string apellidoEditar)
        {

            // Variable de comprobacion
            bool comprobacion = false;

            // Variable para consultas
            string query;

            for (int i = 0; i < contactos.Count; i++)
            {
                // Contacto encontrado y editado
                if (nombreEditar == contactos[i][0] && apellidoEditar == contactos[i][1] || (contactos[i][0].StartsWith(nombreEditar)) && (contactos[i][1].StartsWith(apellidoEditar)))
                {
                    comprobacion = true;
                    Console.Clear();
                    Console.WriteLine("Elija una opcion: ");
                    Console.WriteLine("[1] Nombre");
                    Console.WriteLine("[2] Apellido");
                    Console.WriteLine("[3] Direccion");
                    Console.WriteLine("[4] Telefono");
                    Console.Write("Que desea editar: ");
                    byte opcionEditar = byte.Parse(Console.ReadLine()!);

                    switch (opcionEditar)
                    {
                        //Nuevo nombre
                        case 1:

                            Console.Clear();
                            Console.Write("Ingrese el nuevo nombre del contacto: ");
                            string nuevoNombre = Console.ReadLine()!;

                            comprobacion = true; // Variable de comprobacion

                            // Crear la consulta SQL para actualizar el nombre en la base de datos
                            query = "UPDATE Contactos SET nombre = @nuevoNombre WHERE nombre = @nombreAntiguo AND apellido = @apellidoAntiguo";

                            // Crear el objeto SqlCommand y asignar los valores de los parametros
                            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
                            {
                                connection.Open();

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                                    command.Parameters.AddWithValue("@nombreAntiguo", contactos[i][0]);
                                    command.Parameters.AddWithValue("@apellidoAntiguo", contactos[i][1]);

                                    // Ejecutar la consulta SQL
                                    command.ExecuteNonQuery();
                                }

                                connection.Close();
                            }


                            // Actualizar la lista de contactos con el nuevo nombre
                            contactos[i][0] = nuevoNombre;

                            Console.Clear();
                            Console.WriteLine("Su usuario se ha actualizado correctamente!");
                            Thread.Sleep(1000);
                            Console.Clear();

                            Console.WriteLine($"Nombre: {contactos[i][0]}");
                            Console.WriteLine($"Apellido: {contactos[i][1]}");
                            Console.WriteLine($"Direccion: {contactos[i][2]}");
                            Console.WriteLine($"Telefono: {contactos[i][3]}\n");

                            Thread.Sleep(1000);
                            Console.WriteLine("Presione una tecla para salir.....");
                            Console.ReadKey();

                            break;


                        //Nuevo apellido
                        case 2:

                            Console.Clear();
                            Console.Write("Ingrese el nuevo apellido del contacto: ");
                            string nuevoApellido = Console.ReadLine()!;

                            comprobacion = true; // Variable de comprobacion

                            // Crear la consulta SQL para actualizar el nombre en la base de datos
                            query = "UPDATE Contactos SET apellido = @nuevoApellido WHERE nombre = @nombreAntiguo AND apellido = @apellidoAntiguo";

                            // Crear el objeto SqlCommand y asignar los valores de los parametros
                            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
                            {
                                connection.Open();

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@nuevoApellido", nuevoApellido);
                                    command.Parameters.AddWithValue("@nombreAntiguo", contactos[i][0]);
                                    command.Parameters.AddWithValue("@apellidoAntiguo", contactos[i][1]);

                                    // Ejecutar la consulta SQL
                                    command.ExecuteNonQuery();
                                }

                                connection.Close();
                            }


                            // Actualizar la lista de contactos con el nuevo nombre
                            contactos[i][1] = nuevoApellido;

                            Console.Clear();
                            Console.WriteLine("Su usuario se ha actualizado correctamente!");
                            Thread.Sleep(1000);
                            Console.Clear();

                            Console.WriteLine($"Nombre: {contactos[i][0]}");
                            Console.WriteLine($"Apellido: {contactos[i][1]}");
                            Console.WriteLine($"Direccion: {contactos[i][2]}");
                            Console.WriteLine($"Telefono: {contactos[i][3]}\n");

                            Thread.Sleep(1000);
                            Console.WriteLine("Presione una tecla para salir.....");
                            Console.ReadKey();

                            break;


                        //Nueva direccion
                        case 3:

                            Console.Clear();
                            Console.Write("Ingrese la nueva direccion del contacto: ");
                            string nuevaDireccion = Console.ReadLine()!;

                            comprobacion = true; // Variable de comprobacion

                            // Crear la consulta SQL para actualizar el nombre en la base de datos
                            query = "UPDATE Contactos SET direccion = @nuevaDireccion WHERE nombre = @nombreAntiguo AND apellido = @apellidoAntiguo";

                            // Crear el objeto SqlCommand y asignar los valores de los parametros
                            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
                            {
                                connection.Open();

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@nuevaDireccion", nuevaDireccion);
                                    command.Parameters.AddWithValue("@nombreAntiguo", contactos[i][0]);
                                    command.Parameters.AddWithValue("@apellidoAntiguo", contactos[i][1]);

                                    // Ejecutar la consulta SQL
                                    command.ExecuteNonQuery();
                                }

                            }


                            // Actualizar la lista de contactos con el nuevo nombre
                            contactos[i][2] = nuevaDireccion;

                            Console.Clear();
                            Console.WriteLine("Su usuario se ha actualizado correctamente!");
                            Thread.Sleep(1000);
                            Console.Clear();

                            Console.WriteLine($"Nombre: {contactos[i][0]}");
                            Console.WriteLine($"Apellido: {contactos[i][1]}");
                            Console.WriteLine($"Direccion: {contactos[i][2]}");
                            Console.WriteLine($"Telefono: {contactos[i][3]}\n");

                            Thread.Sleep(1000);
                            Console.WriteLine("Presione una tecla para salir.....");
                            Console.ReadKey();

                            break;

                        //Nuevo telefono
                        case 4:

                            Console.Clear();
                            Console.Write("Ingrese el nuevo telefono del contacto: ");
                            string nuevoTelefono = Console.ReadLine()!;

                            comprobacion = true; // Variable de comprobacion

                            // Crear la consulta SQL para actualizar el nombre en la base de datos
                            query = "UPDATE Contactos SET telefono = @nuevoTelefono WHERE nombre = @nombreAntiguo AND apellido = @apellidoAntiguo";

                            // Crear el objeto SqlCommand y asignar los valores de los parametros
                            using (SqlConnection connection = new SqlConnection("Data Source=ASANTPOZO\\ASANTPOZO;Initial Catalog=AgendaElectronica;Integrated Security=True;TrustServerCertificate=true"))
                            {
                                connection.Open();

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@nuevoTelefono", nuevoTelefono);
                                    command.Parameters.AddWithValue("@nombreAntiguo", contactos[i][0]);
                                    command.Parameters.AddWithValue("@apellidoAntiguo", contactos[i][1]);

                                    // Ejecutar la consulta SQL
                                    command.ExecuteNonQuery();
                                }

                                connection.Close();
                            }


                            // Actualizar la lista de contactos con el nuevo nombre
                            contactos[i][3] = nuevoTelefono;

                            Console.Clear();
                            Console.WriteLine("Su contacto se ha actualizado correctamente!");
                            Thread.Sleep(1000);
                            Console.Clear();

                            Console.WriteLine($"Nombre: {contactos[i][0]}");
                            Console.WriteLine($"Apellido: {contactos[i][1]}");
                            Console.WriteLine($"Direccion: {contactos[i][2]}");
                            Console.WriteLine($"Telefono: {contactos[i][3]}\n");

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


        // Metodo para mostrar la lista de contactos
        public void MostrarContactos(List<string[]> contacto)
        {
            // Comprobacion si la lista esta vacia
            if (contacto.Count == 0)
            {
                Console.Clear();
                Thread.Sleep(1000);
                Console.WriteLine("Lista vacia......");
                Console.WriteLine("Presione una tecla para continuar....");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Lista de Contactos:");

            for (int i = 0; i < contacto.Count; i++)
            {
                Console.WriteLine($"Nombre: {contacto[i][0]} {contacto[i][1]}");
                Console.WriteLine($"Dirección: {contacto[i][2]}");
                Console.WriteLine($"Teléfono: {contacto[i][3]}\n");
            }

            Thread.Sleep(1000);
            Console.WriteLine("Presione una tecla para salir.....");
            Console.ReadKey();
        }

    }
}
