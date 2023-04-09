
ALTER TABLE tfg.[dbo].[RespuestaFormularios]  WITH CHECK ADD  CONSTRAINT [FK_RespuestaFormularios_Pregunta] FOREIGN KEY([IdPregunta])
	REFERENCES tfg.[dbo].[PreguntaFormularios] ([IdPregunta])
	ON UPDATE CASCADE
	ON DELETE CASCADE;

ALTER TABLE tfg.[dbo].[RespuestaFormularios]  WITH CHECK ADD  CONSTRAINT [FK_RespuestaFormularios_IdUsuario] FOREIGN KEY([IdUsuario])
	REFERENCES tfg.[dbo].[Usuarios] ([IdUsuario])
	ON UPDATE CASCADE
	ON DELETE CASCADE;

