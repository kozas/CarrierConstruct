CREATE PROCEDURE [dbo].[spAircraft_Get]
	@SerialNumber int
AS
begin
	select SerialNumber, Manufacturer, Designation, [Name], Squadron, Modex 
	from dbo.Aircraft
	where SerialNumber = @SerialNumber;
end
