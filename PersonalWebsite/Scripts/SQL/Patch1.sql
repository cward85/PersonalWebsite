USE CWARD

GO

DROP TABLE IF EXISTS subscribers

CREATE TABLE subscribers
(
	id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	emailAddress varchar(100),
	validationCode varchar(20),
	ipAddress varchar(20),
	subscribeDate datetime,
	validated bit
);