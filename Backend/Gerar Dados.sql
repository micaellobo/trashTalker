USE trashTalker
GO

SET NOCOUNT ON;
GO

DELETE FROM Pickings
DELETE FROM Measurements
DELETE FROM Alerts
DELETE FROM Containers
DELETE FROM CollectPoint
DELETE FROM RecycleBins
DELETE FROM Routes
DELETE FROM Users
GO

-- ADD USERS
INSERT INTO Users(id,username,[password],[role],firstName,lastName,email,gender,[status],street,city,zipCode,country) VALUES
 ('cd9608d6-fd8f-4cf3-b613-19d1914abdda','manager','$s2$16384$8$1$mo+YsJ2QaM6HA6CfTjji/oIawxBdmJqDZEDUEO5WUgo=$5fWLC8O9W5UUkOwZ18fAUxvDt5Ae+ybHGKRVzPIw7qU=',1,'manager','manager','manager@estg.ipp.pt',0,0,'employee','Felgueiras','4610-222','Portugal')
,('dc06815f-bac7-42bf-b979-5cc768b745ac','employee','$s2$16384$8$1$Jz+wCvnC/s0ZXesSmI/scv405roi3nucoBa567paCYU=$OhXJEvUQVfg3E6Ynmw3ouqgKtvxuwQotheXrM8kMfWo=',0,'John','Doe','employee@estg.ipp.pt',0,0,'employee','Felgueiras','4610-222','Portugal'),
('cd9765d6-fd8f-4cf3-b613-19d1914abdda','admin','$s2$16384$8$1$yPm4RkUbYaR+c/cDjMxiiaVA/xcQM3VQKIMrb3Tt5Ao=$YI+48wyIzdJubctKyE0lG3wVHjKwdF3mhoOVWj7J1Tc=',2,'admin','admin','admin@estg.ipp.pt',0,0,'admin','Felgueiras','4610-222','Portugal');
GO

-- ADD RECYCLE BINS
--(id,status,latit,longit,street,city,zipCode,country)
INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f300',0,'41.452905177935115','-8.170226361409169','Av. 5 de Outubro 177','Fafe','4820-115','Portugal');
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa200',0,'87d27990-4ad4-4463-a844-a9f85d67f300',0,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa201',0,'87d27990-4ad4-4463-a844-a9f85d67f300',1,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa202',0,'87d27990-4ad4-4463-a844-a9f85d67f300',2,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa203',0,'87d27990-4ad4-4463-a844-a9f85d67f300',3,300,300,300,0,null,0);

INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f301',0,'41.379111787557214','-8.310749417065683','Rua Dr.Braulio Caldas','Vizela','4815-478','Portugal');
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa204',0,'87d27990-4ad4-4463-a844-a9f85d67f301',0,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa205',0,'87d27990-4ad4-4463-a844-a9f85d67f301',1,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa206',0,'87d27990-4ad4-4463-a844-a9f85d67f301',2,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa207',0,'87d27990-4ad4-4463-a844-a9f85d67f301',3,300,300,300,0,null,0);

INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f302',0,'41.55117521903238','-8.428351299874176','Praça do Município','Braga','4700-435','Portugal');
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa208',0,'87d27990-4ad4-4463-a844-a9f85d67f302',0,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa209',0,'87d27990-4ad4-4463-a844-a9f85d67f302',1,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa210',0,'87d27990-4ad4-4463-a844-a9f85d67f302',2,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa211',0,'87d27990-4ad4-4463-a844-a9f85d67f302',3,300,300,300,0,null,0);

INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f303',0,'41.44447426796226','-8.291958244052815','Largo Cónego José Maria Gomes 4','Guimarães','4804-534','Portugal');
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa212',0,'87d27990-4ad4-4463-a844-a9f85d67f303',0,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa213',0,'87d27990-4ad4-4463-a844-a9f85d67f303',1,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa214',0,'87d27990-4ad4-4463-a844-a9f85d67f303',2,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa215',0,'87d27990-4ad4-4463-a844-a9f85d67f303',3,300,300,300,0,null,0);

INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f304',0,'41.27745827597592','-8.281235613375067','R. Dr. Francisco Sá Carneiro','Lousada','4620-695','Portugal');
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa216',0,'87d27990-4ad4-4463-a844-a9f85d67f304',0,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa217',0,'87d27990-4ad4-4463-a844-a9f85d67f304',1,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa218',0,'87d27990-4ad4-4463-a844-a9f85d67f304',2,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa219',0,'87d27990-4ad4-4463-a844-a9f85d67f304',3,300,300,300,0,null,0);

INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f305',0,'41.40995945776923','-8.520241144053639','Praça Álvaro Marques','Famalicão','4764-502','Portugal');
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa220',0,'87d27990-4ad4-4463-a844-a9f85d67f305',0,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa221',0,'87d27990-4ad4-4463-a844-a9f85d67f305',1,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa222',0,'87d27990-4ad4-4463-a844-a9f85d67f305',2,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa223',0,'87d27990-4ad4-4463-a844-a9f85d67f305',3,300,300,300,0,null,0);

INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f306',0,'41.48476149705517','-8.022800991044383','Edifício da Antiga Escola Primária','Celorico de Basto','4890-542','Portugal');
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa224',0,'87d27990-4ad4-4463-a844-a9f85d67f306',0,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa225',0,'87d27990-4ad4-4463-a844-a9f85d67f306',1,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa226',0,'87d27990-4ad4-4463-a844-a9f85d67f306',2,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa227',0,'87d27990-4ad4-4463-a844-a9f85d67f306',3,300,300,300,0,null,0);

INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f307',0,'41.32983778478107','-8.565785224567541','Rua Das Indústrias, 324','Trofa','4785-625','Portugal');
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa228',0,'87d27990-4ad4-4463-a844-a9f85d67f307',0,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa229',0,'87d27990-4ad4-4463-a844-a9f85d67f307',1,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa230',0,'87d27990-4ad4-4463-a844-a9f85d67f307',2,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa231',0,'87d27990-4ad4-4463-a844-a9f85d67f307',3,300,300,300,0,null,0);

INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f308',0,'41.340253107311284', '-8.480715555752546','R. dos Trabalhadores do Arco','Santo Tirso','4780-424','Portugal');
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa232',0,'87d27990-4ad4-4463-a844-a9f85d67f308',0,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa233',0,'87d27990-4ad4-4463-a844-a9f85d67f308',1,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa234',0,'87d27990-4ad4-4463-a844-a9f85d67f308',2,300,300,300,0,null,0);
    INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa235',0,'87d27990-4ad4-4463-a844-a9f85d67f308',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f309',0,'41.531630774443585', '-8.617472306925318','Av. Dr. Sidónio Pais 537','Barcelos','4750-333','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa236',0,'87d27990-4ad4-4463-a844-a9f85d67f309',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa237',0,'87d27990-4ad4-4463-a844-a9f85d67f309',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa238',0,'87d27990-4ad4-4463-a844-a9f85d67f309',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa239',0,'87d27990-4ad4-4463-a844-a9f85d67f309',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f310',0,'41.344712739164954', '-8.159500002342241','R. de Cimo de Vila','Lixa','4615-380','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa240',0,'87d27990-4ad4-4463-a844-a9f85d67f310',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa241',0,'87d27990-4ad4-4463-a844-a9f85d67f310',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa242',0,'87d27990-4ad4-4463-a844-a9f85d67f310',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa243',0,'87d27990-4ad4-4463-a844-a9f85d67f310',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f311',0,'41.51157322786919','-7.98849916810665','Av. Cardeal Dom António Ribeiro','Cabeceiras de Basto','4860-306','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa244',0,'87d27990-4ad4-4463-a844-a9f85d67f311',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa245',0,'87d27990-4ad4-4463-a844-a9f85d67f311',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa246',0,'87d27990-4ad4-4463-a844-a9f85d67f311',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa247',0,'87d27990-4ad4-4463-a844-a9f85d67f311',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f312',0,'41.57311920196097','-8.269184534874517','R. Teixeira Ribeiro 212','Póvoa de Lanhoso','4830-512','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa248',0,'87d27990-4ad4-4463-a844-a9f85d67f312',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa249',0,'87d27990-4ad4-4463-a844-a9f85d67f312',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa250',0,'87d27990-4ad4-4463-a844-a9f85d67f312',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa251',0,'87d27990-4ad4-4463-a844-a9f85d67f312',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f313',0,'41.27324497808845','-8.074459668313335','R. Rio Tâmega','Amarante','4600-758','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa252',0,'87d27990-4ad4-4463-a844-a9f85d67f313',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa253',0,'87d27990-4ad4-4463-a844-a9f85d67f313',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa254',0,'87d27990-4ad4-4463-a844-a9f85d67f313',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa255',0,'87d27990-4ad4-4463-a844-a9f85d67f313',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f314',0,'41.21671172673507', '-8.285420422382106','Tv. Sardoal','Penafiel','4560-907','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa256',0,'87d27990-4ad4-4463-a844-a9f85d67f314',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa257',0,'87d27990-4ad4-4463-a844-a9f85d67f314',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa258',0,'87d27990-4ad4-4463-a844-a9f85d67f314',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa259',0,'87d27990-4ad4-4463-a844-a9f85d67f314',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f315',0,'41.287225648272425', '-8.33220019791001','R. de Santa Cruz','Freamunde','4590-306','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa260',0,'87d27990-4ad4-4463-a844-a9f85d67f315',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa261',0,'87d27990-4ad4-4463-a844-a9f85d67f315',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa262',0,'87d27990-4ad4-4463-a844-a9f85d67f315',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa263',0,'87d27990-4ad4-4463-a844-a9f85d67f315',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f316',0,'41.287225648272425', '-8.33220019791001','Rua Dr. Queirós Ribeiro 34','Paços de Ferreira','4590-590','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa264',0,'87d27990-4ad4-4463-a844-a9f85d67f316',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa265',0,'87d27990-4ad4-4463-a844-a9f85d67f316',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa266',0,'87d27990-4ad4-4463-a844-a9f85d67f316',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa267',0,'87d27990-4ad4-4463-a844-a9f85d67f316',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f317',0,'41.17772466514044', '-8.151239537594163','Av. Futebol Clube do Porto 411','Marco de Canaveses','4630-203','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa268',0,'87d27990-4ad4-4463-a844-a9f85d67f317',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa269',0,'87d27990-4ad4-4463-a844-a9f85d67f317',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa270',0,'87d27990-4ad4-4463-a844-a9f85d67f317',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa272',0,'87d27990-4ad4-4463-a844-a9f85d67f317',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f318',0,'41.20734997894551', '-8.541531724082542','Quinta da Formiga','Ermesinde','4445-485','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa273',0,'87d27990-4ad4-4463-a844-a9f85d67f318',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa274',0,'87d27990-4ad4-4463-a844-a9f85d67f318',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa275',0,'87d27990-4ad4-4463-a844-a9f85d67f318',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa276',0,'87d27990-4ad4-4463-a844-a9f85d67f318',3,300,300,300,0,null,0);

