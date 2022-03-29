create database BookStore
use BookStore

create Table UserRegister
(
UserId int primary key identity(1,1),
FullName varchar(10),
EmailId varchar(25),
PhNo varchar(20),
Password varchar(20)
)

select *from UserRegister

create Table Book
(
BookId int primary key identity(1,1),
BookName varchar(50),
AuthorName varchar(50),
DiscountPrice money,
OriginalPrice money,
BookDescription varchar(50),
Image varchar(50),
Rating float ,
Reviewer int,
BookCount int
)

select *from Book

create table BookCart
(
CartId int primary key identity(1,1),
Quantity int,
userId int Foreign Key References UserRegister(userId),
BookId int Foreign Key References Book(BookId)
)

select *from BookCart

create table WishList
(
	WishlistId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES UserRegister(UserId),
	BookId INT NOT NULL
	FOREIGN KEY (BookId) REFERENCES Book(BookId)	
);


select * from WishList


---Address----
create table AddressType
(
	TypeId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Type varchar(20)
);
INSERT INTO AddressType (Type) VALUES ('Home')
INSERT INTO AddressType (Type) VALUES ('Work')
INSERT INTO AddressType (Type) VALUES ('Other')


select * from AddressType

drop table Address

create table Address
(
    AddressId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES UserRegister(UserId),
	Address varchar(max) not null,
	City varchar(100),
	State varchar(100),
	TypeId int
	FOREIGN KEY (TypeId) REFERENCES AddressType(TypeId)
);

select * from Address
select * from AddressType

----order---

create table Orders
(
         OrdersId int not null identity (1,1) primary key,
		 UserId INT NOT NULL,
		 FOREIGN KEY (UserId) REFERENCES UserRegister(UserId),
		 AddressId int
		 FOREIGN KEY (AddressId) REFERENCES Address(AddressId),
	     BookId INT NOT NULL
		 FOREIGN KEY (BookId) REFERENCES Book(BookId),
		 TotalPrice int,
		 BookQuantity int,
		 OrderDate Date
);

create table BooksOrder(
OrderId int primary key identity(1,1),
TotalPrice Money,
BookQuantity int,
OrderDate Date,
userId int Foreign Key References UserRegister(userId),
BookId int Foreign Key References Book(BookId),
AddressId int Foreign Key References Address(AddressId)
)
select * from BooksOrder

----feedback---

create table Feedback
(
         FeedbackId int not null identity (1,1) primary key,
		 UserId INT NOT NULL
		 FOREIGN KEY (UserId) REFERENCES UserRegister(UserId),
	     BookId INT NULL
		 FOREIGN KEY (BookId) REFERENCES Book(BookId),
		 Comments Varchar(max),
		 Ratings int		 
);

create Table FeedBackBook(
FeedbackId int not null identity (1,1) primary key,
FeedBackFromUserName varchar(15),
Comments varchar(50),
Ratings int,
UserId int Foreign Key References UserRegister(UserId),
BookId int Foreign Key References Book(BookId))

drop table FeedBackBook

select * from FeedBackBook