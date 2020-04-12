create procedure spUser
@StateName varchar(50),
@ConstituencyId varchar(50),
@ActionType varchar(50)
as
Begin
	if (@ActionType = 'ConstituencyWise')
		Begin
			Declare @ConstituencyPresent int

			select @ConstituencyPresent = Count(ConstituencyId) from Constituency
			where ConstituencyId = @ConstituencyId

			if (@ConstituencyPresent = 0)
				Begin
					set @ConstituencyPresent = -1
					return @ConstituencyPresent
				End
			else
				Begin
					select CandidateDetail.[Candidate Name], CandidateDetail.[Party Name],
					COUNT(case when Votes.EvmVote = 1 then 1 else null end) as 'Evm Vote',
					COUNT(case when Votes.PostalVote = 1 then 1 else null end) as 'Postal Vote'
					from Votes 
					inner join (
						select Candidate.CandidateId, Candidate.Name as 'Candidate Name', Party.Name 'Party Name'
						from Candidate as Candidate
						inner join Party as Party
						on Candidate.PartyId = Party.PartyId
						where ConstituencyId = @ConstituencyId) as CandidateDetail
					on Votes.CandidateId = CandidateDetail.CandidateId
					group by CandidateDetail.[Candidate Name], CandidateDetail.[Party Name]
				End
		End
End