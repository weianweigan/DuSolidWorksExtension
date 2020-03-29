using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Math
{
    using Math = System.Math;

    /// <summary>
    /// 代表空间中的向量和点
    /// </summary>
    public class Vector3
    {

        private double x;
        private double y;
        private double z;

        /// <summary>
        /// X
        /// </summary>
        public double X { get => x; set => x = value; }
        
        /// <summary>
        /// Y
        /// </summary>
        public double Y { get => y; set => y = value; }
        
        /// <summary>
        /// Z
        /// </summary>
        public double Z { get => z; set => z = value; }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 获取 (0,0,0)
        /// </summary>
        public static Vector3 Zero { get {
                return new Vector3(0, 0, 0);
            } }

        /// <summary>
        /// (0,0,1)
        /// </summary>
        public static Vector3 UnitZ { get {
                return new Vector3(0, 0, 1); }
        }

        /// <summary>
        /// (0, 1, 0)
        /// </summary>
        public static Vector3 UnitY { get {
                return new Vector3(0, 1, 0);
            } }

        /// <summary>
        /// Vector3(1, 0, 0)
        /// </summary>
        public static Vector3 UnitX
        {
            get
            {
                return new Vector3(1, 0, 0);
            }
        }

        /// <summary>
        /// 构造函数 数组值的长度需要大于等于3
        /// </summary>
        /// <param name="arrayData"></param>
        public Vector3(double[] arrayData)
        {
            if (arrayData.Length >= 3)
            {
                this.X = arrayData[0];
                this.Y = arrayData[1];
                this.Z = arrayData[2];
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="v1">X</param>
        /// <param name="v2">Y</param>
        /// <param name="v3">Z</param>
        public Vector3(double v1, double v2, double v3)
        {
            this.X = v1;
            this.Y = v2;
            this.Z = v3;
        }

        /// <summary>
        /// 返回 X,Y坐标
        /// </summary>
        public Vector2 xy
        {
            get { return new Vector2(x, y); }
            set { x = value.X; y = value.Y; }
        }

        /// <summary>
        /// 返回 X Z
        /// </summary>
        public Vector2 xz
        {
            get { return new Vector2(x, z); }
            set { x = value.X; z = value.Y; }
        }

        /// <summary>
        /// 返回 Y Z
        /// </summary>
        public Vector2 yz
        {
            get { return new Vector2(y, z); }
            set { y = value.X; z = value.Y; }
        }

        /// <summary>
        /// 获取长度平方值
        /// </summary>
        public double LengthSquared
        {
            get { return x * x + y * y + z * z; }
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        public double Length
        {
            get { return System.Math.Sqrt(LengthSquared); }
        }

        /// <summary>
        /// 计算和另外一点的距离
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double Distance(ref Vector3 v2)
        {
            double dx = v2.x - x, dy = v2.y - y, dz = v2.z - z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <returns></returns>
        public double[] ToDoubles()
        {
            return new double[] { X, Y, Z };
        }

        /// <summary>
        /// 转换为<see cref="IMathPoint"/> 如果不传入 <see cref="IMathUtility"/>,必须设置静态变量 <see cref="MathUtil.swMathUtility"/>
        /// </summary>
        /// <param name="math"></param>
        /// <returns></returns>
        public MathPoint ToSwMathPoint(IMathUtility math = null)
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

        /// <summary>
        /// 转换为<see cref="IMathVector"/> 如果不传入 <see cref="IMathUtility"/>,必须设置静态变量 <see cref="MathUtil.swMathUtility"/>
        /// </summary>
        /// <param name="math"><see cref="IMathUtility"/></param>
        /// <returns></returns>
        public MathVector ToSwMathVector(IMathUtility math = null)
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

        /// <summary>
        /// 三个坐标值中的最大值
        /// </summary>
        public double Max
        {
            get { return System.Math.Max(x, Math.Max(y, z)); }
        }

        /// <summary>
        /// 三个坐标值中的最小值
        /// </summary>
        public double Min
        {
            get { return Math.Min(x, Math.Min(y, z)); }
        }

        /// <summary>
        /// 绝对值中的做大值
        /// </summary>
        public double MaxAbs
        {
            get { return Math.Max(Math.Abs(x), Math.Max(Math.Abs(y), Math.Abs(z))); }
        }

        /// <summary>
        /// 绝对值中的最小值
        /// </summary>
        public double MinAbs
        {
            get { return Math.Min(Math.Abs(x), Math.Min(Math.Abs(y), Math.Abs(z))); }
        }

        /// <summary>
        /// 转换为绝对值的向量
        /// </summary>
        public Vector3 Abs
        {
            get { return new Vector3(Math.Abs(x), Math.Abs(y), Math.Abs(z)); }
        }

        /// <summary>
        /// 单位化当前实例,并返回单位化前长度
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 返回单位化后的新向量
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 交换 X,Y的值
        /// </summary>
        /// <returns></returns>
        public Vector3 SwitchXY()
        {
            var temp = x;
            x = y;
            y = temp;
            return this;
        }

        /// <summary>
        /// 是否为单位向量
        /// </summary>
        public bool IsNormalized
        {
            get { return Math.Abs((x * x + y * y + z * z) - 1) < MathUtil.ZeroTolerance; }
        }

        /// <summary>
        /// 是否是无穷值
        /// </summary>
        public bool IsFinite
        {
            get { double f = x + y + z; return double.IsNaN(f) == false && double.IsInfinity(f) == false; }
        }

        /// <summary>
        /// 圆整
        /// </summary>
        /// <param name="nDecimals"></param>
        public void Round(int nDecimals)
        {
            x = Math.Round(x, nDecimals);
            y = Math.Round(y, nDecimals);
            z = Math.Round(z, nDecimals);
        }

        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double Dot(Vector3 v2)
        {
            return x * v2.x + y * v2.y + z * v2.z;
        }

        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double Dot(ref Vector3 v2)
        {
            return x * v2.x + y * v2.y + z * v2.z;
        }

        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double Dot(Vector3 v1, Vector3 v2)
        {
            return v1.Dot(ref v2);
        }

        /// <summary>
        /// 叉乘
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public Vector3 Cross(Vector3 v2)
        {
            return new Vector3(
                y * v2.z - z * v2.y,
                z * v2.x - x * v2.z,
                x * v2.y - y * v2.x);
        }

        /// <summary>
        /// 叉乘
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public Vector3 Cross(ref Vector3 v2)
        {
            return new Vector3(
                y * v2.z - z * v2.y,
                z * v2.x - x * v2.z,
                x * v2.y - y * v2.x);
        }

        /// <summary>
        /// 叉乘
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 距离的平方值
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double DistanceSquared(Vector3 v2)
        {
            double dx = v2.x - x, dy = v2.y - y, dz = v2.z - z;
            return dx * dx + dy * dy + dz * dz;
        }

        /// <summary>
        /// 距离的平方值
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double DistanceSquared(ref Vector3 v2)
        {
            double dx = v2.x - x, dy = v2.y - y, dz = v2.z - z;
            return dx * dx + dy * dy + dz * dz;
        }

        /// <summary>
        /// 获取和另外一点的距离
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double Distance(Vector3 v2)
        {
            double dx = v2.x - x, dy = v2.y - y, dz = v2.z - z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// 设置当前实例的值
        /// </summary>
        /// <param name="o"></param>
        public void Set(Vector3 o)
        {
            x = o.x; y = o.y; z = o.z;
        }

        /// <summary>
        /// 设置当前实例的值
        /// </summary>
        /// <param name="fX"></param>
        /// <param name="fY"></param>
        /// <param name="fZ"></param>
        public void Set(double fX, double fY, double fZ)
        {
            x = fX; y = fY; z = fZ;
        }

        /// <summary>
        /// 加
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public Vector3 Add(Vector3 vector3)
        {
            return new Vector3(x + vector3.x, y + vector3.y, z + vector3.z);
        }

        /// <summary>
        /// 向量缩放
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public Vector3 Scale(double length)
        {
            return new Vector3(x * length, y * length, z * length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public Vector3 Orthogonal()
        {
            throw new NotImplementedException();
        }

        #region 操作符

        /// <summary>
        /// 向量相减
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.x, -v.y, -v.z);
        }

        /// <summary>
        /// 向量相乘
        /// </summary>
        /// <param name="f"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 operator *(double f, Vector3 v)
        {
            return new Vector3(f * v.x, f * v.y, f * v.z);
        }

        /// <summary>
        /// 向量相乘
        /// </summary>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 v, double f)
        {
            return new Vector3(f * v.x, f * v.y, f * v.z);
        }

        /// <summary>
        /// 向量除
        /// </summary>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 v, double f)
        {
            return new Vector3(v.x / f, v.y / f, v.z / f);
        }

        /// <summary>
        /// 除
        /// </summary>
        /// <param name="f"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 operator /(double f, Vector3 v)
        {
            return new Vector3(f / v.x, f / v.y, f / v.z);
        }

        /// <summary>
        /// 乘
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// 除
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        /// <summary>
        /// 加
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 v0, Vector3 v1)
        {
            return new Vector3(v0.x + v1.x, v0.y + v1.y, v0.z + v1.z);
        }

        /// <summary>
        /// 加
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 v0, double f)
        {
            return new Vector3(v0.x + f, v0.y + f, v0.z + f);
        }

        /// <summary>
        /// 减
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 v0, Vector3 v1)
        {
            return new Vector3(v0.x - v1.x, v0.y - v1.y, v0.z - v1.z);
        }

        /// <summary>
        /// 减
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 v0, double f)
        {
            return new Vector3(v0.x - f, v0.y - f, v0.z - f);
        }


        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return (a.x == b.x && a.y == b.y && a.z == b.z);
        }

        /// <summary>
        /// 是否不等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return (a.x != b.x || a.y != b.y || a.z != b.z);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this == (Vector3)obj;
        }


        #endregion

    }

    /// <summary>
    /// 将长度大于等于3 的值转换为 向量
    /// </summary>
    public static class doubleExtesnion
    {
        /// <summary>
        /// 转换为 <see cref="Vector3"/>
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this double[] array)
        {
            if (array.Length >= 3)
            {
                return new Vector3(array);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将数组中指定位置的值转换为 <see cref="Vector3"/>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this double[] array,int x,int y,int z)
        {
            return new Vector3(array[x], array[y], array[z]);
        }
    }
}
