create procedure spConstituency
@ConstituencyId int,
@Name varchar(50),
@CityId int,
@ActionType varchar(50)
as
Begin
	if (@ActionType = 'Add')
		Begin
			Declare @ConstituencyPresentCount int

			select @ConstituencyPresentCount = Count(*) from City Where CityId = @CityId

			if (@ConstituencyPresentCount = 0)
				Begin
					set @ConstituencyPresentCount = -1
					return @ConstituencyPresentCount
				End
			else
				Begin
					select @ConstituencyPresentCount = Count(Name) from Constituency
					where Name = @Name and CityId = @CityId

					if (@ConstituencyPresentCount > 0)
						Begin
							set @ConstituencyPresentCount = -2
							return @ConstituencyPresentCount
						End
					else
						Begin
							insert into Constituency(Name, CityId, CreatedAt, ModifiedAt)
							values(@Name, @CityId, GETDATE(), GETDATE())

							select * from Constituency where ConstituencyId = IDENT_CURRENT('Constituency')
						End
				End
		End
	else if (@ActionType = 'GetAll')
		Begin
			select * from Constituency 
		End
	else if (@ActionType = 'GetConstituencyById')
		Begin
			select * from Constituency where ConstituencyId = @ConstituencyId
		End
	else if (@ActionType = 'Update')
		Begin
			Declare @ConstituencyNamePresentCount int

			select @ConstituencyNamePresentCount = Count(*) from City Where CityId = @CityId

			if (@ConstituencyNamePresentCount = 0)
				Begin
					set @ConstituencyNamePresentCount = -1
					return @ConstituencyNamePresentCount
				End
			else
				Begin
					select @ConstituencyNamePresentCount = Count(Name) from Constituency
					where Name = @Name and CityId = @CityId

					if (@ConstituencyNamePresentCount > 0)
						Begin
							set @ConstituencyNamePresentCount = -2
							return @ConstituencyNamePresentCount
						End
					else
						Begin
							update Constituency
							set Name = @Name, CityId = @CityId, ModifiedAt = GETDATE()
							where ConstituencyId = @ConstituencyId

							select * from Constituency where ConstituencyId = @ConstituencyId
						End
				End
		End
	else if (@ActionType = 'Delete')
		Begin
			delete from Constituency where ConstituencyId = @ConstituencyId
		End
End