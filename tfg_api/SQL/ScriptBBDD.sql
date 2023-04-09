

--Usuarios
 ALTER TABLE tfg.dbo.Usuarios
    ADD CONSTRAINT DF_IdUsuario DEFAULT newsequentialid() FOR IdUsuario;

--PreguntaFormularios
 ALTER TABLE tfg.dbo.PreguntaFormularios
    ADD CONSTRAINT DF_IdPregunta DEFAULT newsequentialid() FOR IdPregunta;

--Asignaturas
 ALTER TABLE tfg.dbo.Asignaturas
    ADD CONSTRAINT DF_IdAsignatura DEFAULT newsequentialid() FOR IdAsignatura;

--Notas
 ALTER TABLE tfg.dbo.Notas
    ADD CONSTRAINT DF_IdNota DEFAULT newsequentialid() FOR IdNota;

 ALTER TABLE tfg.dbo.Notas
   ADD CONSTRAINT FK_Notas_IdUsuario FOREIGN KEY (IdUsuario)
      REFERENCES tfg.dbo.Usuarios (IdUsuario)
      ON DELETE CASCADE
      ON UPDATE CASCADE;

 ALTER TABLE tfg.dbo.Notas
   ADD CONSTRAINT FK_Notas_IdAsignatura FOREIGN KEY (IdAsignatura)
      REFERENCES tfg.dbo.Asignaturas (IdAsignatura)
      ON DELETE CASCADE
      ON UPDATE CASCADE;

--Formularios
 ALTER TABLE tfg.dbo.Formularios
    ADD CONSTRAINT DF_IdFormulario DEFAULT newsequentialid() FOR IdFormulario;

  ALTER TABLE tfg.dbo.Formularios
   ADD CONSTRAINT FK_Formularios_IdUsuario FOREIGN KEY (IdUsuario)
      REFERENCES tfg.dbo.Usuarios (IdUsuario)
      ON DELETE CASCADE
      ON UPDATE CASCADE;  

--RespuestaFormularios
 ALTER TABLE tfg.dbo.RespuestaFormularios
    ADD CONSTRAINT DF_IdRespuesta DEFAULT newsequentialid() FOR IdRespuesta;

 ALTER TABLE tfg.dbo.RespuestaFormularios
   ADD CONSTRAINT FK_RespuestaFormularios_IdPregunta FOREIGN KEY (IdPregunta)
      REFERENCES tfg.dbo.PreguntaFormularios (IdPregunta)
      ON DELETE CASCADE
      ON UPDATE CASCADE;

 ALTER TABLE tfg.dbo.RespuestaFormularios
   ADD CONSTRAINT FK_RespuestaFormularios_IdFormulario FOREIGN KEY (IdFormulario)
      REFERENCES tfg.dbo.Formularios (IdFormulario)
      ON DELETE CASCADE
      ON UPDATE CASCADE;