procedure write_1s;stdcall;
var
 my_con_1s,My_conn_1s:pmysql;
 My_Query_1s,BaseName,BaseDel:String;
 FormDateTime,Time_Start,Time_Stop,Time_Sutki:ShortString;
 buffer_1s_work:buffer_array;
 buffer_1s_w:buffer_1s_type;
 TekTime,DaySignal:TDateTime;
 res:integer;
 res_con:integer;
 res_work:integer;
 str_error:string;

begin
     my_con_1s:=mysql_init(nil);
    { My_conn_1s:=mysql_real_connect(my_con_1s,PChar('192.168.0.111'),PChar('writer1s'),
              PChar('7777777'),PChar('basa'),3306,PChar('3306'),0);}
     My_conn_1s:=mysql_real_connect(my_con_1s,PChar('localhost'),PChar('writer1s'),
              PChar('7777777'),PChar('basa'),3306,PChar('3306'),0);
     mysql_select_db(my_con_1s,PChar('basa'));
     //Если подключение успешно то 0
     {
     if res_con<>0 then
      begin
        showmessage(inttostr(res_con));
      end
      else
      begin
        showmessage(inttostr(res_con));
     end;
     }
     while true do begin
        sleep(1000);
        critical_buffer_1s(buffer_1s_work,buffer_MEM);
        buffer_1s_w[0]:=buffer_1s_work[29];
        buffer_1s_w[1]:=buffer_1s_work[30];
        buffer_1s_w[2]:=buffer_1s_work[31];
        buffer_1s_w[3]:=buffer_1s_work[32];
        buffer_1s_w[4]:=buffer_1s_work[33];
        buffer_1s_w[5]:=buffer_1s_work[34];
        buffer_1s_w[6]:=buffer_1s_work[35];
        buffer_1s_w[7]:=buffer_1s_work[36];
        buffer_1s_w[8]:=buffer_1s_work[37];
        buffer_1s_w[9]:=buffer_1s_work[38];
        buffer_1s_w[10]:=Lo(buffer_1s_work[39]);
        buffer_1s_w[11]:=Hi(buffer_1s_work[39]);
        buffer_1s_w[12]:=Lo(buffer_1s_work[40]);
        buffer_1s_w[13]:=Hi(buffer_1s_work[40]);
        buffer_1s_w[14]:=Lo(buffer_1s_work[41]);
        buffer_1s_w[15]:=Hi(buffer_1s_work[41]);
        buffer_1s_w[16]:=Lo(buffer_1s_work[42]);
        buffer_1s_w[17]:=Hi(buffer_1s_work[42]);
        buffer_1s_w[18]:=Lo(buffer_1s_work[43]);
        buffer_1s_w[19]:=Hi(buffer_1s_work[43]);
        buffer_1s_w[20]:=Lo(buffer_1s_work[44]);
        buffer_1s_w[21]:=Hi(buffer_1s_work[44]);
        buffer_1s_w[22]:=Lo(buffer_1s_work[45]);
        buffer_1s_w[23]:=Hi(buffer_1s_work[45]);
        buffer_1s_w[24]:=Lo(buffer_1s_work[46]);
        buffer_1s_w[25]:=Hi(buffer_1s_work[46]);
        buffer_1s_w[26]:=Lo(buffer_1s_work[47]);
        buffer_1s_w[27]:=Hi(buffer_1s_work[47]);
        buffer_1s_w[28]:=Lo(buffer_1s_work[48]);
        buffer_1s_w[29]:=Hi(buffer_1s_work[48]);//
        buffer_1s_w[30]:=Lo(buffer_1s_work[49]);
        buffer_1s_w[31]:=Hi(buffer_1s_work[49]);
        buffer_1s_w[32]:=Lo(buffer_1s_work[50]);
        buffer_1s_w[33]:=Hi(buffer_1s_work[50]);
        buffer_1s_w[34]:=Lo(buffer_1s_work[51]);
        buffer_1s_w[35]:=Hi(buffer_1s_work[51]);
        buffer_1s_w[36]:=Lo(buffer_1s_work[52]);
        buffer_1s_w[37]:=Hi(buffer_1s_work[52]);
        buffer_1s_w[38]:=Lo(buffer_1s_work[53]);
        buffer_1s_w[39]:=Hi(buffer_1s_work[53]);
        buffer_1s_w[40]:=Lo(buffer_1s_work[54]);
        buffer_1s_w[41]:=Hi(buffer_1s_work[54]);
        buffer_1s_w[42]:=Lo(buffer_1s_work[55]);
        buffer_1s_w[43]:=Hi(buffer_1s_work[55]);
        buffer_1s_w[44]:=Lo(buffer_1s_work[56]);
        buffer_1s_w[45]:=Hi(buffer_1s_work[56]);

        TekTime:=Now;
        DaySignal:=TekTime-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,25,0,0);
         BaseName:='stan_1s_'+FormatDateTime('yyyy_mm_dd',(TekTime));
         FormDateTime:=FormatDateTime('yyyy-mm-dd hh:nn:ss.zzz',(TekTime));
         My_Query_1s:='insert into '+BaseName+'  values("'
         +FormDateTime+'","'+
         FloatToStr(buffer_1s_w[0]/100)+'","'+
         FloatToStr(buffer_1s_w[1]/10)+'","'+
         IntToStr(buffer_1s_w[2])+'","'+
         IntToStr(buffer_1s_w[3])+'","'+
         IntToStr(buffer_1s_w[4])+'","'+
         FloatToStr(buffer_1s_w[5]/100)+'","'+
         FloatToStr(buffer_1s_w[6]/100)+'","'+
         FloatToStr(buffer_1s_w[7]/100)+'","'+
         FloatToStr(buffer_1s_w[8]/100)+'","'+
         FloatToStr(buffer_1s_w[9]/100)+'","'+
         FloatToStr(buffer_1s_w[10]*10)+'","'+
         FloatToStr(buffer_1s_w[11]/10)+'","'+
         FloatToStr(buffer_1s_w[12]/10)+'","'+
         FloatToStr(buffer_1s_w[13]/10)+'","'+
         FloatToStr(buffer_1s_w[14]/10)+'","'+
         IntToStr(buffer_1s_w[15])+'","'+
         IntToStr(buffer_1s_w[16])+'","'+
         IntToStr(buffer_1s_w[17])+'","'+
         IntToStr(buffer_1s_w[18])+'","'+
         IntToStr(buffer_1s_w[19])+'","'+
         IntToStr(buffer_1s_w[20])+'","'+
         IntToStr(buffer_1s_w[21])+'","'+
         IntToStr(buffer_1s_w[22])+'","'+
         IntToStr(buffer_1s_w[23])+'","'+
         IntToStr(buffer_1s_w[24])+'","'+
         IntToStr(buffer_1s_w[25])+'","'+
         IntToStr(buffer_1s_w[26])+'","'+
         IntToStr(buffer_1s_w[27])+'","'+
         IntToStr(buffer_1s_w[28])+'","'+
         IntToStr(buffer_1s_w[29])+'","'+
         IntToStr(buffer_1s_w[30])+'","'+
         IntToStr(buffer_1s_w[31])+'","'+
         IntToStr(buffer_1s_w[32])+'","'+
         IntToStr(buffer_1s_w[33])+'","'+
         IntToStr(buffer_1s_w[34])+'","'+
         IntToStr(buffer_1s_w[35])+'","'+
         IntToStr(buffer_1s_w[36])+'","'+
         IntToStr(buffer_1s_w[37])+'","'+
         IntToStr(buffer_1s_w[38])+'","'+
         IntToStr(buffer_1s_w[39])+'","'+
         IntToStr(buffer_1s_w[40])+'","'+
         IntToStr(buffer_1s_w[41])+'","'+
         IntToStr(buffer_1s_w[42])+'","'+
         IntToStr(buffer_1s_w[43])+'","'+
         IntToStr(buffer_1s_w[44]*10)+'");';
        //SQL запрос на вставку
        //showmessage(My_Query_1s);
         res:=mysql_real_query(my_con_1s,PChar(My_Query_1s),
                          StrLen(PChar(My_Query_1s)));
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
             if mysql_errno(my_con_1s)=1146 then
             begin
                    My_Query_1s:='create table '+BaseName+' like stan_1s_temp';
                    mysql_real_query(my_con_1s,PChar(My_Query_1s),StrLen(PChar(My_Query_1s)));
             end
             else
             begin
               // writeLn(f,'mysql_real_connect-1s');
               // ShowMessage('Данные 1с не сохранены!');
                str_error:=FormDateTime+'-Данные в таблицу 1s не записаны, error='+IntToStr(mysql_errno(my_con_1s))+' Описание='+mysql_error(my_con_1s);
                writeLn(f,str_error);
                showmessage(str_error);
             end;

          end;


        {
         //----------------------------------------------------------------
        if res<>0 then
         begin
                writeLn(f,'mysql_real_connect-1s');
                ShowMessage('Данные 1с не сохранены!');
                str_error:=FormDateTime+'-Данные в таблицу сообщений(mess) не записаны, error='+IntToStr(mysql_errno(my_con_1s))+' Описание='+mysql_error(my_con_1s);
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
           My_Query_1s:='create table '+BaseName+' like stan_1s_temp';
           mysql_real_query(my_con_1s,PChar(My_Query_1s),
                          StrLen(PChar(My_Query_1s)));

        end;
        }


        BaseDel:='stan_1s_'+FormatDateTime('yyyy_mm_dd',(DaySignal));
