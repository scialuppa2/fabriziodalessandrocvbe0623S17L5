
QUERY PER LA CREAZIONE DELLE TABELLE DEL DATABASE "POLIZIA MUNICIPALE"

CREATE TABLE ANAGRAFICA (
    IDanagrafica INT PRIMARY KEY IDENTITY,
    Cognome VARCHAR(50) NOT NULL,
    Nome VARCHAR(50) NOT NULL,
    Indirizzo VARCHAR(100),
    Città VARCHAR(50),
    CAP VARCHAR(5) CONSTRAINT CK_CAP CHECK (ISNUMERIC(CAP) = 1),
    CodiceFiscale VARCHAR(16) NOT NULL UNIQUE
);


CREATE TABLE TIPO_VIOLAZIONE (
    IDviolazione INT PRIMARY KEY IDENTITY,
    Descrizione VARCHAR(100) NOT NULL
);

CREATE TABLE VERBALE (
    IDverbale INT PRIMARY KEY IDENTITY,
    DataViolazione DATE NOT NULL,
    IndirizzoViolazione VARCHAR(100) NOT NULL,
    NominativoAgente VARCHAR(100),
    DataTrascrizioneVerbale DATE,
    Importo MONEY NOT NULL,
    DecurtamentoPunti INT NOT NULL,
    IDanagrafica INT NOT NULL,
    IDviolazione INT NOT NULL,
    FOREIGN KEY (IDanagrafica) REFERENCES ANAGRAFICA(IDanagrafica),
    FOREIGN KEY (IDviolazione) REFERENCES TIPO_VIOLAZIONE(IDviolazione)
);

QUERY PER IL POPOLAMENTO DELLE TABELLE

INSERT INTO ANAGRAFICA (Cognome, Nome, Indirizzo, Città, CAP, CodiceFiscale) 
VALUES 
('Rossi', 'Mario', 'Via Roma 1', 'Palermo', '00100', 'RSSMRA01A01H123A'),
('Bianchi', 'Luca', 'Corso Italia 5', 'Ragusa', '20100', 'BNCLCU02B02I456B'),
('Verdi', 'Laura', 'Via Garibaldi 10', 'Catania', '80100', 'VRDLRA03C03L789C'),
('Ferrari', 'Anna', 'Viale dei Fiori 15', 'Catania', '10100', 'FRRNNA04D04M012D'),
('Russo', 'Giuseppe', 'Piazza Dante 20', 'Messina', '50100', 'RSSGPP05E05N345E'),
('Esposito', 'Maria', 'Via Mazzini 25', 'Palermo', '40100', 'SPSMMR06F06O678F'),
('Romano', 'Paolo', 'Corso Vittorio Emanuele 30', 'Messina', '16100', 'RMNPLA07G07P901G'),
('Gallo', 'Giovanna', 'Piazza del Popolo 35', 'Palermo', '90100', 'GLLGVA08H08Q234H'),
('Costa', 'Luigi', 'Via dei Tigli 40', 'Catania', '95100', 'CSTLGI09I09R567I'),
('Fontana', 'Elena', 'Via delle Rose 45', 'Ragusa', '70100', 'FNTLNA10L10S890L'),
('Moretti', 'Alessandro', 'Corso Matteotti 50', 'Trapani', '37100', 'MRTLSN11M11T123M'),
('Barbieri', 'Sara', 'Piazza Navona 55', 'Palermo', '34100', 'BRBSRA12N12U456N'),
('Greco', 'Roberto', 'Via delle Magnolie 60', 'Catania', '35100', 'GRCRRT13O13V789O'),
('Conti', 'Cristina', 'Viale Kennedy 65', 'Messina', '06100', 'CNTCST14P14Z012P'),
('De Luca', 'Angela', 'Corso Europa 70', 'Trapani', '09100', 'DLCNGL15R15A345R');


INSERT INTO TIPO_VIOLAZIONE (Descrizione)
VALUES 
('Eccesso di velocità'),
('Mancato rispetto del segnale di stop'),
('Guida senza cintura di sicurezza'),
('Utilizzo del cellulare alla guida'),
('Guida sotto l''effetto di alcol o droghe'),
('Mancato rispetto del limite di velocità in zona scolastica'),
('Sorpasso in divieto'),
('Guida contromano'),
('Parcheggio in divieto di sosta'),
('Manutenzione non eseguita sul veicolo');

INSERT INTO VERBALE (DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IDanagrafica, IDviolazione) 
VALUES 
('2005-03-15', 'Via Roma 13', 'Fernando Borghese', '2005-03-16', 450.00, 10, 10, 1),
('2006-06-20', 'Corso Italia 56', 'Letizia Murgia', '2006-06-21', 500.00, 15, 12, 6),
('2007-09-10', 'Via Garibaldi 10', 'Letizia Murgia', '2007-09-11', 275.00, 4, 6, 10),
('2008-12-05', 'Viale dei Fiori 15', 'Giovanni Guidotti', '2008-12-06', 320.00, 4, 8, 9),
('2009-05-08', 'Piazza Dante 20', 'Federico Torre', '2009-05-09', 480.00, 10, 4, 5),
('2010-07-12', 'Via Mazzini 71', 'Fernando Borghese', '2010-07-13', 100.00, 5, 8, 6),
('2010-09-18', 'Corso Vittorio Emanuele 30', 'Pamela Lotti', '2010-09-19', 150.00, 5, 11, 4),
('2006-10-25', 'Piazza del Popolo 35', 'Giovanni Guidotti', '2006-10-26', 470.00, 10, 12, 2),
('2007-11-30', 'Via dei Tigli 40', 'Fernando Borghese', '2007-12-01', 210.00, 5, 3, 9),
('2008-12-10', 'Via delle Rose 45', 'Giovanni Guidotti', '2008-12-11', 165.00, 5, 10, 6),
('2009-02-12', 'Corso Matteotti 50', 'Pamela Lotti', '2009-02-13', 485.00, 15, 5, 1),
('2010-02-15', 'Piazza Navona 85', 'Letizia Murgia', '2010-02-16', 405.00, 10, 15, 10),
('2005-04-20', 'Via delle Magnolie 63', 'Federico Torre', '2005-04-21', 370.00, 5, 7, 3),
('2006-05-05', 'Viale Kennedy 78', 'Pino Capua', '2006-05-06', 220.00, 10, 14, 4),
('2009-07-10', 'Corso Europa 24', 'Federico Torre', '2009-07-11', 175.00, 5, 3, 10);




