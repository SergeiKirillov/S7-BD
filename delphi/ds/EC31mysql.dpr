program EC31mysql;

{$APPTYPE CONSOLE}
{$R-}
{$S-}
{$A+}
{$C-}
uses
  SysUtils,
  Windows,
  MySQL,
  Dialogs,
  WinSock;

type Con_Addr_Type=record
     IP_Addr: array[0..4] of byte;
end;

type Con_Table_Type=record
     IP_Addr: array[0..5] of byte;
     AdrType:byte;
     SlotNr:byte;
     RackNr:byte;
end;

type buffer_array=array [0..75] of SmallInt;
type buffer_array_mess=array [0..13] of SmallInt;
type buffer_1s_type=array [0..45] of SmallInt;

type PCon_Table_Type=^Con_Table_Type;

type
  TByte_Signal=record
  Reg_RS,Zapr_S_Mot,Reg_NO,Reg_TD,KNP,VS_Stan,AO_Stan,Davl_PGT,
  T_Redukt,Got_64_1,Got_G21_PGT,Got_G22_PGT_SD,Got_G23_Red,
  Got_G24_NV,FO_SD,SB_FO,FO_Obriv,Reg_FO,VAT_R,VAT_K,VAT_M,
  LK_R,LK_NU,LK_M,LK_K,LK_NV,Got_VSP_M,T_Razm,Got_VSP_R,T_Mot,
  T_R_Vkl,T_M_Vkl,NO_vip,SB_VV29_Vkl,VV29_AO,War_T_Lev,Err_T_Lev,
  War_T_Pr,Err_T_Pr,Blok_VV_29,VV29_Vkl_Y,VV29_Vikl_Pan,VV29_Vikl_Sim,
  VV29_Vikl_Y,SQ_VV29_Vkl,SQ_VV29_Vikl,VV27_Vkl_Y,VV27_Vikl_Y,Vkl_VV27_NU_Pan,
  SB_Vkl_Sim_NU_27,Warn_Tr_NU,Error_Temp_Tr_NU,Error_VV27_NU_I,Blok_VV27_NU_Pan,
  Vikl_VV27_NU_Pan,SB_Vikl_Sim_NU_27,Basa_SB_vv43_M_vkl,Err_VV43_Mot,
  Warn_Temp_Tr1_M,Err_Temp_Tr1_M,Warn_Temp_Tr2_M,Err_Temp_Tr2_M,
  Blok_VV43_Mot_Pan,Vkl_VV43_Mot_Q,Vikl_VV43_Mot_Pan,SB_M_UZ5_vv43_Vikl,
  Vikl_VV43_Q,Basa_SB_vv41_K_vkl,Error_VV41_Kl_I,Warn_Temp_Tr1_UZ5_K,
  Err_Temp_Tr1_UZ5_K,Warn_Temp_Tr2_UZ5_K,Err_Temp_Tr2_UZ5_K,Blok_VV41_Kl_Pan,
  Vkl_VV41_Kl_Q,Vikl_VV41_Kl_Pan,SB_vv41_UZ5_K_vikl,Vikl_VV41_Kl_Q,Basa_SB_vv39_R_vkl,
  Error_VV39_Razm_I,Warn_Temp_Tr1_R,Err_Temp_Tr1_R,Warn_Temp_Tr2_R,
  Err_Temp_Tr2_R,Blok_VV39_Razm_Pan,Vkl_VV39_Razm_Q,Vikl_VV39_Razm_Pan,
  SB_vv39_UZ5_R_vikl,Vikl_VV39_Razm_Q,RDSH_VAT_R,RDSH_VAT_K,RDSH_VAT_M,
  Vkl_VV27_NU_I,Vikl_VV27_NU_I,Vkl_VV39_Razm_I,Vikl_VV39_Razm_I,
  Vkl_VV41_Kl_I,Vikl_VV41_Kl_I,Vkl_VV43_Mot_I,Vikl_VV43_Mot_I,
  Davl_Red_Mot_EKM,SB_AO_R_UZ4,SB_AO_K_UZ4,SB_AO_M_UZ4,FO_vspommex_Mot,
  FO_vspommex_razm,KIP_Termopari,SB_AO_St_PUM_Bok,SB_AO_St_PUR,
  SY_Zapr_Sb_PUK,SB_NO_PUK,SB_NO_PUM:ShortInt;
end;

var
 th_sql,th_MEC,th_mess,th_1s: cardinal;
 h_sql,h_MEC,h_mess,h_1s:integer;
 CS,CS_mess,CS_1s:TRTLCriticalSection;
 buffer_MEM:buffer_array;
 TCon_Table_Type:Con_Table_Type;
 buffer_mess:buffer_array_mess;
 Work_Time_Start,Work_Time_Stop:ShortString;
 Start_Rulon:integer;
 B:SmallInt;
 H,Massa,RMot_Pred:Real;
 f:TextFile;


function LoadConnection_ex6 ( no : Byte; name :PChar;dlina: byte;
             adr :PCon_Table_Type) : Longint; stdcall;
             external 'prodave6.dll' name 'LoadConnection_ex6';
function field_read_ex6 ( TypeData:Char; dbno : SmallInt;
             StartNr: SmallInt; amount : Integer; BufLen: Integer;
             var DatBuf; var DatLen): Integer;stdcall;
             external 'prodave6.dll' name 'field_read_ex6';
function UnloadConnection_ex6( no : Byte) : Integer; stdcall;
             external 'prodave6.dll' name 'UnloadConnection_ex6';
function SetActiveConnection_ex6( no : Byte) : Integer; stdcall;
             external 'prodave6.dll' name 'SetActiveConnection_ex6';


procedure critical_buffer_plc(var buffer_write:buffer_array;
                              buffer_read:buffer_array);
begin
EnterCriticalSection(CS);
   buffer_write:=buffer_read;
LeaveCriticalSection(CS);
end;

procedure critical_buffer_mess(var buffer_write:buffer_array_mess;
                              buffer_read:buffer_array_mess);
begin
EnterCriticalSection(CS_mess);
   buffer_write:=buffer_read;
LeaveCriticalSection(CS_mess);
end;

procedure critical_buffer_1s(var buffer_write:buffer_array;
                              buffer_read:buffer_array);
begin
EnterCriticalSection(CS_1s);
   buffer_write:=buffer_read;
