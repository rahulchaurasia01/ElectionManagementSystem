create procedure spCandidate
@Name varchar(50),
@ConstituencyId int,
@PartyId int,
@ActionType varchar(50)
as
Begin
	If (@ActionType = 'Create')
		Begin
			Declare @CandidatePresentCount int

			select @CandidatePresentCount = Count(Name) from Constituency
				where ConstituencyId = @ConstituencyId

			print @CandidatePresentCount

			Select @CandidatePresentCount = Count(Name) from Candidate
				Where ConstituencyId = @ConstituencyId and PartyId = @PartyId
			
			If (@CandidatePresentCount = 0)
				Begin
					insert into Candidate(Name, ConstituencyId, PartyId, CreatedAt, ModifiedAt)
						Values(@Name, @ConstituencyId, @PartyId, GETDATE(), GETDATE())

					Select * from Candidate where CandidateId = IDENT_CURRENT('Candidate')

				End
			Else
				Begin
					return @CandidatePresentCount
				End
		End

End