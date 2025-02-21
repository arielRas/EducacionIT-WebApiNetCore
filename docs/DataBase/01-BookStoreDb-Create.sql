CREATE DATABASE BookStoreDb;
GO

USE BookStoreDb;


CREATE TABLE GENRE(
	CODE VARCHAR(5) NOT NULL,
	[NAME] VARCHAR(50) NOT NULL
	CONSTRAINT PK_GENRE PRIMARY KEY(CODE)
);

CREATE TABLE EDITION_TYPE(
	CODE VARCHAR(5) NOT NULL,
	[NAME] VARCHAR(50) NOT NULL
	CONSTRAINT PK_EDITION_TYPE PRIMARY KEY(CODE)
);

CREATE TABLE EDITORIAL(
	EDITORIAL_ID INT IDENTITY(1,1),
	[NAME] VARCHAR(50) NOT NULL,
	CONSTRAINT PK_EDITORIAL PRIMARY KEY(EDITORIAL_ID)
);

CREATE TABLE AUTHOR(
	AUTHOR_ID INT IDENTITY(1,1),
	[NAME] VARCHAR(50) NOT NULL,
	LAST_NAME VARCHAR(50),
	CONSTRAINT PK_AUTHOR PRIMARY KEY(AUTHOR_ID)
);

CREATE TABLE BOOK(
	BOOK_ID INT IDENTITY(1,1),
	TITLE VARCHAR(200) NOT NULL,
	SYNOPSIS VARCHAR(MAX),
	CONSTRAINT PK_BOOK PRIMARY KEY(BOOK_ID)
);

CREATE TABLE BOOK_AUTHOR(
	BOOK_ID INT,
	AUTHOR_ID INT,
	CONSTRAINT PK_BOOK_AUTHOR PRIMARY KEY(BOOK_ID, AUTHOR_ID),
	CONSTRAINT FK_BOOK_AUTHOR_BOOK FOREIGN KEY(BOOK_ID)
		REFERENCES BOOK(BOOK_ID) 
		ON DELETE CASCADE,
	CONSTRAINT FK_BOOK_AUTHOR_AUTHOR FOREIGN KEY(AUTHOR_ID)
		REFERENCES AUTHOR(AUTHOR_ID)
);

CREATE TABLE BOOK_GENRE(
	BOOK_ID INT,
	GENRE_CODE VARCHAR(5),
	CONSTRAINT PK_BOOK_GENRE PRIMARY KEY(BOOK_ID, GENRE_CODE),
	CONSTRAINT FK_BOOK_GENRE_BOOK FOREIGN KEY(BOOK_ID)
		REFERENCES BOOK(BOOK_ID)
		ON DELETE CASCADE,
	CONSTRAINT FK_BOOK_GENRE_GENRE FOREIGN KEY(GENRE_CODE)
		REFERENCES GENRE(CODE)
);

CREATE TABLE EDITION(
	EDITION_ID UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	PAGES INT NOT NULL,
	PUBLICATION_DATE DATE NOT NULL,
	[LANGUAGE] VARCHAR(50) NOT NULL,
	BOOK_ID INT NOT NULL,
	TYPE_CODE VARCHAR(5) NOT NULL,
	EDITORIAL_ID INT NOT NULL,
	CONSTRAINT EDITION_PK PRIMARY KEY(EDITION_ID),
	CONSTRAINT FK_EDITION_BOOK FOREIGN KEY(BOOK_ID)
		REFERENCES BOOK(BOOK_ID),
	CONSTRAINT FK_EDITION_EDITION_TYPE FOREIGN KEY(TYPE_CODE)
		REFERENCES EDITION_TYPE(CODE),
	CONSTRAINT FK_EDITION_EDITORIAL FOREIGN KEY(EDITORIAL_ID)
		REFERENCES EDITORIAL(EDITORIAL_ID)
);

CREATE TABLE ISBN(
	CODE VARCHAR(13),
	EDITION_ID UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT PK_ISBN PRIMARY KEY(CODE),
	CONSTRAINT PK_ISBN_EDITION FOREIGN KEY(EDITION_ID)
		REFERENCES EDITION(EDITION_ID),
	CONSTRAINT UQ_ISBN_EDITION_ID UNIQUE(EDITION_ID)
);

CREATE TABLE EDITION_PRICE(	
	EDITION_ID UNIQUEIDENTIFIER,
	PRICE DECIMAL(10,2) NOT NULL,
	CONSTRAINT PK_EDITION_PRICE PRIMARY KEY(EDITION_ID),
	CONSTRAINT PK_EDITION_PRICE_EDITION FOREIGN KEY(EDITION_ID)
		REFERENCES EDITION(EDITION_ID)
);

CREATE TABLE HISTORY_PRICE(
	EDITION_ID UNIQUEIDENTIFIER,
	[DATE] DATETIME NOT NULL,
	PRICE DECIMAL(10,2) NOT NULL,
	CONSTRAINT PK_HISTORY_PRICE PRIMARY KEY(EDITION_ID, [DATE]),
	CONSTRAINT PK_HISTORY_PRICE_EDITION FOREIGN KEY(EDITION_ID)
		REFERENCES EDITION(EDITION_ID)
);
GO