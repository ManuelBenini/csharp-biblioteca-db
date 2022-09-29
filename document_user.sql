CREATE TABLE [dbo].[document_user](
    [id]                    BIGINT PRIMARY KEY IDENTITY NOT NULL,
    [user_id]               BIGINT                      NOT NULL,
    [document_id]           BIGINT                      NOT NULL,
    [loan_start]            DATETIME                    NOT NULL,
    [loan_end]              DATETIME                    NOT NULL,
    [created_at]            DATETIME DEFAULT (NULL)     NULL,
    [updated_at]            DATETIME DEFAULT (NULL)     NULL

    CONSTRAINT [document_user$document_user_user_id_foreign] FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id]),
    CONSTRAINT [document_user$document_user_document_id_foreign] FOREIGN KEY ([document_id]) REFERENCES [dbo].[documents] ([id])
);