use master
GO

--CHEQUEAMOS SI LA BASE DE DATOS EXISTE
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'GestionBancaria')
ALTER DATABASE GestionBancaria SET SINGLE_USER WITH ROLLBACK IMMEDIATE
-- LA LINEA ANTERIOR ELIMINA TODAS LAS CONEXIONES EXISTENTES A LA BASE DE DATOS PARA PODER DROPEARLA
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'GestionBancaria')
DROP DATABASE [GestionBancaria] /* ELIMINAMOS LA BASE DE DATOS */
GO


create database GestionBancaria
GO

use GestionBancaria
GO

create table Usuario(Ci int primary key not null,
					 NombreUsuario nvarchar (15) not null unique,
				     Nombre nvarchar (20) not null,
					 Apellido nvarchar (20) not null,
					 Pass nvarchar (20) not null,
					 Activo bit not null
					 )
GO				 
create table Cliente(IdCliente int primary key not null, 
				     Direccion nvarchar(20),
				     foreign key (IdCliente) references Usuario(Ci))
GO
create table Sucursal(IdSucursal int identity primary key, 
					  Nombre nvarchar(20) not null,
					  Direccion nvarchar(50) not null,
					  Activa bit not null)
GO					 				 
create table Prestamo(IdPrestamo int not null, 
					  NumeroSucursal int not null, 
					  IdCliente int not null,
					  Fecha datetime not null,
					  Cuotas int not null, 
					  Cancelado bit not null,
					  Moneda nvarchar(3) not null,
					  Monto float not null,
					  foreign key (NumeroSucursal) references Sucursal(IdSucursal),
					  foreign key (IdCliente) references Cliente(IdCliente),
					  primary key (IdPrestamo, NumeroSucursal)
					  )
GO					  
create table Cotizacion(Fecha datetime not null,
						PrecioVenta float not null, 
						PrecioCompra float not null
						primary key (Fecha))
					  
					
GO

create table Bitacora(IdBitacora int primary key identity not null, Fecha datetime not null, PrecioVentaViejo float, PrecioCompraViejo float,
					  PrecioVentaNuevo float, PrecioCompraNuevo float, IdUsuario int)
GO--guarda en una bitacora las modificaciones realizadas en Cotizacion
		     
create table TelefonosClientes(IdCliente int, 
							   Tel varchar(50), 
							   primary key(IdCliente,Tel),
							   foreign key(IdCliente) references Cliente(IdCliente))
GO
create table Empleado(IdUsuario int primary key, 
					  IdSucursal int not null,
					  foreign key (IdUsuario) references Usuario(Ci),
					  foreign key (IdSucursal) references Sucursal(IdSucursal))
GO			  
create table Pagos(IdRecibo int identity primary key not null,
				   IdEmpleado int,
				   IdPrestamo int,
				   NumeroSucursal int,
				   Monto float not null,
				   Fecha datetime not null,
				   NumeroCuota int not null,
				   foreign key (IdEmpleado) references Empleado(IdUsuario),
				   foreign key (IdPrestamo,NumeroSucursal) references Prestamo(IdPrestamo,NumeroSucursal))
go
create table Cuenta(IdCuenta int primary key identity (1,1) not null,
					IdSucursal int not null foreign key references Sucursal(IdSucursal),
					Moneda nvarchar(3) not null,
					IdCliente int foreign key references Cliente(IdCliente),
					Saldo float not null,
					FechaApertura datetime not null)
					
					
GO					
create table Movimiento(IdSucursal int not null references Sucursal(IdSucursal),
						NumeroMovimiento int not null,
						primary key (IdSucursal, NumeroMovimiento),
						Tipo int not null,
						Fecha datetime not null,
						Moneda nvarchar(3) not null,
						ViaWeb bit not null,
						Monto float not null,
						IdCuenta int not null references Cuenta(IdCuenta),
						CiUsuario int not null references Usuario(Ci))
						--Si es movimiento web el CiUsuario  se carga con CiCliente. Si es movimiento dentro entidad se carga CiEmpleado
						-- . No es necesario establecer un campo de tipo bit para definir si es un movimiento web o dentro de entidad
					
GO



					/*Ci int primary key not null,
					 NombreUsuario nvarchar (15) not null unique,
				     Nombre nvarchar (20) not null,
					 Apellido nvarchar (20) not null,
					 Pass nvarchar (20) not null
					 )*/


/*create table Cliente(IdCliente int primary key not null, 
				     Direccion nvarchar(20),
				     foreign key (IdCliente) references Usuario(Ci))*/

create proc AltaCliente
@Ci int,
@NombreUsuario nvarchar(15),
@Nombre nvarchar(20),
@Apellido nvarchar(20),
@Pass nvarchar(20),
@Direccion nvarchar(20)
as
BEGIN

begin tran --insertamos el cliente

	if exists(select * from Usuario where Usuario.Ci=@Ci or Usuario.NombreUsuario = @NombreUsuario)
	begin
		return -3   --Usuario ya Existe
	end
 
	insert into Usuario(Ci,Nombre,Apellido,NombreUsuario, Pass, Activo)
			values(@Ci, @Nombre, @Apellido, @NombreUsuario, @Pass, 1)
			
		if @@error<>0
		begin
		rollback tran
			return -1  --Si no se pudo insertar un Usuario--
		end

	insert into Cliente(IdCliente, Direccion)
			values(@Ci, @Direccion)

		if @@error<>0
		begin
		rollback tran
			return -2  --Si no se pudo insertar un Cliente--
		end

