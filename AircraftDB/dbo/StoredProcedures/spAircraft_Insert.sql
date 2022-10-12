CREATE PROCEDURE [dbo].[spAircraft_Insert]
	@Manufacterer nvarchar(50),
	@Designation nvarchar(50),
	@Name nvarchar(50),
	@Squadron nvarchar(50),
	@Modex int
AS
begin
	insert into dbo.Aircraft (Manufacturer, Designation, [Name], Squadron, Modex)
	values (@Manufacterer, @Designation, @Name, @Squadron, @Modex);
end
