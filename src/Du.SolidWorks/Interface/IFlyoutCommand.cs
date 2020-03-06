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

        void Execute(object parameter);
    }
}
