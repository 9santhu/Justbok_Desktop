using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JustbokApplication.Helpers
{
    public class IOHelper
    {
        public static void createDirectory(string directoryPath, string directoryName)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    if (!Directory.Exists(directoryPath + directoryName))
                    {
                        Directory.CreateDirectory(directoryPath + directoryName);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
        }

        public static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
