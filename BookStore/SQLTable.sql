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