sqalter procedure sp_AddingBooks
 @BookName varchar(50),
 @AuthorName varchar(50),
 @DiscountPrice money,
  @OriginalPrice money,
  @BookDescription varchar(50),
  @Rating float,
  @Image varchar(50),
  @Reviewer int,
  @BookCount int
  As 
  Begin
  Insert into Book (BookName,AuthorName,DiscountPrice,OriginalPrice,BookDescription,
  Rating,Image,Reviewer,BookCount) values (@BookName,@AuthorName,@DiscountPrice,@OriginalPrice,@BookDescription,
  @Rating,@Image,@Reviewer,@BookCount)
  End



  create procedure sp_UpdateBooks
 @BookId int,
 @BookName varchar(50),
 @AuthorName varchar(50),
 @DiscountPrice money,
 @OriginalPrice money,
 @BookDescription varchar(50),
 @Rating float,
 @Image varchar(50),
 @Reviewer int,
 @BookCount int
  As 
  Begin
  update Book set BookName=@BookName,AuthorName=@AuthorName,DiscountPrice=@DiscountPrice,
  OriginalPrice=@OriginalPrice,BookDescription=@BookDescription,Rating=@Rating,Reviewer=@Reviewer,
  BookCount=@BookCount,Image=@Image where BookId=@BookId
  End


  create procedure sp_DeleteBooks
  @BookId int
  As
  Begin
  delete Book where BookId=@BookId
  End


create procedure sp_GetAllBooks
  As
  Begin
  select * from Book
  End

  create procedure sp_GetAllBookById
  @BookId int
  As
  Begin
  select * from Book where BookId=@BookId
  End

--BookCart---
 create procedure sp_AddingBookinCart
@Quantity int,
@userId int,
@BookId int
As
Begin
Insert into BookCart (Quantity,userId,BookId) values (@Quantity,@userId,@BookId)
End


create procedure sp_UpdateCart
@Quantity int,
@CartId int
As
Begin
update BookCart set Quantity=@Quantity
where CartId=@CartId
End

create procedure sp_DeleteCart
@CartId int
As
Begin 
Delete BookCart where CartId=@CartId
End

create procedure sp_GetAllBooksinCart
@userId int
As
Begin
--select BookCart.userId,BookCart.CartId,BookCart.BookId,BookCart.Quantity ,Books.BookName,Books.AuthorName,Books.

select BookCart.CartId,BookCart.userId,BookCart.BookId,BookCart.Quantity , Book.BookName,Book.AuthorName,
Book.BookDescription,Book.DiscountPrice,Book.OriginalPrice,Book.Rating,Book.Reviewer,
Book.Image,Book.BookCount from BookCart inner join Book on BookCart.BookId=Book.BookId
where BookCart.userId=@userId
End


----WishList---
create PROCEDURE sp_CreateWishlist
	@UserId INT,
	@BookId INT
AS
BEGIN 
	IF EXISTS(SELECT * FROM WishList WHERE BookId = @BookId AND UserId = @UserId)
		SELECT 1;
	ELSE
	BEGIN
		IF EXISTS(SELECT * FROM Book WHERE BookId = @BookId)
		BEGIN
			INSERT INTO WishList(UserId,BookId)
			VALUES ( @UserId,@BookId)
		END
		ELSE
			SELECT 2;
	END
END


CREATE PROCEDURE sp_DeleteWishlist
	@WishlistId INT
AS
BEGIN
		DELETE FROM WishList WHERE WishlistId = @WishlistId
END

alter PROCEDURE sp_GetWishListbyUserId
  @UserId int
AS
BEGIN
	   select 
		Book.BookId,
		Book.BookName,
		Book.AuthorName,
		Book.DiscountPrice,
		Book.OriginalPrice,
		Book.BookDescription,
		Book.Image,
		Book.Rating,
		Book.Reviewer,
		Book.BookCount,
		WishList.WishlistId,
		WishList.UserId,
		WishList.BookId
		FROM Book
		inner join WishList
		on Wishlist.BookId=Book.BookId where WishList.UserId=@UserId
End


----address----
create procedure Sp_AddAddress(
		@UserId int,
        @Address varchar(600),
		@City varchar(50),
		@State varchar(50),
		@TypeId int	)		
As 
Begin
	IF (EXISTS(SELECT * FROM UserRegister WHERE @UserId = @UserId))
	Begin
	Insert into Address (UserId,Address,City,State,TypeId )
		values (@UserId,@Address,@City,@State,@TypeId);
	End
	Else
	Begin
		Select 1
	End
End

create PROCEDURE sp_UpdateAddress
(
@AddressId int,
@Address varchar(max),
@City varchar(100),
@State varchar(100),
@TypeId int	)

AS
BEGIN
       If (exists(Select * from Address where AddressId=@AddressId))
		begin
			UPDATE Address
			SET 
			Address= @Address, 
			City = @City,
			State=@State,
			TypeId=@TypeId 
				WHERE AddressId=@AddressID;
		 end
		 else
		 begin
		 select 1;
		 end
END

alter procedure sp_DeleteAddress
@AddressId int
As
Begin
Delete Address where AddressId=@AddressId
End


create PROCEDURE sp_GetAllAddresses
AS
BEGIN
	 begin
	   SELECT * FROM Address ;
   	 end
End

Exec sp_GetAllAddresses



create PROCEDURE sp_GetAddressbyUserid
  (
  @UserId int
  )
AS
BEGIN
	   SELECT * FROM Address WHERE UserId=@UserId;
	-- begin
	--	select 1
	--end
END


-----order---


alter procedure sp_BooksOrder
@TotalPrice Money,
@BookQuantity int,
@OrderDate Date,
@userId int,
@BookId int,
@AddressId int
As
Begin
Insert into BooksOrder (TotalPrice,BookQuantity,OrderDate,UserId,BookId,AddressId)
values (@TotalPrice,@BookQuantity,@OrderDate,@userId,@BookId,@AddressId)
End



create procedure sp_GetAllOrder
@userId int
As
Begin
select BooksOrder.OrderId,BooksOrder.userId,BooksOrder.BookId,BooksOrder.AddressId,BooksOrder.BookQuantity,BooksOrder.TotalPrice,BooksOrder.OrderDate,Book.BookName,Book.AuthorName,
Book.BookDescription,Book.DiscountPrice,Book.OriginalPrice,Book.Rating,Book.Reviewer,
Book.Image,Book.BookCount from BooksOrder inner join Book on BooksOrder.BookId=Book.BookId
where BooksOrder.userId=@userId
End


create procedure sp_DeleteOrder
@OrderId int
As
Begin 
Delete BooksOrder where OrderId=@OrderId
End

---feedback--

create procedure sp_AddFeedBackBook
@FeedBackFromUserName varchar(15),
@Comments varchar(50),
@Ratings int,
@userId int,
@BookId int
As
Begin
Insert into FeedBackBook(FeedBackFromUserName,Comments,Ratings,userId,BookId) values 
 (@FeedBackFromUserName,@Comments,@Ratings,@userId,@BookId)
 End




 create procedure sp_GetAllFeedBack
 @BookId int
 As
 Begin
 select * from FeedBackBook where BookId=@BookId
 End