USE SaleOrders

-- Insert order statuses
INSERT INTO OrderStatus (orderstatus_name) VALUES ('Pending');
INSERT INTO OrderStatus (orderstatus_name) VALUES ('Processing');
INSERT INTO OrderStatus (orderstatus_name) VALUES ('Shipped');
INSERT INTO OrderStatus (orderstatus_name) VALUES ('Delivered');
INSERT INTO OrderStatus (orderstatus_name) VALUES ('Cancelled');
GO