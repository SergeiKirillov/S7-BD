procedure write_sql;stdcall;
var
 my_con,My_conn,MyErr:pmysql;
 My_Query_signal:String;
 FormDateTime,Time_Start:ShortString;
 buffer_sql:buffer_array;
 res:integer;
 res_con:integer;
 DaySignal,TekTime:TDateTime;
 BaseName,BaseDel:String;
 str_error:string;
begin
       my_con:=mysql_init(nil);
      //My_conn:=mysql_real_connect(my_con,PChar('localhost'),PChar('root'),
      //          PChar('vb[fbk'),PChar('basa'),3306,PChar('3306'),0);
      My_conn:=mysql_real_connect(my_con,PChar('localhost'),PChar('who'),
                PChar('8888888'),PChar('basa'),3306,PChar('3306'),0);
      { My_conn:=mysql_real_connect(my_con,PChar('localhost'),PChar('root'),
                PChar('29062011s'),PChar('basa'),3306,PChar('3306'),0); }
       res_con:=mysql_select_db(my_con,PChar('basa'));
       //Если подключение успешно то 0




       res:=0;
       while true do begin
           sleep(80);
           TekTime:=Now;
           critical_buffer_plc(buffer_sql,buffer_MEM);
           DaySignal:=TekTime-EncodeTime(12,0,0,0)
           -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
           -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
           -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
           -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
           -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
           -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
           -EncodeTime(12,0,0,0)-EncodeTime(12,15,0,0);
           FormDateTime:=FormatDateTime('yyyy-mm-dd hh:nn:ss.zzz',(TekTime));
           BaseName:='mec_100ms_'+FormatDateTime('yyyy_mm_dd_hh',(TekTime));
           My_Query_signal:='insert '+BaseName+' values("'
           +FormDateTime+'","'						`time` varchar(25) NOT NULL default '',
           +floattostr(buffer_sql[0]/100)+'","'		0   		`VKlet` varchar(10) default NULL,           
           +floattostr(buffer_sql[1])+'","'                  2               `IzadR` varchar(10) default NULL,          
           +floattostr(buffer_sql[2])+'","'                  4                `IzadM` varchar(10) default NULL,         
           +floattostr(buffer_sql[3]/10)+'","'            6                `NKlet` varchar(10) default NULL,          
           +floattostr(buffer_sql[4]/10)+'","'            8                `NRazm` varchar(10) default NULL,       
           +floattostr(buffer_sql[5]/10)+'","'         10                 `NMot` varchar(10) default NULL,          
           +floattostr(buffer_sql[6]*10)+'","'         12                 `TRazm` varchar(10) default NULL,        
           +floattostr(buffer_sql[7]*10)+'","'         14                  `TMot` varchar(10) default NULL,           
           +floattostr(buffer_sql[8])+'","'               16                   `RRazm` varchar(10) default NULL,        
           +floattostr(buffer_sql[9])+'","'               18                   `RMot` varchar(10) default NULL,          
           +floattostr(buffer_sql[10]/100)+'","'     20                  `NVlev` varchar(10) default NULL,         
           +floattostr(buffer_sql[11]/100)+'","'     22                  `NVpr` varchar(10) default NULL,           
           +floattostr(buffer_sql[24]/100)+'","'     48                  `IvozM` varchar(10) default NULL,         
           +floattostr(buffer_sql[25]/1)+'","'         50                   `Imot` varchar(10) default NULL,            
           +floattostr(buffer_sql[26]/10)+'","'       52                  `Urazm` varchar(10) default NULL,        
           +floattostr(buffer_sql[27]/100)+'","'     54                  `IvozR` varchar(10) default NULL,          
           +floattostr(buffer_sql[28]/10)+'","'       56                  `Umot` varchar(10) default NULL,          
           +floattostr(buffer_sql[64]/1)+'","'       128                     `IRUZ4` varchar(10) default NULL,         
           +floattostr(buffer_sql[65]/1)+'","'       130                     `IRUZ5` varchar(10) default NULL,         
           +floattostr(buffer_sql[66]/1)+'","'       132                     `IMUZ4` varchar(10) default NULL,        
           +floattostr(buffer_sql[67]/1)+'","'       134                     `IMUZ5` varchar(10) default NULL,        
           +floattostr(buffer_sql[68]/100)+'","'   136                    `IzovK` varchar(10) default NULL,           
           +floattostr(buffer_sql[69]/10)+'","'     138                    `Ukl` varchar(10) default NULL,              
           +floattostr(buffer_sql[70]/1)+'","'       140                     `IKUZ4` varchar(10) default NULL,         
           +floattostr(buffer_sql[71]/1)+'","'       142                     `IKUZ5` varchar(10) default NULL,         
           +floattostr(buffer_sql[72]/100)+'","'   144                    `ObgTek` varchar(10) default NULL,      
           +floattostr(buffer_sql[73]/1)+'","'        146                    `DatObgDo` varchar(10) default NULL, 
           +floattostr(buffer_sql[74]/1)                 148                   `DatObgZa` varchar(10) default NULL,  
           +'")';
          //showmessage(My_Query_signal);
          // writeLn(f,My_Query_signal);

          res:=mysql_real_query(my_con,PChar(My_Query_signal),
                            StrLen(PChar(My_Query_signal)));
          //Если 0 то удачно выполнен запрос

          {
          if res<>0 then
          begin
           showmessage(inttostr(res));
          end
          else
          begin
           showmessage('Write');
          end;
          }

          //--------------------------------------------------------------------
          //Eсли результат запроса вернул ошибку то:
          //   - и Ошибка=1146 (Нет такой таблицы) нет такой таблицы то создаем таблицу
          //   - в противном случае выводим код ошибки на экран и записываем его в файл 

          if res<>0 then
          begin
             if mysql_errno(my_con)=1146 then
             begin
                 //    showmessage('create table mec_100ms');
                 My_Query_signal:='create table '+BaseName+' like mec_100ms_temp';
                 mysql_real_query(my_con,PChar(My_Query_signal),
                 StrLen(PChar(My_Query_signal)));
             end
             else
             begin
               // writeLn(f,'mysql_real_connect-100ms');
               // ShowMessage('Данные 100мс не сохранены!');
                str_error:=FormDateTime+'-Данные в таблицу 100ms не записаны, error='+IntToStr(mysql_errno(my_con))+' Описание='+mysql_error(my_con);
                writeLn(f,str_error);
                showmessage(str_error);
             end;  

          end;  


          {

              //----------------------------------------------------------------
              if res<>0 then
              begin
                writeLn(f,'mysql_real_connect-100ms');
                ShowMessage('Данные 100мс не сохранены!');
                str_error:=FormDateTime+'-Данные в таблицу сообщений(mess) не записаны, error='+IntToStr(mysql_errno(my_con))+' Описание='+mysql_error(my_con);
                writeLn(f,str_error);
                showmessage(str_error);

              end
              else
              begin
                //writeLn(f,'Connect on PLC');
                //ShowMessage('Активация соединение с контроллером успешное');
              end;
              //----------------------------------------------------------------



          if res<>0 then begin
              //    showmessage('create table mec_100ms');
                 My_Query_signal:='create table '+BaseName+' like mec_100ms_temp';
                 mysql_real_query(my_con,PChar(My_Query_signal),
                                StrLen(PChar(My_Query_signal)));
          end;
         }

          //showmessage(DateToStr(DaySignal));
          BaseDel:='mec_100ms_'+FormatDateTime('yyyy_mm_dd_hh',(DaySignal));
// 2015-11-02 12:09 отключено удаление старой таблицы 100мс
//        My_Query_signal:='drop table if exists '+BaseDel;
//          mysql_real_query(my_con,PChar(My_Query_signal),StrLen(PChar(My_Query_signal)));
      end;

end;