LeaveCriticalSection(CS_1s);
end;

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
       //���� ����������� ������� �� 0




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
           +FormDateTime+'","'
           +floattostr(buffer_sql[0]/100)+'","'
           +floattostr(buffer_sql[1])+'","'
           +floattostr(buffer_sql[2])+'","'
           +floattostr(buffer_sql[3]/10)+'","'
           +floattostr(buffer_sql[4]/10)+'","'
           +floattostr(buffer_sql[5]/10)+'","'
           +floattostr(buffer_sql[6]*10)+'","'
           +floattostr(buffer_sql[7]*10)+'","'
           +floattostr(buffer_sql[8])+'","'
           +floattostr(buffer_sql[9])+'","'
           +floattostr(buffer_sql[10]/100)+'","'
           +floattostr(buffer_sql[11]/100)+'","'
           +floattostr(buffer_sql[24]/100)+'","'
           +floattostr(buffer_sql[25]/1)+'","'
           +floattostr(buffer_sql[26]/10)+'","'
           +floattostr(buffer_sql[27]/100)+'","'
           +floattostr(buffer_sql[28]/10)+'","'
           +floattostr(buffer_sql[64]/1)+'","'
           +floattostr(buffer_sql[65]/1)+'","'
           +floattostr(buffer_sql[66]/1)+'","'
           +floattostr(buffer_sql[67]/1)+'","'
           +floattostr(buffer_sql[68]/100)+'","'
           +floattostr(buffer_sql[69]/10)+'","'
           +floattostr(buffer_sql[70]/1)+'","'
           +floattostr(buffer_sql[71]/1)+'","'
           +floattostr(buffer_sql[72]/100)+'","'
           +floattostr(buffer_sql[73]/1)+'","'
           +floattostr(buffer_sql[74]/1)
           +'")';
          //showmessage(My_Query_signal);
          // writeLn(f,My_Query_signal);

          res:=mysql_real_query(my_con,PChar(My_Query_signal),
                            StrLen(PChar(My_Query_signal)));
          //���� 0 �� ������ �������� ������

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
          //E��� ��������� ������� ������ ������ ��:
          //   - � ������=1146 (��� ����� �������) ��� ����� ������� �� ������� �������
          //   - � ��������� ������ ������� ��� ������ �� ����� � ���������� ��� � ���� 

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
               // ShowMessage('������ 100�� �� ���������!');
                str_error:=FormDateTime+'-������ � ������� 100ms �� ��������, error='+IntToStr(mysql_errno(my_con))+' ��������='+mysql_error(my_con);
                writeLn(f,str_error);
                showmessage(str_error);
             end;  

          end;  


          {

              //----------------------------------------------------------------
              if res<>0 then
              begin
                writeLn(f,'mysql_real_connect-100ms');
                ShowMessage('������ 100�� �� ���������!');
                str_error:=FormDateTime+'-������ � ������� ���������(mess) �� ��������, error='+IntToStr(mysql_errno(my_con))+' ��������='+mysql_error(my_con);
                writeLn(f,str_error);
                showmessage(str_error);

              end
              else
              begin
                //writeLn(f,'Connect on PLC');
                //ShowMessage('��������� ���������� � ������������ ��������');
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
// 2015-11-02 12:09 ��������� �������� ������ ������� 100��
//        My_Query_signal:='drop table if exists '+BaseDel;
//          mysql_real_query(my_con,PChar(My_Query_signal),StrLen(PChar(My_Query_signal)));
      end;

end;

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
                ShowMessage('������ ����� � ������������');
              end
              else
              begin
                //writeLn(f,'Connect on PLC');
                //ShowMessage('���������� � ������������ ��������');
              end;
              //----------------------------------------------------------------
           ProdaveConnectActive:=SetActiveConnection_ex6(1);
              //----------------------------------------------------------------
              if ProdaveConnectActive<>0 then
              begin
                //writeLn(f,'Not connect on PLC');
                ShowMessage('������ ��������� ���������� � ������������');
              end
              else
              begin
                //writeLn(f,'Connect on PLC');
                //ShowMessage('��������� ���������� � ������������ ��������');
              end;
              //----------------------------------------------------------------
        end;
         amount:=150;
        resD:=field_read_ex6('M',0,2000,amount,Byte_Col,buffer_MEC,Byte_Col_r);

        if resD<>0 then
        begin
             UnloadConnection_ex6(1);
             ShowMessage('������ field_read_ex6');
        end;  

        if resD=0 then
        begin
            critical_buffer_plc(buffer_MEM,buffer_MEC);
            //ShowMessage('field_read_ex6 ��������');
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
     //���� ����������� ������� �� 0
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
        //SQL ������ �� �������
        //showmessage(My_Query_1s);
         res:=mysql_real_query(my_con_1s,PChar(My_Query_1s),
                          StrLen(PChar(My_Query_1s)));
        //���� 0 �� ������ �������� ������
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
          //E��� ��������� ������� ������ ������ ��:
          //   - � ������=1146 (��� ����� �������) ��� ����� ������� �� ������� �������
          //   - � ��������� ������ ������� ��� ������ �� ����� � ���������� ��� � ����

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
               // ShowMessage('������ 1� �� ���������!');
                str_error:=FormDateTime+'-������ � ������� 1s �� ��������, error='+IntToStr(mysql_errno(my_con_1s))+' ��������='+mysql_error(my_con_1s);
                writeLn(f,str_error);
                showmessage(str_error);
             end;

          end;


        {
         //----------------------------------------------------------------
        if res<>0 then
         begin
                writeLn(f,'mysql_real_connect-1s');
                ShowMessage('������ 1� �� ���������!');
                str_error:=FormDateTime+'-������ � ������� ���������(mess) �� ��������, error='+IntToStr(mysql_errno(my_con_1s))+' ��������='+mysql_error(my_con_1s);
                writeLn(f,str_error);
                showmessage(str_error);

         end
        else
        begin
                //writeLn(f,'Connect on PLC');
                //ShowMessage('��������� ���������� � ������������ ��������');
        end;
              //----------------------------------------------------------------


        if res<>0 then begin
           My_Query_1s:='create table '+BaseName+' like stan_1s_temp';
           mysql_real_query(my_con_1s,PChar(My_Query_1s),
                          StrLen(PChar(My_Query_1s)));

        end;
        }


        BaseDel:='stan_1s_'+FormatDateTime('yyyy_mm_dd',(DaySignal));
// 2015-11-02 12:09 ��������� �������� ������ �������   1���
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
                   // ShowMessage('������ 1� �� ���������!');
                    str_error:=FormDateTime+'-������ � ������� work_ds �� ��������, error='+IntToStr(mysql_errno(my_con_1s))+' ��������='+mysql_error(my_con_1s);
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

procedure write_mess;stdcall;
var
 Byte_Signal,Byte_Signal_minus:TByte_Signal;
 My_Query,V,BaseName,BaseDel:String;
 FormDateTime:ShortString;
 rc_rm:bool;
 res,ViewCount:Integer;
 Byte_Col_mess,Bytes_Read:DWORD;
 my_con_m,My_conn_m:pmysql;
 buffer_m:buffer_array_mess;
 TekTime,DaySignal:TDateTime;
 res_con,my_conn_option:integer;
  str_error:string;
  res_client,res_connect,res_result:Integer;
begin
   my_con_m:=mysql_init(nil);
        //������������� ������ ���������
           //my_conn_option:=mysql_options(my_con_m, MYSQL_SET_CHARSET_NAME, 'CP1251');
           //my_conn_option:=mysql_options(my_con_m, MYSQL_SET_CHARSET_NAME, 'UFT8');
  //------------------------------------------------------------------------------
//�������� ���������� ����� ����������
{
  if my_conn_option<>0 then
  begin
     //writeLn(f,'not connect on MySQL');
     str_error:=FormDateTime+'-������� ������� �� ��������, error='+IntToStr(mysql_errno(my_con_m))+' ��������='+mysql_error(my_con_m);
    // writeLn(f,str_error);
     ShowMessage(str_error);
  end
  else
  begin
     //writeLn(f,'connect on MySQL');
     ShowMessage('mysql_options=OK');
  end;
  //------------------------------------------------------------------------------
}

   my_conn_m:=mysql_real_connect(my_con_m,PChar('localhost'),
            PChar('writerM'),PChar('5555555'),PChar('basa'),
            3306,PChar('3306'),0);
   {My_conn_m:=mysql_real_connect(my_con_m,PChar('192.168.0.111'),
            PChar('writerM'),PChar('5555555'),PChar('basa'),
            3306,PChar('3306'),0); }
  res_con:=mysql_select_db(my_con_m,PChar('basa'));
   Byte_Col_mess:=SizeOf(buffer_m);
   rc_rm:=false;
   ViewCount:=0;
  while true do begin
    sleep(90);
    critical_buffer_mess(buffer_m,buffer_mess);

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
     -EncodeTime(12,20,0,0);
     V:=intToStr(buffer_m[0]);
     BaseName:='stan_mess_'+FormatDateTime('yyyy_mm_dd',(TekTime));
     FormDateTime:=FormatDateTime('yyyy-mm-dd hh:nn:ss.zzz',(TekTime));
     //mb2025
     Byte_Signal.Reg_RS:=Ord(Swap(buffer_m[1]) and 1=1);
     Byte_Signal.Zapr_S_Mot:=Ord(Swap(buffer_m[1]) and 2=2);
     Byte_Signal.Reg_NO:=Ord(Swap(buffer_m[1]) and 4=4);
     Byte_Signal.Reg_TD:=Ord(Swap(buffer_m[1]) and 8=8);
     Byte_Signal.KNP:=Ord(Swap(buffer_m[1]) and 16=16);
     Byte_Signal.VS_Stan:=Ord(Swap(buffer_m[1]) and 32=32);
     Byte_Signal.AO_Stan:=Ord(Swap(buffer_m[1]) and 64=64);
     Byte_Signal.Davl_PGT:=Ord(Swap(buffer_m[1]) and 128=128);
     //mb2024
     Byte_Signal.T_Redukt:=Ord(Swap(buffer_m[1]) and 256=256);
     Byte_Signal.Got_64_1:=Ord(Swap(buffer_m[1]) and 512=512);
     Byte_Signal.Got_G21_PGT:=Ord(Swap(buffer_m[1]) and 1024=1024);
     Byte_Signal.Got_G22_PGT_SD:=Ord(Swap(buffer_m[1]) and 2048=2048);
     Byte_Signal.Got_G23_Red:=Ord(Swap(buffer_m[1]) and 4096=4096);
     Byte_Signal.Got_G24_NV:=Ord(Swap(buffer_m[1]) and 8192=8192);
     Byte_Signal.FO_SD:=Ord(Swap(buffer_m[1]) and 16384=16384);
     Byte_Signal.SB_FO:=Ord(Swap(buffer_m[1]) and 32768=32768);
     //mb2027
     Byte_Signal.FO_Obriv:=Ord(Swap(buffer_m[2]) and 1=1);
     Byte_Signal.Reg_FO:=Ord(Swap(buffer_m[2]) and 2=2);
     Byte_Signal.VAT_R:=Ord(Swap(buffer_m[2]) and 4=4);
     Byte_Signal.VAT_K:=Ord(Swap(buffer_m[2]) and 8=8);
     Byte_Signal.VAT_M:=Ord(Swap(buffer_m[2]) and 16=16);
     Byte_Signal.LK_R:=Ord(Swap(buffer_m[2]) and 32=32);
     Byte_Signal.LK_NU:=Ord(Swap(buffer_m[2]) and 64=64);
     Byte_Signal.LK_M:=Ord(Swap(buffer_m[2]) and 128=128);
     //mb2026
     Byte_Signal.LK_K:=Ord(Swap(buffer_m[2]) and 256=256);
     Byte_Signal.LK_NV:=Ord(Swap(buffer_m[2]) and 512=512);
     Byte_Signal.Got_VSP_M:=Ord(Swap(buffer_m[2]) and 1024=1024);
     Byte_Signal.T_Razm:=Ord(Swap(buffer_m[2]) and 2048=2048);
     Byte_Signal.Got_VSP_R:=Ord(Swap(buffer_m[2]) and 4096=4096);
     Byte_Signal.T_Mot:=Ord(Swap(buffer_m[2]) and 8192=8192);
     Byte_Signal.T_R_Vkl:=Ord(Swap(buffer_m[2]) and 16384=16384);
     Byte_Signal.T_M_Vkl:=Ord(Swap(buffer_m[2]) and 32768=32768);
     //mb2029
     Byte_Signal.NO_vip:=Ord(Swap(buffer_m[3]) and 1=1);
     Byte_Signal.SB_VV29_Vkl:=Ord(Swap(buffer_m[3]) and 2=2);
     Byte_Signal.VV29_AO:=Ord(Swap(buffer_m[3]) and 4=4);
     Byte_Signal.War_T_Lev:=Ord(Swap(buffer_m[3]) and 8=8);
     Byte_Signal.Err_T_Lev:=Ord(Swap(buffer_m[3]) and 16=16);
     Byte_Signal.War_T_Pr:=Ord(Swap(buffer_m[3]) and 32=32);
     Byte_Signal.Err_T_Pr:=Ord(Swap(buffer_m[3]) and 64=64);
     Byte_Signal.Blok_VV_29:=Ord(Swap(buffer_m[3]) and 128=128);
     //mb2028
     Byte_Signal.VV29_Vkl_Y:=Ord(Swap(buffer_m[3]) and 256=256);
     Byte_Signal.VV29_Vikl_Pan:=Ord(Swap(buffer_m[3]) and 512=512);
     Byte_Signal.VV29_Vikl_Sim:=Ord(Swap(buffer_m[3]) and 1024=1024);
     Byte_Signal.VV29_Vikl_Y:=Ord(Swap(buffer_m[3]) and 2048=2048);
     Byte_Signal.SQ_VV29_Vkl:=Ord(Swap(buffer_m[3]) and 4096=4096);
     Byte_Signal.SQ_VV29_Vikl:=Ord(Swap(buffer_m[3]) and 8192=8192);
     Byte_Signal.VV27_Vkl_Y:=Ord(Swap(buffer_m[3]) and 16384=16384);
     Byte_Signal.VV27_Vikl_Y:=Ord(Swap(buffer_m[3]) and 32768=32768);
      //mb2031
     Byte_Signal.Vkl_VV27_NU_Pan:=Ord(Swap(buffer_m[4]) and 1=1);
     Byte_Signal.SB_Vkl_Sim_NU_27:=Ord(Swap(buffer_m[4]) and 2=2);
     Byte_Signal.Warn_Tr_NU:=Ord(Swap(buffer_m[4]) and 4=4);
     Byte_Signal.Error_Temp_Tr_NU:=Ord(Swap(buffer_m[4]) and 8=8);
     Byte_Signal.Error_VV27_NU_I:=Ord(Swap(buffer_m[4]) and 16=16);
     Byte_Signal.Blok_VV27_NU_Pan:=Ord(Swap(buffer_m[4]) and 32=32);
     Byte_Signal.Vikl_VV27_NU_Pan:=Ord(Swap(buffer_m[4]) and 64=64);
     Byte_Signal.SB_Vikl_Sim_NU_27:=Ord(Swap(buffer_m[4]) and 128=128);
     //mb2030
     Byte_Signal.Basa_SB_vv43_M_vkl:=Ord(Swap(buffer_m[4]) and 256=256);
     Byte_Signal.Err_VV43_Mot:=Ord(Swap(buffer_m[4]) and 512=512);
     Byte_Signal.Warn_Temp_Tr1_M:=Ord(Swap(buffer_m[4]) and 1024=1024);
     Byte_Signal.Err_Temp_Tr1_M:=Ord(Swap(buffer_m[4]) and 2048=2048);
     Byte_Signal.Warn_Temp_Tr2_M:=Ord(Swap(buffer_m[4]) and 4096=4096);
     Byte_Signal.Err_Temp_Tr2_M:=Ord(Swap(buffer_m[4]) and 8192=8192);
     Byte_Signal.Blok_VV43_Mot_Pan:=Ord(Swap(buffer_m[4]) and 16384=16384);
     Byte_Signal.Vkl_VV43_Mot_Q:=Ord(Swap(buffer_m[4]) and 32768=32768);
      //mb2033
     Byte_Signal.Vikl_VV43_Mot_Pan:=Ord(Swap(buffer_m[5]) and 1=1);
     Byte_Signal.SB_M_UZ5_vv43_Vikl:=Ord(Swap(buffer_m[5]) and 2=2);
     Byte_Signal.Vikl_VV43_Q:=Ord(Swap(buffer_m[5]) and 4=4);
     Byte_Signal.Basa_SB_vv41_K_vkl:=Ord(Swap(buffer_m[5]) and 8=8);
     Byte_Signal.Error_VV41_Kl_I:=Ord(Swap(buffer_m[5]) and 16=16);
     Byte_Signal.Warn_Temp_Tr1_UZ5_K:=Ord(Swap(buffer_m[5]) and 32=32);
     Byte_Signal.Err_Temp_Tr1_UZ5_K:=Ord(Swap(buffer_m[5]) and 64=64);
     Byte_Signal.Warn_Temp_Tr2_UZ5_K:=Ord(Swap(buffer_m[5]) and 128=128);
     //mb2032
     Byte_Signal.Err_Temp_Tr2_UZ5_K:=Ord(Swap(buffer_m[5]) and 256=256);
     Byte_Signal.Blok_VV41_Kl_Pan:=Ord(Swap(buffer_m[5]) and 512=512);
     Byte_Signal.Vkl_VV41_Kl_Q:=Ord(Swap(buffer_m[5]) and 1024=1024);
     Byte_Signal.Vikl_VV41_Kl_Pan:=Ord(Swap(buffer_m[5]) and 2048=2048);
     Byte_Signal.SB_vv41_UZ5_K_vikl:=Ord(Swap(buffer_m[5]) and 4096=4096);
     Byte_Signal.Vikl_VV41_Kl_Q:=Ord(Swap(buffer_m[5]) and 8192=8192);
     Byte_Signal.Basa_SB_vv39_R_vkl:=Ord(Swap(buffer_m[5]) and 16384=16384);
     Byte_Signal.Error_VV39_Razm_I:=Ord(Swap(buffer_m[5]) and 32768=32768);
     //mb2035
     Byte_Signal.SB_AO_St_PUM_Bok:=Ord(Swap(buffer_m[6]) and 1=1);
     Byte_Signal.Warn_Temp_Tr1_R:=Ord(Swap(buffer_m[6]) and 2=2);
     Byte_Signal.Err_Temp_Tr1_R:=Ord(Swap(buffer_m[6]) and 4=4);
     Byte_Signal.Warn_Temp_Tr2_R:=Ord(Swap(buffer_m[6]) and 8=8);
     Byte_Signal.Err_Temp_Tr2_R:=Ord(Swap(buffer_m[6]) and 16=16);
     Byte_Signal.Blok_VV39_Razm_Pan:=Ord(Swap(buffer_m[6]) and 32=32);
     Byte_Signal.Vkl_VV39_Razm_Q:=Ord(Swap(buffer_m[6]) and 64=64);
     Byte_Signal.SB_AO_St_PUR:=Ord(Swap(buffer_m[6]) and 128=128);
     //mb2034
     Byte_Signal.Vikl_VV39_Razm_Pan:=Ord(Swap(buffer_m[6]) and 256=256);
     Byte_Signal.SB_vv39_UZ5_R_vikl:=Ord(Swap(buffer_m[6]) and 512=512);
     Byte_Signal.Vikl_VV39_Razm_Q:=Ord(Swap(buffer_m[6]) and 1024=1024);
     Byte_Signal.RDSH_VAT_R:=Ord(Swap(buffer_m[6]) and 2048=2048);
     Byte_Signal.RDSH_VAT_K:=Ord(Swap(buffer_m[6]) and 4096=4096);
     Byte_Signal.RDSH_VAT_M:=Ord(Swap(buffer_m[6]) and 8192=8192);
     Byte_Signal.Vkl_VV27_NU_I:=Ord(Swap(buffer_m[6]) and 16384=16384);
     Byte_Signal.Vikl_VV27_NU_I:=Ord(Swap(buffer_m[6]) and 32768=32768);
     //mb2037
     Byte_Signal.Vkl_VV39_Razm_I:=Ord(Swap(buffer_m[7]) and 1=1);
     Byte_Signal.Vikl_VV39_Razm_I:=Ord(Swap(buffer_m[7]) and 2=2);
     Byte_Signal.Vkl_VV41_Kl_I:=Ord(Swap(buffer_m[7]) and 4=4);
     Byte_Signal.Vikl_VV41_Kl_I:=Ord(Swap(buffer_m[7]) and 8=8);
     Byte_Signal.Vkl_VV43_Mot_I:=Ord(Swap(buffer_m[7]) and 16=16);
     Byte_Signal.Vikl_VV43_Mot_I:=Ord(Swap(buffer_m[7]) and 32=32);
     Byte_Signal.Davl_Red_Mot_EKM:=Ord(Swap(buffer_m[7]) and 64=64);
     Byte_Signal.SB_AO_R_UZ4:=Ord(Swap(buffer_m[7]) and 128=128);
     //mb2036
     Byte_Signal.SB_AO_K_UZ4:=Ord(Swap(buffer_m[7]) and 256=256);
     Byte_Signal.SB_AO_M_UZ4:=Ord(Swap(buffer_m[7]) and 512=512);
     Byte_Signal.FO_vspommex_Mot:=Ord(Swap(buffer_m[7]) and 1024=1024);
     Byte_Signal.FO_vspommex_razm:=Ord(Swap(buffer_m[7]) and 2048=2048);
     Byte_Signal.KIP_Termopari:=Ord(Swap(buffer_m[7]) and 4096=4096);
     Byte_Signal.SY_Zapr_Sb_PUK:=Ord(Swap(buffer_m[7]) and 8192=8192);
     Byte_Signal.SB_NO_PUK:=Ord(Swap(buffer_m[7]) and 16384=16384);
     Byte_Signal.SB_NO_PUM:=Ord(Swap(buffer_m[7]) and 32768=32768);
    My_Query:=
    'insert into '+BaseName+' values';
    ///�� 0 � 1
    if (Byte_Signal_minus.Reg_RS-Byte_Signal.Reg_RS)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Zapr_S_Mot-Byte_Signal.Zapr_S_Mot)<0 then
    begin
      My_Query:=My_Query
      +'(1,'''
      + FormDateTime+
      ''',''������ � ������ ������� ���������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Zapr_S_Mot-Byte_Signal.Zapr_S_Mot)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''������ � ������ ������� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SY_Zapr_Sb_PUK-Byte_Signal.SY_Zapr_Sb_PUK)<0 then
    begin
      My_Query:=My_Query
      +'(1,'''
      + FormDateTime+
      ''',''������ � ������ ����� ���������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.SY_Zapr_Sb_PUK-Byte_Signal.SY_Zapr_Sb_PUK)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''������ � ������ ����� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Reg_NO-Byte_Signal.Reg_NO)<0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''���������� �������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Reg_TD-Byte_Signal.Reg_TD)<0 then
    begin
      My_Query:=My_Query
      +'(1,'''
      + FormDateTime+
      ''',''��� �������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.KNP-Byte_Signal.KNP)<0 then
    begin
      My_Query:=My_Query
      +'(5,'''
      + FormDateTime+
      ''',''���� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.VS_Stan-Byte_Signal.VS_Stan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �� � �� �����'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.AO_Stan-Byte_Signal.AO_Stan)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''������ �� ����� � �� �����'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.SB_AO_St_PUM_Bok-Byte_Signal.SB_AO_St_PUM_Bok)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''������ �� ����� � �� ������� (���)'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.SB_AO_St_PUR-Byte_Signal.SB_AO_St_PUR)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''������ �� ����� � �� �������������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.SB_NO_PUK-Byte_Signal.SB_NO_PUK)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''������ �� � �� �����'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.SB_NO_PUM-Byte_Signal.SB_NO_PUM)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''������ �� � �� �������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Davl_PGT-Byte_Signal.Davl_PGT)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������� ��� ����������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Davl_PGT-Byte_Signal.Davl_PGT)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''�������� ��� �����������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.T_Redukt-Byte_Signal.T_Redukt)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''����������� ���������� ����������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.T_Redukt-Byte_Signal.T_Redukt)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''����������� ���������� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Got_64_1-Byte_Signal.Got_64_1)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������������ �1 ������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Got_64_1-Byte_Signal.Got_64_1)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''������������ �1 �� ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Got_G21_PGT-Byte_Signal.Got_G21_PGT)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�-21 (��� �������) ������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Got_G21_PGT-Byte_Signal.Got_G21_PGT)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''�-21 (��� �������) �� ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Got_G22_PGT_SD-Byte_Signal.Got_G22_PGT_SD)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�-22 (��� ��) ������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Got_G22_PGT_SD-Byte_Signal.Got_G22_PGT_SD)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''�-22 (��� ��) �� ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Got_G23_Red-Byte_Signal.Got_G23_Red)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�-23 (������ ����������) ������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Got_G23_Red-Byte_Signal.Got_G23_Red)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''�-23 (������ ����������) �� ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Got_G24_NV-Byte_Signal.Got_G24_NV)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�-24 (������ ��) ������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Got_G24_NV-Byte_Signal.Got_G24_NV)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''�-24 (������ ��) �� ������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.FO_SD-Byte_Signal.FO_SD)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''������ �� � �� �����'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.SB_FO-Byte_Signal.SB_FO)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''������ �� � �� �������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.FO_Obriv-Byte_Signal.FO_Obriv)<0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''����� ������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Reg_FO-Byte_Signal.Reg_FO)>0 then
    begin
      My_Query:=My_Query
      +'(4,'''
      + FormDateTime+
      ''',''����� �������������� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.VAT_R-Byte_Signal.VAT_R)<0 then
    begin
      My_Query:=My_Query
      +'(6,'''
      + FormDateTime+
      ''',''��� ������������� �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.VAT_R-Byte_Signal.VAT_R)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''��� ������������� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.VAT_K-Byte_Signal.VAT_K)<0 then
    begin
      My_Query:=My_Query
      +'(6,'''
      + FormDateTime+
      ''',''��� ����� �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.VAT_K-Byte_Signal.VAT_K)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''��� ����� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.VAT_M-Byte_Signal.VAT_M)<0 then
    begin
      My_Query:=My_Query
      +'(6,'''
      + FormDateTime+
      ''',''��� ������� �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.VAT_M-Byte_Signal.VAT_M)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''��� ������� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.LK_R-Byte_Signal.LK_R)<0 then
    begin
      My_Query:=My_Query
      +'(6,'''
      + FormDateTime+
      ''',''�� ������������� �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.LK_R-Byte_Signal.LK_R)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''�� ������������� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.LK_NU-Byte_Signal.LK_NU)<0 then
    begin
      My_Query:=My_Query
      +'(6,'''
      + FormDateTime+
      ''',''�� ��������� ���������� �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.LK_NU-Byte_Signal.LK_NU)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''�� ��������� ���������� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.LK_M-Byte_Signal.LK_M)<0 then
    begin
      My_Query:=My_Query
      +'(6,'''
      + FormDateTime+
      ''',''�� ������� �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.LK_M-Byte_Signal.LK_M)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''�� ������� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.LK_K-Byte_Signal.LK_K)<0 then
    begin
      My_Query:=My_Query
      +'(6,'''
      + FormDateTime+
      ''',''�� ����� �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.LK_K-Byte_Signal.LK_K)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''�� ����� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.LK_NV-Byte_Signal.LK_NV)<0 then
    begin
      My_Query:=My_Query
      +'(6,'''
      + FormDateTime+
      ''',''�� �� �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.LK_NV-Byte_Signal.LK_NV)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''�� �� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Got_VSP_M-Byte_Signal.Got_VSP_M)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� ������� ������ � ��'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Got_VSP_M-Byte_Signal.Got_VSP_M)>0 then
    begin
      My_Query:=My_Query
      +'(1,'''
      + FormDateTime+
      ''',''�������������� ������� �� ������ � ��'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.T_Razm-Byte_Signal.T_Razm)<0 then
    begin
      My_Query:=My_Query
      +'(6,'''
      + FormDateTime+
      ''',''��������� �� ������������� ��������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.T_Razm-Byte_Signal.T_Razm)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''��������� �� ������������� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Got_VSP_R-Byte_Signal.Got_VSP_R)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� ������������� � �� ������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Got_VSP_R-Byte_Signal.Got_VSP_R)>0 then
    begin
      My_Query:=My_Query
      +'(1,'''
      + FormDateTime+
      ''',''�������������� ������������� � �� �� ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.T_Mot-Byte_Signal.T_Mot)<0 then
    begin
      My_Query:=My_Query
      +'(6,'''
      + FormDateTime+
      ''',''��������� �� ������� ��������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.T_Mot-Byte_Signal.T_Mot)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''��������� �� ������� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.T_R_Vkl-Byte_Signal.T_R_Vkl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''��������� �� ������������� ��������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.T_R_Vkl-Byte_Signal.T_R_Vkl)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''��������� �� ������������� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.T_M_Vkl-Byte_Signal.T_M_Vkl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''��������� �� ������� ��������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.T_M_Vkl-Byte_Signal.T_M_Vkl)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''��������� �� ������� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.NO_vip-Byte_Signal.NO_vip)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ ������� �����'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SB_VV29_Vkl-Byte_Signal.SB_VV29_Vkl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ ��������� ������ �29 (��)'','+V+'),';
    end;

    ///�� 0 � 1
    //if (Byte_Signal_minus.VV29_AO-Byte_Signal.VV29_AO)<0 then
    //begin
    //My_Query:=My_Query
    //+'(2,'''
    //+ FormDateTime+
    //''',''���������� ������ ������ �29'','+V+'),';
    //end;

    ///�� 1 � 0
    if (Byte_Signal_minus.VV29_AO-Byte_Signal.VV29_AO)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ ������ �29'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.War_T_Lev-Byte_Signal.War_T_Lev)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� �� ����������� ��-�� ������ ��'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Err_T_Lev-Byte_Signal.Err_T_Lev)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� ����������� ��-�� ������ ��'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.War_T_Pr-Byte_Signal.War_T_Pr)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� �� ����������� ��-�� ������� ��'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Err_T_Pr-Byte_Signal.Err_T_Pr)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� ����������� ��-�� ������� ��'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Blok_VV_29-Byte_Signal.Blok_VV_29)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������������� ��������� ������ �29 (��)'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Blok_VV_29-Byte_Signal.Blok_VV_29)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''���������� ��������� ������ �29 (��)'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.VV29_Vkl_Y-Byte_Signal.VV29_Vkl_Y)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''����� ��������� ������ �29 (��)'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.VV29_Vikl_Y-Byte_Signal.VV29_Vikl_Y)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''����� ���������� ������ �29 (��)'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.VV29_Vikl_Pan-Byte_Signal.VV29_Vikl_Pan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''���������� ������ �29 (��) � ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.VV29_Vikl_Sim-Byte_Signal.VV29_Vikl_Sim)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''���������� ������ �29 (��) �� �����'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SQ_VV29_Vkl-Byte_Signal.SQ_VV29_Vkl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �29 (��) ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SQ_VV29_Vikl-Byte_Signal.SQ_VV29_Vikl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �29 (��) ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.VV27_Vkl_Y-Byte_Signal.VV27_Vkl_Y)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''����� ��������� ������ �27 (��)'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.VV27_Vikl_Y-Byte_Signal.VV27_Vikl_Y)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''����� ���������� ������ �27 (��)'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.Vkl_VV27_NU_Pan-Byte_Signal.Vkl_VV27_NU_Pan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''��������� ������ �27 (��) � ������'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.SB_Vkl_Sim_NU_27-Byte_Signal.SB_Vkl_Sim_NU_27)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''��������� ������ �27 (��) �� �����'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.Warn_Tr_NU-Byte_Signal.Warn_Tr_NU)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� �� ����������� ��-�� ��'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Error_Temp_Tr_NU-Byte_Signal.Error_Temp_Tr_NU)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� ����������� ��-�� ��'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Error_VV27_NU_I-Byte_Signal.Error_VV27_NU_I)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ ������ �27 (��)'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Blok_VV27_NU_Pan-Byte_Signal.Blok_VV27_NU_Pan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������������� ��������� ������ �27 (��)'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Blok_VV27_NU_Pan-Byte_Signal.Blok_VV27_NU_Pan)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''���������� ��������� ������ �27 (��)'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vikl_VV27_NU_Pan-Byte_Signal.Vikl_VV27_NU_Pan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''���������� ������ �27 (��) � ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SB_Vikl_Sim_NU_27-Byte_Signal.SB_Vikl_Sim_NU_27)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''���������� ������ �27 (��) �� �����'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.Basa_SB_vv43_M_vkl-Byte_Signal.Basa_SB_vv43_M_vkl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''��������� ������ �43 �������'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Err_VV43_Mot-Byte_Signal.Err_VV43_Mot)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ ������ �43 �������'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.Warn_Temp_Tr1_M-Byte_Signal.Warn_Temp_Tr1_M)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� �� ����������� ��-�� �1 �������'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Err_Temp_Tr1_M-Byte_Signal.Err_Temp_Tr1_M)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� ����������� ��-�� �1 �������'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.Warn_Temp_Tr2_M-Byte_Signal.Warn_Temp_Tr2_M)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� �� ����������� ��-�� �2 �������'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Err_Temp_Tr2_M-Byte_Signal.Err_Temp_Tr2_M)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� ����������� ��-�� �2 �������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Blok_VV43_Mot_Pan-Byte_Signal.Blok_VV43_Mot_Pan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������������� ��������� ������ �43 �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Blok_VV43_Mot_Pan-Byte_Signal.Blok_VV43_Mot_Pan)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''���������� ��������� ������ �43 �������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vkl_VV43_Mot_Q-Byte_Signal.Vkl_VV43_Mot_Q)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''����� ��������� ������ �43 �������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vikl_VV43_Mot_Pan-Byte_Signal.Vikl_VV43_Mot_Pan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''���������� ������ �43 ������� � ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SB_M_UZ5_vv43_Vikl-Byte_Signal.SB_M_UZ5_vv43_Vikl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''���������� ������ �43 ������� �� �����'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Vikl_VV43_Q-Byte_Signal.Vikl_VV43_Q)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''����� ���������� ������ �43 �������'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.Basa_SB_vv41_K_vkl-Byte_Signal.Basa_SB_vv41_K_vkl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''��������� ������ �41 �����'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Error_VV41_Kl_I-Byte_Signal.Error_VV41_Kl_I)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ ������ �41 �����'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.Warn_Temp_Tr1_UZ5_K-Byte_Signal.Warn_Temp_Tr1_UZ5_K)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� �� ����������� ��-�� �1 �����'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Err_Temp_Tr1_UZ5_K-Byte_Signal.Err_Temp_Tr1_UZ5_K)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� ����������� ��-�� �1 �����'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.Warn_Temp_Tr2_UZ5_K-Byte_Signal.Warn_Temp_Tr2_UZ5_K)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� �� ����������� ��-�� �2 �����'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Err_Temp_Tr2_UZ5_K-Byte_Signal.Err_Temp_Tr2_UZ5_K)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� ����������� ��-�� �2 �����'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Blok_VV41_Kl_Pan-Byte_Signal.Blok_VV41_Kl_Pan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������������� ��������� ������ �41 �����'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Blok_VV41_Kl_Pan-Byte_Signal.Blok_VV41_Kl_Pan)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''���������� ��������� ������ �41 �����'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vkl_VV41_Kl_Q-Byte_Signal.Vkl_VV41_Kl_Q)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''����� ��������� ������ �41 �����'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vikl_VV41_Kl_Pan-Byte_Signal.Vikl_VV41_Kl_Pan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''���������� ������ �41 ����� � ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SB_vv41_UZ5_K_vikl-Byte_Signal.SB_vv41_UZ5_K_vikl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''���������� ������ �41 ����� �� �����'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Vikl_VV41_Kl_Q-Byte_Signal.Vikl_VV41_Kl_Q)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''����� ���������� ������ �41 �����'','+V+'),';
    end;

    /////////////////////////////
    //�� 0 � 1
    if (Byte_Signal_minus.Basa_SB_vv39_R_vkl-Byte_Signal.Basa_SB_vv39_R_vkl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''��������� ������ �39 �������������'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Error_VV39_Razm_I-Byte_Signal.Error_VV39_Razm_I)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ ������ �39 �������������'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.Warn_Temp_Tr1_R-Byte_Signal.Warn_Temp_Tr1_R)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� �� ����������� ��-�� �1 �������������'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Err_Temp_Tr1_R-Byte_Signal.Err_Temp_Tr1_R)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� ����������� ��-�� �1 �������������'','+V+'),';
    end;

    //�� 0 � 1
    if (Byte_Signal_minus.Warn_Temp_Tr2_R-Byte_Signal.Warn_Temp_Tr2_R)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������������� �� ����������� ��-�� �2 �������������'','+V+'),';
    end;

    //�� 1 � 0
    if (Byte_Signal_minus.Err_Temp_Tr2_R-Byte_Signal.Err_Temp_Tr2_R)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� ����������� ��-�� �2 �������������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Blok_VV39_Razm_Pan-Byte_Signal.Blok_VV39_Razm_Pan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������������� ��������� ������ �39 �������������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Blok_VV39_Razm_Pan-Byte_Signal.Blok_VV39_Razm_Pan)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''���������� ��������� ������ �39 �������������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vkl_VV39_Razm_Q-Byte_Signal.Vkl_VV39_Razm_Q)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''����� ��������� ������ �39 �������������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vikl_VV39_Razm_Pan-Byte_Signal.Vikl_VV39_Razm_Pan)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''���������� ������ �39 ������������� � ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SB_vv39_UZ5_R_vikl-Byte_Signal.SB_vv39_UZ5_R_vikl)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''���������� ������ �39 ������������� �� �����'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Vikl_VV39_Razm_Q-Byte_Signal.Vikl_VV39_Razm_Q)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''����� ���������� ������ �39 �������������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.RDSH_VAT_R-Byte_Signal.RDSH_VAT_R)<0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''��� ���� ������������� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.RDSH_VAT_K-Byte_Signal.RDSH_VAT_K)<0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''��� ���� ����� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.RDSH_VAT_M-Byte_Signal.RDSH_VAT_M)<0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''��� ���� ������� ���������'','+V+'),';
    end;


    ///�� 0 � 1
    if (Byte_Signal_minus.Vkl_VV27_NU_I-Byte_Signal.Vkl_VV27_NU_I)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �27 (��) ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vikl_VV27_NU_I-Byte_Signal.Vikl_VV27_NU_I)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �27 (��) ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vkl_VV39_Razm_I-Byte_Signal.Vkl_VV39_Razm_I)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �39 ������������� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vikl_VV39_Razm_I-Byte_Signal.Vikl_VV39_Razm_I)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �39 ������������� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vkl_VV41_Kl_I-Byte_Signal.Vkl_VV41_Kl_I)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �41 ����� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vikl_VV41_Kl_I-Byte_Signal.Vikl_VV41_Kl_I)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �41 ����� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vkl_VV43_Mot_I-Byte_Signal.Vkl_VV43_Mot_I)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �43 ������� ��������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Vikl_VV43_Mot_I-Byte_Signal.Vikl_VV43_Mot_I)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''������ �43 ������� ���������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.Davl_Red_Mot_EKM-Byte_Signal.Davl_Red_Mot_EKM)<0 then
    begin
      My_Query:=My_Query
      +'(2,'''
      + FormDateTime+
      ''',''�������� ������ �� ������� (���) ����������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.Davl_Red_Mot_EKM-Byte_Signal.Davl_Red_Mot_EKM)>0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''�������� ������ �� ������� (���) ������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SB_AO_R_UZ4-Byte_Signal.SB_AO_R_UZ4)<0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� �� ����� �������������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SB_AO_K_UZ4-Byte_Signal.SB_AO_K_UZ4)<0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� �� ����� �����'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.SB_AO_M_UZ4-Byte_Signal.SB_AO_M_UZ4)<0 then
    begin
      My_Query:=My_Query
      +'(3,'''
      + FormDateTime+
      ''',''������ �� �� ����� �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.FO_vspommex_Mot-Byte_Signal.FO_vspommex_Mot)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''�� �� ��������������� �������'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.FO_vspommex_razm-Byte_Signal.FO_vspommex_razm)>0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''�� �� ��������������� �������������'','+V+'),';
    end;

    ///�� 0 � 1
    if (Byte_Signal_minus.KIP_Termopari-Byte_Signal.KIP_Termopari)<0 then
    begin
      My_Query:=My_Query
      +'(7,'''
      + FormDateTime+
      ''',''������� ����������� (���������)'','+V+'),';
    end;

    ///�� 1 � 0
    if (Byte_Signal_minus.KIP_Termopari-Byte_Signal.KIP_Termopari)>0 then
    begin
      My_Query:=My_Query
      +'(1,'''
      + FormDateTime+
      ''',''���������� ����������� (���������)'','+V+'),';
    end;





    //--------------------------------------------------------------------------
    //--------------------C������ ������ ������� ��� ������ � ��
    //--------------------------------------------------------------------------

    if My_Query<>'insert into '+BaseName+' values' then
    begin
       Delete(My_Query,Length(My_Query),1); //���� ������� ������ �� ������ ���
       My_Query:=My_Query+';';

      //----------------------
      //SQL ������ �� �������
      // showmessage(My_Query);
      // writeLn(f,My_Query);
      //----------------------

       //----------------------------------------------------------------------
       //�������� ������� ������� ��1251
       res_client:=mysql_query(my_con_m,'SET NAMES cp1251');
       if res_client<>0 then
       begin
          Showmessage('������� ������� �� ��������');
       end;
       //----------------------------------------------------------------------

       res:=mysql_real_query(my_con_m,PChar(My_Query),StrLen(PChar(My_Query)));

      {
      //--------------------------------
      //���� 0 �� ������ �������� ������
        if res<>0 then
        begin
//           showmessage('not write, error='+intToStr(res));
           str_error:=FormDateTime+'-������ � ������� ���������(mess) �� ��������, error='+IntToStr(mysql_errno(my_con_m))+' ��������='+mysql_error(my_con_m);
           writeLn(f,str_error);
           showmessage(str_error);
//           showmessage(IntToStr(mysql_errno(my_con_m)));
//           showmessage(mysql_error(my_con_m));
        end
        else
        begin
//           writeLn(f,'write');
        end;        //-------------------------------
       }

          //--------------------------------------------------------------------
          //E��� ��������� ������� ������ ������ ��:
          //   - � ������=1146 (��� ����� �������) ��� ����� ������� �� ������� �������
          //   - � ��������� ������ ������� ��� ������ �� ����� � ���������� ��� � ����

          if res<>0 then
          begin
             if mysql_errno(my_con_m)=1146 then
             begin
                    My_Query:='create table '+BaseName+' like stan_mess_temp';
                    mysql_real_query(my_con_m,PChar(My_Query),StrLen(PChar(My_Query)));
             end
             else
             begin
               // writeLn(f,'mysql_real_connect-Mess');
               // ShowMessage('������ Mess �� ���������!');
                str_error:=FormDateTime+'-������ � ������� ���������(mess) �� ��������, error='+IntToStr(mysql_errno(my_con_m))+' ��������='+mysql_error(my_con_m);
                writeLn(f,str_error);
                showmessage(str_error);
             end;

          end;



    end;
    {
    if res<>0 then
    begin
       My_Query:='create table '+BaseName+' like stan_mess_temp';
       mysql_real_query(my_con_m,PChar(My_Query),StrLen(PChar(My_Query)));
    end;
    }

    BaseDel:='stan_mess_'+FormatDateTime('yyyy_mm_dd',(DaySignal));
// 2015-11-02 12:09 ��������� �������� ������ ������� ���������
//    My_Query:='drop table if exists '+BaseDel;
//    mysql_real_query(my_con_m,PChar(My_Query),
//                      StrLen(PChar(My_Query)));
    Byte_Signal_minus:=Byte_Signal;
  end;
end;

begin
 AssignFile(f,'c:\logDres.txt');
 {$I-}
 Append(f);
 if IOResult<>0 then
  begin
  {$I-}
  ReWrite(f);
  {$I+};
  if IOResult<>0 then
    begin
      ShowMessage('������ �����');
      exit;
    end;
  end;

 Work_Time_Start:='';
 Work_Time_Stop:='';
 //-----------------------------------------------------------------------------
 DecimalSeparator:= '.'; //�� ����� ������� ����� �����
 //-----------------------------------------------------------------------------
 InitializeCriticalSection(CS);
 InitializeCriticalSection(CS_mess);
 InitializeCriticalSection(CS_1s);
 h_MEC:=beginthread(nil, 1024, @read_MEC, nil, 0, th_MEC);
 h_sql:=beginthread(nil, 1024, @write_sql, nil, 0, th_sql);
 h_mess:=beginthread(nil, 1024, @write_mess, nil, 0, th_mess);
 h_1s:=beginthread(nil, 1024, @write_1s, nil, 0, th_1s);
 FreeConsole;
 while true do begin
sleep(5000);
 end;

 CloseFile(f);
end.
