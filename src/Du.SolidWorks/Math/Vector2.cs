using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Math
{

    /// <summary>
    /// 代表平面向量或者点
    /// </summary>
    public class Vector2
    {
        /// <summary>
        /// X
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(double x,double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 角度转换为弧度
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 FromAngleRad(double angle)
        {
            return new Vector2(System.Math.Cos(angle), System.Math.Sin(angle));
        }

        /// <summary>
        /// 长度平方值
        /// </summary>
        public double LengthSquared
        {
            get { return X * X + Y * Y; }
        }

        /// <summary>
        /// 长度
        /// </summary>
        public double Length
        {
            get { return (double)System.Math.Sqrt(LengthSquared); }
        }

        /// <summary>
        /// 单位化
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public double Normalize(double epsilon = MathUtil.Epsilon)
        {
            double length = Length;
            if (length > epsilon) {
                double invLength = 1.0 / length;
                X *= invLength;
                Y *= invLength;
            } else {
                length = 0;
                X = Y = 0;
            }
            return length;
        }

        /// <summary>
        /// 是否是无穷值
        /// </summary>
        public bool IsFinite
        {
            get { double f = X + Y; return double.IsNaN(f) == false && double.IsInfinity(f) == false; }
        }

        /// <summary>
        /// 原点
        /// </summary>
        public static Vector2 Zero { get { return new Vector2(0, 0); } }

        /// <summary>
        /// (1,0)
        /// </summary>
        public static Vector2 XPlusVector { get { return new Vector2(1, 0); } }

        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double Dot(Vector2 v2)
        {
            return X * v2.X + Y * v2.Y;
        }

        /// <summary>
        /// 叉乘
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double Cross(Vector2 v2)
        {
            return X * v2.Y - Y * v2.X;
        }
        /// <summary>
        /// 求与另外一个向量角度--单位度deg
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double AngleD(Vector2 v2)
        {
            double fDot = MathUtil.Clamp(Dot(v2), -1, 1);
            return System.Math.Acos(fDot) * MathUtil.Rad2Deg;
        }
        /// <summary>
        /// 两个向量角度--单位度deg
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double AngleD(Vector2 v1, Vector2 v2)
        {
            return v1.AngleD(v2);
        }
        /// <summary>
        /// 求与另外一个向量角度--单位弧度Rad
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double AngleR(Vector2 v2)
        {
            double fDot = MathUtil.Clamp(Dot(v2), -1, 1);
            return System.Math.Acos(fDot);
        }
        /// <summary>
        /// 两个向量角度--单位弧度Rad
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double AngleR(Vector2 v1, Vector2 v2)
        {
            return v1.AngleR(v2);
        }

        /// <summary>
        /// 圆整
        /// </summary>
        /// <param name="nDecimals"></param>
        public void Round(int nDecimals)
        {
            X = System.Math.Round(X, nDecimals);
            Y = System.Math.Round(Y, nDecimals);
        }

        /// <summary>
        /// 距离的平方值
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double DistanceSquared(Vector2 v2)
        {
            double dx = v2.X - X, dy = v2.Y - Y;
            return dx * dx + dy * dy;
        }

        /// <summary>
        /// 距离
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double Distance(Vector2 v2)
        {
            double dx = v2.X - X, dy = v2.Y - Y;
            return System.Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 设置当前实例的值
        /// </summary>
        /// <param name="o"></param>
        public void Set(Vector2 o)
        {
            X = o.X; Y = o.Y;
        }

        /// <summary>
        /// 设置当前实例的值
        /// </summary>
        /// <param name="fX"></param>
        /// <param name="fY"></param>
        public void Set(double fX, double fY)
        {
            X= fX; Y = fY;
        }

        /// <summary>
        /// 向量相加
        /// </summary>
        /// <param name="o"></param>
        public void Add(Vector2 o)
        {
           X+= o.X; Y += o.Y;
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="o"></param>
        public void Subtract(Vector2 o)
        {
            X -= o.X; Y -= o.Y;
        }
    }
}
