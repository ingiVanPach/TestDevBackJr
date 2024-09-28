--Querys Varios, poblacion de tablas 

select [Login], Nombre, Paterno, Materno
from Usuarios

--INSERT INTO Usuarios ([Login], Nombre, Paterno, Materno)
select [Login], Nombres, Paterno, Materno
from [dbo].['Info usuarios$']



select UserId,Sueldo,FechaIngreso
from Empleados

--INSERT INTO Empleados (UserId, Sueldo, FechaIngreso)
select Usuarios.UserId, Sueldo, [Fecha Ingreso]
from [dbo].['Info Empleados$']
inner join Usuarios on Usuarios.Login = [dbo].['Info Empleados$'].[Login]

select UserId,Sueldo,FechaIngreso
from Empleados

select [Login], Nombre, Paterno, Materno
from Usuarios

select *
from Empleados 
inner join Usuarios on Empleados.UserId = Usuarios.UserId


