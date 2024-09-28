USE [dbEmpleados]
GO

/****** Object:  StoredProcedure [dbo].[uspEmpleados]    Script Date: 28/09/2024 07:51:54 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Iván Pacheco González>
-- Create date: <28/09/2024>
-- Description:	<Store Procedure para consulta de empleados>
-- =============================================
CREATE PROCEDURE [dbo].[uspEmpleados] 
	@UserId int = NULL,
	@Sueldo decimal(18, 2) = NULL,
	@FechaIngreso date = NULL,
	@FechaAlta datetime = NULL,
	@Activo bit = NULL,
	@Login nvarchar(100) = NULL,
	@Nombre nvarchar(100) = NULL,
	@Paterno nvarchar(100) = NULL,
	@Materno nvarchar(100) = NULL,
	@Bandera nvarchar(10) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @Bandera = 'S1'
	BEGIN
		SELECT top(10)
		Usuarios.UserId, 
		[Login], 
		Nombre + ' ' + Paterno + ' ' + Materno AS Nombre,
		FORMAT(Sueldo, 'C', 'en-US') AS Sueldo,
		FORMAT(FechaIngreso, 'dd/MM/yyyy') AS FechaIngreso
		FROM Usuarios
		INNER JOIN Empleados ON Empleados.UserId = Usuarios.UserId
		order by Usuarios.FechaAlta desc, Nombre
	END

	
	IF @Bandera = 'S2'
	BEGIN
		SELECT 
		[Login],		 
		Nombre + ' ' + Paterno + ' ' + Materno AS NombreCompleto,
		Sueldo,
		FechaIngreso
		FROM Usuarios
		INNER JOIN Empleados ON Empleados.UserId = Usuarios.UserId
		order by Usuarios.FechaAlta desc, Nombre
	END

	IF @Bandera = 'U1'
		BEGIN
			UPDATE Empleados set Sueldo = @Sueldo where UserId = @UserId
		END

	IF @Bandera = 'I1'
	BEGIN
	
		DECLARE @NewUserId bigint
		INSERT INTO Usuarios ([Login], Nombre, Paterno, Materno) values (@Login, UPPER(@Nombre), UPPER(@Paterno), UPPER(@Materno))

		SET @NewUserId = SCOPE_IDENTITY()

		INSERT INTO Empleados (UserId, Sueldo, FechaIngreso) values (@NewUserId, @Sueldo, GETDATE())

	END
	
END
GO


