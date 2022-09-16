using System;
using UnityEngine;

namespace ET
{
	[ObjectSystem]
	public class AnimatorComponentAwakeSystem : AwakeSystem<AnimatorComponent>
	{
		public override void Awake(AnimatorComponent self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class AnimatorComponentUpdateSystem : UpdateSystem<AnimatorComponent>
	{
		public override void Update(AnimatorComponent self)
		{
			self.Update();
		}
	}
	
	[ObjectSystem]
	public class AnimatorComponentDestroySystem : DestroySystem<AnimatorComponent>
	{
		public override void Destroy(AnimatorComponent self)
		{
			self.animationClips = null;
			self.Parameter = null;
			self.Animators.Clear();
		}
	}

	[FriendClass(typeof(AnimatorComponent))]
	public static class AnimatorComponentSystem
	{
		public static void Awake(this AnimatorComponent self)
		{
			var go = self.Parent.GetComponent<GameObjectComponent>().GameObject;
			Animator bodyAnimator = go.GetComponentFormRC<Animator>("Body");
			Animator armAnimator = go.GetComponentFormRC<Animator>("Arm");
			Animator hairAnimator = go.GetComponentFormRC<Animator>("Hair");
			Animator toolAnimator = go.GetComponentFormRC<Animator>("Tool");

			if (bodyAnimator == null || armAnimator == null || hairAnimator == null || toolAnimator == null)
			{
				return;
			}

			self.Animators.Add(AnimatorType.Body, bodyAnimator);
			self.Animators.Add(AnimatorType.Arm, armAnimator);
			self.Animators.Add(AnimatorType.Hair, hairAnimator);
			self.Animators.Add(AnimatorType.Tool, toolAnimator);

			foreach (var animator in self.Animators.Values)
			{
				if (animator.runtimeAnimatorController == null)
				{
					return;
				}
			}
			
			foreach (var animator in self.Animators.Values)
			{
				if (animator.runtimeAnimatorController.animationClips == null)
				{
					return;
				}
			}

			foreach (var animator in self.Animators.Values)
			{
				foreach (AnimationClip animationClip in animator.runtimeAnimatorController.animationClips)
				{
					self.animationClips[animationClip.name] = animationClip;
				}
			}

			foreach (AnimatorControllerParameter animatorControllerParameter in bodyAnimator.parameters)
			{
				self.Parameter.Add(animatorControllerParameter.name);
			}
		}

		public static void Update(this AnimatorComponent self)
		{
			if (self.isStop)
			{
				return;
			}

			if (self.MotionType == MotionType.None)
			{
				return;
			}

			try
			{
				self.ForEveryAnimator(AnimatorControlType.SetFloat, "MotionSpeed", self.MontionSpeed.ToString());
				self.ForEveryAnimator(AnimatorControlType.SetFloat, "InputX", self.InputX.ToString());
				self.ForEveryAnimator(AnimatorControlType.SetFloat, "InputY", self.InputY.ToString());
				self.ForEveryAnimator(AnimatorControlType.SetFloat, "MouseX", self.MouseX.ToString());
				self.ForEveryAnimator(AnimatorControlType.SetFloat, "MouseY", self.MouseY.ToString());

				self.ForEveryAnimator(AnimatorControlType.SetTrigger, self.MotionType.ToString());

				self.MontionSpeed = 1;
				self.MotionType = MotionType.None;
			}
			catch (Exception ex)
			{
				throw new Exception($"动作播放失败: {self.MotionType}", ex);
			}
		}

		public static bool HasParameter(this AnimatorComponent self, string parameter)
		{
			return self.Parameter.Contains(parameter);
		}

		public static void PlayInTime(this AnimatorComponent self, MotionType motionType, float time)
		{
			AnimationClip animationClip;
			if (!self.animationClips.TryGetValue(motionType.ToString(), out animationClip))
			{
				throw new Exception($"找不到该动作: {motionType}");
			}

			float motionSpeed = animationClip.length / time;
			if (motionSpeed < 0.01f || motionSpeed > 1000f)
			{
				Log.Error($"motionSpeed数值异常, {motionSpeed}, 此动作跳过");
				return;
			}
			self.MotionType = motionType;
			self.MontionSpeed = motionSpeed;
		}

		public static void Play(this AnimatorComponent self, MotionType motionType, float motionSpeed = 1f)
		{
			if (!self.HasParameter(motionType.ToString()))
			{
				return;
			}
			self.MotionType = motionType;
			self.MontionSpeed = motionSpeed;
		}

		public static float AnimationTime(this AnimatorComponent self, MotionType motionType)
		{
			AnimationClip animationClip;
			if (!self.animationClips.TryGetValue(motionType.ToString(), out animationClip))
			{
				throw new Exception($"找不到该动作: {motionType}");
			}
			return animationClip.length;
		}

		public static void PauseAnimator(this AnimatorComponent self)
		{
			if (self.isStop)
			{
				return;
			}
			self.isStop = true;

			if (self.CurrentControlAnimator == null)
			{
				return;
			}
			self.stopSpeed = self.CurrentControlAnimator.speed;
			self.CurrentControlAnimator.speed = 0;
		}

		public static void RunAnimator(this AnimatorComponent self)
		{
			if (!self.isStop)
			{
				return;
			}

			self.isStop = false;

			if (self.CurrentControlAnimator == null)
			{
				return;
			}
			self.CurrentControlAnimator.speed = self.stopSpeed;
		}

		public static void SetBoolValue(this AnimatorComponent self, string name, bool state)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.CurrentControlAnimator.SetBool(name, state);
		}

		public static void SetFloatValue(this AnimatorComponent self, string name, float state)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.CurrentControlAnimator.SetFloat(name, state);
		}