-- INSERT INTO RecycleBins VALUES ('87d27990-4ad4-4463-a844-a9f85d67f319',0,'41.18861554222219', '-8.49826001393837','R. da Passagem','Valongo','4440-565','Portugal');
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa277',0,'87d27990-4ad4-4463-a844-a9f85d67f319',0,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa278',0,'87d27990-4ad4-4463-a844-a9f85d67f319',1,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa279',0,'87d27990-4ad4-4463-a844-a9f85d67f319',2,300,300,300,0,null,0);
--     INSERT INTO Containers VALUES ('07ebcea5-8b50-4960-92f9-f1b72d8aa280',0,'87d27990-4ad4-4463-a844-a9f85d67f319',3,300,300,300,0,null,0);
GO

DROP PROCEDURE IF EXISTS gerarLeituras;
DROP PROCEDURE IF EXISTS gerarLeiturasEcoponto;
DROP PROCEDURE IF EXISTS gerarRotaFinalizada;
DROP PROCEDURE IF EXISTS gerarRota;
DROP PROCEDURE IF EXISTS gerarDados;
DROP TRIGGER IF EXISTS triggerPickings;
DROP TRIGGER IF EXISTS triggerPickings_BeforeInsert;
DROP TRIGGER IF EXISTS triggerMeasuramentes;

GO


