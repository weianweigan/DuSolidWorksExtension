using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using SolidWorks.Interop.swconst;

namespace Du.SolidWorks.Extension
{
    public class SketchRelationBuilder
    {
        #region field

        private readonly Sketch _activeSketch;
        private readonly List<KeyValuePair<swSketchEntityType_e, SketchSegment>> _sketchSegments;

        #endregion

        #region .ctor

        public SketchRelationBuilder(Sketch activeSketch)
        {
            _activeSketch = activeSketch;
        }


        public SketchRelationBuilder(Sketch activeSketch, List<KeyValuePair<swSketchEntityType_e, SketchSegment>> sketchSegments)
        {
            _activeSketch = activeSketch;
            _sketchSegments = sketchSegments;
        }

        #endregion

        #region Public Methods

        public int ConstrainAll()
        {
            return _activeSketch.ConstrainAll();
        }

        public ISketchRelationManager GetRelationManager()
        {
            return _activeSketch.RelationManager;
        }

        #endregion


    }
}
