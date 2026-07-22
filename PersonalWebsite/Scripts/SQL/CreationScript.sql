USE CWARD

GO

DROP TABLE IF EXISTS blogTags
DROP TABLE IF EXISTS blogEntry
DROP TABLE IF EXISTS author

CREATE TABLE author
(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	userName varchar(100) NOT NULL,
	name varchar(100) NOT NULL,
	password varchar(100) NOT NULL,
	lastLoginDateTime datetime
);

INSERT INTO author (userName, name, password)
VALUES
	('cward', 'Camilo Ward', '$2a$11$lOrBN3RIbJy6478oh66jZuK4EQX9iYMcVlC3WEB2ughEWnMiT8vYC')

CREATE TABLE blogEntry
(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	authorId INT NOT NULL,
	blogEntryContent varchar(max) NOT NULL,
	blogEntryTitle varchar(500) NOT NULL,
	creationDate datetime NOT NULL	

	CONSTRAINT fk_author
		FOREIGN KEY (authorId)
		REFERENCES author(id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);

INSERT INTO blogEntry (authorId, blogEntryContent, blogEntryTitle, creationDate)
VALUES
	(1, 'As is tradition, I have once again changed how Transparency works in our little tech demo. Last time, I showed a screenshot of the two ways we were thinking of doing transitions from room to room. One method was gradually making objects/walls transparent so you could see the player''s character at all times, but you could still see the original objects themselves. The other method was making every other object/wall totally disappear if you were not in the appropriate room. And then reappear when you move into the appropriate room. It still felt a bit off, so I worked on another method.\n\nThe new and improved method involves a little of column A and a little of column B. Gradually making every other object except the ones in your current room disappear! It still leaves you surrounded by a black void, but it slowly makes the area around you disappear instead of doing it all at once. And the best part! I just reused the code for the transparent method since all I had to do was sent the transparency value to 0 as well as reusing the code for the disappearing method since that has us pass in a list of all objects you want to disappear. It''s just one code file to control anything you want to make totally transparent. If this is the method we end up going with, it''ll vastly simplify the setup of a new environment, since all I''d have to do is attach this script to a Polygon Collider 2D that defines basically the borders of a room and then pass in the objects that I want to disappear when the player enters that room. Even if we go back to the original Transparency method, I''ve learned enough to make that generic as well so I won''t have a million transparency scripts floating around that are one off uses.', 'Once again, making changes to Transparency', '2026-07-21 22:30:30.123'),
	(1, 'So, I previously talked about Transparency when it comes to sprites that make up a room (walls, chairs, tables, etc). Talking with my co-dev, we decided to try a different approach when it came to the player''s character making his way around an area with several rooms/objects.\n\nPreviously, we had any objects that would end up blocking the view of the player''s character fade to be halfway transparent so you could see where they were. This meant setting up colliders that covered the entire area where we wanted certain objects to become transparent. Some rooms would have multiple colliders that would handle different use cases depending on which view was blocked. It actually got quite tricky depending on the layout of the room. Sometimes you might enter another collider while you were still in the first one, which triggered another set of objects becoming transparent. And sometimes, some of those objects would also match objects from the first collider. So, you didn''t want the objects that belonged to both sets of colliders to become opaque when you left the first collider, since the 2nd collider still wanted some of those objects to be transparent. That was a fun little problem to solve.', 'Yay Iteration!', '2026-07-22 20:22:12.123'),
	(1, '3', '3', '2026-07-21 20:22:12.123'),
	(1, '4', '4', '2026-07-20 20:22:12.123'),
	(1, '5', '5', '2026-07-19 20:22:12.123'),
	(1, '6', '6', '2026-07-18 20:22:12.123'),
	(1, '7', '7', '2026-07-17 20:22:12.123'),
	(1, '8', '8', '2026-07-16 20:22:12.123'),
	(1, '9', '9', '2026-07-15 20:22:12.123'),
	(1, '10', '10', '2026-07-14 20:22:12.123'),
	(1, '11', '11', '2026-07-13 20:22:12.123'),
	(1, '12', '12', '2026-07-12 20:22:12.123'),
	(1, '13', '13', '2026-07-11 20:22:12.123')

CREATE TABLE blogTags
(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	blogEntryId INT NOT NULL,
	tag varchar(50) NOT NULL

	CONSTRAINT fk_blogEntry
		FOREIGN KEY (blogEntryId)
		REFERENCES blogEntry(id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);

INSERT INTO blogTags(blogEntryId, tag)
VALUES
	(1, 'test'),
	(1, 'entry'),
	(2, 'iteration'),
	(2, 'gamedev'),
	(3, 'gamedev'),
	(3, 'web dev'),
	(4, 'gamedev'),
	(4, 'test'),
	(5, 'gamedev'),
	(5, 'iteration'),
	(6, 'gamedev'),
	(6, 'journey'),
	(7, 'gamedev'),
	(7, 'numbers'),
	(8, 'gamedev'),
	(8, 'creativity'),
	(9, 'gamedev'),
	(10, 'gamedev'),
	(11, 'gamedev'),
	(12, 'gamedev'),
	(13, 'gamedev')