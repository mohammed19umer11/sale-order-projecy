USE SaleOrders

-- OrderStatus
CREATE TABLE OrderStatus(
	orderstatus_id INT PRIMARY KEY IDENTITY,
	orderstatus_name NVARCHAR(20) NOT NULL,
	isdeleted BIT NOT NULL DEFAULT 0
);
GO

-- Employee table
CREATE TABLE Employee (
    employee_id INT PRIMARY KEY IDENTITY,
    employee_firstname NVARCHAR(100) NOT NULL,
    employee_lastname NVARCHAR(100) NOT NULL,
    employee_email NVARCHAR(100) NOT NULL UNIQUE,
	isdeleted BIT NOT NULL DEFAULT 0
);
GO

-- Customer table
CREATE TABLE Customer (
    customer_id INT PRIMARY KEY IDENTITY,
    customer_name NVARCHAR(100) NOT NULL,
    customer_address NVARCHAR(100) NOT NULL,
	customer_contact NVARCHAR(100) NOT NULL,
	isdeleted BIT NOT NULL DEFAULT 0
);
GO

-- Item table
CREATE TABLE Item (
    item_id INT PRIMARY KEY IDENTITY,
    item_name NVARCHAR(100) NOT NULL,
    item_description NVARCHAR(255),
	item_quantity INT NOT NULL DEFAULT 0,
    item_price DECIMAL(18, 2) NOT NULL,
	isdeleted BIT NOT NULL DEFAULT 0
);
GO

-- SaleOrder table
CREATE TABLE SaleOrder (
    saleorder_id INT PRIMARY KEY IDENTITY,
    customer_id INT NOT NULL,
    employee_id INT NOT NULL,
    orderstatus_id INT NOT NULL,
	createdate DATETIME NOT NULL DEFAULT GETDATE(),
	FOREIGN KEY (customer_id) REFERENCES Customer(customer_id),
	FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
	FOREIGN KEY (orderstatus_id) REFERENCES OrderStatus(orderstatus_id)
);
GO

-- SalesOrderDetail table
CREATE TABLE SaleOrderDetail (
    saleorderdetail_id INT PRIMARY KEY IDENTITY,
    saleorder_id INT NOT NULL,
	item_id INT NOT NULL,
    item_qty INT NOT NULL,
    FOREIGN KEY (saleorder_id) REFERENCES SaleOrder(saleorder_id),
	FOREIGN KEY (item_id) REFERENCES Item(item_id)
);
GO