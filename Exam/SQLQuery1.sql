use WAD

create table Categories(
	Cat_ID varchar(15) not null,
	Cat_Name nvarchar(50) not null,
	Primary key (Cat_ID)
)
Go

create table Products(
	pro_id varchar(15) not null,
	pro_name nvarchar(50) not null,
	quantity int not null,
	pro_price bigint not null,
	pro_image nvarchar(50) not null,
	cat_id varchar(15) not null,
	Description nvarchar(50) not null,
	Primary key (Cat_ID)
)
Go

alter table Products add constraint product_categories foreign key (cat_id) references Categories(Cat_ID);
go