commit tran

END
GO

create proc spAltaTelefono
@IdCliente int,
@Tel int
as
begin
	if not exists(select * from Usuario where Usuario.Ci=@IdCliente or Usuario.Activo = 1)
	begin
		return -1   --Usuario no Existe, o está inactivo
	end
	
	insert into TelefonosClientes(IdCliente,Tel)
							values(@IdCliente,@Tel)
	
end

GO

create proc spBajaTelefono
@IdCliente int
as
begin
	if not exists(select * from Usuario where Usuario.Ci=@IdCliente or Usuario.Activo = 1)
	begin
		return -1   --Usuario no Existe, o está inactivo
	end
	
	delete TelefonosClientes where TelefonosClientes.IdCliente=@IdCliente
	
end
GO

create proc spListarTelefonos
@IdCliente int
as
begin
	if not exists(select * from Usuario where Usuario.Ci=@IdCliente or Usuario.Activo = 1)
	begin
		return -1   --Usuario no Existe, o está inactivo
	end
	
	select Tel from TelefonosClientes where TelefonosClientes.IdCliente = @IdCliente
end
GO


/*
create table Empleado(IdUsuario int primary key, 
					  IdSucursal int not null,
					  foreign key (IdUsuario) references Usuario(Ci),
					  foreign key (IdSucursal) references Sucursal(IdSucursal))*/
create proc AltaEmpleado
@Ci int,
@NombreUsuario nvarchar(15),
@Nombre nvarchar(20),
@Apellido nvarchar(20),
@Pass nvarchar(20),
@IdSucursal int
as
BEGIN
if not exists(select * from Sucursal where Sucursal.IdSucursal=@IdSucursal and Sucursal.Activa = 1)
	begin
		return -1   --sucursal no existe en el sistema o está inactiva
	end
	
	if exists(select * from Usuario where Usuario.Ci=@Ci or Usuario.NombreUsuario = @NombreUsuario)
	begin
		return -2   --Usuario ya Existe, quizás esté inactivo
	end
	
	begin tran --insertamos el Empleado
	insert into Usuario(Ci,Nombre,Apellido,NombreUsuario,Pass,Activo)
			values(@Ci, @Nombre, @Apellido, @NombreUsuario, @Pass, 1)
			
		if @@error<>0
		begin
		rollback tran
			return -2  --Si no se pudo insertar un Usuario--
		end

	
	insert into Empleado(IdUsuario, IdSucursal)
			values(@Ci, @IdSucursal)

	if @@error<>0
	begin
    rollback tran
		return -3  --Si no se pudo insertar un Empleado--
	end

commit tran
END
GO

/*create table Sucursal(IdSucursal int identity primary key, 
					  Nombre nvarchar(20) not null,
					  Direccion nvarchar(50))*/
					  
create proc AltaSucursal
@Nombre nvarchar(20),
@Direccion nvarchar(50)
as
BEGIN
if exists(select * from Sucursal where Sucursal.Direccion=@Direccion or Sucursal.Nombre=@Nombre)
	begin
		return -1   --Nombre/direccion de sucursal ya existe en el sistema
	end

begin tran --insertamos la sucursal
	insert into Sucursal(Direccion, Nombre, Activa)
			values(@Direccion, @Nombre, 1)

	if @@error<>0
	begin
    rollback tran
		return -2  --Si no se pudo insertar sucursal--
	end

commit tran
END
GO

/*create table Prestamo(IdPrestamo int not null, 
					  NumeroSucursal int not null, 
					  Fecha datetime not null,
					  Cuotas int not null, 
					  Cancelado bit not null,
					  Moneda nvarchar(3) not null,
					  Monto float not null,
					  foreign key (NumeroSucursal) references Sucursal(IdSucursal),
					  primary key (IdPrestamo, NumeroSucursal)
					  )*/

create proc AltaPrestamo
@NumeroSucursal int,
@IdCliente int,
@Fecha datetime,
@Cuotas int,
@Moneda nvarchar,
@Monto int
as
BEGIN
if not exists(select * from Sucursal where Sucursal.IdSucursal=@NumeroSucursal and Sucursal.Activa = 1)
	begin
		return -1   --sucursal no existe en el sistema o está inactiva
	end
	
if not exists(select * from Usuario where Usuario.Ci=@IdCliente and Usuario.Activo = 1)
	begin
		return -2   --Usuario no existe en el sistema o está inactivo
	end
	
if not exists(select * from Cliente where Cliente.IdCliente=@IdCliente)
	begin
		return -3   --cliente no existe en el sistema
	end
	
declare @cantidad int
select @cantidad = COUNT(*) from Prestamo where Prestamo.NumeroSucursal=@NumeroSucursal
set @cantidad = @cantidad+1 /*numero del nuevo prestamo*/

insert into Prestamo(IdPrestamo, NumeroSucursal, IdCliente, Fecha, Cuotas, Moneda, Monto, Cancelado)
			values(@cantidad, @NumeroSucursal, @IdCliente, @Fecha, @Cuotas, @Moneda, @Monto, 0)
			
	if @@error<>0
	begin
		return -4  --Si no se pudo insertar prestamo--
	end


END
GO

