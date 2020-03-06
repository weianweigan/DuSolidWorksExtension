using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Du.XUnitSolidWorks
{
    public interface IXUnitSolidWorks
    {
        SldWorks SwApp { get;  }

        MathUtility swMath { get;  }
        SLDWorksDisposeOption SLDWorksDisposeOption { get; set; }
        Action<SldWorks> CloseAction { get; set; }
    }
}
