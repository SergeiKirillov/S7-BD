-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.37-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema basa
--

CREATE DATABASE IF NOT EXISTS basa;
USE basa;

--
-- Definition of table `mec`
--

DROP TABLE IF EXISTS `mec`;
CREATE TABLE `mec` (
  `time` varchar(25) NOT NULL default '',
  `VKlet` varchar(10) default NULL,
  `IzadR` varchar(10) default NULL,
  `IzadM` varchar(10) default NULL,
  `NKlet` varchar(10) default NULL,
  `NRazm` varchar(10) default NULL,
  `NMot` varchar(10) default NULL,
  `TRazm` varchar(10) default NULL,
  `TMot` varchar(10) default NULL,
  `RRazm` varchar(10) default NULL,
  `RMot` varchar(10) default NULL,
  `NVlev` varchar(10) default NULL,
  `NVpr` varchar(10) default NULL,
  PRIMARY KEY  USING BTREE (`time`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;

--
-- Dumping data for table `mec`
--


--
-- Definition of table `mec_100ms_temp`
--

DROP TABLE IF EXISTS `mec_100ms_temp`;
CREATE TABLE `mec_100ms_temp` (
  `time` varchar(25) NOT NULL default '',
  `VKlet` varchar(10) default NULL,
  `IzadR` varchar(10) default NULL,
  `IzadM` varchar(10) default NULL,
  `NKlet` varchar(10) default NULL,
  `NRazm` varchar(10) default NULL,
  `NMot` varchar(10) default NULL,
  `TRazm` varchar(10) default NULL,
  `TMot` varchar(10) default NULL,
  `RRazm` varchar(10) default NULL,
  `RMot` varchar(10) default NULL,
  `NVlev` varchar(10) default NULL,
  `NVpr` varchar(10) default NULL,
  `IvozM` varchar(10) default NULL,
  `Imot` varchar(10) default NULL,
  `Urazm` varchar(10) default NULL,
  `IvozR` varchar(10) default NULL,
  `Umot` varchar(10) default NULL,
  `IRUZ4` varchar(10) default NULL,
  `IRUZ5` varchar(10) default NULL,
  `IMUZ4` varchar(10) default NULL,
  `IMUZ5` varchar(10) default NULL,
  `IzovK` varchar(10) default NULL,
  `Ukl` varchar(10) default NULL,
  `IKUZ4` varchar(10) default NULL,
  `IKUZ5` varchar(10) default NULL,
  `ObgTek` varchar(10) default NULL,
  `DatObgDo` varchar(10) default NULL,
  `DatObgZa` varchar(10) default NULL,
  PRIMARY KEY  USING BTREE (`time`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;

--
-- Dumping data for table `mec_100ms_temp`
--

/*!40000 ALTER TABLE `mec_100ms_temp` DISABLE KEYS */;
/*!40000 ALTER TABLE `mec_100ms_temp` ENABLE KEYS */;


--
-- Definition of table `mec_temp`
--

DROP TABLE IF EXISTS `mec_temp`;
CREATE TABLE `mec_temp` (
  `time` varchar(25) NOT NULL default '',
  `VKlet` varchar(10) default NULL,
  `IzadR` varchar(10) default NULL,
  `IzadM` varchar(10) default NULL,
  `NKlet` varchar(10) default NULL,
  `NRazm` varchar(10) default NULL,
  `NMot` varchar(10) default NULL,
  `TRazm` varchar(10) default NULL,
  `TMot` varchar(10) default NULL,
  `RRazm` varchar(10) default NULL,
  `RMot` varchar(10) default NULL,
  `NVlev` varchar(10) default NULL,
  `NVpr` varchar(10) default NULL,
  PRIMARY KEY  USING BTREE (`time`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;

--
-- Dumping data for table `mec_temp`
--

/*!40000 ALTER TABLE `mec_temp` DISABLE KEYS */;
/*!40000 ALTER TABLE `mec_temp` ENABLE KEYS */;


--
-- Definition of table `stan_1s_temp`
--

DROP TABLE IF EXISTS `stan_1s_temp`;
CREATE TABLE `stan_1s_temp` (
  `timeD` varchar(45) NOT NULL,
  `Uvvod1` varchar(10) default NULL,
  `Ivvod1` varchar(10) default NULL,
  `Wvvod1` varchar(10) default NULL,
  `WRvvod1` varchar(10) default NULL,
  `WQvvod1` varchar(10) default NULL,
  `Cosvvod1` varchar(10) default NULL,
  `gar5vvod1` varchar(10) default NULL,
  `gar7vvod1` varchar(10) default NULL,
  `gar11vvod1` varchar(10) default NULL,
  `gar13vvod1` varchar(10) default NULL,
  `Ur64` varchar(10) default NULL,
  `D21` varchar(10) default NULL,
  `D22` varchar(10) default NULL,
  `D23` varchar(10) default NULL,
  `D24` varchar(10) default NULL,
  `t1` varchar(10) default NULL,
  `t2` varchar(10) default NULL,
  `t3` varchar(10) default NULL,
  `t4` varchar(10) default NULL,
  `t5` varchar(10) default NULL,
  `t6` varchar(10) default NULL,
  `t7` varchar(10) default NULL,
  `t8` varchar(10) default NULL,
  `t9` varchar(10) default NULL,
  `t10` varchar(10) default NULL,
  `t11` varchar(10) default NULL,
  `t12` varchar(10) default NULL,
  `t13` varchar(10) default NULL,
  `t14` varchar(10) default NULL,
  `t15old` varchar(10) default NULL,
  `t16` varchar(10) default NULL,
  `t17` varchar(10) default NULL,
  `t18` varchar(10) default NULL,
  `t19` varchar(10) default NULL,
  `t20` varchar(10) default NULL,
  `t21` varchar(10) default NULL,
  `t22` varchar(10) default NULL,
  `t23` varchar(10) default NULL,
  `t24` varchar(10) default NULL,
  `t25` varchar(10) default NULL,
  `t26` varchar(10) default NULL,
  `t27` varchar(10) default NULL,
  `t28` varchar(10) default NULL,
  `t29` varchar(10) default NULL,
  `Voda` varchar(10) default NULL,
  PRIMARY KEY  (`timeD`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;

--
-- Dumping data for table `stan_1s_temp`
--

/*!40000 ALTER TABLE `stan_1s_temp` DISABLE KEYS */;
/*!40000 ALTER TABLE `stan_1s_temp` ENABLE KEYS */;


--
-- Definition of table `stan_mess_temp`
--

DROP TABLE IF EXISTS `stan_mess_temp`;
CREATE TABLE `stan_mess_temp` (
  `colorM` varchar(4) default NULL,
  `time` varchar(25) default NULL,
  `mess` varchar(100) default NULL,
  `Speed` varchar(10) default NULL
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;

--
-- Dumping data for table `stan_mess_temp`
--

/*!40000 ALTER TABLE `stan_mess_temp` DISABLE KEYS */;
INSERT INTO `stan_mess_temp` (`colorM`,`time`,`mess`,`Speed`) VALUES 
 ('1','ghddgdg','hfhfhf','5');
/*!40000 ALTER TABLE `stan_mess_temp` ENABLE KEYS */;


--
-- Definition of table `work_ds`
--

DROP TABLE IF EXISTS `work_ds`;
CREATE TABLE `work_ds` (
  `Time_Start` varchar(25) default NULL,
  `Time_Stop` varchar(25) default NULL,
  `B` float default NULL,
  `H` float default NULL,
  `Massa` float default NULL
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;

--
-- Dumping data for table `work_ds`
--