/********OPCIONAL********/
create proc CantidadPrestamos /*Si quisiera en algun momento saber la cantidad de prestamos de una sucursal*/
@NumeroSucursal int
as
begin
	declare @cantidad int
	select @cantidad = COUNT(*) from Prestamo where NumeroSucursal = @NumeroSucursal
	return @cantidad

end
GO
/**************************/

/*
create table Movimiento(IdSucursal int not null references Sucursal(IdSucursal),
						NumeroMovimiento int not null,
						primary key (IdSucursal, NumeroMovimiento),
						Tipo int not null,
						Fecha datetime not null,
						Moneda nvarchar(3) not null,
						ViaWeb bit not null,
						Monto float not null,
						IdCuenta int not null references Cuenta(IdCuenta),
						CiUsuario int not null references Usuario(Ci))
						--Si es movimiento web el CiUsuario  se carga con CiCliente. Si es movimiento dentro entidad se carga CiEmpleado
						-- . No es necesario establecer un campo de tipo bit para definir si es un movimiento web o dentro de entidad
						*/

create proc [dbo].[AltaMovimiento] /*podemos controlar lo de la cotizacion fuera del script*/
@IdSucursal int,
@Tipo int,
@Fecha datetime,
@Moneda nvarchar(3),
@ViaWeb bit,
@Monto float,
@CiUsuario int,
@IdCuenta int
as
BEGIN
if not exists(select * from Sucursal where Sucursal.IdSucursal=@IdSucursal and Sucursal.Activa = 1)
	begin
		return -1   --sucursal no existe en el sistema o está inactiva
	end
	
if not exists(select * from Usuario where Usuario.Ci=@CiUsuario and Usuario.Activo = 1)
	begin
		return -2   --Usuario no existe en el sistema o está inactivo
	end
	
declare @cantidad int
select @cantidad = COUNT(*) from Movimiento where Movimiento.IdSucursal=@IdSucursal
set @cantidad = @cantidad+1 /*numero del nuevo movimiento*/

	begin tran
	--guardamos el movimiento
	insert into Movimiento(IdSucursal,NumeroMovimiento,Tipo,Fecha,Moneda,ViaWeb,Monto,IdCuenta,CiUsuario)
			values(@IdSucursal, @cantidad, @Tipo, @Fecha, @Moneda,@ViaWeb,@Monto,@IdCuenta,@CiUsuario)
			
	if @@ERROR <> 0
	begin	
		rollback tran		
		return -3
	end
	
	--Distinguimos si sumamos o restamos el saldo de la cuenta
	declare @saldo float
	select @saldo = Saldo from Cuenta where Cuenta.IdCuenta = @IdCuenta
	
	if @@ERROR <> 0
	begin	
		rollback tran		
		return -3
	end
	
	if (@Tipo = 2)
	begin
		set @saldo = @saldo + @monto
		end
	else
	begin
		set @saldo = @saldo - @monto
	end
		
	update Cuenta set Saldo = @saldo where Cuenta.IdCuenta = @IdCuenta
			
	if @@error<>0
	begin
		rollback tran		
		return -3  --Si no se actualiza el saldo--
	end

commit tran
END
GO
/*
create table Cotizacion(
						Fecha datetime not null,
						PrecioVenta float not null, 
						PrecioCompra float not null
						primary key (Fecha))*/
						
create proc AltaCotizacion
@Fecha datetime,
@PrecioVenta float,
@PrecioCompra float
as
BEGIN
if exists(select * from Cotizacion where Cotizacion.Fecha = @Fecha)
	begin
		return -1   --Ya se insertó una cotización en el día @Fecha
	end

insert into Cotizacion(Fecha,PrecioCompra,PrecioVenta)
			values(@Fecha,@PrecioCompra,@PrecioVenta)
			
	if @@error<>0
	begin
		return -2  --Si no se pudo insertar la cotización--
	end

END
GO

create proc CancelarPrestamo
@IdSucursal int,
@NumeroPrestamo int
as
BEGIN

	update Prestamo set Cancelado=1 where Prestamo.IdPrestamo=@NumeroPrestamo and Prestamo.NumeroSucursal = @IdSucursal
	if @@error<>0
	begin
		return -3  --Si no se pudo insertar el prestamo--
	end
END
GO
/*create table Pagos(IdRecibo int identity primary key not null,
				   IdEmpleado int,
				   IdPrestamo int,
				   NumeroSucursal int,
				   Monto float not null,
				   Fecha datetime not null,
				   NumeroCuota int not null,
				   foreign key (IdEmpleado) references Empleado(IdUsuario),
				   foreign key (IdPrestamo,NumeroSucursal) references Prestamo(IdPrestamo,NumeroSucursal))*/
CREATE PROC AltaPago
@IdEmpleado int,
@IdPrestamo int,
@NumeroSucursal int,
@Monto float,
@Fecha datetime
as
BEGIN
	declare @NumeroCuota int=0
	select @NumeroCuota = Pagos.NumeroCuota from Pagos where Pagos.IdPrestamo=@IdPrestamo
	set @NumeroCuota = @NumeroCuota+1 /*numero de la nueva cuota a pagar*/
	
if not exists(select * from Sucursal where Sucursal.IdSucursal=@NumeroSucursal and Sucursal.Activa = 1)
	begin
		return -1   --sucursal no existe en el sistema o está inactiva
	end
	
