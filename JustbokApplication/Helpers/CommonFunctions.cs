
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CommonFunctions
{
    public static void LogError(Exception ex, ErrorLog.LogSeverity severity)
    {
        try
        {
            ErrorLog.ErrorLogger.GetInstance().Log(ex, severity);
        }
        catch { }
    }
}

