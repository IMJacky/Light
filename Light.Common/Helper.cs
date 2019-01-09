using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Light.Common
{
    public static class Helper
    {
        public static void WriteMessage(string message)
        {
            File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "test.txt"), message);
        }
    }
}
