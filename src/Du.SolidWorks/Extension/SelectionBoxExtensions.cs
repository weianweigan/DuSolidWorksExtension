using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class SelectionBoxExtensions
    {
        public static void SetSelectionColor(this IPropertyManagerPageSelectionbox box, swUserPreferenceIntegerValue_e color)
        {
            box.SetSelectionColor(true, (int)color);
        }
    }
}
