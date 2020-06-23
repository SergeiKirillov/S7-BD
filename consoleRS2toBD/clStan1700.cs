using System;
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



        byte[] stanbuffer;          //данные c контроллера 100ms
        byte[] stanbufferPLC;       //Промежуточное хранение даных
        byte[] stanbufferSQL;       //Данные 101мс
        byte[] stanbufferMessage;   //Данные сообщений
        byte[] stanbufferMessageOld;//Данные сообщений
        byte[] stanbuffer1s;        //Технологические данные
        byte[] stanbufferNet;       //Передача по сети (визуализация)

        int stanamount = 315; //Размер буфера для принятия данных в байтах

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
        private string connectionString = "Data Source = 192.168.0.46; Initial Catalog = rs2stan1700; User ID = rs2admin; Password = 159951";
        private string numberTable;
        private float speed4kl;
        private float H5_work;
        private int B_Work;
        private float Ves_Work;
        private DateTime stanTimeStop;
        private float Dlina_Work;

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





        public void goStart()
        {

            //stan.Data101ms = stanData100ms;
            #region  dtStanPerevalki - формирование dataTable Перевалки(цикл  1s стана)
           
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

                //messageOKRulon = messageRulon;

                //Console.Clear();

                //#region Вывод на консоль

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

                //#endregion
            }
        }

        

        

       
        
        #region Соединение и прием данных с контроллера
        private void stanPLC()
        {
            try
            {
                int i = 0;     //начальная позиция по Top
                int y = 2;     //Конечная позиция по Top

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
                          
                            Program.messageError100mc = rs2.Error(res);
                            Program.dtError100mc = DateTime.Now;

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
                        Program.messageOK100mc = "Соединение активно";
                        Program.dtOK100mc = DateTime.Now;

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
                        Program.messageError100mc = "Ошибка чтения тегов c контроллера:"+rs2.Error(resultReadField);
                        Program.dtError100mc = DateTime.Now;
                    }

                }


            }
            catch (Exception ex)
            {
                /*все исключения кидаем в пустоту*/
                LogSystem.Write(stanNamePLC + " start -" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 1, 1, true);
                Program.messageError100mc = "Общая ошибка 100mc -" + ex.Message;
                Program.dtError100mc = DateTime.Now;

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
            try
            {
                while (true)
                {
                    Thread.Sleep(101);

                    #region Формируем SQL запрос с циклом 101мс и записываем его во временную БД

                    #region Если БД не существует то создаем
                    string comRulon101ms1 = "if not exists (select * from sysobjects where name ='TEMPstan101ms' and xtype='U') create table TEMPstan101ms " +
                       "(" +
                       "datetime101ms datetime , " +
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
                        conSQL101ms1.Open();
                        SqlCommand command = new SqlCommand(comRulon101ms1, conSQL101ms1);
                        command.ExecuteNonQuery();
                        conSQL101ms1.Close();

                    }
                    #endregion

                    if (blRulonStart)
                    {
                        
                        string comRulon101ms2 = "INSERT INTO TEMPstan101ms" +
                       "(datetime101ms,v1,v2,v3,v4,v5,h1,h5,b,dvip,drazm,dmot,vvip,d1,d2,d3,d4,d5,e2,e3,e4,e5,n1l,n1p,n2l,n2p,n3l,n3p,n4l,n4p,n5l,n5p,reserv1,reserv2,t1,t2,t3,t4,t1l,t2l,t3l,t4l,t1p,t2p,t3p,t4p,t1z,t2z,t3z,t4z,erazm,ivozbrazm,izadrazm,w1,w2v,w2n,w3v,w3n,w4v,w4n,w5v,w5n,wmot,imot,izadmot,u1,u2v,u2n,u3v,u3n,u4v,u4n,u5v,u5n,umot,i1,i2v,i2n,i3v,i3n,i4v,i4n,i5v,i5n,rtv,dt1,dt2,dt3,dt4,grt,trt,mv1,mv2,mv3,dh1,dh5,os1klvb,rezerv,mezdoza4)" +
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
                                    Program.messageOK101mc = "101мс во временную базу записана.";
                                    Program.dtOK101mc = DateTime.Now;
                                    conSQL101ms2.Close();
                                }
                                catch (Exception ex)
                                {
                                    Program.messageError101mc = "101mc НЕ ЗАПИСАНЫ. " + ex.Message + " Insert запрос: " + comRulon101ms2;
                                    Program.dtError101mc = DateTime.Now;
                                }
                            }   
                        }


                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {

                //LogSystem.Write(stanNamePLC + " SQL(101ms)-" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 0, 3, true);
                Program.messageError101mc = "Ошибка 101мс-" + ex.Message;
                Program.dtError101mc = DateTime.Now;
            }


        }
        #endregion

        #region Формируем и записываем данные  1c. Название таблицы формируется по принципу YYYYmmddW (W - смена(1 ночная(с 19-07), 2дневная(07-19)))
        private void stanSQL1s()
        {
            byte[] stanbuf1s = new byte[95];

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

                    if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 7 && Convert.ToInt32(DateTime.Now.ToString("HH")) < 19)
                    {
                        numberTable = DateTime.Now.ToString("yyyyMMdd") + "2";
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
                    stanbuf1s[84] = stanbuffer1s[308];          //RasxCD
                    stanbuf1s[85] = stanbuffer1s[24];          //D1_pred 
                    stanbuf1s[86] = stanbuffer1s[25];          //D1_pred
                    stanbuf1s[87] = stanbuffer1s[26];          //D2_pred 
                    stanbuf1s[88] = stanbuffer1s[27];          //D2_pred
                    stanbuf1s[89] = stanbuffer1s[28];          //D3_pred 
                    stanbuf1s[90] = stanbuffer1s[29];          //D3_pred
                    stanbuf1s[91] = stanbuffer1s[30];          //D4_pred 
                    stanbuf1s[92] = stanbuffer1s[31];          //D4_pred
                    stanbuf1s[93] = stanbuffer1s[32];          //D5_pred 
                    stanbuf1s[94] = stanbuffer1s[33];          //D5_pred




                  

                    #region Запись данных 1s

                    
                    string comBD = "if not exists (select * from sysobjects where name ='" + "Stan1s" + numberTable + "' and xtype='U') create table " + "Stan1s" + numberTable +
                       "(" +
                       "datetime1s datetime , " +
                       "s191HL int , " +
                       "s192HL int , " +
                       "s193BL int , " +
                       "s194BL int , " +
                       "s191HR int , " +
                       "s192HR int , " +
                       "s193BR int , " +
                       "s194BR int , " +
                       "s281NL int , " +
                       "s282NL int , " +
                       "s283BL int , " +
                       "s284BL int , " +
                       "s281NR int , " +
                       "s282NR int , " +
                       "s283BR int , " +
                       "s284BR int , " +
                       "s301BL int , " +
                       "s302BL int , " +
                       "s303HL int , " +
                       "s304HL int , " +
                       "s301BR int , " +
                       "s302BR int , " +
                       "s303HR int , " +
                       "s304HR int , " +
                       "s321BL int , " +
                       "s322BL int , " +
                       "s323HL int , " +
                       "s324HL int , " +
                       "s321BR int , " +
                       "s322BR int , " +
                       "s323HR int , " +
                       "s324HR int , " +
                       "s341BL int , " +
                       "s342BL int , " +
                       "s343HL int , " +
                       "s344HL int , " +
                       "s341BR int , " +
                       "s342BR int , " +
                       "s343HR int , " +
                       "s344HR int , " +
                       "s461L int , " +
                       "s462L int , " +
                       "s463L int , " +
                       "s461R int , " +
                       "s462R int , " +
                       "s463R int , " +
                       "sG11L int , " +
                       "sG12L int , " +
                       "sG13L int , " +
                       "sG14L int , " +
                       "sG15L int , " +
                       "sG16L int , " +
                       "sG17L int , " +
                       "sG11R int , " +
                       "sG12R int , " +
                       "sG13R int , " +
                       "sG14R int , " +
                       "sG15R int , " +
                       "sG16R int , " +
                       "sG17R int , " +
                       "sG21L int , " +
                       "sG22L int , " +
                       "sG23L int , " +
                       "sG24L int , " +
                       "sG25L int , " +
                       "sG26L int , " +
                       "sG27L int , " +
                       "sG21R int , " +
                       "sG22R int , " +
                       "sG23R int , " +
                       "sG24R int , " +
                       "sG25R int , " +
                       "sG26R int , " +
                       "sG27R int , " +
                       "sD12 float , " +
                       "sD13 float , " +
                       "sD14 float , " +
                       "sD15 float , " +
                       "sD16 float , " +
                       "sD17 float , " +
                       "sD18 float , " +
                       "sD19 float , " +
                       "sD20 float , " +
                       "sU64 int , " +
                       "sRasxCD int " +
                       ")";

                    using (SqlConnection conSQL1s1 = new SqlConnection(connectionString))
                    {
                        conSQL1s1.Open();
                        SqlCommand command = new SqlCommand(comBD, conSQL1s1);
                        command.ExecuteNonQuery();
                        conSQL1s1.Close();
                    }

                    #region В Insert используем передачу параметров через Переменную
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

                  string comRulon1s1 = "INSERT INTO " + "Stan1s" + numberTable +
                  " (datetime1s,s191HL,s192HL,s193BL,s194BL,s191HR,s192HR,s193BR,s194BR,s281NL,s282NL,s283BL,s284BL,s281NR,s282NR," +
                  "s283BR,s284BR,s301BL,s302BL,s303HL,s304HL,s301BR,s302BR,s303HR,s304HR,s321BL,s322BL,s323HL,s324HL,s321BR,s322BR," +
                  "s323HR,s324HR,s341BL,s342BL,s343HL,s344HL,s341BR,s342BR,s343HR,s344HR,s461L,s462L,s463L,s461R,s462R,s463R,sG11L," +
                  "sG12L,sG13L,sG14L,sG15L,sG16L,sG17L,sG11R,sG12R,sG13R,sG14R,sG15R,sG16R,sG17R,sG21L,sG22L,sG23L,sG24L,sG25L,sG26L," +
                  "sG27L,sG21R,sG22R,sG23R,sG24R,sG25R,sG26R,sG27R,sD12,sD13,sD14,sD15,sD16,sD17,sD18,sD19,sD20,sU64,sRasxCD) " +
                  "VALUES" +
                  " (" +
                  " @datetime1sStan, " +
                  BitConverter.ToInt16(stanbuf1s, 0) + "," +
                  BitConverter.ToInt16(stanbuf1s, 1) + "," +
                  BitConverter.ToInt16(stanbuf1s, 2) + "," +
                  BitConverter.ToInt16(stanbuf1s, 3) + "," +
                  BitConverter.ToInt16(stanbuf1s, 4) + "," +
                  BitConverter.ToInt16(stanbuf1s, 5) + "," +
                  BitConverter.ToInt16(stanbuf1s, 6) + "," +
                  BitConverter.ToInt16(stanbuf1s, 7) + "," +
                  BitConverter.ToInt16(stanbuf1s, 8) + "," +
                  BitConverter.ToInt16(stanbuf1s, 9) + "," +
                  BitConverter.ToInt16(stanbuf1s, 10) + "," +
                  BitConverter.ToInt16(stanbuf1s, 11) + "," +
                  BitConverter.ToInt16(stanbuf1s, 12) + "," +
                  BitConverter.ToInt16(stanbuf1s, 13) + "," +
                  BitConverter.ToInt16(stanbuf1s, 14) + "," +
                  BitConverter.ToInt16(stanbuf1s, 15) + "," +
                  BitConverter.ToInt16(stanbuf1s, 16) + "," +
                  BitConverter.ToInt16(stanbuf1s, 17) + "," +
                  BitConverter.ToInt16(stanbuf1s, 18) + "," +
                  BitConverter.ToInt16(stanbuf1s, 19) + "," +
                  BitConverter.ToInt16(stanbuf1s, 20) + "," +
                  BitConverter.ToInt16(stanbuf1s, 21) + "," +
                  BitConverter.ToInt16(stanbuf1s, 22) + "," +
                  BitConverter.ToInt16(stanbuf1s, 23) + "," +
                  BitConverter.ToInt16(stanbuf1s, 24) + "," +
                  BitConverter.ToInt16(stanbuf1s, 25) + "," +
                  BitConverter.ToInt16(stanbuf1s, 26) + "," +
                  BitConverter.ToInt16(stanbuf1s, 27) + "," +
                  BitConverter.ToInt16(stanbuf1s, 28) + "," +
                  BitConverter.ToInt16(stanbuf1s, 29) + "," +
                  BitConverter.ToInt16(stanbuf1s, 30) + "," +
                  BitConverter.ToInt16(stanbuf1s, 31) + "," +
                  BitConverter.ToInt16(stanbuf1s, 32) + "," +
                  BitConverter.ToInt16(stanbuf1s, 33) + "," +
                  BitConverter.ToInt16(stanbuf1s, 34) + "," +
                  BitConverter.ToInt16(stanbuf1s, 35) + "," +
                  BitConverter.ToInt16(stanbuf1s, 36) + "," +
                  BitConverter.ToInt16(stanbuf1s, 37) + "," +
                  BitConverter.ToInt16(stanbuf1s, 38) + "," +
                  BitConverter.ToInt16(stanbuf1s, 39) + "," +
                  BitConverter.ToInt16(stanbuf1s, 40) + "," +
                  BitConverter.ToInt16(stanbuf1s, 41) + "," +
                  BitConverter.ToInt16(stanbuf1s, 42) + "," +
                  BitConverter.ToInt16(stanbuf1s, 43) + "," +
                  BitConverter.ToInt16(stanbuf1s, 44) + "," +
                  BitConverter.ToInt16(stanbuf1s, 45) + "," +
                  BitConverter.ToInt16(stanbuf1s, 46) + "," +
                  BitConverter.ToInt16(stanbuf1s, 47) + "," +
                  BitConverter.ToInt16(stanbuf1s, 48) + "," +
                  BitConverter.ToInt16(stanbuf1s, 49) + "," +
                  BitConverter.ToInt16(stanbuf1s, 50) + "," +
                  BitConverter.ToInt16(stanbuf1s, 51) + "," +
                  BitConverter.ToInt16(stanbuf1s, 52) + "," +
                  BitConverter.ToInt16(stanbuf1s, 53) + "," +
                  BitConverter.ToInt16(stanbuf1s, 54) + "," +
                  BitConverter.ToInt16(stanbuf1s, 55) + "," +
                  BitConverter.ToInt16(stanbuf1s, 56) + "," +
                  BitConverter.ToInt16(stanbuf1s, 57) + "," +
                  BitConverter.ToInt16(stanbuf1s, 58) + "," +
                  BitConverter.ToInt16(stanbuf1s, 59) + "," +
                  BitConverter.ToInt16(stanbuf1s, 60) + "," +
                  BitConverter.ToInt16(stanbuf1s, 61) + "," +
                  BitConverter.ToInt16(stanbuf1s, 62) + "," +
                  BitConverter.ToInt16(stanbuf1s, 63) + "," +
                  BitConverter.ToInt16(stanbuf1s, 64) + "," +
                  BitConverter.ToInt16(stanbuf1s, 65) + "," +
                  BitConverter.ToInt16(stanbuf1s, 66) + "," +
                  BitConverter.ToInt16(stanbuf1s, 67) + "," +
                  BitConverter.ToInt16(stanbuf1s, 68) + "," +
                  BitConverter.ToInt16(stanbuf1s, 69) + "," +
                  BitConverter.ToInt16(stanbuf1s, 70) + "," +
                  BitConverter.ToInt16(stanbuf1s, 71) + "," +
                  BitConverter.ToInt16(stanbuf1s, 72) + "," +
                  BitConverter.ToInt16(stanbuf1s, 73) + "," +
                  (float)(BitConverter.ToInt16(stanbuf1s, 74)) / 10 + "," +
                  (float)(BitConverter.ToInt16(stanbuf1s, 75)) / 10 + "," +
                  (float)(BitConverter.ToInt16(stanbuf1s, 76)) / 10 + "," +
                  (float)(BitConverter.ToInt16(stanbuf1s, 77)) / 10 + "," +
                  (float)(BitConverter.ToInt16(stanbuf1s, 78)) / 10 + "," +
                  (float)(BitConverter.ToInt16(stanbuf1s, 79)) / 10 + "," +
                  (float)(BitConverter.ToInt16(stanbuf1s, 80)) / 10 + "," +
                  (float)(BitConverter.ToInt16(stanbuf1s, 81)) / 10 + "," +
                  (float)(BitConverter.ToInt16(stanbuf1s, 82)) / 10 + "," +
                  (int)(BitConverter.ToInt16(stanbuf1s, 83) * 10) + "," +
                  (int)(BitConverter.ToInt16(stanbuf1s, 84) * 10) +
                  ")";

                    using (SqlConnection conSQL1s2 = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conSQL1s2.Open();
                            SqlCommand command = new SqlCommand(comRulon1s1, conSQL1s2);
                            command.Parameters.AddWithValue("@datetime1sStan", DateTime.Now);
                            command.ExecuteNonQuery();
                            conSQL1s2.Close();
                            Program.messageOK1c = "Данные в БД(" + "Stan1s" + numberTable + ") 1s записаны";
                            Program.dtOK1c = DateTime.Now;
                        }
                        catch (Exception ex)
                        {

                            Program.messageError1c = "1s НЕ ЗАПИСАНЫ - " + ex.Message + " Insert запрос: " + comRulon1s1;
                            Program.dtError1c = DateTime.Now;
                        }



                    }
                    #endregion

                    #endregion

                    #region Расчет параметров прокатанного рулона после окончания прокатки

                    stanD_tek_mot = (float)(BitConverter.ToInt16(stanbuffer1s, 20));
                    #region  Время начало прокатки рулона

                    if (stanD_pred_mot == 0)
                    {
                        //при первом цикле все данные равны 0, поэтому мы выставляем значения параметров на 600
                        stanD_tek_mot = 600;
                        stanD_pred_mot = 600;
                    }

                    if (stanD_tek_mot > stanD_pred_mot)
                    {
                            if (stanD_pred_mot < 615)
                            {
                                stanTimeStart = DateTime.Now;
                                blRulonStart = true;
                            }

                    }
                    else
                    {
                          //blRulonStart = false;
                    }
                   
                   
                    #endregion

                    #region Толщина и ширина прокатываемого рулона
                    speed4kl = (float)(BitConverter.ToInt16(stanbuffer1s, 6)) / 100;
                    if ((stanTimeStart != new DateTime()) && (H5_work == 0) && (stanD_tek_mot > 700) && (speed4kl > 2))
                    {
                        H5_work = (float)(BitConverter.ToInt16(stanbuffer1s, 12)) / 1000;
                        B_Work = (int)BitConverter.ToInt16(stanbuffer1s, 14);
                    }
                    #endregion

                    #region Формирование сигнала окончания прокатки
                    if ((stanTimeStart != new DateTime()) && (H5_work != 0) && (stanD_tek_mot < 610) && (stanD_tek_mot < stanD_pred_mot))
                    {
                        Ves_Work = (((((stanD_pred_mot * stanD_pred_mot)/1000000 - 0.36F) * 3.141593F) / 4) * (B_Work / 1000)) * 7.85F;
                        stanTimeStop = DateTime.Now;
                        Dlina_Work = ((Ves_Work / 7.85F) / (B_Work / 1000)) / (H5_work / 1000);
                        blRulonStop = true;
                        blRulonStart = false;

                        //messageRulon = stanTimeStart.ToString("HH:mm:ss.fff") + " - " + stanTimeStop.ToString("HH:mm:ss.fff") + "   " + B_Work + "*" + H5_work + "=" + Ves_Work;


                        #region База производство

                        #region Создание БД

                        string comWorkStanCreate = "if not exists (select * from sysobjects where name ='work_stan' and xtype='U') create table work_stan" +
                       "(" +
                       "numberRulona bigint, " +
                       "start datetime , " +
                       "stop datetime , " +
                       "h5 float , " +
                       "b float , " +
                       "ves float , " +
                       "dlinna float , " +
                       "t1 float , " +
                       "t2 float , " +
                       "t3 float , " +
                       "t4 float , " +
                       "t5 float  " +

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
                        string comWorkStan = "INSERT INTO work_stan( " +
                            "numberRulona," +
                            "start," +
                            "stop," +
                            "h5," +
                            "b," +
                            "ves," +
                            "dlinna" +
                            ") " +
                            "VALUES(" +
                            "@NumberRulon, " +
                            "@TimeStart, " +
                            "@TimeStop, " +
                            "@H5_work, " +
                            "@B_Work, " +
                            "@Ves_Work, " +
                            "@Dlina_Work)";

                        string beginWork = stanTimeStart.ToString("ddMMyyyyHHmm");
                        string endWork = stanTimeStop.ToString("HHmm");
                        string strNumberRulona = beginWork + endWork;

                        //Добавляем в таблицу прокатанных рулонов данные по рулонам
                        using (SqlConnection con3 = new SqlConnection(connectionString))
                        {
                            try
                            {
                                con3.Open();
                                SqlCommand command = new SqlCommand(comWorkStan, con3);

                                command.Parameters.AddWithValue("@NumberRulon", strNumberRulona);

                                command.Parameters.AddWithValue("@TimeStart", stanTimeStart);
                                command.Parameters.AddWithValue("@TimeStop", stanTimeStop);
                                command.Parameters.AddWithValue("@H5_work", H5_work);
                                command.Parameters.AddWithValue("@B_Work", B_Work);
                                command.Parameters.AddWithValue("@Ves_Work", Ves_Work);
                                command.Parameters.AddWithValue("@Dlina_Work", Dlina_Work);

                                int WriteSQL = command.ExecuteNonQuery();

                                Program.messageOKProizvodstvo = strNumberRulona+"("+ stanTimeStart.ToString("HH:mm")+"-"+ stanTimeStop.ToString("HH:mm")+") "+ H5_work  +"*"+ B_Work + "->"+ Ves_Work;
                                //messageOKProizvodstvo = "производство";
                                Program.dtOKProizvodstvo = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                Program.messageErrorProizvodstvo = "Ошибка в сохранении данных о прокатанном рулоне " + ex.Message + " Insert запрос: " + comWorkStan;
                                Program.dtErrorProizvodstvo = DateTime.Now;

                            }
                            
                        }
                        #endregion

                        #endregion



                        #region //Очищаем базу временных рулонов
                        //using (SqlConnection conSQL1s3 = new SqlConnection(connectionString))
                        //{
                        //    conSQL1s3.Open();
                        //    string comRulon1s2 = "DELETE FROM TEMPstan101ms";
                        //    SqlCommand command = new SqlCommand(comRulon1s2, conSQL1s3);
                        //    command.ExecuteNonQuery();


                        //}
                        #endregion


                        #region Мпереименовываем временную базу в базу с именем stan100mc(дата+время начала)(время окончания)
                        using (SqlConnection conSQL1s3 = new SqlConnection(connectionString))
                        {
                            try
                            {
                                conSQL1s3.Open();
                                string begin = stanTimeStart.ToString("ddMMyyyyHHmm");
                                string end = stanTimeStop.ToString("HHmm");
                                string comRulon1s2 = "sp_rename 'TEMPstan101ms','" + begin + end + "'";
                                SqlCommand command = new SqlCommand(comRulon1s2, conSQL1s3);
                                command.ExecuteNonQuery();
                                Program.messageOKRulon = "Временная база -> " + begin + end;
                                Program.dtOKRulon = DateTime.Now;
                                conSQL1s3.Close();
                            }
                            catch (Exception ex)
                            {

                                Program.messageErrorRulon = "Временная база не переименована " + ex.Message;
                                Program.dtErrorRulon = DateTime.Now;
                            }
                            


                        }
                        #endregion

                    }
                    #endregion


                    #endregion

                    stanD_pred_mot = stanD_tek_mot;

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

                    try
                    {

                        if (d1_pred != d1)
                        {
                            blSave = true;
                            dtPerevalkiStan.Rows.Add(DateTime.Now, d1, 0, 0, 0, 0);
                        }
                        if (d2_pred != d2)
                        {
                            blSave = true;
                            dtPerevalkiStan.Rows.Add(DateTime.Now, 0, d2, 0, 0, 0);
                        }
                        if (d3_pred != d3)
                        {
                            blSave = true;
                            dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, d3, 0, 0);
                        }
                        if (d4_pred != d4)
                        {
                            blSave = true;
                            dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, 0, d4, 0);
                        }
                        if (d5_pred != d5)
                        {
                            blSave = true;
                            dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, 0, 0, d5);
                        }

                        //if (d1_pred != (int)BitConverter.ToInt16(stanbuffer1s, 24))
                        //{
                        //    blSave = true;
                        //    dtPerevalkiStan.Rows.Add(DateTime.Now, buf85, 0, 0, 0, 0);
                        //}
                        //if (d2_pred != BitConverter.ToInt16(stanbuffer1s, 26))
                        //{
                        //    blSave = true;
                        //    dtPerevalkiStan.Rows.Add(DateTime.Now, 0, buf87, 0, 0, 0);
                        //}
                        //if (d3_pred != BitConverter.ToInt16(stanbuffer1s, 28))
                        //{
                        //    blSave = true;
                        //    dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, buf89, 0, 0);
                        //}
                        //if (d4_pred != BitConverter.ToInt16(stanbuffer1s, 30))
                        //{
                        //    blSave = true;
                        //    dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, 0, buf91, 0);
                        //}
                        //if (d5_pred != BitConverter.ToInt16(stanbuffer1s, 32))
                        //{
                        //    blSave = true;
                        //    dtPerevalkiStan.Rows.Add(DateTime.Now, 0, 0, 0, 0, buf93);
                        //}
                    }
                    catch (Exception ex)
                    {
                        //ошибка
                        Program.messageErrorValki = "Ошибка формировании таблицы валков - "+ ex.Message;
                        Program.dtErrorValki = DateTime.Now; ;
                    }

                    

                    d1_pred = d1;
                    d2_pred = d2;
                    d3_pred = d3;
                    d4_pred = d4;
                    d5_pred = d5;



                    #region Перевалки сохраняем в БД
                    if (blSave)
                    {
                        string strTableNamePerevalki = "StanPerevalki" + DateTime.Now.ToString("yyyyMM");
                        string comBDPerevalki = "if not exists (select * from sysobjects where name='" + strTableNamePerevalki + "' and xtype='U') create table " + strTableNamePerevalki +
                                "(" +
                                "dtPerevalki datetime NOT NULL, " +
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
                                Program.messageErrorValki = "Ошибка формировании таблицы валков " + ex.Message;
                                Program.dtErrorValki = DateTime.Now;

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
                                    Program.messageOKValki = "Данные перевалки в таблицу " + strTableNamePerevalki + " записаны ";
                                    Program.dtOKValki = DateTime.Now;


                                    dtPerevalkiStan.Clear(); //очистка таблицы 
                                }
                                conPerevalki2.Close();

                                 

                            }
                            catch (Exception ex )
                            {
                                Program.messageErrorValki = "Ошибка записи в таблицу валков " + ex.Message;
                                Program.dtErrorValki = DateTime.Now;

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
                Program.messageError1c = "Ошибка глобальная - " + ex;
                Program.dtError1c = DateTime.Now;

            }
            
           
        }

        #endregion


        #region Формируем и записываем сообщения в Базу
        private void stanMessage200ms()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(200);

                    dtMessage = DateTime.Now;

                    float speed = (float)(BitConverter.ToInt16(stanbufferMessage, 16)) / 100;


                    int numberMessage = 0;
                    for (int i = 0; i < 15; i++)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            int z = Convert.ToInt32(Math.Pow(2,b));
                            if (((byte)(stanbufferMessageOld[i]&z)-(byte)(stanbufferMessage[i]&z))<0)
                            {
                                if(MessageStan[numberMessage].statusMenshe != 0)
                                {
                                    string mes = MessageStan[numberMessage].MinusMess;
                                    int status = MessageStan[numberMessage].statusMenshe;
                                    dtMessagestan.Rows.Add(dtMessage.ToString("HH:mm:ss.fff"), status, mes, speed);
                                }
                            }
                            else if (((byte)(stanbufferMessageOld[i] & z) - (byte)(stanbufferMessage[i] & z)) > 0)
                            {
                                if(MessageStan[numberMessage].statusBolshe != 0)
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
                    if (writeMessage>300)
                    {
                        writeMessage = 0;
                        string strTableName = "StanMessage"+numberTable;
                        string comBDMessage = "if not exists (select * from sysobjects where name='" + strTableName + "' and xtype='U') create table " + strTableName +
                                    "(" +
                                    "dtmes datetime NOT NULL, " +
                                    "status int NOT NULL, " +
                                    "message text NOT NULL, " +
                                    "speed float NOT NULL)";

                        //создаем таблицу сообщений стана 
                        using (SqlConnection con1Mess = new SqlConnection(connectionString))
                        {
                            con1Mess.Open();
                            SqlCommand command = new SqlCommand(comBDMessage, con1Mess);
                            int WriteSQL = command.ExecuteNonQuery();
                            con1Mess.Close();
                        }
                        if (dtMessagestan.Rows.Count > 0)
                        {
                            //записываем в таблицу прокатанного рулона данные по прокатке этого рулона
                            using (SqlConnection con2Mess = new SqlConnection(connectionString))
                            {
                                con2Mess.Open();
                                using (var bulkMessage = new SqlBulkCopy(con2Mess))
                                {
                                    bulkMessage.DestinationTableName = strTableName;
                                    bulkMessage.WriteToServer(dtMessagestan);
                                    Program.messageOK200mc="Кол-во сообщений записанных в таблицу " + strTableName + " равно " + dtMessagestan.Rows.Count;
                                    Program.dtOK200mc = dtMessage;
                                }
                            }
                            dtMessagestan.Clear();
                        }
                        else
                        {

                            Program.messageOK200mc = "Сообщений не было с " + DateTime.Now.AddMinutes(-1).ToString() + " по " + DateTime.Now.ToString();
                            Program.dtOK200mc = dtMessage;
                        }

                    }
                    else
                    {
                        writeMessage++;
                    }

                }

            }
            catch (Exception ex)
            {
                Program.messageError200mc = "Global Error в модуле формирования сообщения " + ex.Message;
                Program.dtOK200mc = DateTime.Now;


                
            }
        }
        #endregion
    }
}
