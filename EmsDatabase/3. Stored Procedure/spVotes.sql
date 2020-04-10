create procedure spVotes
@VotesId int,
@CandidateId int,
@EvmVote bit,
@PostalVote bit,
@ActionType varchar(50)
as
Begin
	if (@ActionType = 'Add')
		Begin
			Declare @CandidatePresentCount int

			select @CandidatePresentCount = Count(CandidateId)
			from Candidate
			where CandidateId = @CandidateId

			if (@CandidatePresentCount = 0)
				Begin
					set @CandidatePresentCount = -1
					return @CandidatePresentCount
				End
			else
				Begin
					insert into Votes(CandidateId, EvmVote, PostalVote, CreatedAt, ModifiedAt)
					values(@CandidateId, @EvmVote, @PostalVote, GETDATE(), GETDATE())

					select * from Votes where VotesId = IDENT_CURRENT('Votes')
				End
		End
	else if(@ActionType = 'Delete')
		Begin
			Delete from Votes where VotesId = @VotesId
		End

End