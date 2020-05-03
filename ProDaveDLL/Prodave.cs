using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace HWDiag
{
    public class Prodave
    {
        //int LoadConnection_ex6 (unsigned short ConNr, char* pAccessPoint, unsigned short ConTableLen, CON_TABLE_TYPE * pConTable);
        [DllImport("Prodave6.dll", EntryPoint = "LoadConnection_ex6", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int LoadConnection_ex6(ushort ConNr, byte[] pAccessPoint, ushort ConTableLen, byte[] pConTable);

        public int LoadConnection(int ConNr, int MPIAddr, int SlotNr, int RackNr)
        {
            byte[] mpi = new byte[1];

            mpi[0] = (byte)MPIAddr;

            return LoadConnection(ConNr, 1, mpi, SlotNr, RackNr);
        }

        public int LoadConnection(int ConNr, int AdrType, byte[] Addr, int SlotNr, int RackNr)
        {
            byte[] ap = { 0x53, 0x37, 0x4f, 0x4e, 0x4c, 0x49, 0x4e, 0x45, 0x00 }; // "S7ONLINE"
            byte[] cont = new byte[18];
            int i,res;
            bool err = false;

            //for (i = 0; i < 18; i++) cont[i] = 0;
            switch (AdrType)
            {
                case 1: // MPI
                    if (Addr.Length > 0) cont[0] = Addr[0]; else err = true;
                    break;
                case 2: // IP
                    if (Addr.Length > 3) for (i = 0; i < 4; i++) cont[i] = Addr[i]; else err = true;
                    break;
                case 3: // MAC
                    if (Addr.Length > 5) for (i = 0; i < 6; i++) cont[i] = Addr[i]; else err = true;
                    break;
                default:
                    err = true;
                    break;
            }
            cont[6] = (byte)AdrType;
            cont[7] = (byte)SlotNr;
            cont[8] = (byte)RackNr;

            if (err) return 0x0312;

            try
            {
                res = LoadConnection_ex6((ushort)ConNr, ap, (ushort)cont.Length, cont);
            }
            catch
            {
                res = 0xFFFF;
            }

            return res;
        }

        
        //int UnloadConnection_ex6 (unsigned short ConNr);
        [DllImport("Prodave6.dll", EntryPoint = "UnloadConnection_ex6", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int UnloadConnection_ex6(ushort ConNr);

        public int UnloadConnection(int ConNr)
        {
            int res;

            try
            {
                res = UnloadConnection_ex6((ushort)ConNr);
            }
            catch
            {
                res = 0xFFFF;
            }

            return res;
        }


        //int SetActiveConnection_ex6 (unsigned short ConNr);
        [DllImport("Prodave6.dll", EntryPoint = "SetActiveConnection_ex6", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int SetActiveConnection_ex6(ushort ConNr);

        public int SetActiveConnection(int ConNr)
        {
            int res;

            try
            {
                res = SetActiveConnection_ex6((ushort)ConNr);
            }
            catch
            {
                res = 0xFFFF;
            }

            return res;
        }

        
        //int GetPduSize_ex6(unsigned short ConNr);
        [DllImport("Prodave6.dll", EntryPoint = "GetPduSize_ex6", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int GetPduSize_ex6(ushort ConNr);

        public int GetPduSize(int ConNr)
        {
            int res;

            try
            {
                res = GetPduSize_ex6((ushort)ConNr);
            }
            catch
            {
                res = 0xFFFF;
            }

            return res;
        }


        //int gp_2_float_ex6(ULONG gp, float* pieee);
        [DllImport("Prodave6.dll", EntryPoint = "gp_2_float_ex6", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int gp_2_float_ex6(uint gp, out float pieee);

        public int gp_2_float(uint gp, out float pieee)
        {
            int res;
            float val = 0;

            try
            {
                res = gp_2_float_ex6(gp, out val);
            }
            catch
            {
                res = 0xFFFF;
            }

            pieee = val;
            return res;
        }

        //int db_buch_ex6 (unsigned long BufLen, unsigned short * pBuchBuffer, unsigned long * pDatLen);
        [DllImport("Prodave6.dll", EntryPoint = "db_buch_ex6", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int db_buch_ex6(uint BufLen, [In, Out] ushort[] pBuchBuffer, out uint pDatLen);

        public int db_buch(int BufLen, out ushort[] pBuffer, out int pDatLen)
        {
            int res;
            uint len = 0;
            ushort[] buf = new ushort[BufLen];

            try
            {
                res = db_buch_ex6((uint)BufLen, buf, out len);
            }
            catch
            {
                res = 0xFFFF;
            }

            pBuffer = buf;
            pDatLen = (int)len;
            return res;
        }


        //int get_pb_lifelist_ex6 (unsigned long BufLen, unsigned char * pBuffer, unsigned long * pDatLen);
        [DllImport("Prodave6.dll", EntryPoint = "get_pb_lifelist_ex6", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int get_pb_lifelist_ex6(uint BufLen, [In, Out] byte[] pBuffer, out uint pDatLen);

        public int get_pb_lifelist(int BufLen, out byte[] pBuffer, out int pDatLen)
        {
            int res;
            uint len = 0;
            byte[] buf = new byte[BufLen];

            try
            {
                res = get_pb_lifelist_ex6((uint)BufLen, buf, out len);
            }
            catch
            {
                res = 0xFFFF;
            }

            pBuffer = buf;
            pDatLen = (int)len;
            return res;
        }


        //int get_ie_lifelist_ex6 (unsigned long BufLen, unsigned char * pBuffer, unsigned long * pDatLen);
        [DllImport("Prodave6.dll", EntryPoint = "get_ie_lifelist_ex6", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int get_ie_lifelist_ex6(uint BufLen, [In, Out] byte[] pBuffer, out uint pDatLen);

        public int get_ie_lifelist(int BufLen, out byte[] pBuffer, out int pDatLen)
        {
            int res;
            uint len = 0;
            byte[] buf = new byte[BufLen];

            try
            {
                res = get_ie_lifelist_ex6((uint)BufLen, buf, out len);
            }
            catch
            {
                res = 0xFFFF;
            }

            pBuffer = buf;
            pDatLen = (int)len;
            return res;
        }


        //int field_read_ex6 (char FieldType, unsigned short BlkNr, unsigned short StartNr, unsigned long Amount, unsigned long BufLen, unsigned char * pBuffer, unsigned long * pDatLen);
        [DllImport("Prodave6.dll", EntryPoint = "field_read_ex6", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int field_read_ex6(byte FieldType, ushort BlkNr, ushort StartNr, uint Amount, uint BufLen, [In, Out] byte[] pBuffer, out uint pDatLen);

        public int field_read(char FieldType, int BlkNr, int StartNr, int Amount, out byte[] pBuffer, out int pDatLen)
        {
            int res;
            uint len = 0;
            byte[] buf = new byte[Amount];

            try
            {
                res = field_read_ex6(Convert.ToByte(FieldType), (ushort)BlkNr, (ushort)StartNr, (uint)Amount, (uint)(buf.Length), buf, out len);
            }
            catch
            {
                res = 0xFFFF;
            }

            pBuffer = buf;
            pDatLen = (int)len;
            return res;
        }


        //int bst_read_ex6 (char FieldType, unsigned short BlkNr, unsigned short StartNr, unsigned long Amount, unsigned long BufLen, unsigned char * pBuffer, unsigned long * pDatLen);
        [DllImport("Prodave6.dll", EntryPoint = "bst_read_ex6", ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int bst_read_ex6(byte FieldType, ushort BlkNr, ushort StartNr, uint Amount, uint BufLen, [In, Out] byte[] pBuffer, out uint pDatLen);

        public int bst_read(char FieldType, int BlkNr, int StartNr, int Amount, out byte[] pBuffer, out int pDatLen)
        {
            int res;
            uint len = 0;
            byte[] buf = new byte[Amount];

            try
            {
                res = bst_read_ex6(Convert.ToByte(FieldType), (ushort)BlkNr, (ushort)StartNr, (uint)Amount, (uint)(buf.Length), buf, out len);
            }
            catch
            {
                res = 0xFFFF;
            }

            pBuffer = buf;
            pDatLen = (int)len;
            return res;
        }


        public string Error(int err)
        {
	        string text="";

	        switch(err)
	        {
		        case 0x0000: text = "Success"; break;
		        case 0x0101: text = "Connection not established / no parameters assigned"; break;
		        case 0x0112: text = "Incorrect parameter"; break;
		        case 0x0113: text = "Invalid block type"; break;
		        case 0x0114: text = "Block not found"; break;
		        case 0x0115: text = "Block already exists"; break;
		        case 0x0116: text = "Block is write-protected"; break;
		        case 0x0117: text = "The block is too large"; break;
		        case 0x0118: text = "Invalid block number"; break;
		        case 0x0119: text = "Incorrect password entered"; break;
		        case 0x011A: text = "PG resource error"; break;
		        case 0x011B: text = "PLC resource error"; break;
		        case 0x011C: text = "Internal error: Protocol error"; break; 
		        case 0x011D: text = "Too many blocks (module-related restriction"; break;
		        case 0x011E: text = "No connection to database or S7DOS handle invalid"; break;
		        case 0x011F: text = "Result buffer too small"; break;
		        case 0x0120: text = "End of block list"; break;
		        case 0x0140: text = "Insufficient memory available"; break; 
		        case 0x0141: text = "Job cannot be processed because resources are missing"; break; 
		        case 0x0170: text = "The simulator could not be found"; break;
		        case 0x0180: text = "The activated online function cannot be executed"; break; 
		        case 0x0181: text = "Driver already open or too many open channels"; break;
		        case 0x01C1: text = "The field structure of the current database does not match the expected format"; break; 
		        case 0x01C3: text = "The length specification in the block header does not match the actual length of a section in the database"; break; 
		        case 0x01C4: text = "A problem occurred processing the last ID file"; break;
		        case 0x01C5: text = "Incorrect block format, the block does not have a valid PG format"; break; 
		        case 0x01C6: text = "File not found"; break;
		        case 0x01C7: text = "Invalid operating system update components"; break; 
		        case 0x01C8: text = "The database given as the destination already exists"; break; 
		        case 0x01C9: text = "The database is already blocked by another application"; break;
		        case 0x01CC: text = "A DLL could not be loaded"; break;
		        case 0x01CD: text = "A function could not be found in the dynamically reloaded DLL"; break; 
		        case 0x01CF: text = "File not found or access protection violation"; break;
		        case 0x01D0: text = "Password not found"; break;
		        case 0x01E0: text = "Diagnostics not activated"; break;
		        case 0x01E1: text = "No diagnostic data are available"; break;
		        case 0x01E2: text = "The diagnostic data are inconsistent"; break;
		        case 0x01F0: text = "Function not implemented"; break;
		        case 0x01FF: text = "System error"; break;
		        case 0x0312: text = "Wrong configuration parameters"; break;
		        case 0x0315: text = "MPI address error"; break;
		        case 0x0805: text = "S7 protocol: Invalid flags"; break; 
		        case 0x0810: text = "S7 protocol: The data could not be sent correctly"; break; 
		        case 0x0811: text = "S7 protocol: No job found for the received data"; break;
		        case 0x0D80: text = "A diagnostic error occurred"; break;
		        case 0x400A: text = "The communications server could not be started"; break;
		        case 0x400F: text = "The connection to the communications server has been aborted"; break; 
		        case 0x401F: text = "No such request in asynchronous list"; break;
		        case 0x4020: text = "No local memory available"; break;
		        case 0x4021: text = "The local memory cannot be locked"; break;
		        case 0x4022: text = "No reply to STEP 7 message frame"; break;
		        case 0x4023: text = "Messages are no longer executed by the CPU. The application does not react"; break; 
		        case 0x4060: text = "Wrong Windows mode"; break;
		        case 0x4061: text = "No global memory"; break;
		        case 0x4104: text = "Online: No resources available in the driver"; break; 
		        case 0x4107: text = "Online: Connection is closed"; break;
		        case 0x4109: text = "Online: No acknowledgment of the sending and receiving of data"; break; 
		        case 0x410E: text = "Online: Connection aborted"; break;
		        case 0x4110: text = "Connection can not be established. No response from remote partner.\r\n\r\nSet up your PG/PC-Interface properly and try again"; break; 
		        case 0x4114: text = "Online: Connection already exists"; break;
		        case 0x4116: text = "Online: The connection to the target module cannot be established"; break; 
		        case 0x411A: text = "Online: Illegal address"; break;
		        case 0x411C: text = "Online: Network error"; break;
		        case 0x4201: text = "Online: No resources available in the driver"; break; 
		        case 0x4211: text = "Online: No other station with master capabilities could be found in the subnet"; break; 
		        case 0x4212: text = "Online: Station not online"; break;
		        case 0x4215: text = "Online: Function not implemented or not permitted in current context"; break; 
		        case 0x4216: text = "Online: Invalid DP slave station address or error message from DP slave"; break;
		        case 0x4228: text = "The bus parameters could not be automatically determined (online.\r\nThere are no stations on the bus sending bus parameter message frames.\r\nSet the MPI/PROFIBUS interface manually"; break;
		        case 0x4230: text = "Online: No other active partner can be found"; break;
		        case 0x4231: text = "Online: Bus faulty"; break;
		        case 0x4232: text = "Online: Incorrect highest node address"; break; 
		        case 0x4233: text = "Online: System error"; break;
		        case 0x42A1: text = "Online: Cannot initialize or open COM interface"; break; 
		        case 0x42B0: text = "Online: Hardware not found"; break;
		        case 0x42B1: text = "Online: The local programming device interface is defective"; break; 
		        case 0x42B2: text = "Online: Driver configuration error or invalid registry parameter"; break;
		        case 0x42B3: text = "Online: The local MPI address for the programming device/PC is higher than the maximum node address\r\nor the wrong transmission rate or wrong interrupt vector is set"; break;
		        case 0x42B5: text = "Online: The set local node address is already in use"; break;
		        case 0x42B6: text = "Online: The configured hardware interface is already being used by another programming package.\r\nClose all S7 applications and restart your programming package"; break;
		        case 0x42B7: text = "Online: The set interrupt vector (IRQ is not available for this module"; break;
		        case 0x42B8: text = "Online: The set interrupt vector (IRQ is already in use"; break;
		        case 0x42C0: text = "Online: Cannot load the selected communication driver; File not found"; break; 
		        case 0x42C1: text = "Online: Function not implemented in loaded communication driver"; break;
		        case 0x42C2: text = "A connection between your PC/programming device and the PLC cannot be established"; break; 
		        case 0x42D0: text = "Online: Incompatible adapter version or wrong adapter type connected"; break;
		        case 0x42D1: text = "Online: No interrupt received from PC/MPI cable"; break;
		        case 0x42D2: text = "Online: Communication link to the adapter damaged"; break;
		        case 0x42D3: text = "Online: COM port not configured under Windows"; break;
		        case 0x42D4: text = "Online: COM port currently not accessible"; break;
		        case 0x42D5: text = "Online: The serial driver is currently being used by an application with another configuration"; break; 
		        case 0x42D6: text = "Online: The TS adapter interface is set up for a modem connection and there is no remote connection to a TS adapter"; break; 
		        case 0x42D7: text = "Online: The TS adapter refused the job as the necessary legitimization was missing"; break;
		        case 0x42D9: text = "The connection was not established because another interface is already active on the TS adapter"; break;
		        case 0x42E0: text = "Online: Windows system error in communication driver"; break;
		        case 0x42EE: text = "Online: No global memory available"; break;
		        case 0x42EF: text = "Online: SIN_SERV not started"; break;
		        case 0x42FA: text = "Online: Station is not online"; break;
		        case 0x4305: text = "Invalid S7 transport address buffer"; break;
		        case 0x4306: text = "Job cannot be found. Wrong wUserID or wrong Windows handle"; break; 
		        case 0x4430: text = "Cannot read/write data record"; break;
		        case 0x4501: text = "Incorrect transfer parameter(s) (for example, incorrect structure Version, Pointer ZERO, etc.)"; break;
		        case 0x4510: text = "Direct connection with TS Adapter in modem mode"; break;
		        case 0x7000: text = "General address error"; break;
		        case 0x7010: text = "ConTableLen smaller or 0"; break;
		        case 0x7011: text = "Connection number already assigned parameters.\r\nPlease restart SCAUT Recorder."; break;
		        case 0x7012: text = "Range violation of connection number"; break;
		        case 0x7015: text = "Connection not loaded"; break;
		        case 0x7016: text = "Illegal MPI/Profibus/PPI address"; break;
		        case 0x7017: text = "Illegal MPI address"; break;
		        case 0x7018: text = "Illegal MPI address"; break;
		        case 0x7020: text = "No memory available"; break;
		        case 0x7025: text = "An attribute has the wrong/no value"; break;
		        case 0x7030: text = "Warning: Active connection number unloaded!!!"; break;
		        case 0x7040: text = "Buffer too small"; break;
		        case 0x7041: text = "PDU smaller than data buffer, choose smaller buffer"; break;
		        case 0x7042: text = "PDU smaller than amount, choose smaller amount"; break;
		        case 0x7043: text = "Wrong bit number (too small/large)"; break;
		        case 0x7045: text = "Error when reading data"; break;
		        case 0x7046: text = "Error when writing data"; break;
		        case 0x7050: text = "Error when reading DB"; break;
		        case 0x7051: text = "Error when writing DB"; break;
		        case 0x7060: text = "Error when reading a block"; break;
		        case 0x7061: text = "Error when writing a block"; break;
		        case 0x7062: text = "Too many data are to be read"; break;
		        case 0x7064: text = "Too many data are to be written"; break;
		        case 0x7065: text = "No block found"; break;
		        case 0x7066: text = "Block limit exceeded"; break;
		        case 0x7070: text = "No filestream, file not found"; break;
		        case 0x7071: text = "Permitted amount of error messages exceeded"; break;
		        case 0x7072: text = "Wrong entry in Error.dat"; break;
		        case 0x7080: text = "Info cannot be read"; break;
		        case 0x7081: text = "Hardware version and Firmware version cannot be read"; break;
		        case 0x7082: text = "MLFB cannot be read"; break;
		        case 0x7085: text = "Status cannot be read"; break;
		        case 0x7090: text = "A newer Block already exists"; break;
		        case 0x7100: text = "Block is password-protected, cannot be read"; break;
		        case 0x8001: text = "(8001) The service requested cannot be performed while the block is in the current state;\r\nother block functions are therefore not possible.\r\nRepeat the function later"; break; 
		        case 0x8003: text = "(8003) S7 protocol error: Error occurred while transferring block"; break;
		        case 0x8100: text = "(8100) Application, general error: Service unknown to remote module"; break;
		        case 0x8104: text = "(8104) This service is not implemented on the module, or a message frame error has been registered"; break; 
		        case 0x8204: text = "(8204) The type specification for the object is inconsistent"; break;
		        case 0x8205: text = "(8205) A copied block already exists and is not linked"; break;
		        case 0x8301: text = "(8301) Insufficient memory space or work memory on the module, or specified storage medium not accessible"; break; 
		        case 0x8302: text = "(8302) Too few resources available or the processor resources are not available"; break;
		        case 0x8304: text = "(8304) No further parallel upload possible. Too few resources"; break;
		        case 0x8305: text = "(8305) Function not available"; break;
		        case 0x8306: text = "(8306) Insufficient work memory (for copying, linking, loading AWP)"; break;
		        case 0x8307: text = "(8307) Available work memory not enough  (for copying, linking, loading AWP)"; break;
		        case 0x8401: text = "(8401) S7 protocol error: Invalid service sequence (for example, loading or uploading a block)"; break; 
		        case 0x8402: text = "(8402) Service cannot be performed owing to state of addressed object"; break;
		        case 0x8404: text = "(8404) S7 protocol: The function cannot be performed"; break;
		        case 0x8405: text = "(8405) Remote block is in DISABLE state (CFB). The function cannot be performed"; break; 
		        case 0x8500: text = "(8500) S7 protocol error: Wrong message frame"; break;
		        case 0x8503: text = "(8503) Message from the module: Service canceled prematurely"; break; 
		        case 0x8701: text = "(8701) Error addressing the object in the communications partner (for example, area length error)"; break; 
		        case 0x8702: text = "(8702) The requested service is not supported by the module"; break;
		        case 0x8703: text = "(8703) Access to object refused"; break;
		        case 0x8704: text = "(8704) Access error: Object damaged"; break;
		        case 0xD001: text = "(D001) Protocol error: Illegal job number"; break;
		        case 0xD002: text = "(D002) Parameter error: Illegal job variant"; break;
		        case 0xD003: text = "(D003) Parameter error: Debugging function not supported by module"; break; 
		        case 0xD004: text = "(D004) Parameter error: Illegal job status"; break;
		        case 0xD005: text = "(D005) Parameter error: Illegal job termination"; break;
		        case 0xD006: text = "(D006) Parameter error: Illegal link disconnection ID"; break;
		        case 0xD007: text = "(D007) Parameter error: Illegal number of buffer elements"; break;
		        case 0xD008: text = "(D008) Parameter error: Illegal scan rate"; break;
		        case 0xD009: text = "(D009) Parameter error: Illegal number of executions"; break; 
		        case 0xD00A: text = "(D00A) Parameter error: Illegal trigger event. Check whether the specified trigger is permitted on this module"; break; 
		        case 0xD00B: text = "(D00B) Parameter error: Illegal trigger condition. Check whether the specified trigger is permitted on this module"; break;
		        case 0xD011: text = "(D011) Parameter error in path of the call environment: Block does not exist"; break;
		        case 0xD012: text = "(D012) Parameter error: Wrong address in block"; break;
		        case 0xD014: text = "(D014) Parameter error: Block being deleted/overwritten"; break; 
		        case 0xD015: text = "(D015) Parameter error: Illegal variable address"; break;
		        case 0xD016: text = "(D016) Parameter error: Test jobs not possible, because of errors in user program"; break; 
		        case 0xD017: text = "(D017) Parameter error: Illegal trigger number"; break;
		        case 0xD025: text = "(D025) Parameter error: Incorrect path"; break;
		        case 0xD026: text = "(D026) Parameter error: Incorrect access type"; break;
		        case 0xD027: text = "(D027) Parameter error: Invalid number of data blocks"; break;
		        case 0xD031: text = "(D031) Internal protocol error"; break;
		        case 0xD032: text = "(D032) Parameter error: Wrong result buffer length"; break; 
		        case 0xD033: text = "(D033) Protocol error: Wrong job length"; break;
		        case 0xD03F: text = "(D03F) Coding error: Error in parameter section (for example, reserve bytes not equal to 0)"; break; 
		        case 0xD041: text = "(D041) Data error: Illegal status list ID"; break;
		        case 0xD042: text = "(D042) Data error: Illegal variable address"; break;
		        case 0xD043: text = "(D043) Data error: Referenced job not found, check job data"; break; 
		        case 0xD044: text = "(D044) Data error: Illegal variable value, check job data"; break;
		        case 0xD045: text = "(D045) Data error: Leaving the BASP control (ODIS) is not allowed in HOLD"; break; 
		        case 0xD046: text = "(D046) Data error: Illegal measuring stage during run-time measurement"; break;
		        case 0xD047: text = "(D047) Data error: Illegal hierarchy in 'Read job list'"; break;
		        case 0xD048: text = "(D048) Data error: Illegal deletion code in 'Delete job'"; break;
		        case 0xD049: text = "(D049) Invalid substitute ID in 'Replace job'"; break;
		        case 0xD04A: text = "(D04A) Error executing 'program status'"; break;
		        case 0xD05F: text = "(D05F) Coding error: Error in data section (for example, reserve bytes not equal to 0, ...)"; break;
		        case 0xD061: text = "(D061) Resource error: No memory space for job"; break;
		        case 0xD062: text = "(D062) Resource error: Job list full"; break;
		        case 0xD063: text = "(D063) Resource error: Trigger event occupied"; break; 
		        case 0xD064: text = "(D064) Resource error: Memory space too small for one result buffer element"; break; 
		        case 0xD065: text = "(D065) Resource error: Memory space too small for a number of result buffer elements"; break; 
		        case 0xD066: text = "(D066) Resource error: The timer available for run-time measurement is occupied by another job"; break; 
		        case 0xD067: text = "(D067) Resource error: Too many 'modify variable' jobs active (including multi-processor operation)"; break;
		        case 0xD081: text = "(D081) Function not permitted in current mode"; break;
		        case 0xD082: text = "(D082) Mode error: Cannot exit HOLD mode"; break;
		        case 0xD0A1: text = "(D0A1) Function not permitted in current protection level"; break; 
		        case 0xD0A2: text = "(D0A2) Function not possible at present, because a memory-modifying function is running"; break; 
		        case 0xD0A3: text = "(D0A3) Too many 'modify variable' jobs active on the I/O (including multi-processor operation)"; break;
		        case 0xD0A4: text = "(D0A4) 'Forcing' has already been established"; break;
		        case 0xD0A5: text = "(D0A5) Referenced job not found"; break;
		        case 0xD0A6: text = "(D0A6) Job cannot be disabled/enabled"; break;
		        case 0xD0A7: text = "(D0A7) Job cannot be deleted because it is currently being read, for example. Try again"; break; 
		        case 0xD0A8: text = "(D0A8) Job cannot be replaced because it is currently being read or deleted, for example. Try again"; break; 
		        case 0xD0A9: text = "(D0A9) Job cannot be read because it is currently being deleted, for example. Try again"; break;
		        case 0xD0AA: text = "(D0AA) Time limit exceeded in processing operation"; break;
		        case 0xD0AB: text = "(D0AB) Invalid job parameters in process operation"; break;
		        case 0xD0AC: text = "(D0AC) Invalid job data in process operation"; break;
		        case 0xD0AD: text = "(D0AD) Operating mode already set"; break;
		        case 0xD0AE: text = "(D0AE) The job was set up over a different connection and can be handled over this connection"; break; 
		        case 0xD0C1: text = "(D0C1) At least one error has been detected while accessing the variable(s)"; break;
		        case 0xD0C2: text = "(D0C2) Mode transition to STOP/HOLD"; break;
		        case 0xD0C3: text = "(D0C3) At least one error has been detected while accessing the variable(s). Mode transition to STOP/HOLD"; break; 
		        case 0xD0C4: text = "(D0C4) Time-out during run-time measurement"; break;
		        case 0xD0C5: text = "(D0C5) Display of block stack inconsistent, because blocks were deleted/reloaded"; break;
		        case 0xD0C6: text = "(D0C6) Job was automatically deleted as the jobs it referenced have been deleted"; break;
		        case 0xD0C7: text = "(D0C7) Job was automatically deleted because STOP mode was exited"; break;
		        case 0xD0C8: text = "(D0C8) 'Block status' aborted because of inconsistencies between test job and running program"; break; 
		        case 0xD0C9: text = "(D0C9) Exit the status area by resetting OB90"; break;
		        case 0xD0CA: text = "(D0CA) Exit the status area by resetting OB90 and access error reading variables before exiting"; break; 
		        case 0xD0CB: text = "(D0CB) The output disable for the peripheral outputs has been activated again"; break;
		        case 0xD0CC: text = "(D0CC) The number of data for the debugging functions is restricted by the time limit"; break;
		        case 0xD201: text = "(D201) Syntax error in block name"; break;
		        case 0xD202: text = "(D202) Syntax error in function parameters"; break; 
		        case 0xD205: text = "(D205) Linked block already exists in RAM: Conditional copying is not possible"; break;
		        case 0xD206: text = "(D206) Linked block already exists in EPROM: Conditional copying is not possible"; break;
		        case 0xD208: text = "(D208) Maximum number of copied (not linked) blocks on module exceeded"; break;
		        case 0xD209: text = "(D209) (At least) one of the given blocks not found on the module"; break;
		        case 0xD20A: text = "(D20A) Maximum number of blocks linkable with a job exceeded"; break;
		        case 0xD20B: text = "(D20B) Maximum number of blocks deletable with a job exceeded"; break;
		        case 0xD20C: text = "(D20C) OB cannot be copied as the associated priority class does not exist"; break;
		        case 0xD20D: text = "(D20D) SDB cannot be interpreted (for example, unknown number)"; break;
		        case 0xD20E: text = "(D20E) No (further) block available"; break;
		        case 0xD20F: text = "(D20F) Module-specific maximum block size exceeded"; break;
		        case 0xD210: text = "(D210) Invalid block number"; break;
		        case 0xD212: text = "(D212) Incorrect header attribute (run-time relevant)"; break;
		        case 0xD213: text = "(D213) Too many SDBs. Note the restrictions on the module being used"; break; 
		        case 0xD216: text = "(D216) Invalid user program - reset module"; break;
		        case 0xD217: text = "(D217) Protection level specified in module properties not permitted"; break;
		        case 0xD218: text = "(D218) Incorrect attribute (active/passive)"; break;
		        case 0xD219: text = "(D219) Incorrect block lengths (for example, incorrect length of first section or of the whole block)"; break;
		        case 0xD21A: text = "(D21A) Incorrect local data length or write-protection code faulty"; break;
		        case 0xD21B: text = "(D21B) Module cannot compress or compression was interrupted early. Note the restrictions on the module being used"; break;
		        case 0xD21D: text = "(D21D) The volume of dynamic project data transferred is illegal.\r\nIt does not match the CPU configuration or the current user program.\r\nCheck your settings and then save them again"; break; 
		        case 0xD21E: text = "(D21E) Unable to assign parameters to a module (such as FM, CP). The system data could not be linked.\r\nFor more information see the diagnostic buffer"; break;
		        case 0xD220: text = "(D220) Invalid programming language. Note the restrictions on the module being used"; break;
		        case 0xD221: text = "(D221) The system data for connections or routing are not valid. The clock parameters could have incorrect settings"; break; 
		        case 0xD222: text = "(D222) The system data of the global data definition contain invalid parameters.\r\nNote the restrictions on the module being used and check your configuration"; break;
		        case 0xD223: text = "(D223) Error in instance data block for communication function block or maximum number of instance DBs exceeded.\r\nCheck the programming and note the restrictions on the module being used"; break;
		        case 0xD224: text = "(D224) The SCAN system data block contains invalid parameters"; break;
		        case 0xD225: text = "(D225) The DP system data block contains invalid parameters"; break;
		        case 0xD226: text = "(D226) A structural error occurred in a block"; break;
		        case 0xD230: text = "(D230) A structural error occurred in a block"; break;
		        case 0xD231: text = "(D231) At least one loaded OB cannot be copied as the associated priority class does not exist"; break;
		        case 0xD232: text = "(D232) At least one block number of a loaded block is illegal"; break;
		        case 0xD234: text = "(D234) Block already exists in the given memory medium or in the job"; break;
		        case 0xD235: text = "(D235) The block contains an incorrect checksum"; break;
		        case 0xD236: text = "(D236) The block does not contain a checksum"; break;
		        case 0xD237: text = "(D237) You are about to load the block twice, i.e. a block with the same time stamp already exists on the CPU"; break;
		        case 0xD238: text = "(D238) At least one of the blocks specified is not a DB"; break;
		        case 0xD239: text = "(D239) At least one of the DBs specified is not available as a linked variant in the load memory"; break;
		        case 0xD23A: text = "(D23A) At least one of the DBs specified is considerably different from the copied and linked variant"; break;
		        case 0xD240: text = "(D240) Coordination rules violated"; break;
		        case 0xD241: text = "(D241) The function is not permitted in the current protection level"; break;
		        case 0xD242: text = "(D242) Protection violation while processing F blocks.\r\nF blocks can be processed only after a password has been entered. F block SDB99 cannot be deleted"; break;
		        case 0xD250: text = "(D250) Update and module ID or version do not match"; break;
		        case 0xD251: text = "(D251) Incorrect sequence of operating system components"; break;
		        case 0xD252: text = "(D252) Checksum error"; break;
		        case 0xD253: text = "(D253) No executable loader available; update only possible via memory card"; break;
		        case 0xD254: text = "(D254) Storage error in operating system"; break;
		        case 0xD280: text = "(D280) Error compiling block in S7-300 CPU"; break;
		        case 0xD2A1: text = "(D2A1) Another block function or a trigger on a block is active. Finish executing the other online function"; break;
		        case 0xD2A2: text = "(D2A2) A trigger is active on a block. Complete the debugging function first"; break;
		        case 0xD2A3: text = "(D2A3) The block is not activated (linked) or the block is about to be deleted. Repeat the function later"; break;
		        case 0xD2A4: text = "(D2A4) The block is already being processed via another block function. Repeat the function later"; break;
		        case 0xD2A6: text = "(D2A6) It is not possible to save and change the user program simultaneously. Repeat the function later"; break;
		        case 0xD2A7: text = "(D2A7) The block has the attribute 'unlinked' or is not being processed.\r\nIt is not possible to carry out a debugging function for this block"; break;
		        case 0xD2A8: text = "(D2A8) A running debugging function is preventing parameters from being assigned to the CPU. Complete the debugging function first"; break;
		        case 0xD2A9: text = "(D2A9) New parameters are being assigned to the CPU. It is not possible to load the user program at the same time. Repeat the function later"; break;
		        case 0xD2AA: text = "(D2AA) New parameters are currently being assigned to the modules. Repeat the function later"; break;
		        case 0xD2AB: text = "(D2AB) The volume of dynamic project data is currently being changed. The user program is being re-evaluated.\r\nWait until the system has finished re-evaluating, and then repeat your job"; break;
		        case 0xD2AC: text = "(D2AC) The requested changes cannot be activated while configuring in RUN (CiR), since running jobs are not terminated yet. Execute the function later"; break;
		        case 0xD2B0: text = "(D2B0) An error occurred while configuring in RUN (CiR). The changes requested are not valid"; break;
		        case 0xD2C0: text = "(D2C0) The number of technological objects has been exceeded. Reduce the number of amount of technological objects (axes, cam,...)"; break;
		        case 0xD2C1: text = "(D2C1) The same technology data block already exists on the module"; break;
		        case 0xD2C2: text = "(D2C2) Download user program or download hardware configuration is not possible, if  Enable Peripheral Outputs is active.\r\nExit Enable Peripheral Outputs before download"; break;
		        case 0xD401: text = "(D401) Information function unavailable"; break;
		        case 0xD402: text = "(D402) Information function unavailable"; break;
		        case 0xD403: text = "(D403) Service has already been logged on/off (Diagnostics/PMC)"; break;
		        case 0xD404: text = "(D404) Maximum number of nodes reached. No more logons possible for diagnostics/PMC"; break;
		        case 0xD405: text = "(D405) Service not supported or syntax error in function parameters"; break;
		        case 0xD406: text = "(D406) Required information currently unavailable"; break;
		        case 0xD407: text = "(D407) Diagnostics error occurred"; break;
		        case 0xD408: text = "(D408) Update aborted"; break;
		        case 0xD409: text = "(D409) Error on DP bus"; break;
		        case 0xD601: text = "(D601) Syntax error in function parameter"; break;
		        case 0xD602: text = "(D602) Incorrect password entered"; break;
		        case 0xD603: text = "(D603) Link has already been legitimized"; break;
		        case 0xD604: text = "(D604) Link has already been enabled"; break;
		        case 0xD605: text = "(D605) Legitimization not possible as password does not exist"; break;
		        case 0xD801: text = "(D801) At least one variable address is invalid"; break;
		        case 0xD802: text = "(D802) Specified job does not exist"; break;
		        case 0xD803: text = "(D803) Illegal job status"; break;
		        case 0xD804: text = "(D804) Illegal cycle time (illegal time base or multiple)"; break;
		        case 0xD805: text = "(D805) Additional cyclic read job cannot be set up"; break;
		        case 0xD806: text = "(D806) The referenced job is in a state in which the requested function cannot be performed"; break;
		        case 0xD807: text = "(D807) Function aborted due to overload, meaning executing the read cycle takes longer than the set scan cycle time"; break;
		        case 0xDC01: text = "(DC01) Error in date and/or time specification"; break;
		        case 0xEF01: text = "(EF01) S7 protocol error: Error at ID2. Only 00H permitted in job"; break;
		        case 0xEF02: text = "(EF02) S7 protocol error: Error at ID2. Set of resources does not exist"; break;
		        case 0xFFFE: text = "Unknown error 0xFFFE"; break;
		        case 0xFFFF: text = "Function is not supported by the current installed Prodave version.\r\nPlease update Prodave software"; break;
	        }
            return text.Length > 0 ? "Prodave: " + text : "";
        }
    }
}
