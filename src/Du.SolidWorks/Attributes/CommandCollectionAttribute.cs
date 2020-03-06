using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Attributes
{
    /// <summary>
    /// Box中的按钮集合
    /// </summary>
    public class CommandCollectionAttribute : Attribute
    {
        public Type[] Commands { get; set; }

        public CommandCollectionAttribute(params Type[] commands)
        {
            Commands = commands;
        }
    }
}
