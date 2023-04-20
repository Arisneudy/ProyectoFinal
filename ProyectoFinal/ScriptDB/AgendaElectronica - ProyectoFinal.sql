--Creacion de la base de datos
create database AgendaElectronica
go
use AgendaElectronica
go

--Tabla contactos
create table Contactos (
	id int primary key identity (1,2),
	nombre varchar (50) not null,
	Apellido varchar (100) not null,
	direccion varchar (300) null,
	telefono varchar (80) null
);

--Tabla de eventos
create table Eventos (
	id int primary key identity (1,2),
	nombre varchar (50) not null,
	lugar varchar (100) not null,
	fecha date not null,
	hora time not null
);


/*INSERTANDO DATOS - TABLA EVENTOS*/
insert into Eventos (nombre, lugar, fecha, hora)
values ('Jugar tenis', 'Complejo spm', '2023-04-20', CONVERT(time, '11:30 AM',108))

/*INSERTANDO DATOS - TABLA CONTACTOS*/
insert into Contactos (nombre, apellido, direccion, telefono)
values ('Arisneudy', 'Santana Pozo', 'Villa Azucarera', '8296678519');

select * from Contactos


select id, nombre, lugar, FORMAT(fecha, 'dd-MM-yy', 'es-es' ) as Fecha, hora from Eventos

delete from Eventos