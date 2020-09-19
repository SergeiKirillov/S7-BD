procedure write_tcp;stdcall;
var
 buffer_tcp:buffer_array;
 SS:TSocket;
 Addr:TSockAddr;
 Data:TWSAData;
 ResCon,ResConn:Integer;
begin
   WSAStartup($101,Data);                                                                           //Используя  winsock.dll подключаемся к сокетам(весия сокета и указатель на данные)
   SS:=Socket(AF_Inet,Sock_Stream,0);							//
   Addr.sin_family:=AF_Inet;
   Addr.sin_port:=HToNS(10000);
   Addr.sin_addr.S_addr:=Inet_Addr('127.0.0.1');
   FillChar(Addr.Sin_Zero,SizeOf(Addr.Sin_Zero),0);					//Заполняем массив 0
   ResCon:=5;
    while true do begin
        sleep(250);
         critical_buffer_tcp(buffer_tcp,buffer_s400);
         if ResCon<>0 then
         ResCon:=Connect(SS,Addr,SizeOf(TSockAddr));
        //  SendTo(SS,buffer_tcp,SizeOf(buffer_tcp),0,Addr,SizeOf(Addr));
         Send(SS,buffer_tcp,SizeOf(buffer_tcp),0);
        //  ShutDown(SS,SD_Both);
        //  CloseSocket(SS);
    end;
end;








implementation

uses
RThread;

{$R *.DFM}
var
    R_Thread:ReadThread;
    
procedure TForm_View.FormCreate(Sender: TObject);
begin
  R_Thread:=ReadThread.Create(False);
end;

procedure TForm_View.FormClose(Sender: TObject; var Action: TCloseAction);
begin
ShutDown(R_Thread.Soc,SD_Both);
CloseSocket(R_Thread.Soc);
end;

end.
