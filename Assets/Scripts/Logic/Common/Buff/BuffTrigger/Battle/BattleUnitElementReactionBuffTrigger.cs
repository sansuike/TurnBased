using System;

  /// <summary>
    /// 元素反应触发
    /// </summary>
    public class BattleUnitElementReactionBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            // 元素反应类型
            int beforeElement = Convert.ToInt32(args[0]);
            int afterElement = Convert.ToInt32(args[1]);
            if (_triggerParams[0] == -1 || _triggerParams[0] == beforeElement || _triggerParams[0] == afterElement)
            {
                return true;
            }
            
            // if (_triggerParams[0] != -1 && _triggerParams[0] != beforeElement)
            // {
            //     return false;
            // }
            
            //
            // if (_triggerParams[1] != -1 && _triggerParams[1] != afterElement)
            // {
            //     return false;
            // }

            return false;
        }
    }
