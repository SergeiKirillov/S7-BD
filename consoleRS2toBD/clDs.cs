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
    
    class clDs
    {
        

        
        Dictionary<string, ContData> DsData100ms = new Dictionary<string, ContData>
        {
            {"VKlet", new ContData(0,100,true)},    
            {"IzadR", new ContData(2,1,true)},      
            {"IzadM", new ContData(4,1,true)},      
            {"NKlet", new ContData(6,10,true)},     
            {"NRazm", new ContData(8,10,true)},     
            {"NMot", new ContData(10,10,true)},     
            {"TRazm", new ContData(12,10,true)},    //*
            {"TMot", new ContData(14,10,true)},     //*
            {"RRazm", new ContData(16,1,true)},     
            {"RMot", new ContData(18,1,true)},      
            {"NVlev", new ContData(20,100,true)},   
            {"NVpr", new ContData(22,100,true)},    
            {"IvozM", new ContData(48,100,true)},   
            {"Imot", new ContData(50,1,true)},      
            {"Urazm", new ContData(52,10,true)},    
            {"IvozR", new ContData(54,100,true)},   
            {"Umot", new ContData(56,10,true)},     
            {"IRUZ4", new ContData(128,1,true)},    
            {"IRUZ5", new ContData(130,1,true)},    
            {"IMUZ4", new ContData(132,1,true)},    
            {"IMUZ5", new ContData(134,1,true)},    
            {"IzovK", new ContData(136,100,true)},  
            {"Ukl", new ContData(138,10,true)},     
            {"IKUZ4", new ContData(140,1,true)},    
            {"IKUZ5", new ContData(142,1,true)},    
            {"ObgTek", new ContData(144,100,true)}, 
            {"DatObgDo", new ContData(146,1,true)}, 
            {"DatObgZa", new ContData(148,1,true)}   

            
        };

        Dictionary<int, MessageClass> MessageDs = new Dictionary<int, MessageClass>()
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



            byte[] dsbuffer;          //данные c контроллера 100ms
            byte[] dsbufferPLC;       //Промежуточное хранение даных
            byte[] dsbufferSQL;       //Данные 101мс
            byte[] dsbufferMessage;   //Данные сообщений
            byte[] dsbufferMessageOld;//Данные сообщений
            byte[] dsbuffer1s;        //Технологические данные
            byte[] dsbufferNet;       //Передача по сети (визуализация)

            int dsmount = 315; //Размер буфера для принятия данных в байтах

            byte[] dsIPconnPLC = new byte[] { 192, 168, 0, 21 }; //Передаем адресс контроллера
            int dsconnect = 1;

            double dsdMot = 0.615;

            string dsNamePLC = "Дрессировочный стан";
            int dsSlotconnPC = 2;
            int dsRackconnPC = 0;

            int dsStartAdressTag = 2000; //старт адресов с 3000

            readonly object dslocker1 = new object();
            readonly object dslocker2 = new object();
            readonly object dslocker3 = new object();
            readonly object dslocker4 = new object();

            float dsspeed4kl, dsH_work, dshw, dsBw, dsD_tek_mot, dsB_Work, dsD_pred_mot = 0, dsVes_Work, dsDlina_Work;

            //DataTable dtds101ms;

            bool bldsRulonProkatSaveInData101ms;
            private DateTime dsTimeStart;
            private bool blRulonStart = false;
            private string messageRulon;
            private bool blRulonStop = false;
            private string connectionString = "Data Source = 192.168.0.46; Initial Catalog = rs2ds; User ID = rs2admin; Password = 159951";
            private string numberTable;
            private float speed4kl;
            private float H5_work;
            private int B_Work;
            private float Ves_Work;
            private DateTime dsTimeStop;
            private float Dlina_Work;

            

            private int d1_pred;
            private int d2_pred;
            private int d3_pred;
            private int d4_pred;
            private int d5_pred;
            DataTable dtPerevalkids;
            DataTable dtMessageds;
            DateTime dtMessage;
            int writeMessage = 0; //цикл сохранением значений Message





            public void goStart()
            {

                //ds.Data101ms = dsData100ms;
                #region  dtdsPerevalki - формирование dataTable Перевалки(цикл  1s стана)

                dtPerevalkids = new DataTable();
                dtPerevalkids.Columns.Add("dtPerevalki", typeof(DateTime));
                dtPerevalkids.Columns.Add("kl1", typeof(int));
                dtPerevalkids.Columns.Add("kl2", typeof(int));
                dtPerevalkids.Columns.Add("kl3", typeof(int));
                dtPerevalkids.Columns.Add("kl4", typeof(int));
                dtPerevalkids.Columns.Add("kl5", typeof(int));

                #endregion

                #region dtMessageds - формирование DataTable Message(таблица сообщений стана)
                dtMessageds = new DataTable();
                dtMessageds.Columns.Add("dtmes", typeof(DateTime));
                dtMessageds.Columns.Add("status", typeof(int));
                dtMessageds.Columns.Add("message", typeof(string));
                dtMessageds.Columns.Add("speed", typeof(float));
                #endregion


                Thread queryPLC = new Thread(dsPLC);
                queryPLC.Start();

                //Thread querySQL = new Thread(dsSQL101ms);
                //querySQL.Start();

                //Thread queryMes = new Thread(dsMessage200ms);
                //queryMes.Start();

                //Thread query1s = new Thread(dsSQL1s);
                //query1s.Start();

                while (true)
                {
                    Thread.Sleep(5000);

                   
                }
            }







            #region Соединение и прием данных с контроллера
            private void dsPLC()
            {
                try
                {
                    int i = 0;     //начальная позиция по Top
                    int y = 2;     //Конечная позиция по Top

                    Prodave rs2 = new Prodave();

                    dsbuffer = new byte[dsmount];
                    dsbufferPLC = new byte[dsmount];
                    dsbufferSQL = new byte[dsmount];
                    dsbuffer1s = new byte[dsmount];
                    dsbufferMessage = new byte[22];
                    dsbufferMessageOld = new byte[22];


                    int resultReadField = 5;


                    while (true)
                    {
                        Thread.Sleep(100);

                        if (resultReadField != 0)
                        {
                            int res = rs2.LoadConnection(dsconnect, 2, dsIPconnPLC, dsSlotconnPC, dsRackconnPC);

                            if (res != 0)
                            {
                                Program.messageErrorDs100mc = rs2.Error(res);
                                Program.dtErrorDs100mc = DateTime.Now;

                            }
                            else
                            {
                                int resSAC = rs2.SetActiveConnection(dsconnect);
                            }

                        }

                        int Byte_Col_r = 0;

                        resultReadField = rs2.field_read('M', 0, dsStartAdressTag, dsmount, out dsbuffer, out Byte_Col_r);

                        if (resultReadField == 0)
                        {
                            
                            Program.messageOKDs100mc = "Соединение активно";
                            Program.dtOKDs100mc = DateTime.Now;

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
                            rs2.UnloadConnection(dsconnect);
                            //LogSystem.Write(dsNamePLC + " 100ms", Direction.ERROR, "Error.Read fied PLC. " + rs2.Error(resultReadField), 1, 1, true);
                            Program.messageErrorDs100mc = "Ошибка чтения тегов c контроллера:" + rs2.Error(resultReadField);
                            Program.dtErrorDs100mc = DateTime.Now;
                        }

                    }


                }
                catch (Exception ex)
                {
                    /*все исключения кидаем в пустоту*/
                    LogSystem.Write(dsNamePLC + " start -" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 1, 1, true);
                    Program.messageErrorDs100mc = "Общая ошибка 100mc -" + ex.Message;
                    Program.dtErrorDs100mc = DateTime.Now;

                }
            }



            void BufferToBuffer()
            {
                //критичная секция которая записывает значение в bufferPLC
                lock (dslocker1)
                {

                    //Array.Clear(bufferPLC, 0, bufferPLC.Length);
                    dsbufferPLC = dsbuffer;
                }

            }


            void BufferSQLToBufferPLC()
            {
                //критичная секция которая записывает значение в bufferPLC
                lock (dslocker2)
                {
                    //Array.Clear(bufferSQL, 0, bufferSQL.Length);
                    dsbufferSQL = dsbufferPLC;
                }

            }

            void Buffer1cToBufferPLC()
            {
                lock (dslocker3)
                {
                    dsbuffer1s = dsbufferPLC;


                }
            }

            private void BufferMessageToBufferPLC()
            {
                //критичная секция которая записывает значение в bufferPLC
                //У миши Богуша в delphi программе биты переставлялись с помощью оператора swap и он истользовал не byte, а int
                //У меня используется везьде byte поэтому я просто поменял биты при формировании buffer

                lock (dslocker4)
                {
                    dsbufferMessageOld[0] = dsbufferMessage[0];
                    dsbufferMessageOld[1] = dsbufferMessage[1];
                    dsbufferMessageOld[2] = dsbufferMessage[2];
                    dsbufferMessageOld[3] = dsbufferMessage[3];
                    dsbufferMessageOld[4] = dsbufferMessage[4];
                    dsbufferMessageOld[5] = dsbufferMessage[5];
                    dsbufferMessageOld[6] = dsbufferMessage[6];
                    dsbufferMessageOld[7] = dsbufferMessage[7];
                    dsbufferMessageOld[8] = dsbufferMessage[8];
                    dsbufferMessageOld[9] = dsbufferMessage[9];
                    dsbufferMessageOld[10] = dsbufferMessage[10];
                    dsbufferMessageOld[11] = dsbufferMessage[11];
                    dsbufferMessageOld[12] = dsbufferMessage[12];
                    dsbufferMessageOld[13] = dsbufferMessage[13];
                    dsbufferMessageOld[14] = dsbufferMessage[14];
                    dsbufferMessageOld[15] = dsbufferMessage[15];
                    dsbufferMessageOld[16] = dsbufferMessage[16];
                    dsbufferMessageOld[17] = dsbufferMessage[17];
                    dsbufferMessageOld[18] = dsbufferMessage[18];
                    dsbufferMessageOld[19] = dsbufferMessage[19];
                    dsbufferMessageOld[20] = dsbufferMessage[20];
                    dsbufferMessageOld[21] = dsbufferMessage[21];


                    dsbufferMessage[0] = dsbufferPLC[67];
                    dsbufferMessage[1] = dsbufferPLC[66];
                    dsbufferMessage[2] = dsbufferPLC[69];
                    dsbufferMessage[3] = dsbufferPLC[68];
                    dsbufferMessage[4] = dsbufferPLC[71];
                    dsbufferMessage[5] = dsbufferPLC[70];
                    dsbufferMessage[6] = dsbufferPLC[103];
                    dsbufferMessage[7] = dsbufferPLC[102];
                    dsbufferMessage[8] = dsbufferPLC[105];
                    dsbufferMessage[9] = dsbufferPLC[104];
                    dsbufferMessage[10] = dsbufferPLC[107];
                    dsbufferMessage[11] = dsbufferPLC[106];
                    dsbufferMessage[12] = dsbufferPLC[109];
                    dsbufferMessage[13] = dsbufferPLC[108];
                    dsbufferMessage[14] = dsbufferPLC[111];
                    dsbufferMessage[15] = dsbufferPLC[110];
                    dsbufferMessage[16] = dsbufferPLC[6];   //speed 4kl
                    dsbufferMessage[17] = dsbufferPLC[7];
                    dsbufferMessage[18] = dsbufferPLC[312];
                    dsbufferMessage[19] = dsbufferPLC[313];
                    dsbufferMessage[20] = dsbufferPLC[310];
                    dsbufferMessage[21] = dsbufferPLC[311];

                }

            }


            #endregion


            #region Запись данных(101ms) с контроллера в Базу Данных

            private void dsSQL101ms()
            {
                try
                {
                    while (true)
                    {
                        Thread.Sleep(101);

                        #region Формируем SQL запрос с циклом 101мс и записываем его во временную БД

                        #region Если БД не существует то создаем
                        string comRulon101ms1 = "if not exists (select * from sysobjects where name ='TEMPds101ms' and xtype='U') create table TEMPds101ms " +
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

                            string comRulon101ms2 = "INSERT INTO TEMPds101ms" +
                           "(datetime101ms,v1,v2,v3,v4,v5,h1,h5,b,dvip,drazm,dmot,vvip,d1,d2,d3,d4,d5,e2,e3,e4,e5,n1l,n1p,n2l,n2p,n3l,n3p,n4l,n4p,n5l,n5p,reserv1,reserv2,t1,t2,t3,t4,t1l,t2l,t3l,t4l,t1p,t2p,t3p,t4p,t1z,t2z,t3z,t4z,erazm,ivozbrazm,izadrazm,w1,w2v,w2n,w3v,w3n,w4v,w4n,w5v,w5n,wmot,imot,izadmot,u1,u2v,u2n,u3v,u3n,u4v,u4n,u5v,u5n,umot,i1,i2v,i2n,i3v,i3n,i4v,i4n,i5v,i5n,rtv,dt1,dt2,dt3,dt4,grt,trt,mv1,mv2,mv3,dh1,dh5,os1klvb,rezerv,mezdoza4)" +
                           " VALUES " +
                           "('" +
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'," +
                            (float)(BitConverter.ToInt16(dsbufferSQL, 0)) / 100 + "," +    //v1
                            (float)(BitConverter.ToInt16(dsbufferSQL, 2)) / 100 + "," +    //v2
                            (float)(BitConverter.ToInt16(dsbufferSQL, 4)) / 100 + "," +    //v3
                            (float)(BitConverter.ToInt16(dsbufferSQL, 6)) / 100 + "," +    //v4
                            (float)(BitConverter.ToInt16(dsbufferSQL, 8)) / 100 + "," +    //v5
                            (float)(BitConverter.ToInt16(dsbufferSQL, 10)) / 1000 + "," +  //h1    
                            (float)(BitConverter.ToInt16(dsbufferSQL, 12)) / 1000 + "," +  //h5
                            BitConverter.ToInt16(dsbufferSQL, 14) + "," +                  //b
                            (float)(BitConverter.ToInt16(dsbufferSQL, 16)) / 1000 + "," +  //dvip
                            (float)(BitConverter.ToInt16(dsbufferSQL, 18)) / 1000 + "," +  //drazm
                            (float)(BitConverter.ToInt16(dsbufferSQL, 20)) / 1000 + "," +  //dmot
                            (float)(BitConverter.ToInt16(dsbufferSQL, 22)) / 1000 + "," +  //vvip
                            BitConverter.ToInt16(dsbufferSQL, 24) + "," +                  //d1
                            BitConverter.ToInt16(dsbufferSQL, 26) + "," +                  //d2
                            BitConverter.ToInt16(dsbufferSQL, 28) + "," +                  //d3
                            BitConverter.ToInt16(dsbufferSQL, 30) + "," +                  //d4
                            BitConverter.ToInt16(dsbufferSQL, 32) + "," +                  //d5
                            (float)(BitConverter.ToInt16(dsbufferSQL, 34)) / 100 + "," +   //e2
                            (float)(BitConverter.ToInt16(dsbufferSQL, 36)) / 100 + "," +   //e3
                            (float)(BitConverter.ToInt16(dsbufferSQL, 38)) / 100 + "," +   //e4
                            (float)(BitConverter.ToInt16(dsbufferSQL, 40)) / 100 + "," +   //e5
                            (float)(BitConverter.ToInt16(dsbufferSQL, 42)) / 100 + "," +   //n1l
                            (float)(BitConverter.ToInt16(dsbufferSQL, 44)) / 100 + "," +   //n1p
                            (float)(BitConverter.ToInt16(dsbufferSQL, 46)) / 100 + "," +   //n2l
                            (float)(BitConverter.ToInt16(dsbufferSQL, 48)) / 100 + "," +   //n2p
                            (float)(BitConverter.ToInt16(dsbufferSQL, 50)) / 100 + "," +   //n3l
                            (float)(BitConverter.ToInt16(dsbufferSQL, 52)) / 100 + "," +   //n3p
                            (float)(BitConverter.ToInt16(dsbufferSQL, 54)) / 100 + "," +   //n4l
                            (float)(BitConverter.ToInt16(dsbufferSQL, 56)) / 100 + "," +   //n4p
                            (float)(BitConverter.ToInt16(dsbufferSQL, 58)) / 100 + "," +   //n5l
                            (float)(BitConverter.ToInt16(dsbufferSQL, 60)) / 100 + "," +   //n5p
                            (float)(BitConverter.ToInt16(dsbufferSQL, 68)) / 100 + "," +   //reserv1
                            (float)(BitConverter.ToInt16(dsbufferSQL, 70)) / 100 + "," +   //reserv2
                            (float)(BitConverter.ToInt16(dsbufferSQL, 72)) / 100 + "," +   //t1
                            (float)(BitConverter.ToInt16(dsbufferSQL, 74)) / 100 + "," +   //t2
                            (float)(BitConverter.ToInt16(dsbufferSQL, 76)) / 100 + "," +   //t3
                            (float)(BitConverter.ToInt16(dsbufferSQL, 78)) / 100 + "," +   //t4
                            (float)(BitConverter.ToInt16(dsbufferSQL, 80)) / 100 + "," +   //t1l
                            (float)(BitConverter.ToInt16(dsbufferSQL, 82)) / 100 + "," +   //t2l
                            (float)(BitConverter.ToInt16(dsbufferSQL, 84)) / 100 + "," +   //t3l
                            (float)(BitConverter.ToInt16(dsbufferSQL, 86)) / 100 + "," +   //t4l
                            (float)(BitConverter.ToInt16(dsbufferSQL, 88)) / 100 + "," +   //t1p
                            (float)(BitConverter.ToInt16(dsbufferSQL, 90)) / 100 + "," +   //t2p
                            (float)(BitConverter.ToInt16(dsbufferSQL, 92)) / 100 + "," +   //t3p
                            (float)(BitConverter.ToInt16(dsbufferSQL, 94)) / 100 + "," +   //t4p
                            (float)(BitConverter.ToInt16(dsbufferSQL, 96)) / 100 + "," +   //t1z
                            (float)(BitConverter.ToInt16(dsbufferSQL, 98)) / 100 + "," +   //t2z
                            (float)(BitConverter.ToInt16(dsbufferSQL, 100)) / 100 + "," +  //t3z
                            (float)(BitConverter.ToInt16(dsbufferSQL, 112)) / 100 + "," +  //t4z
                            (float)(BitConverter.ToInt16(dsbufferSQL, 114)) / 10 + "," +   //erazm
                            (float)(BitConverter.ToInt16(dsbufferSQL, 116)) / 100 + "," +  //ivozbrazm
                            (float)(BitConverter.ToInt16(dsbufferSQL, 118)) / 10 + "," +   //izadrazm 
                            (float)(BitConverter.ToInt16(dsbufferSQL, 120)) / 10 + "," +   //w1
                            (float)(BitConverter.ToInt16(dsbufferSQL, 122)) / 10 + "," +   //w2v
                            (float)(BitConverter.ToInt16(dsbufferSQL, 124)) / 10 + "," +   //w2n
                            (float)(BitConverter.ToInt16(dsbufferSQL, 126)) / 10 + "," +   //w3v
                            (float)(BitConverter.ToInt16(dsbufferSQL, 128)) / 10 + "," +   //w3n
                            (float)(BitConverter.ToInt16(dsbufferSQL, 130)) / 10 + "," +   //w4v
                            (float)(BitConverter.ToInt16(dsbufferSQL, 132)) / 10 + "," +   //w4n
                            (float)(BitConverter.ToInt16(dsbufferSQL, 134)) / 10 + "," +   //w5v
                            (float)(BitConverter.ToInt16(dsbufferSQL, 136)) / 10 + "," +   //w5n
                            (float)(BitConverter.ToInt16(dsbufferSQL, 138)) / 10 + "," +   //wmot
                            BitConverter.ToInt16(dsbufferSQL, 140) + "," +                 //imot
                            BitConverter.ToInt16(dsbufferSQL, 142) + "," +                 //izadmot
                            (float)(BitConverter.ToInt16(dsbufferSQL, 144)) / 10 + "," +   //u1
                            (float)(BitConverter.ToInt16(dsbufferSQL, 146)) / 10 + "," +   //u2v
                            (float)(BitConverter.ToInt16(dsbufferSQL, 148)) / 10 + "," +   //u2n
                            (float)(BitConverter.ToInt16(dsbufferSQL, 150)) / 10 + "," +   //u3v
                            (float)(BitConverter.ToInt16(dsbufferSQL, 152)) / 10 + "," +   //u3n
                            (float)(BitConverter.ToInt16(dsbufferSQL, 154)) / 10 + "," +   //u4v
                            (float)(BitConverter.ToInt16(dsbufferSQL, 156)) / 10 + "," +   //u4n
                            (float)(BitConverter.ToInt16(dsbufferSQL, 158)) / 10 + "," +   //u5v
                            (float)(BitConverter.ToInt16(dsbufferSQL, 160)) / 10 + "," +   //u5n
                            (float)(BitConverter.ToInt16(dsbufferSQL, 162)) / 10 + "," +   //umot
                            BitConverter.ToInt16(dsbufferSQL, 164) + "," +                 //i1
                            BitConverter.ToInt16(dsbufferSQL, 166) + "," +                 //i2v
                            BitConverter.ToInt16(dsbufferSQL, 168) + "," +                 //i2n
                            BitConverter.ToInt16(dsbufferSQL, 170) + "," +                 //i3v
                            BitConverter.ToInt16(dsbufferSQL, 172) + "," +                 //i3n
                            BitConverter.ToInt16(dsbufferSQL, 174) + "," +                 //i4v
                            BitConverter.ToInt16(dsbufferSQL, 176) + "," +                 //i4n
                            BitConverter.ToInt16(dsbufferSQL, 178) + "," +                 //i5v
                            BitConverter.ToInt16(dsbufferSQL, 180) + "," +                 //i5n
                            (float)(BitConverter.ToInt16(dsbufferSQL, 192)) / 10 + "," +   //rtv
                            (float)(BitConverter.ToInt16(dsbufferSQL, 194)) / 10 + "," +   //dt1
                            (float)(BitConverter.ToInt16(dsbufferSQL, 196)) / 10 + "," +   //dt2
                            (float)(BitConverter.ToInt16(dsbufferSQL, 198)) / 10 + "," +   //dt3
                            (float)(BitConverter.ToInt16(dsbufferSQL, 200)) / 10 + "," +   //dt4
                            (float)(BitConverter.ToInt16(dsbufferSQL, 202)) / 10 + "," +   //grt
                            (float)(BitConverter.ToInt16(dsbufferSQL, 204)) / 10 + "," +   //trt
                            (float)(BitConverter.ToInt16(dsbufferSQL, 206)) / 10 + "," +   //mv1
                            (float)(BitConverter.ToInt16(dsbufferSQL, 208)) / 10 + "," +   //mv2
                            (float)(BitConverter.ToInt16(dsbufferSQL, 210)) / 10 + "," +   //mv3
                            (float)(BitConverter.ToInt16(dsbufferSQL, 62)) / 10 + "," +    //dh1
                            (float)(BitConverter.ToInt16(dsbufferSQL, 64)) / 10 + "," +    //dh5
                            BitConverter.ToInt16(dsbufferSQL, 216) + "," +                 //os1klvb
                            BitConverter.ToInt16(dsbufferSQL, 218) + "," +                 //rezerv
                            BitConverter.ToInt16(dsbufferSQL, 220) +                       //mezdoza4
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
                                        Program.messageOKDs101mc = "101мс во временную базу записана.";
                                        Program.dtOKDs101mc = DateTime.Now;
                                        conSQL101ms2.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        Program.messageErrorDs101mc = "101mc НЕ ЗАПИСАНЫ. " + ex.Message + " Insert запрос: " + comRulon101ms2;
                                        Program.dtErrorDs101mc = DateTime.Now;
                                    }
                                }
                            }


                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {

                    //LogSystem.Write(dsNamePLC + " SQL(101ms)-" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 0, 3, true);
                    Program.messageErrorDs101mc = "Ошибка 101мс-" + ex.Message;
                    Program.dtErrorDs101mc = DateTime.Now;
                }


            }
            #endregion

            #region Формируем и записываем данные  1c. Название таблицы формируется по принципу YYYYmmddW (W - смена(1 ночная(с 19-07), 2дневная(07-19)))
            private void dsSQL1s()
            {
                byte[] dsbuf1s = new byte[95];

                
                try
                {
                    while (true)
                    {
                        Thread.Sleep(1000);


                        #region Формируем шифр таблицы numberTable = yyyyMMddсмена

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


                        dsbuf1s[0] = dsbuffer1s[224];           //191HL
                        dsbuf1s[1] = dsbuffer1s[225];           //192HL
                        dsbuf1s[2] = dsbuffer1s[226];           //193BL
                        dsbuf1s[3] = dsbuffer1s[227];           //194BL"
                        dsbuf1s[4] = dsbuffer1s[228];           //191HR"
                        dsbuf1s[5] = dsbuffer1s[229];           //192HR"
                        dsbuf1s[6] = dsbuffer1s[230];           //193BR"
                        dsbuf1s[7] = dsbuffer1s[231];           //194BR"
                        dsbuf1s[8] = dsbuffer1s[232];           //281NL"
                        dsbuf1s[9] = dsbuffer1s[233];           //282NL"
                        dsbuf1s[10] = dsbuffer1s[234];          //283BL"
                        dsbuf1s[11] = dsbuffer1s[235];          //284BL"
                        dsbuf1s[12] = dsbuffer1s[236];          //281NR"
                        dsbuf1s[13] = dsbuffer1s[237];          //282NR"
                        dsbuf1s[14] = dsbuffer1s[238];          //283BR"
                        dsbuf1s[15] = dsbuffer1s[239];          //284BR"
                        dsbuf1s[16] = dsbuffer1s[240];          //301BL"
                        dsbuf1s[17] = dsbuffer1s[241];          //302BL"
                        dsbuf1s[18] = dsbuffer1s[242];          //303HL"
                        dsbuf1s[19] = dsbuffer1s[243];          //304HL"
                        dsbuf1s[20] = dsbuffer1s[244];          //301BR"
                        dsbuf1s[21] = dsbuffer1s[245];          //302BR"
                        dsbuf1s[22] = dsbuffer1s[246];          //303HR"
                        dsbuf1s[23] = dsbuffer1s[247];          //304HR"
                        dsbuf1s[24] = dsbuffer1s[248];          //321BL"
                        dsbuf1s[25] = dsbuffer1s[249];          //322BL"
                        dsbuf1s[26] = dsbuffer1s[250];          //323HL"
                        dsbuf1s[27] = dsbuffer1s[251];          //324HL"
                        dsbuf1s[28] = dsbuffer1s[252];          //321BR"
                        dsbuf1s[29] = dsbuffer1s[253];          //322BR"
                        dsbuf1s[30] = dsbuffer1s[254];          //323HR"
                        dsbuf1s[31] = dsbuffer1s[255];          //324HR"
                        dsbuf1s[32] = dsbuffer1s[256];          //341BL"
                        dsbuf1s[33] = dsbuffer1s[257];          //342BL"
                        dsbuf1s[34] = dsbuffer1s[258];          //343HL"
                        dsbuf1s[35] = dsbuffer1s[259];          //344HL"
                        dsbuf1s[36] = dsbuffer1s[260];          //341BR"
                        dsbuf1s[37] = dsbuffer1s[261];          //342BR"
                        dsbuf1s[38] = dsbuffer1s[262];          //343HR"
                        dsbuf1s[39] = dsbuffer1s[263];          //344HR"
                        dsbuf1s[40] = dsbuffer1s[264];          //461L",
                        dsbuf1s[41] = dsbuffer1s[265];          //462L",
                        dsbuf1s[42] = dsbuffer1s[266];          //463L",
                        dsbuf1s[43] = dsbuffer1s[267];          //461R",
                        dsbuf1s[44] = dsbuffer1s[268];          //462R",
                        dsbuf1s[45] = dsbuffer1s[269];          //463R",
                        dsbuf1s[46] = dsbuffer1s[270];          //G11L",
                        dsbuf1s[47] = dsbuffer1s[271];          //G12L",
                        dsbuf1s[48] = dsbuffer1s[272];          //G13L",
                        dsbuf1s[49] = dsbuffer1s[273];          //G14L",
                        dsbuf1s[50] = dsbuffer1s[274];          //G15L",
                        dsbuf1s[51] = dsbuffer1s[275];          //G16L",
                        dsbuf1s[52] = dsbuffer1s[276];          //G17L",
                        dsbuf1s[53] = dsbuffer1s[277];          //G11R",
                        dsbuf1s[54] = dsbuffer1s[278];          //G12R",
                        dsbuf1s[55] = dsbuffer1s[279];          //G13R",
                        dsbuf1s[56] = dsbuffer1s[280];          //G14R",
                        dsbuf1s[57] = dsbuffer1s[281];          //G15R",
                        dsbuf1s[58] = dsbuffer1s[282];          //G16R",
                        dsbuf1s[59] = dsbuffer1s[283];          //G17R",
                        dsbuf1s[60] = dsbuffer1s[284];          //G21L",
                        dsbuf1s[61] = dsbuffer1s[285];          //G22L",
                        dsbuf1s[62] = dsbuffer1s[286];          //G23L",
                        dsbuf1s[63] = dsbuffer1s[287];          //G24L",
                        dsbuf1s[64] = dsbuffer1s[288];          //G25L",
                        dsbuf1s[65] = dsbuffer1s[289];          //G26L",
                        dsbuf1s[66] = dsbuffer1s[290];          //G27L",
                        dsbuf1s[67] = dsbuffer1s[291];          //G21R",
                        dsbuf1s[68] = dsbuffer1s[292];          //G22R",
                        dsbuf1s[69] = dsbuffer1s[293];          //G23R",
                        dsbuf1s[70] = dsbuffer1s[294];          //G24R",
                        dsbuf1s[71] = dsbuffer1s[295];          //G25R",
                        dsbuf1s[72] = dsbuffer1s[296];          //G26R",
                        dsbuf1s[73] = dsbuffer1s[297];          //G27R",
                        dsbuf1s[74] = dsbuffer1s[298];          //D12", 
                        dsbuf1s[75] = dsbuffer1s[299];          //D13", 
                        dsbuf1s[76] = dsbuffer1s[300];          //D14", 
                        dsbuf1s[77] = dsbuffer1s[301];          //D15", 
                        dsbuf1s[78] = dsbuffer1s[302];          //D16", 
                        dsbuf1s[79] = dsbuffer1s[303];          //D17", 
                        dsbuf1s[80] = dsbuffer1s[304];          //D18", 
                        dsbuf1s[81] = dsbuffer1s[305];          //D19", 
                        dsbuf1s[82] = dsbuffer1s[306];          //D20", 
                        dsbuf1s[83] = dsbuffer1s[307];          //U64", 
                        dsbuf1s[84] = dsbuffer1s[308];          //RasxCD
                        dsbuf1s[85] = dsbuffer1s[24];          //D1_pred 
                        dsbuf1s[86] = dsbuffer1s[25];          //D1_pred
                        dsbuf1s[87] = dsbuffer1s[26];          //D2_pred 
                        dsbuf1s[88] = dsbuffer1s[27];          //D2_pred
                        dsbuf1s[89] = dsbuffer1s[28];          //D3_pred 
                        dsbuf1s[90] = dsbuffer1s[29];          //D3_pred
                        dsbuf1s[91] = dsbuffer1s[30];          //D4_pred 
                        dsbuf1s[92] = dsbuffer1s[31];          //D4_pred
                        dsbuf1s[93] = dsbuffer1s[32];          //D5_pred 
                        dsbuf1s[94] = dsbuffer1s[33];          //D5_pred






                        #region Запись данных 1s


                        string comBD = "if not exists (select * from sysobjects where name ='" + "ds1s" + numberTable + "' and xtype='U') create table " + "ds1s" + numberTable +
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
                        // string comRulon1s1 = "INSERT INTO " + "ds1s" + numberTable +
                        //" (datetime1s,s191HL,s192HL,s193BL,s194BL,s191HR,s192HR,s193BR,s194BR,s281NL,s282NL,s283BL,s284BL,s281NR,s282NR," +
                        //"s283BR,s284BR,s301BL,s302BL,s303HL,s304HL,s301BR,s302BR,s303HR,s304HR,s321BL,s322BL,s323HL,s324HL,s321BR,s322BR," +
                        //"s323HR,s324HR,s341BL,s342BL,s343HL,s344HL,s341BR,s342BR,s343HR,s344HR,s461L,s462L,s463L,s461R,s462R,s463R,sG11L," +
                        //"sG12L,sG13L,sG14L,sG15L,sG16L,sG17L,sG11R,sG12R,sG13R,sG14R,sG15R,sG16R,sG17R,sG21L,sG22L,sG23L,sG24L,sG25L,sG26L," +
                        //"sG27L,sG21R,sG22R,sG23R,sG24R,sG25R,sG26R,sG27R,sD12,sD13,sD14,sD15,sD16,sD17,sD18,sD19,sD20,sU64,sRasxCD) " +
                        //"VALUES" +
                        //" ("+
                        //" @datetime1sds, " +
                        //BitConverter.ToInt16(dsbuf1s, 0) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 1) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 2) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 3) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 4) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 5) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 6) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 7) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 8) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 9) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 10) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 11) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 12) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 13) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 14) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 15) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 16) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 17) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 18) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 19) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 20) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 21) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 22) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 23) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 24) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 25) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 26) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 27) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 28) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 29) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 30) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 31) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 32) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 33) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 34) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 35) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 36) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 37) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 38) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 39) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 40) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 41) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 42) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 43) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 44) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 45) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 46) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 47) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 48) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 49) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 50) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 51) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 52) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 53) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 54) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 55) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 56) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 57) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 58) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 59) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 60) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 61) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 62) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 63) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 64) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 65) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 66) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 67) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 68) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 69) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 70) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 71) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 72) + "," +
                        //BitConverter.ToInt16(dsbuf1s, 73) + "," +
                        //"@sD12" + "," +
                        //"@sD13" + "," +
                        //"@sD14" + "," +
                        //"@sD15" + "," +
                        //"@sD16" + "," +
                        //"@sD17" + "," +
                        //"@sD18" + "," +
                        //"@sD19" + "," +
                        //"@sD20" + "," +
                        //(int)(BitConverter.ToInt16(dsbuf1s, 83)*10) + "," +
                        //(int)(BitConverter.ToInt16(dsbuf1s, 84)*10) + 
                        //")";

                        // using (SqlConnection conSQL1s2 = new SqlConnection(connectionString))
                        // {
                        //     try
                        //     {
                        //         conSQL1s2.Open();
                        //         SqlCommand command = new SqlCommand(comRulon1s1, conSQL1s2);
                        //         command.Parameters.AddWithValue("@datetime1sds", DateTime.Now);
                        //         command.Parameters.AddWithValue("@sD12",(float)(BitConverter.ToInt16(dsbuf1s, 74)) / 10);
                        //         command.Parameters.AddWithValue("@sD13",(float)(BitConverter.ToInt16(dsbuf1s, 75)) / 10);
                        //         command.Parameters.AddWithValue("@sD14",(float)(BitConverter.ToInt16(dsbuf1s, 76)) / 10);
                        //         command.Parameters.AddWithValue("@sD15",(float)(BitConverter.ToInt16(dsbuf1s, 77)) / 10);
                        //         command.Parameters.AddWithValue("@sD16",(float)(BitConverter.ToInt16(dsbuf1s, 78)) / 10);
                        //         command.Parameters.AddWithValue("@sD17",(float)(BitConverter.ToInt16(dsbuf1s, 79)) / 10);
                        //         command.Parameters.AddWithValue("@sD18",(float)(BitConverter.ToInt16(dsbuf1s, 80)) / 10);
                        //         command.Parameters.AddWithValue("@sD19",(float)(BitConverter.ToInt16(dsbuf1s, 81)) / 10);
                        //         command.Parameters.AddWithValue("@sD20",(float)(BitConverter.ToInt16(dsbuf1s, 82)) / 10);

                        //         command.ExecuteNonQuery();
                        //         conSQL1s2.Close();
                        //         Program.messageOK1c = "Данные в БД("+ "ds1s" + numberTable + ") 1s записаны";
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

                        string comRulon1s1 = "INSERT INTO " + "ds1s" + numberTable +
                        " (datetime1s,s191HL,s192HL,s193BL,s194BL,s191HR,s192HR,s193BR,s194BR,s281NL,s282NL,s283BL,s284BL,s281NR,s282NR," +
                        "s283BR,s284BR,s301BL,s302BL,s303HL,s304HL,s301BR,s302BR,s303HR,s304HR,s321BL,s322BL,s323HL,s324HL,s321BR,s322BR," +
                        "s323HR,s324HR,s341BL,s342BL,s343HL,s344HL,s341BR,s342BR,s343HR,s344HR,s461L,s462L,s463L,s461R,s462R,s463R,sG11L," +
                        "sG12L,sG13L,sG14L,sG15L,sG16L,sG17L,sG11R,sG12R,sG13R,sG14R,sG15R,sG16R,sG17R,sG21L,sG22L,sG23L,sG24L,sG25L,sG26L," +
                        "sG27L,sG21R,sG22R,sG23R,sG24R,sG25R,sG26R,sG27R,sD12,sD13,sD14,sD15,sD16,sD17,sD18,sD19,sD20,sU64,sRasxCD) " +
                        "VALUES" +
                        " (" +
                        " @datetime1sds, " +
                        BitConverter.ToInt16(dsbuf1s, 0) + "," +
                        BitConverter.ToInt16(dsbuf1s, 1) + "," +
                        BitConverter.ToInt16(dsbuf1s, 2) + "," +
                        BitConverter.ToInt16(dsbuf1s, 3) + "," +
                        BitConverter.ToInt16(dsbuf1s, 4) + "," +
                        BitConverter.ToInt16(dsbuf1s, 5) + "," +
                        BitConverter.ToInt16(dsbuf1s, 6) + "," +
                        BitConverter.ToInt16(dsbuf1s, 7) + "," +
                        BitConverter.ToInt16(dsbuf1s, 8) + "," +
                        BitConverter.ToInt16(dsbuf1s, 9) + "," +
                        BitConverter.ToInt16(dsbuf1s, 10) + "," +
                        BitConverter.ToInt16(dsbuf1s, 11) + "," +
                        BitConverter.ToInt16(dsbuf1s, 12) + "," +
                        BitConverter.ToInt16(dsbuf1s, 13) + "," +
                        BitConverter.ToInt16(dsbuf1s, 14) + "," +
                        BitConverter.ToInt16(dsbuf1s, 15) + "," +
                        BitConverter.ToInt16(dsbuf1s, 16) + "," +
                        BitConverter.ToInt16(dsbuf1s, 17) + "," +
                        BitConverter.ToInt16(dsbuf1s, 18) + "," +
                        BitConverter.ToInt16(dsbuf1s, 19) + "," +
                        BitConverter.ToInt16(dsbuf1s, 20) + "," +
                        BitConverter.ToInt16(dsbuf1s, 21) + "," +
                        BitConverter.ToInt16(dsbuf1s, 22) + "," +
                        BitConverter.ToInt16(dsbuf1s, 23) + "," +
                        BitConverter.ToInt16(dsbuf1s, 24) + "," +
                        BitConverter.ToInt16(dsbuf1s, 25) + "," +
                        BitConverter.ToInt16(dsbuf1s, 26) + "," +
                        BitConverter.ToInt16(dsbuf1s, 27) + "," +
                        BitConverter.ToInt16(dsbuf1s, 28) + "," +
                        BitConverter.ToInt16(dsbuf1s, 29) + "," +
                        BitConverter.ToInt16(dsbuf1s, 30) + "," +
                        BitConverter.ToInt16(dsbuf1s, 31) + "," +
                        BitConverter.ToInt16(dsbuf1s, 32) + "," +
                        BitConverter.ToInt16(dsbuf1s, 33) + "," +
                        BitConverter.ToInt16(dsbuf1s, 34) + "," +
                        BitConverter.ToInt16(dsbuf1s, 35) + "," +
                        BitConverter.ToInt16(dsbuf1s, 36) + "," +
                        BitConverter.ToInt16(dsbuf1s, 37) + "," +
                        BitConverter.ToInt16(dsbuf1s, 38) + "," +
                        BitConverter.ToInt16(dsbuf1s, 39) + "," +
                        BitConverter.ToInt16(dsbuf1s, 40) + "," +
                        BitConverter.ToInt16(dsbuf1s, 41) + "," +
                        BitConverter.ToInt16(dsbuf1s, 42) + "," +
                        BitConverter.ToInt16(dsbuf1s, 43) + "," +
                        BitConverter.ToInt16(dsbuf1s, 44) + "," +
                        BitConverter.ToInt16(dsbuf1s, 45) + "," +
                        BitConverter.ToInt16(dsbuf1s, 46) + "," +
                        BitConverter.ToInt16(dsbuf1s, 47) + "," +
                        BitConverter.ToInt16(dsbuf1s, 48) + "," +
                        BitConverter.ToInt16(dsbuf1s, 49) + "," +
                        BitConverter.ToInt16(dsbuf1s, 50) + "," +
                        BitConverter.ToInt16(dsbuf1s, 51) + "," +
                        BitConverter.ToInt16(dsbuf1s, 52) + "," +
                        BitConverter.ToInt16(dsbuf1s, 53) + "," +
                        BitConverter.ToInt16(dsbuf1s, 54) + "," +
                        BitConverter.ToInt16(dsbuf1s, 55) + "," +
                        BitConverter.ToInt16(dsbuf1s, 56) + "," +
                        BitConverter.ToInt16(dsbuf1s, 57) + "," +
                        BitConverter.ToInt16(dsbuf1s, 58) + "," +
                        BitConverter.ToInt16(dsbuf1s, 59) + "," +
                        BitConverter.ToInt16(dsbuf1s, 60) + "," +
                        BitConverter.ToInt16(dsbuf1s, 61) + "," +
                        BitConverter.ToInt16(dsbuf1s, 62) + "," +
                        BitConverter.ToInt16(dsbuf1s, 63) + "," +
                        BitConverter.ToInt16(dsbuf1s, 64) + "," +
                        BitConverter.ToInt16(dsbuf1s, 65) + "," +
                        BitConverter.ToInt16(dsbuf1s, 66) + "," +
                        BitConverter.ToInt16(dsbuf1s, 67) + "," +
                        BitConverter.ToInt16(dsbuf1s, 68) + "," +
                        BitConverter.ToInt16(dsbuf1s, 69) + "," +
                        BitConverter.ToInt16(dsbuf1s, 70) + "," +
                        BitConverter.ToInt16(dsbuf1s, 71) + "," +
                        BitConverter.ToInt16(dsbuf1s, 72) + "," +
                        BitConverter.ToInt16(dsbuf1s, 73) + "," +
                        (float)(BitConverter.ToInt16(dsbuf1s, 74)) / 10 + "," +
                        (float)(BitConverter.ToInt16(dsbuf1s, 75)) / 10 + "," +
                        (float)(BitConverter.ToInt16(dsbuf1s, 76)) / 10 + "," +
                        (float)(BitConverter.ToInt16(dsbuf1s, 77)) / 10 + "," +
                        (float)(BitConverter.ToInt16(dsbuf1s, 78)) / 10 + "," +
                        (float)(BitConverter.ToInt16(dsbuf1s, 79)) / 10 + "," +
                        (float)(BitConverter.ToInt16(dsbuf1s, 80)) / 10 + "," +
                        (float)(BitConverter.ToInt16(dsbuf1s, 81)) / 10 + "," +
                        (float)(BitConverter.ToInt16(dsbuf1s, 82)) / 10 + "," +
                        (int)(BitConverter.ToInt16(dsbuf1s, 83) * 10) + "," +
                        (int)(BitConverter.ToInt16(dsbuf1s, 84) * 10) +
                        ")";

                        using (SqlConnection conSQL1s2 = new SqlConnection(connectionString))
                        {
                            try
                            {
                                conSQL1s2.Open();
                                SqlCommand command = new SqlCommand(comRulon1s1, conSQL1s2);
                                command.Parameters.AddWithValue("@datetime1sds", DateTime.Now);
                                command.ExecuteNonQuery();
                                conSQL1s2.Close();
                                Program.messageOKDs1c = "Данные в БД(" + "ds1s" + numberTable + ") 1s записаны";
                                Program.dtOKDs1c = DateTime.Now;
                            }
                            catch (Exception ex)
                            {

                                Program.messageErrorDs1c = "1s НЕ ЗАПИСАНЫ - " + ex.Message + " Insert запрос: " + comRulon1s1;
                                Program.dtErrorDs1c = DateTime.Now;
                            }



                        }
                        #endregion

                        #endregion

                        #region Расчет параметров прокатанного рулона после окончания прокатки

                        dsD_tek_mot = (float)(BitConverter.ToInt16(dsbuffer1s, 20));
                        #region  Время начало прокатки рулона

                        if (dsD_pred_mot == 0)
                        {
                            //при первом цикле все данные равны 0, поэтому мы выставляем значения параметров на 600
                            dsD_tek_mot = 600;
                            dsD_pred_mot = 600;
                        }

                        if (dsD_tek_mot > dsD_pred_mot)
                        {
                            if (dsD_pred_mot < 615)
                            {
                                dsTimeStart = DateTime.Now;
                                blRulonStart = true;
                            }

                        }
                        else
                        {
                            //blRulonStart = false;
                        }


                        #endregion

                        #region Толщина и ширина прокатываемого рулона
                        speed4kl = (float)(BitConverter.ToInt16(dsbuffer1s, 6)) / 100;
                        if ((dsTimeStart != new DateTime()) && (H5_work == 0) && (dsD_tek_mot > 700) && (speed4kl > 2))
                        {
                            H5_work = (float)(BitConverter.ToInt16(dsbuffer1s, 12)) / 1000;
                            B_Work = (int)BitConverter.ToInt16(dsbuffer1s, 14);
                        }
                        #endregion

                        #region Формирование сигнала окончания прокатки
                        if ((dsTimeStart != new DateTime()) && (H5_work != 0) && (dsD_tek_mot < 610) && (dsD_tek_mot < dsD_pred_mot))
                        {
                            Ves_Work = (((((dsD_pred_mot * dsD_pred_mot) / 1000000 - 0.36F) * 3.141593F) / 4) * (B_Work / 1000)) * 7.85F;
                            dsTimeStop = DateTime.Now;
                            Dlina_Work = ((Ves_Work / 7.85F) / (B_Work / 1000)) / (H5_work / 1000);
                            blRulonStop = true;
                            blRulonStart = false;

                            
                            #region База производство

                            #region Создание БД

                            string comWorkdsCreate = "if not exists (select * from sysobjects where name ='work_ds' and xtype='U') create table work_ds" +
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
                                SqlCommand command = new SqlCommand(comWorkdsCreate, conSQL1sWork1);
                                command.ExecuteNonQuery();
                                conSQL1sWork1.Close();
                            }
                            #endregion;

                            #region Заполнение производство
                            string comWorkds = "INSERT INTO work_ds( " +
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

                            string beginWork = dsTimeStart.ToString("ddMMyyyyHHmm");
                            string endWork = dsTimeStop.ToString("HHmm");
                            string strNumberRulona = beginWork + endWork;

                            //Добавляем в таблицу прокатанных рулонов данные по рулонам
                            using (SqlConnection con3 = new SqlConnection(connectionString))
                            {
                                try
                                {
                                    con3.Open();
                                    SqlCommand command = new SqlCommand(comWorkds, con3);

                                    command.Parameters.AddWithValue("@NumberRulon", strNumberRulona);

                                    command.Parameters.AddWithValue("@TimeStart", dsTimeStart);
                                    command.Parameters.AddWithValue("@TimeStop", dsTimeStop);
                                    command.Parameters.AddWithValue("@H5_work", H5_work);
                                    command.Parameters.AddWithValue("@B_Work", B_Work);
                                    command.Parameters.AddWithValue("@Ves_Work", Ves_Work);
                                    command.Parameters.AddWithValue("@Dlina_Work", Dlina_Work);

                                    int WriteSQL = command.ExecuteNonQuery();

                                    Program.messageOKDsProizvodstvo = strNumberRulona + "(" + dsTimeStart.ToString("HH:mm") + "-" + dsTimeStop.ToString("HH:mm") + ") " + H5_work + "*" + B_Work + "->" + Ves_Work;
                                    //messageOKProizvodstvo = "производство";
                                    Program.dtOKDsProizvodstvo = DateTime.Now;
                                }
                                catch (Exception ex)
                                {
                                    Program.messageErrorDsProizvodstvo = "Ошибка в сохранении данных о прокатанном рулоне " + ex.Message + " Insert запрос: " + comWorkds;
                                    Program.dtErrorDsProizvodstvo = DateTime.Now;

                                }

                            }
                            #endregion

                            #endregion



                            #region //Очищаем базу временных рулонов
                            //using (SqlConnection conSQL1s3 = new SqlConnection(connectionString))
                            //{
                            //    conSQL1s3.Open();
                            //    string comRulon1s2 = "DELETE FROM TEMPds101ms";
                            //    SqlCommand command = new SqlCommand(comRulon1s2, conSQL1s3);
                            //    command.ExecuteNonQuery();


                            //}
                            #endregion


                            #region Мпереименовываем временную базу в базу с именем ds100mc(дата+время начала)(время окончания)
                            using (SqlConnection conSQL1s3 = new SqlConnection(connectionString))
                            {
                                try
                                {
                                    conSQL1s3.Open();
                                    string begin = dsTimeStart.ToString("ddMMyyyyHHmm");
                                    string end = dsTimeStop.ToString("HHmm");
                                    string comRulon1s2 = "sp_rename 'TEMPds101ms','" + begin + end + "'";
                                    SqlCommand command = new SqlCommand(comRulon1s2, conSQL1s3);
                                    command.ExecuteNonQuery();
                                    Program.messageOKDsRulon = "Временная база -> " + begin + end;
                                    Program.dtOKDsRulon = DateTime.Now;
                                    conSQL1s3.Close();
                                }
                                catch (Exception ex)
                                {

                                    Program.messageErrorDsRulon = "Временная база не переименована " + ex.Message;
                                    Program.dtErrorDsRulon = DateTime.Now;
                                }



                            }
                            #endregion

                        }
                        #endregion


                        #endregion

                        dsD_pred_mot = dsD_tek_mot;

                        #region Перевалки 

                        int d1 = (int)BitConverter.ToInt16(dsbuffer1s, 24);
                        int d2 = (int)BitConverter.ToInt16(dsbuffer1s, 26);
                        int d3 = (int)BitConverter.ToInt16(dsbuffer1s, 28);
                        int d4 = (int)BitConverter.ToInt16(dsbuffer1s, 30);
                        int d5 = (int)BitConverter.ToInt16(dsbuffer1s, 32);

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
                                dtPerevalkids.Rows.Add(DateTime.Now, d1, 0, 0, 0, 0);
                            }
                            if (d2_pred != d2)
                            {
                                blSave = true;
                                dtPerevalkids.Rows.Add(DateTime.Now, 0, d2, 0, 0, 0);
                            }
                            if (d3_pred != d3)
                            {
                                blSave = true;
                                dtPerevalkids.Rows.Add(DateTime.Now, 0, 0, d3, 0, 0);
                            }
                            if (d4_pred != d4)
                            {
                                blSave = true;
                                dtPerevalkids.Rows.Add(DateTime.Now, 0, 0, 0, d4, 0);
                            }
                            if (d5_pred != d5)
                            {
                                blSave = true;
                                dtPerevalkids.Rows.Add(DateTime.Now, 0, 0, 0, 0, d5);
                            }

                           
                        }
                        catch (Exception ex)
                        {
                            //ошибка
                            Program.messageErrorDsValki = "Ошибка формировании таблицы валков - " + ex.Message;
                            Program.dtErrorDsValki = DateTime.Now; ;
                        }



                        d1_pred = d1;
                        d2_pred = d2;
                        d3_pred = d3;
                        d4_pred = d4;
                        d5_pred = d5;



                        #region Перевалки сохраняем в БД
                        if (blSave)
                        {
                            string strTableNamePerevalki = "dsPerevalki" + DateTime.Now.ToString("yyyyMM");
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
                                    Program.messageErrorDsValki = "Ошибка формировании таблицы валков " + ex.Message;
                                    Program.dtErrorDsValki = DateTime.Now;

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
                                        bulk.WriteToServer(dtPerevalkids);
                                        Program.messageOKDsValki = "Данные перевалки в таблицу " + strTableNamePerevalki + " записаны ";
                                        Program.dtOKDsValki = DateTime.Now;


                                        dtPerevalkids.Clear(); //очистка таблицы 
                                    }
                                    conPerevalki2.Close();



                                }
                                catch (Exception ex)
                                {
                                    Program.messageErrorDsValki = "Ошибка записи в таблицу валков " + ex.Message;
                                    Program.dtErrorDsValki = DateTime.Now;

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
                    Program.messageErrorDs1c = "Ошибка глобальная - " + ex;
                    Program.dtErrorDs1c = DateTime.Now;

                }


            }

            #endregion


            #region Формируем и записываем сообщения в Базу
            private void dsMessage200ms()
            {
                try
                {
                    while (true)
                    {
                        Thread.Sleep(200);

                        dtMessage = DateTime.Now;

                        float speed = (float)(BitConverter.ToInt16(dsbufferMessage, 16)) / 100;


                        int numberMessage = 0;
                        for (int i = 0; i < 15; i++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                int z = Convert.ToInt32(Math.Pow(2, b));
                                if (((byte)(dsbufferMessageOld[i] & z) - (byte)(dsbufferMessage[i] & z)) < 0)
                                {
                                    if (MessageDs[numberMessage].statusMenshe != 0)
                                    {
                                        string mes = MessageDs[numberMessage].MinusMess;
                                        int status = MessageDs[numberMessage].statusMenshe;
                                        dtMessageds.Rows.Add(dtMessage.ToString("HH:mm:ss.fff"), status, mes, speed);
                                    }
                                }
                                else if (((byte)(dsbufferMessageOld[i] & z) - (byte)(dsbufferMessage[i] & z)) > 0)
                                {
                                    if (MessageDs[numberMessage].statusBolshe != 0)
                                    {
                                        string mes = MessageDs[numberMessage].PlusMess;
                                        int status = MessageDs[numberMessage].statusBolshe;
                                        dtMessageds.Rows.Add(dtMessage.ToString("HH:mm:ss.fff"), status, mes, speed);
                                    }
                                }
                                numberMessage++;

                            }
                        }

                        //TODO запись сообщений в БД каждую минуту    
                        if (writeMessage > 300)
                        {
                            writeMessage = 0;
                            string strTableName = "dsMessage" + numberTable;
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
                            if (dtMessageds.Rows.Count > 0)
                            {
                                //записываем в таблицу прокатанного рулона данные по прокатке этого рулона
                                using (SqlConnection con2Mess = new SqlConnection(connectionString))
                                {
                                    con2Mess.Open();
                                    using (var bulkMessage = new SqlBulkCopy(con2Mess))
                                    {
                                        bulkMessage.DestinationTableName = strTableName;
                                        bulkMessage.WriteToServer(dtMessageds);
                                        Program.messageOKDs200mc = "Кол-во сообщений записанных в таблицу " + strTableName + " равно " + dtMessageds.Rows.Count;
                                        Program.dtOKDs200mc = dtMessage;
                                    }
                                }
                                dtMessageds.Clear();
                            }
                            else
                            {

                                Program.messageOKDs200mc = "Сообщений не было с " + DateTime.Now.AddMinutes(-1).ToString() + " по " + DateTime.Now.ToString();
                                Program.dtOKDs200mc = dtMessage;
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
                    Program.messageErrorDs200mc = "Global Error в модуле формирования сообщения " + ex.Message;
                    Program.dtOKDs200mc = DateTime.Now;



                }
            }
            #endregion
        

    }
}
