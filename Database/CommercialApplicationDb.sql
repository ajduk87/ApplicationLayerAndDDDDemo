CREATE SCHEMA commercialapplication;


DROP SEQUENCE IF EXISTS commercialapplication.product_id_seq;

DROP SEQUENCE IF EXISTS commercialapplication.storage_id_seq;

DROP SEQUENCE IF EXISTS commercialapplication.customer_id_seq;

DROP SEQUENCE IF EXISTS commercialapplication.sellingprogram_id_seq;

DROP SEQUENCE IF EXISTS commercialapplication.action_id_seq;

DROP SEQUENCE IF EXISTS commercialapplication.orderitem_id_seq;

DROP SEQUENCE IF EXISTS commercialapplication.orders_id_seq;

DROP SEQUENCE IF EXISTS commercialapplication.invoiceitem_id_seq;

DROP SEQUENCE IF EXISTS commercialapplication.invoices_id_seq;

CREATE SEQUENCE commercialapplication.product_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE commercialapplication.storage_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE commercialapplication.customer_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE commercialapplication.sellingprogram_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE commercialapplication.action_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE commercialapplication.orderitem_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE commercialapplication.orders_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE commercialapplication.invoiceitem_id_seq INCREMENT 1 START 1;

CREATE SEQUENCE commercialapplication.invoices_id_seq INCREMENT 1 START 1;



DROP TABLE IF EXISTS commercialapplication.product CASCADE;

CREATE TABLE commercialapplication.product
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('commercialapplication."product_id_seq"'::text)::regclass),
	Name varchar(500) UNIQUE NOT NULL,
	UnitCost varchar(500) NOT NULL,
	Description varchar(500) NULL,
	ImageUrl varchar(500) NULL,
	VideoLink varchar(500) NULL,
	SerialNumber varchar(500) NULL,
	KindOfProduct varchar(500) NOT NULL
);
ALTER TABLE commercialapplication.product ADD CONSTRAINT PK_Product
	PRIMARY KEY (Id);
	
	
	

DROP TABLE IF EXISTS commercialapplication.storage CASCADE;

CREATE TABLE commercialapplication.storage
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('commercialapplication."storage_id_seq"'::text)::regclass),
	Name varchar(500) NULL,
	Location varchar(500) NULL
);
ALTER TABLE commercialapplication.storage ADD CONSTRAINT PK_Storage
	PRIMARY KEY (Id);



-- create table connection BEGIN
DROP TABLE IF EXISTS commercialapplication.productstorage CASCADE;

CREATE TABLE commercialapplication.productstorage
(
	ProductId integer NOT NULL,
	StorageId integer NOT NULL,
	AmountOfProduct integer NOT NULL
);

ALTER TABLE commercialapplication.productstorage ADD CONSTRAINT PK_ProductStorage
	PRIMARY KEY (ProductId,StorageId);

CREATE INDEX IXFK_ProductStorage_Product ON commercialapplication.productstorage (ProductId ASC);

CREATE INDEX IXFK_ProductStorage_Storage ON commercialapplication.productstorage (StorageId ASC);

ALTER TABLE commercialapplication.productstorage ADD CONSTRAINT FK_ProductStorage_Product
	FOREIGN KEY (ProductId) REFERENCES commercialapplication.product (Id) ON DELETE No Action ON UPDATE No Action;
ALTER TABLE commercialapplication.productstorage ADD CONSTRAINT FK_ProductStorage_Storage
	FOREIGN KEY (StorageId) REFERENCES commercialapplication.storage (Id) ON DELETE No Action ON UPDATE No Action;
-- create table connection END



DROP TABLE IF EXISTS commercialapplication.customer CASCADE;

CREATE TABLE commercialapplication.customer
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('commercialapplication."customer_id_seq"'::text)::regclass),
	Name varchar(500) UNIQUE NOT NULL
);
ALTER TABLE commercialapplication.customer ADD CONSTRAINT PK_Customer
	PRIMARY KEY (Id);


DROP TABLE IF EXISTS commercialapplication.sellingprogram CASCADE;

CREATE TABLE commercialapplication.sellingprogram
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('commercialapplication."sellingprogram_id_seq"'::text)::regclass),
	SellingProgram varchar(500) NOT NULL,
	SellingCondition varchar(500) NOT NULL,
	DayForPaying integer NOT NULL
);
ALTER TABLE commercialapplication.sellingprogram ADD CONSTRAINT PK_SellingProgram
	PRIMARY KEY (Id);
	
	
	
	
DROP TABLE IF EXISTS commercialapplication.action CASCADE;

