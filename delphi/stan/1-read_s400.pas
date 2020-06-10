procedure read_S400;stdcall;
var
 resD:LongInt;
 plc_adr_table : array [0..10] of Byte;
 name1:array [0..10] of Char;
 buffer_19:buffer_array;
 Con_Table:Con_Table_Type;
 amount: Integer;
 Byte_Col,Byte_Col_r:DWORD;
 ConTableLen:byte;
 buffer_mm:buffer_array_mess;
 buffer_1s_w:buffer_1s_type;
 ProdaveConnect:integer;
 ProdaveConnectActive:integer;
 str_error:string;
 FormDateTime:ShortString;


begin
    resD:=5;
    Byte_Col:=SizeOf(buffer_19);
    while true do begin
        sleep(100);
        if resD<>0 then begin
            FormDateTime:=FormatDateTime('yyyy-mm-dd hh:nn:ss.zzz',(Now));
            StrCopy(name1,'s7online');
            TCon_Table_Type.IP_Addr[0]:=192;
            TCon_Table_Type.IP_Addr[1]:=168;
            TCon_Table_Type.IP_Addr[2]:=0;
            TCon_Table_Type.IP_Addr[3]:=11;
            TCon_Table_Type.IP_Addr[4]:=0;
            TCon_Table_Type.IP_Addr[5]:=0;
            TCon_Table_Type.AdrType:=3;
            TCon_Table_Type.SlotNr:=3;
            TCon_Table_Type.RackNr:=0;
            ConTableLen:=SizeOf(TCon_Table_Type);
            ProdaveConnect:=LoadConnection_ex6 ( 1, addr(name1),ConTableLen,addr(TCon_Table_Type));
            //----------------------------------------------------------------
              if ProdaveConnect<>0 then
              begin
                str_error:=(FormDateTime+'-Ошибка связи с контроллером(LoadConnection_ex6');
                writeLn(f,str_error);
                ShowMessage(str_error);
              end
              else
              begin
                //writeLn(f,'Connect on PLC');
                //ShowMessage('Соединение с контроллером успешное');
              end;
              //----------------------------------------------------------------

            ProdaveConnectActive:=SetActiveConnection_ex6(1);
            //----------------------------------------------------------------
              if ProdaveConnectActive<>0 then
              begin
                str_error:=(FormDateTime+'-Ошибка активации соединения с контроллером(SetActiveConnection_ex6)');
                writeLn(f,str_error);
                ShowMessage(str_error);
              end
              else
              begin
                //writeLn(f,'Connect on PLC');
                //ShowMessage('Активация соединение с контроллером успешное');
              end;
              //----------------------------------------------------------------


        end;
         amount:=315;
        resD:=field_read_ex6('M',0,3000,amount,Byte_Col,buffer_19,Byte_Col_r);
        if resD<>0 then UnloadConnection_ex6(1);
        if resD=0 then  critical_buffer_plc(buffer_s400,buffer_19);

        buffer_mm[0]:=buffer_s400[33];
        buffer_mm[1]:=buffer_s400[34];
        buffer_mm[2]:=buffer_s400[35];
        buffer_mm[3]:=buffer_s400[51];
        buffer_mm[4]:=buffer_s400[52];
        buffer_mm[5]:=buffer_s400[53];
        buffer_mm[6]:=buffer_s400[54];
        buffer_mm[7]:=buffer_s400[55];
        buffer_mm[8]:=buffer_s400[3];
        buffer_mm[9]:=buffer_s400[156];
        buffer_mm[10]:=buffer_s400[155];
        critical_buffer_mess(buffer_mess,buffer_mm);
        critical_buffer_1s(buffer_s400,buffer_19);
        critical_buffer_tcp(buffer_s400,buffer_19);
    end;
end;
