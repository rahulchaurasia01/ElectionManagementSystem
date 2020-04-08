create procedure spState
@StateId int,
@Name varchar(50),
@ActionType varchar(50)
as
Begin
	if (@ActionType = 'Add')
		Begin
			Declare @StatePresentCount int

			select @StatePresentCount = Count(Name) from State where Name = @Name

			if(@StatePresentCount = 0)
				Begin
					insert into State(Name, CreatedAt, ModifiedAt) values(@Name, GETDATE(), GETDATE())
					select * from State where StateId = IDENT_CURRENT('State')
				End
			else
				Begin
					return @StatePresentCount
				End
		End
	else if (@ActionType = 'GetAll')
		Begin
			select * from State
		End
	else if (@ActionType = 'GetById')
		Begin
			select * from State where StateId = @StateId
		End
	else if (@ActionType = 'Update')
		Begin
			Declare @StateNamePresentCount int

			select @StateNamePresentCount = Count(Name) from State where Name = @Name

			if(@StateNamePresentCount = 0)
				Begin
					Update State set Name = @Name, ModifiedAt = GETDATE() where StateId = @StateId
					select * from State where StateId = @StateId
				End
			else
				begin
					return @StateNamePresentCount
				End
		End
	else if (@ActionType = 'Delete')
		Begin
			Delete from State where StateId = @StateId
		End
End