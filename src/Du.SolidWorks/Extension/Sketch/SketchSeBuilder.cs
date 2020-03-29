using Du.SolidWorks.Math;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using SolidWorks.Interop.swconst;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// 草图建造者
    /// </summary>
    public class SketchSeBuilder:IDisposable
    {
        public ISketchManager skeMgr { get; private set; }

        private Queue<Action<ISketchManager>> builderActions = new Queue<Action<ISketchManager>>();

        private List<KeyValuePair<swSketchEntityType_e, SketchSegment>> SketchSegments;

        private ISketchManager GetSketchManager()
        {
            return skeMgr;
        }

        public SketchSeBuilder(ISketchManager sketchManager)
        {
            skeMgr = sketchManager;
        }

        /// <summary>
        /// 获取 <see cref="ISketch"/> 接口
        /// </summary>
        /// <returns></returns>
        public ISketch GetISketch()
        {
            return skeMgr.ActiveSketch;
        }

        /// <summary>
        /// 绘制所有草图
        /// </summary>
        /// <param name="autoReference">是否自动添加参考</param>
        /// <param name="addToDB">是否添加到Db中</param>
        /// <param name="DisplayWhenAdded">添加完是否显示</param>
        /// <returns></returns>
        public SketchRelationBuilder Build(bool autoReference = false, bool addToDB = true, bool DisplayWhenAdded = false)
        {
            if (skeMgr.ActiveSketch == null)
            {
                throw new NullReferenceException($"No active sketch,please edit or new sketch first");
            }
            SketchSegments = new List<KeyValuePair<swSketchEntityType_e, SketchSegment>>();

            bool inferenceState = skeMgr.AutoInference;
            bool addToDBState = skeMgr.AddToDB;
            bool displayWhenAddedState = skeMgr.DisplayWhenAdded;

            skeMgr.AutoInference = autoReference;
            skeMgr.AddToDB = addToDB;
            if (addToDB)
            {
                skeMgr.DisplayWhenAdded = DisplayWhenAdded;
            }
            else
            {
                throw new InvalidOperationException($"Before you use DisplayWhenAdded property, you need to set AddToDB = true");
            }

            try
            {
                foreach (var item in builderActions)
                {
                    item.Invoke(skeMgr);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //回复状态
                skeMgr.AutoInference = inferenceState;
                if (addToDB)
                {
                    skeMgr.DisplayWhenAdded = displayWhenAddedState;
                }
                skeMgr.AddToDB = addToDBState;
            }

            return new SketchRelationBuilder(skeMgr.ActiveSketch, SketchSegments);
        }

        /// <summary>
        /// 添加直线
        /// </summary>
        /// <param name="startPoint">起点</param>
        /// <param name="endPoint">终点</param>
        /// <returns></returns>
        public SketchSeBuilder AddLine(Vector3 startPoint,Vector3 endPoint)
        {
            builderActions.Enqueue(ske =>
            {
               var se = ske.CreateLine(startPoint, endPoint);
               if (se == null)
               {
                   throw new AddSketchSegmentException($"{nameof(AddLine) } error , no line create");
               }
                else
                {
                    SketchSegments.Add  (new KeyValuePair<swSketchEntityType_e, SketchSegment>( swSketchEntityType_e.swSketchEntityLine, se));
                }
            }
            );
            return this;
        }

        /// <summary>
        /// 添加圆
        /// </summary>
        /// <param name="centerPoint">圆心</param>
        /// <param name="pointOnCircle">圆上一点</param>
        /// <returns></returns>
        public SketchSeBuilder AddCircle(Vector3 centerPoint,Vector3 pointOnCircle)
        {
            builderActions.Enqueue(ske =>
            {
                var se = ske.CreateCircle(centerPoint, pointOnCircle);
                if (se == null)
                {
                    throw new AddSketchSegmentException($"{nameof(AddCircle) } error , no circle create");
                }
                else
                {
                    SketchSegments.Add(new KeyValuePair<swSketchEntityType_e, SketchSegment>(swSketchEntityType_e.swSketchEntityArc, se));
                }
            }
            );
            return this;
        }

        /// <summary>
        /// 通过圆心和半径创建圆
        /// </summary>
        /// <param name="centerPoint"></param>
        /// <param name="Radius"></param>
        /// <returns></returns>
        public SketchSeBuilder AddCircleByRadius(Vector3 centerPoint,double Radius)
        {
            builderActions.Enqueue(ske =>
            {
                var se = ske.CreateCircleByRadius(centerPoint.X, centerPoint.Y, centerPoint.Z, Radius);
                if (se == null)
                {
                    throw new AddSketchSegmentException($"{nameof(AddCircleByRadius) } error , no circle create");
                }
                else
                {
                    SketchSegments.Add(new KeyValuePair<swSketchEntityType_e, SketchSegment>(swSketchEntityType_e.swSketchEntityArc, se));
                }
            });
            return this;
        }

        /// <summary>
        /// 通过三点创建圆弧
        /// </summary>
        /// <param name="pointOne">点1</param>
        /// <param name="pointTwo">点2</param>
        /// <param name="pointThree">点3</param>
        /// <returns></returns>
        public SketchSeBuilder AddArcByThreePoint(Vector3 pointOne, Vector3 pointTwo, Vector3 pointThree, Action<SketchSegment> action = null)
        {
            builderActions.Enqueue(ske =>
            {
                var se = ske.Create3PointArc(pointOne.X, pointOne.Y, pointOne.Z,
                                     pointTwo.X, pointTwo.Y, pointTwo.Z,
                                     pointThree.X, pointThree.Y, pointThree.Z);
                if (se == null)
                {
                    throw new AddSketchSegmentException($"{nameof(AddArcByThreePoint) } error , no circle create");
                }
                else
                {
                    action?.Invoke(se);
                    SketchSegments.Add(new KeyValuePair<swSketchEntityType_e, SketchSegment>(swSketchEntityType_e.swSketchEntityArc, se));
                }
            });
            return this;
        }

        /// <summary>
        /// 将所有点连成一个路径
        /// </summary>
        /// <param name="path">所有的点</param>
        /// <returns></returns>
        public SketchSeBuilder AddPath(Action<SketchSegment> action = null,params Vector3[] path)
        {
            builderActions.Enqueue(skeMgr =>
            {
                for (int i = 0; i < path.Length; i++)
                {
                    var se = skeMgr.CreateLine(path[i], i + 1 == path.Length+1 ? path[0] : path[i+1]);
                    if (se == null)
                    {
                        throw new AddSketchSegmentException($"{nameof(AddArcByThreePoint) } error , no circle create");
                    }
                    else
                    {
                        action?.Invoke(se);
                        SketchSegments.Add(new KeyValuePair<swSketchEntityType_e, SketchSegment>(swSketchEntityType_e.swSketchEntityLine, se));
                    }
                }
            });

            return this;
        }

        public void Dispose()
        {
            skeMgr = null;
            builderActions.Clear();
            SketchSegments.Clear();
        }
    }
}
