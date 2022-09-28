alter table 
Booking 
add AID int,foreign key (AID) references Account(Id);

update Booking set AID=75326 where Id=7531;