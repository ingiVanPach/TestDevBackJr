USE [dbEmpleados]
GO

/****** Object:  Table [dbo].[LogIn]    Script Date: 28/09/2024 07:52:41 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LogIn](
	[IdLogIn] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [nvarchar](50) NULL,
	[Contraseña] [nvarchar](50) NULL,
	[FechaAlta] [datetime] NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_LogIn] PRIMARY KEY CLUSTERED 
(
	[IdLogIn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LogIn] ADD  CONSTRAINT [DF_LogIn_FechaAlta]  DEFAULT (getdate()) FOR [FechaAlta]
GO

ALTER TABLE [dbo].[LogIn] ADD  CONSTRAINT [DF_LogIn_Activo]  DEFAULT ((1)) FOR [Activo]
GO


