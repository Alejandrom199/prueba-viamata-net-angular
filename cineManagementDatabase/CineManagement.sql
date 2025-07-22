-- Creación de la base de datos
CREATE DATABASE CineManagement;

USE CineManagement;

CREATE TABLE pelicula (
    PeliculaId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500),
    Duracion INT,
    Imagen NVARCHAR(MAX),
    EsActivo BIT DEFAULT 1,
);

CREATE TABLE sala_cine (
    SalaId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Estado INT,
    EsActivo BIT DEFAULT 1
);

CREATE TABLE pelicula_sala_cine (
    PeliculaSalaCineId INT PRIMARY KEY IDENTITY(1,1),
    PeliculaId INT NOT NULL,
    SalaId INT NOT NULL,
    FechaPublicacion DATETIME NOT NULL,
    FechaFin DATETIME NOT NULL,
    
    FOREIGN KEY (PeliculaId) REFERENCES pelicula(PeliculaId),
    FOREIGN KEY (SalaId) REFERENCES sala_cine(SalaId),
    
    CONSTRAINT CHK_Fechas CHECK (FechaFin > FechaPublicacion)
);

GO
CREATE PROCEDURE sp_BuscarPeliculaPorNombre
    @Nombre NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.PeliculaId,
        p.Nombre,
        p.Descripcion,
        p.Duracion,
        p.Imagen,
        COUNT(psc.SalaId) AS CantidadSalasAsignadas
    FROM 
        pelicula p
    LEFT JOIN 
        pelicula_sala_cine psc ON p.PeliculaId = psc.PeliculaId
    WHERE 
        p.Nombre LIKE '%' + @Nombre + '%' AND p.EsActivo = 1
    GROUP BY 
        p.PeliculaId, p.Nombre, p.Descripcion, p.Duracion, p.Imagen;
END;
GO

GO
CREATE PROCEDURE sp_ObtenerPeliculasPorFechaPublicacion
    @FechaPublicacion DATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validar que la fecha no sea futura
    IF @FechaPublicacion > GETDATE()
    BEGIN
        RAISERROR('La fecha de publicación no puede ser futura', 16, 1);
        RETURN;
    END
    
    SELECT 
        p.PeliculaId,
        p.Nombre,
        p.Descripcion,
        p.Duracion,
        p.Imagen,
        s.Nombre AS SalaNombre,
        s.Estado AS EstadoSala,
        psc.FechaPublicacion,
        psc.FechaFin
    FROM 
        pelicula p
    JOIN 
        pelicula_sala_cine psc ON p.PeliculaId = psc.PeliculaId
    JOIN 
        sala_cine s ON psc.SalaId = s.SalaId
    WHERE 
        CAST(psc.FechaPublicacion AS DATE) = @FechaPublicacion
        AND p.EsActivo = 1
        AND s.EsActivo = 1
    ORDER BY 
        psc.FechaPublicacion;
END;
GO