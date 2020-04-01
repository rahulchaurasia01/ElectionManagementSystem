Create table Constituency (
	
	ConstituencyId Int Primary Key Identity(1,1),
	Name varchar(50) Not Null,
	City varchar(50) Not NUll,
	State varchar(50) Not Null,
	CreatedAt DateTime Not Null,
	ModifiedAt DateTime Not Null

);

Create table Party (
	
	PartyId Int Primary Key Identity(1,1),
	Name varchar(50) Not Null,
	CreatedAt DateTime Not Null,
	ModifiedAt DateTime Not Null

);

Create table Candidate (

	CandidateId Int Primary Key Identity(1,1),
	Name varchar(50) Not Null,
	ConstituencyId Int Not Null,
	PartyId Int Not Null,
	CreatedAt DateTime Not Null,
	ModifiedAt DateTime Not Null

	Constraint FK_Candidate_Constituency Foreign Key (ConstituencyId) References Constituency(ConstituencyId),
	Constraint FK_Candidate_Party Foreign Key (PartyId) References Party(PartyId)

);

Create table Votes (
	
	VotesId Int Primary Key Identity(1,1),
	CandidateId Int Not Null,
	EvmVote bit not Null,
	PostalVote bit not null,
	CreatedAt DateTime not null,
	ModifiedAt DateTime not null

	Constraint FK_Votes_Candidate Foreign Key (CandidateId) References Candidate(CandidateId)

);