CREATE TABLE [dbo].[Aircraft]
(
	[SerialNumber] INT NOT NULL PRIMARY KEY IDENTITY(100001, 1), 
    [Manufacturer] NVARCHAR(50) NOT NULL, 
    [Designation] NVARCHAR(50) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Squadron] NVARCHAR(50) NOT NULL, 
    [Modex] INT NOT NULL
)
