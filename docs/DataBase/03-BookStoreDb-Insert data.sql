USE BookStoreDb;

--CARGA DE DATOS
INSERT INTO GENRE VALUES
	('FNTSI','Fantasia'),
	('TRROR','Terror'),
	('DRAMA','Drama'),
	('POLIC','Policial'),
	('SUPNS','Suspenso'),
	('RELIG','Religion'),
	('AUTAY','Auto-Ayuda'),
	('CIENC','Ciencia'),
	('CIEFI','Ciencia ficcion'),
	('TECNO','Tecnologia'),
	('PROGR','Programacion'),
	('POLIT','Politica'),
	('FILOS','Filosofia'),
	('HISTO','Historia'),
	('EDUCA','Educacion'),
	('BIOGR','Biografia');

INSERT INTO EDITION_TYPE VALUES
	('TPDUR','Tapa dura'),
	('TPBLA','Tapa blanda'),
	('COLEC','Coleccion'),
	('EBOOK','Libro digital'),
	('BOLSI','Bolsillo');


INSERT INTO EDITORIAL VALUES
	('CASTALIA'),
	('Taylor & Francis'),
	('e-artnow'),
	('IberiaLiteratura'),
	('BoD - Books on Demand'),
	('iUniverse'),
	('Editorial Multimedia Educativa de S.A. de C.V.'),
	('Linkgua'),
	('Grupo Sin Fronteras SAS'),
	('Greenbooks editore'),
	('Lindhardt og Ringhof');

INSERT INTO AUTHOR VALUES
	('Franz', 'Kafka'),
	('Arthur Conan', 'Doyle'),
	('Fiodor', 'Dostoievski'),
	('Sofocles', NULL), 
	('Roberto', 'Arlt');

INSERT INTO BOOK VALUES
	('La metamorfosis', 'Gregor Samsa despierta una ma�ana transformado en un bicho monstruoso. No se trata de una pesadilla: este viajante de comercio no volver� a recuperar nunca m�s su identidad humana. La familia le margina en su cuarto por miedo y verg�enza, y a partir de ese momento todo cambia en sus vidas... El protagonista, convertido en bestia y sumido en la m�s absoluta incomunicaci�n, se ve reducido a la nada y arrastrado inexorablemente a la muerte. Expresi�n sublime del "hombre kafkiano", La metamorfosis, escrita en 1912 y publicada por primera vez en 1916, est� considerada una obra maestra y es ya un texto primordial de la literatura universal del siglo XX.'),
	('El Sabueso de los Baskerville', 'A caballo entre el relato de misterio y el cuento de terror, EL SABUESO DE LOS BASKERVILLE (1902) supuso de hecho �si bien encubierto como recuerdo tard�o de su inseparable doctor Watson� el regreso a la actividad de Sherlock Holmes, despu�s de que ARTHUR CONAN DOYLE (1859-1930), cansado de la preponderancia que la figura del detective hab�a alcanzado en su obra, le hubiera hecho desaparecer algunos a�os antes junto con su antagonista, el doctor Moriarty, en las cataratas de Reichenbach. Trasladado a los inh�spitos y desolados p�ramos de la regi�n de Dartmoor, Holmes se enfrenta al reto de resolver un enigm�tico crimen relacionado con el espectro de un perro diab�lico y sobrenatural, instrumento de la maldici�n que pesa sobre una familia.'),
	('El idiota', 'Este ebook presenta "El idiota", con un �ndice din�mico y detallado. Es una novela escrita por Fi�dor Dostoyevski. Fue publicada originalmente en serie en El mensajero ruso entre 1868 y 1869. Est� considerada como una de las novelas m�s brillantes de Dostoyevski y de la "Edad de Oro" de la literatura rusa. La novela se sit�a en la Rusia de mediados del siglo XIX y narra el regreso del joven pr�ncipe Mishkin a su San Petersburgo natal, tras pasar parte de su vida en Suiza recuper�ndose de su dolencia. Es el propio Mishkin quien se presenta a s� mismo como enfermo de idiotez en proceso de curaci�n, y como tal es recibido por sus conciudadanos, que consideran s�ntomas de la mente enferma del pr�ncipe su inocencia y honestidad, valores ins�litos en el fr�volo ambiente de la sociedad pudiente. Fi�dor Mij�ilovich Dostoyevski (1821 - 1881) es uno de los principales escritores de la Rusia Zarista, cuya literatura explora la psicolog�a humana en el complejo contexto pol�tico, social y espiritual de la sociedad rusa del siglo XIX.'),
	('Ant�gona: Tragedia cl�sica griega', 'Ebook con un sumario din�mico y detallado: En la mitolog�a griega, Ant�gona es hija de Edipo y Yocasta y es hermana de Ismene, Eteocles y Polinices. Acompa�� a su padre Edipo - rey de Tebas - al exilio y, a su muerte, regres� a la ciudad.'),
	('El juguete rabioso', 'Cuando ten�a catorce a�os me inici� en los deleites y afanes de la literatura bandoleresca un viejo zapatero andaluz que ten�a su comercio de remend�n junto a una ferreter�a de fachada verde y blanca, en el zagu�n de una casa antigua en la calle Rivadavia entre Sud Am�rica y Bolivia. Decoraban el frente del cuchitril las policromas car�tulas de los cuadernillos que narraban las aventuras de Montbars el Pirata y de Wenongo el Mohicano. Nosotros los muchachos al salir de la escuela nos deleit�bamos observando los cromos que colgaban en la puerta, descoloridos por el sol. A veces entr�bamos a comprarle medio paquete de cigarrillos Barrilete, y el hombre renegaba de tener que dejar el banquillo para mercar con nosotros. Era cargado de espaldas, carisumido y barbudo, y por a�adidura algo cojo, una cojera extra�a, el pie redondo como el casco de una mula con el tal�n vuelto hacia afuera. Cada vez que le ve�a recordaba este proverbio, que mi madre acostumbraba a decir: "Gu�rdate de los se�alados de Dios".');