if not exists(select * from Usuario where Usuario.Ci=@IdEmpleado and Usuario.Activo = 1)
	begin
		return -2   --Usuario no existe en el sistema o está inactivo
	end
	

	insert into Pagos(IdEmpleado,IdPrestamo,Fecha,Monto,NumeroCuota,NumeroSucursal)
			values(@IdEmpleado,@IdPrestamo,@Fecha,@Monto,@NumeroCuota,@NumeroSucursal)
		
		if @@ERROR <> 0
		begin
			return -3 /*error no se pudo insertar pago*/
		end
		
		return @@identity /*retorno el numero de IdRecibo*/

END

GO
-- LOGINS
create proc spLoginCliente
@Usuario as nvarchar(15),
@Pass as nvarchar(20)
as
begin
	
	select * from Usuario inner join Cliente on Usuario.Ci = Cliente.IdCliente
	where Usuario.NombreUsuario = @Usuario and Usuario.Pass = @Pass
	
end
GO

create proc spLoginEmpleado
@Usuario as nvarchar(15)
as
begin
	
	select Usuario.*, Sucursal.Activa as SucursalActiva, Sucursal.Direccion as DireccionSucursal, Sucursal.IdSucursal,
	Sucursal.Nombre as NombreSucursal from Usuario inner join Empleado on Usuario.Ci = Empleado.IdUsuario
	inner join Sucursal on Empleado.IdSucursal = Sucursal.IdSucursal
	where Usuario.NombreUsuario = @Usuario
	
end
GO

--END LOGINS

--muestra el id prestamo sin cancelar y el último realizado para dicho prestamo
CREATE PROC spListarUltimosPagos 
@IdSucursal int
as
BEGIN

	if not exists(select * from Sucursal where IdSucursal=@IdSucursal and Activa = 1)
	begin
		return -1 --Sucursal no existe o está inactiva
	end
	select * from Prestamo INNER JOIN
		(select IdPrestamo, MAX(Pagos.Fecha) maxfecha from Pagos group by IdPrestamo) MP on Prestamo.IdPrestamo = MP.IdPrestamo 
		INNER JOIN Pagos on Pagos.IdPrestamo=MP.IdPrestamo and Pagos.Fecha = MP.maxfecha	
		where Prestamo.Cancelado = 0 and Prestamo.NumeroSucursal = @IdSucursal
END
GO


CREATE PROC spListarPrestamos
@Cancelado as bit,
@IdSucursal as bit
as
begin
select * from Prestamo where Prestamo.Cancelado = @Cancelado
and Prestamo.NumeroSucursal = @IdSucursal
end

GO

CREATE PROC spListarUltimoPagoPrestamo
@IdPrestamo as bit
as
begin
select top 1 * from Pagos 
where Pagos.IdPrestamo = @IdPrestamo
order by Fecha desc 
end


GO
/*
CREATE PROC spListarAtrasados --sin terminar
@IdSucursal int,
@FechaActual datetime
as
BEGIN

select Prestamo.IdPrestamo from Pagos,Prestamo where 
												(((DATEPART(day,Prestamo.Fecha) - DATEPART(day,@FechaActual)<0) 
													and (DATEPART(month,Pagos.Fecha) - DATEPART(month,@FechaActual)=1) 
													or ((DATEPART(month,Pagos.Fecha) - DATEPART(month,@FechaActual)<1)))
												and (Pagos.NumeroSucursal=@IdSucursal) and (Prestamo.Cancelado=0))

/*Condiciones que se deben cumplir para mostar los prestamos atrasados de pago:

1-el prestamo fue pedido un dia X, por ejemplo 12, 
y estamos a un 13 y hay UN MES de diferencia entre pago anterior y fecha actual,
en este caso estaría atrasdo 1 día

2-Si hay más de un mes de diferencia entre el último pago y el día de hoy, 
ejemplo: ultimo pago 11 de julio, dia actual 10 de diciembre, esta muy atrasado.

3-el prestamo no esta cancelado, y el prestamo es de la IdSucursal pasada por parametro
condiciones del sp*/ 

END
GO*/


CREATE PROC spBuscarClientePorCi -- falta filtrar inactivos, sería agregando and Usuario.Activo=0
@Ci as int
as
begin
select Usuario.*, Cliente.Direccion from Usuario INNER JOIN Cliente ON Usuario.Ci = Cliente.IdCliente
	where Cliente.IdCliente = @Ci
end

go 

create procedure spListarClientes
@Activo bit
as
begin
select Usuario.* ,Cliente.Direccion from Usuario INNER JOIN Cliente ON Usuario.Ci = Cliente.IdCliente 
where Activo = @Activo
end

GO 


/*Bajas, de empleado, cliente, usuario, desactivacion de sucursal, */


/********************************Empleado y Cliente*******************************/

create proc spModificarCliente
@Ci int,
@NombreUsuario nvarchar(15),
@Nombre nvarchar(20),
@Apellido nvarchar(20),
@Pass nvarchar(20),
@Direccion nvarchar(20)
as
begin
	begin tran
		update Cliente set cliente.Direccion=@Direccion where cliente.IdCliente=@Ci
		if @@error<>0
		begin
		rollback tran
			return -2  --Si no se pudo cambiar datos--
		end
		update Usuario set Usuario.Apellido=@Apellido, Usuario.Nombre=@Nombre, 
				Usuario.NombreUsuario=@NombreUsuario, Usuario.Pass=@Pass where Usuario.Ci=@Ci
		if @@error<>0
		begin
		rollback tran
			return -2  --Si no se pudo cambiar datos--
		end
	commit tran
