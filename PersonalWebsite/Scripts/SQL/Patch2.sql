USE CWARD

GO

DROP TABLE IF EXISTS resumeDownloads

DROP TABLE IF EXISTS blogEntryComments

CREATE TABLE resumeDownloads
(
	id int NOT NULL PRIMARY KEY IDENTITY(1,1),	
	ipAddress varchar(20),
	downloadDate datetime	
);

CREATE TABLE blogEntryComments
(
	id int NOT NULL PRIMARY KEY IDENTITY(1,1),	
	blogId int,
	ipAddress varchar(20),
	comment varchar(max),
	displayName varchar(100),
	emailAddress varchar(100),
	commentDate datetime,
	activeFlag bit,

	CONSTRAINT fk_blogId
		FOREIGN KEY (blogId)
		REFERENCES blogEntry(id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);