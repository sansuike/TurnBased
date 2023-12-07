using System;
using FixMath.NET;

public abstract class BaseBuffTrigger
{
    /// <summary>
        /// buff
        /// </summary>
        protected BuffManager _buffManager;
        /// <summary>
        /// 触发CD(帧)
        /// </summary>
        protected int _cooldown;
        /// <summary>
        /// 上次触发的帧
        /// </summary>
        protected int _lastTriggerFrame;
        /// <summary>
        /// 触发次数
        /// </summary>
        protected int _triggerCount;
        
        /// <summary>
        /// 触发次数
        /// </summary>
        protected int _roundTriggerCount;
        
        /// <summary>
        /// 触发参数
        /// </summary>
        protected int[] _triggerParams;
        /// <summary>
        /// 对应buff
        /// </summary>
        protected Buff _buff;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="buff"></param>
        public virtual void Init(Buff buff)
        {
            _buff = buff;
            _buffManager = buff.BuffManager;
            //_cooldown = buff.BuffCfg.TriggerCoolDown * buff.BuffManager.FrameRate / 1000;
            //_triggerParams = buff.BuffCfg.TriggerParams;
            _lastTriggerFrame = _buffManager.CurrFrame;
        }

        /// <summary>
        /// 是否触发次数达到上限
        /// </summary>
        /// <returns></returns>
        public bool IsTriggerLimit(int round)
        {
            int triggerMaxNumber = GetTriggerMaxNumber(round);
            return triggerMaxNumber != -1 && triggerMaxNumber <= (round <= 0 ? _triggerCount : _roundTriggerCount);
        }

        /// <summary>
        /// 最大触发次数
        /// </summary>
        private int GetTriggerMaxNumber(int round)
        {
            // int i1 = Math.Min(round, _buff.BuffCfg.TriggerMaxNumber.Length - 1);
            // int i2 = Math.Min(round, _buff.BuffCfg.TriggerMaxNumberStack.Length - 1);
            // int triggerMaxNumber = i1 < 0 ? 0 : _buff.BuffCfg.TriggerMaxNumber[i1];
            // int triggerMaxNumberStack = i2 < 0 ? 0 : _buff.BuffCfg.TriggerMaxNumberStack[i2];
            // if(triggerMaxNumber == -1)
            // {
            //     return -1;
            // }
            // return triggerMaxNumber + triggerMaxNumberStack * (_buff.StackCount - 1);
            return 10;
        }

        /// <summary>
        /// 触发几率
        /// </summary>
        private Fix64 _TriggerProbability
        {
            get
            {
                return 0;// _buff.BuffCfg.TriggerProbability +  _buff.BuffCfg.TriggerProbabilityStack * _buff.TriggerProFailCount;
            }
        }


        /// <summary>
        /// 重置触发次数
        /// </summary>
        public void ResetTriggerCount()
        {
            _triggerCount = 0;
        }

        public void RoundResetTriggerCount()
        {
            _roundTriggerCount = 0;
        }
        
        /// <summary>
        /// 触发
        /// </summary>
        /// <param name="args"></param>
        /// <returns>是否成功触发</returns>

        public bool OnTrigger(object[] args)
        {
            //判断回合触发次数
            if(IsTriggerLimit(_buff.Holder.GetFight()._round))
            {
                return false;
            }
            //判断总触发次数
            if(IsTriggerLimit(0))
            {
                return false;
            }
            //判断触发冷却时间
            if(_buffManager.CurrFrame - _lastTriggerFrame < _cooldown)
            {
                return false;
            }
            //判断触发几率
            if(_buffManager.Random.randomFix64() >= _TriggerProbability)
            {
                _buff.TriggerProFailCount++;
                return false;
            }
            //判断是否触发成功
            bool success = Trigger(args);
            if (success)
            {
                _lastTriggerFrame = _buffManager.CurrFrame;
                _triggerCount++;
                _roundTriggerCount++;
                _buff.TriggerProFailCount = 0;
            }
            return success;
        }

        /// <summary>
        /// 触发
        /// </summary>
        /// <param name="args"></param>
        /// <returns>是否成功触发</returns>
        protected abstract bool Trigger(object[] args);
}