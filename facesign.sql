/*
SQLyog FULL v10.41 
MySQL - 5.7.28-log : Database - facesign
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`facesign` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `facesign`;

/*Table structure for table `admin` */

DROP TABLE IF EXISTS `admin`;

CREATE TABLE `admin` (
  `pwd` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `admin` */

insert  into `admin`(`pwd`) values ('123456');

/*Table structure for table `signlog` */

DROP TABLE IF EXISTS `signlog`;

CREATE TABLE `signlog` (
  `id` varchar(20) NOT NULL,
  `signid` varchar(100) NOT NULL DEFAULT '0',
  `flag` int(20) DEFAULT '0',
  `signtime` varchar(20) DEFAULT NULL,
  `daytime` varchar(20) NOT NULL,
  PRIMARY KEY (`signid`),
  KEY `id` (`id`),
  CONSTRAINT `signlog_ibfk_1` FOREIGN KEY (`id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `signlog` */

insert  into `signlog`(`id`,`signid`,`flag`,`signtime`,`daytime`) values ('0','02019-12-21',1,'2019/12/21 13:38:00','2019-12-21'),('0','02019-12-22',1,'2019/12/22 10:33:01','2019-12-22'),('2','22019-12-21',1,'2019/12/21 18:27:19','2019-12-21');

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `id` varchar(20) NOT NULL,
  `name` varchar(20) NOT NULL,
  `faceimg` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `user` */

insert  into `user`(`id`,`name`,`faceimg`) values ('0','徐广超','https://www.ylesb.com/csimg/0xgc.jpg'),('1','纪文杰','https://www.ylesb.com/csimg/1jwj.jpg'),('2','蕾蕾','https://www.ylesb.com/csimg/2tl.jpg');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