end
GO

create proc spModificarEmpleado
@Ci int,
@NombreUsuario nvarchar(15),
@Nombre nvarchar(20),
@Apellido nvarchar(20),
@Pass nvarchar(20),
@IdSucursal int
as
begin
	begin tran
		update Empleado set Empleado.IdSucursal=@IdSucursal where Empleado.IdUsuario=@Ci
		if @@error<>0
		begin
		rollback tran
			return -2  --Si no se pudo cambiar datos--
		end
		update Usuario set Usuario.Apellido=@Apellido, Usuario.Nombre=@Nombre, 
				Usuario.NombreUsuario=@NombreUsuario, Usuario.Pass=@Pass where Usuario.Ci=@Ci
		if @@error<>0
		begin
		rollback tran
			return -2  --Si no se pudo cambiar datos--
		end
	commit tran
end
GO

create proc spEliminarCliente --chequear si tiene deudas con el banco/prestamos etc.
@Ci int
as
begin
	update Usuario set Activo = 0 where Usuario.Ci = @Ci
end
GO

create proc spBuscarEmpleado
@Ci int
as
begin
	select Usuario.*, IdSucursal from Usuario 
	inner join Empleado on Usuario.Ci = Empleado.IdUsuario
	where Ci=@Ci 
end
GO

create proc spListarEmpleado
@Activo bit
as
begin
	select * from Empleado inner join Usuario on Usuario.Ci=Empleado.IdUsuario and Activo=@Activo
end
GO

create proc spEliminarEmpleado
@Ci int
as
begin
	update Usuario set Activo = 0 where Usuario.Ci = @Ci
end
GO

/********************************SUCURSAL*******************************/
/********************************SUCURSAL*******************************/
/********************************SUCURSAL*******************************/
/********************************SUCURSAL*******************************/



create proc spListarSucursal
as
begin
	select * from Sucursal --opcional: filtrar por activa o inactiva
							--opcional: elegir todos los campos o solo id,.
end
GO

create proc spBuscarSucursal
@IdSucursal int
as
begin
	select * from Sucursal where Sucursal.IdSucursal=@IdSucursal
end
GO

create proc spBuscarSucursalPorCi
@IdEmpleado int
as
begin
	select * from Sucursal inner join Empleado on Empleado.IdSucursal=Sucursal.IdSucursal and Empleado.IdUsuario=@IdEmpleado
end
GO

create proc spEliminarSucursal
@IdSucursal int
as
begin
	update Sucursal set Activa=0 where Sucursal.IdSucursal=@IdSucursal
end
GO

create proc spModificarSucursal
@IdSucursal int,
@Nombre nvarchar(20), --verificar si es 20
@Direccion nvarchar(20) --    ''     ''
as
begin
	update Sucursal set Nombre=@Nombre, Direccion=@Direccion where Sucursal.IdSucursal=@IdSucursal
end
GO
/********************************CUENTA*******************************/
/********************************CUENTA*******************************/
/********************************CUENTA*******************************/
/********************************CUENTA*******************************/

create proc spListarCuenta
as
begin
	select *, Usuario.* from Cuenta inner join Cliente on Cuenta.IdCliente = Cliente.IdCliente
	inner join Usuario on Cuenta.IdCliente = Usuario.Ci
end
GO

create proc spBuscarCuenta
@IdCuenta int
as
begin
if not exists(select * from Cuenta where Cuenta.IdCuenta=@IdCuenta)
	begin
		return -1   --Cuenta no existe en el sistema o está inactiva
	end
	select Cuenta.*, Usuario.Nombre, Usuario.Apellido from Cuenta
	inner join Usuario on Cuenta.IdCliente = Usuario.Ci
	where Cuenta.IdCuenta=@IdCuenta
end
GO

create proc spBuscarCuentaPorCi
@Ci int
as
begin
if not exists(select * from Usuario where Usuario.Ci = @Ci and Usuario.Activo = 1)
	begin
		return -1   --Usuario no existe en el sistema o está inactiva
	end
	select Cuenta.*, Usuario.*, Cliente.Direccion  from Cuenta inner join Usuario on Usuario.CI = Cuenta.IdCliente
	inner join Cliente on Cuenta.IdCliente = Cliente.IdCliente and Cuenta.IdCliente=@Ci
end
GO

/*create table Cuenta(IdCuenta int primary key identity (1,1) not null,
					IdSucursal int not null foreign key references Sucursal(IdSucursal),
					Moneda nvarchar(3) not null,
					IdCliente int foreign key references Cliente(IdCliente),
					Saldo float not null)*/
create proc spAltaCuenta
@IdSucursal int,
@Moneda nvarchar(3),
@IdCliente int,
@Saldo float,
@FechaApertura datetime
as
begin
if not exists(select * from Sucursal where Sucursal.IdSucursal=@IdSucursal and Sucursal.Activa = 1)
	begin
		return -1   --sucursal no existe en el sistema o está inactiva
	end
	
if not exists(select * from Usuario where Usuario.Ci=@IdCliente and Usuario.Activo = 1)
	begin
		return -2   --Usuario no existe en el sistema o está inactivo
	end
	
	insert into Cuenta(IdCliente,IdSucursal,Moneda,Saldo,FechaApertura) 
				values(@IdCliente,@IdSucursal,@Moneda,@Saldo,@FechaApertura)
	
