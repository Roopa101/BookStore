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

