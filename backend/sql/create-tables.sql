USE TicketAppDb;

DROP TABLE IF EXISTS Tickets;

DROP TABLE IF EXISTS Projects;

CREATE TABLE Projects (
    Id          INT            IDENTITY (1, 1) PRIMARY KEY,
    Name        NVARCHAR (250) NOT NULL,
    Description NVARCHAR (500)
);

CREATE TABLE Tickets (
    Id          INT            IDENTITY (1, 1) PRIMARY KEY,
    Title       NVARCHAR (250) NOT NULL,
    Description NVARCHAR (500) NOT NULL,
    CreatedAt   DATETIME2      NOT NULL,
    DueDate     DATE          ,
    Status      NVARCHAR (20)  NOT NULL CONSTRAINT CK_Tickets_Status CHECK (Status IN ('Open', 'InProgress', 'Resolved', 'Closed')),
    Priority    NVARCHAR (20)  NOT NULL CONSTRAINT CK_Tickets_Priority CHECK (Priority IN ('Low', 'Medium', 'High', 'Critical')),
    ProjectId   INT            NOT NULL,
    FOREIGN KEY (ProjectId) REFERENCES Projects (Id)
);