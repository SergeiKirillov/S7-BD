using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using HWDiag;
using System.Diagnostics.Eventing;
using LoggerInSystem;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

using System.Data.SqlClient;
using System.Data;
using System.Collections;

using System.Net;
using System.Net.Sockets;
using System.IO;


//TODO сделать класс логов в который будем отсылать все сообщения которые нужно будет фиксировать 

namespace RS2toBD
{
    public class MessageClass
    {
        public int statusMenshe { get; set;}
        public int statusBolshe { get; set; }
        public string PlusMess { get; set; }
        public string MinusMess { get; set; }

        public MessageClass(int StatusMenshe,  string MensheNul, int StatusBolshe, string BolsheNul)
        {
            this.statusMenshe = StatusMenshe;
            this.MinusMess = MensheNul;
            this.statusBolshe = StatusBolshe;
            this.PlusMess = BolsheNul;
            
        }
        
    }
    public class Message
    {
        //Dictionary<int, MessageClass> MessageStan = new Dictionary<int, MessageClass>()
        //{
        //    //byte0 -> buffer_m[0](1-128)
        //    //(Byte_Signal_minus.RegTD-Byte_Signal.RegTD)<0   1   Режим ТАК ДЕРЖАТЬ           mb3067(m151.0-m151.7)   Byte_Signal.RegTD:=Ord(Swap(buffer_m[0]) and 1=1);
        //    //(Byte_Signal_minus.RegRazg-Byte_Signal.RegRazg)<0   2   Режим РАЗГОНА           mb3067(m151.0-m151.7)   Byte_Signal.RegRazg:=Ord(Swap(buffer_m[0])and 2=2);
        //    //(Byte_Signal_minus.RegNO-Byte_Signal.RegNO)<0	3	Режим НОРМАЛЬНОГО ОСТАНОВА mb3067(m151.0-m151.7)   Byte_Signal.RegNO:=Ord(Swap(buffer_m[0]) and 4=4);
        //    //(Byte_Signal_minus.RegFO-Byte_Signal.RegFO)<0	4	Режим ФОРСИРОВАННОГО ОСТАНОВА mb3067(m151.0-m151.7)   Byte_Signal.RegFO:=Ord(Swap(buffer_m[0]) and 8=8);
        //    //(Byte_Signal_minus.Vip-Byte_Signal.Vip)<0	5	Режим ВЫПУСКА           mb3067(m151.0-m151.7)   Byte_Signal.Vip:=Ord(Swap(buffer_m[0]) and 16=16);
        //    //(Byte_Signal_minus.T10-Byte_Signal.T10)<0   6   Натяжение в 1 промежутке            mb3067(m151.0-m151.7)   Byte_Signal.T10:=Ord(Swap(buffer_m[0]) and 32=32);
        //    //(Byte_Signal_minus.T10-Byte_Signal.T10)>0	7	Отсутствие натяжения в 1 промежутке mb3067(m151.0-m151.7)   Byte_Signal.T10:=Ord(Swap(buffer_m[0]) and 32=32);
        //    //(Byte_Signal_minus.T20-Byte_Signal.T20)<0	6	Натяжение во 2 промежутке mb3067(m151.0-m151.7)   Byte_Signal.T20:=Ord(Swap(buffer_m[0]) and 64=64);
        //    //(Byte_Signal_minus.T20-Byte_Signal.T20)>0	7	Отсутствие натяжения во 2 промежутке mb3067(m151.0-m151.7)   Byte_Signal.T20:=Ord(Swap(buffer_m[0]) and 64=64);
        //    //(Byte_Signal_minus.T30-Byte_Signal.T30)<0	6	Натяжение в 3 промежутке mb3067(m151.0-m151.7)   Byte_Signal.T30:=Ord(Swap(buffer_m[0]) and 128=128);
        //    //(Byte_Signal_minus.T30-Byte_Signal.T30)>0	7	Отсутствие натяжения в 3 промежутке mb3067(m151.0-m151.7)   Byte_Signal.T30:=Ord(Swap(buffer_m[0]) and 128=128);
        //    [0]= new MessageClass(1,"Режим ТАК ДЕРЖАТЬ",0,""),
        //    [1] = new MessageClass(2, "Режим РАЗГОНА",0,""),
        //    [2] = new MessageClass(3, "Режим НОРМАЛЬНОГО ОСТАНОВА", 0,""),
        //    [3] = new MessageClass(4, "Режим ФОРСИРОВАННОГО ОСТАНОВА",0,""),
        //    [4] = new MessageClass(5, "Режим ВЫПУСКА",0,""),
        //    [5] = new MessageClass(6, "Натяжение в 1 промежутке",7, "Отсутствие натяжения в 1"),
        //    [6] = new MessageClass(6, "Натяжение в 2 промежутке", 7, "Отсутствие натяжения в 3"),
        //    [7] = new MessageClass(6, "Натяжение в 2 промежутке", 7, "Отсутствие натяжения в 3"),

        //    //byte1 -> buffer_m[0](256-32768)
        //    //(Byte_Signal_minus.ZS-Byte_Signal.ZS)<0	2	Кнопка ЗАПРАВКА			mb3066(m150.0-m150.7)	Byte_Signal.ZS:=Ord(Swap(buffer_m[0])and 256=256);
        //    //(Byte_Signal_minus.TD-Byte_Signal.TD)<0	1	Ключ ТАК ДЕРЖАТЬ			mb3066(m150.0-m150.7)	Byte_Signal.TD:=Ord(Swap(buffer_m[0])and 512=512);
        //    //(Byte_Signal_minus.RS-Byte_Signal.RS)<0	2	Ключ РАЗГОН			mb3066(m150.0-m150.7)	Byte_Signal.RS:=Ord(Swap(buffer_m[0])and 1024=1024);
        //    //(Byte_Signal_minus.NO-Byte_Signal.NO)<0	3	Кнопка НОРМАЛЬНЫЙ ОСТАНОВ 			mb3066(m150.0-m150.7)	Byte_Signal.NO:=Ord(Swap(buffer_m[0])and 2048=2048);
        //    //(Byte_Signal_minus.FO-Byte_Signal.FO)<0	4	Кнопка ФОРСИРОВАННЫЙ ОСТАНОВ 			mb3066(m150.0-m150.7)	Byte_Signal.FO:=Ord(Swap(buffer_m[0])and 4096=4096);
        //    //(Byte_Signal_minus.MaxSpeedPeregr-Byte_Signal.MaxSpeedPeregr)<0	6	Максимальный перегруз 			mb3066(m150.0-m150.7)	Byte_Signal.MaxSpeedPeregr:=Ord(Swap(buffer_m[0])and 8192=8192);
        //    //(Byte_Signal_minus.MaxSpeedPeregr-Byte_Signal.MaxSpeedPeregr)>0	7	Отсутствие максимального перегруз			mb3066(m150.0-m150.7)	Byte_Signal.MaxSpeedPeregr:=Ord(Swap(buffer_m[0])and 8192=8192);
        //    //(Byte_Signal_minus.UstavkaSpeed - Byte_Signal.UstavkaSpeed) < 0 1   Уставка рабочей скорости            mb3066(m150.0 - m150.7)   Byte_Signal.UstavkaSpeed:= Ord(Swap(buffer_m[0])and 16384 = 16384);
        //    //(Byte_Signal_minus.MaxSpeed-Byte_Signal.MaxSpeed)<0	2	Перегруз по скорости mb3066(m150.0-m150.7)   Byte_Signal.MaxSpeed:=Ord(Swap(buffer_m[0]) and 32768=32768);
        //    [8] = new MessageClass(2, "Кнопка ЗАПРАВКА", 0, ""),
        //    [9] = new MessageClass(1, "Ключ ТАК ДЕРЖАТЬ", 0, ""),
        //    [10] = new MessageClass(2, "Ключ РАЗГОН", 0, ""),
        //    [11] = new MessageClass(3, "Кнопка НОРМАЛЬНЫЙ ОСТАНОВ", 0, ""),
        //    [12] = new MessageClass(4, "Кнопка ФОРСИРОВАННЫЙ ОСТАНОВ", 0, ""),
        //    [13] = new MessageClass(6, "Максимальный перегруз", 7, "Отсутствие максимального перегруз"),
        //    [14] = new MessageClass(1, "Уставка рабочей скорости", 0, ""),
        //    [15] = new MessageClass(2, "Перегруз по скорости", 0, ""),

        //    //byte2  -> buffer_m[1](1-128)
        //    //(Byte_Signal_minus.LKmot - Byte_Signal.LKmot) < 0   2   ЛК моталки включены             mb3069(m153.0 - m153.7)    Byte_Signal.LKmot:= Ord(Swap(buffer_m[1])and 1 = 1);
        //    //(Byte_Signal_minus.LKmot-Byte_Signal.LKmot)>0	6	ЛК моталки выключены mb3069(m153.0-m153.7)    Byte_Signal.LKmot:=Ord(Swap(buffer_m[1]) and 1=1);
        //    //(Byte_Signal_minus.LKrazm-Byte_Signal.LKrazm)<0	2	ЛК разматывателя включены mb3069(m153.0-m153.7)   Byte_Signal.LKrazm:=Ord(Swap(buffer_m[1]) and 2=2);
        //    //(Byte_Signal_minus.LKrazm-Byte_Signal.LKrazm)>0	6	ЛК разматывателя выключены mb3069(m153.0-m153.7)   Byte_Signal.LKrazm:=Ord(Swap(buffer_m[1]) and 2=2);
        //    //(Byte_Signal_minus.Got_64kg - Byte_Signal.Got_64kg) < 0 1   Гидравлика 64 кг готова             mb3069(m153.0 - m153.7)   Byte_Signal.Got_64kg:= Ord(Swap(buffer_m[1])and 4 = 4);
        //    //(Byte_Signal_minus.Got_64kg-Byte_Signal.Got_64kg)>0	4	Гидравлика 64 кг не готова mb3069(m153.0-m153.7)   Byte_Signal.Got_64kg:=Ord(Swap(buffer_m[1]) and 4=4);
        //    //(Byte_Signal_minus.Rnz12 - Byte_Signal.Rnz12) > 0   6   РНЗ 12 включено             mb3069(m153.0 - m153.7)   Byte_Signal.Rnz12:= Ord(Swap(buffer_m[1])and 8 = 8);
        //    //(Byte_Signal_minus.Rnz12-Byte_Signal.Rnz12)<0	7	РНЗ 12 выключено mb3069(m153.0-m153.7)   Byte_Signal.Rnz12:=Ord(Swap(buffer_m[1]) and 8=8);
        //    //(Byte_Signal_minus.Rnz23-Byte_Signal.Rnz23)<0	6	РНЗ 23 включено mb3069(m153.0-m153.7)   Byte_Signal.Rnz23:=Ord(Swap(buffer_m[1]) and 16=16);
        //    //(Byte_Signal_minus.Rnz23-Byte_Signal.Rnz23)>0	7	РНЗ 23 выключено mb3069(m153.0-m153.7)   Byte_Signal.Rnz23:=Ord(Swap(buffer_m[1]) and 16=16);
        //    //(Byte_Signal_minus.Rnz34-Byte_Signal.Rnz34)<0	6	РНЗ 34 включено mb3069(m153.0-m153.7)   Byte_Signal.Rnz34:=Ord(Swap(buffer_m[1]) and 32=32);
        //    //(Byte_Signal_minus.Rnz34-Byte_Signal.Rnz34)>0	7	РНЗ 34 выключено mb3069(m153.0-m153.7)   Byte_Signal.Rnz34:=Ord(Swap(buffer_m[1]) and 32=32);
        //    //(Byte_Signal_minus.GrtVkl-Byte_Signal.GrtVkl)<0	6	ГРТ включено            mb3069(m153.0-m153.7)   Byte_Signal.GrtVkl:=Ord(Swap(buffer_m[1]) and 64=64);
        //    //(Byte_Signal_minus.GrtVkl-Byte_Signal.GrtVkl)>0	7	ГРТ выключено           mb3069(m153.0-m153.7)   Byte_Signal.GrtVkl:=Ord(Swap(buffer_m[1]) and 64=64);
        //    //(Byte_Signal_minus.TrtVkl-Byte_Signal.TrtVkl)<0	6	ТРТ включено            mb3069(m153.0-m153.7)   Byte_Signal.TrtVkl:=Ord(Swap(buffer_m[1]) and 128=128);
        //    //(Byte_Signal_minus.TrtVkl-Byte_Signal.TrtVkl)>0	7	ТРТ выключено           mb3069(m153.0-m153.7)   Byte_Signal.TrtVkl:=Ord(Swap(buffer_m[1]) and 128=128);
        //    [16] = new MessageClass(2, "ЛК моталки включены", 6, "ЛК моталки выключены"),
        //    [17] = new MessageClass(2, "ЛК разматывателя включены", 6, "ЛК разматывателя выключены"),
        //    [18] = new MessageClass(1, "Гидравлика 64 кг готова", 4, "Гидравлика 64 кг не готова"),
        //    [19] = new MessageClass(7, "РНЗ 12 выключено", 6, "РНЗ 12 включено"),
        //    [20] = new MessageClass(6, "РНЗ 23 включено", 7, "РНЗ 23 выключено"),
        //    [21] = new MessageClass(6, "РНЗ 34 включено", 7, "РНЗ 34 выключено"),
        //    [22] = new MessageClass(6, "ГРТ включено", 7, "ГРТ выключено"),
        //    [23] = new MessageClass(6, "ТРТ включено", 7, "ТРТ выключено"),


        //    //byte3-> buffer_m[1](256-32768)
        //    //(Byte_Signal_minus.T40 - Byte_Signal.T40) < 0   6   Натяжение в 4 промежутке            mb3068(m152.0 - m152.7)   Byte_Signal.T40:= Ord(Swap(buffer_m[1]) and 256 = 256);
        //    //(Byte_Signal_minus.T40-Byte_Signal.T40)>0	7	Отсутствие натяжения в 4 промежутке mb3068(m152.0-m152.7)   Byte_Signal.T40:=Ord(Swap(buffer_m[1]) and 256=256);
        //    //(Byte_Signal_minus.Tmot-Byte_Signal.Tmot)<0	6	Натяжение на моталке mb3068(m152.0-m152.7)   Byte_Signal.Tmot:=Ord(Swap(buffer_m[1]) and 512=512);
        //    //(Byte_Signal_minus.Tmot-Byte_Signal.Tmot)>0	7	Отсутствие натяжения на моталке             mb3068(m152.0-m152.7)   Byte_Signal.Tmot:=Ord(Swap(buffer_m[1]) and 512=512);
        //    //(Byte_Signal_minus.Trazm-Byte_Signal.Trazm)<0	6	Натяжение на разматывателе mb3068(m152.0-m152.7)   Byte_Signal.Trazm:=Ord(Swap(buffer_m[1]) and 1024=1024);
        //    //(Byte_Signal_minus.Trazm-Byte_Signal.Trazm)>0	7	Отсутствие натяжения на разматывателе           mb3068(m152.0-m152.7)   Byte_Signal.Trazm:=Ord(Swap(buffer_m[1]) and 1024=1024);
        //    //(Byte_Signal_minus.LK1-Byte_Signal.LK1)<0	2	ЛК клети 1 включены mb3068(m152.0-m152.7)   Byte_Signal.LK1:=Ord(Swap(buffer_m[1]) and 2048=2048);
        //    //(Byte_Signal_minus.LK1-Byte_Signal.LK1)>0	6	ЛК клети 1 выключены mb3068(m152.0-m152.7)   Byte_Signal.LK1:=Ord(Swap(buffer_m[1]) and 2048=2048);
        //    //(Byte_Signal_minus.LK2-Byte_Signal.LK2)<0	2	ЛК клети 2 включены mb3068(m152.0-m152.7)   Byte_Signal.LK2:=Ord(Swap(buffer_m[1]) and 4096=4096);
        //    //(Byte_Signal_minus.LK2-Byte_Signal.LK2)>0	6	ЛК клети 2 выключены mb3068(m152.0-m152.7)   Byte_Signal.LK2:=Ord(Swap(buffer_m[1]) and 4096=4096);
        //    //(Byte_Signal_minus.LK3-Byte_Signal.LK3)<0	2	ЛК клети 3 включены mb3068(m152.0-m152.7)   Byte_Signal.LK3:=Ord(Swap(buffer_m[1]) and 8192=8192);
        //    //(Byte_Signal_minus.LK3-Byte_Signal.LK3)>0	6	ЛК клети 3 выключены mb3068(m152.0-m152.7)   Byte_Signal.LK3:=Ord(Swap(buffer_m[1]) and 8192=8192);
        //    //(Byte_Signal_minus.LK4-Byte_Signal.LK4)<0	2	ЛК клети 4 включены mb3068(m152.0-m152.7)   Byte_Signal.LK4:=Ord(Swap(buffer_m[1]) and 16384=16384);
        //    //(Byte_Signal_minus.LK4-Byte_Signal.LK4)>0	6	ЛК клети 4 выключены mb3068(m152.0-m152.7)   Byte_Signal.LK4:=Ord(Swap(buffer_m[1]) and 16384=16384);
        //    //(Byte_Signal_minus.LK5-Byte_Signal.LK5)<0	2	ЛК клети 5 включены mb3068(m152.0-m152.7)   Byte_Signal.LK5:=Ord(Swap(buffer_m[1]) and 32768=32768);
        //    //(Byte_Signal_minus.LK5-Byte_Signal.LK5)>0	6	ЛК клети 5 выключены mb3068(m152.0-m152.7)   Byte_Signal.LK5:=Ord(Swap(buffer_m[1]) and 32768=32768);
        //    [24] = new MessageClass(6, "Натяжение в 4 промежутке", 7, "Отсутствие натяжения в 4 промежутке"),
        //    [25] = new MessageClass(6, "Натяжение на моталке", 0, "Отсутствие натяжения на моталке"),
        //    [26] = new MessageClass(6, "Натяжение на разматывателе", 7, "Отсутствие натяжения на разматывателе"),
        //    [27] = new MessageClass(2, "ЛК клети 1 включены", 6, "ЛК клети 1 выключены"),
        //    [28] = new MessageClass(2, "ЛК клети 2 включены", 6, "ЛК клети 2 выключены"),
        //    [29] = new MessageClass(2, "ЛК клети 3 включены", 6, "ЛК клети 3 выключены"),
        //    [30] = new MessageClass(2, "ЛК клети 4 включены", 6, "ЛК клети 4 выключены"),
        //    [31] = new MessageClass(2, "ЛК клети 5 включены", 6, "ЛК клети 5 выключены"),

        //    //byte4  -> buffer_m[2](1-128)
        //    //(Byte_Signal_minus.NalPol - Byte_Signal.NalPol) < 0 5   Наличие полосы в толщиномере за 5 клетью            mb3071(m155.0 - m155.7)   Byte_Signal.NalPol:= Ord(Swap(buffer_m[2]) and 1 = 1);
        //    //(Byte_Signal_minus.NalPol-Byte_Signal.NalPol)>0	7	Отсутствие полосы в толщиномере за 5 клетью mb3071(m155.0-m155.7)   Byte_Signal.NalPol:=Ord(Swap(buffer_m[2]) and 1=1);
        //    //(Byte_Signal_minus.Knp-Byte_Signal.Knp)<0	1	Ноль задания скорости mb3071(m155.0-m155.7)   Byte_Signal.Knp:=Ord(Swap(buffer_m[2]) and 2=2);
        //    //(Byte_Signal_minus.Knp-Byte_Signal.Knp)>0	2	Поехали mb3071(m155.0-m155.7)   Byte_Signal.Knp:=Ord(Swap(buffer_m[2]) and 2=2);
        //    //(Byte_Signal_minus.GotStan-Byte_Signal.GotStan)<0	1	Сборка схемы стана mb3071(m155.0-m155.7)   Byte_Signal.GotStan:=Ord(Swap(buffer_m[2]) and 4=4);
        //    //(Byte_Signal_minus.GotStan-Byte_Signal.GotStan)>0	4	Развал схемы стана mb3071(m155.0-m155.7)   Byte_Signal.GotStan:=Ord(Swap(buffer_m[2]) and 4=4);
        //    //(Byte_Signal_minus.MaxV1-Byte_Signal.MaxV1)<0	4	Максимальная скорость клети 1 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV1:=Ord(Swap(buffer_m[2]) and 8=8);
        //    //(Byte_Signal_minus.MaxV1-Byte_Signal.MaxV1)>0	5	Конец максимальной скорости клети 1 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV1:=Ord(Swap(buffer_m[2]) and 8=8);
        //    //(Byte_Signal_minus.MaxV2-Byte_Signal.MaxV2)<0	4	Максимальная скорость клети 2 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV2:=Ord(Swap(buffer_m[2]) and 16=16);
        //    //(Byte_Signal_minus.MaxV2-Byte_Signal.MaxV2)>0	5	Конец максимальной скорости клети 2 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV2:=Ord(Swap(buffer_m[2]) and 16=16);
        //    //(Byte_Signal_minus.MaxV3-Byte_Signal.MaxV3)<0	4	Максимальная скорость клети 3 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV3:=Ord(Swap(buffer_m[2]) and 32=32);
        //    //(Byte_Signal_minus.MaxV3-Byte_Signal.MaxV3)>0	5	Конец максимальной скорости клети 3 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV3:=Ord(Swap(buffer_m[2]) and 32=32);
        //    //(Byte_Signal_minus.MaxV4-Byte_Signal.MaxV4)<0	4	Максимальная скорость клети 4 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV4:=Ord(Swap(buffer_m[2]) and 64=64);
        //    //(Byte_Signal_minus.MaxV4-Byte_Signal.MaxV4)>0	5	Конец максимальной скорости клети 4 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV4:=Ord(Swap(buffer_m[2]) and 64=64);
        //    //(Byte_Signal_minus.MaxV5-Byte_Signal.MaxV5)<0	4	Максимальная скорость клети 5 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV5:=Ord(Swap(buffer_m[2]) and 128=128);
        //    //(Byte_Signal_minus.MaxV5-Byte_Signal.MaxV5)>0	5	Конец максимальной скорости клети 5 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV5:=Ord(Swap(buffer_m[2]) and 128=128);
        //    [32] = new MessageClass(5, "Наличие полосы в толщиномере за 5 клетью", 7, "Отсутствие полосы в толщиномере за 5 клетью"),
        //    [33] = new MessageClass(1, "Ноль задания скорости", 2, "Поехали"),
        //    [34] = new MessageClass(1, "Сборка схемы стана", 4, "Развал схемы стана"),
        //    [35] = new MessageClass(4, "Максимальная скорость клети 1", 5, "Конец максимальной скорости клети 1"),
        //    [36] = new MessageClass(4, "Максимальная скорость клети 2", 5, "Конец максимальной скорости клети 2"),
        //    [37] = new MessageClass(4, "Максимальная скорость клети 3", 5, "Конец максимальной скорости клети 3"),
        //    [38] = new MessageClass(4, "Максимальная скорость клети 4", 5, "Конец максимальной скорости клети 4"),
        //    [39] = new MessageClass(4, "Максимальная скорость клети 5", 5, "Конец максимальной скорости клети 5"),

        //    //byte5-> buffer_m[2](256-32768)
        //    // (Byte_Signal_minus.RKDVVkl - Byte_Signal.RKDVVkl) < 0   6   РКДВ включен            mb3070(m154.0 - m154.7)   Byte_Signal.RKDVVkl:= Ord(Swap(buffer_m[2]) and 256 = 256);
        //    //(Byte_Signal_minus.RKDVVkl-Byte_Signal.RKDVVkl)>0	7	РКДВ выключен           mb3070(m154.0-m154.7)   Byte_Signal.RKDVVkl:=Ord(Swap(buffer_m[2]) and 256=256);
        //    //(Byte_Signal_minus.RpvVkl-Byte_Signal.RpvVkl)<0	6	РПВ включен             mb3070(m154.0-m154.7)   Byte_Signal.RpvVkl:=Ord(Swap(buffer_m[2]) and 512=512);
        //    //(Byte_Signal_minus.RpvVkl-Byte_Signal.RpvVkl)>0	7	РПВ выключен            mb3070(m154.0-m154.7)   Byte_Signal.RpvVkl:=Ord(Swap(buffer_m[2]) and 512=512);
        //    //(Byte_Signal_minus.Rnv12-Byte_Signal.Rnv12)<0	1	РНВ12 включен           mb3070(m154.0-m154.7)   Byte_Signal.Rnv12:=Ord(Swap(buffer_m[2]) and 1024=1024);
        //    //(Byte_Signal_minus.Rnv12-Byte_Signal.Rnv12)>0	4	РНВ12 выключен          mb3070(m154.0-m154.7)   Byte_Signal.Rnv12:=Ord(Swap(buffer_m[2]) and 1024=1024);
        //    //(Byte_Signal_minus.Rnv23-Byte_Signal.Rnv23)<0	1	РНВ23 включен           mb3070(m154.0-m154.7)   Byte_Signal.Rnv23:=Ord(Swap(buffer_m[2]) and 2048=2048);
        //    //(Byte_Signal_minus.Rnv23-Byte_Signal.Rnv23)>0	4	РНВ23 выключен          mb3070(m154.0-m154.7)   Byte_Signal.Rnv23:=Ord(Swap(buffer_m[2]) and 2048=2048);
        //    //(Byte_Signal_minus.Rnv34-Byte_Signal.Rnv34)<0	1	РНВ34 включен           mb3070(m154.0-m154.7)   Byte_Signal.Rnv34:=Ord(Swap(buffer_m[2]) and 4096=4096);
        //    //(Byte_Signal_minus.Rnv34-Byte_Signal.Rnv34)>0	4	РНВ34 выключен          mb3070(m154.0-m154.7)   Byte_Signal.Rnv34:=Ord(Swap(buffer_m[2]) and 4096=4096);
        //    //(Byte_Signal_minus.Rnz45 - Byte_Signal.Rnz45) < 0   6   РН 45 включено          mb3070(m154.0 - m154.7)   Byte_Signal.Rnz45:= Ord(Swap(buffer_m[2]) and 8192 = 8192);
        //    //(Byte_Signal_minus.Rnz45-Byte_Signal.Rnz45)>0	7	РН 45 выключено mb3070(m154.0-m154.7)   Byte_Signal.Rnz45:=Ord(Swap(buffer_m[2]) and 8192=8192);
        //    //(Byte_Signal_minus.Rtv-Byte_Signal.Rtv)<0	1	РТВ включен             mb3070(m154.0-m154.7)   Byte_Signal.Rtv:=Ord(Swap(buffer_m[2]) and 16384=16384);
        //    //(Byte_Signal_minus.Rtv-Byte_Signal.Rtv)>0	7	РТВ выключен            mb3070(m154.0-m154.7)   Byte_Signal.Rtv:=Ord(Swap(buffer_m[2]) and 16384=16384);
        //    //(Byte_Signal_minus.Got_100kg-Byte_Signal.Got_100kg)<0	1	Гидравлика 100 кг готова            mb3070(m154.0-m154.7)   Byte_Signal.Got_100kg:=Ord(Swap(buffer_m[2]) and 32768=32768);
        //    //(Byte_Signal_minus.Got_100kg-Byte_Signal.Got_100kg)>0	4	Гидравлика 100 кг не готова mb3070(m154.0-m154.7)   Byte_Signal.Got_100kg:=Ord(Swap(buffer_m[2]) and 32768=32768);
        //    [40] = new MessageClass(6, "РКДВ включен", 6, "РКДВ выключен"),
        //    [41] = new MessageClass(6, "РПВ включен", 6, "РПВ выключен"),
        //    [42] = new MessageClass(1, "РНВ12 включен", 4, "РНВ12 выключен"),
        //    [43] = new MessageClass(1, "РНВ23 включен", 4, "РНВ23 выключен"),
        //    [44] = new MessageClass(1, "РНВ34 включен", 4, "РНВ34 выключен"),
        //    [45] = new MessageClass(6, "РН45 включено", 7, "РН45 выключено"),
        //    [46] = new MessageClass(1, "РТВ включен", 7, "РТВ выключен"),
        //    [47] = new MessageClass(1, "Гидравлика 100 кг готова", 4, "Гидравлика 100 кг не готова"),

