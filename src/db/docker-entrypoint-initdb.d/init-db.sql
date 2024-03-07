IF (SCHEMA_ID('dist_cache') IS NULL) 
BEGIN
    EXEC ('CREATE SCHEMA [dist_cache] AUTHORIZATION [dbo]')
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dist_cache].[DataProtectionKeys]') AND type in (N'U'))
    CREATE TABLE dist_cache.DataProtectionKeys(  
        Id int IDENTITY(1,1) not null,
        FriendlyName nvarchar(MAX) null,
        [Xml] nvarchar(MAX) null
    )
GO
