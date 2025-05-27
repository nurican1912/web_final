USE EmployeeManagementDb;
GO


CREATE TABLE Departments (
    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO


CREATE TABLE Positions (
    PositionId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO


CREATE TABLE Employees (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    BirthDate DATE NOT NULL,
    Salary DECIMAL(18,2) NOT NULL,
    DepartmentId INT NOT NULL,
    PositionId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId),
    FOREIGN KEY (PositionId) REFERENCES Positions(PositionId)
);
GO


CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL
);
GO

INSERT INTO Departments (Name) VALUES
('Human Resources'),
('Information Technology'),
('Accounting'),
('Sales'),
('Product Management');

GO


INSERT INTO Positions (Name) VALUES
('Manager'),
('Junior Specialist'),
('Intern'),
('Senior Specialist'),
('Team Leader');

GO

INSERT INTO Users (Username, Password) VALUES
('nurican', '1234');
GO
INSERT INTO Users (Username, Password) VALUES
('mirzahan', '1234');
GO
INSERT INTO Users (Username, Password) VALUES
('gozde', '1234');
GO