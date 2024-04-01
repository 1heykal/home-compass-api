IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [PhotoUrl] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [BirthDate] date NOT NULL,
        [Gender] nvarchar(max) NULL,
        [Role] nvarchar(max) NULL,
        [EmailVerificationToken] nvarchar(max) NULL,
        [EmailVerificationTokenExpiresAt] datetime2 NULL,
        [PasswordResetToken] nvarchar(max) NULL,
        [PasswordResetTokenExpiresAt] datetime2 NULL,
        [PasswordTokenConfirmed] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Info] (
        [Id] int NOT NULL IDENTITY,
        [Category] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Info] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Resources] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [IsAvailable] bit NOT NULL,
        CONSTRAINT [PK_Resources] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Homeless] (
        [Id] int NOT NULL IDENTITY,
        [CurrentLocation] nvarchar(max) NULL,
        [HealthCondition] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [Skills] nvarchar(max) NULL,
        [FullName] nvarchar(max) NOT NULL,
        [Age] int NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [PhotoUrl] nvarchar(max) NULL,
        [Archived] bit NOT NULL,
        [AdditionalDetails] nvarchar(max) NULL,
        [ReporterId] nvarchar(450) NULL,
        CONSTRAINT [PK_Homeless] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Homeless_AspNetUsers_ReporterId] FOREIGN KEY ([ReporterId]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Missings] (
        [Id] int NOT NULL IDENTITY,
        [DateOfDisappearance] datetime2 NOT NULL,
        [LastKnownLocation] nvarchar(max) NULL,
        [PhysicalDescription] nvarchar(max) NULL,
        [ContactNumber] nvarchar(max) NOT NULL,
        [HomeAddress] nvarchar(max) NULL,
        [FullName] nvarchar(max) NOT NULL,
        [Age] int NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [PhotoUrl] nvarchar(max) NULL,
        [Archived] bit NOT NULL,
        [AdditionalDetails] nvarchar(max) NULL,
        [ReporterId] nvarchar(450) NULL,
        CONSTRAINT [PK_Missings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Missings_AspNetUsers_ReporterId] FOREIGN KEY ([ReporterId]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Posts] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Content] nvarchar(max) NOT NULL,
        [PublisedOn] datetime2 NOT NULL,
        [Archived] bit NOT NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Posts_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [RefreshToken] (
        [ApplicationUserId] nvarchar(450) NOT NULL,
        [Id] int NOT NULL IDENTITY,
        [Token] nvarchar(max) NULL,
        [ExpiresOn] datetime2 NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [RevokedOn] datetime2 NULL,
        CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([ApplicationUserId], [Id]),
        CONSTRAINT [FK_RefreshToken_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Facilities] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Location] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [CategoryId] int NOT NULL,
        [Target] nvarchar(max) NULL,
        [Days] nvarchar(max) NULL,
        [Hours] int NOT NULL,
        [PhotoUrl] nvarchar(max) NULL,
        [ContactInformaton] nvarchar(max) NULL,
        [ContributorId] nvarchar(450) NULL,
        CONSTRAINT [PK_Facilities] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Facilities_AspNetUsers_ContributorId] FOREIGN KEY ([ContributorId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Facilities_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Jobs] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [CategoryId] int NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Salary] decimal(38,18) NOT NULL,
        [Skills] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [Days] nvarchar(max) NULL,
        [Hours] int NOT NULL,
        [ContactInformation] nvarchar(max) NULL,
        [Benefits] nvarchar(max) NULL,
        [ContributorId] nvarchar(450) NULL,
        CONSTRAINT [PK_Jobs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Jobs_AspNetUsers_ContributorId] FOREIGN KEY ([ContributorId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Jobs_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Comments] (
        [Id] int NOT NULL IDENTITY,
        [Content] nvarchar(max) NOT NULL,
        [PostId] int NOT NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comments_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Likes] (
        [UserId] nvarchar(450) NOT NULL,
        [PostId] int NOT NULL,
        CONSTRAINT [PK_Likes] PRIMARY KEY ([UserId], [PostId]),
        CONSTRAINT [FK_Likes_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Likes_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [Reports] (
        [Id] int NOT NULL IDENTITY,
        [Type] nvarchar(max) NOT NULL,
        [Details] nvarchar(max) NULL,
        [Date] datetime2 NOT NULL,
        [Archived] bit NOT NULL,
        [ReporterId] nvarchar(450) NULL,
        [PostId] int NOT NULL,
        CONSTRAINT [PK_Reports] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reports_AspNetUsers_ReporterId] FOREIGN KEY ([ReporterId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Reports_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE TABLE [FacilityResource] (
        [FacilitiesId] int NOT NULL,
        [ResourcesId] int NOT NULL,
        CONSTRAINT [PK_FacilityResource] PRIMARY KEY ([FacilitiesId], [ResourcesId]),
        CONSTRAINT [FK_FacilityResource_Facilities_FacilitiesId] FOREIGN KEY ([FacilitiesId]) REFERENCES [Facilities] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_FacilityResource_Resources_ResourcesId] FOREIGN KEY ([ResourcesId]) REFERENCES [Resources] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Comments_PostId] ON [Comments] ([PostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Facilities_CategoryId] ON [Facilities] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Facilities_ContributorId] ON [Facilities] ([ContributorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_FacilityResource_ResourcesId] ON [FacilityResource] ([ResourcesId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Homeless_ReporterId] ON [Homeless] ([ReporterId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Jobs_CategoryId] ON [Jobs] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Jobs_ContributorId] ON [Jobs] ([ContributorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Likes_PostId] ON [Likes] ([PostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Missings_ReporterId] ON [Missings] ([ReporterId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Posts_UserId] ON [Posts] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Reports_PostId] ON [Reports] ([PostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    CREATE INDEX [IX_Reports_ReporterId] ON [Reports] ([ReporterId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401183914_mssql_migration_547'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240401183914_mssql_migration_547', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401185048_SeedRoles'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    VALUES (N''7ff39983-4acc-4910-8774-41203ddbad5f'', N''Admin'', N''ADMIN'', N''e3d11896-3374-4197-b52d-0afb1ac21660'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401185048_SeedRoles'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    VALUES (N''7d163888-170f-4fd6-9ae9-7af110f2b6dc'', N''User'', N''USER'', N''1241fb7d-0168-4a80-98b4-0c4759362b1d'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240401185048_SeedRoles'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240401185048_SeedRoles', N'8.0.0');
END;
GO

COMMIT;
GO

