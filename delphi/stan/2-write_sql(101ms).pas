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
           FloatToStr(buffer_sql[0]/100)+','+
           FloatToStr(buffer_sql[1]/100)+','+
           FloatToStr(buffer_sql[2]/100)+','+
           FloatToStr(buffer_sql[3]/100)+','+
           FloatToStr(buffer_sql[4]/100)+','+
           FloatToStr(buffer_sql[5]/1000)+','+
           FloatToStr(buffer_sql[6]/1000)+','+
           IntToStr(buffer_sql[7])+','+
           FloatToStr(buffer_sql[8]/1000)+','+
           FloatToStr(buffer_sql[9]/1000)+','+
           FloatToStr(buffer_sql[10]/1000)+','+
           FloatToStr(buffer_sql[11]/1000)+','+
           IntToStr(buffer_sql[12])+','+
           IntToStr(buffer_sql[13])+','+
           IntToStr(buffer_sql[14])+','+
           IntToStr(buffer_sql[15])+','+
           IntToStr(buffer_sql[16])+','+
           FloatToStr(buffer_sql[17]/100)+','+
           FloatToStr(buffer_sql[18]/100)+','+
           FloatToStr(buffer_sql[19]/100)+','+
           FloatToStr(buffer_sql[20]/100)+','+
           FloatToStr(buffer_sql[21]/100)+','+
           FloatToStr(buffer_sql[22]/100)+','+
           FloatToStr(buffer_sql[23]/100)+','+
           FloatToStr(buffer_sql[24]/100)+','+
           FloatToStr(buffer_sql[25]/100)+','+
           FloatToStr(buffer_sql[26]/100)+','+
           FloatToStr(buffer_sql[27]/100)+','+
           FloatToStr(buffer_sql[28]/100)+','+
           FloatToStr(buffer_sql[29]/100)+','+
           FloatToStr(buffer_sql[30]/100)+','+
           FloatToStr(buffer_sql[34]/100)+','+
           FloatToStr(buffer_sql[35]/100)+','+
           FloatToStr(buffer_sql[36]/100)+','+
           FloatToStr(buffer_sql[37]/100)+','+
           FloatToStr(buffer_sql[38]/100)+','+
           FloatToStr(buffer_sql[39]/100)+','+
           FloatToStr(buffer_sql[40]/100)+','+
           FloatToStr(buffer_sql[41]/100)+','+
           FloatToStr(buffer_sql[42]/100)+','+
           FloatToStr(buffer_sql[43]/100)+','+
           FloatToStr(buffer_sql[44]/100)+','+
           FloatToStr(buffer_sql[45]/100)+','+
           FloatToStr(buffer_sql[46]/100)+','+
           FloatToStr(buffer_sql[47]/100)+','+
           FloatToStr(buffer_sql[48]/100)+','+
           FloatToStr(buffer_sql[49]/100)+','+
           FloatToStr(buffer_sql[50]/100)+','+
           FloatToStr(buffer_sql[56]/100)+','+
           FloatToStr(buffer_sql[57]/10)+','+
           FloatToStr(buffer_sql[58]/100)+','+
           FloatToStr(buffer_sql[59]/10)+','+
           FloatToStr(buffer_sql[60]/10)+','+
           FloatToStr(buffer_sql[61]/10)+','+
           FloatToStr(buffer_sql[62]/10)+','+
           FloatToStr(buffer_sql[63]/10)+','+
           FloatToStr(buffer_sql[64]/10)+','+
           FloatToStr(buffer_sql[65]/10)+','+
           FloatToStr(buffer_sql[66]/10)+','+
           FloatToStr(buffer_sql[67]/10)+','+
           FloatToStr(buffer_sql[68]/10)+','+
           FloatToStr(buffer_sql[69]/10)+','+
           IntToStr(buffer_sql[70])+','+
           IntToStr(buffer_sql[71])+','+
           FloatToStr(buffer_sql[72]/10)+','+
           FloatToStr(buffer_sql[73]/10)+','+
           FloatToStr(buffer_sql[74]/10)+','+
           FloatToStr(buffer_sql[75]/10)+','+
           FloatToStr(buffer_sql[76]/10)+','+
           FloatToStr(buffer_sql[77]/10)+','+
           FloatToStr(buffer_sql[78]/10)+','+
           FloatToStr(buffer_sql[79]/10)+','+
           FloatToStr(buffer_sql[80]/10)+','+
           FloatToStr(buffer_sql[81]/10)+','+
           IntToStr(buffer_sql[82])+','+
           IntToStr(buffer_sql[83])+','+
           IntToStr(buffer_sql[84])+','+
           IntToStr(buffer_sql[85])+','+
           IntToStr(buffer_sql[86])+','+
           IntToStr(buffer_sql[87])+','+
           IntToStr(buffer_sql[88])+','+
           IntToStr(buffer_sql[89])+','+
           IntToStr(buffer_sql[90])+','+
           FloatToStr(buffer_sql[96]/100)+','+
           FloatToStr(buffer_sql[97]/100)+','+
           FloatToStr(buffer_sql[98]/100)+','+
           FloatToStr(buffer_sql[99]/100)+','+
           FloatToStr(buffer_sql[100]/100)+','+
           FloatToStr(buffer_sql[101]/100)+','+
           FloatToStr(buffer_sql[102]/100)+','+
           FloatToStr(buffer_sql[103]/100)+','+
           FloatToStr(buffer_sql[104]/100)+','+
           FloatToStr(buffer_sql[105]/100)+','+
           FloatToStr(buffer_sql[31]/100)+','+
           FloatToStr(buffer_sql[32]/100)+','+
           IntToStr(buffer_sql[108])+','+
           IntToStr(buffer_sql[109])+','+
           IntToStr(buffer_sql[110])+')';

         // res:=mysql_real_query(my_con,PChar(My_Query_signal),StrLen(PChar(My_Query_signal)));

            //--------------------------------------------------------------------
            //Eсли результат запроса вернул ошибку то:
            //   - и Ошибка=1146 (Нет такой таблицы) нет такой таблицы то создаем таблицу
            //   - в противном случае выводим код ошибки на экран и записываем его в файл

         //   if res<>0 then
         //   begin
         //          if mysql_errno(my_con)=1146 then
         //          begin
         //              My_Query_signal:='create table '+BaseName+' like stan_100ms_temp';
         //              mysql_real_query(my_con,PChar(My_Query_signal), StrLen(PChar(My_Query_signal)));
         //          end
         //          else
         //          begin
         //             str_error:=FormDateTime+'-Данные в таблицу 100ms не записаны, error='+IntToStr(mysql_errno(my_con))+' Описание='+mysql_error(my_con);
         //             writeLn(f,str_error);
         //             showmessage(str_error);

         //          end;

         //   end;

        //  BaseDel:='stan_100ms_'+FormatDateTime('yyyy_mm_dd_hh',(DaySignal));
        //2015-11-02 06:44  отключил удаление таблицы 100мс        My_Query_signal:='drop table if exists '+BaseDel;
        //          mysql_real_query(my_con,PChar(My_Query_signal),StrLen(PChar(My_Query_signal)));
      end;
end;