		public static void SetIntValue(this AnimatorComponent self, string name, int value)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.CurrentControlAnimator.SetInteger(name, value);
		}

		public static void SetTrigger(this AnimatorComponent self, string name)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.CurrentControlAnimator.SetTrigger(name);
		}
		
		public static void ResetTrigger(this AnimatorComponent self, string name)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.CurrentControlAnimator.ResetTrigger(name);
		}

		public static void SetAnimatorSpeed(this AnimatorComponent self, float speed)
		{
			self.stopSpeed = self.CurrentControlAnimator.speed;
			self.CurrentControlAnimator.speed = speed;
		}

		public static void ResetAnimatorSpeed(this AnimatorComponent self)
		{
			self.CurrentControlAnimator.speed = self.stopSpeed;
		}

		public static void SetMoveParmas(this AnimatorComponent self, float inputX, float inputY)
		{
			self.InputX = inputX;
			self.InputY = inputY;
		}
		
		public static void SetMouseParmas(this AnimatorComponent self, float mouseX, float mouseY)
		{
			Vector3 playerPos = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject.transform.position;
			var x = mouseX - playerPos.x;
			var y = mouseY - playerPos.y;
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				y = 0;
			}
			else
			{
				x = 0;
			}
			
			self.MouseX = x;
			self.MouseY = y;
		}

		public static void ForEveryAnimator(this AnimatorComponent self,AnimatorControlType controlType,params string[] pars)
		{
			foreach (var animator in self.Animators.Values)
			{
				self.CurrentControlAnimator = animator;
				switch (controlType)
				{
					case AnimatorControlType.SetTrigger:
						self.SetTrigger(pars[0]);
						break;
					case AnimatorControlType.ResetTrigger:
						self.ResetTrigger(pars[0]);
						break;
					case AnimatorControlType.SetBool:
						self.SetBoolValue(pars[0], bool.Parse(pars[1]));
						break;
					case AnimatorControlType.SetInt:
						self.SetIntValue(pars[0], int.Parse(pars[1]));
						break;
					case AnimatorControlType.SetFloat:
						self.SetFloatValue(pars[0], float.Parse(pars[1]));
						break;
					case AnimatorControlType.SetSpeed:
						self.SetAnimatorSpeed(float.Parse(pars[0]));
						break;
					case AnimatorControlType.ResetSpeed:
						self.ResetAnimatorSpeed();
						break;
					case AnimatorControlType.RunAnimator:
						self.RunAnimator();
						break;
					case AnimatorControlType.PauseAnimator:
						self.PauseAnimator();
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof (controlType), controlType, null);
				}
			}
		}

		public static void OverrideAnimator(this AnimatorComponent self, AnimatorType type, string overrideAnimatorName)
		{
			if (self.Animators.ContainsKey(type))
				self.Animators[type].runtimeAnimatorController = AnimatorHelper.LoadAnimatorController(overrideAnimatorName);
			else
			{
				Log.Error($"Try to override animator {type.ToString()} but not exist");
			}
		}
	}
}