using System;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Http.Headers;

namespace TMREAddons
{
    public static class RegEdit
    {
        private const string MAIN_FOLDERS = "HKEY_CLASSES_ROOT\nHKEY_CURRENT_USER\nHKEY_LOCAL_MACHINE\nHKEY_USERS\nHKEY_CURRENT_CONFIG";
        private static string curLoc = @"";
        private static int folder;
        public static void MainFolder(string fldr) 
        {
            for(int i = 0; i < MAIN_FOLDERS.Split('\n').Length;i++) 
            {
                if(MAIN_FOLDERS.Split('\n')[i] == fldr) 
                {
                    folder = i;
                    
                }
            }
            
        }
        public static string GetMainFolders() 
        {
            return MAIN_FOLDERS;
        }
        public static string GetCurrentFolder() 
        {
            return (MAIN_FOLDERS.Split('\n')[folder] + @"\"+ curLoc);
        }
        public static Dictionary<string, object> GetAllKeysFromCurLoc()
        {
            var valuesByName = new Dictionary<string, object>();
            RegistryKey rootkey = null;
            if (folder == 0) rootkey = Registry.ClassesRoot.OpenSubKey(curLoc);
            if (folder == 1) rootkey = Registry.CurrentUser.OpenSubKey(curLoc);
            if (folder == 2) rootkey = Registry.LocalMachine.OpenSubKey(curLoc);
            if (folder == 3) rootkey = Registry.Users.OpenSubKey(curLoc);
            if (folder == 4) rootkey = Registry.CurrentConfig.OpenSubKey(curLoc);
            if (rootkey != null)
            {
                string[] valuesName = rootkey.GetValueNames();
                foreach (string currSubKey in valuesName)
                {
                    object value = rootkey.GetValue(currSubKey);
                    valuesByName.Add(currSubKey, value);
                }
            }
            else valuesByName = null;
            
            return valuesByName;
        }
        
        public static string GetSubFolder()
        {
            string[] str;
            RegistryKey rootkey = null;
            if (folder == 0) rootkey = Registry.ClassesRoot.OpenSubKey(curLoc);
            if (folder == 1) rootkey = Registry.CurrentUser.OpenSubKey(curLoc);
            if (folder == 2) rootkey = Registry.LocalMachine.OpenSubKey(curLoc);
            if (folder == 3) rootkey = Registry.Users.OpenSubKey(curLoc);
            if (folder == 4) rootkey = Registry.CurrentConfig.OpenSubKey(curLoc);
            if(rootkey == null) 
            {
                return "Error";
            }
            str = rootkey.GetSubKeyNames();
            StringBuilder stb = new StringBuilder();
            foreach (string s in str)
            {
                stb.Append(s + "\n");
            }
            return stb.ToString();
        }
        public static void GetIntoFolder(string folder) 
        {
            curLoc += folder + @"\";
        }
        public static string GetBackOneFolder() 
        {
            StringBuilder stb = new StringBuilder();
            string[] folders = curLoc.Split('\\');
            try
            {
                for (int i = 0; i < folders.Length - 2; i++)
                    stb.Append(folders[i] + @"\");
                curLoc = stb.ToString();
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
            return curLoc;
        }
        public static bool CreateKey(string name, object value) 
        {
            bool ret = false;
            RegistryKey rootkey = null;
            
            try 
            {
                if (folder == 0) rootkey = Registry.ClassesRoot;
                if (folder == 1) rootkey = Registry.CurrentUser;
                if (folder == 2) rootkey = Registry.LocalMachine;
                if (folder == 3) rootkey = Registry.Users;
                if (folder == 4) rootkey = Registry.CurrentConfig;
                if (rootkey == null) return ret;
                rootkey = rootkey.CreateSubKey(curLoc);
                rootkey.SetValue(name, value);
                ret = true;
            }
            catch (Exception err)
            {
                ret = false;
                Console.WriteLine(err);
            }
            return ret;
        }
        
    }
}
