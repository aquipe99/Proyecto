create table rol(
id int primary key identity(1,1),
nombre nvarchar(20) not null unique
)

CREATE TABLE usuario(
id int primary key identity(1,1),
nombre nvarchar(20) not null,
telefono nvarchar(10),
correo nvarchar(100) unique not null,
contrasenia nvarchar(256),
rol_id int,
usuario_crea int,
fecha_crea datetime,
usuario_modifica int,
fecha_modifica datetime,
estado bit CHECK (estado IN (0,1)),
constraint Fk_usuario_rol_id foreign key(rol_id)  references rol(id)
)

create table menu(
id int primary key identity(1,1),
descripcion nvarchar(100),
link nvarchar(255),
icono nvarchar(50),
estado bit CHECK (estado IN (0,1)),
menu_padre int,
orden_menu int
)

create table permiso(
id int primary key identity(1,1),
usuario_id int not null,
menu_id int not null,
usuario_crea int,
fecha_crea datetime,
usuario_modifica int,
fecha_modifica datetime,
constraint Fk_permiso_usuario_id foreign key(usuario_id)  references usuario(id),
constraint Fk_permiso_menu_id foreign key(menu_id)  references menu(id)
)

CREATE TABLE cancha(
id	int primary key identity(1,1),
nombre nvarchar(20) not null,
descripcion nvarchar(100),
usuario_crea int,
fecha_crea datetime,
usuario_modifica int,
fecha_modifica datetime,
estado bit CHECK (estado IN (0,1))
)

CREATE TABLE metodo_pago(
id	int primary key identity(1,1),
nombre nvarchar(100) not null,
usuario_crea int,
fecha_crea datetime,
usuario_modifica int,
fecha_modifica datetime
)

CREATE TABLE reserva(
id	int primary key identity(1,1),
nombre_cliente nvarchar(100) not null,
fecha  datetime not null,
hora_inicio time not null,
hora_fin time not null,
metodo_pago_id int not null,
tipo_pago nvarchar(10) not null, 
cancha_id int not null,
monto_total decimal (10,2),
monto_pagado decimal (10,2),
telefono nvarchar(10),
usuario_id int not null,
usuario_crea int,
fecha_crea datetime,
usuario_modifica int,
fecha_modifica datetime,
constraint Fk_reserva_metodo_pago_id foreign key(metodo_pago_id)  references metodo_pago(id),
constraint Fk_reserva_cancha_id foreign key(cancha_id)  references cancha(id),
constraint Fk_reserva_usuario_id foreign key(usuario_id)  references usuario(id)
)