        //    //byte6  -> buffer_m[3](1-128)
        //    //(Byte_Signal_minus.g12 - Byte_Signal.g12) < 0   1   ПЖТ Ж - 12 готова             m3103.0 - m3103.7 Byte_Signal.g12:= Ord(Swap(buffer_m[3]) and 1 = 1);
        //    //(Byte_Signal_minus.g12-Byte_Signal.g12)>0	4	ПЖТ Ж-12 не готова          m3103.0-m3103.7	Byte_Signal.g12:=Ord(Swap(buffer_m[3]) and 1=1);
        //    //(Byte_Signal_minus.g13-Byte_Signal.g13)<0	1	ПЖТ Ж-13 готова m3103.0-m3103.7	Byte_Signal.g13:=Ord(Swap(buffer_m[3]) and 2=2);
        //    //(Byte_Signal_minus.g13-Byte_Signal.g13)>0	4	ПЖТ Ж-13 не готова          m3103.0-m3103.7	Byte_Signal.g13:=Ord(Swap(buffer_m[3]) and 2=2);
        //    //(Byte_Signal_minus.g14-Byte_Signal.g14)<0	1	ПЖТ Ж-14 готова m3103.0-m3103.7	Byte_Signal.g14:=Ord(Swap(buffer_m[3]) and 4=4);
        //    //(Byte_Signal_minus.g14-Byte_Signal.g14)>0	4	ПЖТ Ж-14 не готова          m3103.0-m3103.7	Byte_Signal.g14:=Ord(Swap(buffer_m[3]) and 4=4);
        //    //(Byte_Signal_minus.g15-Byte_Signal.g15)>0	1	Смазка Ж-15 готова m3103.0-m3103.7	Byte_Signal.g15:=Ord(Swap(buffer_m[3]) and 8=8);
        //    //(Byte_Signal_minus.g15-Byte_Signal.g15)<0	4	Смазка Ж-15 не готова           m3103.0-m3103.7	Byte_Signal.g15:=Ord(Swap(buffer_m[3]) and 8=8);
        //    //(Byte_Signal_minus.g16-Byte_Signal.g16)<0	1	Смазка Ж-16 готова m3103.0-m3103.7	Byte_Signal.g16:=Ord(Swap(buffer_m[3]) and 16=16);
        //    //(Byte_Signal_minus.g16-Byte_Signal.g16)>0	4	Смазка Ж-16 не готова           m3103.0-m3103.7	Byte_Signal.g16:=Ord(Swap(buffer_m[3]) and 16=16);
        //    //(Byte_Signal_minus.NatshUsl-Byte_Signal.NatshUsl)<0	5	Начальные условия           m3103.0-m3103.7	Byte_Signal.NatshUsl:=Ord(Swap(buffer_m[3]) and 32=32);
        //    //(Byte_Signal_minus.GotEmuls-Byte_Signal.GotEmuls)<0	5	Эмульсионная система готова m3103.0-m3103.7	Byte_Signal.GotEmuls:=Ord(Swap(buffer_m[3]) and 64=64);
        //    //(Byte_Signal_minus.GotEmuls-Byte_Signal.GotEmuls)>0	7	Эмульсионная система не готова          m3103.0-m3103.7	Byte_Signal.GotEmuls:=Ord(Swap(buffer_m[3]) and 64=64);
        //    //(Byte_Signal_minus.g17-Byte_Signal.g17)<0	1	Смазка Ж-17 готова m3103.0-m3103.7	Byte_Signal.g17:=Ord(Swap(buffer_m[3]) and 128=128);
        //    //(Byte_Signal_minus.g17-Byte_Signal.g17)>0	4	Смазка Ж-17 не готова           m3103.0-m3103.7	Byte_Signal.g17:=Ord(Swap(buffer_m[3]) and 128=128);
        //    [48] = new MessageClass(1, "ПЖТ Ж - 12 готова", 4, "ПЖТ Ж-12 не готова"),
        //    [49] = new MessageClass(1, "ПЖТ Ж - 13 готова", 4, "ПЖТ Ж-13 не готова"),
        //    [50] = new MessageClass(1, "ПЖТ Ж - 14 готова", 4, "ПЖТ Ж-14 не готова"),
        //    [51] = new MessageClass(1, "Смазка Ж-15 готова", 4, "Смазка Ж-15 не готова"),
        //    [52] = new MessageClass(1, "Смазка Ж-16 готова", 4, "Смазка Ж-16 не готова"),
        //    [53] = new MessageClass(5, "Начальные условия",0 , ""),
        //    [54] = new MessageClass(5, "Эмульсионная система готова", 7, "Эмульсионная система не готова"),
        //    [54] = new MessageClass(1, "Смазка Ж-17 готова", 4, "Смазка Ж-17 не готова"),

        //    //byte7  -> buffer_m[3](256-32768)
        //    //(Byte_Signal_minus.g18 - Byte_Signal.g18) < 0   1   Смазка Ж - 18 готова          m3102.0 - m3102.7 Byte_Signal.g18:= Ord(Swap(buffer_m[3])and 256 = 256);
        //    //(Byte_Signal_minus.g18-Byte_Signal.g18)>0	4	Смазка Ж-18 не готова           m3102.0-m3102.7	Byte_Signal.g18:=Ord(Swap(buffer_m[3]) and 256=256);
        //    //(Byte_Signal_minus.g19-Byte_Signal.g19)>0	1	Смазка Ж-19 не готова           m3102.0-m3102.7	Byte_Signal.g19:=Ord(Swap(buffer_m[3]) and 512=512);
        //    //(Byte_Signal_minus.g19-Byte_Signal.g19)<0	4	Смазка Ж-19 готова m3102.0-m3102.7	Byte_Signal.g19:=Ord(Swap(buffer_m[3]) and 512=512);
        //    //(Byte_Signal_minus.g20-Byte_Signal.g20)<0	1	Смазка Ж-20 готова m3102.0-m3102.7	Byte_Signal.g20:=Ord(Swap(buffer_m[3]) and 1024=1024);
        //    //(Byte_Signal_minus.g20-Byte_Signal.g20)>0	4	Смазка Ж-20 не готова           m3102.0-m3102.7	Byte_Signal.g20:=Ord(Swap(buffer_m[3]) and 1024=1024);
        //    //(Byte_Signal_minus.temp_POU-Byte_Signal.temp_POU)>0	1	Температура в ПОУ нормальная            m3102.0-m3102.7	Byte_Signal.temp_POU:=Ord(Swap(buffer_m[3]) and 2048=2048);
        //    //(Byte_Signal_minus.temp_POU-Byte_Signal.temp_POU)<0	4	Температура в ПОУ высокая           m3102.0-m3102.7	Byte_Signal.temp_POU:=Ord(Swap(buffer_m[3]) and 2048=2048);
        //    //(Byte_Signal_minus.davl_redukt-Byte_Signal.davl_redukt)<0	1	Давление редукторов низкое m3102.0-m3102.7	Byte_Signal.davl_redukt:=Ord(Swap(buffer_m[3]) and 4096=4096);
        //    //(Byte_Signal_minus.davl_redukt-Byte_Signal.davl_redukt)>0	4	Давление редукторов нормальное m3102.0-m3102.7	Byte_Signal.davl_redukt:=Ord(Swap(buffer_m[3]) and 4096=4096);
        //    //(Byte_Signal_minus.davl_PGT-Byte_Signal.davl_PGT)<0	1	Давление ПЖТ низкое m3102.0-m3102.7	Byte_Signal.davl_PGT:=Ord(Swap(buffer_m[3]) and 8192=8192);
        //    //(Byte_Signal_minus.davl_PGT-Byte_Signal.davl_PGT)>0	4	Давление ПЖТ нормальное m3102.0-m3102.7	Byte_Signal.davl_PGT:=Ord(Swap(buffer_m[3]) and 8192=8192);
        //    //(Byte_Signal_minus.temp_privod-Byte_Signal.temp_privod)<0	1	Вентиляция готова           m3102.0-m3102.7	Byte_Signal.temp_privod:=Ord(Swap(buffer_m[3]) and 16384=16384);
        //    //(Byte_Signal_minus.temp_privod-Byte_Signal.temp_privod)>0	4	Вентиляция не готова m3102.0-m3102.7	Byte_Signal.temp_privod:=Ord(Swap(buffer_m[3]) and 16384=16384);
        //    //(Byte_Signal_minus.got_sinhr-Byte_Signal.got_sinhr)<0	1	Синхронные двигатели готовы m3102.0-m3102.7	Byte_Signal.got_sinhr:=Ord(Swap(buffer_m[3]) and 32768=32768);
        //    //(Byte_Signal_minus.got_sinhr-Byte_Signal.got_sinhr)>0	4	Синхронные двигатели не готовы          m3102.0-m3102.7	Byte_Signal.got_sinhr:=Ord(Swap(buffer_m[3]) and 32768=32768);

        //    [55] = new MessageClass(1, "Смазка Ж - 18 готова", 4, "Смазка Ж-18 не готова"),
        //    [56] = new MessageClass(1, "Смазка Ж - 19 готова", 4, "Смазка Ж-19 не готова"),
        //    [57] = new MessageClass(1, "Смазка Ж - 20 готова", 4, "Смазка Ж-20 не готова"),
        //    [58] = new MessageClass(1, "Температура в ПОУ нормальная", 4, "Температура в ПОУ высокая"),
        //    [59] = new MessageClass(1, "Давление редукторов низкое", 4, "Давление редукторов нормальное"),
        //    [60] = new MessageClass(1, "Давление ПЖТ низкое", 4, "Давление ПЖТ нормальное"),
        //    [61] = new MessageClass(1, "Вентиляция готова", 4, "Вентиляция не готова"),
        //    [62] = new MessageClass(1, "Синхронные двигатели готовы", 4, "Синхронные двигатели не готовы"),

        //    //byte8  -> buffer_m[4](1-128)
        //    //(Byte_Signal_minus.OgragdMot - Byte_Signal.OgragdMot) < 0   1   Ограждение моталки закрыто          m3105.0 - m3105.7 Byte_Signal.OgragdMot:= Ord(Swap(buffer_m[4])and 1 = 1);
        //    //(Byte_Signal_minus.OgragdMot-Byte_Signal.OgragdMot)>0	4	Ограждение моталки открыто НО           m3105.0-m3105.7	Byte_Signal.OgragdMot:=Ord(Swap(buffer_m[4]) and 1=1);
        //    //(Byte_Signal_minus.ZaxlestOtMot-Byte_Signal.ZaxlestOtMot)<0	1	Захлестыватель у моталки НО             m3105.0-m3105.7	Byte_Signal.ZaxlestOtMot:=Ord(Swap(buffer_m[4]) and 2=2);
        //    //(Byte_Signal_minus.ZaxlestOtMot-Byte_Signal.ZaxlestOtMot)>0	4	Захлестыватель отведен          m3105.0-m3105.7	Byte_Signal.ZaxlestOtMot:=Ord(Swap(buffer_m[4]) and 2=2);
        //    //(Byte_Signal_minus.NOTempGP-Byte_Signal.NOTempGP)<0	1	Высокая температура ПЖТ ГП          m3105.0-m3105.7	Byte_Signal.NOTempGP:=Ord(Swap(buffer_m[4]) and 4=4);//bpv
        //    //(Byte_Signal_minus.NOTempGP-Byte_Signal.NOTempGP)>0	4	Нормальная температура ПЖТ ГП           m3105.0-m3105.7	Byte_Signal.NOTempGP:=Ord(Swap(buffer_m[4]) and 4=4);//bpv
        //    //(Byte_Signal_minus.Peregr1 - Byte_Signal.Peregr1) < 0   4   Перегруз клети 1            m3105.0 - m3105.7 Byte_Signal.Peregr1:= Ord(Swap(buffer_m[4])and 8 = 8);
        //    //(Byte_Signal_minus.Peregr1-Byte_Signal.Peregr1)>0	5	Конец перегруза клети 1 			m3105.0-m3105.7	Byte_Signal.Peregr1:=Ord(Swap(buffer_m[4]) and 8=8);
        //    //(Byte_Signal_minus.Peregr2-Byte_Signal.Peregr2)<0	4	Перегруз клети 2 			m3105.0-m3105.7	Byte_Signal.Peregr2:=Ord(Swap(buffer_m[4]) and 16=16);
        //    //(Byte_Signal_minus.Peregr2-Byte_Signal.Peregr2)>0	5	Конец перегруза клети 2 			m3105.0-m3105.7	Byte_Signal.Peregr2:=Ord(Swap(buffer_m[4]) and 16=16);
        //    //(Byte_Signal_minus.Peregr3-Byte_Signal.Peregr3)<0	4	Перегруз клети 3 			m3105.0-m3105.7	Byte_Signal.Peregr3:=Ord(Swap(buffer_m[4]) and 32=32);
        //    //(Byte_Signal_minus.Peregr3-Byte_Signal.Peregr3)>0	5	Конец перегруза клети 3 			m3105.0-m3105.7	Byte_Signal.Peregr3:=Ord(Swap(buffer_m[4]) and 32=32);
        //    //(Byte_Signal_minus.Peregr4-Byte_Signal.Peregr4)<0	4	Перегруз клети 4 			m3105.0-m3105.7	Byte_Signal.Peregr4:=Ord(Swap(buffer_m[4]) and 64=64);
        //    //(Byte_Signal_minus.Peregr4-Byte_Signal.Peregr4)>0	5	Конец перегруза клети 4 			m3105.0-m3105.7	Byte_Signal.Peregr4:=Ord(Swap(buffer_m[4]) and 64=64);
        //    //(Byte_Signal_minus.Peregr5-Byte_Signal.Peregr5)<0	4	Перегруз клети 5 			m3105.0-m3105.7	Byte_Signal.Peregr5:=Ord(Swap(buffer_m[4]) and 128=128);
        //    //(Byte_Signal_minus.Peregr5-Byte_Signal.Peregr5)>0	5	Конец перегруза клети 5 			m3105.0-m3105.7	Byte_Signal.Peregr5:=Ord(Swap(buffer_m[4]) and 128=128);
        //    [63] = new MessageClass(1, "Ограждение моталки закрыто", 4, "Ограждение моталки открыто НО"),
        //    [64] = new MessageClass(1, "Захлестыватель у моталки НО", 4, "Захлестыватель отведен"),
        //    [65] = new MessageClass(1, "Высокая температура ПЖТ ГП", 4, "Нормальная температура ПЖТ ГП"),
        //    [66] = new MessageClass(4, "Перегруз клети 1", 5, "Конец перегруза клети 1"),
        //    [67] = new MessageClass(4, "Перегруз клети 2", 5, "Конец перегруза клети 2"),
        //    [68] = new MessageClass(4, "Перегруз клети 3", 5, "Конец перегруза клети 3"),
        //    [69] = new MessageClass(4, "Перегруз клети 4", 5, "Конец перегруза клети 4"),
        //    [70] = new MessageClass(4, "Перегруз клети 5", 5, "Конец перегруза клети 5"),

        //    //byte9  -> buffer_m[4](256-32768)
        //    //(Byte_Signal_minus.NOSinxr - Byte_Signal.NOSinxr) < 0   1   Высокая температура ПЖТ СД          m3104.0 - m3104.7 Byte_Signal.NOSinxr:= Ord(Swap(buffer_m[4])and 256 = 256);
        //    //(Byte_Signal_minus.NOSinxr-Byte_Signal.NOSinxr)>0	4	Нормальная температура ПЖТ СД           m3104.0-m3104.8	Byte_Signal.NOSinxr:=Ord(Swap(buffer_m[4]) and 256=256);
        //    //(Byte_Signal_minus.NOPanPultStar-Byte_Signal.NOPanPultStar)<0	4	Кнопка НО на ПУ старшего нажата             m3104.0-m3104.9	Byte_Signal.NOPanPultStar:=Ord(Swap(buffer_m[4]) and 512=512);
        //    //(Byte_Signal_minus.NOPURazm-Byte_Signal.NOPURazm)>0	4	Кнопка НО на ПУР нажата m3104.0-m3104.10	Byte_Signal.NOPURazm:=Ord(Swap(buffer_m[4]) and 1024=1024);
        //    //(Byte_Signal_minus.NOPU1-Byte_Signal.NOPU1)>0	4	Кнопка НО на ПУ1 нажата m3104.0-m3104.11	Byte_Signal.NOPU1:=Ord(Swap(buffer_m[4]) and 2048=2048);
        //    //(Byte_Signal_minus.NOPU2-Byte_Signal.NOPU2)>0	4	Кнопка НО на ПУ2 нажата m3104.0-m3104.12	Byte_Signal.NOPU2:=Ord(Swap(buffer_m[4]) and 4096=4096);
        //    //(Byte_Signal_minus.NOPU3-Byte_Signal.NOPU3)>0	4	Кнопка НО на ПУ3 нажата m3104.0-m3104.13	Byte_Signal.NOPU3:=Ord(Swap(buffer_m[4]) and 8192=8192);
        //    //(Byte_Signal_minus.NOPU4-Byte_Signal.NOPU4)>0	4	Кнопка НО на ПУ4 нажата m3104.0-m3104.14	Byte_Signal.NOPU4:=Ord(Swap(buffer_m[4]) and 16384=16384);
        //    //(Byte_Signal_minus.NOPU5-Byte_Signal.NOPU5)>0	4	Кнопка НО на ПУ5 нажата m3104.0-m3104.15	Byte_Signal.NOPU5:=Ord(Swap(buffer_m[4]) and 32768=32768);
        //    [71] = new MessageClass(1, "Высокая температура ПЖТ СД", 4, "Нормальная температура ПЖТ СД"),
        //    [72] = new MessageClass(4, "Кнопка НО на ПУ старшего нажата", 0, ""),
        //    [73] = new MessageClass(0, "", 4, "Кнопка НО на ПУР нажата"),
        //    [74] = new MessageClass(0, "", 4, "Кнопка НО на ПУ1 нажата"),
        //    [75] = new MessageClass(0, "", 4, "Кнопка НО на ПУ2 нажата"),
        //    [76] = new MessageClass(0, "", 4, "Кнопка НО на ПУ3 нажата"),
        //    [77] = new MessageClass(0, "", 4, "Кнопка НО на ПУ4 нажата"),
        //    [78] = new MessageClass(0, "", 4, "Кнопка НО на ПУ5 нажата"),

        //    //byte10  -> buffer_m[5](1-128)
        //    //(Byte_Signal_minus.FOPanPultStar - Byte_Signal.FOPanPultStar) < 0   4   Кнопка ФО на ПУ старшего нажата             m3107.0 - m3107.7 Byte_Signal.FOPanPultStar:= Ord(Swap(buffer_m[5])and 1 = 1);
        //    //(Byte_Signal_minus.FOPU5-Byte_Signal.FOPU5)>0	4	Кнопка ФО на ПУ5 нажата m3107.0-m3107.7	Byte_Signal.FOPU5:=Ord(Swap(buffer_m[5]) and 2=2);
        //    //(Byte_Signal_minus.AOPUR-Byte_Signal.AOPUR)>0	4	Кнопка АО на ПУР нажата m3107.0-m3107.7	Byte_Signal.AOPUR:=Ord(Swap(buffer_m[5]) and 4=4);
        //    //(Byte_Signal_minus.TrazmProval-Byte_Signal.TrazmProval)<0	4	Провал натяжения на разматывателе ФО m3107.0-m3107.7	Byte_Signal.TrazmProval:=Ord(Swap(buffer_m[5]) and 8=8);
        //    //(Byte_Signal_minus.TrazmProval-Byte_Signal.TrazmProval)>0	1	Восстановление натяжения на разматывателе ТД m3107.0-m3107.7	Byte_Signal.TrazmProval:=Ord(Swap(buffer_m[5]) and 8=8);
        //    //(Byte_Signal_minus.T12proval-Byte_Signal.T12proval)<0	4	Провал натяжения в 1 промежутке ФО          m3107.0-m3107.7	Byte_Signal.T12proval:=Ord(Swap(buffer_m[5]) and 16=16);
        //    //(Byte_Signal_minus.T12proval-Byte_Signal.T12proval)>0	1	Восстановление натяжения в 1 промежутке ТД          m3107.0-m3107.7	Byte_Signal.T12proval:=Ord(Swap(buffer_m[5]) and 16=16);
        //    //(Byte_Signal_minus.T23proval-Byte_Signal.T23proval)<0	4	Провал натяжения во 2 промежутке ФО             m3107.0-m3107.7	Byte_Signal.T23proval:=Ord(Swap(buffer_m[5]) and 32=32);
        //    //(Byte_Signal_minus.T23proval-Byte_Signal.T23proval)>0	1	Восстановление натяжения во 2 промежутке ТД             m3107.0-m3107.7	Byte_Signal.T23proval:=Ord(Swap(buffer_m[5]) and 32=32);
        //    //(Byte_Signal_minus.T34proval-Byte_Signal.T34proval)<0	4	Провал натяжения в 3 промежутке ФО          m3107.0-m3107.7	Byte_Signal.T34proval:=Ord(Swap(buffer_m[5]) and 64=64);
        //    //(Byte_Signal_minus.T34proval-Byte_Signal.T34proval)>0	1	Восстановление натяжения в 3 промежутке ТД          m3107.0-m3107.7	Byte_Signal.T34proval:=Ord(Swap(buffer_m[5]) and 64=64);
        //    //(Byte_Signal_minus.T45proval-Byte_Signal.T45proval)<0	4	Провал натяжения в 4 промежутке ФО          m3107.0-m3107.7	Byte_Signal.T45proval:=Ord(Swap(buffer_m[5]) and 128=128);
        //    //(Byte_Signal_minus.T45proval-Byte_Signal.T45proval)>0	1	Восстановление натяжения в 4 промежутке ТД          m3107.0-m3107.7	Byte_Signal.T45proval:=Ord(Swap(buffer_m[5]) and 128=128);
        //    [79] = new MessageClass(4, "Кнопка ФО на ПУ старшего нажата", 0, ""),
        //    [81] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ5 нажата"),
        //    [82] = new MessageClass(0, "", 4, "Кнопка АО на ПУР нажата"),
        //    [83] = new MessageClass(4, "Провал натяжения на разматывателе", 1, "Восстановление натяжения на разматывателе ТД"),
        //    [84] = new MessageClass(4, "Провал натяжения в 1 промежутке ФО ", 1, "Восстановление натяжения в 1 промежутке ТД"),
        //    [85] = new MessageClass(4, "Провал натяжения в 2 промежутке ФО ", 1, "Восстановление натяжения в 2 промежутке ТД"),
        //    [86] = new MessageClass(4, "Провал натяжения в 3 промежутке ФО ", 1, "Восстановление натяжения в 3 промежутке ТД"),
        //    [87] = new MessageClass(4, "Провал натяжения в 4 промежутке ФО ", 1, "Восстановление натяжения в 4 промежутке ТД"),

