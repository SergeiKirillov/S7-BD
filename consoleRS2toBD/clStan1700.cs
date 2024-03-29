﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using HWDiag;
using LoggerInSystem;

namespace consoleRS2toBD
{
    public class MessageClass
    {
        public int statusMenshe { get; set; }
        public int statusBolshe { get; set; }
        public string PlusMess { get; set; }
        public string MinusMess { get; set; }

        public MessageClass(int StatusMenshe, string MensheNul, int StatusBolshe, string BolsheNul)
        {
            this.statusMenshe = StatusMenshe;
            this.MinusMess = MensheNul;
            this.statusBolshe = StatusBolshe;
            this.PlusMess = BolsheNul;

        }

    }

    class clStan1700
    {
        Dictionary<string, ContData> stanData100ms = new Dictionary<string, ContData>
        {
            {"v1", new ContData(0,100,true)},
            {"v2", new ContData(2,100,true)},
            {"v3", new ContData(4,100,true)},
            {"v4", new ContData(6,100,true)},
            {"v5", new ContData(8,100,true)},
            {"h1", new ContData(10,1000,true)},
            {"h", new ContData(12,1000,true)},

            {"b", new ContData(14,1,false)},

            {"dvip", new ContData(16,1000,true)},
            {"drazm", new ContData(18,1000,true)},
            {"dmot", new ContData(20,1000,true)},
            {"vvip", new ContData(22,1000,true)},

            {"d1", new ContData(24,1,false)},
            {"d2", new ContData(26,1,false)},
            {"d3", new ContData(28,1,false)},
            {"d4", new ContData(30,1,false)},
            {"d5", new ContData(32,1,false)},

            {"e2", new ContData(34,100,true)},
            {"e3", new ContData(36,100,true)},
            {"e4", new ContData(38,100,true)},
            {"e5", new ContData(40,100,true)},

            {"n1l", new ContData(42,100,true)},
            {"n1p", new ContData(44,100,true)},
            {"n2l", new ContData(46,100,true)},
            {"n2p", new ContData(48,100,true)},
            {"n3l", new ContData(50,100,true)},
            {"n3p", new ContData(52,100,true)},
            {"n4l", new ContData(54,100,true)},
            {"n4p", new ContData(56,100,true)},
            {"n5l", new ContData(58,100,true)},
            {"n5p", new ContData(60,100,true)},

            {"reserv1", new ContData(68,100,true)},
            {"reserv2", new ContData(70,100,true)},

            {"t1", new ContData(72,100,true)},
            {"t2", new ContData(74,100,true)},
            {"t3", new ContData(76,100,true)},
            {"t4", new ContData(78,100,true)},

            {"t1l", new ContData(80,100,true)},
            {"t2l", new ContData(82,100,true)},
            {"t3l", new ContData(84,100,true)},
            {"t4l", new ContData(86,100,true)},

            {"t1p", new ContData(88,100,true)},
            {"t2p", new ContData(90,100,true)},
            {"t3p", new ContData(92,100,true)},
            {"t4p", new ContData(94,100,true)},

            {"t1z", new ContData(96,100,true)},
            {"t2z", new ContData(98,100,true)},
            {"t3z", new ContData(100,100,true)},
            {"t4z", new ContData(112,100,true)},

            {"erazm", new ContData(114,10,true)},
            {"ivozbrazm", new ContData(116,100,true)},
            {"izadrazm", new ContData(118,10,true)},

            {"w1", new ContData(120,10,true)},
            {"w2v", new ContData(122,10,true)},
            {"w2n", new ContData(124,10,true)},
            {"w3v", new ContData(126,10,true)},
            {"w3n", new ContData(128,10,true)},
            {"w4v", new ContData(130,10,true)},
            {"w4n", new ContData(132,10,true)},
            {"w5v", new ContData(134,10,true)},
            {"w5n", new ContData(136,10,true)},
            {"wmot", new ContData(138,10,true)},

            {"imot", new ContData(140,1,false)},
            {"izadmot", new ContData(142,1,false)},

            {"u1", new ContData(144,10,true)},
            {"u2v", new ContData(146,10,true)},
            {"u2n", new ContData(148,10,true)},
            {"u3v", new ContData(150,10,true)},
            {"u3n", new ContData(152,10,true)},
            {"u4v", new ContData(154,10,true)},
            {"u4n", new ContData(156,10,true)},
            {"u5v", new ContData(158,10,true)},
            {"u5n", new ContData(160,10,true)},
            {"umot", new ContData(162,10,true)},


            {"i1", new ContData(164,1,false)},
            {"i2v", new ContData(166,1,false)},
            {"i2n", new ContData(168,1,false)},
            {"i3v", new ContData(170,1,false)},
            {"i3n", new ContData(172,1,false)},
            {"i4v", new ContData(174,1,false)},
            {"i4n", new ContData(176,1,false)},
            {"i5v", new ContData(178,1,false)},
            {"i5n", new ContData(180,1,false)},


            {"rtv", new ContData(192,10,true)},
            {"dt1", new ContData(194,10,true)},
            {"dt2", new ContData(196,10,true)},
            {"dt3", new ContData(198,10,true)},
            {"dt4", new ContData(200,10,true)},
            {"grt", new ContData(202,10,true)},
            {"trt", new ContData(204,10,true)},
            {"mv1", new ContData(206,10,true)},
            {"mv2", new ContData(208,10,true)},
            {"mv3", new ContData(210,10,true)},
            {"dh1", new ContData(62,10,true)},
            {"dh5", new ContData(64,10,true)},

            {"os1klvb", new ContData(216,1,false)},
            {"rezerv", new ContData(218,1,false)},
            {"mezdoza4", new ContData(220,1,false)},
        };

        Dictionary<int, MessageClass> MessageStan = new Dictionary<int, MessageClass>()
        {
            
            [0] = new MessageClass(1, "Режим ТАК ДЕРЖАТЬ", 0, ""),
            [1] = new MessageClass(2, "Режим РАЗГОНА", 0, ""),
            [2] = new MessageClass(3, "Режим НОРМАЛЬНОГО ОСТАНОВА", 0, ""),
            [3] = new MessageClass(4, "Режим ФОРСИРОВАННОГО ОСТАНОВА", 0, ""),
            [4] = new MessageClass(5, "Режим ВЫПУСКА", 0, ""),
            [5] = new MessageClass(6, "Натяжение в 1 промежутке", 7, "Отсутствие натяжения в 1"),
            [6] = new MessageClass(6, "Натяжение в 2 промежутке", 7, "Отсутствие натяжения в 3"),
            [7] = new MessageClass(6, "Натяжение в 2 промежутке", 7, "Отсутствие натяжения в 3"),
            [8] = new MessageClass(2, "Кнопка ЗАПРАВКА", 0, ""),
            [9] = new MessageClass(1, "Ключ ТАК ДЕРЖАТЬ", 0, ""),
            [10] = new MessageClass(2, "Ключ РАЗГОН", 0, ""),
            [11] = new MessageClass(3, "Кнопка НОРМАЛЬНЫЙ ОСТАНОВ", 0, ""),
            [12] = new MessageClass(4, "Кнопка ФОРСИРОВАННЫЙ ОСТАНОВ", 0, ""),
            [13] = new MessageClass(6, "Максимальный перегруз", 7, "Отсутствие максимального перегруз"),
            [14] = new MessageClass(1, "Уставка рабочей скорости", 0, ""),
            [15] = new MessageClass(2, "Перегруз по скорости", 0, ""),
            [16] = new MessageClass(2, "ЛК моталки включены", 6, "ЛК моталки выключены"),
            [17] = new MessageClass(2, "ЛК разматывателя включены", 6, "ЛК разматывателя выключены"),
            [18] = new MessageClass(1, "Гидравлика 64 кг готова", 4, "Гидравлика 64 кг не готова"),
            [19] = new MessageClass(7, "РНЗ 12 выключено", 6, "РНЗ 12 включено"),
            [20] = new MessageClass(6, "РНЗ 23 включено", 7, "РНЗ 23 выключено"),
            [21] = new MessageClass(6, "РНЗ 34 включено", 7, "РНЗ 34 выключено"),
            [22] = new MessageClass(6, "ГРТ включено", 7, "ГРТ выключено"),
            [23] = new MessageClass(6, "ТРТ включено", 7, "ТРТ выключено"),
            [24] = new MessageClass(6, "Натяжение в 4 промежутке", 7, "Отсутствие натяжения в 4 промежутке"),
            [25] = new MessageClass(6, "Натяжение на моталке", 0, "Отсутствие натяжения на моталке"),
            [26] = new MessageClass(6, "Натяжение на разматывателе", 7, "Отсутствие натяжения на разматывателе"),
            [27] = new MessageClass(2, "ЛК клети 1 включены", 6, "ЛК клети 1 выключены"),
            [28] = new MessageClass(2, "ЛК клети 2 включены", 6, "ЛК клети 2 выключены"),
            [29] = new MessageClass(2, "ЛК клети 3 включены", 6, "ЛК клети 3 выключены"),
            [30] = new MessageClass(2, "ЛК клети 4 включены", 6, "ЛК клети 4 выключены"),
            [31] = new MessageClass(2, "ЛК клети 5 включены", 6, "ЛК клети 5 выключены"),
            [32] = new MessageClass(5, "Наличие полосы в толщиномере за 5 клетью", 7, "Отсутствие полосы в толщиномере за 5 клетью"),
            [33] = new MessageClass(1, "Ноль задания скорости", 2, "Поехали"),
            [34] = new MessageClass(1, "Сборка схемы стана", 4, "Развал схемы стана"),
            [35] = new MessageClass(4, "Максимальная скорость клети 1", 5, "Конец максимальной скорости клети 1"),
            [36] = new MessageClass(4, "Максимальная скорость клети 2", 5, "Конец максимальной скорости клети 2"),
            [37] = new MessageClass(4, "Максимальная скорость клети 3", 5, "Конец максимальной скорости клети 3"),
            [38] = new MessageClass(4, "Максимальная скорость клети 4", 5, "Конец максимальной скорости клети 4"),
            [39] = new MessageClass(4, "Максимальная скорость клети 5", 5, "Конец максимальной скорости клети 5"),
            [40] = new MessageClass(6, "РКДВ включен", 6, "РКДВ выключен"),
            [41] = new MessageClass(6, "РПВ включен", 6, "РПВ выключен"),
            [42] = new MessageClass(1, "РНВ12 включен", 4, "РНВ12 выключен"),
            [43] = new MessageClass(1, "РНВ23 включен", 4, "РНВ23 выключен"),
            [44] = new MessageClass(1, "РНВ34 включен", 4, "РНВ34 выключен"),
            [45] = new MessageClass(6, "РН45 включено", 7, "РН45 выключено"),
            [46] = new MessageClass(1, "РТВ включен", 7, "РТВ выключен"),
            [47] = new MessageClass(1, "Гидравлика 100 кг готова", 4, "Гидравлика 100 кг не готова"),
            [48] = new MessageClass(1, "ПЖТ Ж - 12 готова", 4, "ПЖТ Ж-12 не готова"),
            [49] = new MessageClass(1, "ПЖТ Ж - 13 готова", 4, "ПЖТ Ж-13 не готова"),
            [50] = new MessageClass(1, "ПЖТ Ж - 14 готова", 4, "ПЖТ Ж-14 не готова"),
            [51] = new MessageClass(1, "Смазка Ж-15 готова", 4, "Смазка Ж-15 не готова"),
            [52] = new MessageClass(1, "Смазка Ж-16 готова", 4, "Смазка Ж-16 не готова"),
            [53] = new MessageClass(5, "Начальные условия", 0, ""),
            [54] = new MessageClass(5, "Эмульсионная система готова", 7, "Эмульсионная система не готова"),
            [55] = new MessageClass(1, "Смазка Ж-17 готова", 4, "Смазка Ж-17 не готова"),
            [56] = new MessageClass(1, "Смазка Ж - 18 готова", 4, "Смазка Ж-18 не готова"),
            [57] = new MessageClass(1, "Смазка Ж - 19 готова", 4, "Смазка Ж-19 не готова"),
            [58] = new MessageClass(1, "Смазка Ж - 20 готова", 4, "Смазка Ж-20 не готова"),
            [59] = new MessageClass(1, "Температура в ПОУ нормальная", 4, "Температура в ПОУ высокая"),
            [60] = new MessageClass(1, "Давление редукторов низкое", 4, "Давление редукторов нормальное"),
            [61] = new MessageClass(1, "Давление ПЖТ низкое", 4, "Давление ПЖТ нормальное"),
            [62] = new MessageClass(1, "Вентиляция готова", 4, "Вентиляция не готова"),
            [63] = new MessageClass(1, "Синхронные двигатели готовы", 4, "Синхронные двигатели не готовы"),
            [64] = new MessageClass(1, "Ограждение моталки закрыто", 4, "Ограждение моталки открыто НО"),
            [65] = new MessageClass(1, "Захлестыватель у моталки НО", 4, "Захлестыватель отведен"),
            [66] = new MessageClass(1, "Высокая температура ПЖТ ГП", 4, "Нормальная температура ПЖТ ГП"),
            [67] = new MessageClass(4, "Перегруз клети 1", 5, "Конец перегруза клети 1"),
            [68] = new MessageClass(4, "Перегруз клети 2", 5, "Конец перегруза клети 2"),
            [69] = new MessageClass(4, "Перегруз клети 3", 5, "Конец перегруза клети 3"),
            [70] = new MessageClass(4, "Перегруз клети 4", 5, "Конец перегруза клети 4"),
            [71] = new MessageClass(4, "Перегруз клети 5", 5, "Конец перегруза клети 5"),
            [72] = new MessageClass(1, "Высокая температура ПЖТ СД", 4, "Нормальная температура ПЖТ СД"),
            [73] = new MessageClass(4, "Кнопка НО на ПУ старшего нажата", 0, ""),
            [74] = new MessageClass(0, "", 4, "Кнопка НО на ПУР нажата"),
            [75] = new MessageClass(0, "", 4, "Кнопка НО на ПУ1 нажата"),
            [76] = new MessageClass(0, "", 4, "Кнопка НО на ПУ2 нажата"),
            [77] = new MessageClass(0, "", 4, "Кнопка НО на ПУ3 нажата"),
            [78] = new MessageClass(0, "", 4, "Кнопка НО на ПУ4 нажата"),
            [79] = new MessageClass(0, "", 4, "Кнопка НО на ПУ5 нажата"),
            [80] = new MessageClass(4, "Кнопка ФО на ПУ старшего нажата", 0, ""),
            [81] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ5 нажата"),
            [82] = new MessageClass(0, "", 4, "Кнопка АО на ПУР нажата"),
            [83] = new MessageClass(4, "Провал натяжения на разматывателе", 1, "Восстановление натяжения на разматывателе ТД"),
            [84] = new MessageClass(4, "Провал натяжения в 1 промежутке ФО ", 1, "Восстановление натяжения в 1 промежутке ТД"),
            [85] = new MessageClass(4, "Провал натяжения в 2 промежутке ФО ", 1, "Восстановление натяжения в 2 промежутке ТД"),
            [86] = new MessageClass(4, "Провал натяжения в 3 промежутке ФО ", 1, "Восстановление натяжения в 3 промежутке ТД"),
            [87] = new MessageClass(4, "Провал натяжения в 4 промежутке ФО ", 1, "Восстановление натяжения в 4 промежутке ТД"),
            [88] = new MessageClass(4, "Вентилятор обдува 101Г выключен НО", 0, ""),
            [89] = new MessageClass(4, "Вентилятор обдува 102Г выключен НО", 0, ""),
            [90] = new MessageClass(4, "Вентилятор обдува 103Г выключен НО", 0, ""),
            [91] = new MessageClass(4, "Вентилятор обдува 105Г выключен НО", 0, ""),
            [92] = new MessageClass(4, "Вентилятор обдува 106Г выключен НО", 0, ""),
            [93] = new MessageClass(4, "Вентилятор подпора ПА - 1 выключен", 0, ""),
            [94] = new MessageClass(4, "Вентилятор обдува 112Г выключен НО", 0, ""),
            [95] = new MessageClass(4, "Вентилятор обдува 111Г выключен НО", 0, ""),
            [96] = new MessageClass(4, "Вентилятор обдува 110Г выключен НО", 0, ""),
            [97] = new MessageClass(4, "Вентилятор обдува 108Г выключен НО", 0, ""),
            [98] = new MessageClass(4, "Вентилятор обдува 107Г выключен НО", 0, ""),
            [99] = new MessageClass(4, "Вентилятор подпора ПА-2 выключен", 0, ""),
            [100] = new MessageClass(4, "Вентилятор обдува ГП 1 клети выключен", 0, ""),
            [101] = new MessageClass(4, "Вентилятор обдува ГП 2 клети выключен", 0, ""),
            [102] = new MessageClass(4, "Вентилятор обдува ГП 3 клети выключен", 0, ""),
            [103] = new MessageClass(4, "Вентилятор обдува ГП 4 клети выключен", 0, ""),
            [104] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ4 нажата"),
            [105] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ3 нажата"),
            [106] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ2 нажата"),
            [107] = new MessageClass(0, "", 4, "Кнопка ФО на ПУ1 нажата"),
            [108] = new MessageClass(0, "", 4, "Кнопка ФО на ПУР нажата"),
            [109] = new MessageClass(0, "", 4, "Кнопка АО на СУС нажата"),
            [110] = new MessageClass(0, "", 4, "Кнопка АО на ПУ5 нажата"),
            [111] = new MessageClass(0, "", 0, ""),
            [112] = new MessageClass(0, "", 0, ""),
            [113] = new MessageClass(0, "", 0, ""),
            [114] = new MessageClass(4, "Кнопка АО на СУС нажата", 0, ""),
            [115] = new MessageClass(0, "", 0, ""),
            [116] = new MessageClass(0, "", 0, ""),
            [117] = new MessageClass(0, "", 0, ""),
            [118] = new MessageClass(0, "", 0, ""),
            [119] = new MessageClass(0, "", 0, ""),
            [120] = new MessageClass(4, "Вентилятор обдува ГП 5 клети выключен НО", 0, ""),
            [121] = new MessageClass(0, "", 0, ""),
            [122] = new MessageClass(4, "Вентилятор подпора ГП-1 выключен", 0, ""),
            [123] = new MessageClass(4, "Вентилятор подпора ГП-2 выключен", 0, ""),
            [124] = new MessageClass(4, "Вентилятор обдува нажимных винтов выключен", 0, ""),
            [125] = new MessageClass(4, "ХХХ ПЕРЕГРУЗ ГП ХХХ", 0, ""),
            [126] = new MessageClass(0, "", 0, ""),
            [127] = new MessageClass(0, "", 0, ""),
        };

