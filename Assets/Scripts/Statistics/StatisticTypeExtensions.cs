using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


internal static class StatisticTypeExtensions
{
    public static string GetName(this StatisticType type) => Utils.EnumStringToReadable(type.ToString());
    
}