        //    //byte11  -> buffer_m[5](256-32768)
        //    //(Byte_Signal_minus.Vent_101G - Byte_Signal.Vent_101G) < 0   4   Вентилятор обдува 101Г выключен НО          m3106.0 - m3106.7 Byte_Signal.Vent_101G:= Ord(Swap(buffer_m[5])and 256 = 256);
        //    //(Byte_Signal_minus.Vent_102G-Byte_Signal.Vent_102G)<0	4	Вентилятор обдува 102Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_102G:=Ord(Swap(buffer_m[5]) and 512=512);
        //    //(Byte_Signal_minus.Vent_103G-Byte_Signal.Vent_103G)<0	4	Вентилятор обдува 103Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_103G:=Ord(Swap(buffer_m[5]) and 1024=1024);
        //    //(Byte_Signal_minus.Vent_105G-Byte_Signal.Vent_105G)<0	4	Вентилятор обдува 105Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_105G:=Ord(Swap(buffer_m[5]) and 2048=2048);
        //    //(Byte_Signal_minus.Vent_106G-Byte_Signal.Vent_106G)<0	4	Вентилятор обдува 106Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_106G:=Ord(Swap(buffer_m[5]) and 4096=4096);
        //    //(Byte_Signal_minus.Vent_podpor_PA1 - Byte_Signal.Vent_podpor_PA1) < 0   4   Вентилятор подпора ПА - 1 выключен            m3106.0 - m3106.7 Byte_Signal.Vent_podpor_PA1:= Ord(Swap(buffer_m[5])and 8192 = 8192);
        //    //(Byte_Signal_minus.Vent_112G-Byte_Signal.Vent_112G)<0	4	Вентилятор обдува 112Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_112G:=Ord(Swap(buffer_m[5]) and 16384=16384);
        //    //(Byte_Signal_minus.Vent_111G-Byte_Signal.Vent_111G)<0	4	Вентилятор обдува 111Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_111G:=Ord(Swap(buffer_m[5]) and 32768=32768);
        //    [88] = new MessageClass(4, "Вентилятор обдува 101Г выключен НО", 0, ""),
        //    [89] = new MessageClass(4, "Вентилятор обдува 102Г выключен НО", 0, ""),
        //    [90] = new MessageClass(4, "Вентилятор обдува 103Г выключен НО", 0, ""),
        //    [91] = new MessageClass(4, "Вентилятор обдува 105Г выключен НО", 0, ""),
        //    [92] = new MessageClass(4, "Вентилятор обдува 106Г выключен НО", 0, ""),
        //    [93] = new MessageClass(4, "Вентилятор подпора ПА - 1 выключен", 0, ""),
        //    [94] = new MessageClass(4, "Вентилятор обдува 112Г выключен НО", 0, ""),
        //    [95] = new MessageClass(4, "Вентилятор обдува 111Г выключен НО", 0, ""),

        //    //byte12  -> buffer_m[6](1-128)
        //    //(Byte_Signal_minus.Vent_110G - Byte_Signal.Vent_110G) < 0   4   Вентилятор обдува 110Г выключен НО          m3109.0 - m3109.7 Byte_Signal.Vent_110G:= Ord(Swap(buffer_m[6])and 1 = 1);
        //    //(Byte_Signal_minus.Vent_108G-Byte_Signal.Vent_108G)<0	4	Вентилятор обдува 108Г выключен НО m3109.0-m3109.7	Byte_Signal.Vent_108G:=Ord(Swap(buffer_m[6]) and 2=2);
        //    //(Byte_Signal_minus.Vent_107G-Byte_Signal.Vent_107G)<0	4	Вентилятор обдува 107Г выключен НО m3109.0-m3109.7	Byte_Signal.Vent_107G:=Ord(Swap(buffer_m[6]) and 4=4);
        //    //(Byte_Signal_minus.Vent_podpor_PA2-Byte_Signal.Vent_podpor_PA2)<0	4	Вентилятор подпора ПА-2 выключен m3109.0-m3109.7	Byte_Signal.Vent_podpor_PA2:=Ord(Swap(buffer_m[6]) and 8=8);
        //    //(Byte_Signal_minus.Vent_1kl-Byte_Signal.Vent_1kl)<0	4	Вентилятор обдува ГП 1 клети выключен НО m3109.0-m3109.7	Byte_Signal.Vent_1kl:=Ord(Swap(buffer_m[6]) and 16=16);
        //    //(Byte_Signal_minus.Vent_2kl-Byte_Signal.Vent_2kl)<0	4	Вентилятор обдува ГП 2 клети выключен НО m3109.0-m3109.7	Byte_Signal.Vent_2kl:=Ord(Swap(buffer_m[6]) and 32=32);
        //    //(Byte_Signal_minus.Vent_3kl-Byte_Signal.Vent_3kl)<0	4	Вентилятор обдува ГП 3 клети выключен НО m3109.0-m3109.7	Byte_Signal.Vent_3kl:=Ord(Swap(buffer_m[6]) and 64=64);
        //    //(Byte_Signal_minus.Vent_4kl-Byte_Signal.Vent_4kl)<0	4	Вентилятор обдува ГП 4 клети выключен НО m3109.0-m3109.7	Byte_Signal.Vent_4kl:=Ord(Swap(buffer_m[6]) and 128=128);
        //    [96] = new MessageClass(4, "Вентилятор обдува 110Г выключен НО", 0, ""),
        //    [97] = new MessageClass(4, "Вентилятор обдува 108Г выключен НО", 0, ""),
        //    [98] = new MessageClass(4, "Вентилятор обдува 107Г выключен НО", 0, ""),
        //    [99] = new MessageClass(4, "Вентилятор подпора ПА-2 выключен", 0, ""),
        //    [100] = new MessageClass(4, "Вентилятор обдува ГП 1 клети выключен", 0, ""),
        //    [101] = new MessageClass(4, "Вентилятор обдува ГП 2 клети выключен", 0, ""),
        //    [102] = new MessageClass(4, "Вентилятор обдува ГП 3 клети выключен", 0, ""),
        //    [103] = new MessageClass(4, "Вентилятор обдува ГП 4 клети выключен", 0, ""),

        //    //byte13  -> buffer_m[6](256-32768)
        //    //(Byte_Signal_minus.FOPU4 - Byte_Signal.FOPU4) > 0   4   Кнопка ФО на ПУ4 нажата             m3108.0 - m3108.7 Byte_Signal.FOPU4:= Ord(Swap(buffer_m[6])and 256 = 256);
        //    //(Byte_Signal_minus.FOPU3-Byte_Signal.FOPU3)>0	4	Кнопка ФО на ПУ3 нажата m3108.0-m3108.7	Byte_Signal.FOPU3:=Ord(Swap(buffer_m[6]) and 512=512);
        //    //(Byte_Signal_minus.FOPU2-Byte_Signal.FOPU2)>0	4	Кнопка ФО на ПУ2 нажата m3108.0-m3108.7	Byte_Signal.FOPU2:=Ord(Swap(buffer_m[6]) and 1024=1024);
        //    //(Byte_Signal_minus.FOPU1-Byte_Signal.FOPU1)>0	4	Кнопка ФО на ПУ1 нажата m3108.0-m3108.7	Byte_Signal.FOPU1:=Ord(Swap(buffer_m[6]) and 2048=2048);
        //    //(Byte_Signal_minus.FOPUR-Byte_Signal.FOPUR)>0	4	Кнопка ФО на ПУР нажата m3108.0-m3108.7	Byte_Signal.FOPUR:=Ord(Swap(buffer_m[6]) and 4096=4096);
        //    //(Byte_Signal_minus.AOSUSknopka-Byte_Signal.AOSUSknopka)>0	4	Кнопка АО на СУС нажата m3108.0-m3108.7	Byte_Signal.AOSUSknopka:=Ord(Swap(buffer_m[6]) and 8192=8192);
        //    //(Byte_Signal_minus.AO5klet-Byte_Signal.AO5klet)>0	4	Кнопка АО на ПУ5 нажата m3108.0-m3108.7	Byte_Signal.AO5Klet:=Ord(Swap(buffer_m[6]) and 16384=16384);
        //    [104] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ4 нажата"),
        //    [105] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ3 нажата"),
        //    [106] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ2 нажата"),
        //    [107] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ1 нажата"),
        //    [108] = new MessageClass(0, "", 4, "Кнопка ФО на ПУР нажата"),
        //    [109] = new MessageClass(0, "", 4, "Кнопка АО на СУС нажата"),
        //    [110] = new MessageClass(0, "", 4, "Кнопка АО на ПУ5 нажата"),
        //    [111] = new MessageClass(0, "", 0, ""),

        //    //byte14  -> buffer_m[7](1-128)
        //    //(Byte_Signal_minus.KnAOsus - Byte_Signal.KnAOsus) < 0   4   Кнопка АО на СУС нажата         m3111.0 - m3111.7 Byte_Signal.KnAOsus:= Ord(Swap(buffer_m[7])and 4 = 4);
        //    [112] = new MessageClass(0, "", 0, ""),
        //    [113] = new MessageClass(0, "", 0, ""),
        //    [114] = new MessageClass(4, "Кнопка АО на СУС нажата", 0, ""),
        //    [115] = new MessageClass(0, "", 0, ""),
        //    [116] = new MessageClass(0, "", 0, ""),
        //    [117] = new MessageClass(0, "", 0, ""),
        //    [118] = new MessageClass(0, "", 0, ""),
        //    [119] = new MessageClass(0, "", 0, ""),

        //    //byte15  -> buffer_m[7](256-32768)
        //    //(Byte_Signal_minus.Vent_5kl - Byte_Signal.Vent_5kl) < 0 4   Вентилятор обдува ГП 5 клети выключен НО            m3110.0 - m3110.7 Byte_Signal.Vent_5kl:= Ord(Swap(buffer_m[7])and 256 = 256);
        //    //(Byte_Signal_minus.Vent_podpor_GP1-Byte_Signal.Vent_podpor_GP1)<0	4	Вентилятор подпора ГП-1 выключен m3110.0-m3110.7	Byte_Signal.Vent_podpor_GP1:=Ord(Swap(buffer_m[7]) and 1024=1024);
        //    //(Byte_Signal_minus.Vent_podpor_GP2-Byte_Signal.Vent_podpor_GP2)<0	4	Вентилятор подпора ГП-2 выключен m3110.0-m3110.7	Byte_Signal.Vent_podpor_GP2:=Ord(Swap(buffer_m[7]) and 2048=2048);
        //    //(Byte_Signal_minus.Vent_NV-Byte_Signal.Vent_NV)<0	4	Вентилятор обдува нажимных винтов выключен НО           m3110.0-m3110.7	Byte_Signal.Vent_NV:=Ord(Swap(buffer_m[7]) and 4096=4096);
        //    //(Byte_Signal_minus.Dv2pr09sec3vv-Byte_Signal.Dv2pr09sec3vv)>0	4	ХХХ ПЕРЕГРУЗ ГП ХХХ         m3110.0-m3110.7	Byte_Signal.Dv2pr09sec3vv:=Ord(Swap(buffer_m[7]) and 8192=8192);
        //    [120] = new MessageClass(4, "Вентилятор обдува ГП 5 клети выключен НО", 0, ""),
        //    [121] = new MessageClass(0, "", 0, ""),
        //    [122] = new MessageClass(4, "Вентилятор подпора ГП-1 выключен", 0, ""),
        //    [123] = new MessageClass(4, "Вентилятор подпора ГП-2 выключен", 0, ""),
        //    [124] = new MessageClass(4, "Вентилятор обдува нажимных винтов выключен", 0, ""),
        //    [125] = new MessageClass(4, "ХХХ ПЕРЕГРУЗ ГП ХХХ", 0, ""),
        //    [126] = new MessageClass(0, "", 0, ""),
        //};
        //Dictionary<int, string> MessageStan = new Dictionary<int, string>();
        //MessageStan.Add(1,"ddd");

    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Prodave stan;
        int Connect = 0;
        Timer TTimer100ms;
        Timer TTimerMessage;
        Timer TTimerSQL;
        Timer TTimer1s;
        Timer TTimer250msNet;

        DateTime dt1s = DateTime.Now;
        DateTime dtMessage = DateTime.Now;
        DateTime dtSQL = DateTime.Now;
        DateTime dt100ms = DateTime.Now;

        SolidColorBrush offLed = new SolidColorBrush(Color.FromRgb(160, 160, 160));
        SolidColorBrush onOK = new SolidColorBrush(Color.FromRgb(130, 190, 125));
        SolidColorBrush onError = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        ClassStan clStan;


        static byte[] buffer;
        static byte[] bufferPLC;
        static byte[] bufferSQL;
        static byte[] bufferMessage;
        static byte[] bufferMessageOld;
        static byte[] buffer1s;
        static byte[] bufferNet;
        static readonly object locker = new object();
        static readonly object locker2 = new object();

        SqlConnection con;
        string connectingString;

        float speed4kl, H5_work, h5w, Bw, D_tek_mot, B_Work, D_pred_mot = 0, Ves_Work, Dlina_Work;
        DateTime Time_Start, Time_Stop, DT;


        DataTable dt101msStan;

