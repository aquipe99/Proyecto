USE [DB_RESERVAS]
GO

INSERT INTO [dbo].[rol]([nombre]) VALUES ('Administrador')
INSERT INTO [dbo].[rol]([nombre]) VALUES ('Trabajador')
GO


INSERT INTO menu (descripcion, link, icono, estado, menu_padre, orden_menu)
		  VALUES ('Reservas' , 'Reservas', 'bi bi-calendar', 1, NULL, 1);
INSERT INTO menu (descripcion, link, icono, estado, menu_padre, orden_menu)
VALUES ('Canchas', 'Canchas', 'bi bi-geo-alt', 1, NULL, 2);
INSERT INTO menu (descripcion, link, icono, estado, menu_padre, orden_menu)
VALUES ('Ingresos', NULL, 'bi bi-bar-chart', 1, NULL, 3);
INSERT INTO menu (descripcion, link, icono, estado, menu_padre, orden_menu)
VALUES ('Usuarios', 'MetodosPago', 'bi bi-credit-card', 1, NULL,4);
INSERT INTO menu (descripcion, link, icono, estado, menu_padre, orden_menu)
VALUES ('Métodos de Pago', 'MetodosPago', 'bi bi-credit-card', 1, NULL,5);



INSERT INTO menu (descripcion, link, icono, estado, menu_padre, orden_menu)
VALUES ('Reporte de Ingresos', 'ReporteIngresos', 'bi bi-cash-stack', 1, 3, 1);

INSERT INTO menu (descripcion, link, icono, estado, menu_padre, orden_menu)
VALUES ('Reporte de Canchas', 'ReporteCanchas', 'bi bi-building', 1, 3, 2);

go
select * from permiso
select * from menu
INSERT INTO permiso (usuario_id, menu_id, usuario_crea, fecha_crea, usuario_modifica, fecha_modifica)
VALUES (1, 1, 1, GETDATE(), NULL, NULL);
INSERT INTO permiso (usuario_id, menu_id, usuario_crea, fecha_crea, usuario_modifica, fecha_modifica)
VALUES (1, 2, 1, GETDATE(), NULL, NULL);
INSERT INTO permiso (usuario_id, menu_id, usuario_crea, fecha_crea, usuario_modifica, fecha_modifica)
VALUES (1, 3, 1, GETDATE(), NULL, NULL);
INSERT INTO permiso (usuario_id, menu_id, usuario_crea, fecha_crea, usuario_modifica, fecha_modifica)
VALUES (1, 4, 1, GETDATE(), NULL, NULL);
INSERT INTO permiso (usuario_id, menu_id, usuario_crea, fecha_crea, usuario_modifica, fecha_modifica)
VALUES (1, 5, 1, GETDATE(), NULL, NULL);
INSERT INTO permiso (usuario_id, menu_id, usuario_crea, fecha_crea, usuario_modifica, fecha_modifica)
VALUES (1, 6, 1, GETDATE(), NULL, NULL);
INSERT INTO permiso (usuario_id, menu_id, usuario_crea, fecha_crea, usuario_modifica, fecha_modifica)
VALUES (1, 7, 1, GETDATE(), NULL, NULL);