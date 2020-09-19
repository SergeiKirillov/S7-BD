unit RThread;

interface

uses
  Classes,Windows,SysUtils,WinSock;

type
  ReadThread = class(TThread)
   WSData:TWSAData;
 Soc,AcceptedSock:TSocket;
 Addr:TSockAddr;
 Len:integer;
 buffer:array [0..158] of SmallInt;
  protected
    procedure Execute; override;
    procedure UpdateCaption;
  end;

implementation

{ Important: Methods and properties of objects in visual components can only be
  used in a method called using Synchronize, for example,

      Synchronize(UpdateCaption);

  and UpdateCaption could look like,

    procedure ReadThread.UpdateCaption;
    begin
      Form1.Caption := 'Updated in a thread';
    end; }

{ ReadThread }

uses
 View_Form,Graphics;

procedure ReadThread.Execute;
begin
 WSAStartup($0101,WSData);
 Soc:=Socket(AF_INET,Sock_Stream,0);
 Addr.sin_family:=AF_INET;
 Addr.sin_addr.S_addr:=Inet_Addr('127.0.0.1');
 Addr.sin_port:=HtoNS(10000);
 FillChar(Addr.sin_zero,SizeOf(Addr.sin_zero),0);
 Bind(Soc,Addr,SizeOf(Addr));
 Listen(Soc,SoMaxConn);
 Len:=SizeOf(TSockAddr);
 AcceptedSock:=Accept(Soc,@Addr,@Len);
while true do begin
 sleep(0);
// RecvFrom(Soc,buffer,SizeOf(buffer),0,Addr,Len);
 Recv(AcceptedSock,buffer,SizeOf(buffer),0);
// ShutDown(Soc,SD_Both);
// CloseSocket(Soc);
 Synchronize(UpdateCaption);
end;
end;

procedure ReadThread.UpdateCaption;
    begin
if not((buffer[33] and 4096)=4096) and
    not((buffer[33] and 128)=128) and
     not((buffer[33] and 32)=32) and
      not((buffer[33] and 64)=64) and
       not((buffer[35] and 512)=512) then
      begin
Form_View.Label_Speed_Lim.Caption:='';
Form_View.Label_Speed_Lim.Font.Color:=clPurple;
      end;
if not((buffer[35] and 1024)=1024) then
begin
Form_View.Label_Shema.Caption:='Разобрана';
Form_View.Label_Shema.Font.Color:=clRed;
end;

if ((buffer[35] and 1024)=1024) then
begin
Form_View.Label_Shema.Caption:='Собрана';
Form_View.Label_Shema.Font.Color:=clGreen;
end;

if not((buffer[51] and 16384)=16384) then
begin
Form_View.Label_Emuls.Caption:='Не готова';
Form_View.Label_Emuls.Font.Color:=clRed;
end;

if ((buffer[51] and 16384)=16384) then
begin
Form_View.Label_Emuls.Caption:='Готова';
Form_View.Label_Emuls.Font.Color:=clGreen;
end;

if ((buffer[33] and 256)=256) then
begin
Form_View.Label_Regim.Caption:='Так держать';
Form_View.Label_Regim.Font.Color:=clGreen;
end;

if ((buffer[33] and 512)=512) then
begin
Form_View.Label_Regim.Caption:='Разгон';
Form_View.Label_Regim.Font.Color:=clPurple;
end;

if ((buffer[33] and 1024)=1024) then
begin
Form_View.Label_Regim.Caption:='Нормальный останов';
Form_View.Label_Regim.Font.Color:=clBlue;
end;

if ((buffer[33] and 2048)=2048) then
begin
Form_View.Label_Regim.Caption:='Форсированный останов';
Form_View.Label_Regim.Font.Color:=clRed;
end;

if ((buffer[33] and 4096)=4096) then
begin
Form_View.Label_Speed_Lim.Caption:='Выпуск полосы';
Form_View.Label_Speed_Lim.Font.Color:=clOlive;
end;

if ((buffer[33] and 32)=32) then
begin
Form_View.Label_Speed_Lim.Caption:='Неумная скорость';
Form_View.Label_Speed_Lim.Font.Color:=clRed;
end;

if ((buffer[33] and 64)=64) then
begin
Form_View.Label_Speed_Lim.Caption:='Уставка скорости';
Form_View.Label_Speed_Lim.Font.Color:=clGreen;
end;