        public struct TByte_Signal
        {
            public short RegTD;
            public short RegRazg;
            public short RegNO;
            public short RegFO;
            public short Vip;
            public short T10;
            public short T20;
            public short T30;
            public short ZS;
            public short TD;
            public short RS;
            public short NO;
            public short FO;
            public short MaxSpeedPeregr;
            public short UstavkaSpeed;
            public short MaxSpeed;
            public short LK4;
            public short LK5;
            public short Got_64kg;
            public short Rnz12;
            public short Rnz23;
            public short Rnz34;
            public short GrtVkl;
            public short TrtVkl;
            public short T40;
            public short Tmot;
            public short Trazm;
            public short LKmot;
            public short LKrazm;
            public short LK1;
            public short LK2;
            public short LK3;
            public short NalPol;
            public short Knp;
            public short GotStan;
            public short MaxV1;
            public short MaxV2;
            public short MaxV3;
            public short MaxV4;
            public short MaxV5;
            public short RKDVVkl;
            public short RpvVkl;
            public short Rnv12;
            public short Rnv23;
            public short Rnv34;
            public short Rnz45;
            public short Rtv;
            public short Got_100kg;
            public short g12;
            public short g13;
            public short g14;
            public short g15;
            public short g16;
            public short NatshUsl;
            public short GotEmuls;
            public short g17;
            public short g18;
            public short g19;
            public short g20;
            public short temp_POU;
            public short davl_redukt;
            public short davl_PGT;
            public short temp_privod;
            public short got_sinhr;
            public short OgrRdn2nv;
            public short Ig140pr2nv;
            public short BpvVkl;
            public short Peregr1;
            public short Peregr2;
            public short Peregr3;
            public short Peregr4;
            public short Peregr5;
            public short PtP2nv;
            public short LkOmOn2nv;
            public short LkSimOn2nv;
            public short SimRdy2nv;
            public short BkLkShuntRt2nv;
            public short Dv2pr09sec2nv;
            public short OgrRsRt2nv;
            public short OgrRt2nv;
            public short OgrRdn2vv;
            public short Ig140pr2vv;
            public short R31;
            public short TrazmProval;
            public short T12proval;
            public short T23proval;
            public short T34proval;
            public short T45proval;
            public short PtP2vv;
            public short LkOmOn2vv;
            public short LkSimOn2vv;
            public short SimRdy2vv;
            public short Dv2pr09sec2vv;
            public short OgrRsRt2vv;
            public short OgrRt2vv;
            public short OgrRdn3nv;
            public short Ig140pr3nv;
            public short R41;
            public short R42;
            public short R43;
            public short R44;
            public short PtP3nv;
            public short LkOmOn3nv;
            public short LkSimOn3nv;
            public short SimRdy3nv;
            public short BkLkShuntRT3nv;
            public short Dv2pr09sec3nv;
            public short OgrRtRs3nv;
            public short OgrRt3nv;
            public short OgrRdn3vv;
            public short Ig140pr3vv;
            public short KnAOsus;
            public short SmGot;
            public short KteRazm;
            public short AOrele;
            public short FOknop;
            public short R56;
            public short PtP3vv;
            public short LkOmOn3vv;
            public short LkSimOn3vv;
            public short SimRdy3vv;
            public short Dv2pr09sec3vv;
            public short OgrRsRt3vv;
            public short OgrRt3vv;
            public short OgragdMot;
            public short ZaxlestOtMot;
            public short NOTempGP;
            public short NOSinxr;
            public short NOPanPultStar;
            public short NOPURazm;
            public short NOPU1;
            public short NOPU2;
            public short NOPU3;
            public short NOPU4;
            public short NOPU5;
            public short FOPanPultStar;
            public short FOPU5;
            public short AOPUR;
            public short FOPU4;
            public short FOPU3;
            public short FOPU2;
            public short FOPU1;
            public short FOPUR;
            public short AOSUSknopka;
            public short AO5klet;
            public short Vent_101G;
            public short Vent_102G;
            public short Vent_103G;
            public short Vent_105G;
            public short Vent_106G;
            public short Vent_podpor_PA1;
            public short Vent_112G;
            public short Vent_111G;
            public short Vent_110G;
            public short Vent_108G;
            public short Vent_107G;
            public short Vent_podpor_PA2;
            public short Vent_1kl;
            public short Vent_2kl;
            public short Vent_3kl;
            public short Vent_4kl;
            public short Vent_5kl;
            public short Vent_mot;
            public short  Vent_podpor_GP1;
            public short Vent_podpor_GP2;
            public short Vent_NV;
        }



        byte[] stanbuffer;          //данные c контроллера 100ms
        byte[] stanbufferPLC;       //Промежуточное хранение даных
        byte[] stanbufferSQL;       //Данные 101мс
        byte[] stanbufferMessage;   //Данные сообщений
        byte[] stanbufferMessageOld;//Данные сообщений
        byte[] stanbuffer1s;        //Технологические данные
        byte[] stanbufferNet;       //Передача по сети (визуализация)

        int stanamount = 320; //Размер буфера для принятия данных в байтах

        byte[] stanIPconnPLC = new byte[] { 192, 168, 0, 11 }; //Передаем адресс контроллера
        int stanconnect = 0;

        double standMot = 0.615;

        string stanNamePLC = "Стан1700";
        int stanSlotconnPC = 3;
        int stanRackconnPC = 0;

        int stanStartAdressTag = 3000; //старт адресов с 3000

        readonly object stanlocker1 = new object();
        readonly object stanlocker2 = new object();
        readonly object stanlocker3 = new object();
        readonly object stanlocker4 = new object();

        float stanspeed4kl, stanH_work, stanhw, stanBw, stanD_tek_mot, stanB_Work, stanD_pred_mot = 0, stanVes_Work, stanDlina_Work;

        //DataTable dtstan101ms;

        bool blstanRulonProkatSaveInData101ms;
        DateTime stanTimeStart;
        private bool blRulonStart = false;
        private string messageRulon;
        private bool blRulonStop = false;
        private string connectionString = "Data Source = 192.168.0.46; Initial Catalog = standbT; User ID = stanm; Password = 159951";
        private string numberTable;
        private float speed4kl;
        private float H5_work;
        private int B_Work;
        private float Ves_Work;
        private DateTime stanTimeStop;
        private float Dlina_Work;


        private int intCountMessageOKStPLC;
        private string strCountMessageOKStPLC;



        //private string messageError100mc;
        //private string messageOK100mc;
        //private string messageError101mc;
        //private string messageOK101mc;
        //private string messageError200mc;
        //private string messageOK200mc;
        //private string messageError1c;
        //private string messageOK1c;
        //private string messageErrorRulon;
        //private string messageOKRulon;
        //private string messageErrorProizvodstvo;
        //private string messageOKProizvodstvo;
        //private string messageErrorValki;
        //private string messageOKValki;

        //private DateTime dtError100mc;
        //private DateTime dtOK100mc;
        //private DateTime dtError101mc;
        //private DateTime dtOK101mc;
        //private DateTime dtError200mc;
        //private DateTime dtOK200mc;
        //private DateTime dtError1c;
        //private DateTime dtOK1c;
        //private DateTime dtErrorRulon;
        //private DateTime dtOKRulon;
        //private DateTime dtErrorProizvodstvo;
        //private DateTime dtOKProizvodstvo;
        //private DateTime dtErrorValki;
        //private DateTime dtOKValki;

        private int d1_pred;
        private int d2_pred;
        private int d3_pred;
        private int d4_pred;
        private int d5_pred;
        DataTable dtPerevalkiStan;
        DataTable dtMessagestan;
        DateTime dtMessage;
        int writeMessage = 0; //цикл сохранением значений Message


        private bool blOKplc = false;





        public void goStart()
        {

            

            //stan.Data101ms = stanData100ms;
            #region dtStanPerevalki - формирование dataTable Перевалки(цикл  1s стана)

            dtPerevalkiStan = new DataTable();
            dtPerevalkiStan.Columns.Add("dtPerevalki", typeof(DateTime));
            dtPerevalkiStan.Columns.Add("kl1", typeof(int));
            dtPerevalkiStan.Columns.Add("kl2", typeof(int));
            dtPerevalkiStan.Columns.Add("kl3", typeof(int));
            dtPerevalkiStan.Columns.Add("kl4", typeof(int));
            dtPerevalkiStan.Columns.Add("kl5", typeof(int));

            #endregion

            #region dtMessageStan - формирование DataTable Message(таблица сообщений стана)
            dtMessagestan = new DataTable();
            dtMessagestan.Columns.Add("dtmes", typeof(DateTime));
            dtMessagestan.Columns.Add("status", typeof(int));
            dtMessagestan.Columns.Add("message", typeof(string));
            dtMessagestan.Columns.Add("speed", typeof(float));
            #endregion


            Thread queryPLC = new Thread(stanPLC);
            queryPLC.Start();


            Thread querySQL = new Thread(stanSQL101ms);
            querySQL.Start();

            Thread queryMes = new Thread(stanMessage200ms);
            queryMes.Start();

            Thread query1s = new Thread(stanSQL1s);
            query1s.Start();




            while (true)
            {
                Thread.Sleep(5000);

                #region //Вывод на консоль отключен

                //LogSystem.Write("Стан1700", Direction.Ok, "Информация о работе методов класса clStan1700 ( Цикл 5сек )", 1, 1, true); 

                //if (messageError100mc != null)
                //{
                //    LogSystem.Write("Стан1700 ERROR цикла 100mc", Direction.ERROR, dtError100mc, messageError100mc, 1, 5, true);
                //}
                //if (messageOK100mc!=null)
                //{
                //    LogSystem.Write("Стан1700 connection (цикл 100mc)", Direction.Ok, dtOK100mc, messageOK100mc, 1, 6, true);
                //}


                //if (messageError101mc!=null)
                //{
                //    LogSystem.Write("Стан1700 ERROR цикла 101mc", Direction.ERROR, dtError101mc, messageError101mc, 1, 8, true);
                //}
                //if (messageOK101mc!=null)
                //{
                //    LogSystem.Write("Стан1700 Получение данных и запись во временную таблицу (цикл 101mc)", Direction.Ok, dtOK101mc, messageOK101mc, 1, 9, true);
                //}

                //if (messageError200mc!=null)
                //{
                //    LogSystem.Write("Стан1700 ERROR цикла 200mc", Direction.ERROR, dtError200mc, messageError200mc, 1, 11, true);
                //}
                //if (messageOK200mc!=null)
                //{
                //    LogSystem.Write("Стан1700 Сообщения (цикл 200mc)", Direction.Ok, dtOK200mc, messageOK200mc, 1, 12, true);
                //}

                //if (messageError1c != null)
                //{
                //    LogSystem.Write("Стан1700 ERROR цикла 1c", Direction.ERROR, dtError1c, messageError1c, 1, 14, true);
                //}
                //if (messageOK1c != null)
                //{
                //    LogSystem.Write("Стан1700 Получение данных с энергосистемы (Цикл 1c)", Direction.Ok, dtOK1c, messageOK1c, 1, 15, true);
                //}



                //if (messageErrorRulon != null)
                //{
                //    LogSystem.Write("Стан1700 ERROR при переименовании таблицы рулонов", Direction.ERROR, dtErrorRulon, messageErrorRulon, 1, 17, true);
                //}
                //if (messageOKRulon != null)
                //{
                //    LogSystem.Write("Стан1700 переименование таблицы рулонов", Direction.Ok, dtOKRulon, messageOKRulon, 1, 18, true);
                //}



                //if (messageErrorProizvodstvo != null)
                //{
                //    LogSystem.Write("Стан1700 ERROR записи в таблицу Производства", Direction.ERROR, dtErrorProizvodstvo, messageErrorProizvodstvo, 1, 20, true);
                //}
                //if (messageOKProizvodstvo != null)
                //{
                //    LogSystem.Write("Стан1700 запись в таблицу производства", Direction.Ok, dtOKProizvodstvo, messageOKProizvodstvo, 1, 21, true);
                //}



                //if (messageErrorValki != null)
                //{
                //    LogSystem.Write("Стан1700 ERROR записи в таблицу перевалок валков", Direction.ERROR, dtErrorValki, messageErrorValki, 1, 23, true);
                //}
                //if (messageOKValki != null)
                //{
                //    LogSystem.Write("Стан1700 запись в таблицу перевалок валков", Direction.Ok, dtOKValki, messageOKValki, 1, 24, true);
                //}




                ////if (blRulonStop)
                ////{
                ////    LogSystem.Write("Стан1700 Рулон", Direction.Ok, messageRulon, 1, 13, true);
                ////    blRulonStop = false;

                ////}
                ////else
                ////{
                ////    //  Console.WriteLine(stanTimeStart.ToString("HH:mm:ss.fff") + "  " + blRulonStart + "  + " + stanD_pred_mot);
                ////    //Console.WriteLine(numberTable);
                ////}

                ////string message1s = "Имя таблицы 1сек - " + numberTable;
                ////LogSystem.Write("Стан1700 1s", Direction.Ok, message1s, 1, 11, true);

                #endregion

                Program.CountMessageOKStPLC = strCountMessageOKStPLC+"("+ intCountMessageOKStPLC+")";

                //Program.CountMessageOKStPLC = ""; //Сброс данных после цикла 5 сек
                
                strCountMessageOKStPLC = "";
                intCountMessageOKStPLC = 0;

            }
        }

        

        

       
        
        #region Соединение и прием данных с контроллера
        private void stanPLC()
        {
            try
            {
                //int i = 0;     //начальная позиция по Top
                //int y = 2;     //Конечная позиция по Top
                

                Prodave rs2 = new Prodave();

                stanbuffer = new byte[stanamount];
                stanbufferPLC = new byte[stanamount];
                stanbufferSQL = new byte[stanamount];
                stanbuffer1s = new byte[stanamount];
                stanbufferMessage = new byte[22];
                stanbufferMessageOld = new byte[22];


                int resultReadField = 5;


                while (true)
                {
                    Thread.Sleep(100);

                    if (resultReadField != 0)
                    {
                        int res = rs2.LoadConnection(stanconnect, 2, stanIPconnPLC, stanSlotconnPC, stanRackconnPC);

                        if (res != 0)
                        {
                            //Console.WriteLine("error" + rs2.Error(res));
                            //LogSystem.Write(stanNamePLC + " start", Direction.ERROR, "Error connection!. Error - " + rs2.Error(res), 1, 1, true);
                          
                            Program.messageErrorSt100mc = rs2.Error(res);
                            Program.dtErrorSt100mc = DateTime.Now;

                        }
                        else
                        {
                            int resSAC = rs2.SetActiveConnection(stanconnect);
                        }

                    }

                    int Byte_Col_r = 0;

                    resultReadField = rs2.field_read('M', 0, stanStartAdressTag, stanamount, out stanbuffer, out Byte_Col_r);

                    if (resultReadField == 0)
                    {
                        //LogSystem.Write(stanNamePLC + " start", Direction.Ok, "Соединение активно.", 1, 2, true);
                        Program.messageOKSt100mc =  "Соединение активно";
                        intCountMessageOKStPLC = intCountMessageOKStPLC + 1;
                        strCountMessageOKStPLC = strCountMessageOKStPLC + ".";

                        Program.dtOKSt100mc = DateTime.Now;
                        blOKplc = true;

                        //if (blOKplc)
                        //{
                        //    Thread querySQL = new Thread(stanSQL101ms);
                        //    querySQL.Start();

                        //    Thread queryMes = new Thread(stanMessage200ms);
                        //    queryMes.Start();

                        //    Thread query1s = new Thread(stanSQL1s);
                        //    query1s.Start();
                        //}


                        //Буфер PLC
                        Thread PLS100ms = new Thread(BufferToBuffer);
                        PLS100ms.Start();

                        //Буфер SQL 100mc
                        Thread PLS101ms = new Thread(BufferSQLToBufferPLC);
                        PLS101ms.Start();

                        //Буфер сообщений
                        Thread PLS200ms = new Thread(BufferMessageToBufferPLC);
                        PLS200ms.Start();

                        //Буфер 1с
                        Thread PLS1000ms = new Thread(Buffer1cToBufferPLC);
                        PLS1000ms.Start();

                    }
                    else
                    {
                        rs2.UnloadConnection(stanconnect);
                        //LogSystem.Write(stanNamePLC + " 100ms", Direction.ERROR, "Error.Read fied PLC. " + rs2.Error(resultReadField), 1, 1, true);
                        Program.messageErrorSt100mc = "Ошибка чтения тегов c контроллера:"+rs2.Error(resultReadField);
                        Program.dtErrorSt100mc = DateTime.Now;
                        blOKplc = false;
                    }

                }


            }
            catch (Exception ex)
            {
                /*все исключения кидаем в пустоту*/
                LogSystem.Write(stanNamePLC + " start -" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 1, 1, true);
                Program.messageErrorSt100mc = "Общая ошибка 100mc -" + ex.Message;
                Program.dtErrorSt100mc = DateTime.Now;

            }
        }



