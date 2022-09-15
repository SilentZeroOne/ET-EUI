using System.Collections.Generic;
using UnityEngine;

namespace ET
{
	public enum MotionType
	{
		None,
		IsMoving,
	}
	
	public enum AnimatorType
	{
		Arm,
		Body,
		Hair
	}

	//WORKFLOW: 每次添加新的状态需要在这加
	public enum AnimatorStatus
	{
		None,
		Carried
	}

	public enum AnimatorControlType
	{
		SetTrigger,
		ResetTrigger,
		SetBool,
		SetInt,
		SetFloat,
		SetSpeed,
		ResetSpeed,
		RunAnimator,
		PauseAnimator
	}

	[ComponentOf(typeof(Unit))]
	public class AnimatorComponent : Entity, IAwake, IUpdate, IDestroy
	{
		public Dictionary<string, AnimationClip> animationClips = new Dictionary<string, AnimationClip>();
		public HashSet<string> Parameter = new HashSet<string>();

		public MotionType MotionType;
		public float MontionSpeed;
		public float InputX;
		public float InputY;
		public bool isStop;
		public float stopSpeed;
		public Animator CurrentControlAnimator;
		public Dictionary<AnimatorType, Animator> Animators = new Dictionary<AnimatorType, Animator>(3);
	}
}