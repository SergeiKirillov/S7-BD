- Дамп структуры для таблица standb.stan_100ms_temp
CREATE TABLE IF NOT EXISTS `stan_100ms_temp` (
  `vremia` varchar(25) NOT NULL default '',
  `v1` float NOT NULL default '0',
  `v2` float NOT NULL default '0',
  `v3` float NOT NULL default '0',
  `v4` float NOT NULL default '0',
  `v5` float NOT NULL default '0',
  `h1` float NOT NULL default '0',
  `h5` float NOT NULL default '0',
  `b` smallint(6) NOT NULL default '0',
  `dvip` float NOT NULL default '0',
  `drazm` float NOT NULL default '0',
  `dmot` float NOT NULL default '0',
  `vvip` float NOT NULL default '0',
  `d1` smallint(6) NOT NULL default '0',
  `d2` smallint(6) NOT NULL default '0',
  `d3` smallint(6) NOT NULL default '0',
  `d4` smallint(6) NOT NULL default '0',
  `d5` smallint(6) NOT NULL default '0',
  `e2` float NOT NULL default '0',
  `e3` float NOT NULL default '0',
  `e4` float NOT NULL default '0',
  `e5` float NOT NULL default '0',
  `n1l` float NOT NULL default '0',
  `n1p` float NOT NULL default '0',
  `n2l` float NOT NULL default '0',
  `n2p` float NOT NULL default '0',
  `n3l` float NOT NULL default '0',
  `n3p` float NOT NULL default '0',
  `n4l` float NOT NULL default '0',
  `n4p` float NOT NULL default '0',
  `n5l` float NOT NULL default '0',
  `n5p` float NOT NULL default '0',
  `rezerv1` float NOT NULL default '0',
  `rezerv2` float NOT NULL default '0',
  `t1` float NOT NULL default '0',
  `t2` float NOT NULL default '0',
  `t3` float NOT NULL default '0',
  `t4` float NOT NULL default '0',
  `t1l` float NOT NULL default '0',
  `t2l` float NOT NULL default '0',
  `t3l` float NOT NULL default '0',
  `t4l` float NOT NULL default '0',
  `t1p` float NOT NULL default '0',
  `t2p` float NOT NULL default '0',
  `t3p` float NOT NULL default '0',
  `t4p` float NOT NULL default '0',
  `t1z` float NOT NULL default '0',
  `t2z` float NOT NULL default '0',
  `t3z` float NOT NULL default '0',
  `t4z` float NOT NULL default '0',
  `erazm` float NOT NULL default '0',
  `ivozbrazm` float NOT NULL default '0',
  `izadrazm` float NOT NULL default '0',
  `w1` float NOT NULL default '0',
  `w2v` float NOT NULL default '0',
  `w2n` float NOT NULL default '0',
  `w3v` float NOT NULL default '0',
  `w3n` float NOT NULL default '0',
  `w4v` float NOT NULL default '0',
  `w4n` float NOT NULL default '0',
  `w5v` float NOT NULL default '0',
  `w5n` float NOT NULL default '0',
  `wmot` float NOT NULL default '0',
  `imot` smallint(6) NOT NULL default '0',
  `izadmot` smallint(6) NOT NULL default '0',
  `u1` float NOT NULL default '0',
  `u2v` float NOT NULL default '0',
  `u2n` float NOT NULL default '0',
  `u3v` float NOT NULL default '0',
  `u3n` float NOT NULL default '0',
  `u4v` float NOT NULL default '0',
  `u4n` float NOT NULL default '0',
  `u5v` float NOT NULL default '0',
  `u5n` float NOT NULL default '0',
  `umot` float NOT NULL default '0',
  `i1` smallint(6) NOT NULL default '0',
  `i2v` smallint(6) NOT NULL default '0',
  `i2n` smallint(6) NOT NULL default '0',
  `i3v` smallint(6) NOT NULL default '0',
  `i3n` smallint(6) NOT NULL default '0',
  `i4v` smallint(6) NOT NULL default '0',
  `i4n` smallint(6) NOT NULL default '0',
  `i5v` smallint(6) NOT NULL default '0',
  `i5n` smallint(6) NOT NULL default '0',
  `rtv` float NOT NULL default '0',
  `dt1` float NOT NULL default '0',
  `dt2` float NOT NULL default '0',
  `dt3` float NOT NULL default '0',
  `dt4` float NOT NULL default '0',
  `grt` float NOT NULL default '0',
  `trt` float NOT NULL default '0',
  `rnv1` float NOT NULL default '0',
  `rnv2` float NOT NULL default '0',
  `rnv3` float NOT NULL default '0',
  `dh1` float NOT NULL default '0',
  `dh5` float NOT NULL default '0',
  `109` smallint(6) NOT NULL default '0',
  `110` smallint(6) NOT NULL default '0',
  `111` smallint(6) NOT NULL default '0',
  PRIMARY KEY  (`vremia`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251 PACK_KEYS=0 ROW_FORMAT=FIXED;








procedure write_sql;stdcall;
var
 //my_con,My_conn,MyErr:pmysql;
 My_Query_signal,BaseName,BaseDel:String;
 FormDateTime,Time_Start:ShortString;
 buffer_sql:buffer_array;
 res:integer;
 DaySignal,TekTime:TDateTime;
 str_error:string;
begin
      // my_con:=mysql_init(nil);
      // My_conn:=mysql_real_connect(my_con,PChar('localhost'),PChar('termo'),
      //          PChar('1234567890'),PChar('standb'),3306,PChar('3306'),0);
      // mysql_select_db(my_con,PChar('standb'));
      // res:=0;
       while true do begin
           sleep(101);
           TekTime:=Now;
           critical_buffer_plc(buffer_sql,buffer_s400);
           DaySignal:=TekTime-EncodeTime(12,0,0,0)
           -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
           -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
           -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
           -EncodeTime(12,15,0,0);
           BaseName:='stan_100ms_'+FormatDateTime('yyyy_mm_dd_hh',(TekTime));
           FormDateTime:=FormatDateTime('yyyy-mm-dd hh:nn:ss.zzz',(TekTime));
           My_Query_signal:='insert '+BaseName+' values'
           +'('''+FormDateTime+''','+
           FloatToStr(buffer_sql[0]/100)+','+ 	v1
           FloatToStr(buffer_sql[1]/100)+','+		v2
           FloatToStr(buffer_sql[2]/100)+','+		v3
           FloatToStr(buffer_sql[3]/100)+','+		v4
           FloatToStr(buffer_sql[4]/100)+','+		v5
           FloatToStr(buffer_sql[5]/1000)+','+	h1
           FloatToStr(buffer_sql[6]/1000)+','+	h5
           IntToStr(buffer_sql[7])+','+			b
           FloatToStr(buffer_sql[8]/1000)+','+	dvip
           FloatToStr(buffer_sql[9]/1000)+','+	drazm
           FloatToStr(buffer_sql[10]/1000)+','+	dmot
           FloatToStr(buffer_sql[11]/1000)+','+	vvip
           IntToStr(buffer_sql[12])+','+			d1
           IntToStr(buffer_sql[13])+','+			d2
           IntToStr(buffer_sql[14])+','+			d3
           IntToStr(buffer_sql[15])+','+			d4
           IntToStr(buffer_sql[16])+','+			d5
           FloatToStr(buffer_sql[17]/100)+','+	e2
           FloatToStr(buffer_sql[18]/100)+','+	e3
           FloatToStr(buffer_sql[19]/100)+','+	e4
           FloatToStr(buffer_sql[20]/100)+','+	e5
           FloatToStr(buffer_sql[21]/100)+','+	n1l
           FloatToStr(buffer_sql[22]/100)+','+	n1p
           FloatToStr(buffer_sql[23]/100)+','+	n2l
           FloatToStr(buffer_sql[24]/100)+','+	n2p
           FloatToStr(buffer_sql[25]/100)+','+	n3l
           FloatToStr(buffer_sql[26]/100)+','+	n3p
           FloatToStr(buffer_sql[27]/100)+','+	n4l
           FloatToStr(buffer_sql[28]/100)+','+	n4p
           FloatToStr(buffer_sql[29]/100)+','+	n5l
           FloatToStr(buffer_sql[30]/100)+','+	n5p
           FloatToStr(buffer_sql[34]/100)+','+	reserv1
           FloatToStr(buffer_sql[35]/100)+','+	reserv2
           FloatToStr(buffer_sql[36]/100)+','+	t1
           FloatToStr(buffer_sql[37]/100)+','+	t2
           FloatToStr(buffer_sql[38]/100)+','+	t3
           FloatToStr(buffer_sql[39]/100)+','+	t4
           FloatToStr(buffer_sql[40]/100)+','+	t1l
           FloatToStr(buffer_sql[41]/100)+','+	t2l
           FloatToStr(buffer_sql[42]/100)+','+	t3l
           FloatToStr(buffer_sql[43]/100)+','+	t4l	
           FloatToStr(buffer_sql[44]/100)+','+	t1p
           FloatToStr(buffer_sql[45]/100)+','+	t2p
           FloatToStr(buffer_sql[46]/100)+','+	t3p
           FloatToStr(buffer_sql[47]/100)+','+	t4p	
           FloatToStr(buffer_sql[48]/100)+','+	t1z
           FloatToStr(buffer_sql[49]/100)+','+	t2z
           FloatToStr(buffer_sql[50]/100)+','+	t3z
           FloatToStr(buffer_sql[56]/100)+','+	t4z
           FloatToStr(buffer_sql[57]/10)+','+		erazm
           FloatToStr(buffer_sql[58]/100)+','+	ivozbrazm
           FloatToStr(buffer_sql[59]/10)+','+		izadrazm w1
           FloatToStr(buffer_sql[60]/10)+','+		w1
           FloatToStr(buffer_sql[61]/10)+','+		w2v
           FloatToStr(buffer_sql[62]/10)+','+		w2n
           FloatToStr(buffer_sql[63]/10)+','+		w3v
           FloatToStr(buffer_sql[64]/10)+','+		w3n
           FloatToStr(buffer_sql[65]/10)+','+		w4v
           FloatToStr(buffer_sql[66]/10)+','+		w4n
           FloatToStr(buffer_sql[67]/10)+','+		w5v
           FloatToStr(buffer_sql[68]/10)+','+		w5n
           FloatToStr(buffer_sql[69]/10)+','+		wmot
           IntToStr(buffer_sql[70])+','+			imot
           IntToStr(buffer_sql[71])+','+			izadmot
           FloatToStr(buffer_sql[72]/10)+','+		u1
           FloatToStr(buffer_sql[73]/10)+','+		u2v
           FloatToStr(buffer_sql[74]/10)+','+		u2n
           FloatToStr(buffer_sql[75]/10)+','+		u3v
           FloatToStr(buffer_sql[76]/10)+','+		u3n
           FloatToStr(buffer_sql[77]/10)+','+		u4v
           FloatToStr(buffer_sql[78]/10)+','+		u4n
           FloatToStr(buffer_sql[79]/10)+','+		u5v
           FloatToStr(buffer_sql[80]/10)+','+		u5n
           FloatToStr(buffer_sql[81]/10)+','+		umot
           IntToStr(buffer_sql[82])+','+			,i1
           IntToStr(buffer_sql[83])+','+			i2v
           IntToStr(buffer_sql[84])+','+			i2n
           IntToStr(buffer_sql[85])+','+			i3v
           IntToStr(buffer_sql[86])+','+			i3n
           IntToStr(buffer_sql[87])+','+			i4v
           IntToStr(buffer_sql[88])+','+			i4n
           IntToStr(buffer_sql[89])+','+			i5v
           IntToStr(buffer_sql[90])+','+			i5n
           FloatToStr(buffer_sql[96]/100)+','+	rtv
           FloatToStr(buffer_sql[97]/100)+','+	dt1
           FloatToStr(buffer_sql[98]/100)+','+	dt2
           FloatToStr(buffer_sql[99]/100)+','+	dt3
           FloatToStr(buffer_sql[100]/100)+','+	dt4
           FloatToStr(buffer_sql[101]/100)+','+	grt
           FloatToStr(buffer_sql[102]/100)+','+	trt
           FloatToStr(buffer_sql[103]/100)+','+	mv1
           FloatToStr(buffer_sql[104]/100)+','+	mv2
           FloatToStr(buffer_sql[105]/100)+','+	mv3
           FloatToStr(buffer_sql[31]/100)+','+	dh1
           FloatToStr(buffer_sql[32]/100)+','+	dh5
           IntToStr(buffer_sql[108])+','+		os1klvb
           IntToStr(buffer_sql[109])+','+		rezerv
           IntToStr(buffer_sql[110])+')';			mezdoza4

	


         // res:=mysql_real_query(my_con,PChar(My_Query_signal),StrLen(PChar(My_Query_signal)));

            //--------------------------------------------------------------------
            //Enee ?acoeuoao cai?ina aa?ioe ioeaeo oi:
            //   - e Ioeaea=1146 (Iao oaeie oaaeeou) iao oaeie oaaeeou oi nicaaai oaaeeoo
            //   - a i?ioeaiii neo?aa auaiaei eia ioeaee ia ye?ai e caienuaaai aai a oaee

         //   if res<>0 then
         //   begin
         //          if mysql_errno(my_con)=1146 then
         //          begin
         //              My_Query_signal:='create table '+BaseName+' like stan_100ms_temp';
         //              mysql_real_query(my_con,PChar(My_Query_signal), StrLen(PChar(My_Query_signal)));
         //          end
         //          else
         //          begin
         //             str_error:=FormDateTime+'-Aaiiua a oaaeeoo 100ms ia caienaiu, error='+IntToStr(mysql_errno(my_con))+' Iienaiea='+mysql_error(my_con);
         //             writeLn(f,str_error);
         //             showmessage(str_error);

         //          end;

         //   end;

        //  BaseDel:='stan_100ms_'+FormatDateTime('yyyy_mm_dd_hh',(DaySignal));
        //2015-11-02 06:44  ioee??ee oaaeaiea oaaeeou 100in        My_Query_signal:='drop table if exists '+BaseDel;
        //          mysql_real_query(my_con,PChar(My_Query_signal),StrLen(PChar(My_Query_signal)));
      end;
end;
