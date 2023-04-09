
ALTER TABLE tfg.[dbo].[Asignaturas_Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Asignaturas_Usuario_IdAsignatura] FOREIGN KEY([IdAsignatura])
	REFERENCES tfg.[dbo].[Asignaturas] ([IdAsignatura])
	ON UPDATE CASCADE
	ON DELETE CASCADE;

ALTER TABLE tfg.[dbo].[Asignaturas_Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Asignaturas_Usuario_IdUsuario] FOREIGN KEY([IdUsuario])
	REFERENCES tfg.[dbo].[Usuarios] ([IdUsuario])
	ON UPDATE CASCADE
	ON DELETE CASCADE;

