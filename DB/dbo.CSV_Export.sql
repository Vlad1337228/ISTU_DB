CREATE TABLE [dbo].[CSV_Export] (
    [id]        INT             NOT NULL  PRIMARY KEY,
    [parent_id] INT             NULL,
    [name]      NVARCHAR (max)   NULL,
    [note]      NVARCHAR (max) NULL,
);

