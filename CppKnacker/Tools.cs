using System;
using System.Collections.Generic;
using System.Text;

namespace CppKnacker
{
    class Tools
    {
        // file t�pus�nak eld�nt�se
        public static bool IsSourceFile(string FileName) { return FileName.EndsWith(".cpp"); }
        public static bool IsHeaderFile(string FileName) { return FileName.EndsWith(".h"); }
    }
}
