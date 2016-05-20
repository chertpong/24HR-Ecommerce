EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
DECLARE @sql NVARCHAR(max)=''
SELECT @sql += ' Drop table [' + TABLE_SCHEMA + '].['+ TABLE_NAME + ']'
FROM   INFORMATION_SCHEMA.TABLES
WHERE  TABLE_TYPE = 'BASE TABLE'

Exec Sp_executesql @sql 