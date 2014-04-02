using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AESHiringManagement.Models
{
    public sealed class Dashboard
    {
        private static volatile Dashboard instance;
        private static object syncRoot = new Object();
        private static int number = 0;
        private static List<string> names = new List<string>();
        private static List<int> ids = new List<int>();
        private static List<int> status = new List<int>();
        
        private Dashboard() { }
        

        public static Dashboard Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Dashboard();
                    }
                }

                return instance;
            }
        }

        public static int getNumber() 
        {
            return number;
        }

        public static void increment()
        {
            number++;
        }

        public static void addApplicant(string name, int id)
        {
            lock (syncRoot)
            {
                if (!ids.Contains(id))
                {
                    var sb = new StringBuilder();
                    sb.Append("[" + DateTime.Now.ToString("t") + "] " + name);
                    names.Add(sb.ToString());
                    ids.Add(id);
                    status.Add(0);  //0 = new, 1 = checked out, 2 = waiting final approval
                    increment();
                }
            }
        }

        public static string getName(int id)
        {
            return names[ids.IndexOf(id)];
        }

        public static int getId(string name)
        {
            if (names.Contains(name))
                return ids[names.IndexOf(name)];
            else
                return -1;
        }

        public static List<int> getIds()
        {
            return ids;
        }

        public static int getStatus(int id) 
        {
            return status[ids.IndexOf(id)];
        }

        public static void setStatus(int id, int stat)
        {
            lock (syncRoot)
            {
                if (ids.Contains(id))
                {
                    status[ids.IndexOf(id)] = stat;
                }
            }
        }
    }
}