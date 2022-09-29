CREATE TABLE [dbo].[documents] (
    [id]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [code]            VARCHAR (255) NOT NULL,
    [title]           VARCHAR (255) NOT NULL,
    [year]            INT           NOT NULL,
    [sector]          VARCHAR (50)  NOT NULL,
    [available]       TINYINT       NOT NULL,
    [shelf]           VARCHAR (5)   NOT NULL,
    [author]          VARCHAR (100) NOT NULL,
    [type]            VARCHAR (50)  NOT NULL,
    [time]            INT           NULL,
    [number_of_pages] INT           NULL,
    [created_at]      DATETIME      DEFAULT (NULL) NULL,
    [updated_at]      DATETIME      DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);