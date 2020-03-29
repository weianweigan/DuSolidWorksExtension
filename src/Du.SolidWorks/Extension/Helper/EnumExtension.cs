using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{

    public static class EnumObjExtension
    {
        /// <summary>
        /// 将SolidWorks枚举值转换为Int值,只能用于枚举值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SWToInt(this object obj)
        {
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 将SolidWorks枚举值转换为Short值,只能用于枚举值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static short SWToShort(this object obj)
        {
            return Convert.ToInt16(obj);
        }

        /// <summary>
        /// 强制转换类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CastObj<T>(this object obj)
        {
            return (T)obj;
        }
    }
}
