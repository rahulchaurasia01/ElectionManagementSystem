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
					select @CityPresentCount = Count(Name) from City where Name = @Name and StateId = @StateId

					if (@CityPresentCount > 0)
						Begin
							set @CityPresentCount = -2
							return @CityPresentCount
						End
					else
						Begin
							insert into City(Name, StateId, CreatedAt, ModifiedAt) values(@Name, @StateId, 
								GETDATE(), GETDATE())
							select * from City where CityId = IDENT_CURRENT('City')
						End
				End
		End
End