        Dictionary<int, MessageClass> MessageStan = new Dictionary<int, MessageClass>()
        {
            //byte0 -> buffer_m[0](1-128)
            //(Byte_Signal_minus.RegTD-Byte_Signal.RegTD)<0   1   Режим ТАК ДЕРЖАТЬ           mb3067(m151.0-m151.7)   Byte_Signal.RegTD:=Ord(Swap(buffer_m[0]) and 1=1);
            //(Byte_Signal_minus.RegRazg-Byte_Signal.RegRazg)<0   2   Режим РАЗГОНА           mb3067(m151.0-m151.7)   Byte_Signal.RegRazg:=Ord(Swap(buffer_m[0])and 2=2);
            //(Byte_Signal_minus.RegNO-Byte_Signal.RegNO)<0	3	Режим НОРМАЛЬНОГО ОСТАНОВА mb3067(m151.0-m151.7)   Byte_Signal.RegNO:=Ord(Swap(buffer_m[0]) and 4=4);
            //(Byte_Signal_minus.RegFO-Byte_Signal.RegFO)<0	4	Режим ФОРСИРОВАННОГО ОСТАНОВА mb3067(m151.0-m151.7)   Byte_Signal.RegFO:=Ord(Swap(buffer_m[0]) and 8=8);
            //(Byte_Signal_minus.Vip-Byte_Signal.Vip)<0	5	Режим ВЫПУСКА           mb3067(m151.0-m151.7)   Byte_Signal.Vip:=Ord(Swap(buffer_m[0]) and 16=16);
            //(Byte_Signal_minus.T10-Byte_Signal.T10)<0   6   Натяжение в 1 промежутке            mb3067(m151.0-m151.7)   Byte_Signal.T10:=Ord(Swap(buffer_m[0]) and 32=32);
            //(Byte_Signal_minus.T10-Byte_Signal.T10)>0	7	Отсутствие натяжения в 1 промежутке mb3067(m151.0-m151.7)   Byte_Signal.T10:=Ord(Swap(buffer_m[0]) and 32=32);
            //(Byte_Signal_minus.T20-Byte_Signal.T20)<0	6	Натяжение во 2 промежутке mb3067(m151.0-m151.7)   Byte_Signal.T20:=Ord(Swap(buffer_m[0]) and 64=64);
            //(Byte_Signal_minus.T20-Byte_Signal.T20)>0	7	Отсутствие натяжения во 2 промежутке mb3067(m151.0-m151.7)   Byte_Signal.T20:=Ord(Swap(buffer_m[0]) and 64=64);
            //(Byte_Signal_minus.T30-Byte_Signal.T30)<0	6	Натяжение в 3 промежутке mb3067(m151.0-m151.7)   Byte_Signal.T30:=Ord(Swap(buffer_m[0]) and 128=128);
            //(Byte_Signal_minus.T30-Byte_Signal.T30)>0	7	Отсутствие натяжения в 3 промежутке mb3067(m151.0-m151.7)   Byte_Signal.T30:=Ord(Swap(buffer_m[0]) and 128=128);
            [0] = new MessageClass(1, "Режим ТАК ДЕРЖАТЬ", 0, ""),
            [1] = new MessageClass(2, "Режим РАЗГОНА", 0, ""),
            [2] = new MessageClass(3, "Режим НОРМАЛЬНОГО ОСТАНОВА", 0, ""),
            [3] = new MessageClass(4, "Режим ФОРСИРОВАННОГО ОСТАНОВА", 0, ""),
            [4] = new MessageClass(5, "Режим ВЫПУСКА", 0, ""),
            [5] = new MessageClass(6, "Натяжение в 1 промежутке", 7, "Отсутствие натяжения в 1"),
            [6] = new MessageClass(6, "Натяжение в 2 промежутке", 7, "Отсутствие натяжения в 3"),
            [7] = new MessageClass(6, "Натяжение в 2 промежутке", 7, "Отсутствие натяжения в 3"),

            //byte1 -> buffer_m[0](256-32768)
            //(Byte_Signal_minus.ZS-Byte_Signal.ZS)<0	2	Кнопка ЗАПРАВКА			mb3066(m150.0-m150.7)	Byte_Signal.ZS:=Ord(Swap(buffer_m[0])and 256=256);
            //(Byte_Signal_minus.TD-Byte_Signal.TD)<0	1	Ключ ТАК ДЕРЖАТЬ			mb3066(m150.0-m150.7)	Byte_Signal.TD:=Ord(Swap(buffer_m[0])and 512=512);
            //(Byte_Signal_minus.RS-Byte_Signal.RS)<0	2	Ключ РАЗГОН			mb3066(m150.0-m150.7)	Byte_Signal.RS:=Ord(Swap(buffer_m[0])and 1024=1024);
            //(Byte_Signal_minus.NO-Byte_Signal.NO)<0	3	Кнопка НОРМАЛЬНЫЙ ОСТАНОВ 			mb3066(m150.0-m150.7)	Byte_Signal.NO:=Ord(Swap(buffer_m[0])and 2048=2048);
            //(Byte_Signal_minus.FO-Byte_Signal.FO)<0	4	Кнопка ФОРСИРОВАННЫЙ ОСТАНОВ 			mb3066(m150.0-m150.7)	Byte_Signal.FO:=Ord(Swap(buffer_m[0])and 4096=4096);
            //(Byte_Signal_minus.MaxSpeedPeregr-Byte_Signal.MaxSpeedPeregr)<0	6	Максимальный перегруз 			mb3066(m150.0-m150.7)	Byte_Signal.MaxSpeedPeregr:=Ord(Swap(buffer_m[0])and 8192=8192);
            //(Byte_Signal_minus.MaxSpeedPeregr-Byte_Signal.MaxSpeedPeregr)>0	7	Отсутствие максимального перегруз			mb3066(m150.0-m150.7)	Byte_Signal.MaxSpeedPeregr:=Ord(Swap(buffer_m[0])and 8192=8192);
            //(Byte_Signal_minus.UstavkaSpeed - Byte_Signal.UstavkaSpeed) < 0 1   Уставка рабочей скорости            mb3066(m150.0 - m150.7)   Byte_Signal.UstavkaSpeed:= Ord(Swap(buffer_m[0])and 16384 = 16384);
            //(Byte_Signal_minus.MaxSpeed-Byte_Signal.MaxSpeed)<0	2	Перегруз по скорости mb3066(m150.0-m150.7)   Byte_Signal.MaxSpeed:=Ord(Swap(buffer_m[0]) and 32768=32768);
            [8] = new MessageClass(2, "Кнопка ЗАПРАВКА", 0, ""),
            [9] = new MessageClass(1, "Ключ ТАК ДЕРЖАТЬ", 0, ""),
            [10] = new MessageClass(2, "Ключ РАЗГОН", 0, ""),
            [11] = new MessageClass(3, "Кнопка НОРМАЛЬНЫЙ ОСТАНОВ", 0, ""),
            [12] = new MessageClass(4, "Кнопка ФОРСИРОВАННЫЙ ОСТАНОВ", 0, ""),
            [13] = new MessageClass(6, "Максимальный перегруз", 7, "Отсутствие максимального перегруз"),
            [14] = new MessageClass(1, "Уставка рабочей скорости", 0, ""),
            [15] = new MessageClass(2, "Перегруз по скорости", 0, ""),

            //byte2  -> buffer_m[1](1-128)
            //(Byte_Signal_minus.LKmot - Byte_Signal.LKmot) < 0   2   ЛК моталки включены             mb3069(m153.0 - m153.7)    Byte_Signal.LKmot:= Ord(Swap(buffer_m[1])and 1 = 1);
            //(Byte_Signal_minus.LKmot-Byte_Signal.LKmot)>0	6	ЛК моталки выключены mb3069(m153.0-m153.7)    Byte_Signal.LKmot:=Ord(Swap(buffer_m[1]) and 1=1);
            //(Byte_Signal_minus.LKrazm-Byte_Signal.LKrazm)<0	2	ЛК разматывателя включены mb3069(m153.0-m153.7)   Byte_Signal.LKrazm:=Ord(Swap(buffer_m[1]) and 2=2);
            //(Byte_Signal_minus.LKrazm-Byte_Signal.LKrazm)>0	6	ЛК разматывателя выключены mb3069(m153.0-m153.7)   Byte_Signal.LKrazm:=Ord(Swap(buffer_m[1]) and 2=2);
            //(Byte_Signal_minus.Got_64kg - Byte_Signal.Got_64kg) < 0 1   Гидравлика 64 кг готова             mb3069(m153.0 - m153.7)   Byte_Signal.Got_64kg:= Ord(Swap(buffer_m[1])and 4 = 4);
            //(Byte_Signal_minus.Got_64kg-Byte_Signal.Got_64kg)>0	4	Гидравлика 64 кг не готова mb3069(m153.0-m153.7)   Byte_Signal.Got_64kg:=Ord(Swap(buffer_m[1]) and 4=4);
            //(Byte_Signal_minus.Rnz12 - Byte_Signal.Rnz12) > 0   6   РНЗ 12 включено             mb3069(m153.0 - m153.7)   Byte_Signal.Rnz12:= Ord(Swap(buffer_m[1])and 8 = 8);
            //(Byte_Signal_minus.Rnz12-Byte_Signal.Rnz12)<0	7	РНЗ 12 выключено mb3069(m153.0-m153.7)   Byte_Signal.Rnz12:=Ord(Swap(buffer_m[1]) and 8=8);
            //(Byte_Signal_minus.Rnz23-Byte_Signal.Rnz23)<0	6	РНЗ 23 включено mb3069(m153.0-m153.7)   Byte_Signal.Rnz23:=Ord(Swap(buffer_m[1]) and 16=16);
            //(Byte_Signal_minus.Rnz23-Byte_Signal.Rnz23)>0	7	РНЗ 23 выключено mb3069(m153.0-m153.7)   Byte_Signal.Rnz23:=Ord(Swap(buffer_m[1]) and 16=16);
            //(Byte_Signal_minus.Rnz34-Byte_Signal.Rnz34)<0	6	РНЗ 34 включено mb3069(m153.0-m153.7)   Byte_Signal.Rnz34:=Ord(Swap(buffer_m[1]) and 32=32);
            //(Byte_Signal_minus.Rnz34-Byte_Signal.Rnz34)>0	7	РНЗ 34 выключено mb3069(m153.0-m153.7)   Byte_Signal.Rnz34:=Ord(Swap(buffer_m[1]) and 32=32);
            //(Byte_Signal_minus.GrtVkl-Byte_Signal.GrtVkl)<0	6	ГРТ включено            mb3069(m153.0-m153.7)   Byte_Signal.GrtVkl:=Ord(Swap(buffer_m[1]) and 64=64);
            //(Byte_Signal_minus.GrtVkl-Byte_Signal.GrtVkl)>0	7	ГРТ выключено           mb3069(m153.0-m153.7)   Byte_Signal.GrtVkl:=Ord(Swap(buffer_m[1]) and 64=64);
            //(Byte_Signal_minus.TrtVkl-Byte_Signal.TrtVkl)<0	6	ТРТ включено            mb3069(m153.0-m153.7)   Byte_Signal.TrtVkl:=Ord(Swap(buffer_m[1]) and 128=128);
            //(Byte_Signal_minus.TrtVkl-Byte_Signal.TrtVkl)>0	7	ТРТ выключено           mb3069(m153.0-m153.7)   Byte_Signal.TrtVkl:=Ord(Swap(buffer_m[1]) and 128=128);
            [16] = new MessageClass(2, "ЛК моталки включены", 6, "ЛК моталки выключены"),
            [17] = new MessageClass(2, "ЛК разматывателя включены", 6, "ЛК разматывателя выключены"),
            [18] = new MessageClass(1, "Гидравлика 64 кг готова", 4, "Гидравлика 64 кг не готова"),
            [19] = new MessageClass(7, "РНЗ 12 выключено", 6, "РНЗ 12 включено"),
            [20] = new MessageClass(6, "РНЗ 23 включено", 7, "РНЗ 23 выключено"),
            [21] = new MessageClass(6, "РНЗ 34 включено", 7, "РНЗ 34 выключено"),
            [22] = new MessageClass(6, "ГРТ включено", 7, "ГРТ выключено"),
            [23] = new MessageClass(6, "ТРТ включено", 7, "ТРТ выключено"),


            //byte3-> buffer_m[1](256-32768)
            //(Byte_Signal_minus.T40 - Byte_Signal.T40) < 0   6   Натяжение в 4 промежутке            mb3068(m152.0 - m152.7)   Byte_Signal.T40:= Ord(Swap(buffer_m[1]) and 256 = 256);
            //(Byte_Signal_minus.T40-Byte_Signal.T40)>0	7	Отсутствие натяжения в 4 промежутке mb3068(m152.0-m152.7)   Byte_Signal.T40:=Ord(Swap(buffer_m[1]) and 256=256);
            //(Byte_Signal_minus.Tmot-Byte_Signal.Tmot)<0	6	Натяжение на моталке mb3068(m152.0-m152.7)   Byte_Signal.Tmot:=Ord(Swap(buffer_m[1]) and 512=512);
            //(Byte_Signal_minus.Tmot-Byte_Signal.Tmot)>0	7	Отсутствие натяжения на моталке             mb3068(m152.0-m152.7)   Byte_Signal.Tmot:=Ord(Swap(buffer_m[1]) and 512=512);
            //(Byte_Signal_minus.Trazm-Byte_Signal.Trazm)<0	6	Натяжение на разматывателе mb3068(m152.0-m152.7)   Byte_Signal.Trazm:=Ord(Swap(buffer_m[1]) and 1024=1024);
            //(Byte_Signal_minus.Trazm-Byte_Signal.Trazm)>0	7	Отсутствие натяжения на разматывателе           mb3068(m152.0-m152.7)   Byte_Signal.Trazm:=Ord(Swap(buffer_m[1]) and 1024=1024);
            //(Byte_Signal_minus.LK1-Byte_Signal.LK1)<0	2	ЛК клети 1 включены mb3068(m152.0-m152.7)   Byte_Signal.LK1:=Ord(Swap(buffer_m[1]) and 2048=2048);
            //(Byte_Signal_minus.LK1-Byte_Signal.LK1)>0	6	ЛК клети 1 выключены mb3068(m152.0-m152.7)   Byte_Signal.LK1:=Ord(Swap(buffer_m[1]) and 2048=2048);
            //(Byte_Signal_minus.LK2-Byte_Signal.LK2)<0	2	ЛК клети 2 включены mb3068(m152.0-m152.7)   Byte_Signal.LK2:=Ord(Swap(buffer_m[1]) and 4096=4096);
            //(Byte_Signal_minus.LK2-Byte_Signal.LK2)>0	6	ЛК клети 2 выключены mb3068(m152.0-m152.7)   Byte_Signal.LK2:=Ord(Swap(buffer_m[1]) and 4096=4096);
            //(Byte_Signal_minus.LK3-Byte_Signal.LK3)<0	2	ЛК клети 3 включены mb3068(m152.0-m152.7)   Byte_Signal.LK3:=Ord(Swap(buffer_m[1]) and 8192=8192);
            //(Byte_Signal_minus.LK3-Byte_Signal.LK3)>0	6	ЛК клети 3 выключены mb3068(m152.0-m152.7)   Byte_Signal.LK3:=Ord(Swap(buffer_m[1]) and 8192=8192);
            //(Byte_Signal_minus.LK4-Byte_Signal.LK4)<0	2	ЛК клети 4 включены mb3068(m152.0-m152.7)   Byte_Signal.LK4:=Ord(Swap(buffer_m[1]) and 16384=16384);
            //(Byte_Signal_minus.LK4-Byte_Signal.LK4)>0	6	ЛК клети 4 выключены mb3068(m152.0-m152.7)   Byte_Signal.LK4:=Ord(Swap(buffer_m[1]) and 16384=16384);
            //(Byte_Signal_minus.LK5-Byte_Signal.LK5)<0	2	ЛК клети 5 включены mb3068(m152.0-m152.7)   Byte_Signal.LK5:=Ord(Swap(buffer_m[1]) and 32768=32768);
            //(Byte_Signal_minus.LK5-Byte_Signal.LK5)>0	6	ЛК клети 5 выключены mb3068(m152.0-m152.7)   Byte_Signal.LK5:=Ord(Swap(buffer_m[1]) and 32768=32768);
            [24] = new MessageClass(6, "Натяжение в 4 промежутке", 7, "Отсутствие натяжения в 4 промежутке"),
            [25] = new MessageClass(6, "Натяжение на моталке", 0, "Отсутствие натяжения на моталке"),
            [26] = new MessageClass(6, "Натяжение на разматывателе", 7, "Отсутствие натяжения на разматывателе"),
            [27] = new MessageClass(2, "ЛК клети 1 включены", 6, "ЛК клети 1 выключены"),
            [28] = new MessageClass(2, "ЛК клети 2 включены", 6, "ЛК клети 2 выключены"),
            [29] = new MessageClass(2, "ЛК клети 3 включены", 6, "ЛК клети 3 выключены"),
            [30] = new MessageClass(2, "ЛК клети 4 включены", 6, "ЛК клети 4 выключены"),
            [31] = new MessageClass(2, "ЛК клети 5 включены", 6, "ЛК клети 5 выключены"),

            //byte4  -> buffer_m[2](1-128)
            //(Byte_Signal_minus.NalPol - Byte_Signal.NalPol) < 0 5   Наличие полосы в толщиномере за 5 клетью            mb3071(m155.0 - m155.7)   Byte_Signal.NalPol:= Ord(Swap(buffer_m[2]) and 1 = 1);
            //(Byte_Signal_minus.NalPol-Byte_Signal.NalPol)>0	7	Отсутствие полосы в толщиномере за 5 клетью mb3071(m155.0-m155.7)   Byte_Signal.NalPol:=Ord(Swap(buffer_m[2]) and 1=1);
            //(Byte_Signal_minus.Knp-Byte_Signal.Knp)<0	1	Ноль задания скорости mb3071(m155.0-m155.7)   Byte_Signal.Knp:=Ord(Swap(buffer_m[2]) and 2=2);
            //(Byte_Signal_minus.Knp-Byte_Signal.Knp)>0	2	Поехали mb3071(m155.0-m155.7)   Byte_Signal.Knp:=Ord(Swap(buffer_m[2]) and 2=2);
            //(Byte_Signal_minus.GotStan-Byte_Signal.GotStan)<0	1	Сборка схемы стана mb3071(m155.0-m155.7)   Byte_Signal.GotStan:=Ord(Swap(buffer_m[2]) and 4=4);
            //(Byte_Signal_minus.GotStan-Byte_Signal.GotStan)>0	4	Развал схемы стана mb3071(m155.0-m155.7)   Byte_Signal.GotStan:=Ord(Swap(buffer_m[2]) and 4=4);
            //(Byte_Signal_minus.MaxV1-Byte_Signal.MaxV1)<0	4	Максимальная скорость клети 1 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV1:=Ord(Swap(buffer_m[2]) and 8=8);
            //(Byte_Signal_minus.MaxV1-Byte_Signal.MaxV1)>0	5	Конец максимальной скорости клети 1 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV1:=Ord(Swap(buffer_m[2]) and 8=8);
            //(Byte_Signal_minus.MaxV2-Byte_Signal.MaxV2)<0	4	Максимальная скорость клети 2 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV2:=Ord(Swap(buffer_m[2]) and 16=16);
            //(Byte_Signal_minus.MaxV2-Byte_Signal.MaxV2)>0	5	Конец максимальной скорости клети 2 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV2:=Ord(Swap(buffer_m[2]) and 16=16);
            //(Byte_Signal_minus.MaxV3-Byte_Signal.MaxV3)<0	4	Максимальная скорость клети 3 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV3:=Ord(Swap(buffer_m[2]) and 32=32);
            //(Byte_Signal_minus.MaxV3-Byte_Signal.MaxV3)>0	5	Конец максимальной скорости клети 3 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV3:=Ord(Swap(buffer_m[2]) and 32=32);
            //(Byte_Signal_minus.MaxV4-Byte_Signal.MaxV4)<0	4	Максимальная скорость клети 4 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV4:=Ord(Swap(buffer_m[2]) and 64=64);
            //(Byte_Signal_minus.MaxV4-Byte_Signal.MaxV4)>0	5	Конец максимальной скорости клети 4 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV4:=Ord(Swap(buffer_m[2]) and 64=64);
            //(Byte_Signal_minus.MaxV5-Byte_Signal.MaxV5)<0	4	Максимальная скорость клети 5 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV5:=Ord(Swap(buffer_m[2]) and 128=128);
            //(Byte_Signal_minus.MaxV5-Byte_Signal.MaxV5)>0	5	Конец максимальной скорости клети 5 			mb3071(m155.0-m155.7)   Byte_Signal.MaxV5:=Ord(Swap(buffer_m[2]) and 128=128);
            [32] = new MessageClass(5, "Наличие полосы в толщиномере за 5 клетью", 7, "Отсутствие полосы в толщиномере за 5 клетью"),
            [33] = new MessageClass(1, "Ноль задания скорости", 2, "Поехали"),
            [34] = new MessageClass(1, "Сборка схемы стана", 4, "Развал схемы стана"),
            [35] = new MessageClass(4, "Максимальная скорость клети 1", 5, "Конец максимальной скорости клети 1"),
            [36] = new MessageClass(4, "Максимальная скорость клети 2", 5, "Конец максимальной скорости клети 2"),
            [37] = new MessageClass(4, "Максимальная скорость клети 3", 5, "Конец максимальной скорости клети 3"),
            [38] = new MessageClass(4, "Максимальная скорость клети 4", 5, "Конец максимальной скорости клети 4"),
            [39] = new MessageClass(4, "Максимальная скорость клети 5", 5, "Конец максимальной скорости клети 5"),

            //byte5-> buffer_m[2](256-32768)
            // (Byte_Signal_minus.RKDVVkl - Byte_Signal.RKDVVkl) < 0   6   РКДВ включен            mb3070(m154.0 - m154.7)   Byte_Signal.RKDVVkl:= Ord(Swap(buffer_m[2]) and 256 = 256);
            //(Byte_Signal_minus.RKDVVkl-Byte_Signal.RKDVVkl)>0	7	РКДВ выключен           mb3070(m154.0-m154.7)   Byte_Signal.RKDVVkl:=Ord(Swap(buffer_m[2]) and 256=256);
            //(Byte_Signal_minus.RpvVkl-Byte_Signal.RpvVkl)<0	6	РПВ включен             mb3070(m154.0-m154.7)   Byte_Signal.RpvVkl:=Ord(Swap(buffer_m[2]) and 512=512);
            //(Byte_Signal_minus.RpvVkl-Byte_Signal.RpvVkl)>0	7	РПВ выключен            mb3070(m154.0-m154.7)   Byte_Signal.RpvVkl:=Ord(Swap(buffer_m[2]) and 512=512);
            //(Byte_Signal_minus.Rnv12-Byte_Signal.Rnv12)<0	1	РНВ12 включен           mb3070(m154.0-m154.7)   Byte_Signal.Rnv12:=Ord(Swap(buffer_m[2]) and 1024=1024);
            //(Byte_Signal_minus.Rnv12-Byte_Signal.Rnv12)>0	4	РНВ12 выключен          mb3070(m154.0-m154.7)   Byte_Signal.Rnv12:=Ord(Swap(buffer_m[2]) and 1024=1024);
            //(Byte_Signal_minus.Rnv23-Byte_Signal.Rnv23)<0	1	РНВ23 включен           mb3070(m154.0-m154.7)   Byte_Signal.Rnv23:=Ord(Swap(buffer_m[2]) and 2048=2048);
            //(Byte_Signal_minus.Rnv23-Byte_Signal.Rnv23)>0	4	РНВ23 выключен          mb3070(m154.0-m154.7)   Byte_Signal.Rnv23:=Ord(Swap(buffer_m[2]) and 2048=2048);
            //(Byte_Signal_minus.Rnv34-Byte_Signal.Rnv34)<0	1	РНВ34 включен           mb3070(m154.0-m154.7)   Byte_Signal.Rnv34:=Ord(Swap(buffer_m[2]) and 4096=4096);
            //(Byte_Signal_minus.Rnv34-Byte_Signal.Rnv34)>0	4	РНВ34 выключен          mb3070(m154.0-m154.7)   Byte_Signal.Rnv34:=Ord(Swap(buffer_m[2]) and 4096=4096);
            //(Byte_Signal_minus.Rnz45 - Byte_Signal.Rnz45) < 0   6   РН 45 включено          mb3070(m154.0 - m154.7)   Byte_Signal.Rnz45:= Ord(Swap(buffer_m[2]) and 8192 = 8192);
            //(Byte_Signal_minus.Rnz45-Byte_Signal.Rnz45)>0	7	РН 45 выключено mb3070(m154.0-m154.7)   Byte_Signal.Rnz45:=Ord(Swap(buffer_m[2]) and 8192=8192);
            //(Byte_Signal_minus.Rtv-Byte_Signal.Rtv)<0	1	РТВ включен             mb3070(m154.0-m154.7)   Byte_Signal.Rtv:=Ord(Swap(buffer_m[2]) and 16384=16384);
            //(Byte_Signal_minus.Rtv-Byte_Signal.Rtv)>0	7	РТВ выключен            mb3070(m154.0-m154.7)   Byte_Signal.Rtv:=Ord(Swap(buffer_m[2]) and 16384=16384);
            //(Byte_Signal_minus.Got_100kg-Byte_Signal.Got_100kg)<0	1	Гидравлика 100 кг готова            mb3070(m154.0-m154.7)   Byte_Signal.Got_100kg:=Ord(Swap(buffer_m[2]) and 32768=32768);
            //(Byte_Signal_minus.Got_100kg-Byte_Signal.Got_100kg)>0	4	Гидравлика 100 кг не готова mb3070(m154.0-m154.7)   Byte_Signal.Got_100kg:=Ord(Swap(buffer_m[2]) and 32768=32768);
            [40] = new MessageClass(6, "РКДВ включен", 6, "РКДВ выключен"),
            [41] = new MessageClass(6, "РПВ включен", 6, "РПВ выключен"),
            [42] = new MessageClass(1, "РНВ12 включен", 4, "РНВ12 выключен"),
            [43] = new MessageClass(1, "РНВ23 включен", 4, "РНВ23 выключен"),
            [44] = new MessageClass(1, "РНВ34 включен", 4, "РНВ34 выключен"),
            [45] = new MessageClass(6, "РН45 включено", 7, "РН45 выключено"),
            [46] = new MessageClass(1, "РТВ включен", 7, "РТВ выключен"),
            [47] = new MessageClass(1, "Гидравлика 100 кг готова", 4, "Гидравлика 100 кг не готова"),

            //byte6  -> buffer_m[3](1-128)
            //(Byte_Signal_minus.g12 - Byte_Signal.g12) < 0   1   ПЖТ Ж - 12 готова             m3103.0 - m3103.7 Byte_Signal.g12:= Ord(Swap(buffer_m[3]) and 1 = 1);
            //(Byte_Signal_minus.g12-Byte_Signal.g12)>0	4	ПЖТ Ж-12 не готова          m3103.0-m3103.7	Byte_Signal.g12:=Ord(Swap(buffer_m[3]) and 1=1);
            //(Byte_Signal_minus.g13-Byte_Signal.g13)<0	1	ПЖТ Ж-13 готова m3103.0-m3103.7	Byte_Signal.g13:=Ord(Swap(buffer_m[3]) and 2=2);
            //(Byte_Signal_minus.g13-Byte_Signal.g13)>0	4	ПЖТ Ж-13 не готова          m3103.0-m3103.7	Byte_Signal.g13:=Ord(Swap(buffer_m[3]) and 2=2);
            //(Byte_Signal_minus.g14-Byte_Signal.g14)<0	1	ПЖТ Ж-14 готова m3103.0-m3103.7	Byte_Signal.g14:=Ord(Swap(buffer_m[3]) and 4=4);
            //(Byte_Signal_minus.g14-Byte_Signal.g14)>0	4	ПЖТ Ж-14 не готова          m3103.0-m3103.7	Byte_Signal.g14:=Ord(Swap(buffer_m[3]) and 4=4);
            //(Byte_Signal_minus.g15-Byte_Signal.g15)>0	1	Смазка Ж-15 готова m3103.0-m3103.7	Byte_Signal.g15:=Ord(Swap(buffer_m[3]) and 8=8);
            //(Byte_Signal_minus.g15-Byte_Signal.g15)<0	4	Смазка Ж-15 не готова           m3103.0-m3103.7	Byte_Signal.g15:=Ord(Swap(buffer_m[3]) and 8=8);
            //(Byte_Signal_minus.g16-Byte_Signal.g16)<0	1	Смазка Ж-16 готова m3103.0-m3103.7	Byte_Signal.g16:=Ord(Swap(buffer_m[3]) and 16=16);
            //(Byte_Signal_minus.g16-Byte_Signal.g16)>0	4	Смазка Ж-16 не готова           m3103.0-m3103.7	Byte_Signal.g16:=Ord(Swap(buffer_m[3]) and 16=16);
            //(Byte_Signal_minus.NatshUsl-Byte_Signal.NatshUsl)<0	5	Начальные условия           m3103.0-m3103.7	Byte_Signal.NatshUsl:=Ord(Swap(buffer_m[3]) and 32=32);
            //(Byte_Signal_minus.GotEmuls-Byte_Signal.GotEmuls)<0	5	Эмульсионная система готова m3103.0-m3103.7	Byte_Signal.GotEmuls:=Ord(Swap(buffer_m[3]) and 64=64);
            //(Byte_Signal_minus.GotEmuls-Byte_Signal.GotEmuls)>0	7	Эмульсионная система не готова          m3103.0-m3103.7	Byte_Signal.GotEmuls:=Ord(Swap(buffer_m[3]) and 64=64);
            //(Byte_Signal_minus.g17-Byte_Signal.g17)<0	1	Смазка Ж-17 готова m3103.0-m3103.7	Byte_Signal.g17:=Ord(Swap(buffer_m[3]) and 128=128);
            //(Byte_Signal_minus.g17-Byte_Signal.g17)>0	4	Смазка Ж-17 не готова           m3103.0-m3103.7	Byte_Signal.g17:=Ord(Swap(buffer_m[3]) and 128=128);
            [48] = new MessageClass(1, "ПЖТ Ж - 12 готова", 4, "ПЖТ Ж-12 не готова"),
            [49] = new MessageClass(1, "ПЖТ Ж - 13 готова", 4, "ПЖТ Ж-13 не готова"),
            [50] = new MessageClass(1, "ПЖТ Ж - 14 готова", 4, "ПЖТ Ж-14 не готова"),
            [51] = new MessageClass(1, "Смазка Ж-15 готова", 4, "Смазка Ж-15 не готова"),
            [52] = new MessageClass(1, "Смазка Ж-16 готова", 4, "Смазка Ж-16 не готова"),
            [53] = new MessageClass(5, "Начальные условия", 0, ""),
            [54] = new MessageClass(5, "Эмульсионная система готова", 7, "Эмульсионная система не готова"),
            [55] = new MessageClass(1, "Смазка Ж-17 готова", 4, "Смазка Ж-17 не готова"),

            //byte7  -> buffer_m[3](256-32768)
            //(Byte_Signal_minus.g18 - Byte_Signal.g18) < 0   1   Смазка Ж - 18 готова          m3102.0 - m3102.7 Byte_Signal.g18:= Ord(Swap(buffer_m[3])and 256 = 256);
            //(Byte_Signal_minus.g18-Byte_Signal.g18)>0	4	Смазка Ж-18 не готова           m3102.0-m3102.7	Byte_Signal.g18:=Ord(Swap(buffer_m[3]) and 256=256);
            //(Byte_Signal_minus.g19-Byte_Signal.g19)>0	1	Смазка Ж-19 не готова           m3102.0-m3102.7	Byte_Signal.g19:=Ord(Swap(buffer_m[3]) and 512=512);
            //(Byte_Signal_minus.g19-Byte_Signal.g19)<0	4	Смазка Ж-19 готова m3102.0-m3102.7	Byte_Signal.g19:=Ord(Swap(buffer_m[3]) and 512=512);
            //(Byte_Signal_minus.g20-Byte_Signal.g20)<0	1	Смазка Ж-20 готова m3102.0-m3102.7	Byte_Signal.g20:=Ord(Swap(buffer_m[3]) and 1024=1024);
            //(Byte_Signal_minus.g20-Byte_Signal.g20)>0	4	Смазка Ж-20 не готова           m3102.0-m3102.7	Byte_Signal.g20:=Ord(Swap(buffer_m[3]) and 1024=1024);
            //(Byte_Signal_minus.temp_POU-Byte_Signal.temp_POU)>0	1	Температура в ПОУ нормальная            m3102.0-m3102.7	Byte_Signal.temp_POU:=Ord(Swap(buffer_m[3]) and 2048=2048);
            //(Byte_Signal_minus.temp_POU-Byte_Signal.temp_POU)<0	4	Температура в ПОУ высокая           m3102.0-m3102.7	Byte_Signal.temp_POU:=Ord(Swap(buffer_m[3]) and 2048=2048);
            //(Byte_Signal_minus.davl_redukt-Byte_Signal.davl_redukt)<0	1	Давление редукторов низкое m3102.0-m3102.7	Byte_Signal.davl_redukt:=Ord(Swap(buffer_m[3]) and 4096=4096);
            //(Byte_Signal_minus.davl_redukt-Byte_Signal.davl_redukt)>0	4	Давление редукторов нормальное m3102.0-m3102.7	Byte_Signal.davl_redukt:=Ord(Swap(buffer_m[3]) and 4096=4096);
            //(Byte_Signal_minus.davl_PGT-Byte_Signal.davl_PGT)<0	1	Давление ПЖТ низкое m3102.0-m3102.7	Byte_Signal.davl_PGT:=Ord(Swap(buffer_m[3]) and 8192=8192);
            //(Byte_Signal_minus.davl_PGT-Byte_Signal.davl_PGT)>0	4	Давление ПЖТ нормальное m3102.0-m3102.7	Byte_Signal.davl_PGT:=Ord(Swap(buffer_m[3]) and 8192=8192);
            //(Byte_Signal_minus.temp_privod-Byte_Signal.temp_privod)<0	1	Вентиляция готова           m3102.0-m3102.7	Byte_Signal.temp_privod:=Ord(Swap(buffer_m[3]) and 16384=16384);
            //(Byte_Signal_minus.temp_privod-Byte_Signal.temp_privod)>0	4	Вентиляция не готова m3102.0-m3102.7	Byte_Signal.temp_privod:=Ord(Swap(buffer_m[3]) and 16384=16384);
            //(Byte_Signal_minus.got_sinhr-Byte_Signal.got_sinhr)<0	1	Синхронные двигатели готовы m3102.0-m3102.7	Byte_Signal.got_sinhr:=Ord(Swap(buffer_m[3]) and 32768=32768);
            //(Byte_Signal_minus.got_sinhr-Byte_Signal.got_sinhr)>0	4	Синхронные двигатели не готовы          m3102.0-m3102.7	Byte_Signal.got_sinhr:=Ord(Swap(buffer_m[3]) and 32768=32768);

            [56] = new MessageClass(1, "Смазка Ж - 18 готова", 4, "Смазка Ж-18 не готова"),
            [57] = new MessageClass(1, "Смазка Ж - 19 готова", 4, "Смазка Ж-19 не готова"),
            [58] = new MessageClass(1, "Смазка Ж - 20 готова", 4, "Смазка Ж-20 не готова"),
            [59] = new MessageClass(1, "Температура в ПОУ нормальная", 4, "Температура в ПОУ высокая"),
            [60] = new MessageClass(1, "Давление редукторов низкое", 4, "Давление редукторов нормальное"),
            [61] = new MessageClass(1, "Давление ПЖТ низкое", 4, "Давление ПЖТ нормальное"),
            [62] = new MessageClass(1, "Вентиляция готова", 4, "Вентиляция не готова"),
            [63] = new MessageClass(1, "Синхронные двигатели готовы", 4, "Синхронные двигатели не готовы"),

            //byte8  -> buffer_m[4](1-128)
            //(Byte_Signal_minus.OgragdMot - Byte_Signal.OgragdMot) < 0   1   Ограждение моталки закрыто          m3105.0 - m3105.7 Byte_Signal.OgragdMot:= Ord(Swap(buffer_m[4])and 1 = 1);
            //(Byte_Signal_minus.OgragdMot-Byte_Signal.OgragdMot)>0	4	Ограждение моталки открыто НО           m3105.0-m3105.7	Byte_Signal.OgragdMot:=Ord(Swap(buffer_m[4]) and 1=1);
            //(Byte_Signal_minus.ZaxlestOtMot-Byte_Signal.ZaxlestOtMot)<0	1	Захлестыватель у моталки НО             m3105.0-m3105.7	Byte_Signal.ZaxlestOtMot:=Ord(Swap(buffer_m[4]) and 2=2);
            //(Byte_Signal_minus.ZaxlestOtMot-Byte_Signal.ZaxlestOtMot)>0	4	Захлестыватель отведен          m3105.0-m3105.7	Byte_Signal.ZaxlestOtMot:=Ord(Swap(buffer_m[4]) and 2=2);
            //(Byte_Signal_minus.NOTempGP-Byte_Signal.NOTempGP)<0	1	Высокая температура ПЖТ ГП          m3105.0-m3105.7	Byte_Signal.NOTempGP:=Ord(Swap(buffer_m[4]) and 4=4);//bpv
            //(Byte_Signal_minus.NOTempGP-Byte_Signal.NOTempGP)>0	4	Нормальная температура ПЖТ ГП           m3105.0-m3105.7	Byte_Signal.NOTempGP:=Ord(Swap(buffer_m[4]) and 4=4);//bpv
            //(Byte_Signal_minus.Peregr1 - Byte_Signal.Peregr1) < 0   4   Перегруз клети 1            m3105.0 - m3105.7 Byte_Signal.Peregr1:= Ord(Swap(buffer_m[4])and 8 = 8);
            //(Byte_Signal_minus.Peregr1-Byte_Signal.Peregr1)>0	5	Конец перегруза клети 1 			m3105.0-m3105.7	Byte_Signal.Peregr1:=Ord(Swap(buffer_m[4]) and 8=8);
            //(Byte_Signal_minus.Peregr2-Byte_Signal.Peregr2)<0	4	Перегруз клети 2 			m3105.0-m3105.7	Byte_Signal.Peregr2:=Ord(Swap(buffer_m[4]) and 16=16);
            //(Byte_Signal_minus.Peregr2-Byte_Signal.Peregr2)>0	5	Конец перегруза клети 2 			m3105.0-m3105.7	Byte_Signal.Peregr2:=Ord(Swap(buffer_m[4]) and 16=16);
            //(Byte_Signal_minus.Peregr3-Byte_Signal.Peregr3)<0	4	Перегруз клети 3 			m3105.0-m3105.7	Byte_Signal.Peregr3:=Ord(Swap(buffer_m[4]) and 32=32);
            //(Byte_Signal_minus.Peregr3-Byte_Signal.Peregr3)>0	5	Конец перегруза клети 3 			m3105.0-m3105.7	Byte_Signal.Peregr3:=Ord(Swap(buffer_m[4]) and 32=32);
            //(Byte_Signal_minus.Peregr4-Byte_Signal.Peregr4)<0	4	Перегруз клети 4 			m3105.0-m3105.7	Byte_Signal.Peregr4:=Ord(Swap(buffer_m[4]) and 64=64);
            //(Byte_Signal_minus.Peregr4-Byte_Signal.Peregr4)>0	5	Конец перегруза клети 4 			m3105.0-m3105.7	Byte_Signal.Peregr4:=Ord(Swap(buffer_m[4]) and 64=64);
            //(Byte_Signal_minus.Peregr5-Byte_Signal.Peregr5)<0	4	Перегруз клети 5 			m3105.0-m3105.7	Byte_Signal.Peregr5:=Ord(Swap(buffer_m[4]) and 128=128);
            //(Byte_Signal_minus.Peregr5-Byte_Signal.Peregr5)>0	5	Конец перегруза клети 5 			m3105.0-m3105.7	Byte_Signal.Peregr5:=Ord(Swap(buffer_m[4]) and 128=128);
            [64] = new MessageClass(1, "Ограждение моталки закрыто", 4, "Ограждение моталки открыто НО"),
            [65] = new MessageClass(1, "Захлестыватель у моталки НО", 4, "Захлестыватель отведен"),
            [66] = new MessageClass(1, "Высокая температура ПЖТ ГП", 4, "Нормальная температура ПЖТ ГП"),
            [67] = new MessageClass(4, "Перегруз клети 1", 5, "Конец перегруза клети 1"),
            [68] = new MessageClass(4, "Перегруз клети 2", 5, "Конец перегруза клети 2"),
            [69] = new MessageClass(4, "Перегруз клети 3", 5, "Конец перегруза клети 3"),
            [70] = new MessageClass(4, "Перегруз клети 4", 5, "Конец перегруза клети 4"),
            [71] = new MessageClass(4, "Перегруз клети 5", 5, "Конец перегруза клети 5"),

            //byte9  -> buffer_m[4](256-32768)
            //(Byte_Signal_minus.NOSinxr - Byte_Signal.NOSinxr) < 0   1   Высокая температура ПЖТ СД          m3104.0 - m3104.7 Byte_Signal.NOSinxr:= Ord(Swap(buffer_m[4])and 256 = 256);
            //(Byte_Signal_minus.NOSinxr-Byte_Signal.NOSinxr)>0	4	Нормальная температура ПЖТ СД           m3104.0-m3104.8	Byte_Signal.NOSinxr:=Ord(Swap(buffer_m[4]) and 256=256);
            //(Byte_Signal_minus.NOPanPultStar-Byte_Signal.NOPanPultStar)<0	4	Кнопка НО на ПУ старшего нажата             m3104.0-m3104.9	Byte_Signal.NOPanPultStar:=Ord(Swap(buffer_m[4]) and 512=512);
            //(Byte_Signal_minus.NOPURazm-Byte_Signal.NOPURazm)>0	4	Кнопка НО на ПУР нажата m3104.0-m3104.10	Byte_Signal.NOPURazm:=Ord(Swap(buffer_m[4]) and 1024=1024);
            //(Byte_Signal_minus.NOPU1-Byte_Signal.NOPU1)>0	4	Кнопка НО на ПУ1 нажата m3104.0-m3104.11	Byte_Signal.NOPU1:=Ord(Swap(buffer_m[4]) and 2048=2048);
            //(Byte_Signal_minus.NOPU2-Byte_Signal.NOPU2)>0	4	Кнопка НО на ПУ2 нажата m3104.0-m3104.12	Byte_Signal.NOPU2:=Ord(Swap(buffer_m[4]) and 4096=4096);
            //(Byte_Signal_minus.NOPU3-Byte_Signal.NOPU3)>0	4	Кнопка НО на ПУ3 нажата m3104.0-m3104.13	Byte_Signal.NOPU3:=Ord(Swap(buffer_m[4]) and 8192=8192);
            //(Byte_Signal_minus.NOPU4-Byte_Signal.NOPU4)>0	4	Кнопка НО на ПУ4 нажата m3104.0-m3104.14	Byte_Signal.NOPU4:=Ord(Swap(buffer_m[4]) and 16384=16384);
            //(Byte_Signal_minus.NOPU5-Byte_Signal.NOPU5)>0	4	Кнопка НО на ПУ5 нажата m3104.0-m3104.15	Byte_Signal.NOPU5:=Ord(Swap(buffer_m[4]) and 32768=32768);
            [72] = new MessageClass(1, "Высокая температура ПЖТ СД", 4, "Нормальная температура ПЖТ СД"),
            [73] = new MessageClass(4, "Кнопка НО на ПУ старшего нажата", 0, ""),
            [74] = new MessageClass(0, "", 4, "Кнопка НО на ПУР нажата"),
            [75] = new MessageClass(0, "", 4, "Кнопка НО на ПУ1 нажата"),
            [76] = new MessageClass(0, "", 4, "Кнопка НО на ПУ2 нажата"),
            [77] = new MessageClass(0, "", 4, "Кнопка НО на ПУ3 нажата"),
            [78] = new MessageClass(0, "", 4, "Кнопка НО на ПУ4 нажата"),
            [79] = new MessageClass(0, "", 4, "Кнопка НО на ПУ5 нажата"),

            //byte10  -> buffer_m[5](1-128)
            //(Byte_Signal_minus.FOPanPultStar - Byte_Signal.FOPanPultStar) < 0   4   Кнопка ФО на ПУ старшего нажата             m3107.0 - m3107.7 Byte_Signal.FOPanPultStar:= Ord(Swap(buffer_m[5])and 1 = 1);
            //(Byte_Signal_minus.FOPU5-Byte_Signal.FOPU5)>0	4	Кнопка ФО на ПУ5 нажата m3107.0-m3107.7	Byte_Signal.FOPU5:=Ord(Swap(buffer_m[5]) and 2=2);
            //(Byte_Signal_minus.AOPUR-Byte_Signal.AOPUR)>0	4	Кнопка АО на ПУР нажата m3107.0-m3107.7	Byte_Signal.AOPUR:=Ord(Swap(buffer_m[5]) and 4=4);
            //(Byte_Signal_minus.TrazmProval-Byte_Signal.TrazmProval)<0	4	Провал натяжения на разматывателе ФО m3107.0-m3107.7	Byte_Signal.TrazmProval:=Ord(Swap(buffer_m[5]) and 8=8);
            //(Byte_Signal_minus.TrazmProval-Byte_Signal.TrazmProval)>0	1	Восстановление натяжения на разматывателе ТД m3107.0-m3107.7	Byte_Signal.TrazmProval:=Ord(Swap(buffer_m[5]) and 8=8);
            //(Byte_Signal_minus.T12proval-Byte_Signal.T12proval)<0	4	Провал натяжения в 1 промежутке ФО          m3107.0-m3107.7	Byte_Signal.T12proval:=Ord(Swap(buffer_m[5]) and 16=16);
            //(Byte_Signal_minus.T12proval-Byte_Signal.T12proval)>0	1	Восстановление натяжения в 1 промежутке ТД          m3107.0-m3107.7	Byte_Signal.T12proval:=Ord(Swap(buffer_m[5]) and 16=16);
            //(Byte_Signal_minus.T23proval-Byte_Signal.T23proval)<0	4	Провал натяжения во 2 промежутке ФО             m3107.0-m3107.7	Byte_Signal.T23proval:=Ord(Swap(buffer_m[5]) and 32=32);
            //(Byte_Signal_minus.T23proval-Byte_Signal.T23proval)>0	1	Восстановление натяжения во 2 промежутке ТД             m3107.0-m3107.7	Byte_Signal.T23proval:=Ord(Swap(buffer_m[5]) and 32=32);
            //(Byte_Signal_minus.T34proval-Byte_Signal.T34proval)<0	4	Провал натяжения в 3 промежутке ФО          m3107.0-m3107.7	Byte_Signal.T34proval:=Ord(Swap(buffer_m[5]) and 64=64);
            //(Byte_Signal_minus.T34proval-Byte_Signal.T34proval)>0	1	Восстановление натяжения в 3 промежутке ТД          m3107.0-m3107.7	Byte_Signal.T34proval:=Ord(Swap(buffer_m[5]) and 64=64);
            //(Byte_Signal_minus.T45proval-Byte_Signal.T45proval)<0	4	Провал натяжения в 4 промежутке ФО          m3107.0-m3107.7	Byte_Signal.T45proval:=Ord(Swap(buffer_m[5]) and 128=128);
            //(Byte_Signal_minus.T45proval-Byte_Signal.T45proval)>0	1	Восстановление натяжения в 4 промежутке ТД          m3107.0-m3107.7	Byte_Signal.T45proval:=Ord(Swap(buffer_m[5]) and 128=128);
            [80] = new MessageClass(4, "Кнопка ФО на ПУ старшего нажата", 0, ""),
            [81] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ5 нажата"),
            [82] = new MessageClass(0, "", 4, "Кнопка АО на ПУР нажата"),
            [83] = new MessageClass(4, "Провал натяжения на разматывателе", 1, "Восстановление натяжения на разматывателе ТД"),
            [84] = new MessageClass(4, "Провал натяжения в 1 промежутке ФО ", 1, "Восстановление натяжения в 1 промежутке ТД"),
            [85] = new MessageClass(4, "Провал натяжения в 2 промежутке ФО ", 1, "Восстановление натяжения в 2 промежутке ТД"),
            [86] = new MessageClass(4, "Провал натяжения в 3 промежутке ФО ", 1, "Восстановление натяжения в 3 промежутке ТД"),
            [87] = new MessageClass(4, "Провал натяжения в 4 промежутке ФО ", 1, "Восстановление натяжения в 4 промежутке ТД"),

            //byte11  -> buffer_m[5](256-32768)
            //(Byte_Signal_minus.Vent_101G - Byte_Signal.Vent_101G) < 0   4   Вентилятор обдува 101Г выключен НО          m3106.0 - m3106.7 Byte_Signal.Vent_101G:= Ord(Swap(buffer_m[5])and 256 = 256);
            //(Byte_Signal_minus.Vent_102G-Byte_Signal.Vent_102G)<0	4	Вентилятор обдува 102Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_102G:=Ord(Swap(buffer_m[5]) and 512=512);
            //(Byte_Signal_minus.Vent_103G-Byte_Signal.Vent_103G)<0	4	Вентилятор обдува 103Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_103G:=Ord(Swap(buffer_m[5]) and 1024=1024);
            //(Byte_Signal_minus.Vent_105G-Byte_Signal.Vent_105G)<0	4	Вентилятор обдува 105Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_105G:=Ord(Swap(buffer_m[5]) and 2048=2048);
            //(Byte_Signal_minus.Vent_106G-Byte_Signal.Vent_106G)<0	4	Вентилятор обдува 106Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_106G:=Ord(Swap(buffer_m[5]) and 4096=4096);
            //(Byte_Signal_minus.Vent_podpor_PA1 - Byte_Signal.Vent_podpor_PA1) < 0   4   Вентилятор подпора ПА - 1 выключен            m3106.0 - m3106.7 Byte_Signal.Vent_podpor_PA1:= Ord(Swap(buffer_m[5])and 8192 = 8192);
            //(Byte_Signal_minus.Vent_112G-Byte_Signal.Vent_112G)<0	4	Вентилятор обдува 112Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_112G:=Ord(Swap(buffer_m[5]) and 16384=16384);
            //(Byte_Signal_minus.Vent_111G-Byte_Signal.Vent_111G)<0	4	Вентилятор обдува 111Г выключен НО m3106.0-m3106.7	Byte_Signal.Vent_111G:=Ord(Swap(buffer_m[5]) and 32768=32768);
            [88] = new MessageClass(4, "Вентилятор обдува 101Г выключен НО", 0, ""),
            [89] = new MessageClass(4, "Вентилятор обдува 102Г выключен НО", 0, ""),
            [90] = new MessageClass(4, "Вентилятор обдува 103Г выключен НО", 0, ""),
            [91] = new MessageClass(4, "Вентилятор обдува 105Г выключен НО", 0, ""),
            [92] = new MessageClass(4, "Вентилятор обдува 106Г выключен НО", 0, ""),
            [93] = new MessageClass(4, "Вентилятор подпора ПА - 1 выключен", 0, ""),
            [94] = new MessageClass(4, "Вентилятор обдува 112Г выключен НО", 0, ""),
            [95] = new MessageClass(4, "Вентилятор обдува 111Г выключен НО", 0, ""),

            //byte12  -> buffer_m[6](1-128)
            //(Byte_Signal_minus.Vent_110G - Byte_Signal.Vent_110G) < 0   4   Вентилятор обдува 110Г выключен НО          m3109.0 - m3109.7 Byte_Signal.Vent_110G:= Ord(Swap(buffer_m[6])and 1 = 1);
            //(Byte_Signal_minus.Vent_108G-Byte_Signal.Vent_108G)<0	4	Вентилятор обдува 108Г выключен НО m3109.0-m3109.7	Byte_Signal.Vent_108G:=Ord(Swap(buffer_m[6]) and 2=2);
            //(Byte_Signal_minus.Vent_107G-Byte_Signal.Vent_107G)<0	4	Вентилятор обдува 107Г выключен НО m3109.0-m3109.7	Byte_Signal.Vent_107G:=Ord(Swap(buffer_m[6]) and 4=4);
            //(Byte_Signal_minus.Vent_podpor_PA2-Byte_Signal.Vent_podpor_PA2)<0	4	Вентилятор подпора ПА-2 выключен m3109.0-m3109.7	Byte_Signal.Vent_podpor_PA2:=Ord(Swap(buffer_m[6]) and 8=8);
            //(Byte_Signal_minus.Vent_1kl-Byte_Signal.Vent_1kl)<0	4	Вентилятор обдува ГП 1 клети выключен НО m3109.0-m3109.7	Byte_Signal.Vent_1kl:=Ord(Swap(buffer_m[6]) and 16=16);
            //(Byte_Signal_minus.Vent_2kl-Byte_Signal.Vent_2kl)<0	4	Вентилятор обдува ГП 2 клети выключен НО m3109.0-m3109.7	Byte_Signal.Vent_2kl:=Ord(Swap(buffer_m[6]) and 32=32);
            //(Byte_Signal_minus.Vent_3kl-Byte_Signal.Vent_3kl)<0	4	Вентилятор обдува ГП 3 клети выключен НО m3109.0-m3109.7	Byte_Signal.Vent_3kl:=Ord(Swap(buffer_m[6]) and 64=64);
            //(Byte_Signal_minus.Vent_4kl-Byte_Signal.Vent_4kl)<0	4	Вентилятор обдува ГП 4 клети выключен НО m3109.0-m3109.7	Byte_Signal.Vent_4kl:=Ord(Swap(buffer_m[6]) and 128=128);
            [96] = new MessageClass(4, "Вентилятор обдува 110Г выключен НО", 0, ""),
            [97] = new MessageClass(4, "Вентилятор обдува 108Г выключен НО", 0, ""),
            [98] = new MessageClass(4, "Вентилятор обдува 107Г выключен НО", 0, ""),
            [99] = new MessageClass(4, "Вентилятор подпора ПА-2 выключен", 0, ""),
            [100] = new MessageClass(4, "Вентилятор обдува ГП 1 клети выключен", 0, ""),
            [101] = new MessageClass(4, "Вентилятор обдува ГП 2 клети выключен", 0, ""),
            [102] = new MessageClass(4, "Вентилятор обдува ГП 3 клети выключен", 0, ""),
            [103] = new MessageClass(4, "Вентилятор обдува ГП 4 клети выключен", 0, ""),

            //byte13  -> buffer_m[6](256-32768)
            //(Byte_Signal_minus.FOPU4 - Byte_Signal.FOPU4) > 0   4   Кнопка ФО на ПУ4 нажата             m3108.0 - m3108.7 Byte_Signal.FOPU4:= Ord(Swap(buffer_m[6])and 256 = 256);
            //(Byte_Signal_minus.FOPU3-Byte_Signal.FOPU3)>0	4	Кнопка ФО на ПУ3 нажата m3108.0-m3108.7	Byte_Signal.FOPU3:=Ord(Swap(buffer_m[6]) and 512=512);
            //(Byte_Signal_minus.FOPU2-Byte_Signal.FOPU2)>0	4	Кнопка ФО на ПУ2 нажата m3108.0-m3108.7	Byte_Signal.FOPU2:=Ord(Swap(buffer_m[6]) and 1024=1024);
            //(Byte_Signal_minus.FOPU1-Byte_Signal.FOPU1)>0	4	Кнопка ФО на ПУ1 нажата m3108.0-m3108.7	Byte_Signal.FOPU1:=Ord(Swap(buffer_m[6]) and 2048=2048);
            //(Byte_Signal_minus.FOPUR-Byte_Signal.FOPUR)>0	4	Кнопка ФО на ПУР нажата m3108.0-m3108.7	Byte_Signal.FOPUR:=Ord(Swap(buffer_m[6]) and 4096=4096);
            //(Byte_Signal_minus.AOSUSknopka-Byte_Signal.AOSUSknopka)>0	4	Кнопка АО на СУС нажата m3108.0-m3108.7	Byte_Signal.AOSUSknopka:=Ord(Swap(buffer_m[6]) and 8192=8192);
            //(Byte_Signal_minus.AO5klet-Byte_Signal.AO5klet)>0	4	Кнопка АО на ПУ5 нажата m3108.0-m3108.7	Byte_Signal.AO5Klet:=Ord(Swap(buffer_m[6]) and 16384=16384);
            [104] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ4 нажата"),
            [105] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ3 нажата"),
            [106] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ2 нажата"),
            [107] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ1 нажата"),
            [108] = new MessageClass(0, "", 4, "Кнопка ФО на ПУР нажата"),
            [109] = new MessageClass(0, "", 4, "Кнопка АО на СУС нажата"),
            [110] = new MessageClass(0, "", 4, "Кнопка АО на ПУ5 нажата"),
            [111] = new MessageClass(0, "", 0, ""),

            //byte14  -> buffer_m[7](1-128)
            //(Byte_Signal_minus.KnAOsus - Byte_Signal.KnAOsus) < 0   4   Кнопка АО на СУС нажата         m3111.0 - m3111.7 Byte_Signal.KnAOsus:= Ord(Swap(buffer_m[7])and 4 = 4);
            [112] = new MessageClass(0, "", 0, ""),
            [113] = new MessageClass(0, "", 0, ""),
            [114] = new MessageClass(4, "Кнопка АО на СУС нажата", 0, ""),
            [115] = new MessageClass(0, "", 0, ""),
            [116] = new MessageClass(0, "", 0, ""),
            [117] = new MessageClass(0, "", 0, ""),
            [118] = new MessageClass(0, "", 0, ""),
            [119] = new MessageClass(0, "", 0, ""),

            //byte15  -> buffer_m[7](256-32768)
            //(Byte_Signal_minus.Vent_5kl - Byte_Signal.Vent_5kl) < 0 4   Вентилятор обдува ГП 5 клети выключен НО            m3110.0 - m3110.7 Byte_Signal.Vent_5kl:= Ord(Swap(buffer_m[7])and 256 = 256);
            //(Byte_Signal_minus.Vent_podpor_GP1-Byte_Signal.Vent_podpor_GP1)<0	4	Вентилятор подпора ГП-1 выключен m3110.0-m3110.7	Byte_Signal.Vent_podpor_GP1:=Ord(Swap(buffer_m[7]) and 1024=1024);
            //(Byte_Signal_minus.Vent_podpor_GP2-Byte_Signal.Vent_podpor_GP2)<0	4	Вентилятор подпора ГП-2 выключен m3110.0-m3110.7	Byte_Signal.Vent_podpor_GP2:=Ord(Swap(buffer_m[7]) and 2048=2048);
            //(Byte_Signal_minus.Vent_NV-Byte_Signal.Vent_NV)<0	4	Вентилятор обдува нажимных винтов выключен НО           m3110.0-m3110.7	Byte_Signal.Vent_NV:=Ord(Swap(buffer_m[7]) and 4096=4096);
            //(Byte_Signal_minus.Dv2pr09sec3vv-Byte_Signal.Dv2pr09sec3vv)>0	4	ХХХ ПЕРЕГРУЗ ГП ХХХ         m3110.0-m3110.7	Byte_Signal.Dv2pr09sec3vv:=Ord(Swap(buffer_m[7]) and 8192=8192);
            [120] = new MessageClass(4, "Вентилятор обдува ГП 5 клети выключен НО", 0, ""),
            [121] = new MessageClass(0, "", 0, ""),
            [122] = new MessageClass(4, "Вентилятор подпора ГП-1 выключен", 0, ""),
            [123] = new MessageClass(4, "Вентилятор подпора ГП-2 выключен", 0, ""),
            [124] = new MessageClass(4, "Вентилятор обдува нажимных винтов выключен", 0, ""),
            [125] = new MessageClass(4, "ХХХ ПЕРЕГРУЗ ГП ХХХ", 0, ""),
            [126] = new MessageClass(0, "", 0, ""),
            [127] = new MessageClass(0, "", 0, ""),
        };

