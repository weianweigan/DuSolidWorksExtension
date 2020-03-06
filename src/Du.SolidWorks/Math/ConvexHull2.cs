using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Math
{

    /// <summary>
    /// 暂时不能使用
    /// </summary>
    [Obsolete]
    public class ConvexHull2
    {
        private Stack<Vector2> stack = new Stack<Vector2>();

        public List<Vector2> mVectors { get; set; }

        public int MinYVectorIndex { get; private set; } = 0;

        public double mE { get; set; }
        public ConvexHull2(List<Vector2> vectors,double e = MathUtil.Epsilon)
        {
            mVectors = vectors;

            mE = e;
        }

        public List<Vector2> GetConvexHull()
        {
            if (mVectors == null || mVectors.Count < 3)
            {
                throw new Exception("点集为空或者点集不足3");
            }

            stack.Clear();

            double minY = mVectors.FirstOrDefault().Y;

            //找打Y值最小的点
            for (int i = 0; i < mVectors.Count; i++)
            {
                if (mVectors[i].Y < minY)
                {
                    minY = mVectors[i].Y;
                    MinYVectorIndex = i;
                }
            }

            //压入栈
            stack.Push(mVectors[MinYVectorIndex]);

            //根据角度排序
            Dictionary<int, double> IndexWithAngle = new Dictionary<int, double>();

            for (int i = 0; i < mVectors.Count; i++)
            {
                if (i == MinYVectorIndex)
                {
                    break;
                }
                //计算角度
                Vector2 vector2 = new Vector2(mVectors[i].X - mVectors[MinYVectorIndex].X,
                                              mVectors[i].Y - mVectors[MinYVectorIndex].Y);
                double angle = vector2.AngleD(Vector2.XPlusVector);
                IndexWithAngle.Add(i, angle);
            }

            return null;
        }
    }
}