end
GO

create proc spModificarCuenta
@IdCuenta int,
@Saldo float,
@IdSucursal int,
@Moneda nvarchar(3)
as
begin
	if not exists (select * from Cuenta where Cuenta.IdCuenta=@IdCuenta)
	begin
		return -1 --cuenta no existe
	end	
	
	update Cuenta set Saldo=@Saldo, IdSucursal=@IdSucursal, Moneda=@Moneda where Cuenta.IdCuenta=@IdCuenta
end
GO

create proc spEliminarCuenta
@IdCuenta int
as
begin
	if not exists (select * from Cuenta where Cuenta.IdCuenta=@IdCuenta)
	begin
		return -1 --cuenta no existe
	end
	if not exists (select * from Cuenta where Cuenta.IdCuenta=@IdCuenta and Cuenta.Saldo=0)
	begin
		return -1 --cuenta tiene saldo mayor a cero, se debe vaciar la cuenta antes de eliminarla
	end
	begin tran
	
		delete from Movimiento where Movimiento.IdCuenta=@IdCuenta
		if @@error<>0
		begin
		rollback tran
			return -2  --Si no se pudo borrar datos--
		end
		delete from Cuenta where Cuenta.IdCuenta=@IdCuenta
		if @@error<>0
		begin
		rollback tran
			return -3  --Si no se pudo borrar datos--
		end
		
	commit tran
end
go

/********************************COTIZACION*******************************/
/********************************COTIZACION*******************************/
/********************************COTIZACION*******************************/
/********************************COTIZACION*******************************/


create proc spListarCotizacion
@FechaInicio datetime,
@FechaFin datetime
as
begin
	--select * from Cotizacion where cotizacion.Fecha between @FechaFin and @FechaInicio
	select * from Cotizacion where cotizacion.Fecha <= @FechaFin and cotizacion.Fecha >= @FechaInicio
end
GO

create proc spBuscarCotizacion
@Fecha datetime
as
begin
	select * from Cotizacion where dbo.DateTimeToString(cotizacion.Fecha) = dbo.DateTimeToString(@Fecha)
end
GO


create proc spModificarCotizacion
@Fecha datetime,
@IdUsuario int,
@PrecioCompra float,
@PrecioVenta float
as
begin
if not exists(select * from Usuario where Usuario.Ci=@IdUsuario and Usuario.Activo = 1)
	begin
		return -1   --Usuario no existe en el sistema o está inactivo
	end
declare @PrecioCompraViejo float
declare @PrecioVentaViejo float
select @PrecioCompraViejo = Cotizacion.PrecioCompra from Cotizacion where Cotizacion.Fecha=@Fecha
select @PrecioVentaViejo = Cotizacion.PrecioVenta from Cotizacion where Cotizacion.Fecha=@Fecha

	begin tran
		insert into Bitacora(Fecha,IdUsuario,PrecioCompraNuevo,PrecioVentaNuevo,PrecioCompraViejo,PrecioVentaViejo) 
					  values(@Fecha, @IdUsuario,@PrecioCompra,@PrecioVenta,@PrecioCompraViejo,@PrecioVentaViejo)
			
		if @@error<>0
		begin
		rollback tran
			return -2  --Si no se pudo cambiar datos--
		end
		
		update Cotizacion set Cotizacion.PrecioCompra=@PrecioCompra,Cotizacion.PrecioVenta=@PrecioVenta where Cotizacion.Fecha=@Fecha
		
		if @@error<>0
		begin
		rollback tran
			return -3  --Si no se pudo cambiar datos--
		end
end
GO

create proc spEliminarCotizacion  --Limitar derecho de ejecución a solo administradores.
@Fecha datetime
as
begin
if not exists(select * from Cotizacion where Cotizacion.Fecha=@Fecha)
	begin
		return -1 --no existe cotización para tal fecha
	end
	
	delete Cotizacion where Cotizacion.Fecha = @Fecha
	
end
GO
	
create proc spPagoPrestamo --ver sp alta pago
@IdEmpleado as int,
@IdPrestamo as int,
@NumeroSucursal as int,
@Monto as float,
@Fecha as datetime
as
begin
	declare @numCuota as int
	SET @numCuota = (select COUNT(*) from Pagos where IdPrestamo = @IdPrestamo) + 1
	insert into Pagos (IdEmpleado,IdPrestamo,NumeroSucursal,Monto,Fecha,NumeroCuota) values (@IdEmpleado,@IdPrestamo,
	@NumeroSucursal,@Monto,@Fecha,@numCuota)
	return @@error
end
GO

create proc spBuscarPrestamo
@IdPrestamo int
as
begin
	
	select Prestamo.*, Usuario.Nombre, Usuario.Apellido from prestamo
	inner join Usuario on Prestamo.IdCliente = Usuario.Ci
	 where prestamo.IdPrestamo=@IdPrestamo

end
GO