CREATE TRIGGER triggerPickings_BeforeInsert ON Pickings
    INSTEAD OF INSERT
    AS BEGIN
        DECLARE @avgGrowthOccupiedPerDay FLOAT,
                @id VARCHAR(36),
                @volumeRecolhido DECIMAL,
                @containerId VARCHAR(36),
                @datePicking DATETIME,
                @dateLastPicking DATETIME,
                @durationMinute INT,
                @daysDuration FLOAT;

        SELECT @id = id, @volumeRecolhido = volumeRecolhido, @containerId = containerid, @datePicking = [data] FROM INSERTED

        SET @dateLastPicking = (SELECT TOP 1 [data] FROM Pickings WHERE containerid = @containerId ORDER BY data DESC)

        IF @dateLastPicking IS NULL
            BEGIN
                SET @avgGrowthOccupiedPerDay = 0;
                INSERT INTO Pickings VALUES (@id, @volumeRecolhido, @containerId, @datePicking, @avgGrowthOccupiedPerDay)
            END
        ELSE
            BEGIN
                SET @durationMinute = DATEDIFF(SECOND, @dateLastPicking, @datePicking);
                SET @daysDuration = @durationMinute / 86400.0

                SELECT @avgGrowthOccupiedPerDay = (@volumeRecolhido / .27) / @daysDuration 

                INSERT INTO Pickings VALUES (@id, @volumeRecolhido, @containerId, @datePicking, @avgGrowthOccupiedPerDay)
            END
    END
GO

CREATE TRIGGER triggerPickings ON Pickings
    AFTER INSERT
    AS BEGIN
        DECLARE @containerId VARCHAR(36),
                @avg FLOAT;
            
        SELECT @containerId = containerid FROM Inserted 
        SET @avg = (SELECT AVG(avgGrowthOccupiedPerDay) AS "avg" FROM Pickings as pick GROUP BY containerid HAVING containerid = @containerId )

        UPDATE Containers SET avgGrowthOccupiedVolumePerDay = @avg , dateFull = null WHERE id = @containerId
    END
GO

CREATE TRIGGER triggerMeasuramentes ON Measurements
    AFTER INSERT
    AS BEGIN
        DECLARE @containerId VARCHAR(36),
                @distance INT,
                @dateMeasu DATETIME,
                @currentPercentage FLOAT;
            
        SELECT @containerId = containerid, @distance = distance, @dateMeasu = [date] FROM Inserted 

        SET @currentPercentage = 100 -(@distance / 300.0 * 100)
        IF (@currentPercentage = 100)
            BEGIN 
                UPDATE Containers SET currentPercOccupied = @currentPercentage WHERE id = @containerId
                UPDATE Containers SET dateFull = @dateMeasu WHERE id = @containerId
            END
        ELSE
            BEGIN
                UPDATE Containers SET currentPercOccupied = @currentPercentage, dateFull = null WHERE id = @containerId
            END

    END
GO

CREATE PROCEDURE gerarRota
    (
        @dateBegin DATETIME,
        @status INT
    )
    AS BEGIN
        DECLARE @recbins TABLE (tempId VARCHAR(36), id VARCHAR(36));
        DECLARE @routeId VARCHAR(36)= NEWID();
        DECLARE @RecbinId VARCHAR(36);
        DECLARE @countRecBins INT = (SELECT COUNT(*) FROM RecycleBins);
        DECLARE @numberSelectedRecBins INT = FLOOR(RAND()*((@countRecBins + 1)-2)+2);
        DECLARE @countRoutesDb INT = (SELECT COUNT(*) FROM Routes);

        DECLARE @randomEstimatedDistance INT = FLOOR(RAND()*(500-50)+50) * 1000;
        DECLARE @randomEstimatedDuration TIME =  CONVERT(time(0), DATEADD(SECOND, FLOOR(RAND()*(25200-3600)+3600), 0));

        DECLARE @employeeId VARCHAR(36) = (SELECT TOP 1 id FROM Users WHERE [role] = 0)

        -- Escolher aleatoriamente quais os Ecopontos a fazer parte da Rota
        INSERT INTO @recbins (id, tempId) SELECT TOP (@numberSelectedRecBins) * FROM (SELECT id, NEWID() AS tempId FROM RecycleBins) AS RecBinsTemp ORDER BY tempId
        INSERT INTO Routes (id,typeCreation,[name],[status],dateBegin,dateCriation,estimatedDuration,distanceEstimatedKm,employeeid) VALUES 
        (@routeId, 1,CONCAT('Route Teste ', @countRoutesDb), @status ,@dateBegin, DATEADD(MINUTE, 0, @dateBegin), @randomEstimatedDuration, @randomEstimatedDistance, @employeeId)

        SET @dateBegin = DATEADD(MINUTE,  FLOOR(RAND()*(60-5)+5), @dateBegin);
        
        DECLARE @ordem INT = 1;
        -- Associar ecopontos a Rota
        DECLARE cursorSelectedRecBins CURSOR LOCAL FOR SELECT id FROM @recbins FOR READ ONLY
        OPEN cursorSelectedRecBins
        FETCH cursorSelectedRecBins INTO @RecbinId;
        WHILE (@@FETCH_STATUS = 0)
            BEGIN
               INSERT INTO CollectPoint (id, [order], recycleBinid, routeid) VALUES (NEWID(), @ordem, @RecBinId, @routeId);

                SET @dateBegin = DATEADD(MINUTE,  FLOOR(RAND()*(60-5)+5), @dateBegin);
                FETCH cursorSelectedRecBins INTO @RecbinId;
            END
        CLOSE cursorSelectedRecBins

        RETURN;
    END
