using System.Text;

namespace Common
{
    internal class StringHelper
    {
        public static string CutString(string inputString, int len, bool ifAddDot = false)
        {
            if (inputString == null)
                return null;
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (ifAddDot && (mybyte.Length > len))
                tempString += "..";

            return tempString;
        }
    }
}