if ((buffer[33] and 1)=1) then
begin
Form_View.Label_Speed_Lim.Caption:='Нормальная скорость';
Form_View.Label_Speed_Lim.Font.Color:=clPurple;
end;

if not((buffer[35] and 512)=512) then
begin
Form_View.Label_Speed_Lim.Caption:='Ноль скорости';
Form_View.Label_Speed_Lim.Font.Color:=clBlack;
end;

if ((buffer[35] and 256)=256) then
begin
Form_View.Label_Polosa.Caption:='Есть';
Form_View.Label_Polosa.Font.Color:=clGreen;
end;

if not((buffer[35] and 256)=256) then
begin
Form_View.Label_Polosa.Caption:='Нет';
Form_View.Label_Polosa.Font.Color:=clRed;
end;

Form_View.H1z.Caption:=FloatToStr(buffer[5]/1000);
Form_View.H5z.Caption:=FloatToStr(buffer[6]/1000);
Form_View.B.Caption:=FloatToStr(buffer[7]);
Form_View.Dvip.Caption:=FloatToStr(buffer[8]/1000);
Form_View.Rrazm.Caption:=FloatToStr(buffer[9]/1000);
Form_View.Dmot.Caption:=FloatToStr(buffer[10]/1000);
Form_View.Svip.Caption:=FloatToStr(buffer[11]/1000);
Form_View.D1.Caption:=FloatToStr(buffer[12]);
Form_View.D2.Caption:=FloatToStr(buffer[13]);
Form_View.D3.Caption:=FloatToStr(buffer[14]);
Form_View.D4.Caption:=FloatToStr(buffer[15]);
Form_View.D5.Caption:=FloatToStr(buffer[16]);
Form_View.E2.Caption:=FloatToStr(buffer[17]/100);
Form_View.E3.Caption:=FloatToStr(buffer[18]/100);
Form_View.E4.Caption:=FloatToStr(buffer[19]/100);
Form_View.E5.Caption:=FloatToStr(buffer[20]/1000);
Form_View.V1L.Caption:=FloatToStr(buffer[21]/100);
Form_View.V2L.Caption:=FloatToStr(buffer[23]/100);
Form_View.V3L.Caption:=FloatToStr(buffer[25]/100);
Form_View.V4L.Caption:=FloatToStr(buffer[27]/100);
Form_View.V5L.Caption:=FloatToStr(buffer[29]/100);
Form_View.V1p.Caption:=FloatToStr(buffer[22]/100);
Form_View.V2p.Caption:=FloatToStr(buffer[24]/100);
Form_View.V3p.Caption:=FloatToStr(buffer[26]/100);
Form_View.V4p.Caption:=FloatToStr(buffer[28]/100);
Form_View.V5p.Caption:=FloatToStr(buffer[30]/100);
Form_View.DH1.Caption:=FloatToStr(buffer[31]/100);
Form_View.DH5.Caption:=FloatToStr(buffer[32]/100);
Form_View.Urnz45.Caption:=FloatToStr(buffer[20]/1000);
Form_View.T1.Caption:=FloatToStr(buffer[36]/100);
Form_View.T2.Caption:=FloatToStr(buffer[37]/100);
Form_View.T3.Caption:=FloatToStr(buffer[38]/100);
Form_View.T4.Caption:=FloatToStr(buffer[39]/100);
Form_View.T1L.Caption:=FloatToStr(buffer[40]/100);
Form_View.T1p.Caption:=FloatToStr(buffer[44]/100);
Form_View.T2L.Caption:=FloatToStr(buffer[41]/100);
Form_View.T2p.Caption:=FloatToStr(buffer[45]/100);
Form_View.T3L.Caption:=FloatToStr(buffer[42]/100);
Form_View.T3p.Caption:=FloatToStr(buffer[46]/100);
Form_View.T4L.Caption:=FloatToStr(buffer[43]/100);
Form_View.T4p.Caption:=FloatToStr(buffer[47]/100);
Form_View.T1z.Caption:=FloatToStr(buffer[48]/100);
Form_View.T2z.Caption:=FloatToStr(buffer[49]/100);
Form_View.T3z.Caption:=FloatToStr(buffer[50]/100);
Form_View.T4z.Caption:=FloatToStr(buffer[56]/100);
Form_View.EdsR.Caption:=FloatToStr(buffer[57]/10);
Form_View.Ir.Caption:=FloatToStr(buffer[59]/10);
Form_View.V1.Caption:=FloatToStr(buffer[0]/100);
Form_View.V2.Caption:=FloatToStr(buffer[1]/100);
Form_View.V3.Caption:=FloatToStr(buffer[2]/100);
Form_View.V4.Caption:=FloatToStr(buffer[3]/100);
Form_View.V5.Caption:=FloatToStr(buffer[4]/100);
Form_View.Imotz.Caption:=FloatToStr(buffer[71]);
Form_View.OS1.Caption:=FloatToStr(buffer[60]/10);
Form_View.OS2v.Caption:=FloatToStr(buffer[61]/10);
Form_View.OS2n.Caption:=FloatToStr(buffer[62]/10);
Form_View.OS3v.Caption:=FloatToStr(buffer[63]/10);
Form_View.OS3n.Caption:=FloatToStr(buffer[64]/10);
Form_View.OS4v.Caption:=FloatToStr(buffer[65]/10);
Form_View.OS4n.Caption:=FloatToStr(buffer[66]/10);
Form_View.OS5v.Caption:=FloatToStr(buffer[67]/10);
Form_View.OS5n.Caption:=FloatToStr(buffer[68]/10);
Form_View.Imot.Caption:=FloatToStr(buffer[70]);
Form_View.Vmot.Caption:=FloatToStr(buffer[69]/10);
Form_View.U1.Caption:=FloatToStr(buffer[72]/10);
Form_View.U2v.Caption:=FloatToStr(buffer[73]/10);
Form_View.U2n.Caption:=FloatToStr(buffer[74]/10);
Form_View.U3v.Caption:=FloatToStr(buffer[75]/10);
Form_View.U3n.Caption:=FloatToStr(buffer[76]/10);
Form_View.U4v.Caption:=FloatToStr(buffer[77]/10);
Form_View.U4n.Caption:=FloatToStr(buffer[78]/10);
Form_View.U5v.Caption:=FloatToStr(buffer[79]/10);
Form_View.U5n.Caption:=FloatToStr(buffer[80]/10);
Form_View.Um.Caption:=FloatToStr(buffer[81]/10);
Form_View.I1.Caption:=FloatToStr(buffer[82]);
Form_View.I2v.Caption:=FloatToStr(buffer[83]);
Form_View.I2n.Caption:=FloatToStr(buffer[84]);
Form_View.I3v.Caption:=FloatToStr(buffer[85]);
Form_View.I3n.Caption:=FloatToStr(buffer[86]);
Form_View.I4v.Caption:=FloatToStr(buffer[87]);
Form_View.I4n.Caption:=FloatToStr(buffer[88]);
Form_View.I5v.Caption:=FloatToStr(buffer[89]);
Form_View.I5n.Caption:=FloatToStr(buffer[90]);
Form_View.Im.Caption:=FloatToStr(buffer[70]);

