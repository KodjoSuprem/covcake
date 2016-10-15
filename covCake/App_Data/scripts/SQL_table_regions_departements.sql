Code :
- 
-- Structure de la table region
-- 

CREATE TABLE region (
  NUMREGION char(32) NOT NULL default '',
  NOMREGION char(32) default NULL,
  PRIMARY KEY  (NUMREGION)
) TYPE=MyISAM;

-- 
-- Contenu de la table region
-- 

INSERT INTO region VALUES ('1', 'Alsace');
INSERT INTO region VALUES ('2', 'Aquitaine');
INSERT INTO region VALUES ('3', 'Auvergne');
INSERT INTO region VALUES ('4', 'Basse Normandie');
INSERT INTO region VALUES ('5', 'Bourgogne');
INSERT INTO region VALUES ('6', 'Bretagne');
INSERT INTO region VALUES ('7', 'Centre');
INSERT INTO region VALUES ('8', 'Champagne Ardenne');
INSERT INTO region VALUES ('9', 'Corse');
INSERT INTO region VALUES ('10', 'Franche Comte');
INSERT INTO region VALUES ('11', 'Haute Normandie');
INSERT INTO region VALUES ('12', 'Ile de France');
INSERT INTO region VALUES ('13', 'Languedoc Roussillon');
INSERT INTO region VALUES ('14', 'Limousin');
INSERT INTO region VALUES ('15', 'Lorraine');
INSERT INTO region VALUES ('16', 'Midi-Pyrénées');
INSERT INTO region VALUES ('17', 'Nord Pas de Calais');
INSERT INTO region VALUES ('18', 'P.A.C.A');
INSERT INTO region VALUES ('19', 'Pays de la Loire');
INSERT INTO region VALUES ('20', 'Picardie');
INSERT INTO region VALUES ('21', 'Poitou Charente');
INSERT INTO region VALUES ('22', 'Rhone Alpes');

        
-- 
-- Structure de la table ref_departements
-- 

CREATE TABLE ref_departements (
  NUMDEPT char(2) NOT NULL default '',
  NUMREGION char(32) NOT NULL default '',
  NOMDEPT char(32) default NULL,
  PRIMARY KEY  (NUMDEPT),
  KEY FK_ref_departements_REGION (NUMREGION)
) TYPE=MyISAM;

-- 
-- Contenu de la table ref_departements
-- 

