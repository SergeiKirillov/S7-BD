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
 chema,emuls,regim,predel,polosa5,trazm,tmot,
 Vent_101G_m,Vent_102G_m,Vent_103G_m,Vent_105G_m,Vent_106G_m,
 Vent_podpor_PA1_m,Vent_112G_m,Vent_111G_m,Vent_110G_m,
 Vent_108G_m,Vent_107G_m,Vent_podpor_PA2_m,Vent_1kl_m,
 Vent_2kl_m,Vent_3kl_m,Vent_4kl_m,Vent_5kl_m,Vent_mot_m,
 Vent_podpor_GP1_m,Vent_podpor_GP2_m,Vent_NV_m:String;
 str_error:string;

begin
     my_con_m:=mysql_init(nil);
     My_conn_m:=mysql_real_connect(my_con_m,PChar('localhost'),
              PChar('termo'),PChar('1234567890'),PChar('standb'),
              3306,PChar('3306'),0);
     mysql_select_db(my_con_m,PChar('standb'));
     Byte_Col_mess:=SizeOf(buffer_m);
     rc_rm:=false;
     ViewCount:=0;
    while true do begin
        sleep(200);

        // 26.03.2015 9:57:20 -- �� read_S400
        //1-critical_buffer_plc(buffer_s400,buffer_19);
        //2-
        //    buffer_mm[0]:=buffer_s400[33];
        //    buffer_mm[1]:=buffer_s400[34];
        //    buffer_mm[2]:=buffer_s400[35];
        //    buffer_mm[3]:=buffer_s400[51];
        //    buffer_mm[4]:=buffer_s400[52];
        //    buffer_mm[5]:=buffer_s400[53];
        //    buffer_mm[6]:=buffer_s400[54];
        //    buffer_mm[7]:=buffer_s400[55];
        //    buffer_mm[8]:=buffer_s400[3];
        //    buffer_mm[9]:=buffer_s400[156];
        //    buffer_mm[10]:=buffer_s400[155];
        //3-critical_buffer_mess(buffer_mess,buffer_mm);
        //4-critical_buffer_mess(buffer_m,buffer_mess);
        //    |
        //    |
        //   \/
        critical_buffer_mess(buffer_m,buffer_mess);


         TekTime:=Now;
         DaySignal:=TekTime-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,0,0,0)-EncodeTime(12,0,0,0)
         -EncodeTime(12,20,0,0);
         BaseName:='stan_mess_'+FormatDateTime('yyyy_mm_dd',(TekTime));
         FormDateTime:=FormatDateTime('yyyy-mm-dd hh:nn:ss.zzz',(TekTime));

         //--------------------------------------------------------------------
         //Swap - ������������ ������� ������� � ������� ����
         //������� Ord ���������� ������������� �������� ��� ������ ������������� ���� Arg. Ord(True)=1
         // --------------------------------------------------------------------
         //     1.���������� ������������ � buffer_m[]. Swap(buffer_m[])
         //     2.AND �������� � ������. Swap(buffer_m[0]) and 1=1
         //     3.���������� True � 1, � False � 0() Ord(Swap(buffer_m[0]) and 1=1);
         //--------------------------------------------------------------------
         //mb3067(m151.0-m151.7)
         Byte_Signal.RegTD:=Ord(Swap(buffer_m[0]) and 1=1);
         Byte_Signal.RegRazg:=Ord(Swap(buffer_m[0])and 2=2);
         Byte_Signal.RegNO:=Ord(Swap(buffer_m[0])and 4=4);
         Byte_Signal.RegFO:=Ord(Swap(buffer_m[0])and 8=8);
         Byte_Signal.Vip:=Ord(Swap(buffer_m[0])and 16=16);
         Byte_Signal.T10:=Ord(Swap(buffer_m[0]) and 32=32);
         Byte_Signal.T20:=Ord(Swap(buffer_m[0])and 64=64);
         Byte_Signal.T30:=Ord(Swap(buffer_m[0])and 128=128);
         //mb3066(m150.0-m150.7)
         Byte_Signal.ZS:=Ord(Swap(buffer_m[0])and 256=256);
         Byte_Signal.TD:=Ord(Swap(buffer_m[0])and 512=512);
         Byte_Signal.RS:=Ord(Swap(buffer_m[0])and 1024=1024);
         Byte_Signal.NO:=Ord(Swap(buffer_m[0])and 2048=2048);
         Byte_Signal.FO:=Ord(Swap(buffer_m[0])and 4096=4096);
         Byte_Signal.MaxSpeedPeregr:=Ord(Swap(buffer_m[0])and 8192=8192);
         Byte_Signal.UstavkaSpeed:=Ord(Swap(buffer_m[0])and 16384=16384);
         Byte_Signal.MaxSpeed:=Ord(Swap(buffer_m[0])and 32768=32768);
         //mb3069(m153.0-m153.7)
         Byte_Signal.LKmot:=Ord(Swap(buffer_m[1])and 1=1);          //LKmot
         Byte_Signal.LKrazm:=Ord(Swap(buffer_m[1])and 2=2);          //LKrazm
         Byte_Signal.Got_64kg:=Ord(Swap(buffer_m[1])and 4=4);
         Byte_Signal.Rnz12:=Ord(Swap(buffer_m[1])and 8=8);
         Byte_Signal.Rnz23:=Ord(Swap(buffer_m[1])and 16=16);
         Byte_Signal.Rnz34:=Ord(Swap(buffer_m[1])and 32=32);
         Byte_Signal.GrtVkl:=Ord(Swap(buffer_m[1])and 64=64);
         Byte_Signal.TrtVkl:=Ord(Swap(buffer_m[1])and 128=128);
         //mb3068(m152.0-m152.7)
         Byte_Signal.T40:=Ord(Swap(buffer_m[1]) and 256=256);
         Byte_Signal.Tmot:=Ord(Swap(buffer_m[1]) and 512=512);
         Byte_Signal.Trazm:=Ord(Swap(buffer_m[1]) and 1024=1024);
         Byte_Signal.LK1:=Ord(Swap(buffer_m[1]) and 2048=2048);   //LK1
         Byte_Signal.LK2:=Ord(Swap(buffer_m[1]) and 4096=4096);   //LK2
         Byte_Signal.LK3:=Ord(Swap(buffer_m[1]) and 8192=8192);   //LK3
         Byte_Signal.LK4:=Ord(Swap(buffer_m[1]) and 16384=16384); //LK4
         Byte_Signal.LK5:=Ord(Swap(buffer_m[1]) and 32768=32768); //LK5
         //mb3071(m155.0-m155.7)
         Byte_Signal.NalPol:=Ord(Swap(buffer_m[2]) and 1=1);
         Byte_Signal.Knp:=Ord(Swap(buffer_m[2]) and 2=2);
         Byte_Signal.GotStan:=Ord(Swap(buffer_m[2]) and 4=4);
         Byte_Signal.MaxV1:=Ord(Swap(buffer_m[2]) and 8=8);
         Byte_Signal.MaxV2:=Ord(Swap(buffer_m[2]) and 16=16);
         Byte_Signal.MaxV3:=Ord(Swap(buffer_m[2]) and 32=32);
         Byte_Signal.MaxV4:=Ord(Swap(buffer_m[2]) and 64=64);
         Byte_Signal.MaxV5:=Ord(Swap(buffer_m[2]) and 128=128);
         //mb3070(m154.0-m154.7)
         Byte_Signal.RKDVVkl:=Ord(Swap(buffer_m[2]) and 256=256);
         Byte_Signal.RpvVkl:=Ord(Swap(buffer_m[2]) and 512=512);
         Byte_Signal.Rnv12:=Ord(Swap(buffer_m[2]) and 1024=1024);
         Byte_Signal.Rnv23:=Ord(Swap(buffer_m[2]) and 2048=2048);
         Byte_Signal.Rnv34:=Ord(Swap(buffer_m[2]) and 4096=4096);
         Byte_Signal.Rnz45:=Ord(Swap(buffer_m[2]) and 8192=8192);
         Byte_Signal.Rtv:=Ord(Swap(buffer_m[2]) and 16384=16384);
         Byte_Signal.Got_100kg:=Ord(Swap(buffer_m[2]) and 32768=32768);
         //m3103.0-m3103.7
         Byte_Signal.g12:=Ord(Swap(buffer_m[3]) and 1=1);
         Byte_Signal.g13:=Ord(Swap(buffer_m[3])and 2=2);
         Byte_Signal.g14:=Ord(Swap(buffer_m[3])and 4=4);
         Byte_Signal.g15:=Ord(Swap(buffer_m[3])and 8=8);
         Byte_Signal.g16:=Ord(Swap(buffer_m[3])and 16=16);
         Byte_Signal.NatshUsl:=Ord(Swap(buffer_m[3])and 32=32);
         Byte_Signal.GotEmuls:=Ord(Swap(buffer_m[3])and 64=64);
         Byte_Signal.g17:=Ord(Swap(buffer_m[3])and 128=128);
         //m3102.0-m3102.7
         Byte_Signal.g18:=Ord(Swap(buffer_m[3])and 256=256);
         Byte_Signal.g19:=Ord(Swap(buffer_m[3])and 512=512);
         Byte_Signal.g20:=Ord(Swap(buffer_m[3])and 1024=1024);
         Byte_Signal.temp_POU:=Ord(Swap(buffer_m[3])and 2048=2048);
         Byte_Signal.davl_redukt:=Ord(Swap(buffer_m[3])and 4096=4096);
         Byte_Signal.davl_PGT:=Ord(Swap(buffer_m[3])and 8192=8192);
         Byte_Signal.temp_privod:=Ord(Swap(buffer_m[3])and 16384=16384);
         Byte_Signal.got_sinhr:=Ord(Swap(buffer_m[3])and 32768=32768);
         //m3105.0-m3105.7
         Byte_Signal.OgragdMot:=Ord(Swap(buffer_m[4])and 1=1);
         Byte_Signal.ZaxlestOtMot:=Ord(Swap(buffer_m[4])and 2=2);
         Byte_Signal.NOTempGP:=Ord(Swap(buffer_m[4])and 4=4);//bpv
         Byte_Signal.Peregr1:=Ord(Swap(buffer_m[4])and 8=8);
         Byte_Signal.Peregr2:=Ord(Swap(buffer_m[4])and 16=16);
         Byte_Signal.Peregr3:=Ord(Swap(buffer_m[4])and 32=32);
         Byte_Signal.Peregr4:=Ord(Swap(buffer_m[4])and 64=64);
         Byte_Signal.Peregr5:=Ord(Swap(buffer_m[4])and 128=128);
         //m3104.0-m3104.7
         Byte_Signal.NOSinxr:=Ord(Swap(buffer_m[4])and 256=256);
         Byte_Signal.NOPanPultStar:=Ord(Swap(buffer_m[4])and 512=512);
         Byte_Signal.NOPURazm:=Ord(Swap(buffer_m[4])and 1024=1024);
         Byte_Signal.NOPU1:=Ord(Swap(buffer_m[4])and 2048=2048);
         Byte_Signal.NOPU2:=Ord(Swap(buffer_m[4])and 4096=4096);
         Byte_Signal.NOPU3:=Ord(Swap(buffer_m[4])and 8192=8192);
         Byte_Signal.NOPU4:=Ord(Swap(buffer_m[4])and 16384=16384);
         Byte_Signal.NOPU5:=Ord(Swap(buffer_m[4])and 32768=32768);
         //m3107.0-m3107.7
         Byte_Signal.FOPanPultStar:=Ord(Swap(buffer_m[5])and 1=1);
         Byte_Signal.FOPU5:=Ord(Swap(buffer_m[5])and 2=2);
         Byte_Signal.AOPUR:=Ord(Swap(buffer_m[5])and 4=4);
         Byte_Signal.TrazmProval:=Ord(Swap(buffer_m[5])and 8=8);
         Byte_Signal.T12proval:=Ord(Swap(buffer_m[5])and 16=16);
         Byte_Signal.T23proval:=Ord(Swap(buffer_m[5])and 32=32);
         Byte_Signal.T34proval:=Ord(Swap(buffer_m[5])and 64=64);
         Byte_Signal.T45proval:=Ord(Swap(buffer_m[5])and 128=128);
         //m3106.0-m3106.7
         Byte_Signal.Vent_101G:=Ord(Swap(buffer_m[5])and 256=256);
         Byte_Signal.Vent_102G:=Ord(Swap(buffer_m[5])and 512=512);
         Byte_Signal.Vent_103G:=Ord(Swap(buffer_m[5])and 1024=1024);
         Byte_Signal.Vent_105G:=Ord(Swap(buffer_m[5])and 2048=2048);
         Byte_Signal.Vent_106G:=Ord(Swap(buffer_m[5])and 4096=4096);
         Byte_Signal.Vent_podpor_PA1:=Ord(Swap(buffer_m[5])and 8192=8192);
         Byte_Signal.Vent_112G:=Ord(Swap(buffer_m[5])and 16384=16384);
         Byte_Signal.Vent_111G:=Ord(Swap(buffer_m[5])and 32768=32768);
         //m3109.0-m3109.7
         Byte_Signal.Vent_110G:=Ord(Swap(buffer_m[6])and 1=1);
         Byte_Signal.Vent_108G:=Ord(Swap(buffer_m[6])and 2=2);
         Byte_Signal.Vent_107G:=Ord(Swap(buffer_m[6])and 4=4);
         Byte_Signal.Vent_podpor_PA2:=Ord(Swap(buffer_m[6])and 8=8);
         Byte_Signal.Vent_1kl:=Ord(Swap(buffer_m[6])and 16=16);
         Byte_Signal.Vent_2kl:=Ord(Swap(buffer_m[6])and 32=32);
         Byte_Signal.Vent_3kl:=Ord(Swap(buffer_m[6])and 64=64);
         Byte_Signal.Vent_4kl:=Ord(Swap(buffer_m[6])and 128=128);
         //m3108.0-m3108.7
         Byte_Signal.FOPU4:=Ord(Swap(buffer_m[6])and 256=256);
         Byte_Signal.FOPU3:=Ord(Swap(buffer_m[6])and 512=512);
         Byte_Signal.FOPU2:=Ord(Swap(buffer_m[6])and 1024=1024);
         Byte_Signal.FOPU1:=Ord(Swap(buffer_m[6])and 2048=2048);
         Byte_Signal.FOPUR:=Ord(Swap(buffer_m[6])and 4096=4096);
         Byte_Signal.AOSUSknopka:=Ord(Swap(buffer_m[6])and 8192=8192);
         Byte_Signal.AO5Klet:=Ord(Swap(buffer_m[6])and 16384=16384);
         //Byte_Signal.OgrRt3nv:=Ord(Swap(buffer_m[6])and 32768=32768);
         //m3111.0-m3111.7
         //Byte_Signal.OgrRdn3vv:=Ord(Swap(buffer_m[7])and 1=1);
         //Byte_Signal.Ig140pr3vv:=Ord(Swap(buffer_m[7])and 2=2);
         Byte_Signal.KnAOsus:=Ord(Swap(buffer_m[7])and 4=4);
         Byte_Signal.SmGot:=Ord(Swap(buffer_m[7])and 8=8);
         Byte_Signal.KteRazm:=Ord(Swap(buffer_m[7])and 16=16);
         Byte_Signal.AOrele:=Ord(Swap(buffer_m[7])and 32=32);
         Byte_Signal.FOknop:=Ord(Swap(buffer_m[7])and 64=64);
         //Byte_Signal.R56:=Ord(Swap(buffer[7])and 128=128);
         //m3110.0-m3110.7
         Byte_Signal.Vent_5kl:=Ord(Swap(buffer_m[7])and 256=256);
         Byte_Signal.Vent_mot:=Ord(Swap(buffer_m[7])and 512=512);
         Byte_Signal.Vent_podpor_GP1:=Ord(Swap(buffer_m[7])and 1024=1024);
         Byte_Signal.Vent_podpor_GP2:=Ord(Swap(buffer_m[7])and 2048=2048);
         Byte_Signal.Vent_NV:=Ord(Swap(buffer_m[7])and 4096=4096);
         Byte_Signal.Dv2pr09sec3vv:=Ord(Swap(buffer_m[7])and 8192=8192);
         //Byte_Signal.OgrRsRt3vv:=Ord(Swap(buffer_m[7])and 16384=16384);
         //Byte_Signal.OgrRt3vv:=Ord(Swap(buffer_m[7])and 32768=32768);

        //������ � ������� ������ ����� ������
        //1 ������=sleep200
        //5*200=1000->1c
        //����� ������� ���������� � 0
        if ViewCount=5 then
        begin
          chema:='���������';
          if Byte_Signal.GotStan>0 then chema:='�������';

          emuls:='�� ������';
          if Byte_Signal.GotEmuls>0 then emuls:='������';

          regim:='������������� �������';
          if Byte_Signal.RegRazg>0 then regim:='������';

          if Byte_Signal.RegTD>0 then regim:='��� �������';

          if Byte_Signal.RegNO>0 then regim:='���������� �������';

          predel:='���� ��������';
          if Byte_Signal.Vip>0 then predel:='������';

          if Byte_Signal.UstavkaSpeed>0 then predel:='������� ��������';

          if Byte_Signal.MaxSpeed>0 then predel:='������������ ��������';

          if Byte_Signal.MaxSpeedPeregr>0 then predel:='�������� �� ��������';

          polosa5:='���';
          if Byte_Signal.NalPol>0 then polosa5:='����';

          trazm:='���';
          if Byte_Signal.Trazm>0 then trazm:='����';

          tmot:='���';
          if Byte_Signal.Tmot>0 then tmot:='����';

          Vent_101G_m:='��������';
          if Byte_Signal.Vent_101G>0 then Vent_101G_m:='�������';

          Vent_102G_m:='��������';
          if Byte_Signal.Vent_102G>0 then Vent_102G_m:='�������';

          Vent_103G_m:='��������';
          if Byte_Signal.Vent_103G>0 then Vent_103G_m:='�������';

          Vent_105G_m:='��������';
          if Byte_Signal.Vent_105G>0 then Vent_105G_m:='�������';

          Vent_106G_m:='��������';
          if Byte_Signal.Vent_106G>0 then Vent_106G_m:='�������';

          Vent_112G_m:='��������';
          if Byte_Signal.Vent_112G>0 then Vent_112G_m:='�������';

          Vent_111G_m:='��������';
          if Byte_Signal.Vent_111G>0 then Vent_111G_m:='�������';

          Vent_110G_m:='��������';
          if Byte_Signal.Vent_110G>0 then Vent_110G_m:='�������';

          Vent_108G_m:='��������';
          if Byte_Signal.Vent_108G>0 then Vent_108G_m:='�������';

          Vent_107G_m:='��������';
          if Byte_Signal.Vent_107G>0 then Vent_107G_m:='�������';

          Vent_podpor_PA1_m:='��������';
          if Byte_Signal.Vent_podpor_PA1>0 then Vent_podpor_PA1_m:='�������';

          Vent_podpor_PA2_m:='��������';
          if Byte_Signal.Vent_podpor_PA2>0 then Vent_podpor_PA2_m:='�������';

          Vent_1kl_m:='��������';
          if Byte_Signal.Vent_1kl>0 then Vent_1kl_m:='�������';

          Vent_2kl_m:='��������';
          if Byte_Signal.Vent_2kl>0 then Vent_2kl_m:='�������';

          Vent_3kl_m:='��������';
          if Byte_Signal.Vent_3kl>0 then Vent_3kl_m:='�������';

          Vent_4kl_m:='��������';
          if Byte_Signal.Vent_4kl>0 then Vent_4kl_m:='�������';

          Vent_5kl_m:='��������';
          if Byte_Signal.Vent_5kl>0 then Vent_5kl_m:='�������';

          Vent_mot_m:='��������';
          if Byte_Signal.Vent_mot>0 then Vent_mot_m:='�������';

          Vent_podpor_GP1_m:='��������';
          if Byte_Signal.Vent_podpor_GP1>0 then Vent_podpor_GP1_m:='�������';

          Vent_podpor_GP2_m:='��������';
          if Byte_Signal.Vent_podpor_GP2>0 then Vent_podpor_GP2_m:='�������';

          Vent_NV_m:='��������';
          if Byte_Signal.Vent_NV>0 then Vent_NV_m:='�������';

          My_Query:='truncate wiev;';
           mysql_real_query(my_con_m,PChar(My_Query),StrLen(PChar(My_Query)));
           My_Query:='insert wiev values ('
           +''''+chema+''','''
           +emuls+''','''
           +regim+''','''
           +predel+''','''
           +polosa5+''','''
           +trazm+''','''
           +tmot+''','''
           +Vent_101G_m+''','''
           +Vent_102G_m+''','''
           +Vent_103G_m+''','''
           +Vent_105G_m+''','''
           +Vent_106G_m+''','''
           +Vent_podpor_PA1_m+''','''
           +Vent_112G_m+''','''
           +Vent_111G_m+''','''
           +Vent_110G_m+''','''
           +Vent_108G_m+''','''
           +Vent_107G_m+''','''
           +Vent_podpor_PA2_m+''','''
           +Vent_1kl_m+''','''
           +Vent_2kl_m+''','''
           +Vent_3kl_m+''','''
           +Vent_4kl_m+''','''
           +Vent_5kl_m+''','''
           +Vent_mot_m+''','''
           +Vent_podpor_GP1_m+''','''
           +Vent_podpor_GP2_m+''','''
           +Vent_NV_m+''','''
           +FloatToStr(buffer_m[9])+''');';
           mysql_real_query(my_con_m,PChar(My_Query),
                            StrLen(PChar(My_Query)));
        //����� ������� ���������� � 0
          ViewCount:=0;
        end;
        ViewCount:=ViewCount+1;
        V:=IntToStr(buffer_m[8]);
        My_Query:=
        'insert into '+BaseName+' values';

        //----------------------------------------------------------------------
        // (Byte_Signal_minus.TD-Byte_Signal.TD)<0
        //���� ���������� ������ ���� ������� ������ ������ 0 �� �������
        //��������� �� 0 � 1
        //----------------------------------------------------------------------

        //��������� �� 0 � 1
        if (Byte_Signal_minus.TD-Byte_Signal.TD)<0 then
        begin
          My_Query:=My_Query
          +'(1,'''
          + FormDateTime+
          ''',''���� ��� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.ZS-Byte_Signal.ZS)<0 then
        begin
          My_Query:=My_Query
          +'(2,'''
          +FormDateTime+
          ''',''������ ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.RS-Byte_Signal.RS)<0 then
        begin
          My_Query:= My_Query
          +'(2,'''
          +FormDateTime+
          ''',''���� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.NO-Byte_Signal.NO)<0 then
        begin
          My_Query:= My_Query
          +'(3,'''+
           FormDateTime+
           ''',''������ ���������� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.FO-Byte_Signal.FO)<0 then
        begin
          My_Query:= My_Query
          +'(4,'''+
           FormDateTime+
           ''',''������ ������������� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.RegTD-Byte_Signal.RegTD)<0 then
        begin
          My_Query:= My_Query
          +'(1,'''+
           FormDateTime+
           ''',''����� ��� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.RegRazg-Byte_Signal.RegRazg)<0 then
        begin
          My_Query:= My_Query
          +'(2,'''+
           FormDateTime+
           ''',''����� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.RegNO-Byte_Signal.RegNO)<0 then
        begin
          My_Query:=My_Query
          +'(3,'''+
           FormDateTime+
           ''',''����� ����������� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.RegFO-Byte_Signal.RegFO)<0 then
        begin
          My_Query:=My_Query
          +'(4,'''+
           FormDateTime+
           ''',''����� �������������� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Vip-Byte_Signal.Vip)<0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.Trazm-Byte_Signal.Trazm)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��������� �� �������������'','+V+'),';

        end;

        //��������� �� 1 � 0
        if (Byte_Signal_minus.Trazm-Byte_Signal.Trazm)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''���������� ��������� �� �������������'','+V+'),';

        end;

        if (Byte_Signal_minus.T10-Byte_Signal.T10)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��������� � 1 ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.T10-Byte_Signal.T10)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''���������� ��������� � 1 ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.T20-Byte_Signal.T20)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��������� �� 2 ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.T20-Byte_Signal.T20)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''���������� ��������� �� 2 ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.T30-Byte_Signal.T30)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��������� � 3 ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.T30-Byte_Signal.T30)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''���������� ��������� � 3 ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.T40-Byte_Signal.T40)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��������� � 4 ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.T40-Byte_Signal.T40)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''���������� ��������� � 4 ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.Tmot-Byte_Signal.Tmot)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��������� �� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.Tmot-Byte_Signal.Tmot)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''���������� ��������� �� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxSpeedPeregr-Byte_Signal.MaxSpeedPeregr)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''������������ ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxSpeedPeregr-Byte_Signal.MaxSpeedPeregr)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''���������� ������������� ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.UstavkaSpeed-Byte_Signal.UstavkaSpeed)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''������� ������� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxSpeed-Byte_Signal.MaxSpeed)<0 then
        begin
        My_Query:=My_Query
        +'(2,'''+
         FormDateTime+
         ''',''�������� �� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnz12-Byte_Signal.Rnz12)>0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��� 12 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnz12-Byte_Signal.Rnz12)<0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''��� 12 ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnz23-Byte_Signal.Rnz23)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��� 23 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnz23-Byte_Signal.Rnz23)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''��� 23 ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnz34-Byte_Signal.Rnz34)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��� 34 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnz34-Byte_Signal.Rnz34)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''��� 34 ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnz45-Byte_Signal.Rnz45)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''�� 45 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnz45-Byte_Signal.Rnz45)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''�� 45 ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.GrtVkl-Byte_Signal.GrtVkl)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.GrtVkl-Byte_Signal.GrtVkl)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''��� ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.TrtVkl-Byte_Signal.TrtVkl)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.TrtVkl-Byte_Signal.TrtVkl)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''��� ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.NalPol-Byte_Signal.NalPol)<0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''������� ������ � ����������� �� 5 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.NalPol-Byte_Signal.NalPol)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''���������� ������ � ����������� �� 5 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.Knp-Byte_Signal.Knp)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''���� ������� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Knp-Byte_Signal.Knp)>0 then
        begin
        My_Query:=My_Query
        +'(2,'''+
         FormDateTime+
         ''',''�������'','+V+'),';

        end;

        if (Byte_Signal_minus.GotStan-Byte_Signal.GotStan)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''������ ����� �����'','+V+'),';

        end;

        if (Byte_Signal_minus.GotStan-Byte_Signal.GotStan)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ ����� �����'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxV1-Byte_Signal.MaxV1)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������������ �������� ����� 1'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxV1-Byte_Signal.MaxV1)>0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� ������������ �������� ����� 1'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxV2-Byte_Signal.MaxV2)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������������ �������� ����� 2'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxV2-Byte_Signal.MaxV2)>0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� ������������ �������� ����� 2'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxV3-Byte_Signal.MaxV3)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������������ �������� ����� 3'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxV3-Byte_Signal.MaxV3)>0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� ������������ �������� ����� 3'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxV4-Byte_Signal.MaxV4)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������������ �������� ����� 4'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxV4-Byte_Signal.MaxV4)>0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� ������������ �������� ����� 4'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxV5-Byte_Signal.MaxV5)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������������ �������� ����� 5'','+V+'),';

        end;

        if (Byte_Signal_minus.MaxV5-Byte_Signal.MaxV5)>0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� ������������ �������� ����� 5'','+V+'),';

        end;

        if (Byte_Signal_minus.RKDVVkl-Byte_Signal.RKDVVkl)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''���� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.RKDVVkl-Byte_Signal.RKDVVkl)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''���� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.RpvVkl-Byte_Signal.RpvVkl)<0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''��� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.RpvVkl-Byte_Signal.RpvVkl)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''��� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnv12-Byte_Signal.Rnv12)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''���12 �������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnv12-Byte_Signal.Rnv12)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���12 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnv23-Byte_Signal.Rnv23)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''���23 �������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnv23-Byte_Signal.Rnv23)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���23 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnv34-Byte_Signal.Rnv34)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''���34 �������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnv34-Byte_Signal.Rnv34)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���34 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnz45-Byte_Signal.Rnz45)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''��45 �������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rnz45-Byte_Signal.Rnz45)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''��45 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rtv-Byte_Signal.Rtv)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''��� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.Rtv-Byte_Signal.Rtv)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''��� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Got_64kg-Byte_Signal.Got_64kg)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''���������� 64 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.Got_64kg-Byte_Signal.Got_64kg)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� 64 �� �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.Got_100kg-Byte_Signal.Got_100kg)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''���������� 100 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.Got_100kg-Byte_Signal.Got_100kg)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� 100 �� �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g12-Byte_Signal.g12)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''��� �-12 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g12-Byte_Signal.g12)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''��� �-12 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.GotEmuls-Byte_Signal.GotEmuls)<0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''������������ ������� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.GotEmuls-Byte_Signal.GotEmuls)>0 then
        begin
        My_Query:=My_Query
        +'(7,'''+
         FormDateTime+
         ''',''������������ ������� �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.NatshUsl-Byte_Signal.NatshUsl)<0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''��������� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.g13-Byte_Signal.g13)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''��� �-13 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g13-Byte_Signal.g13)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''��� �-13 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g14-Byte_Signal.g14)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''��� �-14 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g14-Byte_Signal.g14)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''��� �-14 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g15-Byte_Signal.g15)>0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''������ �-15 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g15-Byte_Signal.g15)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �-15 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.LKmot-Byte_Signal.LKmot)<0 then
        begin
        My_Query:=My_Query
        +'(2,'''+
         FormDateTime+
         ''',''�� ������� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.LKmot-Byte_Signal.LKmot)>0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''�� ������� ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.g16-Byte_Signal.g16)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''������ �-16 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g16-Byte_Signal.g16)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �-16 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g17-Byte_Signal.g17)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''������ �-17 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g17-Byte_Signal.g17)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �-17 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g18-Byte_Signal.g18)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''������ �-18 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g18-Byte_Signal.g18)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �-18 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g19-Byte_Signal.g19)>0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''������ �-19 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g19-Byte_Signal.g19)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �-19 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g20-Byte_Signal.g20)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''������ �-20 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.g20-Byte_Signal.g20)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �-20 �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.Peregr1-Byte_Signal.Peregr1)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''�������� ����� 1'','+V+'),';

        end;

        if (Byte_Signal_minus.Peregr1-Byte_Signal.Peregr1)>0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� ��������� ����� 1'','+V+'),';

        end;

        if (Byte_Signal_minus.Peregr2-Byte_Signal.Peregr2)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''�������� ����� 2'','+V+'),';

        end;

        if (Byte_Signal_minus.Peregr2-Byte_Signal.Peregr2)>0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� ��������� ����� 2'','+V+'),';

        end;

        if (Byte_Signal_minus.Peregr3-Byte_Signal.Peregr3)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''�������� ����� 3'','+V+'),';

        end;

        if (Byte_Signal_minus.Peregr3-Byte_Signal.Peregr3)>0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� ��������� ����� 3'','+V+'),';

        end;

        if (Byte_Signal_minus.Peregr4-Byte_Signal.Peregr4)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''�������� ����� 4'','+V+'),';

        end;

        if (Byte_Signal_minus.Peregr4-Byte_Signal.Peregr4)>0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� ��������� ����� 4'','+V+'),';

        end;

        if (Byte_Signal_minus.Peregr5-Byte_Signal.Peregr5)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''�������� ����� 5'','+V+'),';

        end;

        if (Byte_Signal_minus.Peregr5-Byte_Signal.Peregr5)>0 then
        begin
        My_Query:=My_Query
        +'(5,'''+
         FormDateTime+
         ''',''����� ��������� ����� 5'','+V+'),';

        end;

        if (Byte_Signal_minus.temp_POU-Byte_Signal.temp_POU)>0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''����������� � ��� ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.temp_POU-Byte_Signal.temp_POU)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''����������� � ��� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.davl_redukt-Byte_Signal.davl_redukt)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''�������� ���������� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.davl_redukt-Byte_Signal.davl_redukt)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''�������� ���������� ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.davl_PGT-Byte_Signal.davl_PGT)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''�������� ��� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.davl_PGT-Byte_Signal.davl_PGT)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''�������� ��� ����������'','+V+'),';

        end;

        if (Byte_Signal_minus.LKrazm-Byte_Signal.LKrazm)<0 then
        begin
        My_Query:=My_Query
        +'(2,'''+
         FormDateTime+
         ''',''�� ������������� ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.LKrazm-Byte_Signal.LKrazm)>0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''�� ������������� ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.temp_privod-Byte_Signal.temp_privod)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''���������� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.temp_privod-Byte_Signal.temp_privod)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.got_sinhr-Byte_Signal.got_sinhr)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''���������� ��������� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.got_sinhr-Byte_Signal.got_sinhr)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ��������� �� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.OgragdMot-Byte_Signal.OgragdMot)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''���������� ������� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.OgragdMot-Byte_Signal.OgragdMot)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������� ������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.ZaxlestOtMot-Byte_Signal.ZaxlestOtMot)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''�������������� � ������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.ZaxlestOtMot-Byte_Signal.ZaxlestOtMot)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''�������������� �������'','+V+'),';

        end;

        if (Byte_Signal_minus.NOTempGP-Byte_Signal.NOTempGP)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''������� ����������� ��� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.NOTempGP-Byte_Signal.NOTempGP)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ����������� ��� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.NOSinxr-Byte_Signal.NOSinxr)<0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''������� ����������� ��� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.NOSinxr-Byte_Signal.NOSinxr)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ����������� ��� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.NOPanPultStar-Byte_Signal.NOPanPultStar)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� �� �������� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.NOPURazm-Byte_Signal.NOPURazm)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.NOPU1-Byte_Signal.NOPU1)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��1 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.NOPU2-Byte_Signal.NOPU2)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��2 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.NOPU3-Byte_Signal.NOPU3)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��3 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.NOPU4-Byte_Signal.NOPU4)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��4 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.NOPU5-Byte_Signal.NOPU5)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��5 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.FOPanPultStar-Byte_Signal.FOPanPultStar)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� �� �������� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.FOPUR-Byte_Signal.FOPUR)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.FOPU1-Byte_Signal.FOPU1)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��1 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.FOPU2-Byte_Signal.FOPU2)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��2 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.FOPU3-Byte_Signal.FOPU3)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��3 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.FOPU4-Byte_Signal.FOPU4)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��4 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.FOPU5-Byte_Signal.FOPU5)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��5 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.AOPUR-Byte_Signal.AOPUR)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.AOSUSknopka-Byte_Signal.AOSUSknopka)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��� ������'','+V+'),';

        end;

        if (Byte_Signal_minus.AO5klet-Byte_Signal.AO5klet)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��5 ������'','+V+'),';

        end;

        if (Byte_Signal_minus.TrazmProval-Byte_Signal.TrazmProval)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ ��������� �� ������������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.TrazmProval-Byte_Signal.TrazmProval)>0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''�������������� ��������� �� ������������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.T12proval-Byte_Signal.T12proval)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ ��������� � 1 ���������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.T12proval-Byte_Signal.T12proval)>0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''�������������� ��������� � 1 ���������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.T23proval-Byte_Signal.T23proval)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ ��������� �� 2 ���������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.T23proval-Byte_Signal.T23proval)>0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''�������������� ��������� �� 2 ���������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.T34proval-Byte_Signal.T34proval)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ ��������� � 3 ���������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.T34proval-Byte_Signal.T34proval)>0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''�������������� ��������� � 3 ���������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.T45proval-Byte_Signal.T45proval)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ ��������� � 4 ���������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.T45proval-Byte_Signal.T45proval)>0 then
        begin
        My_Query:=My_Query
        +'(1,'''+
         FormDateTime+
         ''',''�������������� ��������� � 4 ���������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.LK1-Byte_Signal.LK1)<0 then
        begin
        My_Query:=My_Query
        +'(2,'''+
         FormDateTime+
         ''',''�� ����� 1 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.LK1-Byte_Signal.LK1)>0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
        ''',''�� ����� 1 ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.LK2-Byte_Signal.LK2)<0 then
        begin
        My_Query:=My_Query
        +'(2,'''+
         FormDateTime+
         ''',''�� ����� 2 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.LK2-Byte_Signal.LK2)>0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''�� ����� 2 ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.LK3-Byte_Signal.LK3)<0 then
        begin
        My_Query:=My_Query
        +'(2,'''+
         FormDateTime+
         ''',''�� ����� 3 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.LK3-Byte_Signal.LK3)>0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''�� ����� 3 ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.LK4-Byte_Signal.LK4)<0 then
        begin
        My_Query:=My_Query
        +'(2,'''+
         FormDateTime+
         ''',''�� ����� 4 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.LK4-Byte_Signal.LK4)>0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''�� ����� 4 ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.LK5-Byte_Signal.LK5)<0 then
        begin
        My_Query:=My_Query
        +'(2,'''+
         FormDateTime+
         ''',''�� ����� 5 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.LK5-Byte_Signal.LK5)>0 then
        begin
        My_Query:=My_Query
        +'(6,'''+
         FormDateTime+
         ''',''�� ����� 5 ���������'','+V+'),';

        end;

        if (Byte_Signal_minus.KnAOsus-Byte_Signal.KnAOsus)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''������ �� �� ��� ������'','+V+'),';

        end;
        //////////////////////////////////////////////////////Vent
        if (Byte_Signal_minus.Vent_101G-Byte_Signal.Vent_101G)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ 101� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_102G-Byte_Signal.Vent_102G)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ 102� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_103G-Byte_Signal.Vent_103G)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ 103� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_105G-Byte_Signal.Vent_105G)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ 105� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_106G-Byte_Signal.Vent_106G)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ 106� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_112G-Byte_Signal.Vent_112G)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ 112� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_111G-Byte_Signal.Vent_111G)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ 111� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_110G-Byte_Signal.Vent_110G)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ 110� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_108G-Byte_Signal.Vent_108G)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ 108� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_107G-Byte_Signal.Vent_107G)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ 107� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_podpor_PA1-Byte_Signal.Vent_podpor_PA1)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������� ��-1 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_podpor_PA2-Byte_Signal.Vent_podpor_PA2)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������� ��-2 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_1kl-Byte_Signal.Vent_1kl)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ �� 1 ����� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_2kl-Byte_Signal.Vent_2kl)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ �� 2 ����� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_3kl-Byte_Signal.Vent_3kl)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ �� 3 ����� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_4kl-Byte_Signal.Vent_4kl)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ �� 4 ����� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_5kl-Byte_Signal.Vent_5kl)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ �� 5 ����� �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_podpor_GP1-Byte_Signal.Vent_podpor_GP1)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������� ��-1 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_podpor_GP2-Byte_Signal.Vent_podpor_GP2)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������� ��-2 ��������'','+V+'),';

        end;

        if (Byte_Signal_minus.Vent_NV-Byte_Signal.Vent_NV)<0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''���������� ������ �������� ������ �������� ��'','+V+'),';

        end;

        if (Byte_Signal_minus.Dv2pr09sec3vv-Byte_Signal.Dv2pr09sec3vv)>0 then
        begin
        My_Query:=My_Query
        +'(4,'''+
         FormDateTime+
         ''',''��� �������� �� ���'','+V+'),';

        end;

       //----------------------------------------------------------------------
       //�������� ������� ������� ��1251
       res:=mysql_query(my_con_m,'SET NAMES cp1251');
       if res<>0 then
       begin
          Showmessage('������� ������� �� ��������');
       end;
       //----------------------------------------------------------------------



        if My_Query<>'insert into '+BaseName+' values' then
        begin
         Delete(My_Query,Length(My_Query),1);
         My_Query:=My_Query+';';
         res:=mysql_real_query(my_con_m,PChar(My_Query),StrLen(PChar(My_Query)));
        end;

            //--------------------------------------------------------------------
            //E��� ��������� ������� ������ ������ ��:
            //   - � ������=1146 (��� ����� �������) ��� ����� ������� �� ������� �������
            //   - � ��������� ������ ������� ��� ������ �� ����� � ���������� ��� � ����

            if res<>0 then
            begin
                   if mysql_errno(my_con_m)=1146 then
                   begin
                       My_Query:='create table '+BaseName+' like stan_mess_temp';
                       mysql_real_query(my_con_m,PChar(My_Query), StrLen(PChar(My_Query)));
                   end
                   else
                   begin
                      str_error:=FormDateTime+'-������ � ������� mess �� ��������, error='+IntToStr(mysql_errno(my_con_m))+' ��������='+mysql_error(my_con_m);
                      writeLn(f,str_error);
                      showmessage(str_error);

                   end;

            end;

        BaseDel:='stan_mess_'+FormatDateTime('yyyy_mm_dd',(DaySignal));
//   2015-11-02 06:45 �������� �������� ������� ���������     My_Query:='drop table if exists '+BaseDel;
//        mysql_real_query(my_con_m,PChar(My_Query), StrLen(PChar(My_Query)));
        Byte_Signal_minus:=Byte_Signal;
    end;
end;
