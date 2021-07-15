create database dbPurchase
use dbPurchase

create table Product(
ProductID int not null identity,
ProductName varchar(50),
Description varchar(50),
Price money,
DiscountPercentage decimal(5,2),
Primary key (ProductID))

create table Customer(
CustomerID int not null identity,
CustomerName varchar(50),
Address varchar(100),
ContactNumber char(10),
Primary key (CustomerID))

create table PurchaseOrder(
POID int not null identity,
CustomerID int,
Date date,
Amount money,
Primary key (POID),
Foreign Key (CustomerID )
References Customer(CustomerID))

create table PurchaseOrderDetail(
PODID int not null identity,
POID int,
ProductID int,
Price money,
Quantity int,
Primary Key (PODID),
Foreign key (POID)
References PurchaseOrder,
Foreign key (ProductID)
References Product)


insert into Product(ProductID,ProductName,Description,Price,DiscountPercentage)
values ('Monitor','23inch-Lenova',15,000,10.00)


select Customer.CustomerName, Customer.CustomerID,Sum(PurchaseOrder.Amount)as Amount,PurchaseOrder.Date
from Customer join PurchaseOrder
on PurchaseOrder.CustomerID=Customer.CustomerID
group by Customer.CustomerID,Customer.CustomerName,PurchaseOrder.Date
order by PurchaseOrder.Date asc

