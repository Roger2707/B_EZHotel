namespace EZHotel.Helpers
{
    public static class CF
    {
        public static int GetInt(object val)
        {
            if (val is int) return (int)val;
            else
            {
                int d;
                Int32.TryParse(val + "", out d);
                return d;
            }
        }

        public static double GetDouble(object val)
        {
            if (val is double) return (double)val;
            else
            {
                double d;
                double.TryParse(val + "", out d);
                return d;
            }
        }

        public static long GetLong(object val)
        {
            if (val is long) return (long)val;
            else
            {
                long d;
                long.TryParse(val + "", out d);
                return d;
            }
        }

        public static decimal GetDecimal(object val)
        {
            if (val is decimal) return (decimal)val;
            else
            {
                decimal d;
                decimal.TryParse(val + "", out d);
                return d;
            }
        }
    }
}
