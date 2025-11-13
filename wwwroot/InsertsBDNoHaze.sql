USE [NoHaze];
GO

/* ==============================================
   🔹 ACTUALIZACIÓN DE USUARIOS EXISTENTES
   ============================================== */
UPDATE [dbo].[Usuarios]
SET monedas = 50, racha = 3, ultimoIngreso = CAST('2025-11-13' AS date)
WHERE ID IN (1, 2);
GO

/* ==============================================
   🔹 APPS DE OCIO
   ============================================== */
SET IDENTITY_INSERT [dbo].[AppsOceo] ON;

INSERT INTO [dbo].[AppsOceo] (ID, titulo, foto) VALUES 
(1, 'Study Companion', 'study.jpg'),
(2, 'Focus Master', 'focus.jpg'),
(3, 'Deep Sleep Aid', 'sleep.jpg');

SET IDENTITY_INSERT [dbo].[AppsOceo] OFF;
GO

/* Asociación Apps ↔ Usuarios */
INSERT INTO [dbo].[App X Usuario] (IDUsuario, IDApp) VALUES
(1, 1), (1, 2), (1, 3),
(2, 1), (2, 2), (2, 3);
GO

/* ==============================================
   🔹 FRECUENCIAS BASE
   ============================================== */
SET IDENTITY_INSERT [dbo].[Frecuencias] ON;

INSERT INTO [dbo].[Frecuencias] (ID, titulo, autor, frecuencia) VALUES
(1, '100Hz (Study)', 'NoHaze Team', '100hz'),
(2, '432Hz (Focus)', 'NoHaze Team', '432hz'),
(3, '528Hz (Sleep)', 'NoHaze Team', '528hz'),
(4, '639Hz (Relax)', 'NoHaze Team', '639hz'),
(5, '741Hz (Healing)', 'NoHaze Team', '741hz'),
(6, '852Hz (Clarity)', 'NoHaze Team', '852hz'),
(7, '174Hz (Deep Focus)', 'NoHaze Team', '174hz'),
(8, '285Hz (Recovery)', 'NoHaze Team', '285hz'),
(9, '396Hz (Calm)', 'NoHaze Team', '396hz');

SET IDENTITY_INSERT [dbo].[Frecuencias] OFF;
GO

/* ==============================================
   🔹 PLAYLISTS POR USUARIO
   ============================================== */
SET IDENTITY_INSERT [dbo].[Playlists] ON;

-- Usuario 1 (cajaf)
INSERT INTO [dbo].[Playlists] (ID, titulo, autor, IDUsuario, foto) VALUES
(1, '100Hz (Study)', 'NoHaze Team', 1, 'study.jpg'),
(2, '432Hz (Focus)', 'NoHaze Team', 1, 'focus.jpg'),
(3, '528Hz (Sleep)', 'NoHaze Team', 1, 'sleep.jpg');

-- Usuario 2 (Felibeck)
INSERT INTO [dbo].[Playlists] (ID, titulo, autor, IDUsuario, foto) VALUES
(4, '100Hz (Study)', 'NoHaze Team', 2, 'study.jpg'),
(5, '432Hz (Focus)', 'NoHaze Team', 2, 'focus.jpg'),
(6, '528Hz (Sleep)', 'NoHaze Team', 2, 'sleep.jpg');

SET IDENTITY_INSERT [dbo].[Playlists] OFF;
GO

/* ==============================================
   🔹 FRECUENCIAS X PLAYLIST
   ============================================== */
INSERT INTO [dbo].[Frecuencia X Playlist] (IDFrecuencia, IDPlaylist) VALUES
-- Study
(1, 1), (7, 1), (8, 1),
(1, 4), (7, 4), (8, 4),

-- Focus
(2, 2), (6, 2), (5, 2),
(2, 5), (6, 5), (5, 5),

-- Sleep
(3, 3), (4, 3), (9, 3),
(3, 6), (4, 6), (9, 6);
GO

/* ==============================================
   🔹 TAGS Y RELACIONES
   ============================================== */
SET IDENTITY_INSERT [dbo].[Tags] ON;

INSERT INTO [dbo].[Tags] (ID, titulo) VALUES
(1, 'Focus'),
(2, 'Productivity'),
(3, 'Concentration'),
(4, 'Clarity'),
(5, 'Energy'),
(6, 'Flow'),
(7, 'Relax'),
(8, 'Deep Sleep'),
(9, 'Mindfulness');

SET IDENTITY_INSERT [dbo].[Tags] OFF;
GO

/* Relación Tags ↔ Playlists */
INSERT INTO [dbo].[Tag X Playlist] (IDPlaylist, IDTag) VALUES
-- Study
(1, 1), (1, 2), (1, 3),
(4, 1), (4, 2), (4, 3),

-- Focus
(2, 4), (2, 5), (2, 6),
(5, 4), (5, 5), (5, 6),

-- Sleep
(3, 7), (3, 8), (3, 9),
(6, 7), (6, 8), (6, 9);
GO

/* ==============================================
   🔹 INFORMES (últimos 30 días - usuario 1)
   ============================================== */
DECLARE @fecha DATE = DATEADD(DAY, -30, CAST('2025-11-13' AS DATE));
WHILE @fecha <= '2025-11-13'
BEGIN
    -- 50% de probabilidad de generar informes ese día
    IF (ABS(CHECKSUM(NEWID())) % 2 = 0)
    BEGIN
        DECLARE @n INT = (ABS(CHECKSUM(NEWID())) % 3) + 1; -- 1 a 3 informes por día
        DECLARE @i INT = 1;
        WHILE @i <= @n
        BEGIN
            INSERT INTO [dbo].[Informes] (horas, IDUsuario, dia)
            VALUES ((ABS(CHECKSUM(NEWID())) % 5) + 1, 1, @fecha); -- horas entre 1 y 5
            SET @i += 1;
        END
    END
    SET @fecha = DATEADD(DAY, 1, @fecha);
END
GO
