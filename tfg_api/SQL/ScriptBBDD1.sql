--CREATE DATABASE [tfg];

--Usuario
	CREATE TABLE tfg.[dbo].[Usuarios](
		[IdUsuario] [uniqueidentifier] NOT NULL,
		[Nombre] [nvarchar](50) NOT NULL,
		[Pass] [nvarchar](50) NOT NULL,
		[Administrativas_Contables_Int] [int] NULL DEFAULT 0,
		[Humanisticas_Sociales_Int] [int] NULL DEFAULT 0,
		[Artisticas_Int] [int] NULL DEFAULT 0,
		[Medicina_CsSalud_Int] [int] NULL DEFAULT 0,
		[Ingenieria_Computacion_Int] [int] NULL DEFAULT 0,
		[DefensaSeguridad_Int] [int] NULL DEFAULT 0,
		[CienciasExactas_Agrarias_Int] [int] NULL DEFAULT 0,
		[Administrativas_Contables_Apt] [int] NULL DEFAULT 0,
		[Humanisticas_Sociales_Apt] [int] NULL DEFAULT 0,
		[Artisticas_Apt] [int] NULL DEFAULT 0,
		[Medicina_CsSalud_Apt] [int] NULL DEFAULT 0,
		[Ingenieria_Computacion_Apt] [int] NULL DEFAULT 0,
		[DefensaSeguridad_Apt] [int] NULL DEFAULT 0,
		[CienciasExactas_Agrarias_Apt] [int] NULL DEFAULT 0,
		[CC] [decimal] NULL DEFAULT 0,
		[IGAP] [decimal] NULL DEFAULT 0,
		[ICI] [decimal] NULL DEFAULT 0,
	 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
	(
		[IdUsuario] ASC
	));

	ALTER TABLE tfg.[dbo].[Usuarios] ADD  CONSTRAINT [DF_IdUsuario]  DEFAULT (newsequentialid()) FOR [IdUsuario];

--PreguntaFormularios
	CREATE TABLE tfg.[dbo].[PreguntaFormularios](
		[IdPregunta] [int] NOT NULL IDENTITY (1,1) ,
		[Contenido] [nvarchar](200)  NULL,
		[Imagen_url] [nvarchar](200)  NULL,
		[Tipo] [nvarchar](1) NOT NULL,
	 CONSTRAINT [PK_preguntaFormularios] PRIMARY KEY CLUSTERED 
	(
		[IdPregunta] ASC
	));

	
	
--Asignaturas
	CREATE TABLE  tfg.[dbo].[Asignaturas](
		[IdAsignatura] [uniqueidentifier] NOT NULL,
		[Tipo] [nvarchar](50) NOT NULL,
		[Nombre] [nvarchar](50) NOT NULL,
	 CONSTRAINT [PK_Asignaturas] PRIMARY KEY CLUSTERED 
	(
		[IdAsignatura] ASC
	));
	ALTER TABLE  tfg.[dbo].[Asignaturas] ADD  CONSTRAINT [DF_IdAsignatura]  DEFAULT (newsequentialid()) FOR [IdAsignatura];

--Asignaturas_Usuario
	CREATE TABLE  tfg.[dbo].[Asignaturas_Usuario](
		[IdAsignatura] [uniqueidentifier] NOT NULL,
		[IdUsuario] [uniqueidentifier] NOT NULL,
		[Nota] [float] NOT NULL,
		[TiempoEstudio] [int] NOT NULL,
		[TiempoRecomendado] [int] NOT NULL,
		[Riesgo] [int] NOT NULL,
	 CONSTRAINT [PK_Asignaturas_Usuario] PRIMARY KEY CLUSTERED 
	(
		[IdAsignatura] ASC,
		[IdUsuario] ASC
	));

--RespuestaFormularios
	CREATE TABLE tfg.[dbo].[RespuestaFormularios](
		[IdUsuario] [uniqueidentifier] NOT NULL,
		[IdPregunta] [int] NOT NULL,
		[Valor] [nvarchar](50) NOT NULL,
	 CONSTRAINT [PK_RespuestaFormularios] PRIMARY KEY CLUSTERED 
	(
		[IdUsuario] ASC,
		[IdPregunta] ASC
	 ));



