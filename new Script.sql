create database Library_Management_System;

create Table Library_Table 
(
	Library_ID int,
	Library_Name varchar(50) Not Null,
	Addres varchar(100) not null,
	Contact_Number varchar(20) not null,
	E_Mail varchar(25)
	Constraint Library_pk Primary Key(Library_ID)
);

insert into Library_Table
values (1,'NTU Library','NTU,Faisalabad','041-4284923','ntu.lib@gmail.com');

create table Inventory_Table
(
	Item_Code int ,
	Item_Name varchar(50),
	Total_Iteams int,
	Constraint Inventory_PK Primary Key(Item_Code)
);

insert into Inventory_Table
values (1,'Computers',36),
       (2,'Tables',70),
	   (3,'Chairs',509),
	   (4,'Shelf',30)






create Table Book_Table
(
	Book_ISBN varchar(50),
	Book_Title varchar(50) not null,
	Author_Name varchar(50) not null,
	Pages int not null,
	Price int not null,
	Copies int not null,
	Constraint Book_pk Primary Key(Book_ISBN)
);




Create table Admin_Table
(
	Admin_ID varchar(50),
	Admin_Name varchar(50) not null,
	Admin_Password varchar(20) not null,
	Admin_Address varchar(50),
	Phone_Number varchar(11) not null
	Constraint Admin_Table_PK Primary Key(Admin_ID)
);


 

Create table Department_Table
(
	Department_Name varchar(50) not null
	Constraint Department_PK Primary Key (Department_Name)
);

  insert into Department_Table values('Computer Science');


create table Student_Table
( 
	Registration_Number varchar(15),
	First_Name varchar(15) not null,
	Second_Name varchar(15),
	Phone_Number varchar (12) not null,
	Student_Address varchar (40),
	Department varchar(50),
	CNIC Varchar(15) Unique not null,
	Constraint Student_pk Primary Key(Registration_Number),
	Constraint Student_fk Foreign key(Department)
	References Department_Table (Department_Name)
);
	

Create table IssueBook_Table
(
	Registration_Number varchar(15),
	Book_ISBN varchar(50),
	Admin_ID varchar(50) not null,
	Book_Issue_Date Date default getdate() not null,
	Return_Book_Date Date default getdate() not null
	Constraint Issue_Book_pk Primary Key(Registration_Number,Book_ISBN)
	Constraint Issue_Book_fk_1 Foreign key(Registration_Number)
	References Student_Table (Registration_Number)
	On update Cascade 
	on delete cascade,
	Constraint Issue_Book_fk_2 Foreign key(Book_ISBN)
	References Book_Table (Book_ISBN)
	On update Cascade 
	on delete Cascade,
	Constraint Issue_Book_fk_3 Foreign key(Admin_ID)
	References Admin_Table (Admin_ID)
	On update Cascade 
	on delete Cascade,
);
        
	

create Table Fine_Table
(
	Registration_Number varchar(15),
	Book_ISBN varchar(50),
	Admin_ID varchar(50) not null,
	Fine_Price int not null,
	Book_Issue_Date Date default getdate() not null,
	Return_Book_Date Date default getdate() not null
	Constraint Fine_pk Primary Key(Registration_Number,Book_ISBN)
	Constraint Fine_fk_1 Foreign key(Registration_Number)
	References Student_Table (Registration_Number)
	on update cascade
	on delete cascade,
	Constraint Fine_fk_2 Foreign key(Book_ISBN)
	References Book_Table (Book_ISBN)
	On update Cascade 
	on delete Cascade,
	Constraint Fine_fk_3 Foreign key(Admin_ID)
	References Admin_Table (Admin_ID)
	On update Cascade 
	on delete Cascade,
);

 
 

create table ActivityLog_Table
(
	Activity_ID int IDENTITY(1,1),
	Activity_Date Date default getdate() not null,
	Activity_Performed varchar(max) not null
	Constraint Activity_Log_pk Primary key(Activity_ID)
);

create table Vendor_Table
(
    Vendor_ID int,
	Vendor_Name varchar(50) not null,
	Vendor_Address varchar(100),
	Phone_Number varchar(20) not null
	Constraint Vendor_pk Primary Key(Vendor_ID)
);

	

create table SupplyBook_Table
(
    Supply_ID int IDENTITY(1,1),
    Vendor_ID int not null,
	Book_ISBN varchar(50) not null,
	Supply_Date Date default getdate() not null,
	Total_Copies int,
	Total_Amount int,
	Constraint Supply_Book_fk_1 Foreign Key(Book_ISBN)
	References Book_Table (Book_ISBN),
	Constraint Supply_Book_fk_2 Foreign Key(Vendor_ID)
	References Vendor_Table (Vendor_ID),
	Constraint Supply_Book_pk Primary Key(Supply_ID),
);

		
select * from Library_Table;
select * from Inventory_Table;
select * from ActivityLog_Table;
select * from Book_Table;
select * from Student_Table;
select * from Fine_Table;
select * from IssueBook_Table;
select * from Department_Table;
select * from Vendor_Table;
select * from Admin_Table;
select * from SupplyBook_Table;