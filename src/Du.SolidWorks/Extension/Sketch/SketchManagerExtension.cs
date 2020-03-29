using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// 扩展 ISketchManager 
    /// </summary>
    /// <remarks>
    /// ISketchManager => Provides access to sketch-creation routines. 
    /// </remarks>
    public static class SketchManagerExtension
    {
        /// <summary>
        /// 绘制圆
        /// </summary>
        /// <param name="ske"><see cref="ISketchManager"/> Interface</param>
        /// <param name="centerPoint"><see cref="Vector3"/></param>
        /// <param name="pointOnCircle"><see cref="Vector3"/></param>
        /// <returns><see cref="SketchSegment"/> Interface</returns>
        public static SketchSegment CreateCircle(this ISketchManager ske,Vector3 centerPoint,Vector3 pointOnCircle)
        {
            return ske.CreateCircle(centerPoint.X, centerPoint.Y, centerPoint.Z,
                pointOnCircle.X, pointOnCircle.Y, pointOnCircle.Z);
        }

        public static ISketchPoint CreatePoint(this ISketchManager ske,Vector3 point)
        {
            return ske.CreatePoint(point.X, point.Y, point.Z);
        }

        /// <summary>
        /// 创建直线
        /// </summary>
        /// <param name="ske"><see cref="ISketchManager"/> Interface</param>
        /// <param name="centerPoint"><see cref="Vector3"/></param>
        /// <param name="pointOnCircle"><see cref="Vector3"/></param>
        /// <returns></returns>
        public static SketchSegment CreateLine(this ISketchManager ske, Vector3 centerPoint, Vector3 pointOnCircle)
        {
            return ske.CreateLine(centerPoint.X, centerPoint.Y, centerPoint.Z,
                pointOnCircle.X, pointOnCircle.Y, pointOnCircle.Z);
        }

        /// <summary>
        /// 以不自动约束模式绘制草图 InferenceMode = false
        /// </summary>
        /// <param name="skeMgr"><see cref="ISketchManager"/> Interface</param>
        /// <param name="sketchAction">Action for Drawing SkechSegment</param>
        public static void SketchWithNoReference(this ISketchManager skeMgr, Action<ISketchManager> sketchAction,ISldWorks app)
        {
            app.WithToggleState(swUserPreferenceToggle_e.swSketchInference, false, () =>
              {
                  sketchAction.Invoke(skeMgr);
              }
            );
        }

        /// <summary>
        /// 获取建造对象
        /// </summary>
        /// <param name="sketchManager"><see cref="ISketchManager"/> Interface</param>
        /// <returns> 使用 <see cref="SketchSeBuilder"/> 来绘制草图</returns>
        public static SketchSeBuilder GetBuilder(this ISketchManager sketchManager)
        {
            return new SketchSeBuilder(sketchManager);
        }

        /// <summary>
        /// 退出草图编辑状态
        /// </summary>
        /// <param name="skeMgr"><see cref="ISketchManager"/> Interface</param>
        /// <param name="UpdateEditRebuild"></param>
        /// <returns><see cref="ISketch"/></returns>
        /// <remarks>
        /// 如果SolidWorks不在编辑状态,将会抛出 <see cref="InvalidOperationException"/> 异常
        /// </remarks>
        public static ISketch ExitSketch(this ISketchManager skeMgr,bool UpdateEditRebuild = false)
        {
            var sketch = skeMgr.ActiveSketch;
            if (sketch == null)
            {
                throw new InvalidOperationException($"There is not a active skecth,so you cannot exit sketch");
            }

            skeMgr.InsertSketch(UpdateEditRebuild);

            return sketch;
        }

    }
}
