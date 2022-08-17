using System;
using BM;
using UnityEngine;

namespace ET
{
    public static class AnimatorHelper
    {
        public static RuntimeAnimatorController LoadAnimatorController(string name)
        {
            try
            {
                var animator = AssetComponent.Load<RuntimeAnimatorController>(name.StringToAB());
                ;
                if (animator == null)
                {
                    Log.Error($"sprite is null: {name}");
                }

                return animator;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
    }
}