CREATE TABLE [dbo].[users] (
    [id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [name]       VARCHAR (255) NOT NULL,
    [surname]    VARCHAR (255) NOT NULL,
    [email]      VARCHAR (255) NOT NULL,
    [password]   VARCHAR (50)  NOT NULL,
    [phone]      BIGINT        NOT NULL,
    [created_at] DATETIME      DEFAULT (NULL) NULL,
    [updated_at] DATETIME      DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);