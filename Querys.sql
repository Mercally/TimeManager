
DECLARE @fechaEntrada VARCHAR(10) = '20180101';
DECLARE @fechaSalida VARCHAR(10) = '20180201';

SELECT Id, NumeroBoleta, FechaEntrada, HoraEntrada, FechaSalida, HoraSalida, TiempoEfectivo, TiempoInvertidoEn, ProyectoId, ClienteId, FechaRegistro, UsuarioId, DepartamentoId, EsActivo 
FROM com.Boleta 
WHERE (FechaEntrada >= @fechaEntrada AND FechaSalida < @fechaSalida) AND EsActivo=1;

Select * from com.boleta