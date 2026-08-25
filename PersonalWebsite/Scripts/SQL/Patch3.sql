USE CWARD

GO

DROP TABLE IF EXISTS invoiceEntries

DROP TABLE IF EXISTS invoices

DROP TABLE IF EXISTS clients

DROP TABLE IF EXISTS addresses

DROP TABLE IF EXISTS countries

DROP TABLE IF EXISTS states

CREATE TABLE states
(
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  name varchar(100) NOT NULL,
  code varchar(2) NOT NULL
);
 
INSERT INTO states(name, code) 
VALUES 
	('Alaska', 'AK'),
	('Alabama', 'AL'),
	('American Samoa', 'AS'),
	('Arizona', 'AZ'),
	('Arkansas', 'AR'),
	('California', 'CA'),
	('Colorado', 'CO'),
	('Connecticut', 'CT'),
	('Delaware', 'DE'),
	('District of Columbia', 'DC'),
	('Federated States of Micronesia', 'FM'),
	('Florida', 'FL'),
	('Georgia', 'GA'),
	('Guam', 'GU'),
	('Hawaii', 'HI'),
	('Idaho', 'ID'),
	('Illinois', 'IL'),
	('Indiana', 'IN'),
	('Iowa', 'IA'),
	('Kansas', 'KS'),
	('Kentucky', 'KY'),
	('Louisiana', 'LA'),
	('Maine', 'ME'),
	('Marshall Islands', 'MH'),
	('Maryland', 'MD'),
	('Massachusetts', 'MA'),
	('Michigan', 'MI'),
	('Minnesota', 'MN'),
	('Mississippi', 'MS'),
	('Missouri', 'MO'),
	('Montana', 'MT'),
	('Nebraska', 'NE'),
	('Nevada', 'NV'),
	('New Hampshire', 'NH'),
	('New Jersey', 'NJ'),
	('New Mexico', 'NM'),
	('New York', 'NY'),
	('North Carolina', 'NC'),
	('North Dakota', 'ND'),
	('Northern Mariana Islands', 'MP'),
	('Ohio', 'OH'),
	('Oklahoma', 'OK'),
	('Oregon', 'OR'),
	('Palau', 'PW'),
	('Pennsylvania', 'PA'),
	('Puerto Rico', 'PR'),
	('Rhode Island', 'RI'),
	('South Carolina', 'SC'),
	('South Dakota', 'SD'),
	('Tennessee', 'TN'),
	('Texas', 'TX'),
	('Utah', 'UT'),
	('Vermont', 'VT'),
	('Virgin Islands', 'VI'),
	('Virginia', 'VA'),
	('Washington', 'WA'),
	('West Virginia', 'WV'),
	('Wisconsin', 'WI'),
	('Wyoming', 'WY'),
	('Armed Forces Africa', 'AE'),
	('Armed Forces Americas (except Canada)', 'AA'),
	('Armed Forces Canada', 'AE'),
	('Armed Forces Europe', 'AE'),
	('Armed Forces Middle East', 'AE'),
	('Armed Forces Pacific', 'AP')

CREATE TABLE countries
(
	  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	  name varchar(100) NOT NULL,
	  code varchar(2) NOT NULL
);

