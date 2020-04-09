create procedure spCity
@CityId int,
@Name varchar(50),
@StateId int,
@ActionType varchar(50)
as
Begin
	If (@ActionType = 'Add')
		Begin
			Declare @CityPresentCount int

			select @CityPresentCount = Count(*) from State Where StateId = @StateId

			if (@CityPresentCount = 0)
				Begin
					set @CityPresentCount = -1
					return @CityPresentCount
				End
			else
				Begin
					select @CityPresentCount = Count(Name) from City
					where Name = @Name and StateId = @StateId

					if (@CityPresentCount > 0)
						Begin
							set @CityPresentCount = -2
							return @CityPresentCount
						End
					else
						Begin
							insert into City(Name, StateId, CreatedAt, ModifiedAt)
							values(@Name, @StateId, GETDATE(), GETDATE())

							select * from City where CityId = IDENT_CURRENT('City')
						End
				End
		End
	else if (@ActionType = 'GetAllCity')
		Begin
			select * from City 
		End
	else if (@ActionType = 'GetCityById')
		Begin
			select * from City where CityId = @CityId 
		End
	else if (@ActionType = 'GetAllCityWithStateName')
		Begin
			select CityId, city.Name 'City Name' , state.Name as 'State Name', city.CreatedAt, city.ModifiedAt
			from City as city
			inner join State as state
			on city.StateId = state.StateId
		End
	else if (@ActionType = 'GetCityWithStateNameByCityId')
		Begin
			select CityId, city.Name 'City Name' , state.Name as 'State Name', city.CreatedAt, city.ModifiedAt
			from City as city
			inner join State as state
			on city.StateId = state.StateId
			where CityId = @CityId
		End
	 else if (@ActionType = 'Update')
		Begin
			Declare @CityNamePresentCount int

			select @CityNamePresentCount = Count(*) from State Where StateId = @StateId

			if (@CityNamePresentCount = 0)
				Begin
					set @CityNamePresentCount = -1
					return @CityNamePresentCount
				End
			else
				Begin
					select @CityNamePresentCount = Count(Name) from City
					where Name = @Name and StateId = @StateId

					if (@CityNamePresentCount > 0)
						Begin
							set @CityNamePresentCount = -2
							return @CityNamePresentCount
						End
					else
						Begin
							update City
							set Name = @Name, StateId = @StateId, ModifiedAt = GETDATE()
							where CityId = @CityId

							select * from City where CityId = @CityId
						End
				End
		End
	else if (@ActionType = 'Delete')
		Begin
			delete from City where CityId = @CityId
		End
End