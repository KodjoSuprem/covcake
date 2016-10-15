DROP TABLE IF EXISTS `ref_pays` ;
CREATE TABLE `ref_pays` (
`pays_id` int(11) NOT NULL,
`Libelle` varchar(50) default NULL,
`Libelle_eng` varchar(50) default NULL,
`code` char(3) default NULL,
`code2` char(2) default NULL,
`Capitale` varchar(255) default NULL,
PRIMARY KEY (`pays_id`),
KEY `pays_id` (`pays_id`)
) TYPE=MyISAM;



#
# Dumping data for table 'pays'
#

INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("1", "Afghanistan", "Afghanistan", "AFG", "AF", "Kabul");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("2", "Afrique Du Sud", "South Africa", "ZAF", "ZA", "Pretoria");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("3", "Albanie", "Albania", "ALB", "AL", "Tirane");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("4", "Alg�rie", "Algeria", "DZA", "DZ", "Algers");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("5", "Allemagne", "Germany", "DEU", "DE", "Berlin");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("6", "Andorre", "Andorra", "AND", "AD", "Andorra la Vella");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("7", "Angola", "Angola", "AGO", "AO", "Luanda");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("8", "Anguilla", "Anguilla", "AIA", "AI", "The Valley");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("9", "Antarctique", "Antarctica", "ATA", "AQ", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("10", "Antigua-et-barbuda", "Antigua And Barbuda", "ATG", "AG", "St. John\'s");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("11", "Antilles N�erlandaises", "Netherlands Antilles", "ANT", "AN", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("12", "Arabie Saoudite", "Saudi Arabia", "SAU", "SA", "Riyadh");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("13", "Argentine", "Argentina", "ARG", "AR", "Buenos Aires");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("14", "Arm�nie", "Armenia", "ARM", "AM", "Yerevan");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("15", "Aruba", "Aruba", "ABW", "AW", "Oranjestad");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("16", "Australie", "Australia", "AUS", "AU", "Canberra");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("17", "Autriche", "Austria", "AUT", "AT", "Vienna");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("18", "Azerba�djan", "Azerbaijan", "AZE", "AZ", "Baku");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("19", "Bahamas", "Bahamas", "BHS", "BS", "Nassau");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("20", "Bahre�n", "Bahrain", "BHR", "BH", "Manama");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("21", "Bangladesh", "Bangladesh", "BGD", "BD", "Dhaka");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("22", "Barbade", "Barbados", "BRB", "BB", "BrPaysIdgetown");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("23", "B�larus", "Belarus", "BLR", "BY", "Minsk");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("24", "Belgique", "Belgium", "BEL", "BE", "Brussels");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("25", "Belize", "Belize", "BLZ", "BZ", "Belmopan");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("26", "B�nin", "Benin", "BEN", "BJ", "Port-Novo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("27", "Bermudes", "Bermuda", "BMU", "BM", "Hamilton");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("28", "Bhoutan", "Bhutan", "BTN", "BT", "Thimphu");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("29", "Bolivie", "Bolivia", "BOL", "BO", "Sucre");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("30", "Bosnie-herz�govine", "Bosnia And Herzegovina", "BIH", "BA", "Sarajevo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("31", "Botswana", "Botswana", "BWA", "BW", "Gaborone");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("32", "Bouvet, �le", "Bouvet Island", "BVT", "BV", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("33", "Br�sil", "Brazil", "BRA", "BR", "Brasilia");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("34", "Brun�i Darussalam", "Brunei Darussalam", "BRN", "BN", "Bander Seri Begawan");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("35", "Bulgarie", "Bulgaria", "BGR", "BG", "Sofia");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("36", "Burkina Faso", "Burkina Faso", "BFA", "BF", "Ouagadougou");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("37", "Burundi", "Burundi", "BDI", "BI", "Bujumbura");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("38", "Ca�manes, �les", "Cayman Islands", "CYM", "KY", "George Town");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("39", "Cambodge", "Cambodia", "KHM", "KF", "Phnom Penh");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("40", "Cameroun", "Cameroon", "CMR", "CM", "Yaounde");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("41", "Canada", "Canada", "CAN", "CA", "Ottawa");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("42", "Cap-vert", "Cape Verde", "CPV", "CV", "Praia");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("43", "Centrafricaine, R�publique", "Central African Republic", "CAF", "CF", "Bangui");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("44", "Chili", "Chile", "CHL", "CL", "Santiago");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("45", "Chine", "China", "CHN", "CN", "Beijing");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("46", "Christmas, �le", "Christmas Island", "CXR", "CX", "The Settlement");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("47", "Chypre", "Cyprus", "CYP", "CY", "Nicosia");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("48", "Cocos (keeling), �les", "Cocos (keeling) Islands", "CCK", "CC", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("49", "Colombie", "Colombia", "COL", "CO", "Bogota");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("50", "Comores", "Comoros", "COM", "KM", "Moroni");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("51", "Congo", "Congo", "COG", "CG", "Brazzaville");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("52", "Congo, La R�publique D�mocratique Du", "Congo, The Democratic Republic Of The", "COD", "CD", "Kinshasa");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("53", "Cook, �les", "Cook Islands", "COK", "CK", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("54", "Cor�e, R�publique De", "Korea, Republic Of", "KOR", "KR", "Seoul");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("55", "Cor�e, R�publique Populaire D�mocratique De", "Korea, Democratic People\'s Republic Of", "PRK", "KP", "Pyongyang");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("56", "Costa Rica", "Costa Rica", "CRI", "CR", "San Jose");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("57", "C�te D\'ivoire", "Cote D\'ivoire", "CIV", "CI", "Yamoussoukro");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("58", "Croatie", "Croatia", "HRV", "HR", "Zagreb");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("59", "Cuba", "Cuba", "CUB", "CU", "Havana");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("60", "Danemark", "Denmark", "DNK", "DK", "Copenhagen");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("61", "Djibouti", "Djibouti", "DJI", "DJ", "Djibouti");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("62", "Dominicaine, R�publique", "Dominican Republic", "DOM", "DO", "Santo Domingo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("63", "Dominique", "Dominica", "DMA", "DM", "Roseau");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("64", "�gypte", "Egypt", "EGY", "EG", "Cairo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("65", "El Salvador", "El Salvador", "SLV", "SV", "San Salvador");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("66", "�mirats Arabes Unis", "United Arab Emirates", "ARE", "AE", "Abu Dhabi");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("67", "�quateur", "Ecuador", "ECU", "EC", "Quito");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("68", "�rythr�e", "Eritrea", "ERI", "ER", "Asmara");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("69", "Espagne", "Spain", "ESP", "ES", "MadrPaysId");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("70", "Estonie", "Estonia", "EST", "EE", "Tallinn");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("71", "�tats-unis", "United States", "USA", "US", "Washington D.C.");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("72", "�thiopie", "Ethiopia", "ETH", "ET", "Addis Ababa");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("73", "Falkland, �les (malvinas)", "Falkland Islands (malvinas)", "FLK", "FK", "Port Stanley");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("74", "F�ro�, �les", "Faroe Islands", "FRO", "FO", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("75", "FPaysIdji", "Fiji", "FJI", "FJ", "Suva");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("76", "Finlande", "Finland", "FIN", "FI", "Helsinki");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("77", "France", "France", "FRA", "FR", "Paris");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("78", "Gabon", "Gabon", "GAB", "GA", "Liberville");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("79", "Gambie", "Gambia", "GMB", "GM", "Banjul");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("80", "G�orgie", "Georgia", "GEO", "GE", "Tbilisi");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("81", "G�orgie Du Sud Et Les �les Sandwich Du Sud", "South Georgia And The South Sandwich Islands", "SGS", "GS", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("82", "Ghana", "Ghana", "GHA", "GH", "Accra");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("83", "Gibraltar", "Gibraltar", "GIB", "GI", "Gibraltar");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("84", "Gr�ce", "Greece", "GRC", "GR", "Athens");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("85", "Grenade", "Grenada", "GRD", "GD", "St. George\'s");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("86", "Groenland", "Greenland", "GRL", "GL", "Nuuk");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("87", "Guadeloupe", "Guadeloupe", "GLP", "GP", "Basse-Terre");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("88", "Guam", "Guam", "GUM", "GU", "Agana");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("89", "Guatemala", "Guatemala", "GTM", "GT", "Guatemala City");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("90", "Guin�e", "Guinea", "GIN", "GN", "Conakry");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("91", "Guin�e �quatoriale", "Equatorial Guinea", "GNQ", "GQ", "Malabo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("92", "Guin�e-bissau", "Guinea-bissau", "GNB", "GW", "Bissau");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("93", "Guyana", "Guyana", "GUY", "GY", "Georgetown");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("94", "Guyane Fran�aise", "French Guiana", "GUF", "GF", "Conakry");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("95", "Ha�ti", "Haiti", "HTI", "HT", "Port-au-Prince");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("96", "Heard, �le Et Mcdonald, �les", "Heard Island And Mcdonald Islands", "HMD", "HM", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("97", "Honduras", "Honduras", "HND", "HN", "Tegucigalpa");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("98", "Hong-kong", "Hong Kong", "HKG", "HK", "Hong Kong");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("99", "Hongrie", "Hungary", "HUN", "HU", "Budapest");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("100", "�les Mineures �loign�es Des �tats-unis", "United States Minor Outlying Islands", "UMI", "UM", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("101", "�les Vierges Britanniques", "Virgin Islands, British", "VGB", "VG", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("102", "�les Vierges Des �tats-unis", "Virgin Islands, U.s.", "VIR", "VI", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("103", "Inde", "India", "IND", "IN", "New Delhi");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("104", "Indon�sie", "Indonesia", "PaysIdN", "PaysId", "Jakarta");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("105", "Iran, R�publique Islamique D\'", "Iran, Islamic Republic Of", "IRN", "IR", "Tehran");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("106", "Iraq", "Iraq", "IRQ", "IQ", "Baghdad");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("107", "Irlande", "Ireland", "IRL", "IE", "Dublin");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("108", "Islande", "Iceland", "ISL", "IS", "Reykjav�k");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("109", "Isra�l", "Israel", "ISR", "IL", "Jerusalem");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("110", "Italie", "Italy", "ITA", "IT", "Rome");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("111", "Jama�que", "Jamaica", "JAM", "JM", "Kingston");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("112", "Japon", "Japan", "JPN", "JP", "Tokyo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("113", "Jordanie", "Jordan", "JOR", "JO", "Amman");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("114", "Kazakhstan", "Kazakhstan", "KAZ", "KZ", "Astana");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("115", "Kenya", "Kenya", "KEN", "KE", "Nairobi");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("116", "Kirghizistan", "Kyrgyzstan", "KGZ", "KG", "Bishkek");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("117", "Kiribati", "Kiribati", "KIR", "KI", "Bairiki");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("118", "Kowe�t", "Kuwait", "KWT", "KW", "Kuwait");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("119", "Lao, R�publique D�mocratique Populaire", "Lao People\'s Democratic Republic", "LAO", "LA", "Vientiane");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("120", "Lesotho", "Lesotho", "LSO", "LS", "Maseru");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("121", "Lettonie", "Latvia", "LVA", "LV", "Riga");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("122", "Liban", "Lebanon", "LBN", "LB", "Beirut");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("123", "Lib�ria", "Liberia", "LBR", "LR", "Monrovia");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("124", "Libyenne, Jamahiriya Arabe", "Libyan Arab Jamahiriya", "LBY", "LY", "Tripoli");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("125", "Liechtenstein", "Liechtenstein", "LIE", "LI", "Vaduz");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("126", "Lituanie", "Lithuania", "LTU", "LT", "Vilnius");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("127", "Luxembourg", "Luxembourg", "LUX", "LU", "Luxembourg");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("128", "Macao", "Macao", "MAC", "MO", "Macao");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("129", "Mac�doine, L\'ex-r�publique Yougoslave De", "Macedonia, The Former Yugoslav Republic Of", "MKD", "MK", "Skopje");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("130", "Madagascar", "Madagascar", "MDG", "MG", "Antananarivo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("131", "Malaisie", "Malaysia", "MYS", "MY", "Kuala Lumpur");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("132", "Malawi", "Malawi", "MWI", "MW", "Lilongwe");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("133", "Maldives", "Maldives", "MDV", "MV", "Mal�");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("134", "Mali", "Mali", "MLI", "ML", "Bamako");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("135", "Malte", "Malta", "MLT", "MT", "Valletta");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("136", "Mariannes Du Nord, �les", "Northern Mariana Islands", "MNP", "MP", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("137", "Maroc", "Morocco", "MAR", "MA", "Rabat");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("138", "Marshall, �les", "Marshall Islands", "MHL", "MH", "Majuro");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("139", "Martinique", "Martinique", "MTQ", "MQ", "Fort-de-France");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("140", "Maurice", "Mauritius", "MUS", "MU", "Port Louis");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("141", "Mauritanie", "Mauritania", "MRT", "MR", "Nouakchott");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("142", "Mayotte", "Mayotte", "MYT", "YT", "Mamoudzou");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("143", "Mexique", "Mexico", "MEX", "MX", "Mexico City");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("144", "Micron�sie, �tats F�d�r�s De", "Micronesia, Federated States Of", "FSM", "FM", "Palikir");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("145", "Moldova, R�publique De", "Moldova, Republic Of", "MDA", "MD", "Chisinau");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("146", "Monaco", "Monaco", "MCO", "MC", "Monaco");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("147", "Mongolie", "Mongolia", "MNG", "MN", "Ulaanbaatar");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("148", "Montserrat", "Montserrat", "MSR", "MS", "Plymouth");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("149", "Mozambique", "Mozambique", "MOZ", "MZ", "Maputo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("150", "Myanmar", "Myanmar", "MMR", "MM", "Yang�n");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("151", "Namibie", "Namibia", "NAM", "NA", "Windhoek");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("152", "Nauru", "Nauru", "NRU", "NR", "Yaren");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("153", "N�pal", "Nepal", "NPL", "NP", "Kathmandu");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("154", "Nicaragua", "Nicaragua", "NIC", "NI", "Managua");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("155", "Niger", "Niger", "NER", "NE", "Niamey");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("156", "Nig�ria", "Nigeria", "NGA", "NG", "Abuja");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("157", "Niu�", "Niue", "NIU", "NU", "Alofi");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("158", "Norfolk, �le", "Norfolk Island", "NFK", "NF", "Kingston");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("159", "Norv�ge", "Norway", "NOR", "NO", "Oslo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("160", "Nouvelle-cal�donie", "New Caledonia", "NCL", "NC", "Noumea");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("161", "Nouvelle-z�lande", "New Zealand", "NZL", "NZ", "Wellington");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("162", "Oc�an Indien, Territoire Britannique De L\'", "British Indian Ocean Territory", "IOT", "IO", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("163", "Oman", "Oman", "OMN", "OM", "Muscat");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("164", "Ouganda", "Uganda", "UGA", "UG", "Kampala");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("165", "Ouzb�kistan", "Uzbekistan", "UZB", "UZ", "Tashkent");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("166", "Pakistan", "Pakistan", "PAK", "PK", "Islamabad");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("167", "Palaos", "Palau", "PLW", "PW", "Koror");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("168", "Palestinien Occup�, Territoire", "Palestinian Territory, Occupied", "PSE", "PS", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("169", "Panama", "Panama", "PAN", "PA", "Panama City");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("170", "Papouasie-nouvelle-guin�e", "Papua New Guinea", "PNG", "PG", "Port Moresby");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("171", "Paraguay", "Paraguay", "PRY", "PY", "Asunci�n");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("172", "Pays-bas", "Netherlands", "NLD", "NL", "Amsterdam");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("173", "P�rou", "Peru", "PER", "PE", "Lima");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("174", "Philippines", "Philippines", "PHL", "PH", "Manila");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("175", "Pitcairn", "Pitcairn", "PCN", "PN", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("176", "Pologne", "Poland", "POL", "PL", "Warsaw");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("177", "Polyn�sie Fran�aise", "French Polynesia", "PYF", "PF", "Papeete");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("178", "Porto Rico", "Puerto Rico", "PRI", "PR", "San Juan");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("179", "Portugal", "Portugal", "PRT", "PT", "Lisbon");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("180", "Qatar", "Qatar", "QAT", "QA", "Doha");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("181", "R�union", "Reunion", "REU", "RE", "Saint-Deni");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("182", "Roumanie", "Romania", "ROM", "RO", "Bucharest");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("183", "Royaume-uni", "United Kingdom", "GBR", "GB", "London");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("184", "Russie, F�d�ration De", "Russian Federation", "RUS", "RU", "Moscow");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("185", "Rwanda", "Rwanda", "RWA", "RW", "Kigali");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("186", "Sahara OccPaysIdental", "Western Sahara", "ESH", "EH", "Laayoune");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("187", "Saint-kitts-et-nevis", "Saint Kitts And Nevis", "KNA", "KN", "Basseterre");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("188", "Saint-marin", "San Marino", "SMR", "SM", "San Marino");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("189", "Saint-pierre-et-miquelon", "Saint Pierre And Miquelon", "SPM", "PM", "Saint Pierre");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("190", "Saint-si�ge (�tat De La Cit� Du Vatican)", "Holy See (vatican City State)", "VAT", "VA", "Vatican");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("191", "Saint-vincent-et-les Grenadines", "Saint Vincent And The Grenadines", "VCT", "VC", "Kingstown");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("192", "Sainte-h�l�ne", "Saint Helena", "SHN", "SH", "Jamestown");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("193", "Sainte-lucie", "Saint Lucia", "LCA", "LC", "Castries");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("194", "Salomon, �les", "Solomon Islands", "SLB", "SB", "Honiara");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("195", "Samoa", "Samoa", "WSM", "WS", "Apia");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("196", "Samoa Am�ricaines", "American Samoa", "ASM", "AS", "Pago Pago");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("197", "Sao Tom�-et-principe", "Sao Tome And Principe", "STP", "ST", "S�o Tom�");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("198", "S�n�gal", "Senegal", "SEN", "SN", "Dakar");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("199", "Seychelles", "Seychelles", "SYC", "CS", "Victoria");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("200", "Sierra Leone", "Sierra Leone", "SLE", "SC", "Freetown");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("201", "Singapour", "Singapore", "SGP", "SG", "Singapore");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("202", "Slovaquie", "Slovakia", "SVK", "SK", "Bratislava");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("203", "Slov�nie", "Slovenia", "SVN", "SI", "Ljubljana");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("204", "Somalie", "Somalia", "SOM", "SO", "Mogadishu");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("205", "Soudan", "Sudan", "SDN", "SD", "Khartoum");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("206", "Sri Lanka", "Sri Lanka", "LKA", "LK", "Colombo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("207", "Su�de", "Sweden", "SWE", "SE", "Stockholm");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("208", "Suisse", "Switzerland", "CHE", "CH", "Bern");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("209", "Suriname", "Suriname", "SUR", "SR", "Paramaribo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("210", "Svalbard Et �le Jan Mayen", "Svalbard And Jan Mayen", "SJM", "SJ", "Longyearbyen");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("211", "Swaziland", "Swaziland", "SWZ", "SZ", "Mbabane");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("212", "Syrienne, R�publique Arabe", "Syrian Arab Republic", "SYR", "SY", "Damascus");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("213", "Tadjikistan", "Tajikistan", "TJK", "TJ", "Dushanbe");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("214", "Ta�wan, Province De Chine", "Taiwan, Province Of China", "TWN", "TW", "Taipei");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("215", "Tanzanie, R�publique-unie De", "Tanzania, United Republic Of", "TZA", "TZ", "Dodoma");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("216", "Tchad", "Chad", "TCD", "TD", "N\'Djamena");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("217", "Tch�que, R�publique", "Czech Republic", "CZE", "CZ", "Prague");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("218", "Terres Australes Fran�aises", "French Southern Territories", "ATF", "TF", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("219", "Tha�lande", "Thailand", "THA", "TH", "Bangkok");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("220", "Timor-leste", "Timor-leste", "TMP", "TL", "Dili");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("221", "Togo", "Togo", "TGO", "TG", "Lome");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("222", "Tokelau", "Tokelau", "TKL", "TK", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("223", "Tonga", "Tonga", "TON", "TO", "Nuku\'alofa");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("224", "Trinit�-et-tobago", "TrinPaysIdad And Tobago", "TTO", "TT", "Port of Spain");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("225", "Tunisie", "Tunisia", "TUN", "TN", "Tunis");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("226", "Turkm�nistan", "Turkmenistan", "TKM", "TM", "Ashgabat");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("227", "Turks Et Ca�ques, �les", "Turks And Caicos Islands", "TCA", "TC", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("228", "Turquie", "Turkey", "TUR", "TR", "Ankara");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("229", "Tuvalu", "Tuvalu", "TUV", "TV", "Funafuti");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("230", "Ukraine", "Ukraine", "UKR", "UA", "Kiev");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("231", "Uruguay", "Uruguay", "URY", "UY", "MontevPaysIdeo");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("232", "Vanuatu", "Vanuatu", "VUT", "VU", "Port Vila");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("233", "Venezuela", "Venezuela", "VEN", "VE", "Caracas");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("234", "Viet Nam", "Viet Nam", "VNM", "VN", "Hanoi");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("235", "Wallis Et Futuna", "Wallis And Futuna", "WLF", "WF", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("236", "Y�men", "Yemen", "YEM", "YE", "Sana");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("237", "Yougoslavie", "Yugoslavia", "YUG", "YU", NULL);
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("238", "Zambie", "Zambia", "ZMB", "ZM", "Lusaka");
INSERT INTO ref_pays  (PaysId, Libelle, LibelleEng, code, code2, Capitale) VALUES("239", "Zimbabwe", "Zimbabwe", "ZWE", "ZW", "Harare");[/codebox] 