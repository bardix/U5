CREATE TABLE Clienti (
    CodiceFiscale CHAR(16) PRIMARY KEY,
    Cognome NVARCHAR(50) NOT NULL,
    Nome NVARCHAR(50) NOT NULL,
    Città NVARCHAR(50),
    Provincia NVARCHAR(50),
    Email NVARCHAR(100),
    Telefono NVARCHAR(15),
    Cellulare NVARCHAR(15)
);

-- Creazione della tabella Camere
CREATE TABLE Camere (
    Numero INT PRIMARY KEY,
    Descrizione NVARCHAR(255),
    Tipologia NVARCHAR(20) CHECK (Tipologia IN ('Singola', 'Doppia'))
);

-- Creazione della tabella Prenotazioni
CREATE TABLE Prenotazioni (
    IDPrenotazione INT PRIMARY KEY IDENTITY(1,1),
    CodiceFiscaleCliente CHAR(16) NOT NULL,
    NumeroCamera INT NOT NULL,
    DataPrenotazione DATE NOT NULL,
    NumeroProgressivo INT,
    Anno INT,
    DataInizioSoggiorno DATE NOT NULL,
    DataFineSoggiorno DATE NOT NULL,
    Caparra DECIMAL(10, 2),
    Tariffa DECIMAL(10, 2),
    Dettagli NVARCHAR(255),
    FOREIGN KEY (CodiceFiscaleCliente) REFERENCES Clienti(CodiceFiscale),
    FOREIGN KEY (NumeroCamera) REFERENCES Camere(Numero)
);

-- Creazione della tabella Servizi
CREATE TABLE Servizi (
    IDServizio INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(255) NOT NULL,
    Prezzo DECIMAL(10, 2) NOT NULL
);

-- Creazione della tabella ServiziPrenotazioni
CREATE TABLE ServiziPrenotazioni (
    ID INT PRIMARY KEY IDENTITY(1,1),
    IDPrenotazione INT NOT NULL,
    IDServizio INT NOT NULL,
    Data DATE NOT NULL,
    Quantità INT NOT NULL,
    Prezzo DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IDPrenotazione) REFERENCES Prenotazioni(IDPrenotazione),
    FOREIGN KEY (IDServizio) REFERENCES Servizi(IDServizio)
);

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50)
);


CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL
);


CREATE TABLE UserRoles (
    UserRoleId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);


-- Inserimento di alcuni clienti
INSERT INTO Clienti (CodiceFiscale, Cognome, Nome, Città, Provincia, Email, Telefono, Cellulare)
VALUES ('RSSMRA85M01H501Z', 'Rossi', 'Mario', 'Roma', 'RM', 'mario.rossi@example.com', '066123456', '3341234567');

INSERT INTO Clienti (CodiceFiscale, Cognome, Nome, Città, Provincia, Email, Telefono, Cellulare)
VALUES ('VRDLGI80A01H501Y', 'Verdi', 'Luigi', 'Milano', 'MI', 'luigi.verdi@example.com', '027123456', '3351234567');

-- Inserimento di alcune camere
INSERT INTO Camere (Numero, Descrizione, Tipologia)
VALUES (101, 'Camera singola con vista mare', 'Singola');

INSERT INTO Camere (Numero, Descrizione, Tipologia)
VALUES (102, 'Camera doppia con vista giardino', 'Doppia');

-- Inserimento di alcune prenotazioni
INSERT INTO Prenotazioni (CodiceFiscaleCliente, NumeroCamera, DataPrenotazione, NumeroProgressivo, Anno, DataInizioSoggiorno, DataFineSoggiorno, Caparra, Tariffa, Dettagli)
VALUES ('RSSMRA85M01H501Z', 101, '2023-07-01', 1, 2023, '2023-08-01', '2023-08-07', 100.00, 700.00, 'Pernottamento con Prima Colazione');

INSERT INTO Prenotazioni (CodiceFiscaleCliente, NumeroCamera, DataPrenotazione, NumeroProgressivo, Anno, DataInizioSoggiorno, DataFineSoggiorno, Caparra, Tariffa, Dettagli)
VALUES ('VRDLGI80A01H501Y', 102, '2023-07-02', 2, 2023, '2023-08-10', '2023-08-15', 150.00, 750.00, 'Mezza Pensione');

-- Inserimento di alcuni servizi
INSERT INTO Servizi (Nome, Prezzo)
VALUES ('Colazione in camera', 10.00);

INSERT INTO Servizi (Nome, Prezzo)
VALUES ('Bevande e cibo nel mini bar', 15.00);

INSERT INTO Servizi (Nome, Prezzo)
VALUES ('Internet', 5.00);

INSERT INTO Servizi (Nome, Prezzo)
VALUES ('Letto aggiuntivo', 20.00);

INSERT INTO Servizi (Nome, Prezzo)
VALUES ('Culla', 5.00);

-- Inserimento di alcuni servizi associati alle prenotazioni
INSERT INTO ServiziPrenotazioni (IDPrenotazione, IDServizio, Data, Quantità, Prezzo)
VALUES (1, 1, '2023-08-02', 1, 10.00);

INSERT INTO ServiziPrenotazioni (IDPrenotazione, IDServizio, Data, Quantità, Prezzo)
VALUES (2, 3, '2023-08-11', 2, 5.00);

INSERT INTO Users (UserName, PasswordHash, Email, FirstName, LastName)
VALUES
('admin', 'admin', 'admin@example.com', 'Admin', 'User'),
('user1', 'hashed_password2', 'user1@example.com', 'User', 'One');


INSERT INTO Roles (RoleName)
VALUES
('Admin'),
('User');


INSERT INTO UserRoles (UserId, RoleId)
VALUES
(1, 1), -- Admin role to admin user
(2, 2); -- User role to user1

