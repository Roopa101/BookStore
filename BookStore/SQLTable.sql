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


