
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Interface
{
    public interface ISWCMDGroupService
    {
        ICommandGroup AddCommandGroup(GroupData groupData, IContainer container);
    }
}
