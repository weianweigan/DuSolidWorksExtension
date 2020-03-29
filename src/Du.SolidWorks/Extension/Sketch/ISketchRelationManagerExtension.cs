using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Du.SolidWorks.Extension
{
    /// <summary>
    /// 添加草图约束的扩展方法,常用约束
    /// <see cref="swConstraintType_e.swConstraintType_FIXED" 固定约束/>
    /// <see cref="swConstraintType_e.swConstraintType_COINCIDENT" 重合/>
    /// <see cref="swConstraintType_e.swConstraintType_COLINEAR" 共线/>
    /// <see cref="swConstraintType_e.swConstraintType_CONCENTRIC" 同心/>
    /// <see cref="swConstraintType_e.swConstraintType_FIXEDSLOT" 固定槽口/>
    /// <see cref="swConstraintType_e.swConstraintType_TANGENT" 相切/>
    /// <see cref="swConstraintType_e.swConstraintType_TANGENTFACE" 面相切/>
    /// <see cref="swConstraintType_e.swConstraintType_PARALLEL" 平行/>
    /// </summary>
    public static class ISketchRelationManagerExtension
    {

        /// <summary>
        /// 添加草图约束
        /// </summary>
        /// <param name="skeRelMgr"><see cref="ISketchRelationManager"/> Interface</param>
        /// <param name="swConstraintType"><see cref="swConstraintType_e"/> Relation type 约束类型</param>
        /// <param name="entities">需要被约束的实体</param>
        /// <returns></returns>
        public static ISketchRelation AddRelationEx(this ISketchRelationManager skeRelMgr,swConstraintType_e swConstraintType,params object[] entities)
        {
            //使用DispathWrapper 防止产生访问不可访问的内存的异常
            return skeRelMgr.AddRelation(ComWrapper.ObjectArrayToDispatchWrapper(entities), swConstraintType.SWToInt());
        }



        /// <summary>
        /// 获取特定实体允许的约束类型
        /// </summary>
        /// <param name="skeRelMgr"><see cref="ISketchRelationManager"/> Interface</param>
        /// <param name="entities">entities params</param>
        /// <returns></returns>
        public static IEnumerable<swConstraintType_e> GetAllowedRelationsEx(this ISketchRelationManager skeRelMgr,params object[] entities)
        {
            return (skeRelMgr.GetAllowedRelations(entities) as object[]).Cast<swConstraintType_e>();
        }

        /// <summary>
        /// 先检查是否能添加此种类型的约束,然后确定是否添加约束
        /// </summary>
        /// <param name="skeRelMgr"><see cref="ISketchRelationManager"/> Interface</param>
        /// <param name="swConstraintType"><see cref="swConstraintType_e"/></param>
        /// <param name="sketchRelationResult"><see cref="ISketchRelation"/> 输出参数</param>
        /// <param name="entities">需要被约束的草图实体</param>
        /// <returns></returns>
        public static bool TryAddRelationEx(this ISketchRelationManager skeRelMgr, swConstraintType_e swConstraintType,out ISketchRelation sketchRelationResult, params object[] entities )
        {
            var consTypes = skeRelMgr.GetAllowedRelationsEx(entities);
            if (consTypes.Contains(swConstraintType))
            {
                sketchRelationResult = skeRelMgr.AddRelationEx(swConstraintType, entities);
                return true;
            }
            else
            {
                sketchRelationResult = null;
                return false;
            }
        }

        #region 特定的约束

        /// <summary>
        /// 对两个草图实体添加重合
        /// </summary>
        /// <typeparam name="TEntity1">草图实体类型</typeparam>
        /// <typeparam name="TEntity2">草图实体类型</typeparam>
        /// <param name="skeRelMgr"><see cref="ISketchRelationManager"/> Interface</param>
        /// <param name="entity1">草图实体对象</param>
        /// <param name="entity2">草图实体对象</param>
        /// <returns></returns>
        public static ISketchRelation AddCoincident<TEntity1, TEntity2>(this ISketchRelationManager skeRelMgr, TEntity1 entity1, TEntity2 entity2)
        {
            return skeRelMgr.AddRelationEx(swConstraintType_e.swConstraintType_COINCIDENT, entity1, entity2);
        }

        /// <summary>
        /// 添加固定约束
        /// </summary>
        /// <param name="skeRelMgr"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static ISketchRelation AddFixed(this ISketchRelationManager skeRelMgr,params object[] entities)
        {
            return skeRelMgr.AddRelationEx(swConstraintType_e.swConstraintType_FIXED, entities);
        }

        #endregion
    }
}
