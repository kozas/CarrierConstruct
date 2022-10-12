CREATE PROCEDURE [dbo].[spAircraft_Delete]
	@SerialNumber int
AS
begin
	delete
	from dbo.Aircraft
	where SerialNumber = @SerialNumber;
end
