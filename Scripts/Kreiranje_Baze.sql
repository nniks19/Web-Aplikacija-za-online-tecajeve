CREATE TABLE Razina_Prava(
 id int NOT NULL AUTO_INCREMENT,
 naziv varchar(30),
 CONSTRAINT PK_Razina_Prava PRIMARY KEY (id)
);
CREATE TABLE Osoba(
 oib varchar(11) NOT NULL,
 id_rp int NOT NULL,
 ime varchar(30),
 prezime varchar(30),
 email varchar(30),
 CONSTRAINT PK_Osoba PRIMARY KEY (oib),
 CONSTRAINT FK_Osoba_id_rp FOREIGN KEY(id_rp) REFERENCES Razina_Prava(id)
);
CREATE TABLE Tecaj(
 id int NOT NULL AUTO_INCREMENT,
 vlasnik_tecaja varchar(11),
 naziv varchar(50),
 opis TEXT,
 prosjecna_ocjena float,
 CONSTRAINT PK_Tecaj PRIMARY KEY (id),
 CONSTRAINT FK_Tecaj_vlasnik_tecaja FOREIGN KEY(vlasnik_tecaja) REFERENCES Osoba(oib)
);
CREATE TABLE Ocjene_Tecaja(
 id int NOT NULL AUTO_INCREMENT,
 osoba_oib varchar(11),
 tecaj_id int,
 komentar TEXT,
 ocjena int,
 CONSTRAINT PK_Ocjene_Tecaja PRIMARY KEY(id),
 CONSTRAINT FK_Ocjene_Tecaja_osoba_oib FOREIGN KEY(osoba_oib) REFERENCES Osoba(oib),
 CONSTRAINT FK_Ocjene_Tecaja_tecaj_id FOREIGN KEY(tecaj_id) REFERENCES Tecaj(id)
);
CREATE TABLE Status_Narudzbe(
 id int NOT NULL AUTO_INCREMENT,
 naziv varchar(50),
 constraint PK_Status_Narudzbe PRIMARY KEY(id)
);
CREATE TABLE Nacin_Placanja(
 id int NOT NULL AUTO_INCREMENT,
 naziv varchar(50),
 constraint PK_Nacin_Placanja PRIMARY KEY(id)
);
CREATE TABLE Narudzba(
 id int NOT NULL AUTO_INCREMENT,
 status_id int,
 osoba_oib varchar(11),
 tecaj_id int,
 nacin_placanja_id int,
 datum_pocetak timestamp DEFAULT CURRENT_TIMESTAMP,
 datum_zavrsetak timestamp NULL,
 CONSTRAINT PK_Narudzba PRIMARY KEY(id),
 CONSTRAINT FK_Narudzba_status_id FOREIGN KEY(status_id) REFERENCES Status_Narudzbe(id),
 CONSTRAINT FK_Narudzba_osoba_oib FOREIGN KEY(osoba_oib) REFERENCES Osoba(oib),
 CONSTRAINT FK_Narudzba_tecaj_id FOREIGN KEY(tecaj_id) REFERENCES Tecaj(id),
 CONSTRAINT FK_Narudzba_nacin_placanja_id FOREIGN KEY(nacin_placanja_id) REFERENCES Nacin_Placanja(id)
);
CREATE TABLE Videozapisi(
 id int NOT NULL AUTO_INCREMENT,
 tecaj_id int,
 videozapis_putanja TEXT,
 videozapis_tip varchar(3),
 CONSTRAINT PK_Videozapisi PRIMARY KEY(id),
 CONSTRAINT FK_Videozapisi_tecaj_id FOREIGN KEY(tecaj_id) REFERENCES Tecaj(id)
);