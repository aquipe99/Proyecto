USE [DB_RESERVAS]
GO

CREATE OR ALTER PROCEDURE SP_USUARIO_SELECT_POR_CORREO
    @CORREO		NVARCHAR(100) =NULL
AS
BEGIN
    SELECT TOP 1 
        ID,
        CORREO,
        CONTRASENIA,
        ESTADO
    FROM USUARIO
    WHERE CORREO = @CORREO;
  
END
GO
CREATE OR ALTER PROCEDURE SP_USUARIO_INSERT_UPDATE
    @ID					INT				= NULL,
    @NOMBRE				NVARCHAR(20)	= NULL,
    @TELEFONO			NVARCHAR(10)	= NULL,
    @CORREO				NVARCHAR(100)	= NULL,
    @CONTRASENIA		NVARCHAR(255)	= NULL,
    @ROL_ID				INT				= NULL,
    @USUARIO			INT				= NULL,	
    @ESTADO				CHAR(1)			= NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM USUARIO WHERE ID = @ID)
    BEGIN        
        UPDATE USUARIO
        SET
            NOMBRE			 = @NOMBRE,
            TELEFONO		 = @TELEFONO,
            CORREO			 = @CORREO,
            Contrasenia		 = @CONTRASENIA,
            ROL_ID			 = @ROL_ID,
            USUARIO_MODIFICA = @USUARIO,
            FECHA_MODIFICA   = GETDATE(),
            ESTADO			 = @ESTADO
        WHERE ID = @ID;
    END
    ELSE
    BEGIN        
        INSERT INTO USUARIO (
            NOMBRE,
            TELEFONO,
            CORREO,
            CONTRASENIA,
            ROL_ID,
            USUARIO_CREA,
			FECHA_CREA,
            ESTADO
        )
        VALUES (
            @NOMBRE,
            @TELEFONO,
            @CORREO,
            @CONTRASENIA,
            @ROL_ID,
			@USUARIO,
            GETDATE(),
            @ESTADO
        );
    END
END


