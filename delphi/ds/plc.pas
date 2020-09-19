procedure read_MEC;stdcall;
var
 resD:LongInt;
 plc_adr_table : array [0..10] of Byte;
 name_con:array [0..10] of Char;
 buffer_MEC:buffer_array;
 Con_Table:Con_Table_Type;
 amount: Integer;
 Byte_Col,Byte_Col_r:DWORD;
 ConTableLen:byte;
 buffer_mm:buffer_array_mess;
 ProdaveConnect:integer;
 ProdaveConnectActive:integer;
begin
    resD:=5;
    Byte_Col:=SizeOf(buffer_MEC);
    while true do begin
        sleep(70);
        if resD<>0 then begin
          StrCopy(name_con,'s7online');
          TCon_Table_Type.IP_Addr[0]:=192;
          TCon_Table_Type.IP_Addr[1]:=168;
          TCon_Table_Type.IP_Addr[2]:=0;
          TCon_Table_Type.IP_Addr[3]:=21;
          TCon_Table_Type.IP_Addr[4]:=0;
          TCon_Table_Type.IP_Addr[5]:=0;
          TCon_Table_Type.AdrType:=3;
          TCon_Table_Type.SlotNr:=2;
          TCon_Table_Type.RackNr:=0;
          ConTableLen:=SizeOf(TCon_Table_Type);
          ProdaveConnect:=LoadConnection_ex6 ( 1, addr(name_con),ConTableLen,addr(TCon_Table_Type));
              //----------------------------------------------------------------
              if ProdaveConnect<>0 then
              begin
                //writeLn(f,'Not connect on PLC');
                ShowMessage('Ошибка связи с контроллером');
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
                //writeLn(f,'Not connect on PLC');
                ShowMessage('Ошибка активации соединения с контроллером');
              end
              else
              begin
                //writeLn(f,'Connect on PLC');
                //ShowMessage('Активация соединение с контроллером успешное');
              end;
              //----------------------------------------------------------------
        end;
         amount:=150;
        resD:=field_read_ex6('M',0,2000,amount,Byte_Col,buffer_MEC,Byte_Col_r);

        if resD<>0 then
        begin
             UnloadConnection_ex6(1);
             ShowMessage('Ошибка field_read_ex6');
        end;  

        if resD=0 then
        begin
            critical_buffer_plc(buffer_MEM,buffer_MEC);
            //ShowMessage('field_read_ex6 успешное');
        end;

        buffer_mm[0]:=buffer_MEM[0];
        buffer_mm[1]:=buffer_MEM[12];
        buffer_mm[2]:=buffer_MEM[13];
        buffer_mm[3]:=buffer_MEM[14];
        buffer_mm[4]:=buffer_MEM[15];
        buffer_mm[5]:=buffer_MEM[16];
        buffer_mm[6]:=buffer_MEM[17];
        buffer_mm[7]:=buffer_MEM[18];
        buffer_mm[8]:=buffer_MEM[19];
        buffer_mm[9]:=buffer_MEM[20];
        buffer_mm[10]:=buffer_MEM[21];
        buffer_mm[11]:=buffer_MEM[22];
        buffer_mm[12]:=buffer_MEM[23];
        critical_buffer_mess(buffer_mess,buffer_mm);
        critical_buffer_1s(buffer_MEM,buffer_MEC);
    end;
end;