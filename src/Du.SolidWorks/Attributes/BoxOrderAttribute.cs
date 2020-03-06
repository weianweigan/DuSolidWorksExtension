using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Attributes
{
    /// <summary>
    /// Box序列
    /// </summary>
    public class BoxOrderAttribute : Attribute
    {
        public int Order { get; set; }
    }
}
