create procedure spParty
@PartyId int,
@Name varchar(50),
@ActionType varchar(50)
as
begin
	if (@ActionType = 'Create')
		Begin
			Declare @PartyPresentCount int

			select @PartyPresentCount = Count(Name) from Party where Name = @Name

			if(@PartyPresentCount = 0)
				Begin
					insert into Party(Name, CreatedAt, ModifiedAt) values(@Name, GETDATE(), GETDATE())
					select * from Party where PartyId = IDENT_CURRENT('Party')
				End
			Else
				Begin
					return @PartyPresentCount
				End
		End
	else if (@ActionType = 'GetAll')
		Begin
			Select * from Party
		End
	else if (@ActionType = 'GetById')
		Begin
			select * from Party where PartyId = @PartyId
		End
	else if(@ActionType = 'Update')
		Begin
			Declare @PartyNamePresentCount int

			select @PartyNamePresentCount = Count(Name) from Party where Name = @Name

			if(@PartyNamePresentCount = 0)
				begin
					Update Party set Name = @Name where PartyId = @PartyId
					select * from Party where PartyId = @PartyId
				End
			else
				begin
					return @PartyNamePresentCount
				End
		End
	else if(@ActionType = 'Delete')
		Begin
			Delete from Party where PartyId = @PartyId
		End
End