        DataTable dtMessagestan;

        DataTable dt1sStan;
        DataTable dtPerevalkiStan;

        string numberTable;

        int write1s=0; //цикл сохранением значений 1s
        int writeMessage = 0; //цикл сохранением значений Message

        int d1_pred;
        int d2_pred;
        int d3_pred;
        int d4_pred;
        int d5_pred;

        


        public MainWindow()
        {

            connectingString = Properties.Settings.Default.strConnect;


            buffer = new byte[315];
            bufferPLC = new byte[315];
            bufferSQL = new byte[315];
            bufferMessage = new byte[22];
            bufferMessageOld = new byte[22];
            buffer1s = new byte[315];
            bufferNet = new byte[315];

            #region dt101msStan - формирование DataTable 1 sec 
            dt101msStan = new DataTable();
            dt101msStan.Columns.Add("dtStan", typeof(DateTime));
            dt101msStan.Columns.Add("v1", typeof(float));
            dt101msStan.Columns.Add("v2", typeof(float));
            dt101msStan.Columns.Add("v3", typeof(float));
            dt101msStan.Columns.Add("v4", typeof(float));
            dt101msStan.Columns.Add("v5", typeof(float));
            dt101msStan.Columns.Add("h1", typeof(float));
            dt101msStan.Columns.Add("h5", typeof(float));
            dt101msStan.Columns.Add("b", typeof(int));
            dt101msStan.Columns.Add("dvip", typeof(float));
            dt101msStan.Columns.Add("drazm", typeof(float));
            dt101msStan.Columns.Add("dmot", typeof(float));
            dt101msStan.Columns.Add("vvip", typeof(float));
            dt101msStan.Columns.Add("d1", typeof(int));
            dt101msStan.Columns.Add("d2", typeof(int));
            dt101msStan.Columns.Add("d3", typeof(int));
            dt101msStan.Columns.Add("d4", typeof(int));
            dt101msStan.Columns.Add("d5", typeof(int));
            dt101msStan.Columns.Add("e2", typeof(float));
            dt101msStan.Columns.Add("e3", typeof(float));
            dt101msStan.Columns.Add("e4", typeof(float));
            dt101msStan.Columns.Add("e5", typeof(float));
            dt101msStan.Columns.Add("n1l", typeof(float));
            dt101msStan.Columns.Add("n1p", typeof(float));
            dt101msStan.Columns.Add("n2l", typeof(float));
            dt101msStan.Columns.Add("n2p", typeof(float));
            dt101msStan.Columns.Add("n3l", typeof(float));
            dt101msStan.Columns.Add("n3p", typeof(float));
            dt101msStan.Columns.Add("n4l", typeof(float));
            dt101msStan.Columns.Add("n4p", typeof(float));
            dt101msStan.Columns.Add("n5l", typeof(float));
            dt101msStan.Columns.Add("n5p", typeof(float));
            dt101msStan.Columns.Add("reserv1", typeof(float));
            dt101msStan.Columns.Add("reserv2", typeof(float));
            dt101msStan.Columns.Add("t1", typeof(float));
            dt101msStan.Columns.Add("t2", typeof(float));
            dt101msStan.Columns.Add("t3", typeof(float));
            dt101msStan.Columns.Add("t4", typeof(float));
            dt101msStan.Columns.Add("t1l", typeof(float));
            dt101msStan.Columns.Add("t2l", typeof(float));
            dt101msStan.Columns.Add("t3l", typeof(float));
            dt101msStan.Columns.Add("t4l", typeof(float));
            dt101msStan.Columns.Add("t1p", typeof(float));
            dt101msStan.Columns.Add("t2p", typeof(float));
            dt101msStan.Columns.Add("t3p", typeof(float));
            dt101msStan.Columns.Add("t4p", typeof(float));
            dt101msStan.Columns.Add("t1z", typeof(float));
            dt101msStan.Columns.Add("t2z", typeof(float));
            dt101msStan.Columns.Add("t3z", typeof(float));
            dt101msStan.Columns.Add("t4z", typeof(float));
            dt101msStan.Columns.Add("erazm", typeof(float));
            dt101msStan.Columns.Add("ivozbrazm", typeof(float));
            dt101msStan.Columns.Add("izadrazm", typeof(float));
            dt101msStan.Columns.Add("w1", typeof(float));
            dt101msStan.Columns.Add("w2v", typeof(float));
            dt101msStan.Columns.Add("w2n", typeof(float));
            dt101msStan.Columns.Add("w3v", typeof(float));
            dt101msStan.Columns.Add("w3n", typeof(float));
            dt101msStan.Columns.Add("w4v", typeof(float));
            dt101msStan.Columns.Add("w4n", typeof(float));
            dt101msStan.Columns.Add("w5v", typeof(float));
            dt101msStan.Columns.Add("w5n", typeof(float));
            dt101msStan.Columns.Add("wmot", typeof(float));
            dt101msStan.Columns.Add("imot", typeof(int));
            dt101msStan.Columns.Add("izadmot", typeof(int));
            dt101msStan.Columns.Add("u1", typeof(float));
            dt101msStan.Columns.Add("u2v", typeof(float));
            dt101msStan.Columns.Add("u2n", typeof(float));
            dt101msStan.Columns.Add("u3v", typeof(float));
            dt101msStan.Columns.Add("u3n", typeof(float));
            dt101msStan.Columns.Add("u4v", typeof(float));
            dt101msStan.Columns.Add("u4n", typeof(float));
            dt101msStan.Columns.Add("u5v", typeof(float));
            dt101msStan.Columns.Add("u5n", typeof(float));
            dt101msStan.Columns.Add("umot", typeof(float));
            dt101msStan.Columns.Add("i1", typeof(int));
            dt101msStan.Columns.Add("i2v", typeof(int));
            dt101msStan.Columns.Add("i2n", typeof(int));
            dt101msStan.Columns.Add("i3v", typeof(int));
            dt101msStan.Columns.Add("i3n", typeof(int));
            dt101msStan.Columns.Add("i4v", typeof(int));
            dt101msStan.Columns.Add("i4n", typeof(int));
            dt101msStan.Columns.Add("i5v", typeof(int));
            dt101msStan.Columns.Add("i5n", typeof(int));
            dt101msStan.Columns.Add("rtv", typeof(float));
            dt101msStan.Columns.Add("dt1", typeof(float));
            dt101msStan.Columns.Add("dt2", typeof(float));
            dt101msStan.Columns.Add("dt3", typeof(float));
            dt101msStan.Columns.Add("dt4", typeof(float));
            dt101msStan.Columns.Add("grt", typeof(float));
            dt101msStan.Columns.Add("trt", typeof(float));
            dt101msStan.Columns.Add("mv1", typeof(float));
            dt101msStan.Columns.Add("mv2", typeof(float));
            dt101msStan.Columns.Add("mv3", typeof(float));
            dt101msStan.Columns.Add("dh1", typeof(float));
            dt101msStan.Columns.Add("dh5", typeof(float));
            dt101msStan.Columns.Add("os1klvb", typeof(int));
            dt101msStan.Columns.Add("rezerv", typeof(int));
            dt101msStan.Columns.Add("mezdoza4", typeof(int));
            #endregion

            #region dtMessageStan - формирование DataTable Message(таблица сообщений стана)
            dtMessagestan = new DataTable();
            dtMessagestan.Columns.Add("dtmes", typeof(DateTime));
            dtMessagestan.Columns.Add("status", typeof(int));
            dtMessagestan.Columns.Add("message", typeof(string));
            dtMessagestan.Columns.Add("speed", typeof(float));
            #endregion

            #region dt1sStan - формирование dataTable 1s(таблица 1s стана)
            dt1sStan = new DataTable();
            dt1sStan.Columns.Add("dt1s", typeof(DateTime));
            dt1sStan.Columns.Add("191HL", typeof(int));
            dt1sStan.Columns.Add("192HL", typeof(int));
            dt1sStan.Columns.Add("193BL", typeof(int));
            dt1sStan.Columns.Add("194BL", typeof(int));
            dt1sStan.Columns.Add("191HR", typeof(int));
            dt1sStan.Columns.Add("192HR", typeof(int));
            dt1sStan.Columns.Add("193BR", typeof(int));
            dt1sStan.Columns.Add("194BR", typeof(int));
            dt1sStan.Columns.Add("281NL", typeof(int));
            dt1sStan.Columns.Add("282NL", typeof(int));
            dt1sStan.Columns.Add("283BL", typeof(int));
            dt1sStan.Columns.Add("284BL", typeof(int));
            dt1sStan.Columns.Add("281NR", typeof(int));
            dt1sStan.Columns.Add("282NR", typeof(int));
            dt1sStan.Columns.Add("283BR", typeof(int));
            dt1sStan.Columns.Add("284BR", typeof(int));
            dt1sStan.Columns.Add("301BL", typeof(int));
            dt1sStan.Columns.Add("302BL", typeof(int));
            dt1sStan.Columns.Add("303HL", typeof(int));
            dt1sStan.Columns.Add("304HL", typeof(int));
            dt1sStan.Columns.Add("301BR", typeof(int));
            dt1sStan.Columns.Add("302BR", typeof(int));
            dt1sStan.Columns.Add("303HR", typeof(int));
            dt1sStan.Columns.Add("304HR", typeof(int));
            dt1sStan.Columns.Add("321BL", typeof(int));
            dt1sStan.Columns.Add("322BL", typeof(int));
            dt1sStan.Columns.Add("323HL", typeof(int));
            dt1sStan.Columns.Add("324HL", typeof(int));
            dt1sStan.Columns.Add("321BR", typeof(int));
            dt1sStan.Columns.Add("322BR", typeof(int));
            dt1sStan.Columns.Add("323HR", typeof(int));
            dt1sStan.Columns.Add("324HR", typeof(int));
            dt1sStan.Columns.Add("341BL", typeof(int));
            dt1sStan.Columns.Add("342BL", typeof(int));
            dt1sStan.Columns.Add("343HL", typeof(int));
            dt1sStan.Columns.Add("344HL", typeof(int));
            dt1sStan.Columns.Add("341BR", typeof(int));
            dt1sStan.Columns.Add("342BR", typeof(int));
            dt1sStan.Columns.Add("343HR", typeof(int));
            dt1sStan.Columns.Add("344HR", typeof(int));
            dt1sStan.Columns.Add("461L", typeof(int));
            dt1sStan.Columns.Add("462L", typeof(int));
            dt1sStan.Columns.Add("463L", typeof(int));
            dt1sStan.Columns.Add("461R", typeof(int));
            dt1sStan.Columns.Add("462R", typeof(int));
            dt1sStan.Columns.Add("463R", typeof(int));
            dt1sStan.Columns.Add("G11L", typeof(int));
            dt1sStan.Columns.Add("G12L", typeof(int));
            dt1sStan.Columns.Add("G13L", typeof(int));
            dt1sStan.Columns.Add("G14L", typeof(int));
            dt1sStan.Columns.Add("G15L", typeof(int));
            dt1sStan.Columns.Add("G16L", typeof(int));
            dt1sStan.Columns.Add("G17L", typeof(int));
            dt1sStan.Columns.Add("G11R", typeof(int));
            dt1sStan.Columns.Add("G12R", typeof(int));
            dt1sStan.Columns.Add("G13R", typeof(int));
            dt1sStan.Columns.Add("G14R", typeof(int));
            dt1sStan.Columns.Add("G15R", typeof(int));
            dt1sStan.Columns.Add("G16R", typeof(int));
            dt1sStan.Columns.Add("G17R", typeof(int));
            dt1sStan.Columns.Add("G21L", typeof(int));
            dt1sStan.Columns.Add("G22L", typeof(int));
            dt1sStan.Columns.Add("G23L", typeof(int));
            dt1sStan.Columns.Add("G24L", typeof(int));
            dt1sStan.Columns.Add("G25L", typeof(int));
            dt1sStan.Columns.Add("G26L", typeof(int));
            dt1sStan.Columns.Add("G27L", typeof(int));
            dt1sStan.Columns.Add("G21R", typeof(int));
            dt1sStan.Columns.Add("G22R", typeof(int));
            dt1sStan.Columns.Add("G23R", typeof(int));
            dt1sStan.Columns.Add("G24R", typeof(int));
            dt1sStan.Columns.Add("G25R", typeof(int));
            dt1sStan.Columns.Add("G26R", typeof(int));
            dt1sStan.Columns.Add("G27R", typeof(int));
            dt1sStan.Columns.Add("D12", typeof(float));
            dt1sStan.Columns.Add("D13", typeof(float));
            dt1sStan.Columns.Add("D14", typeof(float));
            dt1sStan.Columns.Add("D15", typeof(float));
            dt1sStan.Columns.Add("D16", typeof(float));
            dt1sStan.Columns.Add("D17", typeof(float));
            dt1sStan.Columns.Add("D18", typeof(float));
            dt1sStan.Columns.Add("D19", typeof(float));
            dt1sStan.Columns.Add("D20", typeof(float));
            dt1sStan.Columns.Add("U64", typeof(int));
            dt1sStan.Columns.Add("RasxCD", typeof(int));
            #endregion

            #region  dtStanPerevalki - формирование dataTable Перевалки(цикл  1s стана)
            dtPerevalkiStan = new DataTable();
            dtPerevalkiStan.Columns.Add("dtPerevalki", typeof(DateTime));
            dtPerevalkiStan.Columns.Add("kl1", typeof(int));
            dtPerevalkiStan.Columns.Add("kl2", typeof(int));
            dtPerevalkiStan.Columns.Add("kl3", typeof(int));
            dtPerevalkiStan.Columns.Add("kl4", typeof(int));
            dtPerevalkiStan.Columns.Add("kl5", typeof(int));

            #endregion

            InitializeComponent();
        }