create proc spArqueoCaja
@Fecha datetime,
@Moneda nvarchar(3),
@IdEmpleado int
as
begin
	declare @Pagos as int
	declare @Depositos as int
	declare @Retiros as int
	declare @ArqueoTotal as int
	declare @Sucursal as int
	
	set @Sucursal = (select Empleado.IdSucursal from Empleado where Empleado.IdUsuario=@IdEmpleado)
	
	set @Pagos = (select SUM(Pagos.Monto) from Pagos inner join Prestamo on Prestamo.IdPrestamo=Pagos.IdPrestamo where Prestamo.Moneda=@Moneda and Pagos.Fecha = @Fecha and Pagos.NumeroSucursal=@Sucursal and Pagos.IdEmpleado=@IdEmpleado)
	set @Depositos = (select SUM(Movimiento.Monto) from Movimiento where Movimiento.Fecha = @Fecha and Movimiento.Tipo=0 and Movimiento.Moneda=@Moneda and Movimiento.IdSucursal=@Sucursal and Movimiento.CiUsuario=@IdEmpleado)
	set @Retiros = (select SUM(Movimiento.Monto) from Movimiento where Movimiento.Fecha = @Fecha and Movimiento.Tipo = 1 and Movimiento.Moneda=@Moneda and Movimiento.IdSucursal=@Sucursal and Movimiento.CiUsuario=@IdEmpleado)
	set @ArqueoTotal = @Pagos+@Depositos-@Retiros --resto los retiros, sumo pagos y depositos.
	
	return @ArqueoTotal  --devuelvo el resultado.

end
GO

create proc spListadoProductividadComparativo
@FechaInicio as datetime,
@FechaFin as datetime
as
begin
	select Sucursal.Nombre, Sucursal.Direccion, CantidadCuentas.cantCuentas as CantCuentasAbiertas,
CantidadPrestamos.cantPrestamos as CantPrestamosOtorgados from Sucursal 
left outer join (
select Count(Cuenta.IdCuenta) as cantCuentas, Cuenta.IdSucursal from Cuenta
where @FechaInicio <= Cuenta.FechaApertura and Cuenta.FechaApertura <= @FechaFin
group by Cuenta.IdSucursal) as CantidadCuentas on Sucursal.IdSucursal = CantidadCuentas.IdSucursal
left outer join (
select Count(Prestamo.IdPrestamo) as cantPrestamos, Prestamo.NumeroSucursal from Prestamo
where @FechaInicio <= Prestamo.Fecha and Prestamo.Fecha <= @FechaFin
group by Prestamo.NumeroSucursal) as CantidadPrestamos on Sucursal.IdSucursal = CantidadPrestamos.NumeroSucursal

where Sucursal.Activa = 1
end

GO

CREATE FUNCTION DateTimeToString (@DateTimeValue DateTime) RETURNS nvarchar(10)
AS
BEGIN
 return LTRIM(STR(MONTH(@DateTimeValue))) + '/' + LTRIM(STR(DAY(@DateTimeValue))) + '/' + LTRIM(STR(YEAR(@DateTimeValue))) 
END

GO

create proc spTotalesArqueoCaja
@IdEmpleado as int,
@IdSucursal as int,
@CantTotalDepositos as int output,
@CantTotalRetiros as int output,
@CantTotalPagos as int output
as
begin
 
	--suponiendo el server de base de datos se encuentra en el mismo uso horario que el empleado que ejecuta el arqueo
	select @CantTotalPagos = COUNT(*) from Pagos 
	where dbo.DateTimeToString(Pagos.Fecha) = dbo.DateTimeToString(GETDATE()) and Pagos.IdEmpleado = @IdEmpleado
	
	
	select @CantTotalRetiros = COUNT(*) from Movimiento 
	where dbo.DateTimeToString(Movimiento.Fecha) = dbo.DateTimeToString(GETDATE()) and Movimiento.CiUsuario = @IdEmpleado
	and Movimiento.Tipo = 1
	
	
	select @CantTotalDepositos = COUNT(*) from Movimiento
	where dbo.DateTimeToString(Movimiento.Fecha) = dbo.DateTimeToString(GETDATE()) and Movimiento.CiUsuario = @IdEmpleado
	and Movimiento.Tipo = 2
	
end

GO

create proc spModificarContraseña
@CiUsuario as int,
@PasswordActual as nvarchar(20),
@PasswordNuevo as nvarchar(20)
as
begin
	if exists (select * from Usuario where Usuario.Ci = @CiUsuario and Pass = @PasswordActual)
	begin
		update Usuario set Pass = @PasswordNuevo where Pass = @PasswordActual
	end
	else
	begin
		return -1
	end
end

GO

