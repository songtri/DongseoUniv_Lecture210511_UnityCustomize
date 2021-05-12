using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiTarget
{
	None,
	Self,
	Ally,
	Enemy,
}

public enum Condition
{
	None,
	Health,
	Target,
}

public enum Behavior
{
	None,
	NormalAttack,
	UseSkill,
	Wait,
}

public class CharacterAI
{
	public AiTarget Target;
	public Condition ConditionToCheck;
	public int ConditionValue1;
	public int ConditionValue2;
	public Behavior Behavior;
	public int BehaviorValue;
}

public class AiData
{
	[SerializeField]
	public List<CharacterAI> aiData = new List<CharacterAI>();
}