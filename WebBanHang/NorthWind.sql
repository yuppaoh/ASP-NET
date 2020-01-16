/**
 Author: Mrin 
 Model : NorthWind
**/



IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'northwind')
	CREATE DATABASE quanlybanhang
GO

use quanlybanhang;
GO

/* Table: customers */

CREATE TABLE customers (
  id              INT IDENTITY(1,1) NOT NULL,
  last_name       NVARCHAR(50) ,
  first_name      NVARCHAR(50) ,
  email           NVARCHAR(50) ,
  company         NVARCHAR(50) ,
  phone           NVARCHAR(25) ,
  address1        NVARCHAR(150),
  address2        NVARCHAR(150),
  city            NVARCHAR(50) ,
  state           NVARCHAR(50) ,
  postal_code     NVARCHAR(15) ,
  country         NVARCHAR(50) ,
  PRIMARY KEY (id)
);
GO

/* Table: employees */

CREATE TABLE employees (
  id              INT IDENTITY(1,1) NOT NULL,
  last_name       NVARCHAR(50) ,
  first_name      NVARCHAR(50) ,
  email           NVARCHAR(50) ,
  password		  NVARCHAR(500),
  avatar          NVARCHAR(250) ,
  job_title       NVARCHAR(50) ,
  department      NVARCHAR(50) ,
  manager_id      INT ,
  phone           NVARCHAR(25) ,
  address1        NVARCHAR(150),
  address2        NVARCHAR(150),
  city            NVARCHAR(50) ,
  state           NVARCHAR(50) ,
  postal_code     NVARCHAR(15) ,
  country         NVARCHAR(50) ,
  PRIMARY KEY (id)
);
GO

/* Table: orders */

CREATE TABLE orders (
  id              INT IDENTITY(1,1) NOT NULL,
  employee_id     INT ,
  customer_id     INT ,
  order_date      DATETIME ,
  shipped_date    DATETIME ,
  ship_name       NVARCHAR(50) ,
  ship_address1   NVARCHAR(150) ,
  ship_address2   NVARCHAR(150) ,
  ship_city       NVARCHAR(50) ,
  ship_state      NVARCHAR(50) ,
  ship_postal_code NVARCHAR(50) ,
  ship_country    NVARCHAR(50) ,
  shipping_fee    DECIMAL(19,4) NULL DEFAULT '0.0000',
  payment_type    NVARCHAR(50) ,
  paid_date       DATETIME ,
  order_status    NVARCHAR(25),
  PRIMARY KEY (id)
);
GO

/* Table: order_details */

CREATE TABLE order_details (
  order_id            INT IDENTITY(1,1) NOT NULL,
  product_id          INT ,
  quantity            DECIMAL(18,4) NOT NULL DEFAULT '0.0000',
  unit_price          DECIMAL(19,4) NULL DEFAULT '0.0000',
  discount            DECIMAL NOT NULL DEFAULT '0',
  order_detail_status NVARCHAR(25),
  date_allocated      DATETIME ,
  PRIMARY KEY (order_id, product_id)
);
GO



/* Table: products */

CREATE TABLE products (
  id              INT IDENTITY(1,1) NOT NULL,
  product_code    NVARCHAR(25) ,
  product_name    NVARCHAR(50) ,
  description     NVARCHAR(250),
  standard_cost   DECIMAL(19,4) NULL DEFAULT '0.0000',
  list_price      DECIMAL(19,4) NOT NULL DEFAULT '0.0000',
  target_level    INT ,
  reorder_level   INT ,
  minimum_reorder_quantity INT ,
  quantity_per_unit NVARCHAR(50) ,
  discontinued    TINYINT NOT NULL DEFAULT '0',
  category        NVARCHAR(50),
  PRIMARY KEY (id)
);
GO

/* Foreign Key: orders */

ALTER TABLE orders ADD CONSTRAINT fk_orders__customers FOREIGN KEY (customer_id) REFERENCES customers(id);
ALTER TABLE orders ADD CONSTRAINT fk_orders__employees FOREIGN KEY (employee_id) REFERENCES employees(id);

/* Foreign Key:  order_details */

ALTER TABLE order_details ADD CONSTRAINT fk_order_details__orders      FOREIGN KEY (order_id) REFERENCES orders(id);
ALTER TABLE order_details ADD CONSTRAINT fk_order_details__products    FOREIGN KEY (product_id) REFERENCES products(id);