        void BufferToBuffer()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (stanlocker1)
            {

                //Array.Clear(bufferPLC, 0, bufferPLC.Length);
                stanbufferPLC = stanbuffer;
            }

        }
        void BufferSQLToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (stanlocker2)
            {
                //Array.Clear(bufferSQL, 0, bufferSQL.Length);
                stanbufferSQL = stanbufferPLC;
            }

        }
        void Buffer1cToBufferPLC()
        {
            lock (stanlocker3)
            {
                stanbuffer1s = stanbufferPLC;


            }
        }
        private void BufferMessageToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            //У миши Богуша в delphi программе биты переставлялись с помощью оператора swap и он истользовал не byte, а int
            //У меня используется везьде byte поэтому я просто поменял биты при формировании buffer

            lock (stanlocker4)
            {
                stanbufferMessageOld[0] = stanbufferMessage[0];
                stanbufferMessageOld[1] = stanbufferMessage[1];
                stanbufferMessageOld[2] = stanbufferMessage[2];
                stanbufferMessageOld[3] = stanbufferMessage[3];
                stanbufferMessageOld[4] = stanbufferMessage[4];
                stanbufferMessageOld[5] = stanbufferMessage[5];
                stanbufferMessageOld[6] = stanbufferMessage[6];
                stanbufferMessageOld[7] = stanbufferMessage[7];
                stanbufferMessageOld[8] = stanbufferMessage[8];
                stanbufferMessageOld[9] = stanbufferMessage[9];
                stanbufferMessageOld[10] = stanbufferMessage[10];
                stanbufferMessageOld[11] = stanbufferMessage[11];
                stanbufferMessageOld[12] = stanbufferMessage[12];
                stanbufferMessageOld[13] = stanbufferMessage[13];
                stanbufferMessageOld[14] = stanbufferMessage[14];
                stanbufferMessageOld[15] = stanbufferMessage[15];
                stanbufferMessageOld[16] = stanbufferMessage[16];
                stanbufferMessageOld[17] = stanbufferMessage[17];
                stanbufferMessageOld[18] = stanbufferMessage[18];
                stanbufferMessageOld[19] = stanbufferMessage[19];
                stanbufferMessageOld[20] = stanbufferMessage[20];
                stanbufferMessageOld[21] = stanbufferMessage[21];



                stanbufferMessage[0] = stanbufferPLC[67];
                stanbufferMessage[1] = stanbufferPLC[66];
                stanbufferMessage[2] = stanbufferPLC[69];
                stanbufferMessage[3] = stanbufferPLC[68];
                stanbufferMessage[4] = stanbufferPLC[71];
                stanbufferMessage[5] = stanbufferPLC[70];
                stanbufferMessage[6] = stanbufferPLC[103];
                stanbufferMessage[7] = stanbufferPLC[102];
                stanbufferMessage[8] = stanbufferPLC[105];
                stanbufferMessage[9] = stanbufferPLC[104];
                stanbufferMessage[10] = stanbufferPLC[107];
                stanbufferMessage[11] = stanbufferPLC[106];
                stanbufferMessage[12] = stanbufferPLC[109];
                stanbufferMessage[13] = stanbufferPLC[108];
                stanbufferMessage[14] = stanbufferPLC[111];
                stanbufferMessage[15] = stanbufferPLC[110];
                stanbufferMessage[16] = stanbufferPLC[6];   //speed 4kl
                stanbufferMessage[17] = stanbufferPLC[7];
                stanbufferMessage[18] = stanbufferPLC[312];
                stanbufferMessage[19] = stanbufferPLC[313];
                stanbufferMessage[20] = stanbufferPLC[310];
                stanbufferMessage[21] = stanbufferPLC[311];

            }

        }



        #endregion


        #region Запись данных(101ms) с контроллера в Базу Данных

        private void stanSQL101ms()
        {
            string NameRulon="";
            try
            {
                while (true)
                {
                    Thread.Sleep(101);

                    //System.Diagnostics.Debug.WriteLine("101mc");

                    #region Формируем SQL запрос с циклом 101мс и записываем его во временную БД

                    #region //Если БД не существует то создаем -> TEMPstan101ms
                    string comRulon101ms1 = "if not exists (select * from sysobjects where name ='TEMPstan101ms' and xtype='U') create table TEMPstan101ms " +
                       "(" +
                       "dtsave datetime , " +
                       "v1 float," +
                       "v2 float," +
                       "v3 float," +
                       "v4 float," +
                       "v5 float," +
                       "h1 float," +
                       "h5 float," +
                       "b int," +
                       "dvip float," +
                       "drazm float," +
                       "dmot float," +
                       "vvip float," +
                       "d1 int," +
                       "d2 int," +
                       "d3 int," +
                       "d4 int," +
                       "d5 int," +
                       "e2 float," +
                       "e3 float," +
                       "e4 float," +
                       "e5 float," +
                       "n1l float," +
                       "n1p float," +
                       "n2l float," +
                       "n2p float," +
                       "n3l float," +
                       "n3p float," +
                       "n4l float," +
                       "n4p float," +
                       "n5l float," +
                       "n5p float," +
                       "reserv1 float," +
                       "reserv2 float," +
                       "t1 float," +
                       "t2 float," +
                       "t3 float," +
                       "t4 float," +
                       "t1l float," +
                       "t2l float," +
                       "t3l float," +
                       "t4l float," +
                       "t1p float," +
                       "t2p float," +
                       "t3p float," +
                       "t4p float," +
                       "t1z float," +
                       "t2z float," +
                       "t3z float," +
                       "t4z float," +
                       "erazm float," +
                       "ivozbrazm float," +
                       "izadrazm float," +
                       "w1 float," +
                       "w2v float," +
                       "w2n float," +
                       "w3v float," +
                       "w3n float," +
                       "w4v float," +
                       "w4n float," +
                       "w5v float," +
                       "w5n float," +
                       "wmot float," +
                       "imot int," +
                       "izadmot int," +
                       "u1 float," +
                       "u2v float," +
                       "u2n float," +
                       "u3v float," +
                       "u3n float," +
                       "u4v float," +
                       "u4n float," +
                       "u5v float," +
                       "u5n float," +
                       "umot float," +
                       "i1 int," +
                       "i2v int," +
                       "i2n int," +
                       "i3v int," +
                       "i3n int," +
                       "i4v int," +
                       "i4n int," +
                       "i5v int," +
                       "i5n int," +
                       "rtv float," +
                       "dt1 float," +
                       "dt2 float," +
                       "dt3 float," +
                       "dt4 float," +
                       "grt float," +
                       "trt float," +
                       "mv1 float," +
                       "mv2 float," +
                       "mv3 float," +
                       "dh1 float," +
                       "dh5 float," +
                       "os1klvb int," +
                       "rezerv int," +
                       "mezdoza4 int" +
                       ")";

                    using (SqlConnection conSQL101ms1 = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conSQL101ms1.Open();
                            SqlCommand command = new SqlCommand(comRulon101ms1, conSQL101ms1);
                            command.ExecuteNonQuery();
                            conSQL101ms1.Close();
                        }
                        catch (Exception)
                        {

                            //Program.messageErrorSt101mc = "101mc НЕ ЗАПИСАНЫ. " + ex.Message + " Insert запрос: " + comRulon101ms1;
                            Program.messageErrorSt101mc = "101mc НЕ ЗАПИСАНЫ. Создание таблицы TEMPstan101ms."; 
                            Program.dtErrorSt101mc = DateTime.Now;
                        }


                    }
                    #endregion

                    if (blRulonStart)
                    //if (true)
                    {
                        
                        string comRulon101ms2 = "INSERT INTO TEMPstan101ms" +
                       "(dtsave,v1,v2,v3,v4,v5,h1,h5,b,dvip,drazm,dmot,vvip,d1,d2,d3,d4,d5,e2,e3,e4,e5,n1l,n1p,n2l,n2p,n3l,n3p,n4l,n4p,n5l,n5p,reserv1,reserv2,t1,t2,t3,t4,t1l,t2l,t3l,t4l,t1p,t2p,t3p,t4p,t1z,t2z,t3z,t4z,erazm,ivozbrazm,izadrazm,w1,w2v,w2n,w3v,w3n,w4v,w4n,w5v,w5n,wmot,imot,izadmot,u1,u2v,u2n,u3v,u3n,u4v,u4n,u5v,u5n,umot,i1,i2v,i2n,i3v,i3n,i4v,i4n,i5v,i5n,rtv,dt1,dt2,dt3,dt4,grt,trt,mv1,mv2,mv3,dh1,dh5,os1klvb,rezerv,mezdoza4)" +
                       " VALUES " +
                       "('" +
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'," +
                        (float)(BitConverter.ToInt16(stanbufferSQL, 0)) / 100 + "," +    //v1
                        (float)(BitConverter.ToInt16(stanbufferSQL, 2)) / 100 + "," +    //v2
                        (float)(BitConverter.ToInt16(stanbufferSQL, 4)) / 100 + "," +    //v3
                        (float)(BitConverter.ToInt16(stanbufferSQL, 6)) / 100 + "," +    //v4
                        (float)(BitConverter.ToInt16(stanbufferSQL, 8)) / 100 + "," +    //v5
                        (float)(BitConverter.ToInt16(stanbufferSQL, 10)) / 1000 + "," +  //h1    
                        (float)(BitConverter.ToInt16(stanbufferSQL, 12)) / 1000 + "," +  //h5
                        BitConverter.ToInt16(stanbufferSQL, 14) + "," +                  //b
                        (float)(BitConverter.ToInt16(stanbufferSQL, 16)) / 1000 + "," +  //dvip
                        (float)(BitConverter.ToInt16(stanbufferSQL, 18)) / 1000 + "," +  //drazm
                        (float)(BitConverter.ToInt16(stanbufferSQL, 20)) / 1000 + "," +  //dmot
                        (float)(BitConverter.ToInt16(stanbufferSQL, 22)) / 1000 + "," +  //vvip
                        BitConverter.ToInt16(stanbufferSQL, 24) + "," +                  //d1
                        BitConverter.ToInt16(stanbufferSQL, 26) + "," +                  //d2
                        BitConverter.ToInt16(stanbufferSQL, 28) + "," +                  //d3
                        BitConverter.ToInt16(stanbufferSQL, 30) + "," +                  //d4
                        BitConverter.ToInt16(stanbufferSQL, 32) + "," +                  //d5
                        (float)(BitConverter.ToInt16(stanbufferSQL, 34)) / 100 + "," +   //e2
                        (float)(BitConverter.ToInt16(stanbufferSQL, 36)) / 100 + "," +   //e3
                        (float)(BitConverter.ToInt16(stanbufferSQL, 38)) / 100 + "," +   //e4
                        (float)(BitConverter.ToInt16(stanbufferSQL, 40)) / 100 + "," +   //e5
                        (float)(BitConverter.ToInt16(stanbufferSQL, 42)) / 100 + "," +   //n1l
                        (float)(BitConverter.ToInt16(stanbufferSQL, 44)) / 100 + "," +   //n1p
                        (float)(BitConverter.ToInt16(stanbufferSQL, 46)) / 100 + "," +   //n2l
                        (float)(BitConverter.ToInt16(stanbufferSQL, 48)) / 100 + "," +   //n2p
                        (float)(BitConverter.ToInt16(stanbufferSQL, 50)) / 100 + "," +   //n3l
                        (float)(BitConverter.ToInt16(stanbufferSQL, 52)) / 100 + "," +   //n3p
                        (float)(BitConverter.ToInt16(stanbufferSQL, 54)) / 100 + "," +   //n4l
                        (float)(BitConverter.ToInt16(stanbufferSQL, 56)) / 100 + "," +   //n4p
                        (float)(BitConverter.ToInt16(stanbufferSQL, 58)) / 100 + "," +   //n5l
                        (float)(BitConverter.ToInt16(stanbufferSQL, 60)) / 100 + "," +   //n5p
                        (float)(BitConverter.ToInt16(stanbufferSQL, 68)) / 100 + "," +   //reserv1
                        (float)(BitConverter.ToInt16(stanbufferSQL, 70)) / 100 + "," +   //reserv2
                        (float)(BitConverter.ToInt16(stanbufferSQL, 72)) / 100 + "," +   //t1
                        (float)(BitConverter.ToInt16(stanbufferSQL, 74)) / 100 + "," +   //t2
                        (float)(BitConverter.ToInt16(stanbufferSQL, 76)) / 100 + "," +   //t3
                        (float)(BitConverter.ToInt16(stanbufferSQL, 78)) / 100 + "," +   //t4
                        (float)(BitConverter.ToInt16(stanbufferSQL, 80)) / 100 + "," +   //t1l
                        (float)(BitConverter.ToInt16(stanbufferSQL, 82)) / 100 + "," +   //t2l
                        (float)(BitConverter.ToInt16(stanbufferSQL, 84)) / 100 + "," +   //t3l
                        (float)(BitConverter.ToInt16(stanbufferSQL, 86)) / 100 + "," +   //t4l
                        (float)(BitConverter.ToInt16(stanbufferSQL, 88)) / 100 + "," +   //t1p
                        (float)(BitConverter.ToInt16(stanbufferSQL, 90)) / 100 + "," +   //t2p
                        (float)(BitConverter.ToInt16(stanbufferSQL, 92)) / 100 + "," +   //t3p
                        (float)(BitConverter.ToInt16(stanbufferSQL, 94)) / 100 + "," +   //t4p
                        (float)(BitConverter.ToInt16(stanbufferSQL, 96)) / 100 + "," +   //t1z
                        (float)(BitConverter.ToInt16(stanbufferSQL, 98)) / 100 + "," +   //t2z
                        (float)(BitConverter.ToInt16(stanbufferSQL, 100)) / 100 + "," +  //t3z
                        (float)(BitConverter.ToInt16(stanbufferSQL, 112)) / 100 + "," +  //t4z
                        (float)(BitConverter.ToInt16(stanbufferSQL, 114)) / 10 + "," +   //erazm
                        (float)(BitConverter.ToInt16(stanbufferSQL, 116)) / 100 + "," +  //ivozbrazm
                        (float)(BitConverter.ToInt16(stanbufferSQL, 118)) / 10 + "," +   //izadrazm 
                        (float)(BitConverter.ToInt16(stanbufferSQL, 120)) / 10 + "," +   //w1
                        (float)(BitConverter.ToInt16(stanbufferSQL, 122)) / 10 + "," +   //w2v
                        (float)(BitConverter.ToInt16(stanbufferSQL, 124)) / 10 + "," +   //w2n
                        (float)(BitConverter.ToInt16(stanbufferSQL, 126)) / 10 + "," +   //w3v
                        (float)(BitConverter.ToInt16(stanbufferSQL, 128)) / 10 + "," +   //w3n
                        (float)(BitConverter.ToInt16(stanbufferSQL, 130)) / 10 + "," +   //w4v
                        (float)(BitConverter.ToInt16(stanbufferSQL, 132)) / 10 + "," +   //w4n
                        (float)(BitConverter.ToInt16(stanbufferSQL, 134)) / 10 + "," +   //w5v
                        (float)(BitConverter.ToInt16(stanbufferSQL, 136)) / 10 + "," +   //w5n
                        (float)(BitConverter.ToInt16(stanbufferSQL, 138)) / 10 + "," +   //wmot
                        BitConverter.ToInt16(stanbufferSQL, 140) + "," +                 //imot
                        BitConverter.ToInt16(stanbufferSQL, 142) + "," +                 //izadmot
                        (float)(BitConverter.ToInt16(stanbufferSQL, 144)) / 10 + "," +   //u1
                        (float)(BitConverter.ToInt16(stanbufferSQL, 146)) / 10 + "," +   //u2v
                        (float)(BitConverter.ToInt16(stanbufferSQL, 148)) / 10 + "," +   //u2n
                        (float)(BitConverter.ToInt16(stanbufferSQL, 150)) / 10 + "," +   //u3v
                        (float)(BitConverter.ToInt16(stanbufferSQL, 152)) / 10 + "," +   //u3n
                        (float)(BitConverter.ToInt16(stanbufferSQL, 154)) / 10 + "," +   //u4v
                        (float)(BitConverter.ToInt16(stanbufferSQL, 156)) / 10 + "," +   //u4n
                        (float)(BitConverter.ToInt16(stanbufferSQL, 158)) / 10 + "," +   //u5v
                        (float)(BitConverter.ToInt16(stanbufferSQL, 160)) / 10 + "," +   //u5n
                        (float)(BitConverter.ToInt16(stanbufferSQL, 162)) / 10 + "," +   //umot
                        BitConverter.ToInt16(stanbufferSQL, 164) + "," +                 //i1
                        BitConverter.ToInt16(stanbufferSQL, 166) + "," +                 //i2v
                        BitConverter.ToInt16(stanbufferSQL, 168) + "," +                 //i2n
                        BitConverter.ToInt16(stanbufferSQL, 170) + "," +                 //i3v
                        BitConverter.ToInt16(stanbufferSQL, 172) + "," +                 //i3n
                        BitConverter.ToInt16(stanbufferSQL, 174) + "," +                 //i4v
                        BitConverter.ToInt16(stanbufferSQL, 176) + "," +                 //i4n
                        BitConverter.ToInt16(stanbufferSQL, 178) + "," +                 //i5v
                        BitConverter.ToInt16(stanbufferSQL, 180) + "," +                 //i5n
                        (float)(BitConverter.ToInt16(stanbufferSQL, 192)) / 10 + "," +   //rtv
                        (float)(BitConverter.ToInt16(stanbufferSQL, 194)) / 10 + "," +   //dt1
                        (float)(BitConverter.ToInt16(stanbufferSQL, 196)) / 10 + "," +   //dt2
                        (float)(BitConverter.ToInt16(stanbufferSQL, 198)) / 10 + "," +   //dt3
                        (float)(BitConverter.ToInt16(stanbufferSQL, 200)) / 10 + "," +   //dt4
                        (float)(BitConverter.ToInt16(stanbufferSQL, 202)) / 10 + "," +   //grt
                        (float)(BitConverter.ToInt16(stanbufferSQL, 204)) / 10 + "," +   //trt
                        (float)(BitConverter.ToInt16(stanbufferSQL, 206)) / 10 + "," +   //mv1
                        (float)(BitConverter.ToInt16(stanbufferSQL, 208)) / 10 + "," +   //mv2
                        (float)(BitConverter.ToInt16(stanbufferSQL, 210)) / 10 + "," +   //mv3
                        (float)(BitConverter.ToInt16(stanbufferSQL, 62)) / 10 + "," +    //dh1
                        (float)(BitConverter.ToInt16(stanbufferSQL, 64)) / 10 + "," +    //dh5
                        BitConverter.ToInt16(stanbufferSQL, 216) + "," +                 //os1klvb
                        BitConverter.ToInt16(stanbufferSQL, 218) + "," +                 //rezerv
                        BitConverter.ToInt16(stanbufferSQL, 220) +                       //mezdoza4
                        ")";



                        if (true) //TODO Если установлен bit что прокатка рулона(1s) то тогда пишем во временную таблицу
                        {
                            using (SqlConnection conSQL101ms2 = new SqlConnection(connectionString))
                            {
                                try
                                {
                                    conSQL101ms2.Open();
                                    SqlCommand command = new SqlCommand(comRulon101ms2, conSQL101ms2);
                                    command.ExecuteNonQuery();
                                    Program.intConMessageOKSt101mc = Program.intConMessageOKSt101mc + 1;
                                    Program.messageOKSt101mc = Program.messageOKSt101mc+".";
                                    Program.dtOKSt101mc = DateTime.Now;
                                    conSQL101ms2.Close();
                                }
                                catch (Exception)
                                {
                                    //Program.messageErrorSt101mc = "101mc НЕ ЗАПИСАНЫ. " + ex.Message + " Insert запрос: " + comRulon101ms2;
                                    Program.messageErrorSt101mc = "101mc НЕ ЗАПИСАНЫ. Вставка данных в таблицу TEMPstan101ms.";
                                    Program.dtErrorSt101mc = DateTime.Now;
                                }
                            }   
                        }


                    }
                    #endregion

                    #region Расчет параметров прокатанного рулона после окончания прокатки

                    stanD_tek_mot = (float)(BitConverter.ToInt16(stanbufferSQL, 20)); //dmot

                    #region  Время начало прокатки рулона ver 2

                    if (stanD_pred_mot == 0)
                    {
                        //при первом цикле все данные равны 0, поэтому мы выставляем значения параметров на 600
                        stanD_tek_mot = 600;
                        stanD_pred_mot = 600;
                    }

                    if (stanD_tek_mot > stanD_pred_mot)
                    { 

                        if (stanD_pred_mot == 600) //
                        {
                            blRulonStart = false; //бит записи в таблицу Temp - не записываем
                        }
                        else if (stanD_pred_mot < 615) //запись в таблицу TEMP только с начала рулона
                        {
                            stanTimeStart = DateTime.Now;
                            blRulonStart = true; //бит записи в таблицу Temp -записываем
                        }

                    }

                    //if (stanD_pred_mot==600) //
                    //{
                    //    blRulonStart = false; //бит записи в таблицу Temp - не записываем
                    //}

                    #endregion

                    #region Толщина и ширина прокатываемого рулона
                    speed4kl = (float)(BitConverter.ToInt16(stanbufferSQL, 6)) / 100;

                    if ((stanTimeStart != new DateTime()) && (H5_work == 0) && (stanD_tek_mot > 700) && (speed4kl > 2))
                    {
                        H5_work = (float)(BitConverter.ToInt16(stanbufferSQL, 12)) / 1000;
                        B_Work = (int)BitConverter.ToInt16(stanbufferSQL, 14);
                    }
                    #endregion

                    #region Формирование сигнала окончания прокатки
                    if ((stanTimeStart != new DateTime()) && (H5_work != 0) && (stanD_tek_mot < 610) && (stanD_tek_mot < stanD_pred_mot))
                    {
                        Ves_Work = (((((stanD_pred_mot * stanD_pred_mot) / 1000000 - 0.36F) * 3.141593F) / 4) * ((float)B_Work / 1000)) * 7.85F;
                        stanTimeStop = DateTime.Now;
                        Dlina_Work = ((Ves_Work / 7.85F) / ((float)B_Work / 1000)) / (H5_work / 1000);
                        blRulonStart = false;


                            //Формируем имя рулона
                    

                        #region Переименовываем временную базу в базу с именем stan100mc(дата+время начала)(время окончания)
                        using (SqlConnection conSQL1s3 = new SqlConnection(connectionString))
                        {
                            try
                            {
                                conSQL1s3.Open();
                                string begin = stanTimeStart.ToString("yyyyMMddHHmm");
                                string end = stanTimeStop.ToString("HHmm");
                                NameRulon= "stan100ms" + begin + end;
                                string comRulon1s2 = "sp_rename 'TEMPstan101ms', '"+ NameRulon+"'";
                                SqlCommand command = new SqlCommand(comRulon1s2, conSQL1s3);
                                command.ExecuteNonQuery();
                                Program.messageOKStRulon = "Временная база -> " + NameRulon;
                                Program.dtOKStRulon = DateTime.Now;
                                conSQL1s3.Close();
                            }
                            catch (Exception ex)
                            {

                                Program.messageErrorStRulon = "Временная база не переименована " + ex.Message;
                                Program.dtErrorStRulon = DateTime.Now;
                            }
                        }



                        #endregion

                        #region Сохраняем данные прокатки в таблицу производство

                        //messageRulon = NameRulon +"-"+ stanTimeStart.ToString("HH:mm:ss.fff") + " - " + stanTimeStop.ToString("HH:mm:ss.fff") + "   " + B_Work + "*" + H5_work + "=" + Ves_Work;

                        #region База производство

                        #region Создание БД stanwork

                        string comWorkStanCreate = "if not exists (select * from sysobjects where name ='stanwork' and xtype='U') create table stanwork" +
                       "(" +
                       "dtsave datetime, " +
                       "RulonName varchar(100), " +
                       "startRulon datetime , " +
                       "stopRulon datetime , " +
                       "h5 float , " +
                       "b float , " +
                       "ves float , " +
                       "dlina float " +
                       ")";

                        using (SqlConnection conSQL1sWork1 = new SqlConnection(connectionString))
                        {
                            conSQL1sWork1.Open();
                            SqlCommand command = new SqlCommand(comWorkStanCreate, conSQL1sWork1);
                            command.ExecuteNonQuery();
                            conSQL1sWork1.Close();
                        }
                        #endregion;

                        #region Заполнение производство
                        string comWorkStan = "INSERT INTO stanwork( " +
                            "dtsave," +
                            "RulonName," +
                            "startRulon," +
                            "stopRulon," +
                            "h5," +
                            "b," +
                            "ves," +
                            "dlina" +
                            ") " +
                            "VALUES(" +
                            "@TimeNow, " +
                            "@NumberRulon, " +
                            "@TimeStart, " +
                            "@TimeStop, " +
                            "@H5_work, " +
                            "@B_Work, " +
                            "@Ves_Work, " +
                            "@Dlina_Work)";

                       
                        //Добавляем в таблицу прокатанных рулонов данные по рулонам
                        using (SqlConnection con3 = new SqlConnection(connectionString))
                        {
                            try
                            {
                                con3.Open();
                                SqlCommand command = new SqlCommand(comWorkStan, con3);

                                command.Parameters.AddWithValue("@TimeNow", DateTime.Now);
                                command.Parameters.AddWithValue("@NumberRulon", NameRulon);
                                command.Parameters.AddWithValue("@TimeStart", stanTimeStart);
                                command.Parameters.AddWithValue("@TimeStop", stanTimeStop);
                                command.Parameters.AddWithValue("@H5_work", H5_work);
                                command.Parameters.AddWithValue("@B_Work", B_Work);
                                command.Parameters.AddWithValue("@Ves_Work", Ves_Work);
                                command.Parameters.AddWithValue("@Dlina_Work", Dlina_Work);

                                int WriteSQL = command.ExecuteNonQuery();

                                Program.messageOKStProizvodstvo = NameRulon + "(" + stanTimeStart.ToString("HH:mm") + "-" + stanTimeStop.ToString("HH:mm") + ")->" + H5_work + "мм/" + B_Work + "мм/" + Ves_Work + "т/" + Dlina_Work + "m";
                                //messageOKProizvodstvo = "производство";
                                Program.dtOKStProizvodstvo = DateTime.Now;

                                #region Сброс переменных  после записи
                                NameRulon = "";
                                stanTimeStart = new DateTime();
                                stanTimeStop = new DateTime();
                                H5_work = 0;
                                B_Work = 0;
                                Ves_Work = 0;
                                Dlina_Work = 0;
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                Program.messageErrorStProizvodstvo = "Ошибка в сохранении данных о прокатанном рулоне " + ex.Message + " Insert запрос: " + comWorkStan;
                                Program.dtErrorStProizvodstvo = DateTime.Now;

                            }

                        }
                        #endregion

                        // #endregion



                        // #region //Очищаем базу временных рулонов
                        // //using (SqlConnection conSQL1s3 = new SqlConnection(connectionString))
                        // //{
                        // //    conSQL1s3.Open();
                        // //    string comRulon1s2 = "DELETE FROM TEMPstan101ms";
                        // //    SqlCommand command = new SqlCommand(comRulon1s2, conSQL1s3);
                        // //    command.ExecuteNonQuery();


                        // //}
                        // #endregion







                        // }
                        #endregion

                        #region //Создаем TEMPstan101ms, если её нет

                        // #region Если БД не существует то создаем
                        // string comRulon101ms1 = "if not exists (select * from sysobjects where name ='TEMPstan101ms' and xtype='U') create table TEMPstan101ms " +
                        //    "(" +
                        //    "datetime101ms datetime , " +
                        //    "v1 float," +
                        //    "v2 float," +
                        //    "v3 float," +
                        //    "v4 float," +
                        //    "v5 float," +
                        //    "h1 float," +
                        //    "h5 float," +
                        //    "b int," +
                        //    "dvip float," +
                        //    "drazm float," +
                        //    "dmot float," +
                        //    "vvip float," +
                        //    "d1 int," +
                        //    "d2 int," +
                        //    "d3 int," +
                        //    "d4 int," +
                        //    "d5 int," +
                        //    "e2 float," +
                        //    "e3 float," +
                        //    "e4 float," +
                        //    "e5 float," +
                        //    "n1l float," +
                        //    "n1p float," +
                        //    "n2l float," +
                        //    "n2p float," +
                        //    "n3l float," +
                        //    "n3p float," +
                        //    "n4l float," +
                        //    "n4p float," +
                        //    "n5l float," +
                        //    "n5p float," +
                        //    "reserv1 float," +
                        //    "reserv2 float," +
                        //    "t1 float," +
                        //    "t2 float," +
                        //    "t3 float," +
                        //    "t4 float," +
                        //    "t1l float," +
                        //    "t2l float," +
                        //    "t3l float," +
                        //    "t4l float," +
                        //    "t1p float," +
                        //    "t2p float," +
                        //    "t3p float," +
                        //    "t4p float," +
                        //    "t1z float," +
                        //    "t2z float," +
                        //    "t3z float," +
                        //    "t4z float," +
                        //    "erazm float," +
                        //    "ivozbrazm float," +
                        //    "izadrazm float," +
                        //    "w1 float," +
                        //    "w2v float," +
                        //    "w2n float," +
                        //    "w3v float," +
                        //    "w3n float," +
                        //    "w4v float," +
                        //    "w4n float," +
                        //    "w5v float," +
                        //    "w5n float," +
                        //    "wmot float," +
                        //    "imot int," +
                        //    "izadmot int," +
                        //    "u1 float," +
                        //    "u2v float," +
                        //    "u2n float," +
                        //    "u3v float," +
                        //    "u3n float," +
                        //    "u4v float," +
                        //    "u4n float," +
                        //    "u5v float," +
                        //    "u5n float," +
                        //    "umot float," +
                        //    "i1 int," +
                        //    "i2v int," +
                        //    "i2n int," +
                        //    "i3v int," +
                        //    "i3n int," +
                        //    "i4v int," +
                        //    "i4n int," +
                        //    "i5v int," +
                        //    "i5n int," +
                        //    "rtv float," +
                        //    "dt1 float," +
                        //    "dt2 float," +
                        //    "dt3 float," +
                        //    "dt4 float," +
                        //    "grt float," +
                        //    "trt float," +
                        //    "mv1 float," +
                        //    "mv2 float," +
                        //    "mv3 float," +
                        //    "dh1 float," +
                        //    "dh5 float," +
                        //    "os1klvb int," +
                        //    "rezerv int," +
                        //    "mezdoza4 int" +
                        //    ")";

                        // using (SqlConnection conSQL101ms1 = new SqlConnection(connectionString))
                        // {
                        //     conSQL101ms1.Open();
                        //     SqlCommand command = new SqlCommand(comRulon101ms1, conSQL101ms1);
                        //     command.ExecuteNonQuery();
                        //     conSQL101ms1.Close();

                        // }

                        #endregion


                    }

                    #endregion


                    #endregion

                    stanD_pred_mot = stanD_tek_mot;
                    #endregion
                }
            }
            catch (Exception ex)
            {

                //LogSystem.Write(stanNamePLC + " SQL(101ms)-" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 0, 3, true);
                Program.messageErrorSt101mc = "Ошибка 101мс-" + ex.Message;
                Program.dtErrorSt101mc = DateTime.Now;
            }


        }
        #endregion

        #region Формируем и записываем данные  1c. Название таблицы формируется по принципу YYYYmmddW (W - смена(1 ночная(с 19-07), 2дневная(07-19)))
        private void stanSQL1s()
        {
            byte[] stanbuf1s = new byte[100];
            string NumberTable1s="yyyyMMdd";


            //#region Формируем шифр таблицы numberTable = stan1syyyyMMddсмена

            //numberTable = "";

            //if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 7 && Convert.ToInt32(DateTime.Now.ToString("HH")) < 19)
            //{
            //    numberTable = "Stan1s" + DateTime.Now.ToString("yyyyMMdd") + "2";
            //}
            //else if (Convert.ToInt32(DateTime.Now.ToString("HH")) < 7)
            //{
            //    numberTable = "Stan1s" + DateTime.Now.ToString("yyyyMMdd") + "1";
            //}
            //else if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 19)
            //{
            //    numberTable = "Stan1s" + DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "1";
            //}

            //#endregion

            try
            {
                while (true)
                {
                    Thread.Sleep(1000);


                    #region Формируем шифр таблицы numberTable = stan1syyyyMMddсмена

                    numberTable = "";

                    TimeSpan NowTime = DateTime.Now.TimeOfDay;
                    TimeSpan Time1 = new TimeSpan(07, 00, 00);
                    TimeSpan Time2 = new TimeSpan(19, 00, 00);



                    //if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 7 && Convert.ToInt32(DateTime.Now.ToString("HH")) < 19)
                    //{
                    //    numberTable = DateTime.Now.ToString("yyyyMMdd") + "2";
                    //}
                    //else if ((Convert.ToInt32(DateTime.Now.ToString("HH")) < 7) && (Convert.ToInt32(DateTime.Now.ToString("mm")) == 00))
                    //{
                    //    numberTable = DateTime.Now.ToString("yyyyMMdd") + "1";
                    //}
                    //else if ((Convert.ToInt32(DateTime.Now.ToString("HH")) >= 19) && (Convert.ToInt32(DateTime.Now.ToString("mm")) == 00))
                    //{
                    //    numberTable = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "1";
                    //}

                    if ((NowTime>Time1)&&(NowTime<Time2))
                    {
                        //2 смена

                        NumberTable1s = DateTime.Now.ToString("yyyyMMdd") + "2";
                    }
                    else if ((NowTime < Time1)||(NowTime > Time2))
                    {
                        //1 смена
                        if (NowTime > Time2)
                        {
                            //1 смена после 19
                            NumberTable1s = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "1";
                        }
                        else
                        {
                            //1 смена до 7
                            NumberTable1s = DateTime.Now.ToString("yyyyMMdd") + "1";
                        }
                    }
                    


                    #endregion


                    stanbuf1s[0] = stanbuffer1s[224];           //191HL
                    stanbuf1s[1] = stanbuffer1s[225];           //192HL
                    stanbuf1s[2] = stanbuffer1s[226];           //193BL
                    stanbuf1s[3] = stanbuffer1s[227];           //194BL"
                    stanbuf1s[4] = stanbuffer1s[228];           //191HR"
                    stanbuf1s[5] = stanbuffer1s[229];           //192HR"
                    stanbuf1s[6] = stanbuffer1s[230];           //193BR"
                    stanbuf1s[7] = stanbuffer1s[231];           //194BR"
                    stanbuf1s[8] = stanbuffer1s[232];           //281NL"
                    stanbuf1s[9] = stanbuffer1s[233];           //282NL"
                    stanbuf1s[10] = stanbuffer1s[234];          //283BL"
                    stanbuf1s[11] = stanbuffer1s[235];          //284BL"
                    stanbuf1s[12] = stanbuffer1s[236];          //281NR"
                    stanbuf1s[13] = stanbuffer1s[237];          //282NR"
                    stanbuf1s[14] = stanbuffer1s[238];          //283BR"
                    stanbuf1s[15] = stanbuffer1s[239];          //284BR"
                    stanbuf1s[16] = stanbuffer1s[240];          //301BL"
                    stanbuf1s[17] = stanbuffer1s[241];          //302BL"
                    stanbuf1s[18] = stanbuffer1s[242];          //303HL"
                    stanbuf1s[19] = stanbuffer1s[243];          //304HL"
                    stanbuf1s[20] = stanbuffer1s[244];          //301BR"
                    stanbuf1s[21] = stanbuffer1s[245];          //302BR"
                    stanbuf1s[22] = stanbuffer1s[246];          //303HR"
                    stanbuf1s[23] = stanbuffer1s[247];          //304HR"
                    stanbuf1s[24] = stanbuffer1s[248];          //321BL"
                    stanbuf1s[25] = stanbuffer1s[249];          //322BL"
                    stanbuf1s[26] = stanbuffer1s[250];          //323HL"
                    stanbuf1s[27] = stanbuffer1s[251];          //324HL"
                    stanbuf1s[28] = stanbuffer1s[252];          //321BR"
                    stanbuf1s[29] = stanbuffer1s[253];          //322BR"
                    stanbuf1s[30] = stanbuffer1s[254];          //323HR"
                    stanbuf1s[31] = stanbuffer1s[255];          //324HR"
                    stanbuf1s[32] = stanbuffer1s[256];          //341BL"
                    stanbuf1s[33] = stanbuffer1s[257];          //342BL"
                    stanbuf1s[34] = stanbuffer1s[258];          //343HL"
                    stanbuf1s[35] = stanbuffer1s[259];          //344HL"
                    stanbuf1s[36] = stanbuffer1s[260];          //341BR"
                    stanbuf1s[37] = stanbuffer1s[261];          //342BR"
                    stanbuf1s[38] = stanbuffer1s[262];          //343HR"
                    stanbuf1s[39] = stanbuffer1s[263];          //344HR"
                    stanbuf1s[40] = stanbuffer1s[264];          //461L",
                    stanbuf1s[41] = stanbuffer1s[265];          //462L",
                    stanbuf1s[42] = stanbuffer1s[266];          //463L",
                    stanbuf1s[43] = stanbuffer1s[267];          //461R",
                    stanbuf1s[44] = stanbuffer1s[268];          //462R",
                    stanbuf1s[45] = stanbuffer1s[269];          //463R",
                    stanbuf1s[46] = stanbuffer1s[270];          //G11L",
                    stanbuf1s[47] = stanbuffer1s[271];          //G12L",
                    stanbuf1s[48] = stanbuffer1s[272];          //G13L",
                    stanbuf1s[49] = stanbuffer1s[273];          //G14L",
                    stanbuf1s[50] = stanbuffer1s[274];          //G15L",
                    stanbuf1s[51] = stanbuffer1s[275];          //G16L",
                    stanbuf1s[52] = stanbuffer1s[276];          //G17L",
                    stanbuf1s[53] = stanbuffer1s[277];          //G11R",
                    stanbuf1s[54] = stanbuffer1s[278];          //G12R",
                    stanbuf1s[55] = stanbuffer1s[279];          //G13R",
                    stanbuf1s[56] = stanbuffer1s[280];          //G14R",
                    stanbuf1s[57] = stanbuffer1s[281];          //G15R",
                    stanbuf1s[58] = stanbuffer1s[282];          //G16R",
                    stanbuf1s[59] = stanbuffer1s[283];          //G17R",
                    stanbuf1s[60] = stanbuffer1s[284];          //G21L",
                    stanbuf1s[61] = stanbuffer1s[285];          //G22L",
                    stanbuf1s[62] = stanbuffer1s[286];          //G23L",
                    stanbuf1s[63] = stanbuffer1s[287];          //G24L",
                    stanbuf1s[64] = stanbuffer1s[288];          //G25L",
                    stanbuf1s[65] = stanbuffer1s[289];          //G26L",
                    stanbuf1s[66] = stanbuffer1s[290];          //G27L",
                    stanbuf1s[67] = stanbuffer1s[291];          //G21R",
                    stanbuf1s[68] = stanbuffer1s[292];          //G22R",
                    stanbuf1s[69] = stanbuffer1s[293];          //G23R",
                    stanbuf1s[70] = stanbuffer1s[294];          //G24R",
                    stanbuf1s[71] = stanbuffer1s[295];          //G25R",
                    stanbuf1s[72] = stanbuffer1s[296];          //G26R",
                    stanbuf1s[73] = stanbuffer1s[297];          //G27R",
                    stanbuf1s[74] = stanbuffer1s[298];          //D12", 
                    stanbuf1s[75] = stanbuffer1s[299];          //D13", 
                    stanbuf1s[76] = stanbuffer1s[300];          //D14", 
                    stanbuf1s[77] = stanbuffer1s[301];          //D15", 
                    stanbuf1s[78] = stanbuffer1s[302];          //D16", 
                    stanbuf1s[79] = stanbuffer1s[303];          //D17", 
                    stanbuf1s[80] = stanbuffer1s[304];          //D18", 
                    stanbuf1s[81] = stanbuffer1s[305];          //D19", 
                    stanbuf1s[82] = stanbuffer1s[306];          //D20", 
                    stanbuf1s[83] = stanbuffer1s[307];          //U64", 
                    stanbuf1s[84] = stanbuffer1s[309];          //RasxCD
                    stanbuf1s[85] = stanbuffer1s[6];             //speeed 
                    stanbuf1s[86] = stanbuffer1s[7];             //speeed 


                    //speed4kl = (float)(BitConverter.ToInt16(stanbuffer1s, 6)) / 100;




                    #region Запись данных 1s

                    #region создание Таблицы stan1s

                   
                    //string comBD = "if not exists (select * from sysobjects where name ='" + "stan1s" + numberTable + "' and xtype='U') create table " + "stan1s" + numberTable +
                    //   "(" +
                    //   "dtsave datetime , " +
                    //   "HL191 int , " +
                    //   "HL192 int , " +
                    //   "BL193 int , " +
                    //   "BL194 int , " +
                    //   "HR191 int , " +
                    //   "HR192 int , " +
                    //   "BR193 int , " +
                    //   "BR194 int , " +
                    //   "NL281 int , " +
                    //   "NL282 int , " +
                    //   "BL283 int , " +
                    //   "BL284 int , " +
                    //   "NR281 int , " +
                    //   "NR282 int , " +
                    //   "BR283 int , " +
                    //   "BR284 int , " +
                    //   "BL301 int , " +
                    //   "BL302 int , " +
                    //   "HL303 int , " +
                    //   "HL304 int , " +
                    //   "BR301 int , " +
                    //   "BR302 int , " +
                    //   "HR303 int , " +
                    //   "HR304 int , " +
                    //   "BL321 int , " +
                    //   "BL322 int , " +
                    //   "HL323 int , " +
                    //   "HL324 int , " +
                    //   "BR321 int , " +
                    //   "BR322 int , " +
                    //   "HR323 int , " +
                    //   "HR324 int , " +
                    //   "BL341 int , " +
                    //   "BL342 int , " +
                    //   "HL343 int , " +
                    //   "HL344 int , " +
                    //   "BR341 int , " +
                    //   "BR342 int , " +
                    //   "HR343 int , " +
                    //   "HR344 int , " +
                    //   "L461 int , " +
                    //   "L462 int , " +
                    //   "L463 int , " +
                    //   "R461 int , " +
                    //   "R462 int , " +
                    //   "R463 int , " +
                    //   "G11L int , " +
                    //   "G12L int , " +
                    //   "G13L int , " +
                    //   "G14L int , " +
                    //   "G15L int , " +
                    //   "G16L int , " +
                    //   "G17L int , " +
                    //   "G11R int , " +
                    //   "G12R int , " +
                    //   "G13R int , " +
                    //   "G14R int , " +
                    //   "G15R int , " +
                    //   "G16R int , " +
                    //   "G17R int , " +
                    //   "G21L int , " +
                    //   "G22L int , " +
                    //   "G23L int , " +
                    //   "G24L int , " +
                    //   "G25L int , " +
                    //   "G26L int , " +
                    //   "G27L int , " +
                    //   "G21R int , " +
                    //   "G22R int , " +
                    //   "G23R int , " +
                    //   "G24R int , " +
                    //   "G25R int , " +
                    //   "G26R int , " +
                    //   "G27R int , " +
                    //   "D12 float , " +
                    //   "D13 float , " +
                    //   "D14 float , " +
                    //   "D15 float , " +
                    //   "D16 float , " +
                    //   "D17 float , " +
                    //   "D18 float , " +
                    //   "D19 float , " +
                    //   "D20 float , " +
                    //   "U64 int , " +
                    //   "RasxCD int, " +
                    //   "speed float " +
                    //   ")";

                    string comBD = "if not exists (select * from sysobjects where name ='" + "stan1s" + NumberTable1s + "' and xtype='U') create table " + "stan1s" + NumberTable1s +
                       "(" +
                       "id bigint IDENTITY(1,1) NOT NULL," +
                       // "dtsave datetime NULL CONSTRAINT " + "stan1s" + numberTable + "_dtsave  DEFAULT (getdate())," +
                       "dtsave datetime default NULL," +
                       "HL191 smallint default NULL," +
                       "HL192 smallint default NULL," +
                       "BL193 smallint default NULL," +
                       "BL194 smallint default NULL," +
                       "HR191 smallint default NULL," +
                       "HR192 smallint default NULL," +
                       "BR193 smallint default NULL," +
                       "BR194 smallint default NULL," +
                       "NL281 smallint default NULL," +
                       "NL282 smallint default NULL," +
                       "BL283 smallint default NULL," +
                       "BL284 smallint default NULL," +
                       "NR281 smallint default NULL," +
                       "NR282 smallint default NULL," +
                       "BR283 smallint default NULL," +
                       "BR284 smallint default NULL," +
                       "BL301 smallint default NULL," +
                       "BL302 smallint default NULL," +
                       "NL303 smallint default NULL," +
                       "NL304 smallint default NULL," +
                       "BR301 smallint default NULL," +
                       "BR302 smallint default NULL," +
                       "HR303 smallint default NULL," +
                       "HR304 smallint default NULL," +
                       "BL321 smallint default NULL," +
                       "BL322 smallint default NULL," +
                       "HL323 smallint default NULL," +
                       "HL324 smallint default NULL," +
                       "BR321 smallint default NULL," +
                       "BR322 smallint default NULL," +
                       "HR323 smallint default NULL," +
                       "HR324 smallint default NULL," +
                       "BL341 smallint default NULL," +
                       "BL342 smallint default NULL," +
                       "HL343 smallint default NULL," +
                       "HL344 smallint default NULL," +
                       "BR341 smallint default NULL," +
                       "BR342 smallint default NULL," +
                       "HR343 smallint default NULL," +
                       "HR344 smallint default NULL," +
                       "L461 smallint default NULL," +
                       "L462 smallint default NULL," +
                       "L463 smallint default NULL," +
                       "R461 smallint default NULL," +
                       "R462 smallint default NULL," +
                       "R463 smallint default NULL," +
                       "G11L smallint default NULL," +
                       "G12L smallint default NULL," +
                       "G13L smallint default NULL," +
                       "G14L smallint default NULL," +
                       "G15L smallint default NULL," +
                       "G16L smallint default NULL," +
                       "G17L smallint default NULL," +
                       "G11R smallint default NULL," +
                       "G12R smallint default NULL," +
                       "G13R smallint default NULL," +
                       "G14R smallint default NULL," +
                       "G15R smallint default NULL," +
                       "G16R smallint default NULL," +
                       "G17R smallint default NULL," +
                       "G21L smallint default NULL," +
                       "G22L smallint default NULL," +
                       "G23L smallint default NULL," +
                       "G24L smallint default NULL," +
                       "G25L smallint default NULL," +
                       "G26L smallint default NULL," +
                       "G27L smallint default NULL," +
                       "G21R smallint default NULL," +
                       "G22R smallint default NULL," +
                       "G23R smallint default NULL," +
                       "G24R smallint default NULL," +
                       "G25R smallint default NULL," +
                       "G26R smallint default NULL," +
                       "G27R smallint default NULL," +
                       "D12 float default NULL," +
                       "D13 float default NULL," +
                       "D14 float default NULL," +
                       "D15 float default NULL," +
                       "D16 float default NULL," +
                       "D17 float default NULL," +
                       "D18 float default NULL," +
                       "D19 float default NULL," +
                       "D20 float default NULL," +
                       "U64 smallint default NULL," +
                       "RasxCD smallint default NULL," +
                       "speed float default 0" +
                       ")";


                    using (SqlConnection conSQL1s1 = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conSQL1s1.Open();
                            SqlCommand command = new SqlCommand(comBD, conSQL1s1);
                            command.ExecuteNonQuery();
                            conSQL1s1.Close();
                        }
                        catch (Exception)
                        {
                            //Program.messageErrorSt1c = "Stan1s" + numberTable + " НЕ ЗАПИСАНЫ - " + ex.Message + " Insert запрос: " + comRulon1s1;
                            Program.messageErrorSt1cTab = "stan1s" + NumberTable1s + " НЕ СОЗДАНА ";
                            Program.dtErrorSt1cTab = DateTime.Now;

                        }
                        
                    }

                    #endregion

                    #region  // В Insert используем передачу параметров через Переменную
                    // string comRulon1s1 = "INSERT INTO " + "Stan1s" + numberTable +
                    //" (datetime1s,s191HL,s192HL,s193BL,s194BL,s191HR,s192HR,s193BR,s194BR,s281NL,s282NL,s283BL,s284BL,s281NR,s282NR," +
                    //"s283BR,s284BR,s301BL,s302BL,s303HL,s304HL,s301BR,s302BR,s303HR,s304HR,s321BL,s322BL,s323HL,s324HL,s321BR,s322BR," +
                    //"s323HR,s324HR,s341BL,s342BL,s343HL,s344HL,s341BR,s342BR,s343HR,s344HR,s461L,s462L,s463L,s461R,s462R,s463R,sG11L," +
                    //"sG12L,sG13L,sG14L,sG15L,sG16L,sG17L,sG11R,sG12R,sG13R,sG14R,sG15R,sG16R,sG17R,sG21L,sG22L,sG23L,sG24L,sG25L,sG26L," +
                    //"sG27L,sG21R,sG22R,sG23R,sG24R,sG25R,sG26R,sG27R,sD12,sD13,sD14,sD15,sD16,sD17,sD18,sD19,sD20,sU64,sRasxCD) " +
                    //"VALUES" +
                    //" ("+
                    //" @datetime1sStan, " +
                    //BitConverter.ToInt16(stanbuf1s, 0) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 1) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 2) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 3) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 4) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 5) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 6) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 7) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 8) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 9) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 10) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 11) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 12) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 13) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 14) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 15) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 16) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 17) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 18) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 19) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 20) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 21) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 22) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 23) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 24) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 25) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 26) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 27) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 28) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 29) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 30) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 31) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 32) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 33) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 34) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 35) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 36) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 37) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 38) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 39) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 40) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 41) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 42) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 43) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 44) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 45) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 46) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 47) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 48) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 49) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 50) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 51) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 52) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 53) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 54) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 55) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 56) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 57) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 58) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 59) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 60) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 61) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 62) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 63) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 64) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 65) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 66) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 67) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 68) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 69) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 70) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 71) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 72) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 73) + "," +
                    //"@sD12" + "," +
                    //"@sD13" + "," +
                    //"@sD14" + "," +
                    //"@sD15" + "," +
                    //"@sD16" + "," +
                    //"@sD17" + "," +
                    //"@sD18" + "," +
                    //"@sD19" + "," +
                    //"@sD20" + "," +
                    //(int)(BitConverter.ToInt16(stanbuf1s, 83)*10) + "," +
                    //(int)(BitConverter.ToInt16(stanbuf1s, 84)*10) + 
                    //")";

                    // using (SqlConnection conSQL1s2 = new SqlConnection(connectionString))
                    // {
                    //     try
                    //     {
                    //         conSQL1s2.Open();
                    //         SqlCommand command = new SqlCommand(comRulon1s1, conSQL1s2);
                    //         command.Parameters.AddWithValue("@datetime1sStan", DateTime.Now);
                    //         command.Parameters.AddWithValue("@sD12",(float)(BitConverter.ToInt16(stanbuf1s, 74)) / 10);
                    //         command.Parameters.AddWithValue("@sD13",(float)(BitConverter.ToInt16(stanbuf1s, 75)) / 10);
                    //         command.Parameters.AddWithValue("@sD14",(float)(BitConverter.ToInt16(stanbuf1s, 76)) / 10);
                    //         command.Parameters.AddWithValue("@sD15",(float)(BitConverter.ToInt16(stanbuf1s, 77)) / 10);
                    //         command.Parameters.AddWithValue("@sD16",(float)(BitConverter.ToInt16(stanbuf1s, 78)) / 10);
                    //         command.Parameters.AddWithValue("@sD17",(float)(BitConverter.ToInt16(stanbuf1s, 79)) / 10);
                    //         command.Parameters.AddWithValue("@sD18",(float)(BitConverter.ToInt16(stanbuf1s, 80)) / 10);
                    //         command.Parameters.AddWithValue("@sD19",(float)(BitConverter.ToInt16(stanbuf1s, 81)) / 10);
                    //         command.Parameters.AddWithValue("@sD20",(float)(BitConverter.ToInt16(stanbuf1s, 82)) / 10);

                    //         command.ExecuteNonQuery();
                    //         conSQL1s2.Close();
                    //         Program.messageOK1c = "Данные в БД("+ "Stan1s" + numberTable + ") 1s записаны";
                    //         Program.dtOK1c = DateTime.Now;
                    //     }
                    //     catch (Exception ex)
                    //     {

                    //         Program.messageError1c = "1s НЕ ЗАПИСАНЫ - " + ex.Message + " Insert запрос: " + comRulon1s1;
                    //         Program.dtError1c = DateTime.Now;
                    //     }



                    // }
                    #endregion

                    #region Через обычный инсерт но перед передачей выставили региональные настройки с помощью System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                    #region //Неправилько сконвртированно значение получаемых данных
                    //string comRulon1s1 = "INSERT INTO " + "Stan1s" + numberTable +
                    //" (datetime1s,s191HL,s192HL,s193BL,s194BL,s191HR,s192HR,s193BR,s194BR,s281NL,s282NL,s283BL,s284BL,s281NR,s282NR," +
                    //"s283BR,s284BR,s301BL,s302BL,s303HL,s304HL,s301BR,s302BR,s303HR,s304HR,s321BL,s322BL,s323HL,s324HL,s321BR,s322BR," +
                    //"s323HR,s324HR,s341BL,s342BL,s343HL,s344HL,s341BR,s342BR,s343HR,s344HR,s461L,s462L,s463L,s461R,s462R,s463R,sG11L," +
                    //"sG12L,sG13L,sG14L,sG15L,sG16L,sG17L,sG11R,sG12R,sG13R,sG14R,sG15R,sG16R,sG17R,sG21L,sG22L,sG23L,sG24L,sG25L,sG26L," +
                    //"sG27L,sG21R,sG22R,sG23R,sG24R,sG25R,sG26R,sG27R,sD12,sD13,sD14,sD15,sD16,sD17,sD18,sD19,sD20,sU64,sRasxCD) " +
                    //"VALUES" +
                    //" (" +
                    //" @datetime1sStan, " +
                    //BitConverter.ToInt16(stanbuf1s, 0) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 1) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 2) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 3) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 4) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 5) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 6) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 7) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 8) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 9) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 10) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 11) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 12) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 13) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 14) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 15) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 16) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 17) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 18) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 19) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 20) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 21) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 22) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 23) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 24) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 25) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 26) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 27) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 28) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 29) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 30) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 31) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 32) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 33) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 34) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 35) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 36) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 37) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 38) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 39) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 40) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 41) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 42) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 43) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 44) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 45) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 46) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 47) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 48) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 49) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 50) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 51) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 52) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 53) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 54) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 55) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 56) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 57) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 58) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 59) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 60) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 61) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 62) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 63) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 64) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 65) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 66) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 67) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 68) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 69) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 70) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 71) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 72) + "," +
                    //BitConverter.ToInt16(stanbuf1s, 73) + "," +
                    //(float)(BitConverter.ToInt16(stanbuf1s, 74)) / 10 + "," +
                    //(float)(BitConverter.ToInt16(stanbuf1s, 75)) / 10 + "," +
                    //(float)(BitConverter.ToInt16(stanbuf1s, 76)) / 10 + "," +
                    //(float)(BitConverter.ToInt16(stanbuf1s, 77)) / 10 + "," +
                    //(float)(BitConverter.ToInt16(stanbuf1s, 78)) / 10 + "," +
                    //(float)(BitConverter.ToInt16(stanbuf1s, 79)) / 10 + "," +
                    //(float)(BitConverter.ToInt16(stanbuf1s, 80)) / 10 + "," +
                    //(float)(BitConverter.ToInt16(stanbuf1s, 81)) / 10 + "," +
                    //(float)(BitConverter.ToInt16(stanbuf1s, 82)) / 10 + "," +
                    //(int)(BitConverter.ToInt16(stanbuf1s, 83) * 10) + "," +
                    //(int)(BitConverter.ToInt16(stanbuf1s, 84) * 10) +
                    //")";
                    #endregion

                    string comRulon1s1 = "INSERT INTO " + "stan1s" + NumberTable1s +
                  " (dtsave,HL191,HL192,BL193,BL194,HR191,HR192,BR193,BR194,NL281,NL282,BL283,BL284,NR281,NR282,BR283,BR284,BL301,BL302,NL303,NL304,BR301,BR302"+
                    ",HR303,HR304,BL321,BL322,HL323,HL324,BR321,BR322,HR323,HR324,BL341,BL342,HL343,HL344,BR341,BR342,HR343,HR344,L461,L462,L463,R461,R462,R463" +
                    ",G11L,G12L,G13L,G14L,G15L,G16L,G17L,G11R,G12R,G13R,G14R,G15R,G16R,G17R,G21L,G22L,G23L,G24L,G25L,G26L,G27L,G21R,G22R,G23R,G24R,G25R,G26R,G27R,D12,D13,D14,D15,D16,D17,D18,D19,D20,U64,RasxCD,speed) " +
                  "VALUES" +
                  " (" +
                  " @datetime1sStan, " +
                  //DateTime.Now + "," + 
                  stanbuf1s[0] + "," +
                  stanbuf1s[1] + "," +
                  stanbuf1s[2] + "," +
                  stanbuf1s[3] + "," +
                  stanbuf1s[4] + "," +
                  stanbuf1s[5] + "," +
                  stanbuf1s[6] + "," +
                  stanbuf1s[7] + "," +
                  stanbuf1s[8] + "," +
                  stanbuf1s[9] + "," +
                  stanbuf1s[10] + "," +
                  stanbuf1s[11] + "," +
                  stanbuf1s[12] + "," +
                  stanbuf1s[13] + "," +
                  stanbuf1s[14] + "," +
                  stanbuf1s[15] + "," +
                  stanbuf1s[16] + "," +
                  stanbuf1s[17] + "," +
                  stanbuf1s[18] + "," +
                  stanbuf1s[19] + "," +
                  stanbuf1s[20] + "," +
                  stanbuf1s[21] + "," +
                  stanbuf1s[22] + "," +
                  stanbuf1s[23] + "," +
                  stanbuf1s[24] + "," +
                  stanbuf1s[25] + "," +
                  stanbuf1s[26] + "," +
                  stanbuf1s[27] + "," +
                  stanbuf1s[28] + "," +
                  stanbuf1s[29] + "," +
                  stanbuf1s[30] + "," +
                  stanbuf1s[31] + "," +
                  stanbuf1s[32] + "," +
                  stanbuf1s[33] + "," +
                  stanbuf1s[34] + "," +
                  stanbuf1s[35] + "," +
                  stanbuf1s[36] + "," +
                  stanbuf1s[37] + "," +
                  stanbuf1s[38] + "," +
                  stanbuf1s[39] + "," +
                  stanbuf1s[40] + "," +
                  stanbuf1s[41] + "," +
                  stanbuf1s[42] + "," +
                  stanbuf1s[43] + "," +
                  stanbuf1s[44] + "," +
                  stanbuf1s[45] + "," +
                  stanbuf1s[46] + "," +
                  stanbuf1s[47] + "," +
                  stanbuf1s[48] + "," +
                  stanbuf1s[49] + "," +
                  stanbuf1s[50] + "," +
                  stanbuf1s[51] + "," +
                  stanbuf1s[52] + "," +
                  stanbuf1s[53] + "," +
                  stanbuf1s[54] + "," +
                  stanbuf1s[55] + "," +
                  stanbuf1s[56] + "," +
                  stanbuf1s[57] + "," +
                  stanbuf1s[58] + "," +
                  stanbuf1s[59] + "," +
                  stanbuf1s[60] + "," +
                  stanbuf1s[61] + "," +
                  stanbuf1s[62] + "," +
                  stanbuf1s[63] + "," +
                  stanbuf1s[64] + "," +
                  stanbuf1s[65] + "," +
                  stanbuf1s[66] + "," +
                  stanbuf1s[67] + "," +
                  stanbuf1s[68] + "," +
                  stanbuf1s[69] + "," +
                  stanbuf1s[70] + "," +
                  stanbuf1s[71] + "," +
                  stanbuf1s[72] + "," +
                  stanbuf1s[73] + "," +
                  
                   //(float)(BitConverter.ToInt16(stanbuf1s, 74)) / 10 + "," +
                  ((float)stanbuf1s[74])/10 + "," +
                  //(float)(BitConverter.ToInt16(stanbuf1s, 75)) / 10 + "," +
                  ((float)stanbuf1s[75]) / 10 + "," +
                  //(float)(BitConverter.ToInt16(stanbuf1s, 76)) / 10 + "," +
                  ((float)stanbuf1s[76]) / 10 + "," +
                  //(float)(BitConverter.ToInt16(stanbuf1s, 77)) / 10 + "," +
                  ((float)stanbuf1s[77]) / 10 + "," +
                  //(float)(BitConverter.ToInt16(stanbuf1s, 78)) / 10 + "," +
                  ((float)stanbuf1s[78]) / 10 + "," +
                  //(float)(BitConverter.ToInt16(stanbuf1s, 79)) / 10 + "," +
                  ((float)stanbuf1s[79]) / 10 + "," +
                  //(float)(BitConverter.ToInt16(stanbuf1s, 80)) / 10 + "," +
                  ((float)stanbuf1s[80]) / 10 + "," +
                  //(float)(BitConverter.ToInt16(stanbuf1s, 81)) / 10 + "," +
                  ((float)stanbuf1s[81]) / 10 + "," +
                  //(float)(BitConverter.ToInt16(stanbuf1s, 82)) / 10 + "," +
                  ((float)stanbuf1s[82]) / 10 + "," +
                  //(int)(BitConverter.ToInt16(stanbuf1s, 83) * 10) + "," +
                  stanbuf1s[83] * 10 + "," +
                  //(int)(BitConverter.ToInt16(stanbuf1s, 84) * 10) + "," +
                  stanbuf1s[84] * 10 + "," +
                  //(float)(BitConverter.ToInt16(stanbuffer1s, 6)) / 100 + 
                  (float)(BitConverter.ToInt16(stanbuf1s, 85)) / 100 +
                  ")";
                    //speed4kl = (float)(BitConverter.ToInt16(stanbuffer1s, 6)) / 100;
                    //(float)(BitConverter.ToInt16(stanbuf1s, 85)) / 100
                    //(float)(BitConverter.ToInt16(stanbuf1s, 95)) / 100
                    // (float)(BitConverter.ToInt16(stanbuffer1s, 6)) / 100
                    //(float)(BitConverter.ToInt16(stanbufferSQL, 6)) / 100

                    using (SqlConnection conSQL1s2 = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conSQL1s2.Open();
                            SqlCommand command = new SqlCommand(comRulon1s1, conSQL1s2);
                            command.Parameters.AddWithValue("@datetime1sStan", DateTime.Now);
                            command.ExecuteNonQuery();
                            conSQL1s2.Close();
                            Program.intConMessageOKSt1c = Program.intConMessageOKSt1c + 1;
                            Program.messageOKSt1c1 = "Данные в БД(" + "stan1s" + NumberTable1s + ") 1s записаны";
                            Program.messageOKSt1c2 = Program.messageOKSt1c2+".";
                            Program.dtOKSt1c = DateTime.Now;

                           
                        }
                        catch (Exception ex)
                        {

                            Program.messageErrorSt1c = "Stan1s" + numberTable + " НЕ ЗАПИСАНЫ - " + ex.Message + " Insert запрос: " + comRulon1s1;
                            Program.messageErrorSt1c = "stan1s" + NumberTable1s + " НЕ ЗАПИСАНЫ ";
                            Program.dtErrorSt1c = DateTime.Now;
                        }



                    }
                    #endregion

                    #endregion

                    #region //Расчет параметров прокатанного рулона после окончания прокатки

                    // stanD_tek_mot = (float)(BitConverter.ToInt16(stanbuffer1s, 20));
                    #region  //Время начало прокатки рулона ver 1

                    // if (stanD_pred_mot == 0)
                    //{
                    //    //при первом цикле все данные равны 0, поэтому мы выставляем значения параметров на 600
                    //    stanD_tek_mot = 600;
                    //    stanD_pred_mot = 600;
                    //}

                    //if (stanD_tek_mot > stanD_pred_mot)
                    //{
                    //        if (stanD_pred_mot < 615)
                    //        {
                    //            stanTimeStart = DateTime.Now;
                    //            blRulonStart = true;
                    //        }

                    //}
                    //else
                    //{
                    //      //blRulonStart = false;
                    //}


                    #endregion

                    #region //Толщина и ширина прокатываемого рулона
                    //speed4kl = (float)(BitConverter.ToInt16(stanbuffer1s, 6)) / 100;
                    //if ((stanTimeStart != new DateTime()) && (H5_work == 0) && (stanD_tek_mot > 700) && (speed4kl > 2))
                    //{
                    //    H5_work = (float)(BitConverter.ToInt16(stanbuffer1s, 12)) / 1000;
                    //    B_Work = (int)BitConverter.ToInt16(stanbuffer1s, 14);
                    //}
                    #endregion

                    #region //Формирование сигнала окончания прокатки
                    //if ((stanTimeStart != new DateTime()) && (H5_work != 0) && (stanD_tek_mot < 610) && (stanD_tek_mot < stanD_pred_mot))
                    //{
                    //    Ves_Work = (((((stanD_pred_mot * stanD_pred_mot)/1000000 - 0.36F) * 3.141593F) / 4) * (B_Work / 1000)) * 7.85F;
                    //    stanTimeStop = DateTime.Now;
                    //    Dlina_Work = ((Ves_Work / 7.85F) / (B_Work / 1000)) / (H5_work / 1000);
                    //    blRulonStop = true;
                    //    blRulonStart = false;

                    //    //messageRulon = stanTimeStart.ToString("HH:mm:ss.fff") + " - " + stanTimeStop.ToString("HH:mm:ss.fff") + "   " + B_Work + "*" + H5_work + "=" + Ves_Work;


                    //    #region //База производство

                    //    #region //Создание БД

                    //   // string comWorkStanCreate = "if not exists (select * from sysobjects where name ='work_stan' and xtype='U') create table work_stan" +
                    //   //"(" +
                    //   //"numberRulona bigint, " +
                    //   //"start datetime , " +
                    //   //"stop datetime , " +
                    //   //"h5 float , " +
                    //   //"b float , " +
                    //   //"ves float , " +
                    //   //"dlinna float , " +
                    //   //"t1 float , " +
                    //   //"t2 float , " +
                    //   //"t3 float , " +
                    //   //"t4 float , " +
                    //   //"t5 float  " +

                    //   //")";

                    //   // using (SqlConnection conSQL1sWork1 = new SqlConnection(connectionString))
                    //   // {
                    //   //     conSQL1sWork1.Open();
                    //   //     SqlCommand command = new SqlCommand(comWorkStanCreate, conSQL1sWork1);
                    //   //     command.ExecuteNonQuery();
                    //   //     conSQL1sWork1.Close();
                    //   // }
                    //    #endregion;

                    //    #region //Заполнение производство
                    //    //string comWorkStan = "INSERT INTO work_stan( " +
                    //    //    "numberRulona," +
                    //    //    "start," +
                    //    //    "stop," +
                    //    //    "h5," +
                    //    //    "b," +
                    //    //    "ves," +
                    //    //    "dlinna" +
                    //    //    ") " +
                    //    //    "VALUES(" +
                    //    //    "@NumberRulon, " +
                    //    //    "@TimeStart, " +
                    //    //    "@TimeStop, " +
                    //    //    "@H5_work, " +
                    //    //    "@B_Work, " +
                    //    //    "@Ves_Work, " +
                    //    //    "@Dlina_Work)";

                    //    //string beginWork = stanTimeStart.ToString("ddMMyyyyHHmm");
                    //    //string endWork = stanTimeStop.ToString("HHmm");
                    //    //string strNumberRulona = beginWork + endWork;

                    //    ////Добавляем в таблицу прокатанных рулонов данные по рулонам
                    //    //using (SqlConnection con3 = new SqlConnection(connectionString))
                    //    //{
                    //    //    try
                    //    //    {
                    //    //        con3.Open();
                    //    //        SqlCommand command = new SqlCommand(comWorkStan, con3);

                    //    //        command.Parameters.AddWithValue("@NumberRulon", strNumberRulona);

                    //    //        command.Parameters.AddWithValue("@TimeStart", stanTimeStart);
                    //    //        command.Parameters.AddWithValue("@TimeStop", stanTimeStop);
                    //    //        command.Parameters.AddWithValue("@H5_work", H5_work);
                    //    //        command.Parameters.AddWithValue("@B_Work", B_Work);
                    //    //        command.Parameters.AddWithValue("@Ves_Work", Ves_Work);
                    //    //        command.Parameters.AddWithValue("@Dlina_Work", Dlina_Work);

                    //    //        int WriteSQL = command.ExecuteNonQuery();

                    //    //        Program.messageOKStProizvodstvo = strNumberRulona+"("+ stanTimeStart.ToString("HH:mm")+"-"+ stanTimeStop.ToString("HH:mm")+") "+ H5_work  +"*"+ B_Work + "->"+ Ves_Work;
                    //    //        //messageOKProizvodstvo = "производство";
                    //    //        Program.dtOKStProizvodstvo = DateTime.Now;
                    //    //    }
                    //    //    catch (Exception ex)
                    //    //    {
                    //    //        Program.messageErrorStProizvodstvo = "Ошибка в сохранении данных о прокатанном рулоне " + ex.Message + " Insert запрос: " + comWorkStan;
                    //    //        Program.dtErrorStProizvodstvo = DateTime.Now;

                    //    //    }

                    //    //}
                    //    #endregion

                    //    #endregion



                    //    #region //Очищаем базу временных рулонов
                    //    //using (SqlConnection conSQL1s3 = new SqlConnection(connectionString))
                    //    //{
                    //    //    conSQL1s3.Open();
                    //    //    string comRulon1s2 = "DELETE FROM TEMPstan101ms";
                    //    //    SqlCommand command = new SqlCommand(comRulon1s2, conSQL1s3);
                    //    //    command.ExecuteNonQuery();


                    //    //}
                    //    #endregion




                    //    #region //Мпереименовываем временную базу в базу с именем stan100mc(дата+время начала)(время окончания)
                    //    //using (SqlConnection conSQL1s3 = new SqlConnection(connectionString))
                    //    //{
                    //    //    try
                    //    //    {
                    //    //        conSQL1s3.Open();
                    //    //        string begin = stanTimeStart.ToString("ddMMyyyyHHmm");
                    //    //        string end = stanTimeStop.ToString("HHmm");
                    //    //        string comRulon1s2 = "sp_rename 'TEMPstan101ms','" + begin + end + "'";
                    //    //        SqlCommand command = new SqlCommand(comRulon1s2, conSQL1s3);
                    //    //        command.ExecuteNonQuery();
                    //    //        Program.messageOKStRulon = "Временная база -> " + begin + end;
                    //    //        Program.dtOKStRulon = DateTime.Now;
                    //    //        conSQL1s3.Close();
                    //    //    }
                    //    //    catch (Exception ex)
                    //    //    {

                    //    //        Program.messageErrorStRulon = "Временная база не переименована " + ex.Message;
                    //    //        Program.dtErrorStRulon = DateTime.Now;
                    //    //    }



                    //    }
                    //    #endregion


                    //    #region //Если БД не существует то создаем
                    ////    string comRulon101ms1 = "if not exists (select * from sysobjects where name ='TEMPstan101ms' and xtype='U') create table TEMPstan101ms " +
                    ////       "(" +
                    ////       "datetime101ms datetime , " +
                    ////       "v1 float," +
                    ////       "v2 float," +
                    ////       "v3 float," +
                    ////       "v4 float," +
                    ////       "v5 float," +
                    ////       "h1 float," +
                    ////       "h5 float," +
                    ////       "b int," +
                    ////       "dvip float," +
                    ////       "drazm float," +
                    ////       "dmot float," +
                    ////       "vvip float," +
                    ////       "d1 int," +
                    ////       "d2 int," +
                    ////       "d3 int," +
                    ////       "d4 int," +
                    ////       "d5 int," +
                    ////       "e2 float," +
                    ////       "e3 float," +
                    ////       "e4 float," +
                    ////       "e5 float," +
                    ////       "n1l float," +
                    ////       "n1p float," +
                    ////       "n2l float," +
                    ////       "n2p float," +
                    ////       "n3l float," +
                    ////       "n3p float," +
                    ////       "n4l float," +
                    ////       "n4p float," +
                    ////       "n5l float," +
                    ////       "n5p float," +
                    ////       "reserv1 float," +
                    ////       "reserv2 float," +
                    ////       "t1 float," +
                    ////       "t2 float," +
                    ////       "t3 float," +
                    ////       "t4 float," +
                    ////       "t1l float," +
                    ////       "t2l float," +
                    ////       "t3l float," +
                    ////       "t4l float," +
                    ////       "t1p float," +
                    ////       "t2p float," +
                    ////       "t3p float," +
                    ////       "t4p float," +
                    ////       "t1z float," +
                    ////       "t2z float," +
                    ////       "t3z float," +
                    ////       "t4z float," +
                    ////       "erazm float," +
                    ////       "ivozbrazm float," +
                    ////       "izadrazm float," +
                    ////       "w1 float," +
                    ////       "w2v float," +
                    ////       "w2n float," +
                    ////       "w3v float," +
                    ////       "w3n float," +
                    ////       "w4v float," +
                    ////       "w4n float," +
                    ////       "w5v float," +
                    ////       "w5n float," +
                    ////       "wmot float," +
                    ////       "imot int," +
                    ////       "izadmot int," +
                    ////       "u1 float," +
                    ////       "u2v float," +
                    ////       "u2n float," +
                    ////       "u3v float," +
                    ////       "u3n float," +
                    ////       "u4v float," +
                    ////       "u4n float," +
                    ////       "u5v float," +
                    ////       "u5n float," +
                    ////       "umot float," +
                    ////       "i1 int," +
                    ////       "i2v int," +
                    ////       "i2n int," +
                    ////       "i3v int," +
                    ////       "i3n int," +
                    ////       "i4v int," +
                    ////       "i4n int," +
                    ////       "i5v int," +
                    ////       "i5n int," +
                    ////       "rtv float," +
                    ////       "dt1 float," +
                    ////       "dt2 float," +
                    ////       "dt3 float," +
                    ////       "dt4 float," +
                    ////       "grt float," +
                    ////       "trt float," +
                    ////       "mv1 float," +
                    ////       "mv2 float," +
                    ////       "mv3 float," +
                    ////       "dh1 float," +
                    ////       "dh5 float," +
                    ////       "os1klvb int," +
                    ////       "rezerv int," +
                    ////       "mezdoza4 int" +
                    ////       ")";

                    ////    using (SqlConnection conSQL101ms1 = new SqlConnection(connectionString))
                    ////    {
                    ////        conSQL101ms1.Open();
                    ////        SqlCommand command = new SqlCommand(comRulon101ms1, conSQL101ms1);
                    ////        command.ExecuteNonQuery();
                    ////        conSQL101ms1.Close();

                    ////    }
                    ////    #endregion

                    ////}
                    //#endregion


                    #endregion

                    // stanD_pred_mot = stanD_tek_mot;

                    #endregion

                    #region Перевалки 

                    int d1 = (int)BitConverter.ToInt16(stanbuffer1s, 24);
                    int d2 = (int)BitConverter.ToInt16(stanbuffer1s, 26);
                    int d3 = (int)BitConverter.ToInt16(stanbuffer1s, 28);
                    int d4 = (int)BitConverter.ToInt16(stanbuffer1s, 30);
                    int d5 = (int)BitConverter.ToInt16(stanbuffer1s, 32);

                    if (d1_pred == 0) d1_pred = d1;
                    if (d2_pred == 0) d2_pred = d2;
                    if (d3_pred == 0) d3_pred = d3;
                    if (d4_pred == 0) d4_pred = d4;
                    if (d5_pred == 0) d5_pred = d5;

                    bool blSave = false;

                    string Perevalki = "";

                    try
                    {

                        if (d1_pred != d1)
                        {
                            blSave = true;
                            dtPerevalkiStan.Rows.Add(DateTime.Now, d1, 0, 0, 0, 0);
                            Perevalki = Perevalki + " d1=" + d1;
                        }
                        if (d2_pred != d2)
                        {
                           blSave = true;
                            dtPerevalkiStan.Rows.Add(DateTime.Now, 0, d2, 0, 0, 0);
                            Perevalki = Perevalki + " d2=" + d2;
                        }
                        if (d3_pred != d3)
                        {
                           blSave = true;
                            dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, d3, 0, 0);
                            Perevalki = Perevalki + " d3=" + d3;
                        }
                        if (d4_pred != d4)
                        {
                            blSave = true;
                            dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, 0, d4, 0);
                            Perevalki = Perevalki + " d4=" + d4;

                        }
                        if (d5_pred != d5)
                        {
                             blSave = true;
                            dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, 0, 0, d5);
                            Perevalki = Perevalki + " d5=" + d5;
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        //ошибка
                        Program.messageErrorStValki = "Ошибка формировании таблицы валков - " + ex.Message;
                        Program.dtErrorStValki = DateTime.Now; ;
                    }



                    d1_pred = d1;
                    d2_pred = d2;
                    d3_pred = d3;
                    d4_pred = d4;
                    d5_pred = d5;



                    #region Перевалки сохраняем в БД

                    if (blSave)
                    {
                        //string strTableNamePerevalki = "StanPerevalki" + DateTime.Now.ToString("yyyyMM");
                        string strTableNamePerevalki = "perevalki";
                        string comBDPerevalki = "if not exists (select * from sysobjects where name='" + strTableNamePerevalki + "' and xtype='U') create table " + strTableNamePerevalki +
                                "(" +
                                "dtsave datetime NOT NULL, " +
                                "kl1 int NOT NULL, " +
                                "kl2 int NOT NULL, " +
                                "kl3 int NOT NULL, " +
                                "kl4 int NOT NULL, " +
                                "kl5 int NOT NULL )";

                        //создаем таблицу значений Перевалок
                        using (SqlConnection conPerevalki1 = new SqlConnection(connectionString))
                        {
                            try
                            {
                                conPerevalki1.Open();
                                SqlCommand command = new SqlCommand(comBDPerevalki, conPerevalki1);
                                command.ExecuteNonQuery();
                                conPerevalki1.Close();
                                //messageErrorValki = "OK формировании таблицы валков";

                            }
                            catch (Exception ex)
                            {
                                Program.messageErrorStValki = "Ошибка формировании таблицы валков " + ex.Message;
                                Program.dtErrorStValki = DateTime.Now;

                            }


                        }
                        //записываем в таблицу прокатанного рулона данные по прокатке этого рулона
                        using (SqlConnection conPerevalki2 = new SqlConnection(connectionString))
                        {
                            try
                            {
                                conPerevalki2.Open();
                                using (var bulk = new SqlBulkCopy(conPerevalki2))
                                {
                                    bulk.DestinationTableName = strTableNamePerevalki;
                                    bulk.WriteToServer(dtPerevalkiStan);
                                    Program.messageOKStValki = "Перевалки (" + strTableNamePerevalki+")"+Perevalki;
                                    Program.dtOKStValki = DateTime.Now;


                                    dtPerevalkiStan.Clear(); //очистка таблицы 
                                }
                                conPerevalki2.Close();



                            }
                            catch (Exception ex)
                            {
                                Program.messageErrorStValki = "Ошибка записи в таблицу валков " + ex.Message;
                                Program.dtErrorStValki = DateTime.Now;

                            }
                        }

                    }

                    #endregion

                    //Console.WriteLine(d1_pred + "-" + d2_pred + "-" + d3_pred + "-" + d4_pred + "-" + d5_pred);

                    #endregion


                }
            }
            catch (Exception ex)
            {
                Program.messageErrorSt1c = "Ошибка глобальная - " + ex;
                Program.dtErrorSt1c = DateTime.Now;

            }
            
           
        }

       #endregion


        #region 200мс  - Формируем и записываем сообщения в Базу
        private void stanMessage200ms()
        {
            TByte_Signal arr, arr_minus = new TByte_Signal();

            arr = default(TByte_Signal);
            

            try
            {
                while (true)
                {
                    Thread.Sleep(200);


                    #region Формируем шифр таблицы numberTable = stanmessyyyyMMddсмена

                    numberTable = "";

                    TimeSpan NowTime = DateTime.Now.TimeOfDay;
                    TimeSpan Time1 = new TimeSpan(07, 00, 00);
                    TimeSpan Time2 = new TimeSpan(19, 00, 00);

                    if ((NowTime > Time1) && (NowTime < Time2))
                    {
                        //2 смена
                        numberTable = DateTime.Now.ToString("yyyyMMdd") + "2";
                    }
                    else if ((NowTime < Time1) || (NowTime > Time2))
                    {
                        //1 смена
                        if (NowTime > Time2)
                        {
                            //1 смена после 19
                            numberTable = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "1";
                        }
                        else
                        {
                            //1 смена до 7
                            numberTable = DateTime.Now.ToString("yyyyMMdd") + "1";
                        }
                    }

                    string strTableName = "stanmess" + numberTable;

                    #endregion


                    //Program.intmessageOKSt200mc = Program.intmessageOKSt200mc + 1;

                    Program.messageOKSt200mc = "(" + strTableName + ")";
                    Program.messageOKSt200mc1 = Program.messageOKSt200mc1 + ".";
                    Program.dtOKSt200mc = dtMessage;

                    dtMessage = DateTime.Now;

                    float speed = (float)(BitConverter.ToInt16(stanbufferMessage, 16)) / 100;


                    int numberMessage = 0;

                    #region //Версия 1 - не происходит сбора данных
                    //for (int i = 0; i < 15; i++)
                    //{
                    //    for (int b = 0; b < 8; b++)
                    //    {
                    //        int z = Convert.ToInt32(Math.Pow(2, b));
                    //        if (((byte)(stanbufferMessageOld[i] & z) - (byte)(stanbufferMessage[i] & z)) < 0)
                    //        {
                    //            if (MessageStan[numberMessage].statusMenshe != 0)
                    //            {
                    //                string mes = MessageStan[numberMessage].MinusMess;
                    //                int status = MessageStan[numberMessage].statusMenshe;
                    //                dtMessagestan.Rows.Add(dtMessage.ToString("HH:mm:ss.fff"), status, mes, speed);
                    //            }
                    //        }
                    //        else if (((byte)(stanbufferMessageOld[i] & z) - (byte)(stanbufferMessage[i] & z)) > 0)
                    //        {
                    //            if (MessageStan[numberMessage].statusBolshe != 0)
                    //            {
                    //                string mes = MessageStan[numberMessage].PlusMess;
                    //                int status = MessageStan[numberMessage].statusBolshe;
                    //                dtMessagestan.Rows.Add(dtMessage.ToString("HH:mm:ss.fff"), status, mes, speed);
                    //            }
                    //        }
                    //        numberMessage++;

                    //    }
                    //}

                    //TODO запись сообщений в БД каждую минуту  

                    //if (writeMessage > 300)
                    //{
                    //    writeMessage = 0;
                    //    string strTableName = "stanmess" + numberTable;

                    //    #region Проверяем создана ли таблица
                    //    string comBDMessage = "if not exists (select * from sysobjects where name='" + strTableName + "' and xtype='U') create table " + strTableName +
                    //                "(" +
                    //                "dtmes datetime NOT NULL, " +
                    //                "status int NOT NULL, " +
                    //                "message text NOT NULL, " +
                    //                "speed float NOT NULL)";

                    //    //создаем таблицу сообщений стана 
                    //    using (SqlConnection con1Mess = new SqlConnection(connectionString))
                    //    {
                    //        try
                    //        {
                    //            con1Mess.Open();
                    //            SqlCommand command = new SqlCommand(comBDMessage, con1Mess);
                    //            int WriteSQL = command.ExecuteNonQuery();
                    //            con1Mess.Close();
                    //        }
                    //        catch (Exception)
                    //        {
                    //            Program.dtErrorSt200mc = DateTime.Now;
                    //            Program.messageErrorSt200mc = "Ошибка при создании таблицы Сообщений (" + strTableName + ")";
                    //        }

                    //    }
                    //    #endregion

                    //    #region Сохраняем данные в таблицу
                    //    if (dtMessagestan.Rows.Count > 0)
                    //    {
                    //        //записываем в таблицу прокатанного рулона данные по прокатке этого рулона
                    //        try
                    //        {
                    //            using (SqlConnection con2Mess = new SqlConnection(connectionString))
                    //            {
                    //                con2Mess.Open();
                    //                using (var bulkMessage = new SqlBulkCopy(con2Mess))
                    //                {
                    //                    bulkMessage.DestinationTableName = strTableName;
                    //                    bulkMessage.WriteToServer(dtMessagestan);
                    //                    // Program.messageOKSt200mc1 = Program.messageOKSt200mc1 + ".";
                    //                    Program.intmessageOKSt200mc = Program.intmessageOKSt200mc + dtMessagestan.Rows.Count;
                    //                    Program.messageOKSt200mc = "В таблицу " + strTableName + " за 5с записано [" + Program.intmessageOKSt200mc + "|" + dtMessagestan.Rows.Count + ":" + writeMessage + "]";
                    //                    Program.dtOKSt200mc = dtMessage;
                    //                }
                    //            }
                    //            dtMessagestan.Clear();
                    //        }
                    //        catch (Exception)
                    //        {
                    //            Program.dtErrorSt200mc = DateTime.Now;
                    //            Program.messageErrorSt200mc = "Ошибка при записи в таблицу Сообщений (" + strTableName + ")";

                    //        }


                    //    }
                    //    else
                    //    {

                    //        Program.messageOKSt200mc = "Сообщений не было с " + DateTime.Now.AddMinutes(-1).ToString("dd:MM HH:mm") + " по " + DateTime.Now.ToString("HH:mm");
                    //        Program.dtOKSt200mc = dtMessage;

                    //    }
                    //    #endregion


                    //}
                    //else
                    //{
                    //    writeMessage++;
                    //}

                    #endregion


                    #region Версия 2  - 

                    #region Преобразование сигнала в структуру
                    arr.RegTD = (byte)(stanbufferMessage[0] & 1);
                    arr.RegRazg = (byte)(stanbufferMessage[0] & 2);
                    arr.RegNO = (byte)(stanbufferMessage[0] & 4);
                    arr.RegFO = (byte)(stanbufferMessage[0] & 8);
                    arr.Vip = (byte)(stanbufferMessage[0] & 16);
                    arr.T10 = (byte)(stanbufferMessage[0] & 32);
                    arr.T20 = (byte)(stanbufferMessage[0] & 64);
                    arr.T30 = (byte)(stanbufferMessage[0] & 128);

                    arr.ZS = (byte)(stanbufferMessage[1] & 1);
                    arr.TD = (byte)(stanbufferMessage[1] & 2);
                    arr.RS = (byte)(stanbufferMessage[1] & 4);
                    arr.NO = (byte)(stanbufferMessage[1] & 8);
                    arr.FO = (byte)(stanbufferMessage[1] & 16);
                    arr.MaxSpeedPeregr = (byte)(stanbufferMessage[1] & 32);
                    arr.UstavkaSpeed = (byte)(stanbufferMessage[1] & 64);
                    arr.MaxSpeed = (byte)(stanbufferMessage[1] & 128);

                    //mb3069-mb3068
                    arr.LKmot = (byte)(stanbufferMessage[2] & 1);
                    arr.LKrazm = (byte)(stanbufferMessage[2] & 2);
                    arr.Got_64kg = (byte)(stanbufferMessage[2] & 4);
                    arr.Rnz12 = (byte)(stanbufferMessage[2] & 8);
                    arr.Rnz23 = (byte)(stanbufferMessage[2] & 16);
                    arr.Rnz34 = (byte)(stanbufferMessage[2] & 32);
                    arr.GrtVkl = (byte)(stanbufferMessage[2] & 64);
                    arr.TrtVkl = (byte)(stanbufferMessage[2] & 128);

                    arr.T40 = (byte)(stanbufferMessage[3] & 1);
                    arr.Tmot = (byte)(stanbufferMessage[3] & 2);
                    arr.Trazm = (byte)(stanbufferMessage[3] & 4);
                    arr.LK1 = (byte)(stanbufferMessage[3] & 8);
                    arr.LK2 = (byte)(stanbufferMessage[3] & 16);
                    arr.LK3 = (byte)(stanbufferMessage[3] & 32);
                    arr.LK4 = (byte)(stanbufferMessage[3] & 64);
                    arr.LK5 = (byte)(stanbufferMessage[3] & 128);

                    //mb3071-mb3070
                    arr.NalPol = (byte)(stanbufferMessage[4] & 1);
                    arr.Knp = (byte)(stanbufferMessage[4] & 2);
                    arr.GotStan = (byte)(stanbufferMessage[4] & 4);
                    arr.MaxV1 = (byte)(stanbufferMessage[4] & 8);
                    arr.MaxV2 = (byte)(stanbufferMessage[4] & 16);
                    arr.MaxV3 = (byte)(stanbufferMessage[4] & 32);
                    arr.MaxV4 = (byte)(stanbufferMessage[4] & 64);
                    arr.MaxV5 = (byte)(stanbufferMessage[4] & 128);

                    arr.RKDVVkl = (byte)(stanbufferMessage[5] & 1);
                    arr.RpvVkl = (byte)(stanbufferMessage[5] & 2);
                    arr.Rnv12 = (byte)(stanbufferMessage[5] & 4);
                    arr.Rnv23 = (byte)(stanbufferMessage[5] & 8);
                    arr.Rnv34 = (byte)(stanbufferMessage[5] & 16);
                    arr.Rnz45 = (byte)(stanbufferMessage[5] & 32);
                    arr.Rtv = (byte)(stanbufferMessage[5] & 64);
                    arr.Got_100kg = (byte)(stanbufferMessage[5] & 128);

                    //m3103-m3102
                    arr.g12 = (byte)(stanbufferMessage[6] & 1);
                    arr.g13 = (byte)(stanbufferMessage[6] & 2);
                    arr.g14 = (byte)(stanbufferMessage[6] & 4);
                    arr.g15 = (byte)(stanbufferMessage[6] & 8);
                    arr.g16 = (byte)(stanbufferMessage[6] & 16);
                    arr.NatshUsl = (byte)(stanbufferMessage[6] & 32);
                    arr.GotEmuls = (byte)(stanbufferMessage[6] & 64);
                    arr.g17 = (byte)(stanbufferMessage[6] & 128);

                    arr.g18 = (byte)(stanbufferMessage[7] & 1);
                    arr.g19 = (byte)(stanbufferMessage[7] & 2);
                    arr.g20 = (byte)(stanbufferMessage[7] & 4);
                    arr.temp_POU = (byte)(stanbufferMessage[7] & 8);
                    arr.davl_redukt = (byte)(stanbufferMessage[7] & 16);
                    arr.davl_PGT = (byte)(stanbufferMessage[7] & 32);
                    arr.temp_privod = (byte)(stanbufferMessage[7] & 64);
                    arr.got_sinhr = (byte)(stanbufferMessage[7] & 128);

                    //m3105-m3104
                    arr.OgragdMot = (byte)(stanbufferMessage[8] & 1);
                    arr.ZaxlestOtMot = (byte)(stanbufferMessage[8] & 2);
                    arr.NOTempGP = (byte)(stanbufferMessage[8] & 4);
                    arr.Peregr1 = (byte)(stanbufferMessage[8] & 8);
                    arr.Peregr2 = (byte)(stanbufferMessage[8] & 16);
                    arr.Peregr3 = (byte)(stanbufferMessage[8] & 32);
                    arr.Peregr4 = (byte)(stanbufferMessage[8] & 64);
                    arr.Peregr5 = (byte)(stanbufferMessage[8] & 128);

                    arr.NOSinxr = (byte)(stanbufferMessage[9] & 1);
                    arr.NOPanPultStar = (byte)(stanbufferMessage[9] & 2);
                    arr.NOPURazm = (byte)(stanbufferMessage[9] & 4);
                    arr.NOPU1 = (byte)(stanbufferMessage[9] & 8);
                    arr.NOPU2 = (byte)(stanbufferMessage[9] & 16);
                    arr.NOPU3 = (byte)(stanbufferMessage[9] & 32);
                    arr.NOPU4 = (byte)(stanbufferMessage[9] & 64);
                    arr.NOPU5 = (byte)(stanbufferMessage[9] & 128);

                    //m3103-m3102
                    arr.FOPanPultStar = (byte)(stanbufferMessage[10] & 1);
                    arr.FOPU5 = (byte)(stanbufferMessage[10] & 2);
                    arr.AOPUR = (byte)(stanbufferMessage[10] & 4);
                    arr.TrazmProval = (byte)(stanbufferMessage[10] & 8);
                    arr.T12proval = (byte)(stanbufferMessage[10] & 16);
                    arr.T23proval = (byte)(stanbufferMessage[10] & 32);
                    arr.T34proval = (byte)(stanbufferMessage[10] & 64);
                    arr.T45proval = (byte)(stanbufferMessage[10] & 128);

                    arr.Vent_101G = (byte)(stanbufferMessage[11] & 1);
                    arr.Vent_102G = (byte)(stanbufferMessage[11] & 2);
                    arr.Vent_103G = (byte)(stanbufferMessage[11] & 4);
                    arr.Vent_105G = (byte)(stanbufferMessage[11] & 8);
                    arr.Vent_106G = (byte)(stanbufferMessage[11] & 16);
                    arr.Vent_podpor_PA1 = (byte)(stanbufferMessage[11] & 32);
                    arr.Vent_112G = (byte)(stanbufferMessage[11] & 64);
                    arr.Vent_111G = (byte)(stanbufferMessage[11] & 128);
                    
                    //m3103-m3102
                    arr.Vent_110G = (byte)(stanbufferMessage[12] & 1);
                    arr.Vent_108G = (byte)(stanbufferMessage[12] & 2);
                    arr.Vent_107G = (byte)(stanbufferMessage[12] & 4);
                    arr.Vent_podpor_PA2 = (byte)(stanbufferMessage[12] & 8);
                    arr.Vent_1kl = (byte)(stanbufferMessage[12] & 16);
                    arr.Vent_2kl = (byte)(stanbufferMessage[12] & 32);
                    arr.Vent_3kl = (byte)(stanbufferMessage[12] & 64);
                    arr.Vent_4kl = (byte)(stanbufferMessage[12] & 128);

                    arr.FOPU4 = (byte)(stanbufferMessage[13] & 1);
                    arr.FOPU3 = (byte)(stanbufferMessage[13] & 2);
                    arr.FOPU2 = (byte)(stanbufferMessage[13] & 4);
                    arr.FOPU1 = (byte)(stanbufferMessage[13] & 8);
                    arr.FOPUR = (byte)(stanbufferMessage[13] & 16);
                    arr.AOSUSknopka = (byte)(stanbufferMessage[13] & 22);
                    arr.AO5klet = (byte)(stanbufferMessage[13] & 64);
                    //arr.OgrRdn3nv = (byte)(stanbufferMessage[13] & 128);


                    //m3103-m3102
                    //arr.OgrRdn3vv = (byte)(stanbufferMessage[14] & 1);
                    //arr.Ig140pr3vv = (byte)(stanbufferMessage[14] & 2);
                    arr.KnAOsus = (byte)(stanbufferMessage[14] & 4);
                    arr.SmGot = (byte)(stanbufferMessage[14] & 8);
                    arr.KteRazm = (byte)(stanbufferMessage[14] & 16);
                    arr.AOrele = (byte)(stanbufferMessage[14] & 32);
                    arr.FOknop = (byte)(stanbufferMessage[14] & 64);
                    arr.R56 = (byte)(stanbufferMessage[14] & 128);

                    arr.Vent_5kl = (byte)(stanbufferMessage[15] & 1);
                    arr.Vent_mot = (byte)(stanbufferMessage[15] & 2);
                    arr.Vent_podpor_GP1 = (byte)(stanbufferMessage[15] & 4);
                    arr.Vent_podpor_GP2 = (byte)(stanbufferMessage[15] & 8);
                    arr.Vent_NV = (byte)(stanbufferMessage[15] & 16);
                    arr.Dv2pr09sec3vv = (byte)(stanbufferMessage[15] & 32);
                    //arr.OgrRsRt3vv = (byte)(stanbufferMessage[15] & 64);
                    //arr.OgrRt3vv = (byte)(stanbufferMessage[15] & 128);

                    #endregion

                    #region для таблицы View
                    string chema = (arr.GotStan > 0) ? "Собрана" : "Разобрана";
                    string emuls = (arr.GotEmuls > 0) ? "Готова" : "Не готова";
                    string regim = (arr.RegRazg > 0) ? "Разгон" : "Форсированный останов";
                    regim = (arr.RegTD > 0) ? "Так держать" : "Форсированный останов";
                    regim = (arr.RegNO > 0) ? "Нормальный останов" : "Форсированный останов";
                    string predel = (arr.Vip > 0) ? "Выпуск" : "Ноль скорости";
                    predel = (arr.UstavkaSpeed > 0) ? "Уставка скорости" : "Ноль скорости";
                    predel = (arr.MaxSpeed > 0) ? "Максимальная скорость" : "Ноль скорости";
                    predel = (arr.MaxSpeedPeregr > 0) ? "Перегруз по скорости" : "Ноль скорости";
                    string polosa5 = (arr.NalPol > 0) ? "Есть" : "Нет";
                    string trazm = (arr.Trazm > 0) ? "Есть" : "Нет";
                    string tmot = (arr.Tmot > 0) ? "Есть" : "Нет";

                    #endregion

                    string QuerySQL = "";
                    int QueryCount = 0;

                    #region QuerySQL - Формирование сообщений в таблицу сообщений "(1,'" + dtMessage.ToString() + "','Сообшение'," + speed.ToString() + "),"; QueryCount++;

                    if ((arr_minus.TD - arr.TD) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Ключ ТАК ДЕРЖАТЬ'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.ZS - arr.ZS) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка ЗАПРАВКА'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.RS - arr.RS) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Ключ РАЗГОН'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NO - arr.NO) < 0) { QuerySQL = QuerySQL + "(3,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка НОРМАЛЬНЫЙ ОСТАНОВ'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.FO - arr.FO) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка ФОРСИРОВАННЫЙ ОСТАНОВ'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.RegTD - arr.RegTD) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Режим ТАК ДЕРЖАТЬ'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.RegRazg - arr.RegRazg) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Режим РАЗГОНА'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.RegNO - arr.RegNO) < 0) { QuerySQL = QuerySQL + "(3,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Режим НОРМАЛЬНОГО ОСТАНОВА'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.RegFO - arr.RegFO) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Режим ФОРСИРОВАННОГО ОСТАНОВА'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vip - arr.Vip) < 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Режим ВЫПУСКА'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Trazm - arr.Trazm) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Натяжение на разматывателе'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Trazm - arr.Trazm) < 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Отсутствие натяжения на разматывателе'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T10 - arr.T10) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Натяжение в 1 промежутке'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T10 - arr.T10) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Отсутствие натяжения в 1 промежутке '," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T20 - arr.T20) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Натяжение в 2 промежутке'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T20 - arr.T20) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Отсутствие натяжения в 2 промежутке '," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T30 - arr.T30) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Натяжение в 3 промежутке'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T30 - arr.T30) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Отсутствие натяжения в 3 промежутке '," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T40 - arr.T40) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Натяжение в 4 промежутке'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T40 - arr.T40) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Отсутствие натяжения в 4 промежутке '," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Tmot - arr.Tmot) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Натяжение на моталке'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Tmot - arr.Tmot) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Отсутствие натяжения на моталке '," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxSpeedPeregr - arr.MaxSpeedPeregr) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Максимальный перегруз'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxSpeedPeregr - arr.MaxSpeedPeregr) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Отсутствие максимального перегруза'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.UstavkaSpeed - arr.UstavkaSpeed) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Уставка рабочей скорости'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxSpeed - arr.MaxSpeed) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Перегруз по скорости'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnz12 - arr.Rnz12) > 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНЗ 12 включено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnz12 - arr.Rnz12) < 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНЗ 12 выключено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnz23 - arr.Rnz23) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНЗ 23 включено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnz23 - arr.Rnz23) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНЗ 23 выключено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnz34 - arr.Rnz34) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНЗ 34 включено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnz34 - arr.Rnz34) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНЗ 34 выключено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnz45 - arr.Rnz45) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНЗ 45 включено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnz45 - arr.Rnz45) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНЗ 45 выключено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.GrtVkl - arr.GrtVkl) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ГРТ включено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.GrtVkl - arr.GrtVkl) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ГРТ выключено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.TrtVkl - arr.TrtVkl) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ТРТ включено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.TrtVkl - arr.TrtVkl) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ТРТ выключено'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NalPol - arr.NalPol) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Наличие полосы в толщиномере за 5 клетью'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NalPol - arr.NalPol) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Отсутствие полосы в толщиномере за 5 клетью'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Knp - arr.Knp) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Ноль задания скорости'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Knp - arr.Knp) > 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Поехали'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.GotStan - arr.GotStan) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Сборка схемы стана'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.GotStan - arr.GotStan) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Развал схемы стана'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxV1 - arr.MaxV1) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Максимальная скорость клети 1'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxV1 - arr.MaxV1) > 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Конец максимальной скорости клети 1'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxV2 - arr.MaxV2) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Максимальная скорость клети 2'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxV2 - arr.MaxV2) > 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Конец максимальной скорости клети 2'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxV3 - arr.MaxV3) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Максимальная скорость клети 3'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxV3 - arr.MaxV3) > 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Конец максимальной скорости клети 3'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxV4 - arr.MaxV4) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Максимальная скорость клети 4'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxV4 - arr.MaxV4) > 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Конец максимальной скорости клети 4'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxV5 - arr.MaxV5) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Максимальная скорость клети 5'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.MaxV5 - arr.MaxV5) > 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Конец максимальной скорости клети 5'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.RKDVVkl - arr.RKDVVkl) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РКДВ включен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.RKDVVkl - arr.RKDVVkl) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РКДВ выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.RpvVkl - arr.RpvVkl) < 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РПВ включен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.RpvVkl - arr.RpvVkl) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РПВ выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnv12 - arr.Rnv12) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНВ12 включен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnv12 - arr.Rnv12) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНВ12 выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnv23 - arr.Rnv23) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНВ23 включен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnv23 - arr.Rnv23) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНВ23 выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnv34 - arr.Rnv34) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНВ34 включен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnv34 - arr.Rnv34) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РНВ34 выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnz45 - arr.Rnz45) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РН45 включен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rnz45 - arr.Rnz45) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РН45 выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rtv - arr.Rtv) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РТВ включен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Rtv - arr.Rtv) > 0) { QuerySQL = QuerySQL + "(7,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','РТВ выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Got_64kg - arr.Got_64kg) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Гидравлика 64 кг готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Got_64kg - arr.Got_64kg) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Гидравлика 64 кг не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Got_100kg - arr.Got_100kg) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Гидравлика 100 кг готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Got_100kg - arr.Got_100kg) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Гидравлика 100 кг не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g12 - arr.g12) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ПЖТ Ж-12 готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g12 - arr.g12) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ПЖТ Ж-12 не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.GotEmuls - arr.GotEmuls) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Эмульсионная система готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.GotEmuls - arr.GotEmuls) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Эмульсионная система не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NatshUsl - arr.NatshUsl) < 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Начальные условия'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g13 - arr.g13) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ПЖТ Ж-13 готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g13 - arr.g13) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ПЖТ Ж-13 не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g14 - arr.g14) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ПЖТ Ж-14 готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g14 - arr.g14) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ПЖТ Ж-14 не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g15 - arr.g15) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ПЖТ Ж-15 готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g15 - arr.g15) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ПЖТ Ж-15 не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LKmot - arr.LKmot) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК моталки включены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LKmot - arr.LKmot) > 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК моталки выключены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g16 - arr.g16) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Смазка Ж-16 готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g16 - arr.g16) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Смазка Ж-16 не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g17 - arr.g17) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Смазка Ж-17 готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g17 - arr.g17) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Смазка Ж-17 не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g18 - arr.g18) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Смазка Ж-18 готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g18 - arr.g18) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Смазка Ж-18 не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g19 - arr.g19) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Смазка Ж-19 готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g19 - arr.g19) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Смазка Ж-19 не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g20 - arr.g20) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Смазка Ж-20 готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.g20 - arr.g20) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Смазка Ж-20 не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Peregr1 - arr.Peregr1) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Перегруз клети 1'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Peregr1 - arr.Peregr1) > 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Конец перегруза клети 1'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Peregr2 - arr.Peregr2) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Перегруз клети 2'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Peregr2 - arr.Peregr2) > 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Конец перегруза клети 2'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Peregr3 - arr.Peregr3) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Перегруз клети 3'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Peregr3 - arr.Peregr3) > 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Конец перегруза клети 3'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Peregr4 - arr.Peregr4) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Перегруз клети 4'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Peregr4 - arr.Peregr4) > 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Конец перегруза клети 4'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Peregr5 - arr.Peregr5) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Перегруз клети 5'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Peregr5 - arr.Peregr5) > 0) { QuerySQL = QuerySQL + "(5,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Конец перегруза клети 5'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.temp_POU - arr.temp_POU) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Температура в ПОУ высокая'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.temp_POU - arr.temp_POU) > 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Температура в ПОУ нормальная'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.davl_redukt - arr.davl_redukt) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Давление редукторов низкое'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.davl_redukt - arr.davl_redukt) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Давление редукторов нормальное'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.davl_PGT - arr.davl_PGT) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Давление ПЖТ низкое'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.davl_PGT - arr.davl_PGT) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Давление ПЖТ нормальное'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LKrazm - arr.LKrazm) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК разматывателя включены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LKrazm - arr.LKrazm) > 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК разматывателя выключены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.temp_privod - arr.temp_privod) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентиляция готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.temp_privod - arr.temp_privod) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентиляция не готова'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.got_sinhr - arr.got_sinhr) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Синхронные двигатели готовы'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.got_sinhr - arr.got_sinhr) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Синхронные двигатели не готовы'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.OgragdMot - arr.OgragdMot) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Ограждение моталки закрыто'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.OgragdMot - arr.OgragdMot) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Ограждение моталки открыто НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.ZaxlestOtMot - arr.ZaxlestOtMot) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Захлестыватель у моталки НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.ZaxlestOtMot - arr.ZaxlestOtMot) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Захлестыватель отведен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOTempGP - arr.NOTempGP) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Высокая температура ПЖТ ГП'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOTempGP - arr.NOTempGP) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Нормальная температура ПЖТ ГП'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOSinxr - arr.NOSinxr) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Высокая температура ПЖТ СД'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOSinxr - arr.NOSinxr) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Нормальная температура ПЖТ СД'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOPanPultStar - arr.NOPanPultStar) < 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка НО на ПУ старшего нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOPanPultStar - arr.NOPanPultStar) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка НО на ПУР нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOPU1 - arr.NOPU1) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка НО на ПУ1 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOPU2 - arr.NOPU2) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка НО на ПУ2 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOPU3 - arr.NOPU3) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка НО на ПУ3 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOPU4 - arr.NOPU4) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка НО на ПУ4 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.NOPU5 - arr.NOPU5) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка НО на ПУ5 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.FOPanPultStar - arr.FOPanPultStar) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка ФО на ПУ старшего нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.FOPUR - arr.FOPUR) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка ФО на ПУР нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.FOPU1 - arr.FOPU1) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка ФО на ПУ1 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.FOPU2 - arr.FOPU2) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка ФО на ПУ2 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.FOPU3 - arr.FOPU3) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка ФО на ПУ3 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.FOPU4 - arr.FOPU4) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка ФО на ПУ4 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.FOPU5 - arr.FOPU5) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка ФО на ПУ5 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.AOPUR - arr.AOPUR) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка АО на ПУР нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.AOSUSknopka - arr.AOSUSknopka) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка АО на СУС нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.AO5klet - arr.AO5klet) > 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка АО на ПУ5 нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.TrazmProval - arr.TrazmProval) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Провал натяжения на разматывателе ФО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.TrazmProval - arr.TrazmProval) > 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Восстановление натяжения на разматывателе ТД'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T12proval - arr.T12proval) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Провал натяжения в 1 промежутке ФО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T12proval - arr.T12proval) > 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Восстановление натяжения в 1 промежутке ТД'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T23proval - arr.T23proval) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Провал натяжения в 2 промежутке ФО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T23proval - arr.T23proval) > 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Восстановление натяжения в 2 промежутке ТД'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T34proval - arr.T34proval) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Провал натяжения в 3 промежутке ФО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T34proval - arr.T34proval) > 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Восстановление натяжения в 3 промежутке ТД'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T45proval - arr.T45proval) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Провал натяжения в 4 промежутке ФО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.T45proval - arr.T45proval) > 0) { QuerySQL = QuerySQL + "(1,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Восстановление натяжения в 4 промежутке ТД'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LK1 - arr.LK1) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК клети 1 включены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LK1 - arr.LK1) > 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК клети 1 выключены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LK2 - arr.LK2) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК клети 2 включены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LK2 - arr.LK2) > 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК клети 2 выключены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LK3 - arr.LK3) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК клети 3 включены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LK3 - arr.LK3) > 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК клети 3 выключены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LK4 - arr.LK4) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК клети 4 включены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LK4 - arr.LK4) > 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК клети 4 выключены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LK5 - arr.LK5) < 0) { QuerySQL = QuerySQL + "(2,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК клети 5 включены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.LK5 - arr.LK5) > 0) { QuerySQL = QuerySQL + "(6,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ЛК клети 5 выключены'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.KnAOsus - arr.KnAOsus) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Кнопка АО на СУС нажата'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_101G - arr.Vent_101G) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува 101Г выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_102G - arr.Vent_102G) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува 102Г выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_103G - arr.Vent_103G) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува 103Г выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_105G - arr.Vent_105G) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува 105Г выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_106G - arr.Vent_106G) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува 106Г выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_112G - arr.Vent_112G) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува 112Г выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_111G - arr.Vent_111G) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува 111Г выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_110G - arr.Vent_110G) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува 110Г выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_108G - arr.Vent_108G) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува 108Г выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_107G - arr.Vent_107G) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува 107Г выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_podpor_PA1 - arr.Vent_podpor_PA1) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор подпора ПА-1 выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_podpor_PA2 - arr.Vent_podpor_PA2) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор подпора ПА-2 выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_1kl - arr.Vent_1kl) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува ГП 1 клети выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_2kl - arr.Vent_2kl) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува ГП 2 клети выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_3kl - arr.Vent_3kl) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува ГП 3 клети выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_4kl - arr.Vent_4kl) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува ГП 4 клети выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_5kl - arr.Vent_5kl) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува ГП 5 клети выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_podpor_GP1 - arr.Vent_podpor_GP1) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор подпора ГП-1 выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_podpor_GP2 - arr.Vent_podpor_GP2) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор подпора ГП-2 выключен'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Vent_NV - arr.Vent_NV) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','Вентилятор обдува нажимных винтов выключен НО'," + speed.ToString() + "),"; QueryCount++; }
                    if ((arr_minus.Dv2pr09sec3vv - arr.Dv2pr09sec3vv) < 0) { QuerySQL = QuerySQL + "(4,'" + dtMessage.ToString("yyyy-MM-dd HH:mm:ss.fff" ) + "','ХХХ ПЕРЕГРУЗ ГП ХХХ'," + speed.ToString() + "),"; QueryCount++; }




                    #endregion

                    #region БД

                    //'dtsave datetime NULL CONSTRAINT ' + BaseName + '_dtsave  DEFAULT (getdate()),' +

                    #region Проверяем создана ли таблица
                    string comBDMessage = "if not exists (select * from sysobjects where name='" + strTableName + "' and xtype='U') create table " + strTableName +
                                "(" +
                                "dtsave datetime NULL CONSTRAINT " + strTableName + "_dtsave  DEFAULT (getdate())," +
                                "number int NOT NULL, " +
                                "vremiamess datetime NOT NULL, " +
                                "note varchar(75) NOT NULL, " +
                                "speed float NOT NULL)";

                    //создаем таблицу сообщений стана 
                    using (SqlConnection con1Mess = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con1Mess.Open();
                            SqlCommand command = new SqlCommand(comBDMessage, con1Mess);
                            int WriteSQL = command.ExecuteNonQuery();
                            con1Mess.Close();
                            
                        }
                        catch (Exception)
                        {
                            Program.dtErrorSt200mc = DateTime.Now;
                            Program.messageErrorSt200mc = "Ошибка при создании таблицы Сообщений (" + strTableName + ")";
                        }

                    }
                    #endregion


                    #region Сохраняем данные в таблицу
                    

                    if (QuerySQL.Length > 0)
                    {
                        QuerySQL = QuerySQL.Substring(0, QuerySQL.Length - 1);
                        string SQLMessage = "INSERT INTO " + strTableName + " (number,vremiamess,note,speed) Values " + QuerySQL;

                        using (SqlConnection con2Mess = new SqlConnection(connectionString))
                        {
                            try
                            {
                                con2Mess.Open();
                                SqlCommand command = new SqlCommand(SQLMessage, con2Mess);
                                //command.Parameters.AddWithValue("@datetimeStanMess", DateTime.Now);
                                int WriteSQL = command.ExecuteNonQuery();

                                Program.intmessageOKSt200mc = Program.intmessageOKSt200mc + QueryCount;

                                
                                Program.messageOKSt200mc1 = Program.messageOKSt200mc1 + "(" + QueryCount + ")";
                                

                                con2Mess.Close();

                                arr_minus = arr;



                            }
                            catch (Exception ex)
                            {
                                Program.dtErrorSt200mc = DateTime.Now;
                                Program.messageErrorSt200mc = "Ошибка при ЗАПИСИ в таблицу Сообщений (" + strTableName + ")";

                            }
                        }
                    }

                    #endregion


                    #endregion

                    #endregion
                }

            }   
            catch (Exception ex)
            {
                Program.messageErrorSt200mc = "Global Error в модуле формирования сообщения " + ex.Message;
                Program.dtOKSt200mc = DateTime.Now;
                



            }
        }
        #endregion
    }
}
