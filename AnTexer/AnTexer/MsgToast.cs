using System;
using System.Collections.Generic;
using System.Text;

namespace AnTexer
{
    public interface MsgToast
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
