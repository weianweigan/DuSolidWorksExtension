using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Interface
{
    public interface IFlyoutCommand
    {
        int FlyoutCMDId { get; set; }

        IFlyoutGroup  FlyoutGroup { get; set; }
        int UserId { get; set; }
        string[] MainIconList { get; }
        string[] IconList { get; }

        void Execute(object parameter);
    }
}