GO

CREATE PROCEDURE gerarRotaFinalizada
    (
        @dateBegin DATETIME OUTPUT
    )
    AS BEGIN
        DECLARE @containerIds TABLE (linha INT, id VARCHAR(36));
        DECLARE @contId1 VARCHAR(36);
        DECLARE @contId2 VARCHAR(36);
        DECLARE @contId3 VARCHAR(36);
        DECLARE @contId4 VARCHAR(36);
        DECLARE @lastMeasurecontId1 INT;
        DECLARE @lastMeasurecontId2 INT;
        DECLARE @lastMeasurecontId3 INT;
        DECLARE @lastMeasurecontId4 INT;

        DECLARE @variacaoMaxCont1 INT;
        DECLARE @variacaoMaxCont2 INT;
        DECLARE @variacaoMaxCont3 INT;
        DECLARE @variacaoMaxCont4 INT;

        DECLARE @recbins TABLE (tempId VARCHAR(36), id VARCHAR(36));
        DECLARE @routeId VARCHAR(36)= NEWID();
        DECLARE @RecbinId VARCHAR(36);
        DECLARE @countRecBins INT = (SELECT COUNT(*) FROM RecycleBins);
        DECLARE @numberSelectedRecBins INT = FLOOR(RAND()*((@countRecBins + 1)-2)+2);
        DECLARE @countRoutesDb INT = (SELECT COUNT(*) FROM Routes);

        DECLARE @ordem INT = 1;

        DECLARE @employeeId VARCHAR(36) = (SELECT TOP 1 id FROM Users WHERE [role] = 0)

        -- Escolher aleatoriamente quais os Ecopontos a fazer parte da Rota
        INSERT INTO @recbins (id, tempId) SELECT TOP (@numberSelectedRecBins) * FROM (SELECT id, NEWID() AS tempId FROM RecycleBins) AS RecBinsTemp ORDER BY tempId
        INSERT INTO Routes (id,typeCreation,[name],[status],dateBegin,dateCriation,estimatedDuration,distanceEstimatedKm,employeeid) VALUES 
        (@routeId, 1,CONCAT('Route Teste ', @countRoutesDb), 2 ,@dateBegin, DATEADD(MINUTE, 0, @dateBegin), '00:00:00', 0, @employeeId)

        SET @dateBegin = DATEADD(MINUTE,  FLOOR(RAND()*(60-5)+5), @dateBegin);

        -- Associar ecopontos a Rota
        DECLARE cursorSelectedRecBins CURSOR LOCAL FOR SELECT id FROM @recbins FOR READ ONLY

        OPEN cursorSelectedRecBins
        FETCH cursorSelectedRecBins INTO @RecbinId;
        WHILE (@@FETCH_STATUS = 0)
            BEGIN
               
                INSERT INTO CollectPoint (id, [order],recycleBinid, routeid) VALUES (NEWID(), @ordem,@RecBinId, @routeId);

                INSERT INTO @containerIds (linha, id) SELECT ROW_NUMBER() OVER (ORDER BY id) row_num, id FROM Containers WHERE Recbinid = @recBinId

                SET @contId1 = (SELECT id FROM @containerIds WHERE linha = 1)
                SET @contId2 = (SELECT id FROM @containerIds WHERE linha = 2)
                SET @contId3 = (SELECT id FROM @containerIds WHERE linha = 3)
                SET @contId4 = (SELECT id FROM @containerIds WHERE linha = 4)

                SET @lastMeasurecontId1 = (SELECT TOP 1 distance FROM Measurements WHERE containerid = @contId1 ORDER BY [date] DESC)
                SET @lastMeasurecontId2 = (SELECT TOP 1 distance FROM Measurements WHERE containerid = @contId2 ORDER BY [date] DESC)
                SET @lastMeasurecontId3 = (SELECT TOP 1 distance FROM Measurements WHERE containerid = @contId3 ORDER BY [date] DESC)
                SET @lastMeasurecontId4 = (SELECT TOP 1 distance FROM Measurements WHERE containerid = @contId4 ORDER BY [date] DESC)
                
                INSERT INTO Pickings VALUES (NEWID(), 3*3*(3-(@lastMeasurecontId1/100.0)), @contId1, @dateBegin,0)
                INSERT INTO Pickings VALUES (NEWID(), 3*3*(3-(@lastMeasurecontId2/100.0)), @contId2, @dateBegin,0)
                INSERT INTO Pickings VALUES (NEWID(), 3*3*(3-(@lastMeasurecontId3/100.0)), @contId3, @dateBegin,0)
                INSERT INTO Pickings VALUES (NEWID(), 3*3*(3-(@lastMeasurecontId4/100.0)), @contId4, @dateBegin,0)

                INSERT INTO Measurements VALUES (NEWID(), 300, @dateBegin, @contId1)
                INSERT INTO Measurements VALUES (NEWID(), 300, @dateBegin, @contId2)
                INSERT INTO Measurements VALUES (NEWID(), 300, @dateBegin, @contId3)
                INSERT INTO Measurements VALUES (NEWID(), 300, @dateBegin, @contId4)

                SET @dateBegin = DATEADD(MINUTE,  FLOOR(RAND()*(60-5)+5), @dateBegin);
                DELETE FROM @containerIds
                FETCH cursorSelectedRecBins INTO @RecbinId;
            END
        CLOSE cursorSelectedRecBins

        RETURN;
    END
