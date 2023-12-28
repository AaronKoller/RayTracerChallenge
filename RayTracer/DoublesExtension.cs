using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    using System;
    public static class DoubleExtensions
    {

        public static bool EqualsD(this double a, double b)
        {
            if ((Math.Abs(a - b)) <= Constants.EPSILON)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
