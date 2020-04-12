create procedure spAdmin
@EmailId varchar(50),
@Password nvarchar(50),
@ActionType varchar(50)
as
Begin
	if (@ActionType = 'Create')
		Begin
			Declare @EmailPresent int

			select @EmailPresent = Count(EmailId) from Admin where EmailId = @EmailId

			if (@EmailPresent > 0)
				Begin
					set @EmailPresent = -1
					return @EmailPresent
				End
			else
				Begin
					insert into Admin(EmailId, Password, CreatedAt, ModifiedAt)
					values(@EmailId, @Password, GETDATE(), GETDATE())

					select AdminId, EmailId, CreatedAt, ModifiedAt from Admin where AdminId = IDENT_CURRENT('Admin')
				End

		End
	else if (@ActionType = 'Login')
		Begin
			Declare @adminPresent int

			select @adminPresent = COUNT(EmailId) from Admin where EmailId = @EmailId and Password = @Password

			if (@adminPresent = 0)
				Begin
					set @adminPresent = -1
					return @adminPresent
				End
			else 
				Begin
					select AdminId, EmailId, CreatedAt, ModifiedAt
					from Admin
					where EmailId = @EmailId and Password = @Password
				End

		End
End