GO


CREATE PROCEDURE gerarLeiturasEcoponto 
    (
        @numeroLeituras INT,
        @intervaloHorasEntreLeituras INT,
        @cont1Id VARCHAR(36), 
        @cont2Id VARCHAR(36), 
        @cont3Id VARCHAR(36), 
        @cont4Id VARCHAR(36),
        @variacaoMaxCont1 INT,
        @variacaoMaxCont2 INT,
        @variacaoMaxCont3 INT,
        @variacaoMaxCont4 INT,
        @date DATETIME
    )
    AS BEGIN
        DECLARE @minCont1 INT,
                @minCont2 INT,
                @minCont3 INT,
                @minCont4 INT;
        DECLARE @prevDistanceCont1 INT,
                @prevDistanceCont2 INT,
                @prevDistanceCont3 INT,
                @prevDistanceCont4 INT;
        DECLARE @maxCont1 INT = @variacaoMaxCont1,
                @maxCont2 INT = @variacaoMaxCont2, 
                @maxCont3 INT = @variacaoMaxCont3,
                @maxCont4 INT = @variacaoMaxCont4;
        DECLARE @ramdomCon1 INT,
                @ramdomCon2 INT,
                @ramdomCon3 INT,
                @ramdomCon4 INT;

        
        SET @prevDistanceCont1 = (SELECT TOP 1 distance FROM Measurements WHERE containerid = @cont1Id ORDER BY [date] DESC)                  
        SET @prevDistanceCont2 = (SELECT TOP 1 distance FROM Measurements WHERE containerid = @cont4Id ORDER BY [date] DESC)                  
        SET @prevDistanceCont3 = (SELECT TOP 1 distance FROM Measurements WHERE containerid = @cont2Id ORDER BY [date] DESC)                  
        SET @prevDistanceCont4 = (SELECT TOP 1 distance FROM Measurements WHERE containerid = @cont3Id ORDER BY [date] DESC)                  

        IF @prevDistanceCont1 IS NULL
            SET @prevDistanceCont1 = 0;
        IF @prevDistanceCont2 IS NULL
            SET @prevDistanceCont2 = 0;
        IF @prevDistanceCont3 IS NULL
            SET @prevDistanceCont3 = 0;
        IF @prevDistanceCont4 IS NULL
            SET @prevDistanceCont4 = 0;

        SET @minCont1 = @prevDistanceCont1/3;
        SET @minCont2 = @prevDistanceCont2/3;
        SET @minCont3 = @prevDistanceCont3/3;
        SET @minCont4 = @prevDistanceCont4/3;

        DECLARE @cont INT = 0;
        WHILE @cont < @numeroLeituras
                BEGIN
                    SET @ramdomCon1 = FLOOR(RAND()*(@maxCont1-@minCont1+1)+@minCont1);
                    SET @ramdomCon2 = FLOOR(RAND()*(@maxCont2-@minCont2+1)+@minCont2);
                    SET @ramdomCon3 = FLOOR(RAND()*(@maxCont3-@minCont3+1)+@minCont3);
                    SET @ramdomCon4 = FLOOR(RAND()*(@maxCont4-@minCont4+1)+@minCont4);

                    INSERT INTO Measurements VALUES (NEWID(), 300-@ramdomCon1*3, @date, @cont1Id)
                    INSERT INTO Measurements VALUES (NEWID(), 300-@ramdomCon2*3, @date, @cont2Id)
                    INSERT INTO Measurements VALUES (NEWID(), 300-@ramdomCon3*3, @date, @cont3Id)
                    INSERT INTO Measurements VALUES (NEWID(), 300-@ramdomCon4*3, @date, @cont4Id)

                    SET @minCont1 = @ramdomCon1;
                    SET @minCont2 = @ramdomCon2;
                    SET @minCont3 = @ramdomCon3;
                    SET @minCont4 = @ramdomCon4;

                    SET @maxCont1 = FLOOR(RAND()*((@ramdomCon1 + @variacaoMaxCont1)-@ramdomCon1)+@ramdomCon1);
                    SET @maxCont2 = FLOOR(RAND()*((@ramdomCon2 + @variacaoMaxCont2)-@ramdomCon2)+@ramdomCon2);
                    SET @maxCont3 = FLOOR(RAND()*((@ramdomCon3 + @variacaoMaxCont3)-@ramdomCon3)+@ramdomCon3);
                    SET @maxCont4 = FLOOR(RAND()*((@ramdomCon4 + @variacaoMaxCont4)-@ramdomCon4)+@ramdomCon4);

                    IF @maxCont1 >= 100
                        SET @maxCont1 = 100;
                    IF @maxCont2 >= 100
                        SET @maxCont2 = 100;
                    IF @maxCont3 >= 100
                        SET @maxCont3 = 100;
                    IF @maxCont4 >= 100
                        SET @maxCont4 = 100;

                    SET @date = DATEADD(HOUR , @intervaloHorasEntreLeituras, @date);
                    SET @cont = @cont + 1;
                END
    RETURN;
    END
