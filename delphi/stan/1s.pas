procedure write_1s;stdcall;
var
 //my_con_1s,My_conn_1s:pmysql;
 My_Query_1s,BaseName,BaseDel:String;
 FormDateTime,Time_Start,Time_Stop,Time_Sutki:ShortString;
 buffer_1s_work:buffer_array;
 buffer_1s_w:buffer_1s_type;
 TekTime,DaySignal:TDateTime;
 res:integer;
 D1_pred,D2_pred,D3_pred,D4_pred,D5_pred:Integer;
 H5_Work,Ves_Work,Dlina_Work,D_tek_mot,D_pred_mot,B_Work:real;
 str_error:string;

begin
   //my_con_1s:=mysql_init(nil);
   //My_conn_1s:=mysql_real_connect(my_con_1s,PChar('localhost'),PChar('termo'),
   //         PChar('1234567890'),PChar('standb'),3306,PChar('3306'),0);
   //mysql_select_db(my_con_1s,PChar('standb'));
   while true do begin
      sleep(1000);
      critical_buffer_1s(buffer_1s_work,buffer_s400);
      buffer_1s_w[0]:=Hi(buffer_1s_work[112]);
      buffer_1s_w[1]:=Lo(buffer_1s_work[112]);
      buffer_1s_w[2]:=Hi(buffer_1s_work[113]);
      buffer_1s_w[3]:=Lo(buffer_1s_work[113]);
      buffer_1s_w[4]:=Hi(buffer_1s_work[114]);
      buffer_1s_w[5]:=Lo(buffer_1s_work[114]);
      buffer_1s_w[6]:=Hi(buffer_1s_work[115]);
      buffer_1s_w[7]:=Lo(buffer_1s_work[115]);
      buffer_1s_w[8]:=Hi(buffer_1s_work[116]);
      buffer_1s_w[9]:=Lo(buffer_1s_work[116]);
      buffer_1s_w[10]:=Hi(buffer_1s_work[117]);
      buffer_1s_w[11]:=Lo(buffer_1s_work[117]);
      buffer_1s_w[12]:=Hi(buffer_1s_work[118]);
      buffer_1s_w[13]:=Lo(buffer_1s_work[118]);
      buffer_1s_w[14]:=Hi(buffer_1s_work[119]);
      buffer_1s_w[15]:=Lo(buffer_1s_work[119]);
      buffer_1s_w[16]:=Hi(buffer_1s_work[120]);
      buffer_1s_w[17]:=Lo(buffer_1s_work[120]);
      buffer_1s_w[18]:=Hi(buffer_1s_work[121]);
      buffer_1s_w[19]:=Lo(buffer_1s_work[121]);
      buffer_1s_w[20]:=Hi(buffer_1s_work[122]);
      buffer_1s_w[21]:=Lo(buffer_1s_work[122]);
      buffer_1s_w[22]:=Hi(buffer_1s_work[123]);
      buffer_1s_w[23]:=Lo(buffer_1s_work[123]);
      buffer_1s_w[24]:=Hi(buffer_1s_work[124]);
      buffer_1s_w[25]:=Lo(buffer_1s_work[124]);
      buffer_1s_w[26]:=Hi(buffer_1s_work[125]);
      buffer_1s_w[27]:=Lo(buffer_1s_work[125]);
      buffer_1s_w[28]:=Hi(buffer_1s_work[126]);
      buffer_1s_w[29]:=Lo(buffer_1s_work[126]);
      buffer_1s_w[30]:=Hi(buffer_1s_work[127]);
      buffer_1s_w[31]:=Lo(buffer_1s_work[127]);
      buffer_1s_w[32]:=Hi(buffer_1s_work[128]);
      buffer_1s_w[33]:=Lo(buffer_1s_work[128]);
      buffer_1s_w[34]:=Hi(buffer_1s_work[129]);
      buffer_1s_w[35]:=Lo(buffer_1s_work[129]);
      buffer_1s_w[36]:=Hi(buffer_1s_work[130]);
      buffer_1s_w[37]:=Lo(buffer_1s_work[130]);
      buffer_1s_w[38]:=Hi(buffer_1s_work[131]);
      buffer_1s_w[39]:=Lo(buffer_1s_work[131]);
      buffer_1s_w[40]:=Hi(buffer_1s_work[132]);
      buffer_1s_w[41]:=Lo(buffer_1s_work[132]);
      buffer_1s_w[42]:=Hi(buffer_1s_work[133]);
      buffer_1s_w[43]:=Lo(buffer_1s_work[133]);
      buffer_1s_w[44]:=Hi(buffer_1s_work[134]);
      buffer_1s_w[45]:=Lo(buffer_1s_work[134]);
      buffer_1s_w[46]:=Hi(buffer_1s_work[135]);
      buffer_1s_w[47]:=Lo(buffer_1s_work[135]);
      buffer_1s_w[48]:=Hi(buffer_1s_work[136]);
      buffer_1s_w[49]:=Lo(buffer_1s_work[136]);
      buffer_1s_w[50]:=Hi(buffer_1s_work[137]);
      buffer_1s_w[51]:=Lo(buffer_1s_work[137]);
      buffer_1s_w[52]:=Hi(buffer_1s_work[138]);
      buffer_1s_w[53]:=Lo(buffer_1s_work[138]);
      buffer_1s_w[54]:=Hi(buffer_1s_work[139]);
      buffer_1s_w[55]:=Lo(buffer_1s_work[139]);
      buffer_1s_w[56]:=Hi(buffer_1s_work[140]);
      buffer_1s_w[57]:=Lo(buffer_1s_work[140]);
      buffer_1s_w[58]:=Hi(buffer_1s_work[141]);
      buffer_1s_w[59]:=Lo(buffer_1s_work[141]);
      buffer_1s_w[60]:=Hi(buffer_1s_work[142]);
      buffer_1s_w[61]:=Lo(buffer_1s_work[142]);
      buffer_1s_w[62]:=Hi(buffer_1s_work[143]);
      buffer_1s_w[63]:=Lo(buffer_1s_work[143]);
      buffer_1s_w[64]:=Hi(buffer_1s_work[144]);
      buffer_1s_w[65]:=Lo(buffer_1s_work[144]);
      buffer_1s_w[66]:=Hi(buffer_1s_work[145]);
      buffer_1s_w[67]:=Lo(buffer_1s_work[145]);
      buffer_1s_w[68]:=Hi(buffer_1s_work[146]);
      buffer_1s_w[69]:=Lo(buffer_1s_work[146]);
      buffer_1s_w[70]:=Hi(buffer_1s_work[147]);
      buffer_1s_w[71]:=Lo(buffer_1s_work[147]);
      buffer_1s_w[72]:=Hi(buffer_1s_work[148]);
      buffer_1s_w[73]:=Lo(buffer_1s_work[148]);
      buffer_1s_w[74]:=Lo(buffer_1s_work[149]);
      buffer_1s_w[75]:=Hi(buffer_1s_work[149]);
      buffer_1s_w[76]:=Lo(buffer_1s_work[150]);
      buffer_1s_w[77]:=Hi(buffer_1s_work[150]);
      buffer_1s_w[78]:=Lo(buffer_1s_work[151]);
      buffer_1s_w[79]:=Hi(buffer_1s_work[151]);
      buffer_1s_w[80]:=Lo(buffer_1s_work[152]);
      buffer_1s_w[81]:=Hi(buffer_1s_work[152]);
      buffer_1s_w[82]:=Lo(buffer_1s_work[153]);
      buffer_1s_w[83]:=Hi(buffer_1s_work[153]);
      buffer_1s_w[84]:=Hi(buffer_1s_work[154]);
      TekTime:=Now;
      DaySignal:=TekTime-EncodeTime(12,0,0,0)
       -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
       -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
       -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
       -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
       -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
       -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
       -EncodeTime(12,25,0,0);
       BaseName:='stan_1s_'+FormatDateTime('yyyy_mm_dd',(TekTime));
       FormDateTime:=FormatDateTime('yyyy-mm-dd hh:nn:ss.zzz',(TekTime));
       My_Query_1s:='insert into '+BaseName+' values'
       +'('''+FormDateTime+''','+
       IntToStr(buffer_1s_w[0])+','+
       IntToStr(buffer_1s_w[1])+','+
       IntToStr(buffer_1s_w[2])+','+
       IntToStr(buffer_1s_w[3])+','+
       IntToStr(buffer_1s_w[4])+','+
       IntToStr(buffer_1s_w[5])+','+
       IntToStr(buffer_1s_w[6])+','+
       IntToStr(buffer_1s_w[7])+','+
       IntToStr(buffer_1s_w[8])+','+
       IntToStr(buffer_1s_w[9])+','+
       IntToStr(buffer_1s_w[10])+','+
       IntToStr(buffer_1s_w[11])+','+
       IntToStr(buffer_1s_w[12])+','+
       IntToStr(buffer_1s_w[13])+','+
       IntToStr(buffer_1s_w[14])+','+
       IntToStr(buffer_1s_w[15])+','+
       IntToStr(buffer_1s_w[16])+','+
       IntToStr(buffer_1s_w[17])+','+
       IntToStr(buffer_1s_w[18])+','+
       IntToStr(buffer_1s_w[19])+','+
       IntToStr(buffer_1s_w[20])+','+
       IntToStr(buffer_1s_w[21])+','+
       IntToStr(buffer_1s_w[22])+','+
       IntToStr(buffer_1s_w[23])+','+
       IntToStr(buffer_1s_w[24])+','+
       IntToStr(buffer_1s_w[25])+','+
       IntToStr(buffer_1s_w[26])+','+
       IntToStr(buffer_1s_w[27])+','+
       IntToStr(buffer_1s_w[28])+','+
       IntToStr(buffer_1s_w[29])+','+
       IntToStr(buffer_1s_w[30])+','+
       IntToStr(buffer_1s_w[31])+','+
       IntToStr(buffer_1s_w[32])+','+
       IntToStr(buffer_1s_w[33])+','+
       IntToStr(buffer_1s_w[34])+','+
       IntToStr(buffer_1s_w[35])+','+
       IntToStr(buffer_1s_w[36])+','+
       IntToStr(buffer_1s_w[37])+','+
       IntToStr(buffer_1s_w[38])+','+
       IntToStr(buffer_1s_w[39])+','+
       IntToStr(buffer_1s_w[40])+','+
       IntToStr(buffer_1s_w[41])+','+
       IntToStr(buffer_1s_w[42])+','+
       IntToStr(buffer_1s_w[43])+','+
       IntToStr(buffer_1s_w[44])+','+
       IntToStr(buffer_1s_w[45])+','+
       IntToStr(buffer_1s_w[46])+','+
       IntToStr(buffer_1s_w[47])+','+
       IntToStr(buffer_1s_w[48])+','+
       IntToStr(buffer_1s_w[49])+','+
       IntToStr(buffer_1s_w[50])+','+
       IntToStr(buffer_1s_w[51])+','+
       IntToStr(buffer_1s_w[52])+','+
       IntToStr(buffer_1s_w[53])+','+
       IntToStr(buffer_1s_w[54])+','+
       IntToStr(buffer_1s_w[55])+','+
       IntToStr(buffer_1s_w[56])+','+
       IntToStr(buffer_1s_w[57])+','+
       IntToStr(buffer_1s_w[58])+','+
       IntToStr(buffer_1s_w[59])+','+
       IntToStr(buffer_1s_w[60])+','+
       IntToStr(buffer_1s_w[61])+','+
       IntToStr(buffer_1s_w[62])+','+
       IntToStr(buffer_1s_w[63])+','+
       IntToStr(buffer_1s_w[64])+','+
       IntToStr(buffer_1s_w[65])+','+
       IntToStr(buffer_1s_w[66])+','+
       IntToStr(buffer_1s_w[67])+','+
       IntToStr(buffer_1s_w[68])+','+
       IntToStr(buffer_1s_w[69])+','+
       IntToStr(buffer_1s_w[70])+','+
       IntToStr(buffer_1s_w[71])+','+
       IntToStr(buffer_1s_w[72])+','+
       IntToStr(buffer_1s_w[73])+','+
       FloatToStr(buffer_1s_w[74]/10)+','+
       FloatToStr(buffer_1s_w[75]/10)+','+
       FloatToStr(buffer_1s_w[76]/10)+','+
       FloatToStr(buffer_1s_w[77]/10)+','+
       FloatToStr(buffer_1s_w[78]/10)+','+
       FloatToStr(buffer_1s_w[79]/10)+','+
       FloatToStr(buffer_1s_w[80]/10)+','+
       FloatToStr(buffer_1s_w[81]/10)+','+
       FloatToStr(buffer_1s_w[82]/10)+','+
       FloatToStr(buffer_1s_w[83]*10)+','+
       FloatToStr(buffer_1s_w[84]*10)+');';
       end;
      //BaseDel:='stan_1s_'+FormatDateTime('yyyy_mm_dd',(DaySignal));
//2015-11-02 06:42 ioee??ee oaaeaiea oaaeeou 1 nae      My_Query_1s:='drop table if exists '+BaseDel;
//      mysql_real_query(my_con_1s,PChar(My_Query_1s),StrLen(PChar(My_Query_1s)));

      if D1_pred=0 then  D1_pred:=buffer_1s_work[12];
      if D2_pred=0 then  D2_pred:=buffer_1s_work[13];
      if D3_pred=0 then  D3_pred:=buffer_1s_work[14];
      if D4_pred=0 then  D4_pred:=buffer_1s_work[15];
      if D5_pred=0 then  D5_pred:=buffer_1s_work[16];

      if (D1_pred<>buffer_1s_work[12]) then
         begin
               My_Query_1s:='insert perevalki values'
               +'('''+FormDateTime+''','+
               IntToStr(buffer_1s_work[12])+','+
               IntToStr(0)+','+
               IntToStr(0)+','+
               IntToStr(0)+','+
               IntToStr(0)+');';
                //res:=mysql_real_query(my_con_1s,PChar(My_Query_1s),StrLen(PChar(My_Query_1s)));
                        //--------------------------------------------------------------------
                //        if res<>0 then
                //        begin
                //           str_error:=FormDateTime+'-Aaiiua a oaaeeoo Perevalki ia caienaiu(buffer_1s_work[12]), error='+IntToStr(mysql_errno(my_con_1s))+' Iienaiea='+mysql_error(my_con_1s);
                //           writeLn(f,str_error);
                //           showmessage(str_error);
                //        end;
         end;
      if (D2_pred<>buffer_1s_work[13]) then
         begin
               My_Query_1s:='insert perevalki values'
               +'('''+FormDateTime+''','+
               IntToStr(0)+','+
               IntToStr(buffer_1s_work[13])+','+
               IntToStr(0)+','+
               IntToStr(0)+','+
               IntToStr(0)+');';
             //  res:=mysql_real_query(my_con_1s,PChar(My_Query_1s),StrLen(PChar(My_Query_1s)));
               //--------------------------------------------------------------------
             //           if res<>0 then
             //           begin
             //              str_error:=FormDateTime+'-Aaiiua a oaaeeoo Perevalki ia caienaiu(buffer_1s_work[13]), error='+IntToStr(mysql_errno(my_con_1s))+' Iienaiea='+mysql_error(my_con_1s);
             //              writeLn(f,str_error);
             //              showmessage(str_error);
             //           end;

         end;
      if (D3_pred<>buffer_1s_work[14]) then
         begin
               My_Query_1s:='insert perevalki values'
               +'('''+FormDateTime+''','+
               IntToStr(0)+','+
               IntToStr(0)+','+
               IntToStr(buffer_1s_work[14])+','+
               IntToStr(0)+','+
               IntToStr(0)+');';
           //     res:=mysql_real_query(my_con_1s,PChar(My_Query_1s),StrLen(PChar(My_Query_1s)));
                //--------------------------------------------------------------------
           //             if res<>0 then
           //             begin
           //                str_error:=FormDateTime+'-Aaiiua a oaaeeoo Perevalki ia caienaiu(buffer_1s_work[14]), error='+IntToStr(mysql_errno(my_con_1s))+' Iienaiea='+mysql_error(my_con_1s);
           //                writeLn(f,str_error);
           //                showmessage(str_error);
           //             end;

         end;
      if (D4_pred<>buffer_1s_work[15]) then
         begin
               My_Query_1s:='insert perevalki values'
               +'('''+FormDateTime+''','+
               IntToStr(0)+','+
               IntToStr(0)+','+
               IntToStr(0)+','+
               IntToStr(buffer_1s_work[15])+','+
               IntToStr(0)+');';
           //    res:=mysql_real_query(my_con_1s,PChar(My_Query_1s),StrLen(PChar(My_Query_1s)));
               //--------------------------------------------------------------------
           //             if res<>0 then
           //             begin
           //                str_error:=FormDateTime+'-Aaiiua a oaaeeoo Perevalki ia caienaiu(buffer_1s_work[15]), error='+IntToStr(mysql_errno(my_con_1s))+' Iienaiea='+mysql_error(my_con_1s);
           //                writeLn(f,str_error);
           //                showmessage(str_error);
           //             end;

         end;
      if (D5_pred<>buffer_1s_work[16]) then
         begin
             My_Query_1s:='insert perevalki values'
             +'('''+FormDateTime+''','+
             IntToStr(0)+','+
             IntToStr(0)+','+
             IntToStr(0)+','+
             IntToStr(0)+','+
             IntToStr(buffer_1s_work[16])+');';
            // res:=mysql_real_query(my_con_1s,PChar(My_Query_1s),StrLen(PChar(My_Query_1s)));
            //--------------------------------------------------------------------
            //            if res<>0 then
            //            begin
            //               str_error:=FormDateTime+'-Aaiiua a oaaeeoo Perevalki ia caienaiu(buffer_1s_work[16]), error='+IntToStr(mysql_errno(my_con_1s))+' Iienaiea='+mysql_error(my_con_1s);
            //               writeLn(f,str_error);
            //               showmessage(str_error);
            //            end;
         end;

      D1_pred:=buffer_1s_work[12];
      D2_pred:=buffer_1s_work[13];
      D3_pred:=buffer_1s_work[14];
      D4_pred:=buffer_1s_work[15];
      D5_pred:=buffer_1s_work[16];
      D_tek_mot:=buffer_1s_work[10];

      if (D_tek_mot>D_pred_mot) then
      begin
           if (D_pred_mot<615) then Time_Start:=FormDateTime;
      end;
      if ((Time_Start<>'') and (H5_Work=0) and (D_tek_mot>700) and ((buffer_1s_work[3]/100)>2)) then
      begin
           H5_Work:=buffer_1s_work[6]/1000;
           B_Work:=buffer_1s_work[7];
      end;

      if ((Time_Start<>'') and (H5_Work<>0) and (D_tek_mot<610) and (D_tek_mot<D_pred_mot)) then
        begin
          Ves_Work:=((((((D_pred_mot*D_pred_mot)/1000000)-0.36)*3.141593)/4)*(B_Work/1000))*7.85;
          Time_Stop:=FormDateTime;
          Dlina_Work:=((Ves_Work/7.85)/(B_Work/1000))/(H5_Work/1000);
          My_Query_1s:='insert into work_stan values ('''+
                      Time_Start+''','+''''+Time_Stop+''','+
                      FloatToStr(H5_Work)+','+FloatToStr(B_Work)+','+
                      FloatToStr(Ves_Work)+','+FloatToStr(Dlina_Work)+');';
         // res:=mysql_real_query(my_con_1s,PChar(My_Query_1s),StrLen(PChar(My_Query_1s)));
         //--------------------------------------------------------------------
         //               if res<>0 then
         //               begin
         //                  str_error:=FormDateTime+'-Aaiiua a oaaeeoo work_stan ia caienaiu, error='+IntToStr(mysql_errno(my_con_1s))+' Iienaiea='+mysql_error(my_con_1s);
         //                  writeLn(f,str_error);
         //                  showmessage(str_error);
         //               end;

          Time_Start:='';
          Time_Stop:='';
          H5_Work:=0;
          B_Work:=0;
          Ves_Work:=0;
          Dlina_Work:=0;
      end;
      D_pred_mot:=D_tek_mot;

       Time_Sutki:=FormatDateTime('yyyy-mm-dd',(TekTime-EncodeTime(12,0,0,0)
                                                     -EncodeTime(13,0,0,0)));
       My_Query_1s:='insert into raz_sutki values'
       +'('''+Time_Sutki+''','+
       FloatToStr(buffer_1s_work[155])+');';
       // res:=mysql_real_query(my_con_1s,PChar(My_Query_1s),StrLen(PChar(My_Query_1s)));
       //--------------------------------------------------------------------
       //                 if res<>0 then
       //                 begin
       //                    str_error:=FormDateTime+'-Aaiiua a oaaeeoo raz_sutki ia caienaiu, error='+IntToStr(mysql_errno(my_con_1s))+' Iienaiea='+mysql_error(my_con_1s);
       //                    writeLn(f,str_error);
       //                    showmessage(str_error);
       //                 end;

  end;
//end;