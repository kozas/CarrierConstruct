CREATE PROCEDURE [dbo].[spAircraft_GetAll]
AS
begin
	select SerialNumber, Manufacturer, Designation, [Name], Squadron, Modex 
	from dbo.Aircraft
end
