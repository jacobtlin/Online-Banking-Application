create database bankingAdminDB
use bankingAdminDB

create table adminInfo
(
	adminNo int primary key,
	adminName varchar(20),
	adminType varchar(20),
	adminIsActive bit,
)


select * from adminInfo
--------------------------------------------------------------------------------------------------
create table adminLogin
(
	adminUsername varchar(20),
	adminPassword varchar(20),
	adminStatus varchar(20) default 'Active',
	adminAttempts int default (0),

	constraint pk_username primary key(adminUsername),
	constraint chk_password_len check(len(adminPassword) > 8),
	constraint chk_accountsStatus_list check (adminStatus in ('Active', 'Locked', 'Disabled')),
)


update adminLogin set adminAttempts = 3

select * from adminLogin
--------------------------------------------------------------------------------------------------

create table customerLogin
(
	customerUsername varchar(20),
	customerPassword varchar(20),
	customerStatus varchar(20) default 'Active',
	customerAttempts int default(0),

	constraint prk_username primary key(customerUsername),
	constraint ck_password_len check(len(customerPassword) > 8),
	constraint ck_accountStatus_list check (customerStatus in ('Active', 'Locked', 'Frozen')),
)

update customerLogin set customerAttempts = 3
update customerLogin set customerUsername = 'Joey' where customerUsername = 'Joe'

select * from customerLogin
----------------------------------------------------------------------------------------------------

create table customerAccounts
(
	customerNo int primary key not null,
	customerName varchar(20) not null,
	customerAccType varchar(20) not null,
	customerAccCheckBalance int not null,
	customerAccSaveBalance int not null,
	customerAccLoanBalance int not null,
	customerAccStatus varchar(20) default 'Active',

	constraint chk_customerName check (len(customerName) > 3),
	constraint chk_customerAccType check (customerAccType in ('Guest', 'Admin')),
	constraint chk_customerAccCheckBalance check (customerAccCheckBalance > 100),
	constraint chk_customerAccStatus_list check (customerAccStatus in ('Active', 'Locked', 'Frozen'))
)


drop table customerAccounts
select * from customerAccounts

-----------------------------------------------------------------------------------------------------------
create table TransactionInfo
(
	TransferID int primary key identity,
	TimeOfTransfer datetime,
	FromAccount int,
	ToAccount int,
	Amount int,
	TranserredBy varchar(20),

	constraint fk_fromAccount foreign key(FromAccount) references customerAccounts,
	constraint fk_toAccount foreign key(ToAccount) references customerAccounts
)

select * from customerAccounts

update customerAccounts set customerAccCheckBalance = customerAccCheckBalance - 2000 where customerNo = 1
update customerAccounts set customerAccCheckBalance = customerAccCheckBalance + 2000 where customerNo = 1
insert into TransactionInfo values(GETDATE(),1,2,2000,'Admin')

drop table TransactionInfo
select * from customerAccounts
select * from TransactionInfo