        private void SwitchPTO_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (SwitchPTO.IsSwitching1)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }

       

        private void Stop()
        {
            //TODO Закрытие таймеров
            TTimer100ms.Dispose();
            TTimerMessage.Dispose();
            TTimerSQL.Dispose();
            TTimer1s.Dispose();
            TTimer250msNet.Dispose();


            #region Сброс индикации
            //Ellipse101ms.Fill = offLed;
            //Ellipse1s.Fill = offLed;
            //Ellipse200ms.Fill = offLed;
            //EllipseNet.Fill = offLed;
           
            
            #endregion


            int result = stan.UnloadConnection(0);

            if (result == 0)
            {
                
                //LogSystem.WriteEventLog("ProDaveStan", "Test", "Close connection", EventLogEntryType.Information);

               
            }
            else
            {
                
                //LogSystem.WriteEventLog("ProDaveStan", "Test", "Connect open. Error - " + stan.Error(result), EventLogEntryType.Warning);
                lstStatus.Items.Add("Connect open. Warning - " + stan.Error(result));
                
            }

        }



        #region START - Старт таймера 100ms для опроса PLC

        private void Start()
        {
            //TODO запуск таймеров 100ms(100ms), 101ms(SQL), 200ms(message), 1000ms(1s)

            stan = new Prodave();
            byte[] conn = new byte[] { 192, 168, 0, 11 };

            int res = stan.LoadConnection(Connect, 2, conn, 3, 0);

            if (res != 0)
            {
                Console.WriteLine("Error connection! " + stan.Error(res));
                //LogSystem.WriteEventLog("ProDaveStan", "Test", "Error connection!. Error - " + stan.Error(res), EventLogEntryType.Error);
                lstStatus.Items.Add("Error connection PLC стан! " + stan.Error(res));
            }
            else
            {
                //LogSystem.WriteEventLog("ProDaveStan", "Test", "Connect OK!", EventLogEntryType.Information);

                int resSAC = stan.SetActiveConnection(Connect);
                if (resSAC == 0)
                {
                    Console.WriteLine("Соединение активно.");
                   // LogSystem.WriteEventLog("ProDaveStan", "Test", "Соединение активно.", EventLogEntryType.Information);



                    //Connect100ms();
                    TTimer100ms = new Timer(new TimerCallback(TicTimer100ms), null, 0, 100);

                    TTimerMessage = new Timer(new TimerCallback(TicTimerMessage), null, 0, 200);
                    TTimerSQL = new Timer(new TimerCallback(TicTimerSQL), null, 0, 101);
                    TTimer1s = new Timer(new TimerCallback(TicTimer1s), null, 0, 1000);

                    
                    TTimer250msNet = new Timer(new TimerCallback(TicTimer250msNet), null, 0, 250);



                }
                else
                {
                    Console.WriteLine("Соединение не активировано. " + stan.Error(resSAC));
                    //LogSystem.WriteEventLog("ProDaveStan", "Test", "Соединение не активировано. " + stan.Error(resSAC), EventLogEntryType.Error);
                    System.Diagnostics.Debug.WriteLine("Error - Соединение не активировано.");
                    lstStatus.Items.Add("Соединение с PLC-стан не активировано. " + stan.Error(resSAC));

                }

            }
        }

        

        private void TicTimer100ms(object state)
        {
            Connect100ms();
        }
        private void Connect100ms()
        {
            short[] buffer_array = new short[158];

            int amount = 315;


            //var buffer2 = new ushort[128];
            int Byte_Col_r = 0;

            int resultReadField = stan.field_read('M', 0, 3000, amount, out buffer, out Byte_Col_r);
            if (resultReadField == 0)
            {
                //Console.WriteLine("OK.Read fied M3000-M3315");
                //System.Diagnostics.Debug.WriteLine("OK.Read fied M3000-M3315");
                //Console.WriteLine(string.Format("\t\t {0}  ({1})  {2}", "Message 100mc", DateTime.Now - dt100ms, Thread.CurrentThread.ManagedThreadId));

                dt100ms = DateTime.Now;


                //Todo 100ms Далее передаем данные в критическую сецкию из которой будут забирать потоки каждый в свой момент
                //Console.WriteLine("Byte_Col_r = " + Byte_Col_r);

                //Console.WriteLine("Скорость 1(in) = " + BitConverter.ToInt16(buffer, 0));
                //Console.WriteLine("Скорость 2 = " + BitConverter.ToInt16(buffer, 2));


                Thread PLS100ms = new Thread(BufferToBuffer);
                PLS100ms.Start();


            }
            else
            {
                Console.WriteLine("Error.Read fied M3000-M3315. " + stan.Error(resultReadField));
                System.Diagnostics.Debug.WriteLine("Error.Read fied M3000-M3315. " + stan.Error(resultReadField));
                //LogSystem.WriteEventLog("ProDaveStan", "Test", "Соединение не активировано. " + stan.Error(resultReadField), EventLogEntryType.Error);
                lstStatus.Items.Add("Error PLC stan . Error read fied M3000-M3315. " + stan.Error(resultReadField));
            }



        }
        #endregion



