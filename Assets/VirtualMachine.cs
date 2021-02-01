using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Instruction : byte
{
	INST_SET_HEALTH = 0x0,
	INST_SET_WISDOM = 0x1,
	INST_SET_AGILITY = 0x02,

	INST_PLAY_SOUND = 0x03,
	INST_SPAWN_PARTICLES = 0x04,

	INST_GET_HEALTH = 0x05,
	INST_GET_WISDOM = 0x06,
	INST_GET_AGILITY = 0x07,

	INST_LITERAL = 0x10,
	INST_ADD = 0x11,
}

public class Wizard
{
	public int health { get; set; }
	public int wisdom { get; set; }
	public int agility { get; set; }
}

public class VirtualMachine
{
	public void Interpret(byte[] bytes)
	{
		for (var i = 0; i < bytes.Length; ++i)
		{
			var instruction = bytes[i];
			switch (instruction)
			{
				case (byte)Instruction.INST_LITERAL:
					{
						// 次のbyteをpush
						int value = bytes[++i];
						Push(value);
					}
					break;
				case (byte)Instruction.INST_ADD:
					{
						int b = Pop();
						int a = Pop();
						Push(a + b);
					}
					break;

				case (byte)Instruction.INST_SET_HEALTH:
					{
						var amount = Pop();
						var wizard = Pop();
						SetHealth(wizard, amount);
					}
					break;
				case (byte)Instruction.INST_SET_WISDOM:
					{
						var amount = Pop();
						var wizard = Pop();
						SetWisdom(wizard, amount);
					}
					break;
				case (byte)Instruction.INST_SET_AGILITY:
					{
						var amount = Pop();
						var wizard = Pop();
						SetAgility(wizard, amount);
					}
					break;

				case (byte)Instruction.INST_GET_HEALTH:
					{
						var wizard = Pop();
						Push(GetHealth(wizard));
					}
					break;
				case (byte)Instruction.INST_GET_WISDOM:
					{
						var wizard = Pop();
						Push(GetWisdom(wizard));
					}
					break;
				case (byte)Instruction.INST_GET_AGILITY:
					{
						var wizard = Pop();
						Push(GetAgility(wizard));
					}
					break;

				case (byte)Instruction.INST_PLAY_SOUND:
					{
						var id = Pop();
						PlaySound(id);
					}
					break;
				case (byte)Instruction.INST_SPAWN_PARTICLES:
					{
						var id = Pop();
						SpawnParticles(id);
					}
					break;
			}
		}
	}

	void Push(int value)
	{
		Debug.Assert(stackSize < MaxStackSize);
		stack[stackSize++] = value;
	}
	int Pop()
	{
		Debug.Assert(stackSize > 0);
		return stack[--stackSize];
	}

	void SetHealth(int wizard, int amount)
	{
		wizards[wizard].health = amount;
	}
	void SetWisdom(int wizard, int amount)
	{
		wizards[wizard].wisdom = amount;
	}
	void SetAgility(int wizard, int amount)
	{
		wizards[wizard].agility = amount;
	}

	int GetHealth(int wizard)
	{
		return wizards[wizard].health;
	}
	int GetWisdom(int wizard)
	{
		return wizards[wizard].wisdom;
	}
	int GetAgility(int wizard)
	{
		return wizards[wizard].agility;
	}

	void PlaySound(int soundId)
	{
	}
	void SpawnParticles(int particleType)
	{
	}

	const int MaxStackSize = 128;
	int stackSize = 0;
	int[] stack = new int[MaxStackSize];

	// TODO : 
	public int GetStackTop() { return stackSize > 0 ? stack[stackSize-1] : 0; }
	public Wizard GetWizard(int wizard) { return wizards[wizard]; }
	Wizard[] wizards = new Wizard[] { new Wizard(), new Wizard() };
}
