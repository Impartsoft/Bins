using System;
using System.Collections.Generic;
using System.Text;

namespace EnumDemo
{
   public enum ExceptionCode
    {
        [BaseAttributes(DescribeName ="OK")]
        OK=200,
    }
}
