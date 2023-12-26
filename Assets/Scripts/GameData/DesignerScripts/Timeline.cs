using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignerScripts
{
    public class Timeline
    {
        public static Dictionary<string, TimelineEvent> functions = new Dictionary<string, TimelineEvent>()
        {
            { "CasterPlayAnim", CasterPlayAnim },
            { "CasterForceMove", CasterForceMove },
            { "SetCasterControlState", SetCasterControlState },
            { "PlaySightEffectOnCaster", PlaySightEffectOnCaster },
            { "StopSightEffectOnCaster", StopSightEffectOnCaster },
            { "CasterImmune", CasterImmune },
            { "CreateAoE", CreateAoE },
            { "AddBuffToCaster", AddBuffToCaster },
            { "SummonCharacter", SummonCharacter }
        };

        private static void SummonCharacter(TimelineObj timeline, object[] args)
        {
            throw new System.NotImplementedException();
        }

        private static void AddBuffToCaster(TimelineObj timeline, object[] args)
        {
            throw new System.NotImplementedException();
        }

        private static void CreateAoE(TimelineObj timeline, object[] args)
        {
            throw new System.NotImplementedException();
        }

        private static void CasterImmune(TimelineObj timeline, object[] args)
        {
            throw new System.NotImplementedException();
        }

        private static void StopSightEffectOnCaster(TimelineObj timeline, object[] args)
        {
            throw new System.NotImplementedException();
        }

        private static void PlaySightEffectOnCaster(TimelineObj timeline, object[] args)
        {
            throw new System.NotImplementedException();
        }

        private static void SetCasterControlState(TimelineObj timeline, object[] args)
        {
            throw new System.NotImplementedException();
        }

        private static void CasterForceMove(TimelineObj timeline, object[] args)
        {
            throw new System.NotImplementedException();
        }

        private static void CasterPlayAnim(TimelineObj timeline, object[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}