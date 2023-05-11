
ALTER TABLE tfg.[dbo].[Asignaturas_Area]  WITH CHECK ADD  CONSTRAINT [FK_AA_IdAsignatura] FOREIGN KEY([IdAsignatura])
	REFERENCES tfg.[dbo].[Asignaturas] ([IdAsignatura])
	ON UPDATE CASCADE
	ON DELETE CASCADE;

ALTER TABLE tfg.[dbo].[Asignaturas_Area]  WITH CHECK ADD  CONSTRAINT [FK_AA_IdArea] FOREIGN KEY([IdArea])
	REFERENCES tfg.[dbo].[Areas_Conocimiento] ([IdArea])
	ON UPDATE CASCADE
	ON DELETE CASCADE;

