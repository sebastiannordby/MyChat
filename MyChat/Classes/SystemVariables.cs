using System.Text;
using System.Collections.Generic;

namespace MyChat.Classes
{
    public class SystemVariables
    {
        public static string BuildNumber { get; set; }
        public static string RestAPI { get; set; }
        public static List<string> AllowedPaths { get; set; }
        public static string TokenIssuer { get; set; }
        public static string Audience { get; set; }
        public static string APIKey { get; set; }

        public static byte[] SecretKey = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
    }
}