Form_View.Rrazm.Font.Color:=clRed;
if not((buffer[33] and 4096)=4096) then
Form_View.Rrazm.Font.Color:=clTeal;

Form_View.Dmot.Font.Color:=clGreen;
if not((buffer[34] and 2)=2) then
Form_View.Dmot.Font.Color:=clRed;

Form_View.Label_T_razm.Font.Color:=clRed;
if ((buffer[34] and 4)=4) then
Form_View.Label_T_razm.Font.Color:=clGreen;

Form_View.Urnz45.Font.Color:=clRed;
if ((buffer[35] and 32)=32) then
Form_View.Urnz45.Font.Color:=clTeal;

Form_View.Gauge_T1_sr.Progress:=
buffer[36];
Form_View.T1.Font.Color:=clRed;
if ((buffer[33] and 8192)=8192) then
Form_View.T1.Font.Color:=clBlue;

Form_View.Gauge_T2_sr.Progress:=
buffer[37];
Form_View.T2.Font.Color:=clRed;
if ((buffer[33] and 16384)=16384) then
Form_View.T2.Font.Color:=clBlue;

Form_View.Gauge_T3_sr.Progress:=
buffer[38];
Form_View.T3.Font.Color:=clRed;
if ((buffer[33] and 32768)=32768) then
Form_View.T3.Font.Color:=clBlue;