CREATE TABLE commercialapplication.action
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('commercialapplication."action_id_seq"'::text)::regclass),
	Productid integer NOT NULL,
	Discount  NUMERIC (5, 2) NOT NULL,
	ThresholdAmount integer NOT NULL,
	CustomerId integer NOT NULL
);
ALTER TABLE commercialapplication.action ADD CONSTRAINT PK_Action
	PRIMARY KEY (Id);
ALTER TABLE commercialapplication.action ADD CONSTRAINT FK_Action_Product
	FOREIGN KEY (Productid) REFERENCES commercialapplication.product (Id) ON DELETE No Action ON UPDATE No Action;
ALTER TABLE commercialapplication.action ADD CONSTRAINT FK_Action_Customer
	FOREIGN KEY (CustomerId) REFERENCES commercialapplication.customer (Id) ON DELETE No Action ON UPDATE No Action;
	
	
DROP TABLE IF EXISTS commercialapplication.orderitem CASCADE;

CREATE TABLE commercialapplication.orderitem
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('commercialapplication."orderitem_id_seq"'::text)::regclass),
	Productid integer NOT NULL,
	Amount integer NOT NULL,
	Value NUMERIC(8,2) NOT NULL,
	DiscountBasic numeric NOT NULL,
	ActionId integer NOT NULL
);
ALTER TABLE commercialapplication.orderitem ADD CONSTRAINT PK_Orderitem
	PRIMARY KEY (Id);
ALTER TABLE commercialapplication.orderitem ADD CONSTRAINT FK_Orderitem_Product
	FOREIGN KEY (Productid) REFERENCES commercialapplication.product (Id) ON DELETE No Action ON UPDATE No Action;
ALTER TABLE commercialapplication.orderitem ADD CONSTRAINT FK_Orderitem_Action
	FOREIGN KEY (ActionId) REFERENCES commercialapplication.action (Id) ON DELETE No Action ON UPDATE No Action;	



DROP TABLE IF EXISTS commercialapplication.orders CASCADE;

CREATE TABLE commercialapplication.orders
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('commercialapplication."orderitem_id_seq"'::text)::regclass),
	State varchar(500) NOT NULL,
	CreationDate timestamp without time zone NOT NULL
);
ALTER TABLE commercialapplication.orders ADD CONSTRAINT PK_Orders
	PRIMARY KEY (Id);
	
	
	
	
	
-- create table connection BEGIN
DROP TABLE IF EXISTS commercialapplication.orderitemorders CASCADE;

CREATE TABLE commercialapplication.orderitemorders
(
	OrderItemId integer NOT NULL,
	OrderId integer NOT NULL
);

ALTER TABLE commercialapplication.orderitemorders ADD CONSTRAINT PK_OrderItemOrders
	PRIMARY KEY (OrderItemId,OrderId);

CREATE INDEX IXFK_OrderItemOrders_OrderItem ON commercialapplication.orderitemorders (OrderItemId ASC);

CREATE INDEX IXFK_OrderItemOrders_Order ON commercialapplication.orderitemorders (OrderId ASC);

ALTER TABLE commercialapplication.orderitemorders ADD CONSTRAINT FK_OrderItemOrders_OrderItem
	FOREIGN KEY (OrderItemId) REFERENCES commercialapplication.orderitem (Id) ON DELETE No Action ON UPDATE No Action;
ALTER TABLE commercialapplication.orderitemorders ADD CONSTRAINT FK_OrderItemOrders_Orders
	FOREIGN KEY (OrderId) REFERENCES commercialapplication.orders (Id) ON DELETE No Action ON UPDATE No Action;
-- create table connection END


-- create table connection BEGIN
DROP TABLE IF EXISTS commercialapplication.orderscustomer CASCADE;

CREATE TABLE commercialapplication.orderscustomer
(
	OrderId integer NOT NULL,
	CustomerId integer NOT NULL
);

ALTER TABLE commercialapplication.orderscustomer ADD CONSTRAINT PK_OrdersCustomer
	PRIMARY KEY (OrderId,CustomerId);

CREATE INDEX IXFK_OrdersCustomer_Orders ON commercialapplication.orderscustomer (OrderId ASC);

CREATE INDEX IXFK_OrdersCustomer_Customer ON commercialapplication.orderscustomer (CustomerId ASC);

ALTER TABLE commercialapplication.orderscustomer ADD CONSTRAINT FK_OrdersCustomer_Orders
	FOREIGN KEY (OrderId) REFERENCES commercialapplication.orders (Id) ON DELETE No Action ON UPDATE No Action;
ALTER TABLE commercialapplication.orderscustomer ADD CONSTRAINT FK_OrdersCustomer_Customer
	FOREIGN KEY (CustomerId) REFERENCES commercialapplication.customer (Id) ON DELETE No Action ON UPDATE No Action;