GO

CREATE PROCEDURE gerarLeituras 
    (
        @dateStart DATETIME,
        @numeroLeiturasEcoponto INT,
        @intervaloHorasEntreLeituras INT,
        @intervaloVariMaxCont1 INT,
        @intervaloVariMaxCont2 INT,
        @intervaloVariMaxCont3 INT,
        @intervaloVariMaxCont4 INT
    )
    AS BEGIN
        DECLARE @containerIds TABLE (linha INT, id VARCHAR(36));
        DECLARE @contId1 VARCHAR(36),
                @contId2 VARCHAR(36),
                @contId3 VARCHAR(36),
                @contId4 VARCHAR(36),
                @variacaoMaxCont1 INT,
                @variacaoMaxCont2 INT,
                @variacaoMaxCont3 INT,
                @variacaoMaxCont4 INT;

        DECLARE @recBinId VARCHAR(36);
        DECLARE cursorRecBins CURSOR LOCAL FOR SELECT id FROM RecycleBins FOR READ ONLY
         
        OPEN cursorRecBins
        FETCH cursorRecBins INTO @recBinId;
        WHILE (@@FETCH_STATUS = 0)
            BEGIN
                INSERT INTO @containerIds (linha, id) SELECT ROW_NUMBER() OVER (ORDER BY id) row_num, id FROM Containers WHERE Recbinid = @recBinId
                SET @contId1 = (SELECT id FROM @containerIds WHERE linha = 1)
                SET @contId2 = (SELECT id FROM @containerIds WHERE linha = 2)
                SET @contId3 = (SELECT id FROM @containerIds WHERE linha = 3)
                SET @contId4 = (SELECT id FROM @containerIds WHERE linha = 4)

                SET @variacaoMaxCont1 = FLOOR(RAND()*@intervaloVariMaxCont1);
                SET @variacaoMaxCont2 = FLOOR(RAND()*@intervaloVariMaxCont2);
                SET @variacaoMaxCont3 = FLOOR(RAND()*@intervaloVariMaxCont3);
                SET @variacaoMaxCont4 = FLOOR(RAND()*@intervaloVariMaxCont4);

                EXEC gerarLeiturasEcoponto @numeroLeiturasEcoponto, @intervaloHorasEntreLeituras, @contId1, @contId2, @contId3, @contId4, @variacaoMaxCont1, @variacaoMaxCont2, @variacaoMaxCont3, @variacaoMaxCont4, @dateStart;

                DELETE FROM @containerIds
                FETCH cursorRecBins INTO @recBinId;
            END
        CLOSE cursorRecBins
    RETURN;
    END
