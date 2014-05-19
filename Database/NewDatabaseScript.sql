IF OBJECT_ID('[dbo].[JobsAppliedFor]', 'U') IS NOT NULL
DROP TABLE [dbo].[JobsAppliedFor];
IF OBJECT_ID('[dbo].[ApplicantAuth]', 'U') IS NOT NULL
DROP TABLE [dbo].[ApplicantAuth];
IF OBJECT_ID('[dbo].[Notes]', 'U') IS NOT NULL
DROP TABLE [dbo].[Notes];
IF OBJECT_ID('[dbo].[Availability]', 'U') IS NOT NULL
DROP TABLE [dbo].[Availability];
IF OBJECT_ID('[dbo].[Education]', 'U') IS NOT NULL
DROP TABLE [dbo].[Education];
IF OBJECT_ID('[dbo].[References]', 'U') IS NOT NULL
DROP TABLE [dbo].[References];
IF OBJECT_ID('[dbo].[ElectronicSig]', 'U') IS NOT NULL
DROP TABLE [dbo].[ElectronicSig];
IF OBJECT_ID('[dbo].[JobHistory]', 'U') IS NOT NULL
DROP TABLE [dbo].[JobHistory];
IF OBJECT_ID('[dbo].[Questionaire]', 'U') IS NOT NULL
DROP TABLE [dbo].[Questionaire];
IF OBJECT_ID('[dbo].[Question]', 'U') IS NOT NULL
DROP TABLE [dbo].[Question];
IF OBJECT_ID('[dbo].[Application]', 'U') IS NOT NULL
DROP TABLE [dbo].[Application];
IF OBJECT_ID('[dbo].[AvailablePosition]', 'U') IS NOT NULL
DROP TABLE [dbo].[AvailablePosition];
IF OBJECT_ID('[dbo].[Position]', 'U') IS NOT NULL
DROP TABLE [dbo].[Position];
IF OBJECT_ID('[dbo].[Store]', 'U') IS NOT NULL
DROP TABLE [dbo].[Store];
IF OBJECT_ID('[dbo].[PersonalInfo]', 'U') IS NOT NULL
DROP TABLE [dbo].[PersonalInfo];
IF OBJECT_ID('[dbo].[Managers]', 'U') IS NOT NULL
DROP TABLE [dbo].[Managers];



