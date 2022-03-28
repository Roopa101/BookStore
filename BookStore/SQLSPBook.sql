alter procedure sp_AddingBooks
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
		DELETE FROM Wishlist WHERE WishlistId = @WishlistId
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