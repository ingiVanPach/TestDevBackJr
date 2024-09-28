USE [dbEmpleados]
GO

/****** Object:  StoredProcedure [dbo].[uspLogIn]    Script Date: 28/09/2024 07:52:14 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Iv�n Pacheco Gonz�lez>
-- Create date: <28/09/2024>
-- Description:	<Store Procedure para LogIn al Sistema>
-- =============================================
CREATE PROCEDURE [dbo].[uspLogIn] 
	@idLogIn int = NULL,
	@Usuario nvarchar(50) = NULL,
	@Contrase�a nvarchar(50) = NULL,
	@FechaAlta datetime = NULL,
	@Activo bit = NULL,
	@Bandera nvarchar(10) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @Bandera = 'S1'
	BEGIN
		Select Usuario, [Contrase�a]
		from [LogIn]
		where Usuario = @Usuario and [Contrase�a] = @Contrase�a
	END
	
END
GO


