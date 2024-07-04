
CREATE TABLE Proveedor (
    id_proveedor SERIAL PRIMARY KEY,
    razon_social VARCHAR(255) NOT NULL,
    tipo_documento VARCHAR(50),
    numero_documento VARCHAR(50),
    direccion VARCHAR(255),
    mail VARCHAR(255),
    celular VARCHAR(20),
    estado BOOLEAN
);

CREATE TABLE Sucursal (
    id_sucursal SERIAL PRIMARY KEY,
    descripcion VARCHAR(255),
    direccion VARCHAR(255),
    telefono VARCHAR(20),
    whatsapp VARCHAR(20),
    mail VARCHAR(255),
    estado BOOLEAN
);

CREATE TABLE Productos (
    id_producto SERIAL PRIMARY KEY,
    descripcion VARCHAR(255) NOT NULL,
    cantidad_minima INT,
    cantidad_stock INT,
    precio_compra DECIMAL(10, 2),
    precio_venta DECIMAL(10, 2),
    categoria VARCHAR(100),
    marca VARCHAR(100),
    estado BOOLEAN
);

CREATE TABLE Pedido_Compra (
    id_pedido_compra SERIAL PRIMARY KEY,
    id_proveedor INT,
    id_sucursal INT,
    fecha_hora TIMESTAMP,
    total DECIMAL(10, 2),
    FOREIGN KEY (id_proveedor) REFERENCES Proveedor(id_proveedor),
    FOREIGN KEY (id_sucursal) REFERENCES Sucursal(id_sucursal)
);

CREATE TABLE Detalle_Pedido (
    id_detalle_pedido SERIAL PRIMARY KEY,
    id_pedido_compra INT,
    id_producto INT,
    cantidad_producto INT,
    subtotal DECIMAL(10, 2),
    FOREIGN KEY (id_pedido_compra) REFERENCES Pedido_Compra(id_pedido_compra),
    FOREIGN KEY (id_Producto) REFERENCES Productos(id_producto)
);


--select a la tabla de productos
select * from productos;
--select a la tabla de proveedor
select * from proveedor;
--select a la tabla de sucursal
select * from sucursal;
--select a la tabla de pedido_compra
select * from pedido_compra;
--select a la tabla de detalle_pedido
select * from detalle_pedido;