CREATE TABLE [dbo].[PersonalInfo]
(
	[applicantId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [firstName] VARCHAR(50) NOT NULL, 
    [middleName] VARCHAR(50) NULL, 
    [lastName] VARCHAR(50) NOT NULL, 
    [socialNum] VARCHAR(16) NOT NULL UNIQUE, 
    [street] VARCHAR(50) NOT NULL,
	[city] VARCHAR(50) NOT NULL,
	[email] VARCHAR(50) NULL,
	[state] CHAR(2) NOT NULL,
	[zip] VARCHAR(10) NOT NULL,
    [Phone] VARCHAR(16) NULL, 
    [alias] VARCHAR(50) NULL
);

CREATE TABLE [dbo].[Store]
(
	[storeId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [name] VARCHAR(50) NOT NULL, 
    [street] VARCHAR(50) NOT NULL, 
    [city] VARCHAR(50) NOT NULL,
	[state] CHAR(2) NOT NULL,
	[zip] VARCHAR(10) NOT NULL
);

INSERT INTO [dbo].[Store] VALUES ('West Wilsonville Store', '1234 Road St', 'Wilsonville', 'OR', '93252');
INSERT INTO [dbo].[Store] VALUES ('East Wilsonville Store', '3678 High St', 'Wilsonville', 'OR', '93252');
INSERT INTO [dbo].[Store] VALUES ('West Portland Store', '786 Stree st', 'Portland', 'OR', '97006');

CREATE TABLE [dbo].[Position]
(
	[positionId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [title] VARCHAR(50) NOT NULL, 
	[requirements] VARCHAR (2000) NULL,
	[education] VARCHAR (255) NULL,
    [description] VARCHAR(2000) NOT NULL, 
    [pay] VARCHAR(50) NOT NULL,
);

INSERT INTO [dbo].[Position] VALUES ('Cashier','none','none', 'Greet customers as they arrive at the store and provide them with information 
Respond to customers’ records and resolve their issues
Take payment in exchange of items sold
Bag, box or wrap purchased items
Enter transactions in the cash register and provide customers with the total
Identify prices of goods using memory or scanner
Issue receipts and change to customers
', '$10/hr');

INSERT INTO [dbo].[Position] VALUES ('Custodian', 'none','none' ,'
Clean building floors by sweeping, mopping, scrubbing, or vacuuming them.
Gather and empty trash.
Service, clean, and supply restrooms.
Clean and polish furniture and fixtures.
Clean windows, glass partitions, and mirrors, using soapy water or other cleaners, sponges, and squeegees.
Dust furniture, walls, machines, and equipment.
', '$9.80/hr');

INSERT INTO [dbo].[Position] VALUES ('Assistant Manager','none','none', '
Ensure the business operations run smoothly in the shop
Keeping on track to meet the monthly budgets and targets
Provide training for staff and new recruitments
Provide excellent customer service
Prepared to attend to urgent customers when required
Planning rosters and duties for staff
Monitoring the displays of products on shelves
Handling the deliveries, checking quality, expiry date, price and correct quantity
', '$15/hr');

CREATE TABLE [dbo].[AvailablePosition]
(
	[availablePosId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [storeId] INT NOT NULL, 
    [positionId] INT NOT NULL, 
	[mondayFrom] TIME NULL,
	[mondayTo] TIME NULL,
	[tuesdayFrom] TIME NULL,
	[tuesdayTo] TIME NULL,
	[wednesdayFrom] TIME NULL,
	[wednesdayTo] TIME NULL,
	[thursdayFrom] TIME NULL,
	[thursdayTo] TIME NULL,
	[fridayFrom] TIME NULL,
	[fridayTo] TIME NULL,
	[saturdayFrom] TIME NULL,
	[saturdayTo] TIME NULL,
	[sundayFrom] TIME NULL,
	[sundayTo] TIME NULL,
	CONSTRAINT [FK_Store_AvailablePosition] FOREIGN KEY (storeId) REFERENCES [dbo].[Store] ([storeId]),
    CONSTRAINT [FK_Position_AvailablePosition] FOREIGN KEY (positionId) REFERENCES [dbo].[Position] ([positionId])
);

INSERT INTO [dbo].[AvailablePosition] VALUES ('1', '1', '06:00:00am', '03:00:00pm', NULL, NULL, NULL,NULL,NULL,NULL,NULL, NULL,NULL,NULL,NULL, NULL);
INSERT INTO [dbo].[AvailablePosition] VALUES ('1', '2', NULL, NULL, NULL, NULL, NULL,NULL,NULL,NULL,NULL, NULL,NULL,NULL,NULL, NULL);
INSERT INTO [dbo].[AvailablePosition] VALUES ('1', '3', NULL, NULL, NULL, NULL, NULL,NULL,NULL,NULL,NULL, NULL,NULL,NULL,NULL, NULL);
INSERT INTO [dbo].[AvailablePosition] VALUES ('2', '3', NULL, NULL, NULL, NULL, NULL,NULL,NULL,NULL,NULL, NULL,NULL,NULL,NULL, NULL);

CREATE TABLE [dbo].[Application]
(
	[applicantId] INT NOT NULL,
    [availablePosId] INT NOT NULL,
	[storeId] INT NOT NULL,
	CONSTRAINT [FK_PersonalInfo_Application] FOREIGN KEY (applicantId) REFERENCES [dbo].[PersonalInfo] ([applicantId]),
    CONSTRAINT [FK_AvailablePosition_Application] FOREIGN KEY (availablePosId) REFERENCES [dbo].[AvailablePosition] ([availablePosId]),
    CONSTRAINT [FK_Store_Application] FOREIGN KEY (storeId) REFERENCES [dbo].[Store] ([storeId]),  
    PRIMARY KEY CLUSTERED ([applicantId] ASC, [availablePosId] ASC)
);

CREATE TABLE [dbo].[Education]
(
	[applicantId] INT NOT NULL,
	[educationId] INT NOT NULL IDENTITY(1,1),
	[name] VARCHAR(50) NULL,
	[street] VARCHAR(50) NULL,
	[city] VARCHAR(50) NULL,
	[stateAbrev] CHAR(2) NULL,
	[zip] VARCHAR(10) NULL,
    [yearFrom] VARCHAR(4) NULL,
	[yearTo] VARCHAR(4) NULL,
    [graduatedYN] CHAR NULL, 
    [degreeMajor] VARCHAR(50) NULL,
    CONSTRAINT [FK_PersonalInfo_Education] FOREIGN KEY (applicantId) REFERENCES [dbo].[PersonalInfo] ([applicantId]), 
	PRIMARY KEY CLUSTERED ([applicantId] ASC, [educationId] ASC)
);

CREATE TABLE [dbo].[ElectronicSig]
(
	[applicantId] INT NOT NULL PRIMARY KEY,
    [date] DATETIME NULL,
	CONSTRAINT [FK_PersonalInfo_ElectronicSig] FOREIGN KEY (applicantId) REFERENCES [dbo].[PersonalInfo] ([applicantId])
);

CREATE TABLE [dbo].[JobHistory]
(
	[applicantId] INT NOT NULL,
	[jobHistoryId] INT NOT NULL IDENTITY(1,1), 
    [employer] VARCHAR(50) NULL, 
    [fromDate] DATE NULL, 
    [toDate] DATE NULL, 
    [street] VARCHAR(50) NULL,
	[city] VARCHAR(50) NULL,
	[stateAbrev] CHAR(2) NULL,
	[zip] VARCHAR(10),
    [phone] VARCHAR(16) NULL, 
    [supervisor] VARCHAR(50) NULL, 
    [position] VARCHAR(50) NULL, 
    [startSalary] VARCHAR(50) NULL, 
    [endSalary] VARCHAR(50) NULL, 
    [reasonLeave] VARCHAR(50) NULL, 
    [duties] VARCHAR(2000) NULL,
	PRIMARY KEY CLUSTERED ([applicantId] ASC, [jobHistoryId] ASC),
	CONSTRAINT [FK_PersonalInfo_JobHistory] FOREIGN KEY (applicantId) REFERENCES [dbo].[PersonalInfo] ([applicantId])
);

CREATE TABLE [dbo].[Question]
(
	[questionId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [theQuestion] VARCHAR(500) NOT NULL UNIQUE, 
    [theAnswer] NCHAR(1) NOT NULL
);

INSERT INTO [dbo].[Question] VALUES ('Can you lift 50 lbs?', 'y');
INSERT INTO [dbo].[Question] VALUES ('Can you stand for over 8 hours', 'y');
INSERT INTO [dbo].[Question] VALUES ('Do you have transportation?', 'y');

CREATE TABLE [dbo].[Questionaire]
(
	[questionaireId] INT NOT NULL IDENTITY(1,1),
	[questionId] INT NOT NULL, 
    [positionId] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([questionId] ASC, [positionId] ASC),
    CONSTRAINT [FK_Question_Questionaire] FOREIGN KEY (questionId) REFERENCES [dbo].[Question] ([questionId]),
    CONSTRAINT [FK_Position_Questionaire] FOREIGN KEY (positionId) REFERENCES [dbo].[Position] ([positionId])
);

INSERT INTO [dbo].[Questionaire] VALUES (1, 1);
INSERT INTO [dbo].[Questionaire] VALUES (2, 1);
INSERT INTO [dbo].[Questionaire] VALUES (3, 1);
INSERT INTO [dbo].[Questionaire] VALUES (1, 2);
INSERT INTO [dbo].[Questionaire] VALUES (2, 2);
INSERT INTO [dbo].[Questionaire] VALUES (3, 2);

CREATE TABLE [dbo].[References]
(
	[applicantId] INT NOT NULL,
	[referenceId] INT NOT NULL IDENTITY(1,1),
    [name] VARCHAR(50) NULL, 
    [phone] VARCHAR(16) NULL, 
    [company] VARCHAR(50) NULL, 
    [title] VARCHAR(50) NULL,
	CONSTRAINT [FK_PersonalInfo_References] FOREIGN KEY (applicantId) REFERENCES [dbo].[PersonalInfo] ([applicantId]), 
	PRIMARY KEY CLUSTERED ([applicantId] ASC, [referenceId] ASC)
);

CREATE TABLE [dbo].[JobsAppliedFor]
(
	[appId] INT NOT NULL,
	[posId] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([appId] ASC, [posId] ASC)
);

CREATE TABLE [dbo].[Notes]
(
	[applicantId] INT NOT NULL PRIMARY KEY,
	[personalInfoNotes] VARCHAR(3000) NULL,
	[educationNotes] VARCHAR(3000) NULL,
	[availabilityNotes] VARCHAR(3000) NULL,
	[referenceNotes] VARCHAR(3000) NULL,
	[jobHistoryNotes] VARCHAR(3000) NULL,
	CONSTRAINT [FK_PersonalInfo_Notes] FOREIGN KEY (applicantId) REFERENCES [dbo].[PersonalInfo] ([applicantId])
);

CREATE TABLE [dbo].[Availability]
(
	[applicantId] INT NOT NULL PRIMARY KEY,
	[salaryExpected] VARCHAR(50) NULL,
	[fullTimeYN] CHAR NULL,
	[daysYN] CHAR NULL,
	[eveningsYN] CHAR NULL,
	[weekendsYN] CHAR NULL,
	[mondayFrom] TIME NULL,
	[mondayTo] TIME NULL,
	[tuesdayFrom] TIME NULL,
	[tuesdayTo] TIME NULL,
	[wednesdayFrom] TIME NULL,
	[wednesdayTo] TIME NULL,
	[thursdayFrom] TIME NULL,
	[thursdayTo] TIME NULL,
	[fridayFrom] TIME NULL,
	[fridayTo] TIME NULL,
	[saturdayFrom] TIME NULL,
	[saturdayTo] TIME NULL,
	[sundayFrom] TIME NULL,
	[sundayTo] TIME NULL,
	CONSTRAINT [FK_PersonalInfo_Availability] FOREIGN KEY (applicantId) REFERENCES [dbo].[PersonalInfo] ([applicantId])
);

CREATE TABLE [dbo].[Managers]
(
	[userName] VARCHAR(50) NOT NULL PRIMARY KEY,
	[password] VARCHAR(50) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
	[permission] VARCHAR(50) NOT NULL
);

INSERT INTO [dbo].[Managers] VALUES ('Logan', '1', 'Admin');
INSERT INTO [dbo].[Managers] VALUES ('Keith', '1', 'Staffing Expert');
INSERT INTO [dbo].[Managers] VALUES ('Chris', '1', 'Hiring Manager');
INSERT INTO [dbo].[Managers] VALUES ('Dustin', '1', 'Phone Screener');

CREATE TABLE [dbo].[ApplicantAuth]
(
	[applicantId] INT NOT NULL PRIMARY KEY,
	[password] VARCHAR(50) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
	CONSTRAINT [FK_PersonalInfo_ApplicantAuth] FOREIGN KEY (applicantId) REFERENCES [dbo].[PersonalInfo] ([applicantId])
);