INSERT INTO ref_departements VALUES ('1', '22', 'Ain');
INSERT INTO ref_departements VALUES ('2', '20', 'Aisne');
INSERT INTO ref_departements VALUES ('3', '3', 'Allier');
INSERT INTO ref_departements VALUES ('4', '18', 'Alpes de haute provence');
INSERT INTO ref_departements VALUES ('5', '18', 'Hautes alpes');
INSERT INTO ref_departements VALUES ('6', '18', 'Alpes maritimes');
INSERT INTO ref_departements VALUES ('7', '22', 'Ardèche');
INSERT INTO ref_departements VALUES ('8', '8', 'Ardennes');
INSERT INTO ref_departements VALUES ('9', '16', 'Ariège');
INSERT INTO ref_departements VALUES ('10', '8', 'Aube');
INSERT INTO ref_departements VALUES ('11', '13', 'Aude');
INSERT INTO ref_departements VALUES ('12', '16', 'Aveyron');
INSERT INTO ref_departements VALUES ('13', '18', 'Bouches du rhône');
INSERT INTO ref_departements VALUES ('14', '4', 'Calvados');
INSERT INTO ref_departements VALUES ('15', '3', 'Cantal');
INSERT INTO ref_departements VALUES ('16', '21', 'Charente');
INSERT INTO ref_departements VALUES ('17', '21', 'Charente maritime');
INSERT INTO ref_departements VALUES ('18', '7', 'Cher');
INSERT INTO ref_departements VALUES ('19', '14', 'Corrèze');
INSERT INTO ref_departements VALUES ('21', '5', 'Côte d''or');
INSERT INTO ref_departements VALUES ('22', '6', 'Côtes d''Armor');
INSERT INTO ref_departements VALUES ('23', '14', 'Creuse');
INSERT INTO ref_departements VALUES ('24', '2', 'Dordogne');
INSERT INTO ref_departements VALUES ('25', '10', 'Doubs');
INSERT INTO ref_departements VALUES ('26', '22', 'Drôme');
INSERT INTO ref_departements VALUES ('27', '11', 'Eure');
INSERT INTO ref_departements VALUES ('28', '7', 'Eure et Loir');
INSERT INTO ref_departements VALUES ('29', '6', 'Finistère');
INSERT INTO ref_departements VALUES ('30', '13', 'Gard');
INSERT INTO ref_departements VALUES ('31', '16', 'Haute garonne');
INSERT INTO ref_departements VALUES ('32', '16', 'Gers');
INSERT INTO ref_departements VALUES ('33', '2', 'Gironde');
INSERT INTO ref_departements VALUES ('34', '13', 'Hérault');
INSERT INTO ref_departements VALUES ('35', '6', 'Ile et Vilaine');
INSERT INTO ref_departements VALUES ('36', '7', 'Indre');
INSERT INTO ref_departements VALUES ('37', '7', 'Indre et Loire');
INSERT INTO ref_departements VALUES ('38', '22', 'Isère');
INSERT INTO ref_departements VALUES ('39', '10', 'Jura');
INSERT INTO ref_departements VALUES ('40', '2', 'Landes');
INSERT INTO ref_departements VALUES ('41', '7', 'Loir et Cher');
INSERT INTO ref_departements VALUES ('42', '22', 'Loire');
INSERT INTO ref_departements VALUES ('43', '3', 'Haute loire');
INSERT INTO ref_departements VALUES ('44', '19', 'Loire Atlantique');
INSERT INTO ref_departements VALUES ('45', '7', 'Loiret');
INSERT INTO ref_departements VALUES ('46', '16', 'Lot');
INSERT INTO ref_departements VALUES ('47', '2', 'Lot et Garonne');
INSERT INTO ref_departements VALUES ('48', '13', 'Lozère');
INSERT INTO ref_departements VALUES ('49', '19', 'Maine et Loire');
INSERT INTO ref_departements VALUES ('50', '4', 'Manche');
INSERT INTO ref_departements VALUES ('51', '8', 'Marne');
INSERT INTO ref_departements VALUES ('52', '8', 'Haute Marne');
INSERT INTO ref_departements VALUES ('53', '19', 'Mayenne');
INSERT INTO ref_departements VALUES ('54', '15', 'Meurthe et Moselle');
INSERT INTO ref_departements VALUES ('55', '15', 'Meuse');
INSERT INTO ref_departements VALUES ('56', '6', 'Morbihan');
INSERT INTO ref_departements VALUES ('57', '15', 'Moselle');
INSERT INTO ref_departements VALUES ('58', '5', 'Nièvre');
INSERT INTO ref_departements VALUES ('59', '17', 'Nord');
INSERT INTO ref_departements VALUES ('60', '20', 'Oise');
INSERT INTO ref_departements VALUES ('61', '4', 'Orne');
INSERT INTO ref_departements VALUES ('62', '17', 'Pas de Calais');
INSERT INTO ref_departements VALUES ('63', '3', 'Puy de Dôme');
INSERT INTO ref_departements VALUES ('64', '2', 'Pyrénées Atlantiques');
INSERT INTO ref_departements VALUES ('65', '16', 'Hautes Pyrénées');
INSERT INTO ref_departements VALUES ('66', '13', 'Pyrénées Orientales');
INSERT INTO ref_departements VALUES ('67', '1', 'Bas Rhin');
INSERT INTO ref_departements VALUES ('68', '1', 'Haut Rhin');
INSERT INTO ref_departements VALUES ('69', '22', 'Rhône');
INSERT INTO ref_departements VALUES ('70', '10', 'Haute Saône');
INSERT INTO ref_departements VALUES ('71', '5', 'Saône et Loire');
INSERT INTO ref_departements VALUES ('72', '19', 'Sarthe');
INSERT INTO ref_departements VALUES ('73', '22', 'Savoie');
INSERT INTO ref_departements VALUES ('74', '22', 'Haute Savoie');
INSERT INTO ref_departements VALUES ('75', '12', 'Paris');
INSERT INTO ref_departements VALUES ('76', '11', 'Seine Maritime');
INSERT INTO ref_departements VALUES ('77', '12', 'Seine et Marne');
INSERT INTO ref_departements VALUES ('78', '12', 'Yvelines');
INSERT INTO ref_departements VALUES ('79', '21', 'Deux Sèvres');
INSERT INTO ref_departements VALUES ('80', '20', 'Somme');
INSERT INTO ref_departements VALUES ('81', '16', 'Tarn');
INSERT INTO ref_departements VALUES ('82', '16', 'Tarn et Garonne');
INSERT INTO ref_departements VALUES ('83', '18', 'Var');
INSERT INTO ref_departements VALUES ('84', '18', 'Vaucluse');
INSERT INTO ref_departements VALUES ('85', '19', 'Vendée');
INSERT INTO ref_departements VALUES ('86', '21', 'Vienne');
INSERT INTO ref_departements VALUES ('87', '14', 'Haute Vienne');
INSERT INTO ref_departements VALUES ('88', '15', 'Vosge');
INSERT INTO ref_departements VALUES ('89', '5', 'Yonne');
INSERT INTO ref_departements VALUES ('90', '10', 'Territoire de Belfort');
INSERT INTO ref_departements VALUES ('91', '12', 'Essonne');
INSERT INTO ref_departements VALUES ('92', '12', 'Haut de seine');
INSERT INTO ref_departements VALUES ('93', '12', 'Seine Saint Denis');
INSERT INTO ref_departements VALUES ('94', '12', 'Val de Marne');
INSERT INTO ref_departements VALUES ('95', '12', 'Val d Oise');
INSERT INTO ref_departements VALUES ('2a', '9', 'Corse du Sud');
INSERT INTO ref_departements VALUES ('2b', '9', 'Haute Corse');
