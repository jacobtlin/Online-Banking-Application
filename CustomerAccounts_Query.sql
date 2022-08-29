create database customerLoginDB
use customerLoginDB

create table table_customerLogin
(
	customerUsername varchar(20),
	customerPassword varchar(20),
	customerStatus varchar(20) default 'Active',
	customerAttempts int,

	constraint pk_username primary key(customerUsername),
	constraint chk_password_len check(len(customerPassword) > 8),
	constraint chk_accountStatus_list check (customerStatus in ('Active', 'Deactivated', 'Frozen')),
)
insert into table_customerLogin(customerUsername, customerPassword) values('Jacob', 'Password123')
insert into table_customerLogin(customerUsername, customerPassword) values('Nikhil', 'Password321')
insert into table_customerLogin(customerUsername, customerPassword) values('Jonathan', 'Password456')
update table_customerLogin set customerAttempts = 3

select * from table_customerLogin


-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------

create database bankingCustomerDB
use bankingCustomerDB

create table customerAccounts
(
	customerNo int,
	customerName varchar(20) not null,
	customerAccType varchar(20) not null,
	customerAccBalance int not null,
	customerAccStatus varchar(20) default 'Active',

	constraint chk_customerName check (len(customerName) > 3),
	constraint chk_customerAccType check (customerAccType in ('Checking', 'Savings', 'Loans')),
	constraint chk_customerAccBalance check (customerAccBalance > 100),
	constraint chk_customerAccStatus_list check (customerAccStatus in ('Active', 'Deactivated', 'Frozen'))
)

insert into customerAccounts values(01, 'Jacob', 'Checking', 42000, 'Active')
insert into customerAccounts values(01, 'Jacob', 'Savings', 12500, 'Active')
insert into customerAccounts values(01, 'Jacob', 'Loans', 3300, 'Active')

insert into customerAccounts values(02, 'Nikhil', 'Checking', 85000, 'Active') 
insert into customerAccounts values(02, 'Nikhil', 'Savings', 42200, 'Active')
insert into customerAccounts values(02, 'Nikhil', 'Loans', 9100, 'Active')

insert into customerAccounts values(03, 'Jonathan', 'Checking', 92500, 'Active')
insert into customerAccounts values(03, 'Jonathan', 'Savings', 22500, 'Active')
insert into customerAccounts values(03, 'Jonathan', 'Loans', 5300, 'Active')

select * from customerAccounts

-----------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------

create table TransactionInfo
(
	transferNo int,
	calendar datetime,
	fromAccount int,
	toAccount int,
	Amount int,
	transerredBy varchar(20),

)

select * from table_customerAccounts

update table_customerAccounts set customerAccBalance = customerAccBalance - 2000 where customerNo = 102
update table_customerAccounts set customerAccBalance = customerAccBalance + 2000 where customerNo = 104
insert into TransactionInfo values(GETDATE(),102,104,2000,'Admin')

select * from TransactionInfo