INSERT INTO countries (code, name) VALUES
	('AF', 'Afghanistan'),
	('AX', 'Åland Islands'),
	('AL', 'Albania'),
	('DZ', 'Algeria'),
	('AS', 'American Samoa'),
	('AD', 'Andorra'),
	('AO', 'Angola'),
	('AI', 'Anguilla'),
	('AQ', 'Antarctica'),
	('AG', 'Antigua and Barbuda'),
	('AR', 'Argentina'),
	('AM', 'Armenia'),
	('AW', 'Aruba'),
	('AU', 'Australia'),
	('AT', 'Austria'),
	('AZ', 'Azerbaijan'),
	('BS', 'Bahamas'),
	('BH', 'Bahrain'),
	('BD', 'Bangladesh'),
	('BB', 'Barbados'),
	('BY', 'Belarus'),
	('BE', 'Belgium'),
	('BZ', 'Belize'),
	('BJ', 'Benin'),
	('BM', 'Bermuda'),
	('BT', 'Bhutan'),
	('BO', 'Bolivia (Plurinational State of)'),
	('BA', 'Bosnia and Herzegovina'),
	('BW', 'Botswana'),
	('BV', 'Bouvet Island'),
	('BR', 'Brazil'),
	('IO', 'British Indian Ocean Territory'),
	('BN', 'Brunei Darussalam'),
	('BG', 'Bulgaria'),
	('BF', 'Burkina Faso'),
	('BI', 'Burundi'),
	('CV', 'Cabo Verde'),
	('KH', 'Cambodia'),
	('CM', 'Cameroon'),
	('CA', 'Canada'),
	('BQ', 'Caribbean Netherlands'),
	('KY', 'Cayman Islands'),
	('CF', 'Central African Republic'),
	('TD', 'Chad'),
	('CL', 'Chile'),
	('CN', 'China'),
	('CX', 'Christmas Island'),
	('CC', 'Cocos (Keeling) Islands'),
	('CO', 'Colombia'),
	('KM', 'Comoros'),
	('CG', 'Congo'),
	('CD', 'Congo, Democratic Republic of the'),
	('CK', 'Cook Islands'),
	('CR', 'Costa Rica'),
	('HR', 'Croatia'),
	('CU', 'Cuba'),
	('CW', 'Curaçao'),
	('CY', 'Cyprus'),
	('CZ', 'Czech Republic'),
	('CI', 'Côte d''Ivoire'),
	('DK', 'Denmark'),
	('DJ', 'Djibouti'),
	('DM', 'Dominica'),
	('DO', 'Dominican Republic'),
	('EC', 'Ecuador'),
	('EG', 'Egypt'),
	('SV', 'El Salvador'),
	('GQ', 'Equatorial Guinea'),
	('ER', 'Eritrea'),
	('EE', 'Estonia'),
	('SZ', 'Eswatini (Swaziland)'),
	('ET', 'Ethiopia'),
	('FK', 'Falkland Islands (Malvinas)'),
	('FO', 'Faroe Islands'),
	('FJ', 'Fiji'),
	('FI', 'Finland'),
	('FR', 'France'),
	('GF', 'French Guiana'),
	('PF', 'French Polynesia'),
	('TF', 'French Southern Territories'),
	('GA', 'Gabon'),
	('GM', 'Gambia'),
	('GE', 'Georgia'),
	('DE', 'Germany'),
	('GH', 'Ghana'),
	('GI', 'Gibraltar'),
	('GR', 'Greece'),
	('GL', 'Greenland'),
	('GD', 'Grenada'),
	('GP', 'Guadeloupe'),
	('GU', 'Guam'),
	('GT', 'Guatemala'),
	('GG', 'Guernsey'),
	('GN', 'Guinea'),
	('GW', 'Guinea-Bissau'),
	('GY', 'Guyana'),
	('HT', 'Haiti'),
	('HM', 'Heard Island and Mcdonald Islands'),
	('HN', 'Honduras'),
	('HK', 'Hong Kong'),
	('HU', 'Hungary'),
	('IS', 'Iceland'),
	('IN', 'India'),
	('ID', 'Indonesia'),
	('IR', 'Iran'),
	('IQ', 'Iraq'),
	('IE', 'Ireland'),
	('IM', 'Isle of Man'),
	('IL', 'Israel'),
	('IT', 'Italy'),
	('JM', 'Jamaica'),
	('JP', 'Japan'),
	('JE', 'Jersey'),
	('JO', 'Jordan'),
	('KZ', 'Kazakhstan'),
	('KE', 'Kenya'),
	('KI', 'Kiribati'),
	('KP', 'Korea, North'),
	('KR', 'Korea, South'),
	('XK', 'Kosovo'),
	('KW', 'Kuwait'),
	('KG', 'Kyrgyzstan'),
	('LA', 'Lao People''s Democratic Republic'),
	('LV', 'Latvia'),
	('LB', 'Lebanon'),
	('LS', 'Lesotho'),
	('LR', 'Liberia'),
	('LY', 'Libya'),
	('LI', 'Liechtenstein'),
	('LT', 'Lithuania'),
	('LU', 'Luxembourg'),
	('MO', 'Macao'),
	('MK', 'Macedonia North'),
	('MG', 'Madagascar'),
	('MW', 'Malawi'),
	('MY', 'Malaysia'),
	('MV', 'Maldives'),
	('ML', 'Mali'),
	('MT', 'Malta'),
	('MH', 'Marshall Islands'),
	('MQ', 'Martinique'),
	('MR', 'Mauritania'),
	('MU', 'Mauritius'),
	('YT', 'Mayotte'),
	('MX', 'Mexico'),
	('FM', 'Micronesia'),
	('MD', 'Moldova'),
	('MC', 'Monaco'),
	('MN', 'Mongolia'),
	('ME', 'Montenegro'),
	('MS', 'Montserrat'),
	('MA', 'Morocco'),
	('MZ', 'Mozambique'),
	('MM', 'Myanmar (Burma)'),
	('NA', 'Namibia'),
	('NR', 'Nauru'),
	('NP', 'Nepal'),
	('NL', 'Netherlands'),
	('AN', 'Netherlands Antilles'),
	('NC', 'New Caledonia'),
	('NZ', 'New Zealand'),
	('NI', 'Nicaragua'),
	('NE', 'Niger'),
	('NG', 'Nigeria'),
	('NU', 'Niue'),
	('NF', 'Norfolk Island'),
	('MP', 'Northern Mariana Islands'),
	('NO', 'Norway'),
	('OM', 'Oman'),
	('PK', 'Pakistan'),
	('PW', 'Palau'),
	('PS', 'Palestine'),
	('PA', 'Panama'),
	('PG', 'Papua New Guinea'),
	('PY', 'Paraguay'),
	('PE', 'Peru'),
	('PH', 'Philippines'),
	('PN', 'Pitcairn Islands'),
	('PL', 'Poland'),
	('PT', 'Portugal'),
	('PR', 'Puerto Rico'),
	('QA', 'Qatar'),
	('RE', 'Reunion'),
	('RO', 'Romania'),
	('RU', 'Russian Federation'),
	('RW', 'Rwanda'),
	('BL', 'Saint Barthelemy'),
	('SH', 'Saint Helena'),
	('KN', 'Saint Kitts and Nevis'),
	('LC', 'Saint Lucia'),
	('MF', 'Saint Martin'),
	('PM', 'Saint Pierre and Miquelon'),
	('VC', 'Saint Vincent and the Grenadines'),
	('WS', 'Samoa'),
	('SM', 'San Marino'),
	('ST', 'Sao Tome and Principe'),
	('SA', 'Saudi Arabia'),
	('SN', 'Senegal'),
	('RS', 'Serbia'),
	('CS', 'Serbia and Montenegro'),
	('SC', 'Seychelles'),
	('SL', 'Sierra Leone'),
	('SG', 'Singapore'),
	('SX', 'Sint Maarten'),
	('SK', 'Slovakia'),
	('SI', 'Slovenia'),
	('SB', 'Solomon Islands'),
	('SO', 'Somalia'),
	('ZA', 'South Africa'),
	('GS', 'South Georgia and the South Sandwich Islands'),
	('SS', 'South Sudan'),
	('ES', 'Spain'),
	('LK', 'Sri Lanka'),
	('SD', 'Sudan'),
	('SR', 'Suriname'),
	('SJ', 'Svalbard and Jan Mayen'),
	('SE', 'Sweden'),
	('CH', 'Switzerland'),
	('SY', 'Syria'),
	('TW', 'Taiwan'),
	('TJ', 'Tajikistan'),
	('TZ', 'Tanzania'),
	('TH', 'Thailand'),
	('TL', 'Timor-Leste'),
	('TG', 'Togo'),
	('TK', 'Tokelau'),
	('TO', 'Tonga'),
	('TT', 'Trinidad and Tobago'),
	('TN', 'Tunisia'),
	('TR', 'Turkey (Türkiye)'),
	('TM', 'Turkmenistan'),
	('TC', 'Turks and Caicos Islands'),
	('TV', 'Tuvalu'),
	('UM', 'U.S. Outlying Islands'),
	('UG', 'Uganda'),
	('UA', 'Ukraine'),
	('AE', 'United Arab Emirates'),
	('GB', 'United Kingdom'),
	('US', 'United States'),
	('UY', 'Uruguay'),
	('UZ', 'Uzbekistan'),
	('VU', 'Vanuatu'),
	('VA', 'Vatican City Holy See'),
	('VE', 'Venezuela'),
	('VN', 'Vietnam'),
	('VG', 'Virgin Islands, British'),
	('VI', 'Virgin Islands, U.S'),
	('WF', 'Wallis and Futuna'),
	('EH', 'Western Sahara'),
	('YE', 'Yemen'),
	('ZM', 'Zambia'),
	('ZW', 'Zimbabwe')

