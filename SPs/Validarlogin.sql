CREATE PROCEDURE sp_ValidarLogin
    @Email NVARCHAR(100),
    @Password NVARCHAR(100) -- Esto debe ir hasheado en producción
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificamos si el usuario existe
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Email = @Email)
    BEGIN
        SELECT 0 AS CodigoResultado; -- Usuario no existe
        RETURN;
    END

    -- Obtenemos datos del usuario
    DECLARE @ClaveBD NVARCHAR(100);
    DECLARE @Activo BIT;

    SELECT @ClaveBD = Clave, @Activo = EstaActivo
    FROM Usuarios
    WHERE Email = @Email;

    -- Verificamos la contraseña
    IF @ClaveBD <> @Password
    BEGIN
        SELECT 1 AS CodigoResultado; -- Contraseña incorrecta
        RETURN;
    END

    -- Verificamos si está activo
    IF @Activo = 0
    BEGIN
        SELECT 2 AS CodigoResultado; -- Usuario desactivado
        RETURN;
    END

    -- Login exitoso
    SELECT 3 AS CodigoResultado; -- Login correcto
END
