create procedure spCreateParty
@party varchar(50)
as
begin
	insert into Party(Name, CreatedAt, ModifiedAt) values(@party, GETDATE(), GETDATE())

	select * from Party where PartyId = IDENT_CURRENT('Party')
End

create procedure spUpdateParty
@PartyId int,
@UpdatePartyName varchar(50)
as
begin
	Update Party set Name = @UpdatePartyName where PartyId = @PartyId
End

create procedure spDeleteParty
@PartyId int
as
begin
	Delete from Party where PartyId = @PartyId
End