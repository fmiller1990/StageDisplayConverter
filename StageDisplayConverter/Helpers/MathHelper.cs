using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StageDisplayConverter.Helpers
{
   public static class MathHelper
    {

        public static double ConvertPointToCm(double pts) {
            return pts * 0.03527778;
        }

        public static double ConvertCmToPoint(double cms) {
            return cms * 28.34646;

        }
    }
}