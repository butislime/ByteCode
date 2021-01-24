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

	INST_LITERAL = 0x10,
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
					// 次のbyteをpush
					int value = bytes[++i];
					Push(value);
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
	}
	void SetWisdom(int wizard, int amount)
	{
	}
	void SetAgility(int wizard, int amount)
	{
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
}
