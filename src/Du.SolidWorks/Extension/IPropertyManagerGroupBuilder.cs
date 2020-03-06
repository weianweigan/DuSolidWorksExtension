using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public class IPropertyManagerGroupBuilder
    {
        public IPropertyManagerPageGroup Group { get; }

        public Dictionary<int, dynamic> controls { get; }

        public IPropertyManagerGroupBuilder(IPropertyManagerPageGroup page)
        {
            Group = page;
            controls = new Dictionary<int, dynamic>();
        }


        public IPropertyManagerGroupBuilder AddControlToBuilder<T>(int id,
            string caption,
            string tip,
            swPropertyManagerPageControlLeftAlign_e leftAlign,
            IEnumerable<swAddControlOptions_e> options,
            Action<T> pmpageContrlConfig)
        {
            var control = Group.AddControl<T>(id, caption, tip, leftAlign, options);

            if (control != null)
            {
                if (!controls.ContainsKey(id))
                {
                    controls.Add(id, control);
                }
                pmpageContrlConfig?.Invoke(control);
            }
            return this;
        }
    }
}