        #region Блок критичных секций
        static void BufferToBuffer()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                bufferPLC = buffer;
            }

        }

        static void BufferSQLToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                bufferSQL = bufferPLC;
            }

        }

        static void BufferMessageToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            //У миши Богуша в delphi программе биты переставлялись с помощью оператора swap и он истользовал не byte, а int
            //У меня используется везьде byte поэтому я просто поменял биты при формировании buffer

            lock (locker)
            {
                bufferMessageOld[0] = bufferMessage[0];
                bufferMessageOld[1] = bufferMessage[1];
                bufferMessageOld[2] = bufferMessage[2];
                bufferMessageOld[3] = bufferMessage[3];
                bufferMessageOld[4] = bufferMessage[4];
                bufferMessageOld[5] = bufferMessage[5];
                bufferMessageOld[6] = bufferMessage[6];
                bufferMessageOld[7] = bufferMessage[7];
                bufferMessageOld[8] = bufferMessage[8];
                bufferMessageOld[9] = bufferMessage[9];
                bufferMessageOld[10] = bufferMessage[10];
                bufferMessageOld[11] = bufferMessage[11];
                bufferMessageOld[12] = bufferMessage[12];
                bufferMessageOld[13] = bufferMessage[13];
                bufferMessageOld[14] = bufferMessage[14];
                bufferMessageOld[15] = bufferMessage[15];
                bufferMessageOld[16] = bufferMessage[16];
                bufferMessageOld[17] = bufferMessage[17];
                bufferMessageOld[18] = bufferMessage[18];
                bufferMessageOld[19] = bufferMessage[19];
                bufferMessageOld[20] = bufferMessage[20];
                bufferMessageOld[21] = bufferMessage[21];



                bufferMessage[0] = bufferPLC[67];
                bufferMessage[1] = bufferPLC[66];
                bufferMessage[2] = bufferPLC[69];
                bufferMessage[3] = bufferPLC[68];
                bufferMessage[4] = bufferPLC[71];
                bufferMessage[5] = bufferPLC[70];
                bufferMessage[6] = bufferPLC[103];
                bufferMessage[7] = bufferPLC[102];
                bufferMessage[8] = bufferPLC[105];
                bufferMessage[9] = bufferPLC[104];
                bufferMessage[10] = bufferPLC[107];
                bufferMessage[11] = bufferPLC[106];
                bufferMessage[12] = bufferPLC[109];
                bufferMessage[13] = bufferPLC[108];
                bufferMessage[14] = bufferPLC[111];
                bufferMessage[15] = bufferPLC[110];
                bufferMessage[16] = bufferPLC[6];
                bufferMessage[17] = bufferPLC[7];
                bufferMessage[18] = bufferPLC[312];
                bufferMessage[19] = bufferPLC[313];
                bufferMessage[20] = bufferPLC[310];
                bufferMessage[21] = bufferPLC[311];

            }

        }
        static void Buffer1sToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                buffer1s[0] = bufferPLC[224];           //191HL
                buffer1s[1] = bufferPLC[225];           //192HL
                buffer1s[2] = bufferPLC[226];           //193BL
                buffer1s[3] = bufferPLC[227];           //194BL"
                buffer1s[4] = bufferPLC[228];           //191HR"
                buffer1s[5] = bufferPLC[229];           //192HR"
                buffer1s[6] = bufferPLC[230];           //193BR"
                buffer1s[7] = bufferPLC[231];           //194BR"
                buffer1s[8] = bufferPLC[232];           //281NL"
                buffer1s[9] = bufferPLC[233];           //282NL"
                buffer1s[10] = bufferPLC[234];          //283BL"
                buffer1s[11] = bufferPLC[235];          //284BL"
                buffer1s[12] = bufferPLC[236];          //281NR"
                buffer1s[13] = bufferPLC[237];          //282NR"
                buffer1s[14] = bufferPLC[238];          //283BR"
                buffer1s[15] = bufferPLC[239];          //284BR"
                buffer1s[16] = bufferPLC[240];          //301BL"
                buffer1s[17] = bufferPLC[241];          //302BL"
                buffer1s[18] = bufferPLC[242];          //303HL"
                buffer1s[19] = bufferPLC[243];          //304HL"
                buffer1s[20] = bufferPLC[244];          //301BR"
                buffer1s[21] = bufferPLC[245];          //302BR"
                buffer1s[22] = bufferPLC[246];          //303HR"
                buffer1s[23] = bufferPLC[247];          //304HR"
                buffer1s[24] = bufferPLC[248];          //321BL"
                buffer1s[25] = bufferPLC[249];          //322BL"
                buffer1s[26] = bufferPLC[250];          //323HL"
                buffer1s[27] = bufferPLC[251];          //324HL"
                buffer1s[28] = bufferPLC[252];          //321BR"
                buffer1s[29] = bufferPLC[253];          //322BR"
                buffer1s[30] = bufferPLC[254];          //323HR"
                buffer1s[31] = bufferPLC[255];          //324HR"
                buffer1s[32] = bufferPLC[256];          //341BL"
                buffer1s[33] = bufferPLC[257];          //342BL"
                buffer1s[34] = bufferPLC[258];          //343HL"
                buffer1s[35] = bufferPLC[259];          //344HL"
                buffer1s[36] = bufferPLC[260];          //341BR"
                buffer1s[37] = bufferPLC[261];          //342BR"
                buffer1s[38] = bufferPLC[262];          //343HR"
                buffer1s[39] = bufferPLC[263];          //344HR"
                buffer1s[40] = bufferPLC[264];          //461L",
                buffer1s[41] = bufferPLC[265];          //462L",
                buffer1s[42] = bufferPLC[266];          //463L",
                buffer1s[43] = bufferPLC[267];          //461R",
                buffer1s[44] = bufferPLC[268];          //462R",
                buffer1s[45] = bufferPLC[269];          //463R",
                buffer1s[46] = bufferPLC[270];          //G11L",
                buffer1s[47] = bufferPLC[271];          //G12L",
                buffer1s[48] = bufferPLC[272];          //G13L",
                buffer1s[49] = bufferPLC[273];          //G14L",
                buffer1s[50] = bufferPLC[274];          //G15L",
                buffer1s[51] = bufferPLC[275];          //G16L",
                buffer1s[52] = bufferPLC[276];          //G17L",
                buffer1s[53] = bufferPLC[277];          //G11R",
                buffer1s[54] = bufferPLC[278];          //G12R",
                buffer1s[55] = bufferPLC[279];          //G13R",
                buffer1s[56] = bufferPLC[280];          //G14R",
                buffer1s[57] = bufferPLC[281];          //G15R",
                buffer1s[58] = bufferPLC[282];          //G16R",
                buffer1s[59] = bufferPLC[283];          //G17R",
                buffer1s[60] = bufferPLC[284];          //G21L",
                buffer1s[61] = bufferPLC[285];          //G22L",
                buffer1s[62] = bufferPLC[286];          //G23L",
                buffer1s[63] = bufferPLC[287];          //G24L",
                buffer1s[64] = bufferPLC[288];          //G25L",
                buffer1s[65] = bufferPLC[289];          //G26L",
                buffer1s[66] = bufferPLC[290];          //G27L",
                buffer1s[67] = bufferPLC[291];          //G21R",
                buffer1s[68] = bufferPLC[292];          //G22R",
                buffer1s[69] = bufferPLC[293];          //G23R",
                buffer1s[70] = bufferPLC[294];          //G24R",
                buffer1s[71] = bufferPLC[295];          //G25R",
                buffer1s[72] = bufferPLC[296];          //G26R",
                buffer1s[73] = bufferPLC[297];          //G27R",
                buffer1s[74] = bufferPLC[298];          //D12", 
                buffer1s[75] = bufferPLC[299];          //D13", 
                buffer1s[76] = bufferPLC[300];          //D14", 
                buffer1s[77] = bufferPLC[301];          //D15", 
                buffer1s[78] = bufferPLC[302];          //D16", 
                buffer1s[79] = bufferPLC[303];          //D17", 
                buffer1s[80] = bufferPLC[304];          //D18", 
                buffer1s[81] = bufferPLC[305];          //D19", 
                buffer1s[82] = bufferPLC[306];          //D20", 
                buffer1s[83] = bufferPLC[307];          //U64", 
                buffer1s[84] = bufferPLC[308];          //RasxCD
                buffer1s[85] = bufferPLC[24];          //D1_pred 
                buffer1s[86] = bufferPLC[25];          //D1_pred
                buffer1s[87] = bufferPLC[26];          //D2_pred 
                buffer1s[88] = bufferPLC[27];          //D2_pred
                buffer1s[89] = bufferPLC[28];          //D3_pred 
                buffer1s[90] = bufferPLC[29];          //D3_pred
                buffer1s[91] = bufferPLC[30];          //D4_pred 
                buffer1s[92] = bufferPLC[31];          //D4_pred
                buffer1s[93] = bufferPLC[32];          //D5_pred 
                buffer1s[94] = bufferPLC[33];          //D5_pred

            }

        }

        static void BufferNetToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                bufferNet = buffer;
            }

        }

        #endregion

        

        #region SQL - запись в БД значений runtime (101ms)
        private void TicTimerSQL(object state)
        {
            #region Формируем шифр таблицы (yyyy-MM-dd-смена)
            
            if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 7 && Convert.ToInt32(DateTime.Now.ToString("HH")) < 19)
            {
                numberTable = DateTime.Now.ToString("yyyyMMdd") +"2";
            }
            else if (Convert.ToInt32(DateTime.Now.ToString("HH")) < 7)
            {
                numberTable = DateTime.Now.ToString("yyyyMMdd") + "1";
            }
            else if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 19)
            {
                numberTable = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "1";
            }

            #endregion

            //Console.WriteLine("numberTable -> " + numberTable);

            //this.Dispatcher.Invoke((Action)this.ConvertBufferSQLandWriteSQL); //Записываем данные в БД
            this.Dispatcher.Invoke((Action)this.ConvertBufferSQLandWriteTable); //Формируем таблицу а после окончания прокатки рулона создаем в БД таблицу с номером рулона и записываем данные в неё
        }

        private void ConvertBufferSQLandWriteSQL()
        {

            //Ellipse101ms.Fill = offLed;
            try
            {
                //Console.WriteLine(string.Format("\t\t {0} ({1}) {2}", "SQL 101mc", DateTime.Now - dtSQL, Thread.CurrentThread.ManagedThreadId));
                dtSQL = DateTime.Now;

                //Из критичной секции получаем значения из PLC
                Thread tSQL = new Thread(BufferSQLToBufferPLC);
                tSQL.Start();

                #region SQL insert
                if (con.State!=System.Data.ConnectionState.Open)
                {
                    SQLconBD();
                }

                string sqlExpression =
                   "INSERT INTO RS2stan100ms" +
                   "(v1,v2,v3,v4,v5,h1,h5,b,dvip,drazm,dmot,vvip,d1,d2,d3,d4,d5,e2,e3,e4,e5,n1l,n1p,n2l,n2p,n3l,n3p,n4l,n4p,n5l,n5p,reserv1,reserv2,t1,t2,t3,t4,t1l,t2l,t3l,t4l,t1p,t2p,t3p,t4p,t1z,t2z,t3z,t4z,erazm,ivozbrazm,izadrazm,w1,w2v,w2n,w3v,w3n,w4v,w4n,w5v,w5n,wmot,imot,izadmot,u1,u2v,u2n,u3v,u3n,u4v,u4n,u5v,u5n,umot,i1,i2v,i2n,i3v,i3n,i4v,i4n,i5v,i5n,rtv,dt1,dt2,dt3,dt4,grt,trt,mv1,mv2,mv3,dh1,dh5,os1klvb,rezerv,mezdoza4)" +
                   " VALUES " +
                   "(" +
                    (float)(BitConverter.ToInt16(bufferSQL, 0)) / 100 + "," +    //v1
                    (float)(BitConverter.ToInt16(bufferSQL, 2)) / 100 + "," +    //v2
                    (float)(BitConverter.ToInt16(bufferSQL, 4)) / 100 + "," +    //v3
                    (float)(BitConverter.ToInt16(bufferSQL, 6)) / 100 + "," +    //v4
                    (float)(BitConverter.ToInt16(bufferSQL, 8)) / 100 + "," +    //v5
                    (float)(BitConverter.ToInt16(bufferSQL, 10)) / 1000 + "," +  //h1    
                    (float)(BitConverter.ToInt16(bufferSQL, 12)) / 1000 + "," +  //h5
                    BitConverter.ToInt16(bufferSQL, 14) + "," +                  //b
                    (float)(BitConverter.ToInt16(bufferSQL, 16)) / 1000 + "," +  //dvip
                    (float)(BitConverter.ToInt16(bufferSQL, 18)) / 1000 + "," +  //drazm
                    (float)(BitConverter.ToInt16(bufferSQL, 20)) / 1000 + "," +  //dmot
                    (float)(BitConverter.ToInt16(bufferSQL, 22)) / 1000 + "," +  //vvip
                    BitConverter.ToInt16(bufferSQL, 24) + "," +                  //d1
                    BitConverter.ToInt16(bufferSQL, 26) + "," +                  //d2
                    BitConverter.ToInt16(bufferSQL, 28) + "," +                  //d3
                    BitConverter.ToInt16(bufferSQL, 30) + "," +                  //d4
                    BitConverter.ToInt16(bufferSQL, 32) + "," +                  //d5
                    (float)(BitConverter.ToInt16(bufferSQL, 34)) / 100 + "," +   //e2
                    (float)(BitConverter.ToInt16(bufferSQL, 36)) / 100 + "," +   //e3
                    (float)(BitConverter.ToInt16(bufferSQL, 38)) / 100 + "," +   //e4
                    (float)(BitConverter.ToInt16(bufferSQL, 40)) / 100 + "," +   //e5
                    (float)(BitConverter.ToInt16(bufferSQL, 42)) / 100 + "," +   //n1l
                    (float)(BitConverter.ToInt16(bufferSQL, 44)) / 100 + "," +   //n1p
                    (float)(BitConverter.ToInt16(bufferSQL, 46)) / 100 + "," +   //n2l
                    (float)(BitConverter.ToInt16(bufferSQL, 48)) / 100 + "," +   //n2p
                    (float)(BitConverter.ToInt16(bufferSQL, 50)) / 100 + "," +   //n3l
                    (float)(BitConverter.ToInt16(bufferSQL, 52)) / 100 + "," +   //n3p
                    (float)(BitConverter.ToInt16(bufferSQL, 54)) / 100 + "," +   //n4l
                    (float)(BitConverter.ToInt16(bufferSQL, 56)) / 100 + "," +   //n4p
                    (float)(BitConverter.ToInt16(bufferSQL, 58)) / 100 + "," +   //n5l
                    (float)(BitConverter.ToInt16(bufferSQL, 60)) / 100 + "," +   //n5p
                    (float)(BitConverter.ToInt16(bufferSQL, 68)) / 100 + "," +   //reserv1
                    (float)(BitConverter.ToInt16(bufferSQL, 70)) / 100 + "," +   //reserv2
                    (float)(BitConverter.ToInt16(bufferSQL, 72)) / 100 + "," +   //t1
                    (float)(BitConverter.ToInt16(bufferSQL, 74)) / 100 + "," +   //t2
                    (float)(BitConverter.ToInt16(bufferSQL, 76)) / 100 + "," +   //t3
                    (float)(BitConverter.ToInt16(bufferSQL, 78)) / 100 + "," +   //t4
                    (float)(BitConverter.ToInt16(bufferSQL, 80)) / 100 + "," +   //t1l
                    (float)(BitConverter.ToInt16(bufferSQL, 82)) / 100 + "," +   //t2l
                    (float)(BitConverter.ToInt16(bufferSQL, 84)) / 100 + "," +   //t3l
                    (float)(BitConverter.ToInt16(bufferSQL, 86)) / 100 + "," +   //t4l
                    (float)(BitConverter.ToInt16(bufferSQL, 88)) / 100 + "," +   //t1p
                    (float)(BitConverter.ToInt16(bufferSQL, 90)) / 100 + "," +   //t2p
                    (float)(BitConverter.ToInt16(bufferSQL, 92)) / 100 + "," +   //t3p
                    (float)(BitConverter.ToInt16(bufferSQL, 94)) / 100 + "," +   //t4p
                    (float)(BitConverter.ToInt16(bufferSQL, 96)) / 100 + "," +   //t1z
                    (float)(BitConverter.ToInt16(bufferSQL, 98)) / 100 + "," +   //t2z
                    (float)(BitConverter.ToInt16(bufferSQL, 100)) / 100 + "," +  //t3z
                    (float)(BitConverter.ToInt16(bufferSQL, 112)) / 100 + "," +  //t4z
                    (float)(BitConverter.ToInt16(bufferSQL, 114)) / 10 + "," +   //erazm
                    (float)(BitConverter.ToInt16(bufferSQL, 116)) / 100 + "," +  //ivozbrazm
                    (float)(BitConverter.ToInt16(bufferSQL, 118)) / 10 + "," +   //izadrazm 
                    (float)(BitConverter.ToInt16(bufferSQL, 120)) / 10 + "," +   //w1
                    (float)(BitConverter.ToInt16(bufferSQL, 122)) / 10 + "," +   //w2v
                    (float)(BitConverter.ToInt16(bufferSQL, 124)) / 10 + "," +   //w2n
                    (float)(BitConverter.ToInt16(bufferSQL, 126)) / 10 + "," +   //w3v
                    (float)(BitConverter.ToInt16(bufferSQL, 128)) / 10 + "," +   //w3n
                    (float)(BitConverter.ToInt16(bufferSQL, 130)) / 10 + "," +   //w4v
                    (float)(BitConverter.ToInt16(bufferSQL, 132)) / 10 + "," +   //w4n
                    (float)(BitConverter.ToInt16(bufferSQL, 134)) / 10 + "," +   //w5v
                    (float)(BitConverter.ToInt16(bufferSQL, 136)) / 10 + "," +   //w5n
                    (float)(BitConverter.ToInt16(bufferSQL, 138)) / 10 + "," +   //wmot
                    BitConverter.ToInt16(bufferSQL, 140) + "," +                 //imot
                    BitConverter.ToInt16(bufferSQL, 142) + "," +                 //izadmot
                    (float)(BitConverter.ToInt16(bufferSQL, 144)) / 10 + "," +   //u1
                    (float)(BitConverter.ToInt16(bufferSQL, 146)) / 10 + "," +   //u2v
                    (float)(BitConverter.ToInt16(bufferSQL, 148)) / 10 + "," +   //u2n
                    (float)(BitConverter.ToInt16(bufferSQL, 150)) / 10 + "," +   //u3v
                    (float)(BitConverter.ToInt16(bufferSQL, 152)) / 10 + "," +   //u3n
                    (float)(BitConverter.ToInt16(bufferSQL, 154)) / 10 + "," +   //u4v
                    (float)(BitConverter.ToInt16(bufferSQL, 156)) / 10 + "," +   //u4n
                    (float)(BitConverter.ToInt16(bufferSQL, 158)) / 10 + "," +   //u5v
                    (float)(BitConverter.ToInt16(bufferSQL, 160)) / 10 + "," +   //u5n
                    (float)(BitConverter.ToInt16(bufferSQL, 162)) / 10 + "," +   //umot
                    BitConverter.ToInt16(bufferSQL, 164) + "," +                 //i1
                    BitConverter.ToInt16(bufferSQL, 166) + "," +                 //i2v
                    BitConverter.ToInt16(bufferSQL, 168) + "," +                 //i2n
                    BitConverter.ToInt16(bufferSQL, 170) + "," +                 //i3v
                    BitConverter.ToInt16(bufferSQL, 172) + "," +                 //i3n
                    BitConverter.ToInt16(bufferSQL, 174) + "," +                 //i4v
                    BitConverter.ToInt16(bufferSQL, 176) + "," +                 //i4n
                    BitConverter.ToInt16(bufferSQL, 178) + "," +                 //i5v
                    BitConverter.ToInt16(bufferSQL, 180) + "," +                 //i5n
                    (float)(BitConverter.ToInt16(bufferSQL, 192)) / 10 + "," +   //rtv
                    (float)(BitConverter.ToInt16(bufferSQL, 194)) / 10 + "," +   //dt1
                    (float)(BitConverter.ToInt16(bufferSQL, 196)) / 10 + "," +   //dt2
                    (float)(BitConverter.ToInt16(bufferSQL, 198)) / 10 + "," +   //dt3
                    (float)(BitConverter.ToInt16(bufferSQL, 200)) / 10 + "," +   //dt4
                    (float)(BitConverter.ToInt16(bufferSQL, 202)) / 10 + "," +   //grt
                    (float)(BitConverter.ToInt16(bufferSQL, 204)) / 10 + "," +   //trt
                    (float)(BitConverter.ToInt16(bufferSQL, 206)) / 10 + "," +   //mv1
                    (float)(BitConverter.ToInt16(bufferSQL, 208)) / 10 + "," +   //mv2
                    (float)(BitConverter.ToInt16(bufferSQL, 210)) / 10 + "," +   //mv3
                    (float)(BitConverter.ToInt16(bufferSQL, 62)) / 10 + "," +    //dh1
                    (float)(BitConverter.ToInt16(bufferSQL, 64)) / 10 + "," +    //dh5
                    BitConverter.ToInt16(bufferSQL, 216) + "," +                 //os1klvb
                    BitConverter.ToInt16(bufferSQL, 218) + "," +                 //rezerv
                    BitConverter.ToInt16(bufferSQL, 220) +                       //mezdoza4
                    ")";


                //"1,2,3,4,5,1,5,6,1234,11,22,33,1,2,3,4,5,2,3,4,5,11,12,21,22,31,32,41,42,51,52,200,200,1,2,3,4,11,21,31,41,12,22,32,42,13,23,33,43,111,222,333,1,21,22,31,32,41,42,51,52,444,555,666,1,21,22,31,32,41,42,51,52,777,1,21,22,33,33,4,4,5,5,6,1,2,3,4,7,8,1,2,3,1,5,6,7,1001)";

                SqlCommand command = new SqlCommand(sqlExpression, con);
                int number = command.ExecuteNonQuery();
                if (number==1)
                {
                    //Ellipse101ms.Fill = onOK;
                }
                else
                {
                    //Ellipse101ms.Fill = onError;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка сохранеия в БД кол-во записей = " + number.ToString());
                    Console.ResetColor();
                    //LogSystem.WriteEventLog("ProDaveStan", "Стан", "Ошибка сохранеия в БД кол - во записей = " + number.ToString() + " Запрос = " + sqlExpression, EventLogEntryType.Error);
                    System.Diagnostics.Debug.WriteLine("Ошибка сохранеия в БД кол-во записей = " + number.ToString());
                    lstStatus.Items.Add(DateTime.Now.ToString()+" - Ошибка сохранеия в БД кол-во записей = " + number.ToString());
                }


                //Console.WriteLine("{0} Добавлено {1} записей", DateTime.Now.ToString("HH.mm.ss.fff"),number);

                

                #endregion

                

               
            }
            catch (Exception ex)
            {
                //Ellipse101ms.Fill = onError;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Error в SQL стан - > БД " + ex.Message);
                Console.ResetColor();
                //LogSystem.WriteEventLog("ProDaveStan", "Стан", "Error в SQL стан - > БД " + ex.Message, EventLogEntryType.Error);
                System.Diagnostics.Debug.WriteLine("Error в SQL стан - > БД " + ex.Message);
                lstStatus.Items.Add("Error в SQL стан - > БД " + ex.Message);

                throw;
            }
            

            

        }

        private void Stan_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clStan = new ClassStan(stan100ms:true, stan1s:true, stan200ms:true, NetSend:false);
            clStan.Start();

        }

        private void ConvertBufferSQLandWriteTable()
        {
            try
            {
                if (sbStan100.IsSwitching1)
                {




                    //Ellipse101ms.Fill = offLed;


                    //Из критичной секции получаем значения из PLC
                    Thread tSQL = new Thread(BufferSQLToBufferPLC);
                    tSQL.Start();

                    //Определяемся с началом записи и очищаем таблицу
                    //если не конец прокатки то записываем данные в таблицу
                    //если конец рулона то скидываем сформированную таблицы в БД

                    dt101msStan.Rows.Add(
                            DateTime.Now,
                            (float)(BitConverter.ToInt16(bufferSQL, 0)) / 100,    //v1
                            (float)(BitConverter.ToInt16(bufferSQL, 2)) / 100,    //v2
                            (float)(BitConverter.ToInt16(bufferSQL, 4)) / 100,    //v3
                            (float)(BitConverter.ToInt16(bufferSQL, 6)) / 100,    //v4
                            (float)(BitConverter.ToInt16(bufferSQL, 8)) / 100,    //v5
                            (float)(BitConverter.ToInt16(bufferSQL, 10)) / 1000,  //h1    
                            (float)(BitConverter.ToInt16(bufferSQL, 12)) / 1000,  //h5
                            BitConverter.ToInt16(bufferSQL, 14),                  //b
                            (float)(BitConverter.ToInt16(bufferSQL, 16)) / 1000,  //dvip
                            (float)(BitConverter.ToInt16(bufferSQL, 18)) / 1000,  //drazm
                            (float)(BitConverter.ToInt16(bufferSQL, 20)) / 1000,  //dmot
                            (float)(BitConverter.ToInt16(bufferSQL, 22)) / 1000,  //vvip
                            BitConverter.ToInt16(bufferSQL, 24),                  //d1
                            BitConverter.ToInt16(bufferSQL, 26),                  //d2
                            BitConverter.ToInt16(bufferSQL, 28),                  //d3
                            BitConverter.ToInt16(bufferSQL, 30),                  //d4
                            BitConverter.ToInt16(bufferSQL, 32),                  //d5
                            (float)(BitConverter.ToInt16(bufferSQL, 34)) / 100,   //e2
                            (float)(BitConverter.ToInt16(bufferSQL, 36)) / 100,   //e3
                            (float)(BitConverter.ToInt16(bufferSQL, 38)) / 100,   //e4
                            (float)(BitConverter.ToInt16(bufferSQL, 40)) / 100,   //e5
                            (float)(BitConverter.ToInt16(bufferSQL, 42)) / 100,   //n1l
                            (float)(BitConverter.ToInt16(bufferSQL, 44)) / 100,   //n1p
                            (float)(BitConverter.ToInt16(bufferSQL, 46)) / 100,   //n2l
                            (float)(BitConverter.ToInt16(bufferSQL, 48)) / 100,   //n2p
                            (float)(BitConverter.ToInt16(bufferSQL, 50)) / 100,   //n3l
                            (float)(BitConverter.ToInt16(bufferSQL, 52)) / 100,   //n3p
                            (float)(BitConverter.ToInt16(bufferSQL, 54)) / 100,   //n4l
                            (float)(BitConverter.ToInt16(bufferSQL, 56)) / 100,   //n4p
                            (float)(BitConverter.ToInt16(bufferSQL, 58)) / 100,   //n5l
                            (float)(BitConverter.ToInt16(bufferSQL, 60)) / 100,   //n5p
                            (float)(BitConverter.ToInt16(bufferSQL, 68)) / 100,   //reserv1
                            (float)(BitConverter.ToInt16(bufferSQL, 70)) / 100,   //reserv2
                            (float)(BitConverter.ToInt16(bufferSQL, 72)) / 100,   //t1
                            (float)(BitConverter.ToInt16(bufferSQL, 74)) / 100,   //t2
                            (float)(BitConverter.ToInt16(bufferSQL, 76)) / 100,   //t3
                            (float)(BitConverter.ToInt16(bufferSQL, 78)) / 100,   //t4
                            (float)(BitConverter.ToInt16(bufferSQL, 80)) / 100,   //t1l
                            (float)(BitConverter.ToInt16(bufferSQL, 82)) / 100,   //t2l
                            (float)(BitConverter.ToInt16(bufferSQL, 84)) / 100,   //t3l
                            (float)(BitConverter.ToInt16(bufferSQL, 86)) / 100,   //t4l
                            (float)(BitConverter.ToInt16(bufferSQL, 88)) / 100,   //t1p
                            (float)(BitConverter.ToInt16(bufferSQL, 90)) / 100,   //t2p
                            (float)(BitConverter.ToInt16(bufferSQL, 92)) / 100,   //t3p
                            (float)(BitConverter.ToInt16(bufferSQL, 94)) / 100,   //t4p
                            (float)(BitConverter.ToInt16(bufferSQL, 96)) / 100,   //t1z
                            (float)(BitConverter.ToInt16(bufferSQL, 98)) / 100,   //t2z
                            (float)(BitConverter.ToInt16(bufferSQL, 100)) / 100,  //t3z
                            (float)(BitConverter.ToInt16(bufferSQL, 112)) / 100,  //t4z
                            (float)(BitConverter.ToInt16(bufferSQL, 114)) / 10,  //erazm
                            (float)(BitConverter.ToInt16(bufferSQL, 116)) / 100,  //ivozbrazm
                            (float)(BitConverter.ToInt16(bufferSQL, 118)) / 10,  //izadrazm 
                            (float)(BitConverter.ToInt16(bufferSQL, 120)) / 10,  //w1
                            (float)(BitConverter.ToInt16(bufferSQL, 122)) / 10,  //w2v
                            (float)(BitConverter.ToInt16(bufferSQL, 124)) / 10,  //w2n
                            (float)(BitConverter.ToInt16(bufferSQL, 126)) / 10,  //w3v
                            (float)(BitConverter.ToInt16(bufferSQL, 128)) / 10,  //w3n
                            (float)(BitConverter.ToInt16(bufferSQL, 130)) / 10,  //w4v
                            (float)(BitConverter.ToInt16(bufferSQL, 132)) / 10,  //w4n
                            (float)(BitConverter.ToInt16(bufferSQL, 134)) / 10,  //w5v
                            (float)(BitConverter.ToInt16(bufferSQL, 136)) / 10,  //w5n
                            (float)(BitConverter.ToInt16(bufferSQL, 138)) / 10,  //wmot
                            BitConverter.ToInt16(bufferSQL, 140),                 //imot
                            BitConverter.ToInt16(bufferSQL, 142),                 //izadmot
                            (float)(BitConverter.ToInt16(bufferSQL, 144)) / 10,   //u1
                            (float)(BitConverter.ToInt16(bufferSQL, 146)) / 10,   //u2v
                            (float)(BitConverter.ToInt16(bufferSQL, 148)) / 10,   //u2n
                            (float)(BitConverter.ToInt16(bufferSQL, 150)) / 10,   //u3v
                            (float)(BitConverter.ToInt16(bufferSQL, 152)) / 10,   //u3n
                            (float)(BitConverter.ToInt16(bufferSQL, 154)) / 10,   //u4v
                            (float)(BitConverter.ToInt16(bufferSQL, 156)) / 10,   //u4n
                            (float)(BitConverter.ToInt16(bufferSQL, 158)) / 10,   //u5v
                            (float)(BitConverter.ToInt16(bufferSQL, 160)) / 10,   //u5n
                            (float)(BitConverter.ToInt16(bufferSQL, 162)) / 10,   //umot
                            BitConverter.ToInt16(bufferSQL, 164),                 //i1
                            BitConverter.ToInt16(bufferSQL, 166),                 //i2v
                            BitConverter.ToInt16(bufferSQL, 168),                 //i2n
                            BitConverter.ToInt16(bufferSQL, 170),                 //i3v
                            BitConverter.ToInt16(bufferSQL, 172),                 //i3n
                            BitConverter.ToInt16(bufferSQL, 174),                 //i4v
                            BitConverter.ToInt16(bufferSQL, 176),                 //i4n
                            BitConverter.ToInt16(bufferSQL, 178),                 //i5v
                            BitConverter.ToInt16(bufferSQL, 180),                 //i5n
                            (float)(BitConverter.ToInt16(bufferSQL, 192)) / 10,   //rtv
                            (float)(BitConverter.ToInt16(bufferSQL, 194)) / 10,   //dt1
                            (float)(BitConverter.ToInt16(bufferSQL, 196)) / 10,   //dt2
                            (float)(BitConverter.ToInt16(bufferSQL, 198)) / 10,   //dt3
                            (float)(BitConverter.ToInt16(bufferSQL, 200)) / 10,   //dt4
                            (float)(BitConverter.ToInt16(bufferSQL, 202)) / 10,   //grt
                            (float)(BitConverter.ToInt16(bufferSQL, 204)) / 10,   //trt
                            (float)(BitConverter.ToInt16(bufferSQL, 206)) / 10,   //mv1
                            (float)(BitConverter.ToInt16(bufferSQL, 208)) / 10,   //mv2
                            (float)(BitConverter.ToInt16(bufferSQL, 210)) / 10,   //mv3
                            (float)(BitConverter.ToInt16(bufferSQL, 62)) / 10,   //dh1
                            (float)(BitConverter.ToInt16(bufferSQL, 64)) / 10,   //dh5
                            BitConverter.ToInt16(bufferSQL, 216),                 //os1klvb
                            BitConverter.ToInt16(bufferSQL, 218),                 //rezerv
                            BitConverter.ToInt16(bufferSQL, 220)                  //mezdoza4
                        );





                    D_tek_mot = (float)(BitConverter.ToInt16(bufferSQL, 20)) / 1000;
                    //Console.WriteLine("D_tek_mot="+ D_tek_mot);
                    h5w = (float)(BitConverter.ToInt16(bufferSQL, 12)) / 1000;
                    //Console.WriteLine("h5w=" + h5w);
                    speed4kl = (float)(BitConverter.ToInt16(bufferSQL, 6)) / 100;
                    //Console.WriteLine("speed4kl=" + speed4kl);
                    Bw = BitConverter.ToInt16(bufferSQL, 14);
                    //Console.WriteLine("Bw=" + Bw);




                    #region Расчет параметров прокатанного рулона после окончания прокатки

                    if (D_tek_mot > D_pred_mot)
                    {
                        if (D_pred_mot < 0.615)
                        {
                            Time_Start = DateTime.Now;
                            //Console.WriteLine("Time_Start" + Time_Start);
                        }
                    }

                    if ((Time_Start != new DateTime()) && (H5_work == 0) && (D_tek_mot > 0.7) && (speed4kl > 2))
                    {
                        H5_work = h5w;
                        B_Work = Bw;
                    }

                    #region Окончание прокатки рулона 
                    if ((Time_Start != new DateTime()) && (H5_work != 0) && (D_tek_mot < 0.610) && (D_tek_mot < D_pred_mot))
                    {


                        //Time_Stop = DateTime.Now;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Время начала записи SQL=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                        Ves_Work = (((((D_pred_mot * D_pred_mot) - 0.36F) * 3.141593F) / 4) * (B_Work / 1000)) * 7.85F;
                        Time_Stop = DateTime.Now;

                        Dlina_Work = ((Ves_Work / 7.85F) / (B_Work / 1000)) / (H5_work / 1000);

                        //Ellipse101ms.Fill = onOK;

                        #region Формируем данные для передачи в Базу Данных

                        //yyyy - MM - dd HH: mm: ss.fff
                        string strTimeStart = Time_Start.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        string strTimeStop = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        TimeSpan tp = Time_Stop.Subtract(Time_Start);
                        double dbltp = tp.TotalMilliseconds;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Время сбора информации (ms):" + dbltp.ToString());
                        Console.WriteLine("Кол-во строк полученных в системе:" + dt101msStan.Rows.Count.ToString());
                        Console.WriteLine("Среднее время цикла обновления данных:" + (dbltp / (dt101msStan.Rows.Count)).ToString());
                        Console.ResetColor();

                        string strNumberRulon = DateTime.Now.ToString("yyyyMMdd") + Time_Start.ToString("HHmm") + DateTime.Now.ToString("HHmm");


                        //Console.WriteLine("НомерРулона=" + DateTime.Now.ToString("yyyyMMdd")+ Time_Start.ToString("HHmm")+ DateTime.Now.ToString("HHmm"));

                        //string sql_1secQuery = "INSERT INTO stanm_proiz (dtcontrollerstart, dtcontrollerstop, ves, dlinna, h5, b, speed4kl) VALUES ('" + strTimeStart + "', '" + strTimeStop + "', " + Ves_Work + ", " + Dlina_Work + ", " + H5_work + ", " + B_Work + ", " + speed4kl + ")";

                        //Console.WriteLine(sql_1secQuery);
                        //int rowsdt = dt.Rows.Count;
                        //Console.WriteLine("кол-во строк в тавлице " + dt.Rows.Count);


                        //SQLCom = new SqlCommand(sql_1secQuery, SQLCon);
                        //int number = SQLCom.ExecuteNonQuery();
                        //Console.WriteLine("Добавлено объектов {0}", number);



                        //LogSystem.WriteEventLog("ProDaveStan", "Стан", "Error в SQL стан - > БД " + ex.Message, EventLogEntryType.Error);
                        //System.Diagnostics.Debug.WriteLine("Error в SQL стан - > БД " + ex.Message);

                        //создаем таблицу при условии что ее нет
                        string strTableName = "Stan100ms" + strNumberRulon;
                        string comBD = "if not exists (select * from sysobjects where name='" + strTableName + "' and xtype='U') create table RS2stan100ms" + strNumberRulon +
                            "(" +
                            "datetimeSQL datetime NOT NULL, " +
                            "v1 float NOT NULL, " +
                            "v2 float NOT NULL, " +
                            "v3 float NOT NULL, " +
                            "v4 float NOT NULL, " +
                            "v5 float NOT NULL, " +
                            "h1 float NOT NULL, " +
                            "h5 float NOT NULL, " +
                            "b smallint NOT NULL, " +
                            "dvip float NOT NULL, " +
                            "drazm float NOT NULL, " +
                            "dmot float NOT NULL, " +
                            "vvip float NOT NULL, " +
                            "d1 smallint NOT NULL, " +
                            "d2 smallint NOT NULL, " +
                            "d3 smallint NOT NULL, " +
                            "d4 smallint NOT NULL, " +
                            "d5 smallint NOT NULL, " +
                            "e2 float NOT NULL, " +
                            "e3 float NOT NULL, " +
                            "e4 float NOT NULL, " +
                            "e5 float NOT NULL, " +
                            "n1l float NOT NULL, " +
                            "n1p float NOT NULL, " +
                            "n2l float NOT NULL, " +
                            "n2p float NOT NULL, " +
                            "n3l float NOT NULL, " +
                            "n3p float NOT NULL, " +
                            "n4l float NOT NULL, " +
                            "n4p float NOT NULL, " +
                            "n5l float NOT NULL, " +
                            "n5p float NOT NULL, " +
                            "reserv1 float NOT NULL, " +
                            "reserv2 float NOT NULL, " +
                            "t1 float NOT NULL, " +
                            "t2 float NOT NULL, " +
                            "t3 float NOT NULL, " +
                            "t4 float NOT NULL, " +
                            "t1l float NOT NULL, " +
                            "t2l float NOT NULL, " +
                            "t3l float NOT NULL, " +
                            "t4l float NOT NULL, " +
                            "t1p float NOT NULL, " +
                            "t2p float NOT NULL, " +
                            "t3p float NOT NULL, " +
                            "t4p float NOT NULL, " +
                            "t1z float NOT NULL, " +
                            "t2z float NOT NULL, " +
                            "t3z float NOT NULL, " +
                            "t4z float NOT NULL, " +
                            "erazm float NOT NULL, " +
                            "ivozbrazm float NOT NULL, " +
                            "izadrazm float NOT NULL, " +
                            "w1 float NOT NULL, " +
                            "w2v float NOT NULL, " +
                            "w2n float NOT NULL, " +
                            "w3v float NOT NULL, " +
                            "w3n float NOT NULL, " +
                            "w4v float NOT NULL, " +
                            "w4n float NOT NULL, " +
                            "w5v float NOT NULL, " +
                            "w5n float NOT NULL, " +
                            "wmot float NOT NULL, " +
                            "imot smallint NOT NULL, " +
                            "izadmot smallint NOT NULL, " +
                            "u1 float NOT NULL, " +
                            "u2v float NOT NULL, " +
                            "u2n float NOT NULL, " +
                            "u3v float NOT NULL, " +
                            "u3n float NOT NULL, " +
                            "u4v float NOT NULL, " +
                            "u4n float NOT NULL, " +
                            "u5v float NOT NULL, " +
                            "u5n float NOT NULL, " +
                            "umot float NOT NULL, " +
                            "i1 smallint NOT NULL, " +
                            "i2v smallint NOT NULL, " +
                            "i2n smallint NOT NULL, " +
                            "i3v smallint NOT NULL, " +
                            "i3n smallint NOT NULL, " +
                            "i4v smallint NOT NULL, " +
                            "i4n smallint NOT NULL, " +
                            "i5v smallint NOT NULL, " +
                            "i5n smallint NOT NULL, " +
                            "rtv float NOT NULL, " +
                            "dt1 float NOT NULL, " +
                            "dt2 float NOT NULL, " +
                            "dt3 float NOT NULL, " +
                            "dt4 float NOT NULL, " +
                            "grt float NOT NULL, " +
                            "trt float NOT NULL, " +
                            "mv1 float NOT NULL, " +
                            "mv2 float NOT NULL, " +
                            "mv3 float NOT NULL, " +
                            "dh1 float NOT NULL, " +
                            "dh5 float NOT NULL, " +
                            "os1klvb smallint NOT NULL, " +
                            "rezerv smallint NOT NULL, " +
                            "mezdoza4 smallint NOT NULL)";

                        //Properties.Settings.Default.strConnect
                        //string connectingString = "Data Source = 192.168.0.46; Initial Catalog = rs2; User ID = rs2admin; Password = 159951";
                        //string connectingString = Properties.Settings.Default.strConnect;

                        string comWorkStan = "INSERT INTO work_stan( " +
                            "numberRulona," +
                            "start," +
                            "stop," +
                            "h5," +
                            "b," +
                            "ves," +
                            "dlinna," +
                            "countData," +
                            "timeData," +
                            "timePeriod" +
                            ") " +
                            "VALUES(" +
                            "@NumberRulon, " +
                            "@TimeStart, " +
                            "@TimeStop, " +
                            "@H5_work, " +
                            "@B_Work, " +
                            "@Ves_Work, " +
                            "@Dlina_Work, " +
                            "@CountData, " +
                            "@TimeData, " +
                            "@TimePeriod)";


                        
                        
                        try
                        {   
                            //создаем таблицу прокатанного рулона
                            using (SqlConnection con1 = new SqlConnection(connectingString))
                            {
                                con1.Open();
                                SqlCommand command = new SqlCommand(comBD, con1);
                                int WriteSQL = command.ExecuteNonQuery();
                                if (WriteSQL == -1)
                                {
                                    //Console.WriteLine("Таблицу создали");

                                }
                                else if (WriteSQL == 0)
                                {
                                    //Console.WriteLine("Таблица заново пересоздана");

                                }
                            }
                            //записываем в таблицу прокатанного рулона данные по прокатке этого рулона
                            using (SqlConnection con2 = new SqlConnection(connectingString))
                            {
                                con2.Open();
                                using (var bulk = new SqlBulkCopy(con2))
                                {
                                    bulk.DestinationTableName = strTableName;
                                    bulk.WriteToServer(dt101msStan);
                                }
                            }
                            //Добавляем в таблицу прокатанных рулонов данные по рулонам
                            using (SqlConnection con3 = new SqlConnection(connectingString))
                            {
                                con3.Open();
                                SqlCommand command = new SqlCommand(comWorkStan, con3);

                                command.Parameters.AddWithValue("@NumberRulon", strNumberRulon);

                                command.Parameters.AddWithValue("@TimeStart", Time_Start);
                                command.Parameters.AddWithValue("@TimeStop", Time_Stop);
                                command.Parameters.AddWithValue("@H5_work", H5_work);
                                command.Parameters.AddWithValue("@B_Work", B_Work);
                                command.Parameters.AddWithValue("@Ves_Work", Ves_Work);
                                command.Parameters.AddWithValue("@Dlina_Work", Dlina_Work);
                                command.Parameters.AddWithValue("@CountData", dt101msStan.Rows.Count);
                                command.Parameters.AddWithValue("@TimeData", dbltp);
                                command.Parameters.AddWithValue("@TimePeriod", dbltp/(dt101msStan.Rows.Count));

                               //Console.WriteLine("comWorkStan=" + comWorkStan);


                                int WriteSQL = command.ExecuteNonQuery();
                                if (WriteSQL == 1)
                                {
                                    //Console.WriteLine("Данные добавлены.");

                                }
                                else
                                {
                                    //Console.WriteLine("Error - таблица пропушена");

                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("Ошибка - " + ex.Message);
                            Console.ResetColor();
                        }






                        #endregion

                        //lstStatus.Items.Add(strTimeStop + " - Номер рулона=" + strNumberRulon + ", кол-во записей в таблице =" + dt.Rows.Count);


                        H5_work = 0; B_Work = 0; Ves_Work = 0; Dlina_Work = 0;
                        Time_Start = new DateTime();
                        Time_Stop = new DateTime();
                        dt101msStan.Clear(); //очистка таблицы 100мс

                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Время окончания записи SQL=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    }

                    D_pred_mot = D_tek_mot;

                    #endregion
                    #endregion
                }
            }
            catch (Exception ex)
            {
                //LogSystem.WriteEventLog("ProDaveStan", "Стан", "Ошибка SQL стан (101ms) - "+ex.ToString(), EventLogEntryType.Error);
                //System.Diagnostics.Debug.WriteLine("Ошибка сохранеия в БД кол-во записей = " + number.ToString());
                lstStatus.Items.Add(DateTime.Now.ToString() + " - Ошибка 101 ms= " + ex.ToString());
                
            }


        }
        
        
        #endregion

        #region Messages
        private void TicTimerMessage(object state)
        {
            this.Dispatcher.Invoke((Action)this.UpdateFormMessage); //выполняем в том же потоке.
        }

        private void UpdateFormMessage()
        {
            try
            {
                if (sbStanMessage.IsSwitching1)
                {
                    //Ellipse200ms.Fill = onOK;
                    //Console.WriteLine(string.Format("\t\t {0}  ({1})  {2}", "Message 200mc", DateTime.Now - dtMessage, Thread.CurrentThread.ManagedThreadId));
                    

                    Thread tMessage = new Thread(BufferMessageToBufferPLC);
                    tMessage.Start();
                    dtMessage = DateTime.Now;

                    float speed = (float)(BitConverter.ToInt16(bufferMessage, 16)) / 100;

                   
                        

                       
                        int numberMessage = 0;

                        for (int i = 0; i < 15; i++) //ограничил массив сообщений
                        {
                            //Console.WriteLine("byte " + i);
                            for (int b = 0; b < 8; b++)
                            {


                                int z = Convert.ToInt32(Math.Pow(2, b));
                                //Console.WriteLine("bit"+z);
                                if (((byte)(bufferMessageOld[i] & z) - (byte)(bufferMessage[i] & z)) < 0)
                                {
                                    //Console.WriteLine("------<0-----------------byte=" +i+ " bit="+z);
                                    //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " - " + "Сообщение " + " Скорость " + speed);
                                    if (MessageStan[numberMessage].statusMenshe != 0)
                                    {
                                        string mes = MessageStan[numberMessage].MinusMess;
                                        int status = MessageStan[numberMessage].statusMenshe;
                                        //Console.WriteLine("(" + dtMessage.ToString("HH:mm:ss.fff") + ")" + i + "-" + z + "=" + numberMessage + "----" + mes);

                                        dtMessagestan.Rows.Add(dtMessage.ToString("HH:mm:ss.fff"), status, mes, speed);


                                    }

                                    



                                }
                                else if (((byte)(bufferMessageOld[i] & z) - (byte)(bufferMessage[i] & z)) > 0)
                                {
                                    
                                    if (MessageStan[numberMessage].statusBolshe != 0)
                                    {
                                        string mes = MessageStan[numberMessage].PlusMess;
                                        int status = MessageStan[numberMessage].statusBolshe;
                                       

                                        dtMessagestan.Rows.Add(dtMessage.ToString("HH:mm:ss.fff"), status, mes, speed);

                                        
                                        
                                    }

                                    
                                }


                                numberMessage++;
                            }

                        }

                    
                    //TODO запись сообщений в БД каждую минуту                   
                    if (writeMessage > 300)
                    {
                        //сохраняем через 300 циклов  => 1 минуте
                        writeMessage = 0; //если циклов больше 

                            //Console.WriteLine(DateTime.Now + " Скидываем сообщения");
                            
                            string strTableName = "StanMessage" + numberTable;
                            
                            string comBDMessage  = "if not exists (select * from sysobjects where name='" + strTableName + "' and xtype='U') create table " + strTableName +
                                    "(" +
                                    "dtmes datetime NOT NULL, " +
                                    "status int NOT NULL, " +
                                    "message text NOT NULL, " +
                                    "speed float NOT NULL)";

                            //создаем таблицу сообщений стана 
                            using (SqlConnection con1Mess = new SqlConnection(connectingString))
                            {
                                con1Mess.Open();
                                SqlCommand command = new SqlCommand(comBDMessage, con1Mess);
                                int WriteSQL = command.ExecuteNonQuery();
                                if (WriteSQL == -1)
                                {
                                    //Console.WriteLine("Таблицу создали");

                                }
                                else if (WriteSQL == 0)
                                {
                                    //Console.WriteLine("Таблица заново пересоздана");

                                }
                            }
                            if (dtMessagestan.Rows.Count>0)
                            {
                                //записываем в таблицу прокатанного рулона данные по прокатке этого рулона
                                using (SqlConnection con2Mess = new SqlConnection(connectingString))
                                {
                                    con2Mess.Open();
                                    using (var bulkMessage = new SqlBulkCopy(con2Mess))
                                    {
                                        bulkMessage.DestinationTableName = strTableName;
                                        bulkMessage.WriteToServer(dtMessagestan);
                                        Console.BackgroundColor = ConsoleColor.Yellow;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.WriteLine("Кол-во сообщений записанных в таблицу "+ strTableName + " равно " + dtMessagestan.Rows.Count + ". Время записи - " + DateTime.Now);
                                        Console.ResetColor();

                                    }
                                }
                                dtMessagestan.Clear();
                            }
                            else
                            {
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine("Сообщений не было с "+DateTime.Now.AddMinutes(-1).ToString()+ " по " + DateTime.Now.ToString());
                                    Console.ResetColor();
                            }

                        
                    }
                    else
                    {
                        writeMessage++;
                        //Console.WriteLine("writeMessage" + writeMessage);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                throw;
            }
        }
        #endregion

        #region 1s 

        private void TicTimer1s(object state)
        {
            this.Dispatcher.Invoke((Action)this.UpdateForm1s); //выполняем в том же потоке.
        }

        private void UpdateForm1s()
        {

            try
            {
                string strTableName = "Stan1s" + numberTable;


                //numberTable = DateTime.Now.ToString();

                if (sbStan1s.IsSwitching1)
                {


                    string comBD = "if not exists (select * from sysobjects where name ='" + strTableName + "' and xtype='U') create table " + strTableName + 
                        "(" +
                        "datetime1s datetime NOT NULL, " +
                        "s191HL int NOT NULL, " +
                        "s192HL int NOT NULL, " +
                        "s193BL int NOT NULL, " +
                        "s194BL int NOT NULL, " +
                        "s191HR int NOT NULL, " +
                        "s192HR int NOT NULL, " +
                        "s193BR int NOT NULL, " +
                        "s194BR int NOT NULL, " +
                        "s281NL int NOT NULL, " +
                        "s282NL int NOT NULL, " +
                        "s283BL int NOT NULL, " +
                        "s284BL int NOT NULL, " +
                        "s281NR int NOT NULL, " +
                        "s282NR int NOT NULL, " +
                        "s283BR int NOT NULL, " +
                        "s284BR int NOT NULL, " +
                        "s301BL int NOT NULL, " +
                        "s302BL int NOT NULL, " +
                        "s303HL int NOT NULL, " +
                        "s304HL int NOT NULL, " +
                        "s301BR int NOT NULL, " +
                        "s302BR int NOT NULL, " +
                        "s303HR int NOT NULL, " +
                        "s304HR int NOT NULL, " +
                        "s321BL int NOT NULL, " +
                        "s322BL int NOT NULL, " +
                        "s323HL int NOT NULL, " +
                        "s324HL int NOT NULL, " +
                        "s321BR int NOT NULL, " +
                        "s322BR int NOT NULL, " +
                        "s323HR int NOT NULL, " +
                        "s324HR int NOT NULL, " +
                        "s341BL int NOT NULL, " +
                        "s342BL int NOT NULL, " +
                        "s343HL int NOT NULL, " +
                        "s344HL int NOT NULL, " +
                        "s341BR int NOT NULL, " +
                        "s342BR int NOT NULL, " +
                        "s343HR int NOT NULL, " +
                        "s344HR int NOT NULL, " +
                        "s461L int NOT NULL, " +
                        "s462L int NOT NULL, " +
                        "s463L int NOT NULL, " +
                        "s461R int NOT NULL, " +
                        "s462R int NOT NULL, " +
                        "s463R int NOT NULL, " +
                        "sG11L int NOT NULL, " +
                        "sG12L int NOT NULL, " +
                        "sG13L int NOT NULL, " +
                        "sG14L int NOT NULL, " +
                        "sG15L int NOT NULL, " +
                        "sG16L int NOT NULL, " +
                        "sG17L int NOT NULL, " +
                        "sG11R int NOT NULL, " +
                        "sG12R int NOT NULL, " +
                        "sG13R int NOT NULL, " +
                        "sG14R int NOT NULL, " +
                        "sG15R int NOT NULL, " +
                        "sG16R int NOT NULL, " +
                        "sG17R int NOT NULL, " +
                        "sG21L int NOT NULL, " +
                        "sG22L int NOT NULL, " +
                        "sG23L int NOT NULL, " +
                        "sG24L int NOT NULL, " +
                        "sG25L int NOT NULL, " +
                        "sG26L int NOT NULL, " +
                        "sG27L int NOT NULL, " +
                        "sG21R int NOT NULL, " +
                        "sG22R int NOT NULL, " +
                        "sG23R int NOT NULL, " +
                        "sG24R int NOT NULL, " +
                        "sG25R int NOT NULL, " +
                        "sG26R int NOT NULL, " +
                        "sG27R int NOT NULL, " +
                        "sD12 float NOT NULL, " +
                        "sD13 float NOT NULL, " +
                        "sD14 float NOT NULL, " +
                        "sD15 float NOT NULL, " +
                        "sD16 float NOT NULL, " +
                        "sD17 float NOT NULL, " +
                        "sD18 float NOT NULL, " +
                        "sD19 float NOT NULL, " +
                        "sD20 float NOT NULL, " +
                        "sU64 int NOT NULL, " +
                        "sRasxCD int NOT NULL " +
                        ")";

                    
                    // Console.WriteLine(string.Format("\t\t {0} ({1}) {2}","Расчет 1c", DateTime.Now - dt1s, Thread.CurrentThread.ManagedThreadId));
                    dt1s = DateTime.Now;
                    Thread t1s = new Thread(Buffer1sToBufferPLC);
                    t1s.Start();

                    //Console.WriteLine(buffer1s[0]);
                    #region Добавление данных в таблицу dt1sStan

                    
                    dt1sStan.Rows.Add(
                            DateTime.Now,
                            (buffer1s[0]),
                            (buffer1s[1]),
                            (buffer1s[2]),
                            (buffer1s[3]),
                            (buffer1s[4]),
                            (buffer1s[5]),
                            (buffer1s[6]),
                            (buffer1s[7]),
                            (buffer1s[8]),
                            (buffer1s[9]),
                            (buffer1s[10]),
                            (buffer1s[11]),
                            (buffer1s[12]),
                            (buffer1s[13]),
                            (buffer1s[14]),
                            (buffer1s[15]),
                            (buffer1s[16]),
                            (buffer1s[17]),
                            (buffer1s[18]),
                            (buffer1s[19]),
                            (buffer1s[20]),
                            (buffer1s[21]),
                            (buffer1s[22]),
                            (buffer1s[23]),
                            (buffer1s[24]),
                            (buffer1s[25]),
                            (buffer1s[26]),
                            (buffer1s[27]),
                            (buffer1s[28]),
                            (buffer1s[29]),
                            (buffer1s[30]),
                            (buffer1s[31]),
                            (buffer1s[32]),
                            (buffer1s[33]),
                            (buffer1s[34]),
                            (buffer1s[35]),
                            (buffer1s[36]),
                            (buffer1s[37]),
                            (buffer1s[38]),
                            (buffer1s[39]),
                            (buffer1s[40]),
                            (buffer1s[41]),
                            (buffer1s[42]),
                            (buffer1s[43]),
                            (buffer1s[44]),
                            (buffer1s[45]),
                            (buffer1s[46]),
                            (buffer1s[47]),
                            (buffer1s[48]),
                            (buffer1s[49]),
                            (buffer1s[50]),
                            (buffer1s[51]),
                            (buffer1s[52]),
                            (buffer1s[53]),
                            (buffer1s[54]),
                            (buffer1s[55]),
                            (buffer1s[56]),
                            (buffer1s[57]),
                            (buffer1s[58]),
                            (buffer1s[59]),
                            (buffer1s[60]),
                            (buffer1s[61]),
                            (buffer1s[62]),
                            (buffer1s[63]),
                            (buffer1s[64]),
                            (buffer1s[65]),
                            (buffer1s[66]),
                            (buffer1s[67]),
                            (buffer1s[68]),
                            (buffer1s[69]),
                            (buffer1s[70]),
                            (buffer1s[71]),
                            (buffer1s[72]),
                            (buffer1s[73]),
                            (float)(buffer1s[74]) / 10,
                            (float)(buffer1s[75]) / 10,
                            (float)(buffer1s[76]) / 10,
                            (float)(buffer1s[77]) / 10,
                            (float)(buffer1s[78]) / 10,
                            (float)(buffer1s[79]) / 10,
                            (float)(buffer1s[80]) / 10,
                            (float)(buffer1s[81]) / 10,
                            (float)(buffer1s[82]) / 10,
                            (int)((buffer1s[83]) * 10),
                            (int)((buffer1s[84]) * 10));


                    #endregion

                    #region Создаем таблицу и записываем значения если закончена прокатка рулона

                    

                    if (write1s>60) //записываем раз в 60 секунд
                    {
                        write1s = 0;
                        
                        try
                        {
                            //создаем таблицу значений с циклом 1s
                            using (SqlConnection con1s = new SqlConnection(connectingString))
                            {
                                con1s.Open();
                                SqlCommand command = new SqlCommand(comBD, con1s);
                                int WriteSQL = command.ExecuteNonQuery();
                                if (WriteSQL == -1)
                                {
                                    //Console.WriteLine("Таблицу 1s создали c именем " + strTableName);
                                }
                                else if (WriteSQL == 0)
                                {
                                    //Console.WriteLine("Таблица заново пересоздана");

                                }
                            }
                            //записываем в таблицу прокатанного рулона данные по прокатке этого рулона
                            using (SqlConnection con21s = new SqlConnection(connectingString))
                            {
                                con21s.Open();
                                using (var bulk = new SqlBulkCopy(con21s))
                                {
                                    bulk.DestinationTableName = strTableName;
                                    bulk.WriteToServer(dt1sStan);
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine("Данные 1сек в таблицу " + strTableName +
                                        " записаны, кол-во строк " + dt1sStan.Rows.Count+". Время записи - "+DateTime.Now);
                                    Console.ResetColor();

                                    dt1sStan.Clear(); //очистка таблицы 
                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("Ошибка - " + ex.Message);
                            Console.ResetColor();
                        }
                    }

                    else
                    {
                        //Console.WriteLine("write1s  " + write1s);
                        write1s++;
                    }

                    #endregion

                    #region Перевалки 
                    
                    int buf85 = BitConverter.ToInt16(buffer1s, 85);
                    int buf87 = BitConverter.ToInt16(buffer1s, 87);
                    int buf89 = BitConverter.ToInt16(buffer1s, 89);
                    int buf91 = BitConverter.ToInt16(buffer1s, 91);
                    int buf93 = BitConverter.ToInt16(buffer1s, 93);
                    
                    if (d1_pred == 0) d1_pred = buf85;
                    if (d2_pred == 0) d2_pred = buf87;
                    if (d3_pred == 0) d3_pred = buf89;
                    if (d4_pred == 0) d4_pred = buf91;
                    if (d5_pred == 0) d5_pred = buf93;
                    bool blSave = false;
                    

                    if (d1_pred != BitConverter.ToInt16(buffer1s, 85))
                    {
                        blSave = true;
                        dtPerevalkiStan.Rows.Add(DateTime.Now, buf85, 0, 0, 0, 0);
                    }
                    if (d2_pred != BitConverter.ToInt16(buffer1s, 87))
                    {
                        blSave = true;
                        dtPerevalkiStan.Rows.Add(DateTime.Now, 0, buf87, 0, 0, 0);
                    }
                    if (d3_pred != BitConverter.ToInt16(buffer1s, 89))
                    {
                        blSave = true;
                        dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, buf89, 0, 0);
                    }
                    if (d4_pred != BitConverter.ToInt16(buffer1s, 91))
                    {
                        blSave = true;
                        dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, 0, buf91, 0);
                    }
                    if (d5_pred != BitConverter.ToInt16(buffer1s, 93))
                    {
                        blSave = true;
                        dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, 0, 0, buf93);
                    }

                    d1_pred = buf85;
                    d2_pred = buf87;
                    d3_pred = buf89;
                    d4_pred = buf91;
                    d5_pred = buf93;

                    

                    #region Перевалки сохраняем в БД
                    if (blSave)
                    {
                        string strTableNamePerevalki = "StanPerevalki"+DateTime.Now.ToString("yyyyMM");
                        string comBDPerevalki = "if not exists (select * from sysobjects where name='" + strTableNamePerevalki + "' and xtype='U') create table " + strTableNamePerevalki +
                                "(" +
                                "dtPerevalki datetime NOT NULL, " +
                                "kl1 int NOT NULL, " +
                                "kl2 int NOT NULL, " +
                                "kl3 int NOT NULL, " +
                                "kl4 int NOT NULL, " +
                                "kl5 int NOT NULL )";

                        //создаем таблицу значений Перевалок
                        using (SqlConnection conPerevalki1 = new SqlConnection(connectingString))
                        {
                            conPerevalki1.Open();
                            SqlCommand command = new SqlCommand(comBDPerevalki, conPerevalki1);
                            int WriteSQL = command.ExecuteNonQuery();
                            if (WriteSQL == -1)
                            {
                                //Console.WriteLine("Таблицу 1s создали c именем " + strTableName);
                            }
                            else if (WriteSQL == 0)
                            {
                                //Console.WriteLine("Таблица заново пересоздана");

                            }
                        }
                        //записываем в таблицу прокатанного рулона данные по прокатке этого рулона
                        using (SqlConnection conPerevalki2 = new SqlConnection(connectingString))
                        {
                            conPerevalki2.Open();
                            using (var bulk = new SqlBulkCopy(conPerevalki2))
                            {
                                bulk.DestinationTableName = strTableNamePerevalki;
                                bulk.WriteToServer(dtPerevalkiStan);
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine("Данные перевалки в таблицу " + strTableNamePerevalki +
                                    " записаны, кол-во строк " + dtPerevalkiStan.Rows.Count + ". Время записи - " + DateTime.Now);
                                Console.ResetColor();

                                dtPerevalkiStan.Clear(); //очистка таблицы 
                            }
                        }

                    }

                    #endregion

                    //Console.WriteLine(d1_pred + "-" + d2_pred + "-" + d3_pred + "-" + d4_pred + "-" + d5_pred);

                    #endregion
                }






            }
            catch (Exception)
            {

                throw;
            }
            
        }
        #endregion

        #region Передача данных по сети для дальнейшего использовании в визуализации   

        private void TicTimer250msNet(object state)
        {
            this.Dispatcher.Invoke((Action)this.UpdateForm250ms); //выполняем в том же потоке.
        }

        private void UpdateForm250ms()
        {
            try
            {
                Thread tMessage = new Thread(BufferNetToBufferPLC);

               

            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Error в передачи по сети "+ ex.Message);
                Console.ResetColor();

            }
            
            
            
        }



        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // SQLconBD();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clStan.Stop();
           
        }

        private void SQLconBD()
        {
            string connectingString = "Data Source = 192.168.0.46; Initial Catalog = rs2; User ID = rs2admin; Password = 159951";

            con = new SqlConnection(connectingString);
            try
            {
                con.Open();
                Console.WriteLine("Подключены к серверу" + con.DataSource);
                Console.WriteLine("Сoстояние - " + con.State);
            }
            catch (SqlException ex)
            {
                string ExcMessage = DateTime.Now.ToString() + " Подключение к " + connectingString + " вызвало ошибку " + ex.Message;
                Console.Write(ExcMessage);
                throw;
            }
        }

        
    }
}
