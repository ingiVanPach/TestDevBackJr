--Depurar solo los ID diferentes de 6,7,9 y 10  de la tabla  **usuarios** **
SELECT *,
--Update usuarios set
Activo = 0
FROM usuarios
WHERE userId NOT IN (6, 7, 9, 10)

SELECT *
--Delete
FROM usuarios
WHERE userId NOT IN (6, 7, 9, 10)
--------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------

--Actualizar el dato Sueldo en un 10 porciento a los empleados que tienen fechas entre el año 2000 y 2001
Select *,
--UPDATE Empleados SET 
Sueldo = (Sueldo * 1.10)
FROM Empleados
WHERE FechaIngreso BETWEEN '2000-01-01' AND '2001-12-31'
--------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------


--Realiza una consulta para traer el nombre de usuario y fecha de ingreso de los usuarios que gananen mas de 10000 y su apellido 
--comience con T ordernado del mas reciente al mas antiguo 

SELECT U.Nombre, E.FechaIngreso
FROM Usuarios U
JOIN Empleados E ON U.userId = E.userId
WHERE E.Sueldo > 10000 AND U.Paterno LIKE 'T%'
ORDER BY E.FechaIngreso ASC
--------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------


--Realiza una consulta donde agrupes a los empleados por sueldo, un grupo con los que ganan menos de 1200 y uno mayor o igual a 1200, 
--cuantos hay en cada grupo?

SELECT 
    'Menos de 1200' AS RangoSueldo,
    COUNT(*) AS Cantidad
FROM empleados
WHERE Sueldo < 1200
UNION
SELECT 
    '1200 o más' AS RangoSueldo,
    COUNT(*) AS Cantidad
FROM empleados
WHERE Sueldo >= 1200


