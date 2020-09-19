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
 