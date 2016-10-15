CREATE DATABASE IF NOT EXISTS `db_covoyage` ;

DROP TABLE IF EXISTS `cov_user`;
DROP TABLE IF EXISTS `cov_destination`;
DROP TABLE IF EXISTS `cov_photo`; 
DROP TABLE IF EXISTS `cov_album`;
DROP TABLE IF EXISTS `cov_user_profile`;
DROP TABLE IF EXISTS `cov_privmsgs`;
DROP TABLE IF EXISTS `cov_residence`;
DROP TABLE IF EXISTS `cov_transport`;
DROP TABLE IF EXISTS `cov_projet`;
DROP TABLE IF EXISTS cov_favDest;
DROP TABLE IF EXISTS cov_usergroup;
DROP TABLE IF EXISTS cov_userprojet;

CREATE TABLE `cov_user` (
  `usr_id` int(11)  UNSIGNED  NOT NULL auto_increment,
  `usr_userName` varchar(32) NOT NULL,
  `usr_nom` varchar(30) default NULL,
  `usr_prenom` varchar(30) default NULL,
  `usr_sexe` BOOL NOT NULL default '0',
  `usr_hashedPass` varchar(128) default NULL,
  `usr_email` varchar(255) default NULL,
  `usr_locked` BOOL  default '0',
  `usr_registrationKey` varchar(32) default NULL,
  `usr_actif` BOOL NOT NULL default '0',
  `usr_regtime` int NOT NULL default '0',
  PRIMARY KEY  (`usr_id` ),
  UNIQUE KEY `usr_userName` (`usr_userName`),
  UNIQUE KEY `usr_email` (`usr_email`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;



CREATE TABLE `cov_destination`(
`dest_id` int(11)  UNSIGNED  NOT NULL auto_increment,
`dest_paysId` int(11)  NOT NULL,
`dest_ville` varchar(99),
`dest_region` varchar(99),
PRIMARY KEY (`dest_id`),
KEY (`dest_paysId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;



CREATE TABLE `cov_photo`(
`photo_id` int(11) UNSIGNED NOT NULL auto_increment,
`photo_albumId` int(11) UNSIGNED NOT NULL,
`photo_filesysPath` text NOT NULL,
PRIMARY KEY (`photo_id`),
KEY (`photo_albumId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;



CREATE TABLE `cov_album`(
`album_id` int(11) UNSIGNED NOT NULL auto_increment,
`album_ownerId` int(11) UNSIGNED NOT NULL,
`album_private` BOOL default '0',
PRIMARY KEY (`album_id`),
KEY(`album_ownerId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;


CREATE TABLE `cov_user_profile`(
`usr_profile_userId` int(11) UNSIGNED NOT NULL,
`usr_profile_ville` varchar(99),
 `usr_profile_datenaiss` DATE NOT NULL,
`usr_profile_publicmail` BOOL NOT NULL DEFAULT '0',
`usr_profile_selfdesc` text,
KEY(`usr_profile_userId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

CREATE TABLE `cov_favDest`
(
`favDest_userId` int(11) UNSIGNED NOT NULL,
`favDest_destId` int(11) UNSIGNED NOT NULL,

KEY(favDest_userId),
KEY(favDest_userId)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;


CREATE TABLE `cov_userGroup`(
`usrgrp_id` int(11) UNSIGNED NOT NULL,
`usrgrp_projetId` int(11) UNSIGNED NOT NULL ,
`usrgrp_leaderId` int(11) UNSIGNED,
PRIMARY KEY (`usrgrp_id`),
KEY (`usrgrp_projetId`),
KEY(`usrgrp_leaderId`)

) ENGINE=MyISAM DEFAULT CHARSET=utf8;



CREATE TABLE `cov_privmsgs`(
  `privmsgs_id` int(11) UNSIGNED NOT NULL auto_increment,
  `privmsgs_type` tinyint(4) NOT NULL default '0',
  `privmsgs_from_userid` mediumint(8) NOT NULL default '0',
  `privmsgs_to_userid` mediumint(8) NOT NULL default '0',
  `privmsgs_date` TIMESTAMP NOT NULL,
  `privmsgs_ip` varchar(15) NOT NULL default '',
  `privmsgs_enable_bbcode` BOOL NOT NULL default '0',
  `privmsgs_isNew` BOOL NOT NULL default '0',
  `privmsgs_subject` varchar(255) NOT NULL default '0',
  `privmsgs_text` text,
  PRIMARY KEY (`privmsgs_id`),
  KEY privmsgs_from_userid (`privmsgs_from_userid`),
  KEY privmsgs_to_userid (`privmsgs_to_userid`)
)  ENGINE=MyISAM DEFAULT CHARSET=utf8;



CREATE TABLE `cov_residence`(
`residence_id` int(11) UNSIGNED NOT NULL auto_increment,
`residence_nom` varchar(50) ,
`residence_adr` text NOT NULL,
`residence_ville` varchar(99) NOT NULL ,
`residence_paysId` int(11) NOT NULL,
`residence_tel` varchar(30),
`residence_siteweb` text,
`residence_prix` varchar(20),
 PRIMARY KEY (`residence_id`),
 KEY (`residence_paysId`)

) ENGINE=MyISAM DEFAULT CHARSET=utf8;




CREATE TABLE `cov_transport`(
`trans_id` int(11) UNSIGNED NOT NULL auto_increment,
`trans_lib` varchar(50) NOT NULL,
  PRIMARY KEY (`trans_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;


CREATE TABLE `cov_userprojet`
(
   `usrprojet_userId` int(11) UNSIGNED NOT NULL,
    `usrprojet_projetId` int(11) UNSIGNED NOT NULL, 
    `usrprojet_typepersrech` tinyint(1) UNSIGNED NOT NULL DEFAULT '2',
  KEY(`usrprojet_projetId`),
    KEY(`usrprojet_userId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

CREATE TABLE `cov_projet`(
   `projet_id` int(11) UNSIGNED NOT NULL auto_increment,
   `projet_destId` int(11) UNSIGNED NOT NULL,
   `projet_transportId` int(11) UNSIGNED ,
   `projet_residenceId` int(11) UNSIGNED,
   `projet_dispo_dateDebut` DATE NOT NULL,
   `projet_dispo_dateFin` DATE NOT NULL,
   `projet_motivation` TEXT,
   `projet_realise` BOOL NOT NULL DEFAULT '0',
    PRIMARY KEY (`projet_id`),
    KEY(`projet_destId`),
    KEY(`projet_transportId`),
    KEY(`projet_residenceId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;


ALTER TABLE cov_userGroup
ADD CONSTRAINT FK_usrgrp_projet FOREIGN KEY(`usrgrp_projetId`) REFERENCES cov_projet(projet_id)
;

ALTER TABLE cov_favDest
ADD CONSTRAINT `FK_favDest_id` FOREIGN KEY (`favDest_destId`) REFERENCES `cov_destination` (`dest_id`),
ADD CONSTRAINT `FK_favDest_userId` FOREIGN KEY (`favDest_userId`) REFERENCES `cov_user` (`user_id`)
;


ALTER TABLE cov_photo 
 ADD CONSTRAINT `FK_photo_albumId` FOREIGN KEY (`photo_albumId`) REFERENCES `cov_album` (`album_id`)
 ;
 
ALTER TABLE cov_destination 
 ADD CONSTRAINT `FK_dest_paysId` FOREIGN KEY (`dest_paysId`) REFERENCES `ref_pays` (`pays_id`)
;

ALTER TABLE cov_album
ADD CONSTRAINT `FK_album_ownerId` FOREIGN KEY (`album_ownerId`) REFERENCES `cov_user` (`usr_id`)
;

ALTER TABLE cov_user_profile
ADD CONSTRAINT `FK_usr_profile_userId` FOREIGN KEY (`usr_profile_userId`) REFERENCES `cov_user` (`usr_id`)
;



ALTER TABLE cov_privmsgs
 ADD CONSTRAINT `FK_cov_privmsgs_from_userid` FOREIGN KEY (`privmsgs_from_userid`) REFERENCES `cov_user` (`usr_id`),
 ADD CONSTRAINT `FK_cov_privmsgs_to_userid` FOREIGN KEY (`privmsgs_to_userid`) REFERENCES `cov_user` (`usr_id`)
 ;

ALTER TABLE cov_residence
ADD CONSTRAINT FK_residence_paysId FOREIGN KEY(residence_paysId) REFERENCES `ref_pays`(`pays_id`)
;

ALTER TABLE cov_projet
 ADD CONSTRAINT `FK_cov_projet_destId` FOREIGN KEY (`projet_destId`) REFERENCES `cov_destination` (`dest_id`),
 ADD CONSTRAINT `FK_cov_projet_transportId` FOREIGN KEY (`projet_transportId`) REFERENCES `cov_transport` (`trans_id`),
 ADD CONSTRAINT `FK_cov_projet_residenceId` FOREIGN KEY (`projet_residenceId`) REFERENCES `cov_residence` (`residence_id`)
 ;