// 2015-11-02 12:09 отключено удаление старой таблицы   1сек
//        My_Query_1s:='drop table if exists '+BaseDel;
//        mysql_real_query(my_con_1s,PChar(My_Query_1s),
//                          StrLen(PChar(My_Query_1s)));

        if (buffer_1s_work[9]>301) and (Work_Time_Start='') then
        begin
          Work_Time_Start:=FormDateTime;
        end;

        if (buffer_1s_work[9]<301) and (Work_Time_Start<>'') then
        begin

            //showmessage('create table 1s buffer_1s_work[9]<301 ');
            Work_Time_Stop:=FormDateTime;
            B:=buffer_1s_work[62];
            H:=buffer_1s_work[63]/100;
            Massa:=((3.141592*RMot_Pred*RMot_Pred*B/1000)-(0.09*B/1000))*7.85;
            My_Query_1s:='insert into work_ds  values("'
            +Work_Time_Start+'","'+Work_Time_Stop+'","'+
             IntToStr(B)+'","'+
//             FloatToStr(H)+'","'+
             FormatFloat('0.00',H)+'","'+
             Format('%6.2f',[Massa])+'");';
             res_work:=mysql_real_query(my_con_1s,PChar(My_Query_1s),StrLen(PChar(My_Query_1s)));

              if res_work<>0 then
              begin
                   // writeLn(f,'mysql_real_connect-1s');
                   // ShowMessage('Данные 1с не сохранены!');
                    str_error:=FormDateTime+'-Данные в таблицу work_ds не записаны, error='+IntToStr(mysql_errno(my_con_1s))+' Описание='+mysql_error(my_con_1s);
                    writeLn(f,str_error);
                    showmessage(str_error);
               end;

        end;
        if (Work_Time_Start<>'') and (Work_Time_Stop<>'') then
        begin
          Work_Time_Start:='';
          Work_Time_Stop:='';
          B:=0;
          H:=0;
          Massa:=0;
        end;
        RMot_Pred:=buffer_1s_work[9]/1000;
    end;
end;

