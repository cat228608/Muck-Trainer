using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Memory
{
    public class Mem
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(int hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        private Process process;
        public IntPtr processHandle;

        public bool Attach(string procName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(procName.Replace(".exe", ""));
                if (processes.Length > 0)
                {
                    process = processes[0];
                    processHandle = OpenProcess(0x1F0FFF, false, process.Id);
                    return !process.HasExited;
                }
                return false;
            }
            catch { return false; }
        }

        public ProcessModule GetModule(string moduleName)
        {
            if (process == null || process.HasExited) return null;
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                {
                    return module;
                }
            }
            return null;
        }

        public byte[] ReadBytes(long address, int length)
        {
            byte[] buffer = new byte[length];
            int bytesRead = 0;
            ReadProcessMemory((int)processHandle, address, buffer, buffer.Length, ref bytesRead);
            return buffer;
        }

        public bool WriteBytes(long address, byte[] bytes)
        {
            int bytesWritten = 0;
            return WriteProcessMemory((int)processHandle, address, bytes, bytes.Length, ref bytesWritten);
        }

        public long ReadInt64(long address) => BitConverter.ToInt64(ReadBytes(address, 8), 0);
        public float ReadFloat(long address) => BitConverter.ToSingle(ReadBytes(address, 4), 0);
        public double ReadDouble(long address) => BitConverter.ToDouble(ReadBytes(address, 8), 0);
        public byte ReadByte(long address) => ReadBytes(address, 1)[0];
        public bool WriteFloat(long address, float value) => WriteBytes(address, BitConverter.GetBytes(value));
        public bool WriteDouble(long address, double value) => WriteBytes(address, BitConverter.GetBytes(value));
        public bool WriteByte(long address, byte value) => WriteBytes(address, new byte[] { value });

        public int ReadInt32(long address)
        {
            return BitConverter.ToInt32(ReadBytes(address, 4), 0);
        }

        public string ReadString(long address, string encoding, int length)
        {
            byte[] buffer = ReadBytes(address, length);
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == 0)
                {
                    return Encoding.ASCII.GetString(buffer, 0, i);
                }
            }
            return Encoding.ASCII.GetString(buffer);
        }
    }
}