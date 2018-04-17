using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashcode2018
{
    public class Verification
    {
        public Verification()
        {

        }

        public bool isValidFormat(List<string> result)
        {
            try
            {
                foreach (string item in result)
                {
                    string[] arrSplitter = item.Split(' ');
                    for (int i = 0; i < arrSplitter.Length; i++)
                    {
                        int.Parse(arrSplitter[i]);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
