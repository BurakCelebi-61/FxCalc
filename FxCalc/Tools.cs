using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCalc
{
    public static class Tools
    {
        public static decimal ToDecimal(this object value)
        {
            if (value !=null)
            {
                try
                {
                    return Convert.ToDecimal(value);
                }
                catch (Exception)
                {

                    return 0;
                }
            }
            return 0;
        }
        public static int ToInt(this object value)
        {
            if (value != null)
            {
                try
                {
                    return Convert.ToInt32(value);
                }
                catch (Exception)
                {

                    return 0;
                }
            }
            return 0;
        }
        public static double ToDouble(this object value)
        {
            if (value != null)
            {
                try
                {
                    return Convert.ToDouble(value);
                }
                catch (Exception)
                {

                    return 0;
                }
            }
            return 0;
        }

        
    }
}
