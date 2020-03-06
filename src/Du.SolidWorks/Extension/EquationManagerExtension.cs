using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    public static class EquationManagerExtension
    {
        /// <summary>
        /// 获取所有的方程式
        /// </summary>
        /// <param name="equMgr"></param>
        /// <returns></returns>
        public static List<swEqu> GetAllEqu(this EquationMgr equMgr)
        {
            int count = equMgr.GetCount();
            List<swEqu> swEquList = new List<swEqu>();
            for (int i = 0; i < count; i++)
            {
                swEqu equ = equMgr.GetEqu(i);
                swEquList.Add(equ);
            }
            return swEquList;
        }
        /// <summary>
        /// 设置方程式
        /// </summary>
        /// <param name="equMgr"></param>
        /// <param name="Index">序号</param>
        /// <param name="Value">表达式</param>
        /// <returns></returns>
        public static bool SetEqu(this EquationMgr equMgr,int Index,string equation)
        {
            equMgr.Equation[Index] = equation;
            string value = equMgr.Equation[Index];
            Debug.Print(value);
            if (value != equation)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 设置表达式值
        /// </summary>
        /// <param name="equMgr"></param>
        /// <param name="name"></param>
        /// <param name="equation">表达式</param>
        /// <returns></returns>
        /// <remarks>
        /// 全局变量表达式: "B" = 2
        /// 组件表达式:     "N_SPOKES@CirPattern1" = "BARLENGTH@Sketch2" /10
        /// 尺寸表达式       "D1@Extrude2" = (("D1@Extrude3">20, 15, 6))+5
        /// </remarks>
        public static bool SetEqu(this EquationMgr equMgr, string name, string equation)
        {
            int Index = equMgr.GetAllEqu().
                Where(p => p.Equation.Split('=')?.FirstOrDefault()?.Replace("\"", "").Trim() == name)
                .Select(p => p.Index).FirstOrDefault();

            equMgr.Equation[Index] = equation;
            if (equMgr.Equation[Index] != equation)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equMgr"></param>
        /// <param name="equationValue">字典的Value应该为表达式</param>
        /// <remarks>
        /// 全局变量表达式: "B" = 2
        /// 组件表达式:     "N_SPOKES@CirPattern1" = "BARLENGTH@Sketch2" /10
        /// 尺寸表达式       "D1@Extrude2" = (("D1@Extrude3">20, 15, 6))+5
        /// </remarks>
        public static void SetEqu(this EquationMgr equMgr,Dictionary<int,string> equationValue)
        {
            foreach (var item in equationValue)
            {
                var result = equMgr.SetEqu(item.Key, item.Value);
                if (!result)
                {
                    throw new Exception("方程式值设置失败," + item.Key + "," + item.Value);
                }
            }
        }

        /// <summary>
        /// 设置全局变量的值
        /// </summary>
        /// <param name="equMgr"></param>
        /// <param name="equList"></param>
        /// <remarks>
        /// 全局变量表达式: "B" = 2
        /// </remarks>
        public static void SetGlobalEqu(this EquationMgr equMgr, List<swEqu> equList)
        {
            foreach (var item in equList)
            {
                if (item.GlobalVariable)
                {
                    var result = equMgr.SetEqu(item.Index, item.Equation);
                    if (!result)
                    {
                        throw new Exception("方程式值设置失败," + item.Index + "," + item.Value);
                    }
                }
            }
        }

        /// <summary>
        /// 获取单条方程式
        /// </summary>
        /// <param name="equMgr"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static swEqu GetEqu(this EquationMgr equMgr,int i)
        {
            return new swEqu()
            {
                Index = i,
                Equation = equMgr.Equation[i],
                Value = equMgr.Value[i],
                Status = equMgr.Status,
                Suppression = equMgr.Suppression[i],
                GlobalVariable = equMgr.GlobalVariable[i]
            };
        }

        /// <summary>
        /// 添加方程式
        /// </summary>
        /// <param name="equMgr"></param>
        /// <param name="equation">方程式表达式</param>
        /// <param name="solve">是否立刻求解</param>
        /// <param name="index">序号,-1代表在后面添加</param>
        /// <param name="configOption">配置选项,默认为所有配置</param>
        /// <param name="configName">如果不是所有配置,指定配置名</param>
        /// <returns>-1 代表添加失败,成功则返回Index</returns>
        public static int AddEqu(this EquationMgr equMgr,string equation,
            bool solve = true,int index = -1, 
            swInConfigurationOpts_e configOption = swInConfigurationOpts_e.swAllConfiguration,
            string configName = "")
        {
            return equMgr.Add3(index, equation, solve, (int)configOption, configName);
        }
    }

    public class swEqu
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 方程式表达式
        /// </summary>
        public string Equation { get; set; }

        private string[] equStr { get { return Equation.Split('='); } }

        /// <summary>
        /// 表达式名-不带双引号
        /// </summary>
        /// <remarks>
        /// 例子
        /// 阵列个数: N_SPOKES@CirPattern1
        /// 全局变量名: B
        /// 尺寸名: D1@Extrude2
        /// </remarks>
        public string VarName
        {
            get
            {
                return equStr.Length == 2 ?
                    equStr[0].Replace("\"", "")
                    : string.Empty;
            }
        }

        /// <summary>
        /// 表达式值--带双引号
        /// </summary>
        /// <remarks>
        /// 例子
        /// "BARLENGTH@Sketch2" /10
        /// (("D1@Extrude3">20, 15, 6))+5
        /// 0.05
        /// </remarks>
        public string VarValue
        {
            get
            {
                return equStr.Length == 2 ?
                    equStr[1].Trim()
                    : string.Empty;
            }
        }

        /// <summary>
        /// 设置表达式名字
        /// </summary>
        /// <param name="equation">表达式 "N_SPOKES@CirPattern1" = "BARLENGTH@Sketch2" /10
        /// </param>
        /// <param name="Name">不需要加双引号 N_SPOKES@CirPattern1</param>
        /// <returns></returns>
        public static string SetEquationName(string equation,string Name)
        {
            string[] equStr = equation.Split('=');
            if (equStr.Length == 2)
            {
                return  "\"" + Name + "\"" + " =" + equStr[1];
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 设置表达式值
        /// </summary>
        /// <param name="equation">表达式 "D1@Extrude2" = (("D1@Extrude3">20, 15, 6))+5
        /// </param>
        /// <param name="Value">(("D1@Extrude3">20, 15, 6))+10</param>
        /// <returns>"D1@Extrude2" = (("D1@Extrude3">20, 15, 6))+10</returns>
        public static string SetEquationValue(string equation, string Value)
        {
            string[] equStr = equation.Split('=');
            if (equStr.Length == 2)
            {
                return equStr[0] + "=" + Value;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 全局变量设置值
        /// </summary>
        public swEqu SetGlobalValue(double value)
        {
            if (GlobalVariable)
            {
                SetValue(value.ToString());
                //if (equStr.Length == 2)
                //{
                //    Equation = equStr[0] + "=" + value;
                //}
            }
            else
            {
                throw new Exception("不是全局变量");
            }
            return this;
        }

        /// <summary>
        /// 设置全局变量名
        /// </summary>
        /// <param name="Name">不需要加双引号</param>
        public swEqu SetGlobalName(string Name)
        {
            if (GlobalVariable)
            {
                if (equStr.Length == 2)
                {
                    Equation = "\"" + Name + "\"" + " =" + equStr[1];
                }
            }
            else
            {
                throw new Exception("不是全局变量");
            }
            return this;
        }
        /// <summary>
        /// 设置表达式名
        /// </summary>
        /// <param name="Name"></param>
        public void SetName(string Name)
        {

                if (equStr.Length == 2)
                {
                    Equation = "\"" + Name + "\"" + " =" + equStr[1];
                }
        }

        public void SetValue(string value)
        {
            if (equStr.Length == 2)
            {
                if (string.IsNullOrEmpty(Remark))
                {
                    Equation = equStr[0] + "=" + value;

                }
                else
                {
                    Equation = equStr[0] + "=" + value + "\'" + Remark;
                }
            }
        }

        /// <summary>
        /// 值(求解后的值)
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 当前序号下的方程式是否求解成功
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否压缩
        /// </summary>
        public bool Suppression { get; set; }

        /// <summary>
        /// 是否是全局变量 2016 SP4 Revision Number 24.4
        /// </summary>
        public bool GlobalVariable { get; set; }

        public string Remark
        {
            get { return 
                    equStr[1].Contains('\'') ? equStr[1].Split('\'')[1] : string.Empty; }
            set {
                if (equStr[1].Contains('\''))
                {
                    Equation = equStr[0] + "=" + equStr[1].Split('\'')[0] + "'"+value;
                }
                else
                {
                    Equation = equStr[0] + "=" + equStr[1] + "'" + value;
                }
            }
        }
    }
}
