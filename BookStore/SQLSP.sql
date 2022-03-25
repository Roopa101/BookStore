create procedure sp_UserRegister
@FullName varchar(10),
@EmailId varchar(25),
@PhNo varchar(20),
@Password varchar(20)
As
Begin
Insert into UserRegister (FullName,EmailId,PhNo,Password) values (@FullName,@EmailId,@PhNo,@Password)
End


create procedure sp_UserLogin
@EmailId varchar(25),
@Password varchar(20)
As
Begin
select EmailId,Password from UserRegister where EmailId=@EmailId AND Password=@Password
End



create procedure sp_ForgetPassword
@EmailId varchar(25)
As
Begin
select EmailId from UserRegister where EmailId=@EmailId 
End


alter procedure sp_ForgetPassword

(
@EmailId varchar(50)
)

as
begin try
     update UserRegister set Password=null where EmailId=@EmailId
end try
begin catch
  select
    ERROR_NUMBER() as ErrorNumber,
    ERROR_STATE() as ErrorState,
    ERROR_PROCEDURE() as ErrorProcedure,
    ERROR_LINE() as ErrorLine,
    ERROR_MESSAGE() as ErrorMessage;
end catch






create procedure sp_ResetPassword

@EmailId varchar(25),
@Password varchar(10)
As
Begin
update UserRegister set Password=@Password where EmailId=@EmailId
End