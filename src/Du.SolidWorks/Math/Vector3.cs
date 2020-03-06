using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Math
{
    using Math = System.Math;
    public class Vector3
    {

        private double x;
        private double y;
        private double z;

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }
        public static Vector3 Zero { get {
                return new Vector3(0, 0, 0);
            } }
        public static Vector3 UnitZ { get {
                return new Vector3(0, 0, 1); }
        }

        public static Vector3 UnitY { get {
                return new Vector3(0, 1, 0);
            } }

        public static Vector3 UnitX
        {
            get
            {
                return new Vector3(1, 0, 0);
            }
        }
        public Vector3(double[] arrayData)
        {
            if (arrayData.Length >= 3)
            {
                this.X = arrayData[0];
                this.Y = arrayData[1];
                this.Z = arrayData[2];
            }
        }

        public Vector3(double v1, double v2, double v3)
        {
            this.X = v1;
            this.Y = v2;
            this.Z = v3;
        }

        public Vector2 xy
        {
            get { return new Vector2(x, y); }
            set { x = value.X; y = value.Y; }
        }
        public Vector2 xz
        {
            get { return new Vector2(x, z); }
            set { x = value.X; z = value.Y; }
        }
        public Vector2 yz
        {
            get { return new Vector2(y, z); }
            set { y = value.X; z = value.Y; }
        }

        public double LengthSquared
        {
            get { return x * x + y * y + z * z; }
        }
        public double Length
        {
            get { return System.Math.Sqrt(LengthSquared); }
        }


        public double[] ToDoubles()
        {
            return new double[] { X, Y, Z };
        }

        public MathPoint ToSwMathPoint(MathUtility math = null)
        {
            if (math == null)
            {
                math = MathUtil.swMathUtility;
            }
            if (math == null)
            {
                throw new NullReferenceException("MathUtility未将对象引用到对象的实例");
            }
            return math.CreatePoint(ToDoubles()) as MathPoint;
        }

        public MathVector ToSwMathVector(MathUtility math = null)
        {
            if (math == null)
            {
                math = MathUtil.swMathUtility;
            }
            if (math == null)
            {
                throw new NullReferenceException("MathUtility未将对象引用到对象的实例");
            }
            return math.CreateVector(ToDoubles()) as MathVector;
        }

        /// <summary>
        /// 求三个坐标的绝对值长度和
        /// </summary>
        public double LengthL1
        {
            get { return System.Math.Abs(x) + System.Math.Abs(y) + System.Math.Abs(z); }
        }

        public double Max
        {
            get { return System.Math.Max(x, Math.Max(y, z)); }
        }
        public double Min
        {
            get { return Math.Min(x, Math.Min(y, z)); }
        }

        public double MaxAbs
        {
            get { return Math.Max(Math.Abs(x), Math.Max(Math.Abs(y), Math.Abs(z))); }
        }
        public double MinAbs
        {
            get { return Math.Min(Math.Abs(x), Math.Min(Math.Abs(y), Math.Abs(z))); }
        }

        public Vector3 Abs
        {
            get { return new Vector3(Math.Abs(x), Math.Abs(y), Math.Abs(z)); }
        }

        public double Normalize(double epsilon = MathUtil.Epsilon)
        {
            double length = Length;
            if (length > epsilon)
            {
                double invLength = 1.0 / length;
                x *= invLength;
                y *= invLength;
                z *= invLength;
            }
            else
            {
                length = 0;
                x = y = z = 0;
            }
            return length;
        }

        public Vector3 Unit(double epsilon = MathUtil.Epsilon)
        {
           
                double length = Length;
                if (length > MathUtil.Epsilon)
                {
                    double invLength = 1.0 / length;
                    return new Vector3(x * invLength, y * invLength, z * invLength);
                }
                else
                    return Vector3.Zero;
            
        }

        public bool IsNormalized
        {
            get { return Math.Abs((x * x + y * y + z * z) - 1) < MathUtil.ZeroTolerance; }
        }

        public bool IsFinite
        {
            get { double f = x + y + z; return double.IsNaN(f) == false && double.IsInfinity(f) == false; }
        }

        public void Round(int nDecimals)
        {
            x = Math.Round(x, nDecimals);
            y = Math.Round(y, nDecimals);
            z = Math.Round(z, nDecimals);
        }

        public double Dot(Vector3 v2)
        {
            return x * v2.x + y * v2.y + z * v2.z;
        }
        public double Dot(ref Vector3 v2)
        {
            return x * v2.x + y * v2.y + z * v2.z;
        }
        public static double Dot(Vector3 v1, Vector3 v2)
        {
            return v1.Dot(ref v2);
        }

        public Vector3 Cross(Vector3 v2)
        {
            return new Vector3(
                y * v2.z - z * v2.y,
                z * v2.x - x * v2.z,
                x * v2.y - y * v2.x);
        }
        public Vector3 Cross(ref Vector3 v2)
        {
            return new Vector3(
                y * v2.z - z * v2.y,
                z * v2.x - x * v2.z,
                x * v2.y - y * v2.x);
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return v1.Cross(ref v2);
        }

        /// <summary>
        /// 叉乘后单位化
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public Vector3 UnitCross(ref Vector3 v2)
        {
            Vector3 n = new Vector3(
                y * v2.z - z * v2.y,
                z * v2.x - x * v2.z,
                x * v2.y - y * v2.x);
            n.Normalize();
            return n;
        }

        /// <summary>
        /// 叉乘后单位化
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public Vector3 UnitCross(Vector3 v2)
        {
            return UnitCross(ref v2);
        }

        /// <summary>
        /// 角度
        /// </summary>
        /// <param name="v2">单位向量</param>
        /// <returns></returns>
        public double AngleD(Vector3 v2)
        {
            double fDot = MathUtil.Clamp(Dot(v2), -1, 1);
            return Math.Acos(fDot) * MathUtil.Rad2Deg;
        }

        /// <summary>
        /// 判断两个向量是否平行
        /// 角度为0或者180
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool ParaWithVec(Vector3 v2)
        {
            double angleD = Unit().AngleD(v2.Unit());
            if (Math.Abs(angleD) < 0.00001 || Math.Abs((Math.Abs(angleD) - 180)) < 0.00001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断两个向量是否相同方向
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool IsSameDirection(Vector3 v2)
        {
            double angleD = Unit().AngleD(v2.Unit());
            if (Math.Abs(angleD) < 0.00001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 角度--单位向量
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double AngleD(Vector3 v1, Vector3 v2)
        {
            return v1.AngleD(v2);
        }

        /// <summary>
        /// 弧度--单位向量
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double AngleR(Vector3 v2)
        {
            double fDot = MathUtil.Clamp(Dot(v2), -1, 1);
            return Math.Acos(fDot);
        }

        /// <summary>
        /// 弧度--单位向量
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double AngleR(Vector3 v1, Vector3 v2)
        {
            return v1.AngleR(v2);
        }

        public double DistanceSquared(Vector3 v2)
        {
            double dx = v2.x - x, dy = v2.y - y, dz = v2.z - z;
            return dx * dx + dy * dy + dz * dz;
        }
        public double DistanceSquared(ref Vector3 v2)
        {
            double dx = v2.x - x, dy = v2.y - y, dz = v2.z - z;
            return dx * dx + dy * dy + dz * dz;
        }

        public double Distance(Vector3 v2)
        {
            double dx = v2.x - x, dy = v2.y - y, dz = v2.z - z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
        public double Distance(ref Vector3 v2)
        {
            double dx = v2.x - x, dy = v2.y - y, dz = v2.z - z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public void Set(Vector3 o)
        {
            x = o.x; y = o.y; z = o.z;
        }
        public void Set(double fX, double fY, double fZ)
        {
            x = fX; y = fY; z = fZ;
        }

        public Vector3 Add(Vector3 vector3)
        {
            return new Vector3(x + vector3.x, y + vector3.y, z + vector3.z);
        }

        public Vector3 Scale(double length)
        {
            return new Vector3(x * length, y * length, z * length);
        }

        [Obsolete]
        public Vector3 Orthogonal()
        {
            throw new NotImplementedException();
        }
    }

    public static class doubleExtesnion
    {
        public static Vector3 ToVector3(this double[] array)
        {
            if (array.Length >= 3)
            {
                return new Vector3(array[0], array[1], array[2]);
            }
            else
            {
                return null;
            }
        }
    }
}
