using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashcode2018
{
    public class DataAccess
    {
        public DataAccess()
        {

        }

        public List<string> Read(string textFilePath)
        {
            List<string> temp = new List<string>();
            if (File.Exists(textFilePath))
            {
                StreamReader reader = new StreamReader(new FileStream(textFilePath, FileMode.Open, FileAccess.Read));
                while (!reader.EndOfStream)
                {
                    temp.Add(reader.ReadLine());
                }

                reader.Close();
                return temp;
            }
            return new List<string>();
        }

        public bool Write(List<string> dataToWrite, string textFilePath)
        {
            try
            {
                StreamWriter writer = new StreamWriter(new FileStream(textFilePath, FileMode.Create, FileAccess.Write));
                foreach (string line in dataToWrite)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
