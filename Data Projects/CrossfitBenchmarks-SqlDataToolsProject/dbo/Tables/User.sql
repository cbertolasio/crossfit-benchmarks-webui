CREATE TABLE [dbo].[User] (
    [UserId]           INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]        NVARCHAR (100) NOT NULL,
    [LastName]         NVARCHAR (100) NOT NULL,
    [TimeZoneOffset]   INT            NOT NULL,
    [Email]            NVARCHAR (255) NOT NULL,
    [IpName]           NVARCHAR (255) NULL,
    [IpNameIdentifier] VARCHAR (255)  NULL,
    [IpEmail]          NVARCHAR (100) NULL,
    [IdentityProvider] VARCHAR (100)  NULL,
    [PromoCode]        VARCHAR (50)   NULL,
    [DateCreated]      DATETIME2 (7)  CONSTRAINT [DF_User_DateCreated] DEFAULT (getutcdate()) NULL,
    [LastActivityDate] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_User] PRIMARY KEY NONCLUSTERED ([UserId] ASC),
    CONSTRAINT [IX_User] UNIQUE CLUSTERED ([IpNameIdentifier] ASC, [IdentityProvider] ASC)
);

