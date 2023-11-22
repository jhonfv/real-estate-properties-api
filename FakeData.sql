USE Properties;
GO

-- Insertar 5 dueños ficticios en la tabla Owner
INSERT INTO Owner (Name, Address, PhotoPath, Birthday) VALUES 
('Alex Rich', '123 Billionaire Row, New York, NY, USA', '/images/owners/alexrich.jpg', '1975-03-21'),
('Jamie Wealthy', '456 Moneybag Ave, Los Angeles, CA, USA', '/images/owners/jamiewealthy.jpg', '1982-07-14'),
('Taylor Fortune', '789 Cash Cove, Greenwich, CT, USA', '/images/owners/taylorfortune.jpg', '1990-05-30'),
('Jordan Abundance', '1010 Gold Street, Miami, FL, USA', '/images/owners/jordanabundance.jpg', '1969-11-08'),
('Morgan Prosper', '1111 Prosperity Lane, Chicago, IL, USA', '/images/owners/morganprosper.jpg', '1985-12-17');
GO

-- Insertar 20 megamansiones para multimillonarios con diferentes dueños
DECLARE @Counter INT = 1;
DECLARE @OwnerCount INT = (SELECT COUNT(*) FROM Owner);
DECLARE @RandomOwner INT;
DECLARE @Price MONEY;
DECLARE @Name NVARCHAR(100);
DECLARE @Address NVARCHAR(255);
DECLARE @CodeInternal NVARCHAR(50);
DECLARE @Year INT = YEAR(GETDATE());

WHILE @Counter <= 20
BEGIN
    -- Seleccionar un dueño al azar de la tabla Owner
    SET @RandomOwner = CEILING(@OwnerCount * RAND(CHECKSUM(NEWID())));

    -- Generar un precio aleatorio entre 50 y 150 millones
    SET @Price = ROUND((RAND(CHECKSUM(NEWID())) * (150000000 - 50000000)) + 50000000, 2);

    -- Generar un nombre y una dirección ficticios para la mansión
    SET @Name = CONCAT('MegaMansion ', @Counter);
    SET @Address = CONCAT('Exclusive Estates ', @Counter, ', Beverly Hills, CA, USA');
    SET @CodeInternal = CONCAT('MM-', RIGHT('000' + CAST(@Counter AS NVARCHAR), 3));

    -- Insertar la mansión en la tabla Property
    INSERT INTO Property (Name, Address, Price, CodeInternal, Year, IdOwner)
    VALUES (@Name, @Address, @Price, @CodeInternal, @Year, @RandomOwner);
    
    SET @Counter = @Counter + 1;
END


-- Insertar registros de prueba en PropertyTrace para algunas propiedades
SET @Counter = 0;
DECLARE @PropertyCount INT = (SELECT COUNT(*) FROM Property);
DECLARE @RandomProperty INT;
DECLARE @DateSale DATE;
DECLARE @Value MONEY;
DECLARE @Tax MONEY;

WHILE @Counter <= 10 -- Crear registros para 10 transacciones ficticias
BEGIN
    -- Seleccionar una propiedad al azar de la tabla Property
    SET @RandomProperty = CEILING(@PropertyCount * RAND(CHECKSUM(NEWID())));

    -- Generar una fecha de venta aleatoria en los últimos 5 años
    SET @DateSale = DATEADD(YEAR, -CAST(RAND(CHECKSUM(NEWID())) * 5 AS INT), GETDATE());

    -- Generar un nombre ficticio para la transacción
    SET @Name = CONCAT('Transaction ', @Counter);

    -- Establecer un valor y un impuesto aleatorios para la transacción
    -- Asumiendo que el valor está cerca del precio de la propiedad y el impuesto es un porcentaje de ese valor
    SET @Value = (SELECT TOP 1 Price FROM Property WHERE IdProperty = @RandomProperty) * (1 - RAND(CHECKSUM(NEWID())) * 0.1);
    SET @Tax = @Value * 0.07; -- Supongamos que el impuesto es el 7% del valor

    -- Insertar el rastreo de la propiedad en la tabla PropertyTrace
    INSERT INTO PropertyTrace (DateSale, Name, Value, Tax, IdProperty)
    VALUES (@DateSale, @Name, @Value, @Tax, @RandomProperty);
    
    SET @Counter = @Counter + 1;
END
GO
