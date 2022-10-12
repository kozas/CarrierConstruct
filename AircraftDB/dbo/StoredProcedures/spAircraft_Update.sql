CREATE PROCEDURE [dbo].[spAircraft_Update]
	@SerialNumber int,
	@Manufacterer nvarchar(50),
	@Designation nvarchar(50),
	@Name nvarchar(50),
	@Squadron nvarchar(50),
	@Modex int
AS
begin
	update.dbo.Aircraft
	set Manufacturer = @Manufacterer, Designation = @Designation, [Name] = @Name, Squadron = @Squadron, Modex = @Modex
	where SerialNumber = @SerialNumber;
end