CREATE TABLE addresses
(
	id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	street1 varchar(120) NOT NULL,
	street2 varchar(120),
	city varchar(100) NOT NULL,
	stateId int,
	countryId int NOT NULL,
	postCode varchar(16) NOT NULL

	CONSTRAINT fk_addressState
		FOREIGN KEY (stateId)
		REFERENCES states(id)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	CONSTRAINT fk_addressCountry
		FOREIGN KEY (countryId)
		REFERENCES countries(id)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
);

CREATE TABLE clients
(
	id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	userId int,
	name varchar(200),
	addressId int,
	creationDate datetime,

	CONSTRAINT fk_address
		FOREIGN KEY (addressId)
		REFERENCES addresses(id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,

	CONSTRAINT fk_userId
		FOREIGN KEY (userId)
		REFERENCES author(id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);

CREATE TABLE invoices
(
	id int NOT NULL PRIMARY KEY IDENTITY(1,1),	
	clientId int,
	invoiceDate datetime,
	invoiceDueDate datetime,
	sentDate datetime,

	CONSTRAINT fk_client
		FOREIGN KEY (clientId)
		REFERENCES clients(id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);

CREATE TABLE invoiceEntries
(
	id int NOT NULL PRIMARY KEY IDENTITY(1,1),	
	invoiceId int NOT NULL,
	description varchar(100) NOT NULL,
	rate real NOT NULL,
	rateUnit varchar(50) NOT NULL,
	unitAmount real NOT NULL,

	totalAmount AS (rate * unitAmount),

	CONSTRAINT fk_invoices
		FOREIGN KEY (invoiceId)
		REFERENCES invoices(id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);

INSERT INTO addresses(street1, street2, city, stateId, countryId, postCode)
VALUES ('1920 Lawrence St NE', '', 'Washington', 22, 106, '15230')

INSERT INTO clients(userId, name, addressId, creationDate)
VALUES (1, 'TGIF Enterprise', 1, '2026-08-24 11:42:54')

INSERT INTO invoices(clientId, invoiceDate, invoiceDueDate)
VALUES (1, '2026-08-01', '2026-08-31')

INSERT INTO invoiceEntries(invoiceId, description, rate, rateUnit, unitAmount)
VALUES (1, 'Hourly Rate', '100', 'Hour', '5')
INSERT INTO invoiceEntries(invoiceId, description, rate, rateUnit, unitAmount)
VALUES (1, 'Maintenance Fee', '200', 'Month', '1')