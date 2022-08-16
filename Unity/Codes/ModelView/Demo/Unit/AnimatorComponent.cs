using System.Collections.Generic;
using UnityEngine;

namespace ET
{
	public enum MotionType
	{
		None,
		IsMoving,
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
		public Animator BodyAnimator;
		public Animator ArmAnimator;
		public Animator HairAnimator;
		public Animator CurrentControlAnimator;
		public List<Animator> Animators = new List<Animator>(3);
	}
}