-- create table connection END

DROP TABLE IF EXISTS commercialapplication.invoiceitem CASCADE;

CREATE TABLE commercialapplication.invoiceitem
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('commercialapplication."invoiceitem_id_seq"'::text)::regclass),
	Productid integer NOT NULL,
	Amount integer NOT NULL,
	Value NUMERIC(8,2) NOT NULL,
	DiscountBasic numeric NOT NULL,
	ActionId integer NOT NULL
);
ALTER TABLE commercialapplication.invoiceitem ADD CONSTRAINT PK_InvoiceItem
	PRIMARY KEY (Id);
ALTER TABLE commercialapplication.invoiceitem ADD CONSTRAINT FK_InvoiceItem_Product
	FOREIGN KEY (Productid) REFERENCES commercialapplication.product (Id) ON DELETE No Action ON UPDATE No Action;
ALTER TABLE commercialapplication.invoiceitem ADD CONSTRAINT FK_InvoiceItem_Action
	FOREIGN KEY (ActionId) REFERENCES commercialapplication.action (Id) ON DELETE No Action ON UPDATE No Action;
	
	

DROP TABLE IF EXISTS commercialapplication.invoices CASCADE;

CREATE TABLE commercialapplication.invoices
(
	Id integer NOT NULL   DEFAULT NEXTVAL(('commercialapplication."invoices_id_seq"'::text)::regclass),
	SellingProgramId integer NOT NULL,
	CreationDate timestamp without time zone NOT NULL
);
ALTER TABLE commercialapplication.invoices ADD CONSTRAINT PK_Invoices
	PRIMARY KEY (Id);
ALTER TABLE commercialapplication.invoices ADD CONSTRAINT FK_Invoices_SellingProgram
	FOREIGN KEY (SellingProgramId) REFERENCES commercialapplication.sellingprogram (Id) ON DELETE No Action ON UPDATE No Action;

-- create table connection BEGIN
DROP TABLE IF EXISTS commercialapplication.invoiceiteminvoices CASCADE;

CREATE TABLE commercialapplication.invoiceiteminvoices
(
	InvoiceId integer NOT NULL,
	InvoiceItemId integer NOT NULL
);

ALTER TABLE commercialapplication.invoiceiteminvoices ADD CONSTRAINT PK_InvoiceItemInvoices
	PRIMARY KEY (InvoiceItemId,InvoiceId);

CREATE INDEX IXFK_InvoiceItemInvoices_Invoices ON commercialapplication.invoices (Id ASC);

CREATE INDEX IXFK_InvoiceItemInvoices_InvoiceItem ON commercialapplication.invoiceitem (Id ASC);

ALTER TABLE commercialapplication.invoiceiteminvoices ADD CONSTRAINT FK_InvoiceItemInvoices_Invoices
	FOREIGN KEY (InvoiceId) REFERENCES commercialapplication.invoices (Id) ON DELETE No Action ON UPDATE No Action;
ALTER TABLE commercialapplication.invoiceiteminvoices ADD CONSTRAINT FK_InvoiceItemInvoices_InvoiceItem
	FOREIGN KEY (InvoiceItemId) REFERENCES commercialapplication.invoiceitem (Id) ON DELETE No Action ON UPDATE No Action;
-- create table connection END	
	
-- create table connection BEGIN
DROP TABLE IF EXISTS commercialapplication.invoicescustomer CASCADE;

CREATE TABLE commercialapplication.invoicescustomer
(
	InvoiceId integer NOT NULL,
	CustomerId integer NOT NULL
);

ALTER TABLE commercialapplication.invoicescustomer ADD CONSTRAINT PK_InvoicesItemCustomer
	PRIMARY KEY (CustomerId,InvoiceId);

CREATE INDEX IXFK_InvoicesCustomer_Invoices ON commercialapplication.invoicescustomer (InvoiceId ASC);

CREATE INDEX IXFK_InvoicesCustomer_Customer ON commercialapplication.invoicescustomer (CustomerId ASC);

ALTER TABLE commercialapplication.invoicescustomer ADD CONSTRAINT FK_InvoicesCustomer_Invoices
	FOREIGN KEY (InvoiceId) REFERENCES commercialapplication.invoices (Id) ON DELETE No Action ON UPDATE No Action;
ALTER TABLE commercialapplication.invoicescustomer ADD CONSTRAINT FK_InvoicesCustomer_Customer
	FOREIGN KEY (CustomerId) REFERENCES commercialapplication.customer (Id) ON DELETE No Action ON UPDATE No Action;
-- create table connection END