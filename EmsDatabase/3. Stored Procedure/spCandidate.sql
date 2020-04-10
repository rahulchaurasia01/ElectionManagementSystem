create procedure spCandidate
@CandidateId int,
@Name varchar(50),
@ConstituencyId int,
@PartyId int,
@ActionType varchar(50)
as
Begin
	If (@ActionType = 'Create')
		Begin
			Declare @CandidatePresentCount int

			select @CandidatePresentCount = Count(Name)
			from Constituency
			where ConstituencyId = @ConstituencyId

			if (@CandidatePresentCount = 0)
				Begin
					set @CandidatePresentCount = -1
					return @CandidatePresentCount
				End

			Select @CandidatePresentCount = Count(Name)
			from Party
			where PartyId = @PartyId

			if (@CandidatePresentCount = 0)
				Begin
					set @CandidatePresentCount = -2
					return @CandidatePresentCount
				End
			
			Select @CandidatePresentCount = Count(Name)
			from Candidate
			Where ConstituencyId = @ConstituencyId and PartyId = @PartyId

			If (@CandidatePresentCount = 0)
				Begin
					insert into Candidate(Name, ConstituencyId, PartyId, CreatedAt, ModifiedAt)
						Values(@Name, @ConstituencyId, @PartyId, GETDATE(), GETDATE())

					Select * from Candidate where CandidateId = IDENT_CURRENT('Candidate')

				End
			Else
				Begin
					set @CandidatePresentCount = -3
					return @CandidatePresentCount
				End
		End
	else if (@ActionType = 'GetAll')
		Begin
			select * from Candidate
		End
	else if (@ActionType = 'GetCandidateById')
		Begin
			select * from Candidate where CandidateId = @CandidateId
		End
	else if (@ActionType = 'Update')
		Begin
			Declare @CandidateNamePresentCount int

			select @CandidateNamePresentCount = Count(Name)
			from Candidate
			where CandidateId = @CandidateId

			if (@CandidateNamePresentCount = 0)
				Begin
					set @CandidateNamePresentCount = -1
					return @CandidateNamePresentCount
				End
			else
				Begin
					Update Candidate
					set Name = @Name, ModifiedAt = GETDATE()
					where CandidateId = @CandidateId

					Select * from Candidate where CandidateId = @CandidateId
				End
		End
	else if (@ActionType = 'Delete')
		Begin
			Delete from Candidate where CandidateId = @CandidateId
		End
End