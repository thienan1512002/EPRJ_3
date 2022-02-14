Create  Database ClinicDB
go
use ClinicDB
go
Create table Brands
(
	BrandID varchar(10) primary key , 
	BrandName varchar(50)
)
go
Create table Medicine
(
	MedID varchar(10)  primary key ,
	MedName varchar(50),
	Type varchar(50),
	BrandID varchar(10) foreign key references Brands,
	Price decimal(18,2),
	Quantity int,
	Description text,
	DateCreate datetime,
	Featured bit,
	Image varchar(200)
)
go
Create table EquipmentForClinic
(
	EquipmentID varchar(10) primary key , 
	EquipmentName varchar(50) , 
	BrandID varchar(10) foreign key references Brands,
	Price int , 
	Quantity int ,
	Description text,
	DateCreate datetime,
	Image varchar(200)
)
go
Create table EquipmentForEcomerce
(
	EquipmentID varchar(10) primary key , 
	EquipmentName varchar(50) , 
	BrandID varchar(10) foreign key references Brands,
	Price int , 
	Quantity int ,
	Description text,
	DateCreate datetime,
	Image varchar(200)
)
go
Create table StaffAccount
(
	AccountID varchar(10) primary key , 
	Username varchar(20),
	Email varchar(30),
	Password varchar(30),
	Role varchar(30) not null check (Role in ('Manager','Staff')),
	Image varchar(200)
)
go
Create table Course 
(
	CourseID varchar(10) primary key , 
	CourseName varchar(50),
	Lectures varchar(50),
	Start_date datetime , 
	Location varchar(50),
	End_date datetime ,
	Slot int
)
go
Create table Enrollment
(
	EnrollmentID int identity primary key ,
	CourseID varchar(10) foreign key references Course , 
	AccountID varchar(10) foreign key references StaffAccount
)
go
Create table AdminOrder
(
	OrderID int identity primary key , 
	AccountID varchar(10) foreign key references StaffAccount,
	OrderDate datetime ,
	Status nvarchar(20) not null check (Status in('Finished','Not Yet'))
)
go
Create table AdminOrderDetails
(
	OrderID int identity primary key ,
	OrderDetailID int foreign key references AdminOrder,
	EquipmentID varchar(10) foreign key references EquipmentForClinic ,
	Quantity int , 
	Purpose varchar(200)
)
go
Create table CustomerAccount
(
	CustomerID varchar(50) primary key , 
	CustomerName varchar(50),
	Email varchar(50),
	Password varchar(50),
	Phone varchar(20),
	Address varchar(50),
	Status varchar(20) not null check (Status in('Block','Available'))
)
go
Create table EcomerceOrder
(
	OrderID int identity primary key , 
	CustomerID varchar(50) foreign key references CustomerAccount,
	OrderDate datetime,
	Address varchar(50),
	Status varchar(20) not null check (Status in('Pending','Completed','Decline'))
)
go
Create table EcomerceMedOrderDetail
(
	OrderID int identity primary key , 
	OrderDetailID int foreign key references EcomerceOrder ,
	MedID varchar(10) foreign key references Medicine ,
	Quantity int,
	Total int
)
go
Create table EcomerceEquipDetail
(
	OrderID int identity primary key , 
	OrderDetailID int foreign key references EcomerceOrder ,
	EquipmentID varchar(10) foreign key references EquipmentForEcomerce ,
	Quantity int,
	Total int
)
go
Create table Feedback
(
	FeedbackID int primary key identity , 
	CustomerID varchar(50) foreign key references CustomerAccount,
	Content varchar(300),
	DateCreate datetime,
	Reply varchar(300)
)
go
--Seeding first account for debug
Insert into StaffAccount(AccountID,Username,Password,Email,Role) values ('Account001','Manager','1','manager@gmail.com','Manager')