INSERT INTO BOOK_AUTHOR VALUES
	(1,1),
	(2,2),
	(3,3),
	(4,4),
	(5,5);

INSERT INTO BOOK_GENRE VALUES
	(1,1),
	(1,9),
	(2,4),
	(2,5),
	(3,5),
	(4,1),
	(5,5),
	(5,9);


INSERT INTO EDITION VALUES
	('B5852A44-6DBB-4FA9-B123-572762D51AF4', 62, '2012-10-01','es', 1, 2, 1),
	('7BAF3B70-4FBA-4C3A-B4E0-84E76B0393ED', 242, '2001-02-14','es', 2, 1, 2),
	('E33CF443-2C4D-41CB-9DB0-0543D8061D95', 809, '2015-07-22','es', 3, 4, 3),
	('6C543A61-0531-47C0-8A20-7D5FBC5D7EA4', 59, '2015-03-31','es', 4, 4, 4),
	('802A5A4D-0327-4638-B6BF-B2B74BE14B4D', 131, '2023-05-18','es', 5, 2, 5);


INSERT INTO ISBN VALUES
	('9788420636818' ,'7BAF3B70-4FBA-4C3A-B4E0-84E76B0393ED'),
	('9788026834854','E33CF443-2C4D-41CB-9DB0-0543D8061D95'),
	('9783959280051', '6C543A61-0531-47C0-8A20-7D5FBC5D7EA4'),
	('9791041808243','802A5A4D-0327-4638-B6BF-B2B74BE14B4D');


INSERT INTO EDITION_PRICE VALUES
	('B5852A44-6DBB-4FA9-B123-572762D51AF4', 800.00),
	('7BAF3B70-4FBA-4C3A-B4E0-84E76B0393ED', 600.00),
	('E33CF443-2C4D-41CB-9DB0-0543D8061D95', 550.00),
	('6C543A61-0531-47C0-8A20-7D5FBC5D7EA4', 750.00),
	('802A5A4D-0327-4638-B6BF-B2B74BE14B4D', 300.00);


INSERT INTO EDITION_STOCK VALUES
	('B5852A44-6DBB-4FA9-B123-572762D51AF4', 10),
	('7BAF3B70-4FBA-4C3A-B4E0-84E76B0393ED', 20),
	('E33CF443-2C4D-41CB-9DB0-0543D8061D95', 4),
	('6C543A61-0531-47C0-8A20-7D5FBC5D7EA4', 80),
	('802A5A4D-0327-4638-B6BF-B2B74BE14B4D', 0);
GO