create proc spRealizarTransferencia
@IdSucursalOrigen int,
@IdSucursalDestino int,
@Fecha datetime,
@Moneda nvarchar(3),
@MontoOrigen float,
@MontoDestino float,
@CiUsuario int,
@IdCuentaOrigen int,
@IdCuentaDestino int
as
BEGIN
 begin tran
 
	if not exists(select * from Sucursal where Sucursal.IdSucursal=@IdSucursalOrigen and Sucursal.Activa = 1)
	begin
		return -1   --sucursal no existe en el sistema o está inactiva
	end
	
	if not exists(select * from Sucursal where Sucursal.IdSucursal=@IdSucursalDestino and Sucursal.Activa = 1)
	begin
		return -1   --sucursal no existe en el sistema o está inactiva
	end
	
	if not exists(select * from Usuario where Usuario.Ci=@CiUsuario and Usuario.Activo = 1)
	begin
		return -2   --Usuario no existe en el sistema o está inactivo
	end
	
 ----Retiro
 --exec AltaMovimiento @IdSucursalOrigen,1,@Fecha,@Moneda,1,@Monto,@CiUsuario,@IdCuentaOrigen
	--if @@ERROR <> 0
	--	rollback tran
	--	return -1
 ----DEPOSITO
 -- exec AltaMovimiento @IdSucursalDestino,2,@Fecha,@Moneda,1,@Monto,@CiUsuario,@IdCuentaDestino
	--if @@ERROR <> 0
	--	rollback tran
	--	return -1
	
	declare @cantidad int
	select @cantidad = COUNT(*) from Movimiento where Movimiento.IdSucursal=@IdSucursalOrigen
	set @cantidad = @cantidad+1 /*numero del nuevo movimiento*/

	
	--guardamos el movimiento
	insert into Movimiento(IdSucursal,NumeroMovimiento,Tipo,Fecha,Moneda,ViaWeb,Monto,IdCuenta,CiUsuario)
			values(@IdSucursalOrigen, @cantidad, 1, @Fecha, @Moneda,1,@MontoOrigen,@IdCuentaOrigen,@CiUsuario)
			
	if @@ERROR <> 0
	begin	
		rollback tran		
		return -3
	end
	
	--Distinguimos si sumamos o restamos el saldo de la cuenta
	declare @saldo float
	select @saldo = Saldo from Cuenta where Cuenta.IdCuenta = @IdCuentaOrigen
	
	if @@ERROR <> 0
	begin	
		rollback tran		
		return -3
	end
	
	--RESTAMOS EL SALDO EN LA CUENTA DE ORIGEN
	---------------------------------------------
	set @saldo = @saldo - @MontoOrigen
	
		
	update Cuenta set Saldo = @saldo where Cuenta.IdCuenta = @IdCuentaOrigen
			
	if @@error<>0
	begin
		rollback tran		
		return -3  --Si no se actualiza el saldo--
	end
		
		
	--************ COMIENZA EL DEPOSITO
	--**************************************************************************	
	set @saldo = 0
	
	select @cantidad = COUNT(*) from Movimiento where Movimiento.IdSucursal=@IdSucursalDestino
	set @cantidad = @cantidad+1 /*numero del nuevo movimiento*/

	
	--guardamos el movimiento
	insert into Movimiento(IdSucursal,NumeroMovimiento,Tipo,Fecha,Moneda,ViaWeb,Monto,IdCuenta,CiUsuario)
			values(@IdSucursalDestino, @cantidad, 2, @Fecha, @Moneda,1,@MontoDestino,@IdCuentaDestino,@CiUsuario)
			
	if @@ERROR <> 0
	begin	
		rollback tran		
		return -3
	end
	
	--Distinguimos si sumamos o restamos el saldo de la cuenta
	select @saldo = Saldo from Cuenta where Cuenta.IdCuenta = @IdCuentaDestino
	
	if @@ERROR <> 0
	begin	
		rollback tran		
		return -3
	end
	
	--SUMAMOS EL SALDO EN LA CUENTA DE ORIGEN
	---------------------------------------------
	set @saldo = @saldo + @MontoDestino
		
	update Cuenta set Saldo = @saldo where Cuenta.IdCuenta = @IdCuentaDestino
			
	if @@error<>0
	begin
		rollback tran		
		return -3  --Si no se actualiza el saldo--
	end
		
		-- COMMITS TRANSACTION
 commit tran
end

GO

create proc spConstultaMovimientos
@IdCuenta AS int,
@FechaInicio as Datetime
as 
begin
	select * from Movimiento where IdCuenta = @IdCuenta and Fecha >= @FechaInicio
end

GO

create proc spListarPagosPorPrestamo
@IdSucursal int,
@IdPrestamo int
as
begin

	select * from Pagos where Pagos.IdPrestamo=@IdPrestamo and Pagos.NumeroSucursal=@IdSucursal
	
end

GO
--INSERTAMOS VALORES PREDETERMINADOS
------------------------------------

--SUCURSAL
insert into Sucursal (Nombre,Direccion,Activa) values ('Sucursal Portones','Avda Bolivia 4507',1)
exec AltaEmpleado @IdSucursal=1, @Ci=1234567,@NombreUsuario='ElGaucho', @Nombre='Roberto',@Apellido='Gonzales',@Pass='1234'
exec AltaCliente @Direccion='aca', @Nombre='Amalfi', @Apellido='Marini',@Pass='jojojo', @Ci=3446586, @NombreUsuario = 'fito'
exec AltaPrestamo @Monto=10000, @Cuotas=10, @Moneda='UYU', @Fecha='01/01/1998', @IdCliente=3446586, @NumeroSucursal = 1
exec AltaPago @Fecha='01/02/1981', @Monto=1000, @NumeroSucursal=1, @IdEmpleado = 1234567, @IdPrestamo=1

EXEC AltaPrestamo @NumeroSucursal = 1,@IdCliente = 3446586,@Fecha = '01/01/2013',@Cuotas = 5,@Moneda = 'USD',@Monto = 200
EXEC AltaPago @IdEmpleado = 1234567, @IdPrestamo = 2, @NumeroSucursal = 1, @Monto = 40, @Fecha = '12/01/2013'

--exec spListarUltimosPagos 1

select * from Pagos
select * from Cliente
select * from Sucursal
select * from Usuario
select * from Prestamo




