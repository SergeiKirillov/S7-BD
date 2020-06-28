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

                Thread querySQL = new Thread(dsSQL80ms);
                querySQL.Start();

                
                Thread query1s = new Thread(dsSQL1s);
                query1s.Start();

                //Thread queryMes = new Thread(dsMessage200ms);
                //queryMes.Start();


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
                    dsbufferMessage = new byte[25];
                    dsbufferMessageOld = new byte[25];


                    int resultReadField = 5;


                    while (true)
                    {
                        Thread.Sleep(70);

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
                    Program.messageErrorDs100mc = "Общая ошибка 70mc -" + ex.Message;
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
                    dsbufferMessageOld[22] = dsbufferMessage[21];
                    dsbufferMessageOld[23] = dsbufferMessage[21];
                    dsbufferMessageOld[24] = dsbufferMessage[21];
                


                dsbufferMessage[0] = dsbufferPLC[0];
                    dsbufferMessage[1] = dsbufferPLC[1];
                    dsbufferMessage[2] = dsbufferPLC[24];
                    dsbufferMessage[3] = dsbufferPLC[25];
                    dsbufferMessage[4] = dsbufferPLC[26];
                    dsbufferMessage[5] = dsbufferPLC[27];
                    dsbufferMessage[6] = dsbufferPLC[28];
                    dsbufferMessage[7] = dsbufferPLC[29];
                    dsbufferMessage[8] = dsbufferPLC[30];
                    dsbufferMessage[9] = dsbufferPLC[31];
                    dsbufferMessage[10] = dsbufferPLC[32];
                    dsbufferMessage[11] = dsbufferPLC[33];
                    dsbufferMessage[12] = dsbufferPLC[34];
                    dsbufferMessage[13] = dsbufferPLC[35];
                    dsbufferMessage[14] = dsbufferPLC[36];
                    dsbufferMessage[15] = dsbufferPLC[37];
                    dsbufferMessage[16] = dsbufferPLC[38];   //speed 4kl
                    dsbufferMessage[17] = dsbufferPLC[40];
                    dsbufferMessage[18] = dsbufferPLC[41];
                    dsbufferMessage[19] = dsbufferPLC[42];
                    dsbufferMessage[20] = dsbufferPLC[43];
                    dsbufferMessage[21] = dsbufferPLC[44];
                    dsbufferMessage[22] = dsbufferPLC[45];
                    dsbufferMessage[23] = dsbufferPLC[46];
                    dsbufferMessage[24] = dsbufferPLC[47];

                }

            }


            #endregion


            #region Запись данных(80ms) с контроллера в Базу Данных

            private void dsSQL80ms()
            {
                try
                {
                    while (true)
                    {
                        Thread.Sleep(80);

                        #region Формируем SQL запрос с циклом 80мс и записываем его во временную БД

                        #region Если БД не существует то создаем
                        string comRulon80ms1 = "if not exists (select * from sysobjects where name ='TEMPds80ms' and xtype='U') create table TEMPds80ms " +
                           "(" +
                           "datetime80ms datetime , " +
                           "VKlet float," +              
                           "IzadR float," +              
                           "IzadM float," +              
                           "NKlet float," +              
                           "NRazm float," +              
                           "NMot float," +               
                           "TRazm float," +              
                           "TMot float," +               
                           "RRazm float," +              
                           "RMot float," +               
                           "NVlev float," +              
                           "NVpr float," +               
                           "IvozM float," +              
                           "Imot float," +               
                           "Urazm float," +              
                           "IvozR float," +              
                           "Umot float," +               
                           "IRUZ4 float," +              
                           "IRUZ5 float," +              
                           "IMUZ4 float," +              
                           "IMUZ5 float," +              
                           "IzovK float," +              
                           "Ukl float," +                
                           "IKUZ4 float," +              
                           "IKUZ5 float," +              
                           "ObgTek float," +             
                           "DatObgDo float," +           
                           "DatObgZa float"  +             
                           ")";



                        using (SqlConnection conSQL80ms1 = new SqlConnection(connectionString))
                        {
                            conSQL80ms1.Open();
                            SqlCommand command = new SqlCommand(comRulon80ms1, conSQL80ms1);
                            command.ExecuteNonQuery();
                            conSQL80ms1.Close();

                        }
                        #endregion

                        //if (blRulonStart)
                        if (true)
                        {

                            string comRulon80ms2 = "INSERT INTO TEMPds80ms" +
                           "(datetime80ms,VKlet,IzadR,IzadM,NKlet,NRazm,NMot,TRazm,TMot,RRazm,RMot,NVlev,NVpr,IvozM,Imot,Urazm,IvozR,Umot,IRUZ4,IRUZ5,IMUZ4,IMUZ5,IzovK,Ukl,IKUZ4,IKUZ5,ObgTek,DatObgDo,DatObgZa)" +
                           " VALUES " +
                           "('" +
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'," +       //"datetime101ms datetime , "
                            (float)(BitConverter.ToInt16(dsbufferSQL, 0)) / 100 + "," +     //"VKlet float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 2)) / 1 + "," +       //"IzadR float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 4)) / 1 + "," +       //"IzadM float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 6)) / 10 + "," +      //"NKlet float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 8)) / 10 + "," +      //"NRazm float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 10)) / 10 + "," +     //"NMot float," +             
                            (float)(BitConverter.ToInt16(dsbufferSQL, 12)) * 10 + "," +     //"TRazm float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 14)) * 10 + "," +     //"TMot float," +             
                            (float)(BitConverter.ToInt16(dsbufferSQL, 16)) / 1 + "," +      //"RRazm float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 18)) / 1 + "," +      //"RMot float," +             
                            (float)(BitConverter.ToInt16(dsbufferSQL, 20)) / 100 + "," +    //"NVlev float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 22)) / 100 + "," +    //"NVpr float," +             
                            (float)(BitConverter.ToInt16(dsbufferSQL, 48)) / 100 + "," +    //"IvozM float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 50)) / 1 + "," +      //"Imot float," +             
                            (float)(BitConverter.ToInt16(dsbufferSQL, 52)) / 10 + "," +     //"Urazm float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 54)) / 100 + "," +    //"IvozR float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 56)) / 10 + "," +     //"Umot float," +             
                            (float)(BitConverter.ToInt16(dsbufferSQL, 128)) / 1 + "," +     //"IRUZ4 float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 130)) / 1 + "," +     //"IRUZ5 float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 132)) / 1 + "," +     //"IMUZ4 float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 134)) / 1 + "," +     //"IMUZ5 float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 136)) / 100 + "," +   //"IzovK float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 138)) / 10 + "," +    //"Ukl float," +              
                            (float)(BitConverter.ToInt16(dsbufferSQL, 140)) / 1 + "," +     //"IKUZ4 float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 142)) / 1 + "," +     //"IKUZ5 float," +            
                            (float)(BitConverter.ToInt16(dsbufferSQL, 144)) / 100 + "," +   //"ObgTek float," +           
                            (float)(BitConverter.ToInt16(dsbufferSQL, 146)) / 1 + "," +     //"DatObgDo float," +         
                            (float)(BitConverter.ToInt16(dsbufferSQL, 148)) / 1 +           //"DatObgZa float"  +         
                            ")";



                            if (true) //TODO Если установлен bit что прокатка рулона(1s) то тогда пишем во временную таблицу
                            {
                                using (SqlConnection conSQL80ms2 = new SqlConnection(connectionString))
                                {
                                    try
                                    {
                                        conSQL80ms2.Open();
                                        SqlCommand command = new SqlCommand(comRulon80ms2, conSQL80ms2);
                                        command.ExecuteNonQuery();
                                        Program.messageOKDs101mc = "-Write";
                                        Program.dtOKDs101mc = DateTime.Now;
                                        conSQL80ms2.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        Program.messageErrorDs101mc = "80mc НЕ ЗАПИСАНЫ. " + ex.Message + " Insert запрос: " + comRulon80ms2;
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
                    Program.messageErrorDs101mc = "Ошибка 80мс-" + ex.Message;
                    Program.dtErrorDs101mc = DateTime.Now;
                }


            }
            #endregion

            #region Формируем и записываем данные  1c. Название таблицы формируется по принципу YYYYmmddW (W - смена(1 ночная(с 19-07), 2дневная(07-19)))
            private void dsSQL1s()
            {
                byte[] dsbuf1s = new byte[56];

                
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


                    dsbuf1s[0] = dsbuffer1s[58];    //buffer_1s_w[0]:
                    dsbuf1s[1] = dsbuffer1s[59];

                    dsbuf1s[2] = dsbuffer1s[60];    //buffer_1s_w[1]:
                    dsbuf1s[3] = dsbuffer1s[61];

                    dsbuf1s[4] = dsbuffer1s[62];    //buffer_1s_w[2]:
                    dsbuf1s[5] = dsbuffer1s[63];
                    
                    dsbuf1s[6] = dsbuffer1s[64];    //buffer_1s_w[3]:  
                    dsbuf1s[7] = dsbuffer1s[65];    

                    dsbuf1s[8] = dsbuffer1s[66];     //buffer_1s_w[4]:
                    dsbuf1s[9] = dsbuffer1s[67];

                    dsbuf1s[10] = dsbuffer1s[68];    //buffer_1s_w[5]:   
                    dsbuf1s[11] = dsbuffer1s[69];

                    dsbuf1s[12] = dsbuffer1s[70];   //buffer_1s_w[6]:   
                    dsbuf1s[13] = dsbuffer1s[71];

                    dsbuf1s[14] = dsbuffer1s[72];   //buffer_1s_w[7]:   
                    dsbuf1s[15] = dsbuffer1s[73];   

                    dsbuf1s[16] = dsbuffer1s[74];   //buffer_1s_w[8]: 
                    dsbuf1s[17] = dsbuffer1s[75];   

                    dsbuf1s[18] = dsbuffer1s[76];   //buffer_1s_w[9]: 
                    dsbuf1s[19] = dsbuffer1s[77];   

                    dsbuf1s[20] = dsbuffer1s[78];   //buffer_1s_w[10]:      
                    dsbuf1s[21] = dsbuffer1s[79];   //buffer_1s_w[11]:
                    dsbuf1s[22] = dsbuffer1s[80];   //buffer_1s_w[12]:
                    dsbuf1s[23] = dsbuffer1s[81];   //buffer_1s_w[13]:
                    dsbuf1s[24] = dsbuffer1s[82];   //buffer_1s_w[14]:
                    dsbuf1s[25] = dsbuffer1s[83];   //buffer_1s_w[15]:   //t1   
                    dsbuf1s[26] = dsbuffer1s[84];   //buffer_1s_w[16]:   //t2      
                    dsbuf1s[27] = dsbuffer1s[85];   //buffer_1s_w[17]:   //t3      
                    dsbuf1s[28] = dsbuffer1s[86];   //buffer_1s_w[18]:   //t4      
                    dsbuf1s[29] = dsbuffer1s[87];   //buffer_1s_w[19]:   //t5      
                    dsbuf1s[30] = dsbuffer1s[88];   //buffer_1s_w[20]:   //t6      
                    dsbuf1s[31] = dsbuffer1s[89];   //buffer_1s_w[21]:   //t7      
                    dsbuf1s[32] = dsbuffer1s[90];   //buffer_1s_w[22]:   //t8      
                    dsbuf1s[33] = dsbuffer1s[91];   //buffer_1s_w[23]:   //t9      
                    dsbuf1s[34] = dsbuffer1s[92];   //buffer_1s_w[24]:   //t10     
                    dsbuf1s[35] = dsbuffer1s[93];   //buffer_1s_w[25]:   //t11     
                    dsbuf1s[36] = dsbuffer1s[94];   //buffer_1s_w[26]:   //t12     
                    dsbuf1s[37] = dsbuffer1s[95];   //buffer_1s_w[27]:   //t13     
                    dsbuf1s[38] = dsbuffer1s[96];   //buffer_1s_w[28]:   //t14     
                    dsbuf1s[39] = dsbuffer1s[97];   //buffer_1s_w[29]:   //t15     
                    dsbuf1s[40] = dsbuffer1s[98];   //buffer_1s_w[30]:   //t16     
                    dsbuf1s[41] = dsbuffer1s[99];   //buffer_1s_w[31]:   //t17     
                    dsbuf1s[42] = dsbuffer1s[100];  //buffer_1s_w[32]:   //t18     
                    dsbuf1s[43] = dsbuffer1s[101];  //buffer_1s_w[33]:   //t19     
                    dsbuf1s[44] = dsbuffer1s[102];  //buffer_1s_w[34]:   //t20     
                    dsbuf1s[45] = dsbuffer1s[103];  //buffer_1s_w[35]:   //t21     
                    dsbuf1s[46] = dsbuffer1s[104];  //buffer_1s_w[36]:   //t22     
                    dsbuf1s[47] = dsbuffer1s[105];  //buffer_1s_w[37]:   //t23     
                    dsbuf1s[48] = dsbuffer1s[106];  //buffer_1s_w[38]:   //t24     
                    dsbuf1s[49] = dsbuffer1s[107];  //buffer_1s_w[39]:   //t25     
                    dsbuf1s[50] = dsbuffer1s[108];  //buffer_1s_w[40]:   //t26     
                    dsbuf1s[51] = dsbuffer1s[109];  //buffer_1s_w[41]:   //t27     
                    dsbuf1s[52] = dsbuffer1s[110];  //buffer_1s_w[42]:   //t28     
                    dsbuf1s[53] = dsbuffer1s[111];  //buffer_1s_w[43]:   //t29     
                    dsbuf1s[54] = dsbuffer1s[112];  //buffer_1s_w[44]:   //Voda    
                    dsbuf1s[55] = dsbuffer1s[113];  //buffer_1s_w[45]:      




                    #region Запись данных 1s


                    string comBD = "if not exists (select * from sysobjects where name ='" + "ds1s" + numberTable + "' and xtype='U') create table " + "ds1s" + numberTable +
                           "(" +
                           "datetime1s datetime , " +           // `timeD` varchar(45) NOT NULL,                  
                           "Uvvod1 float , " +                  // `Uvvod1` varchar(10) default NULL,           
                           "Ivvod1 float , " +                    // `Ivvod1` varchar(10) default NULL,            
                           "Wvvod1 int , " +                    // `Wvvod1` varchar(10) default NULL,          
                           "WRvvod1 int , " +                    // `WRvvod1` varchar(10) default NULL,       
                           "WQvvod1 int , " +                    // `WQvvod1` varchar(10) default NULL,       
                           "Cosvvod1  float , " +                    // `Cosvvod1` varchar(10) default NULL,       
                           "gar5vvod1 float , " +                    // `gar5vvod1` varchar(10) default NULL,     
                           "gar7vvod1 float , " +                    // `gar7vvod1` varchar(10) default NULL,     
                           "gar11vvod1 float , " +                    // `gar11vvod1` varchar(10) default NULL,   
                           "gar13vvod1 float , " +                    // `gar13vvod1` varchar(10) default NULL,   
                           "Ur64 float , " +                    // `Ur64` varchar(10) default NULL,               
                           "D21 float , " +                    // `D21` varchar(10) default NULL,                 
                           "D22 float , " +                    // `D22` varchar(10) default NULL,                 
                           "D23 float , " +                    // `D23` varchar(10) default NULL,                 
                           "D24 float , " +                    // `D24` varchar(10) default NULL,                 
                           "t1 int , " +                    // `t1` varchar(10) default NULL,               
                           "t2 int , " +                    // `t2` varchar(10) default NULL,               
                           "t3 int , " +                    // `t3` varchar(10) default NULL,               
                           "t4 int , " +                    // `t4` varchar(10) default NULL,               
                           "t5 int , " +                    // `t5` varchar(10) default NULL,               
                           "t6 int , " +                    // `t6` varchar(10) default NULL,               
                           "t7 int , " +                    // `t7` varchar(10) default NULL,               
                           "t8 int , " +                    // `t8` varchar(10) default NULL,               
                           "t9 int , " +                    // `t9` varchar(10) default NULL,               
                           "t10 int , " +                    // `t10` varchar(10) default NULL,            
                           "t11 int , " +                    // `t11` varchar(10) default NULL,            
                           "t12 int , " +                    // `t12` varchar(10) default NULL,            
                           "t13 int , " +                    // `t13` varchar(10) default NULL,            
                           "t14 int , " +                    // `t14` varchar(10) default NULL,            
                           "t15old int , " +                 // `t15old` varchar(10) default NULL,       
                           "t16 int , " +                    // `t16` varchar(10) default NULL,            
                           "t17 int , " +                    // `t17` varchar(10) default NULL,            
                           "t18 int , " +                    // `t18` varchar(10) default NULL,            
                           "t19 int , " +                    // `t19` varchar(10) default NULL,            
                           "t20 int , " +                    // `t20` varchar(10) default NULL,            
                           "t21 int , " +                    // `t21` varchar(10) default NULL,            
                           "t22 int , " +                    // `t22` varchar(10) default NULL,            
                           "t23 int , " +                    // `t23` varchar(10) default NULL,            
                           "t24 int , " +                    // `t24` varchar(10) default NULL,            
                           "t25 int , " +                    // `t25` varchar(10) default NULL,            
                           "t26 int , " +                     // `t26` varchar(10) default NULL,            
                           "t27 int , " +                     // `t27` varchar(10) default NULL,            
                           "t28 int , " +                     // `t28` varchar(10) default NULL,            
                           "t29 int , " +                     // `t29` varchar(10) default NULL,           
                           "Voda int  " +                     // `Voda` varchar(10) default NULL,         
                           ")";

                     using (SqlConnection conSQL1s1 = new SqlConnection(connectionString))
                     {
                         conSQL1s1.Open();
                         SqlCommand command = new SqlCommand(comBD, conSQL1s1);
                         command.ExecuteNonQuery();
                         conSQL1s1.Close();
                     }

                        

                        #region Через обычный инсерт но перед передачей выставили региональные настройки с помощью System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                        string comRulon1s1 = "INSERT INTO " + "ds1s" + numberTable +
                        " (" +
                        "datetime1s,Uvvod1,Ivvod1,Wvvod1,WRvvod1,WQvvod1,Cosvvod1,gar5vvod1,gar7vvod1,gar11vvod1,gar13vvod1,Ur64,D21,D22,D23,D24,t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15old,t16,t17,t18,t19,t20,t21,t22,t23,t24,t25,t26,t27,t28,t29,Voda)" +
                        "VALUES" +
                        " (" +
                        " @datetime1sds, " +                                                 //"datetime1s datetime , " + 
                        (float)(BitConverter.ToInt16(dsbuf1s, 0)) / 100 + "," +                 //"Uvvod1 float , " +        
                        (float)(BitConverter.ToInt16(dsbuf1s, 2)) / 10 + "," +                  //"Ivvod1 float , " +        
                        BitConverter.ToInt16(dsbuf1s, 4) + "," +                                //"Wvvod1 int , " +          
                        BitConverter.ToInt16(dsbuf1s, 6) + "," +                                //"WRvvod1 int , " +         
                        BitConverter.ToInt16(dsbuf1s, 8) + "," +                                //"WQvvod1 int , " +         
                        (float)BitConverter.ToInt16(dsbuf1s, 10) / 100 + "," +                  //"Cosvvod1  float , " +     
                        (float)BitConverter.ToInt16(dsbuf1s, 12) / 100 + "," +                  //"gar5vvod1 float , " +     
                        (float)BitConverter.ToInt16(dsbuf1s, 14) / 100 + "," +                  //"gar7vvod1 float , " +     
                        (float)BitConverter.ToInt16(dsbuf1s, 16) / 100 + "," +                  //"gar11vvod1 float , " +    
                        (float)BitConverter.ToInt16(dsbuf1s, 18) / 100 + "," + //buffer_1s_w[9] //"gar13vvod1 float , " +    

                        (float)BitConverter.ToInt16(dsbuf1s, 20)*10 + "," +     //buffer_1s_w[10] "Ur64 float , " +          
                        (float)BitConverter.ToInt16(dsbuf1s, 21)/10 + "," +                     //"D21 float , " +           
                        (float)BitConverter.ToInt16(dsbuf1s, 22)/10 + "," +                     //"D22 float , " +           
                        (float)BitConverter.ToInt16(dsbuf1s, 23)/10 + "," +                     //"D23 float , " +           
                        (float)BitConverter.ToInt16(dsbuf1s, 24)/10 + "," +     //buffer_1s_w[14] "D24 float , " +           
                        BitConverter.ToInt16(dsbuf1s, 25) + "," +                                //t1                                        
                        BitConverter.ToInt16(dsbuf1s, 26) + "," +                                //t2                          
                        BitConverter.ToInt16(dsbuf1s, 27) + "," +                                //t3                          
                        BitConverter.ToInt16(dsbuf1s, 28) + "," +                                //t4                          
                        BitConverter.ToInt16(dsbuf1s, 29) + "," +                                //t5                          
                        BitConverter.ToInt16(dsbuf1s, 30) + "," +                                //t6                          
                        BitConverter.ToInt16(dsbuf1s, 31) + "," +                                //t7                          
                        BitConverter.ToInt16(dsbuf1s, 32) + "," +                                //t8                          
                        BitConverter.ToInt16(dsbuf1s, 33) + "," +                                //t9                          
                        BitConverter.ToInt16(dsbuf1s, 34) + "," +                                //t10                         
                        BitConverter.ToInt16(dsbuf1s, 35) + "," +                                //t11                         
                        BitConverter.ToInt16(dsbuf1s, 36) + "," +                                //t12                         
                        BitConverter.ToInt16(dsbuf1s, 37) + "," +                                //t13                         
                        BitConverter.ToInt16(dsbuf1s, 38) + "," +                                //t14                         
                        BitConverter.ToInt16(dsbuf1s, 39) + "," +                                //t15                      
                        BitConverter.ToInt16(dsbuf1s, 40) + "," +                                //t16                         
                        BitConverter.ToInt16(dsbuf1s, 41) + "," +                                //t17                         
                        BitConverter.ToInt16(dsbuf1s, 42) + "," +                                //t18                         
                        BitConverter.ToInt16(dsbuf1s, 43) + "," +                                //t19                         
                        BitConverter.ToInt16(dsbuf1s, 44) + "," +                                //t20                         
                        BitConverter.ToInt16(dsbuf1s, 45) + "," +                                //t21                         
                        BitConverter.ToInt16(dsbuf1s, 46) + "," +                                //t22                         
                        BitConverter.ToInt16(dsbuf1s, 47) + "," +                                //t23                         
                        BitConverter.ToInt16(dsbuf1s, 48) + "," +                                //t24                         
                        BitConverter.ToInt16(dsbuf1s, 49) + "," +                                //t25                         
                        BitConverter.ToInt16(dsbuf1s, 50) + "," +                                //t26                         
                        BitConverter.ToInt16(dsbuf1s, 51) + "," +                                //t27                         
                        BitConverter.ToInt16(dsbuf1s, 52) + "," +                                //t28                         
                        BitConverter.ToInt16(dsbuf1s, 53) + "," +                                //t29                        
                        BitConverter.ToInt16(dsbuf1s, 54) +                               //Voda
                        
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

                        //#region Расчет параметров прокатанного рулона после окончания прокатки

                        //dsD_tek_mot = (float)(BitConverter.ToInt16(dsbuffer1s, 20));
                        //#region  Время начало прокатки рулона

                        //if (dsD_pred_mot == 0)
                        //{
                        //    //при первом цикле все данные равны 0, поэтому мы выставляем значения параметров на 600
                        //    dsD_tek_mot = 600;
                        //    dsD_pred_mot = 600;
                        //}

                        //if (dsD_tek_mot > dsD_pred_mot)
                        //{
                        //    if (dsD_pred_mot < 615)
                        //    {
                        //        dsTimeStart = DateTime.Now;
                        //        blRulonStart = true;
                        //    }

                        //}
                        //else
                        //{
                        //    //blRulonStart = false;
                        //}


                        //#endregion

                        //#region Толщина и ширина прокатываемого рулона
                        //speed4kl = (float)(BitConverter.ToInt16(dsbuffer1s, 6)) / 100;
                        //if ((dsTimeStart != new DateTime()) && (H5_work == 0) && (dsD_tek_mot > 700) && (speed4kl > 2))
                        //{
                        //    H5_work = (float)(BitConverter.ToInt16(dsbuffer1s, 12)) / 1000;
                        //    B_Work = (int)BitConverter.ToInt16(dsbuffer1s, 14);
                        //}
                        //#endregion

                        //#region Формирование сигнала окончания прокатки
                        //if ((dsTimeStart != new DateTime()) && (H5_work != 0) && (dsD_tek_mot < 610) && (dsD_tek_mot < dsD_pred_mot))
                        //{
                        //    Ves_Work = (((((dsD_pred_mot * dsD_pred_mot) / 1000000 - 0.36F) * 3.141593F) / 4) * (B_Work / 1000)) * 7.85F;
                        //    dsTimeStop = DateTime.Now;
                        //    Dlina_Work = ((Ves_Work / 7.85F) / (B_Work / 1000)) / (H5_work / 1000);
                        //    blRulonStop = true;
                        //    blRulonStart = false;

                            
                        //    #region База производство

                        //    #region Создание БД

                        //    string comWorkdsCreate = "if not exists (select * from sysobjects where name ='work_ds' and xtype='U') create table work_ds" +
                        //   "(" +
                        //   "numberRulona bigint, " +
                        //   "start datetime , " +
                        //   "stop datetime , " +
                        //   "h5 float , " +
                        //   "b float , " +
                        //   "ves float , " +
                        //   "dlinna float , " +
                        //   "t1 float , " +
                        //   "t2 float , " +
                        //   "t3 float , " +
                        //   "t4 float , " +
                        //   "t5 float  " +

                        //   ")";

                        //    using (SqlConnection conSQL1sWork1 = new SqlConnection(connectionString))
                        //    {
                        //        conSQL1sWork1.Open();
                        //        SqlCommand command = new SqlCommand(comWorkdsCreate, conSQL1sWork1);
                        //        command.ExecuteNonQuery();
                        //        conSQL1sWork1.Close();
                        //    }
                        //    #endregion;

                        //    #region Заполнение производство
                        //    string comWorkds = "INSERT INTO work_ds( " +
                        //        "numberRulona," +
                        //        "start," +
                        //        "stop," +
                        //        "h5," +
                        //        "b," +
                        //        "ves," +
                        //        "dlinna" +
                        //        ") " +
                        //        "VALUES(" +
                        //        "@NumberRulon, " +
                        //        "@TimeStart, " +
                        //        "@TimeStop, " +
                        //        "@H5_work, " +
                        //        "@B_Work, " +
                        //        "@Ves_Work, " +
                        //        "@Dlina_Work)";

                        //    string beginWork = dsTimeStart.ToString("ddMMyyyyHHmm");
                        //    string endWork = dsTimeStop.ToString("HHmm");
                        //    string strNumberRulona = beginWork + endWork;

                        //    //Добавляем в таблицу прокатанных рулонов данные по рулонам
                        //    using (SqlConnection con3 = new SqlConnection(connectionString))
                        //    {
                        //        try
                        //        {
                        //            con3.Open();
                        //            SqlCommand command = new SqlCommand(comWorkds, con3);

                        //            command.Parameters.AddWithValue("@NumberRulon", strNumberRulona);

                        //            command.Parameters.AddWithValue("@TimeStart", dsTimeStart);
                        //            command.Parameters.AddWithValue("@TimeStop", dsTimeStop);
                        //            command.Parameters.AddWithValue("@H5_work", H5_work);
                        //            command.Parameters.AddWithValue("@B_Work", B_Work);
                        //            command.Parameters.AddWithValue("@Ves_Work", Ves_Work);
                        //            command.Parameters.AddWithValue("@Dlina_Work", Dlina_Work);

                        //            int WriteSQL = command.ExecuteNonQuery();

                        //            Program.messageOKDsProizvodstvo = strNumberRulona + "(" + dsTimeStart.ToString("HH:mm") + "-" + dsTimeStop.ToString("HH:mm") + ") " + H5_work + "*" + B_Work + "->" + Ves_Work;
                        //            //messageOKProizvodstvo = "производство";
                        //            Program.dtOKDsProizvodstvo = DateTime.Now;
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            Program.messageErrorDsProizvodstvo = "Ошибка в сохранении данных о прокатанном рулоне " + ex.Message + " Insert запрос: " + comWorkds;
                        //            Program.dtErrorDsProizvodstvo = DateTime.Now;

                        //        }

                        //    }
                        //    #endregion

                        //    #endregion



                        //    #region //Очищаем базу временных рулонов
                        //    //using (SqlConnection conSQL1s3 = new SqlConnection(connectionString))
                        //    //{
                        //    //    conSQL1s3.Open();
                        //    //    string comRulon1s2 = "DELETE FROM TEMPds101ms";
                        //    //    SqlCommand command = new SqlCommand(comRulon1s2, conSQL1s3);
                        //    //    command.ExecuteNonQuery();


                        //    //}
                        //    #endregion


                        //    #region Мпереименовываем временную базу в базу с именем ds100mc(дата+время начала)(время окончания)
                        //    using (SqlConnection conSQL1s3 = new SqlConnection(connectionString))
                        //    {
                        //        try
                        //        {
                        //            conSQL1s3.Open();
                        //            string begin = dsTimeStart.ToString("ddMMyyyyHHmm");
                        //            string end = dsTimeStop.ToString("HHmm");
                        //            string comRulon1s2 = "sp_rename 'TEMPds101ms','" + begin + end + "'";
                        //            SqlCommand command = new SqlCommand(comRulon1s2, conSQL1s3);
                        //            command.ExecuteNonQuery();
                        //            Program.messageOKDsRulon = "Временная база -> " + begin + end;
                        //            Program.dtOKDsRulon = DateTime.Now;
                        //            conSQL1s3.Close();
                        //        }
                        //        catch (Exception ex)
                        //        {

                        //            Program.messageErrorDsRulon = "Временная база не переименована " + ex.Message;
                        //            Program.dtErrorDsRulon = DateTime.Now;
                        //        }



                        //    }
                        //    #endregion

                        //}
                        //#endregion


                        //#endregion

                        //dsD_pred_mot = dsD_tek_mot;

                        //#region Перевалки 

                        //int d1 = (int)BitConverter.ToInt16(dsbuffer1s, 24);
                        //int d2 = (int)BitConverter.ToInt16(dsbuffer1s, 26);
                        //int d3 = (int)BitConverter.ToInt16(dsbuffer1s, 28);
                        //int d4 = (int)BitConverter.ToInt16(dsbuffer1s, 30);
                        //int d5 = (int)BitConverter.ToInt16(dsbuffer1s, 32);

                        //if (d1_pred == 0) d1_pred = d1;
                        //if (d2_pred == 0) d2_pred = d2;
                        //if (d3_pred == 0) d3_pred = d3;
                        //if (d4_pred == 0) d4_pred = d4;
                        //if (d5_pred == 0) d5_pred = d5;
                        //bool blSave = false;

                        //try
                        //{

                        //    if (d1_pred != d1)
                        //    {
                        //        blSave = true;
                        //        dtPerevalkids.Rows.Add(DateTime.Now, d1, 0, 0, 0, 0);
                        //    }
                        //    if (d2_pred != d2)
                        //    {
                        //        blSave = true;
                        //        dtPerevalkids.Rows.Add(DateTime.Now, 0, d2, 0, 0, 0);
                        //    }
                        //    if (d3_pred != d3)
                        //    {
                        //        blSave = true;
                        //        dtPerevalkids.Rows.Add(DateTime.Now, 0, 0, d3, 0, 0);
                        //    }
                        //    if (d4_pred != d4)
                        //    {
                        //        blSave = true;
                        //        dtPerevalkids.Rows.Add(DateTime.Now, 0, 0, 0, d4, 0);
                        //    }
                        //    if (d5_pred != d5)
                        //    {
                        //        blSave = true;
                        //        dtPerevalkids.Rows.Add(DateTime.Now, 0, 0, 0, 0, d5);
                        //    }

                           
                        //}
                        //catch (Exception ex)
                        //{
                        //    //ошибка
                        //    Program.messageErrorDsValki = "Ошибка формировании таблицы валков - " + ex.Message;
                        //    Program.dtErrorDsValki = DateTime.Now; ;
                        //}



                        //d1_pred = d1;
                        //d2_pred = d2;
                        //d3_pred = d3;
                        //d4_pred = d4;
                        //d5_pred = d5;



                        //#region Перевалки сохраняем в БД
                        //if (blSave)
                        //{
                        //    string strTableNamePerevalki = "dsPerevalki" + DateTime.Now.ToString("yyyyMM");
                        //    string comBDPerevalki = "if not exists (select * from sysobjects where name='" + strTableNamePerevalki + "' and xtype='U') create table " + strTableNamePerevalki +
                        //            "(" +
                        //            "dtPerevalki datetime NOT NULL, " +
                        //            "kl1 int NOT NULL, " +
                        //            "kl2 int NOT NULL, " +
                        //            "kl3 int NOT NULL, " +
                        //            "kl4 int NOT NULL, " +
                        //            "kl5 int NOT NULL )";

                        //    //создаем таблицу значений Перевалок
                        //    using (SqlConnection conPerevalki1 = new SqlConnection(connectionString))
                        //    {
                        //        try
                        //        {
                        //            conPerevalki1.Open();
                        //            SqlCommand command = new SqlCommand(comBDPerevalki, conPerevalki1);
                        //            command.ExecuteNonQuery();
                        //            conPerevalki1.Close();
                        //            //messageErrorValki = "OK формировании таблицы валков";

                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            Program.messageErrorDsValki = "Ошибка формировании таблицы валков " + ex.Message;
                        //            Program.dtErrorDsValki = DateTime.Now;

                        //        }


                        //    }
                        //    //записываем в таблицу прокатанного рулона данные по прокатке этого рулона
                        //    using (SqlConnection conPerevalki2 = new SqlConnection(connectionString))
                        //    {
                        //        try
                        //        {
                        //            conPerevalki2.Open();
                        //            using (var bulk = new SqlBulkCopy(conPerevalki2))
                        //            {
                        //                bulk.DestinationTableName = strTableNamePerevalki;
                        //                bulk.WriteToServer(dtPerevalkids);
                        //                Program.messageOKDsValki = "Данные перевалки в таблицу " + strTableNamePerevalki + " записаны ";
                        //                Program.dtOKDsValki = DateTime.Now;


                        //                dtPerevalkids.Clear(); //очистка таблицы 
                        //            }
                        //            conPerevalki2.Close();



                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            Program.messageErrorDsValki = "Ошибка записи в таблицу валков " + ex.Message;
                        //            Program.dtErrorDsValki = DateTime.Now;

                        //        }
                        //    }

                        //}

                        //#endregion

                        ////Console.WriteLine(d1_pred + "-" + d2_pred + "-" + d3_pred + "-" + d4_pred + "-" + d5_pred);

                        //#endregion


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
