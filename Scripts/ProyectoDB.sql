CREATE TABLE usuarios(
id_usuario int primary key identity(1,1),
nombre nvarchar(20) not null,
correo nvarchar(100) unique not null,
contrasenia nvarchar(8),
usuario_crea int,
fecha_crea datetime,
usuario_modifica int,
fecha_modifica datetime,
activo nvarchar(2)
)
go
create table roles(
id_rol int primary key identity(1,1),
nombre nvarchar(20) not null unique,
descripcion nvarchar(50),
usuario_crea int,
fecha_crea datetime,
usuario_modifica int,
fecha_modifica datetime
)
go
create table permisos(
id_permiso int primary key identity(1,1),
nombre nvarchar(20) not null unique,
descripcion nvarchar(50),
usuario_crea int,
fecha_crea datetime,
usuario_modifica int,
fecha_modifica datetime
)

go
create table usuarios_roles(
id_usuario int not null,
id_rol int not null,
constraint PK_usuarios_roles primary key(id_usuario,id_rol),
constraint Fk_usuarios_roles_usuario_id foreign key(id_usuario)  references usuarios(id_usuario),
constraint Fk_usuarios_roles_rol_id foreign key(id_rol)  references roles(id_rol)
)
go
create table roles_permisos(
id_rol int not null,
id_permiso int not null,
constraint PK_roles_permisos primary key(id_rol,id_permiso),
constraint Fk_roles_permisos_rol_id foreign key(id_rol)  references roles(id_rol),
constraint Fk_roles_permisos_permiso_id foreign key(id_permiso)  references permisos(id_permiso)
)
create table menus(
id_menu int primary key identity(1,1),
descripcion nvarchar(100),
link nvarchar(255),
icono nvarchar(50),
activo nvarchar(2),
menu_padre int,
orden_menu int
)
go 
create table roles_menus(
id_rol int,
id_menu int,
constraint PK_roles_menus primary key(id_rol,id_menu),
constraint Fk_roles_menus_rol_id foreign key(id_rol)  references roles(id_rol),
constraint Fk_roles_menus_menu_id foreign key(id_menu)  references menus(id_menu)
)