GO


CREATE PROCEDURE gerarDados 
    (
    @date DATETIME,
    @numeroLeiturasEcoponto INT ,
    @numeroRotasFinalizadas INT,
    @numeroRotasAleatorias INT,
    @intervaloHorasEntreLeituras INT,
    @intervaloVariMaxCont1 INT,
    @intervaloVariMaxCont2 INT,
    @intervaloVariMaxCont3 INT,
    @intervaloVariMaxCont4 INT
    )
    AS BEGIN
        DECLARE @cont INT = 0;
        WHILE(@cont < @numeroRotasFinalizadas)
            BEGIN

            EXEC gerarLeituras @date, @numeroLeiturasEcoponto, @intervaloHorasEntreLeituras, @intervaloVariMaxCont1, @intervaloVariMaxCont2, @intervaloVariMaxCont3, @intervaloVariMaxCont4

            SET @date = DATEADD(HOUR, @numeroLeiturasEcoponto * @intervaloHorasEntreLeituras, @date);
            SET @date = DATEADD(MINUTE,  FLOOR(RAND()*(600-300)+300), @date);

            EXEC gerarRotaFinalizada @date OUTPUT

            SET @cont = @cont + 1;
            END
        
        EXEC gerarRota @date, 2

        EXEC gerarLeituras @date, @numeroLeiturasEcoponto, @intervaloHorasEntreLeituras, @intervaloVariMaxCont1, @intervaloVariMaxCont2, @intervaloVariMaxCont3, @intervaloVariMaxCont4

        SET @date = DATEADD(HOUR, @numeroLeiturasEcoponto * @intervaloHorasEntreLeituras, @date);

        SET @cont = 0;
        DECLARE @randomStatus INT = 2;
        WHILE(@cont < @numeroRotasAleatorias)
            BEGIN

            SET @date = DATEADD(MINUTE, FLOOR(RAND()*(2160- 720)+720), @date);

            WHILE(@randomStatus = 2)
                SET @randomStatus = 3;

            EXEC gerarRota @date, @randomStatus

            SET @randomStatus = 2

            SET @cont = @cont + 1;
            END
    RETURN;
    END
GO


DECLARE @date DATETIME = DATEADD(MONTH,  -3, GETDATE());
--Quantas mais leituras por ecoponto mais provavel e ficar cheio no momento da recolha
DECLARE @numeroLeiturasEcoponto INT = 5;
DECLARE @numeroRotasFinalizadas INT = 50;
DECLARE @numeroRotasAleatorias INT = 50;
DECLARE @intervaloHorasEntreLeituras INT = 4;

--Valores mais altos, mais rapido enchem os contetores
DECLARE @intervaloVariMaxCont1 INT = FLOOR(RAND()*101);
DECLARE @intervaloVariMaxCont2 INT = FLOOR(RAND()*101);
DECLARE @intervaloVariMaxCont3 INT = FLOOR(RAND()*101);
DECLARE @intervaloVariMaxCont4 INT = FLOOR(RAND()*101);

PRINT RTRIM(@intervaloVariMaxCont1)
PRINT RTRIM(@intervaloVariMaxCont2)
PRINT RTRIM(@intervaloVariMaxCont3)
PRINT RTRIM(@intervaloVariMaxCont4)

EXEC gerarDados @date,@numeroLeiturasEcoponto,@numeroRotasFinalizadas,@numeroRotasAleatorias,@intervaloHorasEntreLeituras,@intervaloVariMaxCont1,@intervaloVariMaxCont2,@intervaloVariMaxCont3,@intervaloVariMaxCont4

DROP PROCEDURE IF EXISTS gerarLeiturasEcoponto;
DROP PROCEDURE IF EXISTS gerarLeituras;
DROP PROCEDURE IF EXISTS gerarRotaFinalizada;
DROP PROCEDURE IF EXISTS gerarRota;
DROP PROCEDURE IF EXISTS gerarDados;
DROP TRIGGER IF EXISTS triggerPickings;
DROP TRIGGER IF EXISTS triggerPickings_BeforeInsert;
DROP TRIGGER IF EXISTS triggerMeasuramentes;
GO



