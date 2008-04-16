using System;
using System.Collections.Generic;
using System.Text;

namespace CppKnacker
{
    class KException : System.Exception
    {
        public enum ExceptionType { UnknownFileType }
        ExceptionType m_ExType;
        public KException( ExceptionType extype )
        {
            m_ExType = extype;
        }
    }
}
