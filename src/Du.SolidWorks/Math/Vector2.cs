using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Math
{
    public class Vector2
    {
        public double X { get; set; }

        public double Y { get; set; }

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

        public bool IsFinite
        {
            get { double f = X + Y; return double.IsNaN(f) == false && double.IsInfinity(f) == false; }
        }

        public static Vector2 Zero { get { return new Vector2(0, 0); } }

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

        public double DistanceSquared(Vector2 v2)
        {
            double dx = v2.X - X, dy = v2.Y - Y;
            return dx * dx + dy * dy;
        }
        public double Distance(Vector2 v2)
        {
            double dx = v2.X - X, dy = v2.Y - Y;
            return System.Math.Sqrt(dx * dx + dy * dy);
        }

        public void Set(Vector2 o)
        {
            X = o.X; Y = o.Y;
        }
        public void Set(double fX, double fY)
        {
            X= fX; Y = fY;
        }
        public void Add(Vector2 o)
        {
           X+= o.X; Y += o.Y;
        }
        public void Subtract(Vector2 o)
        {
            X -= o.X; Y -= o.Y;
        }
    }
}