Form_View.Gauge_T4_sr.Progress:=
buffer[39];
Form_View.T4.Font.Color:=clRed;
if ((buffer[34] and 1)=1) then
Form_View.T4.Font.Color:=clBlue;

Form_View.Gauge_T1_lev.Progress:=
buffer[40];
Form_View.Gauge_T1_prav.Progress:=
buffer[44];
Form_View.Gauge_T2_lev.Progress:=
buffer[41];
Form_View.Gauge_T2_prav.Progress:=
buffer[45];
Form_View.Gauge_T3_lev.Progress:=
buffer[42];
Form_View.Gauge_T3_prav.Progress:=
buffer[46];
Form_View.Gauge_T4_lev.Progress:=
buffer[43];
Form_View.Gauge_T4_prav.Progress:=
buffer[47];

Form_View.V1.Font.Color:=clTeal;
if ((buffer[35] and 2048)=2048) then
Form_View.V1.Font.Color:=clRed;
Form_View.Gauge_Speed1.Progress:=
buffer[0];

Form_View.V2.Font.Color:=clTeal;
if ((buffer[35] and 4096)=4096) then
Form_View.V2.Font.Color:=clRed;
Form_View.Gauge_Speed2.Progress:=
buffer[1];

Form_View.V3.Font.Color:=clTeal;
if ((buffer[35] and 8192)=8192) then
Form_View.V3.Font.Color:=clRed;
Form_View.Gauge_Speed3.Progress:=
buffer[2];

Form_View.V4.Font.Color:=clTeal;
if ((buffer[35] and 16384)=16384) then
Form_View.V4.Font.Color:=clRed;
Form_View.Gauge_Speed4.Progress:=
buffer[3];

Form_View.V5.Font.Color:=clTeal;
if ((buffer[35] and 32768)=32768) then
Form_View.V5.Font.Color:=clRed;
Form_View.Gauge_Speed5.Progress:=
buffer[4];

Form_View.Gauge_Speed1_vv.Progress:=
buffer[60];
Form_View.Gauge_Speed2_vv.Progress:=
buffer[61];
Form_View.Gauge_Speed2_nv.Progress:=
buffer[62];
Form_View.Gauge_Speed3_vv.Progress:=
buffer[63];
Form_View.Gauge_Speed3_nv.Progress:=
buffer[64];
Form_View.Gauge_Ed_razm.Progress:=
buffer[57];
Form_View.Gauge_Iv_razm.Progress:=
buffer[58];

Form_View.Gauge_V_mot.Progress:=
buffer[69];
Form_View.Gauge_Izad_mot.Progress:=
buffer[71];
Form_View.Gauge_I_mot.Progress:=
buffer[70];
Form_View.Gauge_Speed4_vv.Progress:=
buffer[65];
Form_View.Gauge_Speed4_nv.Progress:=
buffer[66];
Form_View.Gauge_Speed5_vv.Progress:=
buffer[67];
Form_View.Gauge_Speed5_nv.Progress:=
buffer[68];

if  (buffer[31])<=0 then
Form_View.Gauge_dH1_minus.Progress:=1000+
buffer[31];
if  (buffer[31])>=0 then
Form_View.Gauge_dH1_minus.Progress:=1000;

if  (buffer[31])>=0 then
Form_View.Gauge_dH1_plus.Progress:=
buffer[31];
if  (buffer[31])<=0 then
Form_View.Gauge_dH1_plus.Progress:=0;

Form_View.dH1.Font.Color:=clRed;
if ((buffer[35] and 64)=64) then
Form_View.dH1.Font.Color:=clTeal;

if  (buffer[32])<=0 then
Form_View.Gauge_dH5_minus.Progress:=1000+
buffer[32];
if  (buffer[32])>=0 then
Form_View.Gauge_dH5_minus.Progress:=1000;

if  (buffer[32])>=0 then
Form_View.Gauge_dH5_plus.Progress:=
buffer[32];
if  (buffer[32])<=0 then
Form_View.Gauge_dH5_plus.Progress:=0;

Form_View.dH5.Font.Color:=clRed;
if ((buffer[34] and 32768)=32768) then
Form_View.dH5.Font.Color:=clTeal;
end;

end.
