using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConfigEditor.Model
{
    public class IniFile
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, IntPtr RetVal, int Size, string FilePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileSectionNames(IntPtr buffer, int bufferSize, string filePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
        public string[] GetKeys(string Section = null)
        {
            IntPtr pMem = Marshal.AllocHGlobal(4096 * sizeof(char));
            string temp = "";

            int count = GetPrivateProfileString(Section ?? EXE, null, null, pMem, 4096 * sizeof(char), Path) - 1;
            if (count > 0)
                temp = Marshal.PtrToStringUni(pMem, count);

            Marshal.FreeHGlobal(pMem);

            return temp.Split('\0');
        }

        public List<string> GetSections()
        {
            List<string> sections = new List<string>();
            IntPtr buffer = Marshal.AllocCoTaskMem(65536); // максимальный размер буфера
            try
            {
                int bufferSize = GetPrivateProfileSectionNames(buffer, 65536, Path);
                if (bufferSize > 0)
                {
                    string[] sectionNames = Marshal.PtrToStringAuto(buffer, bufferSize).Split('\0');
                    foreach (string sectionName in sectionNames)
                    {
                        if (!string.IsNullOrEmpty(sectionName))
                        {
                            sections.Add(sectionName);
                        }
                    }
                }
            }
            finally
            {
                Marshal.FreeCoTaskMem(buffer);
            }
            return sections;
        }


        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}
