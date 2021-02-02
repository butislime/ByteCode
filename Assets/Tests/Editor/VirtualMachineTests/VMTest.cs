using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Tests.Editor
{
	public class VMTest
	{
		VirtualMachine vm;
		[SetUp]
		public void SetUp()
		{
			vm = new VirtualMachine();
		}
		[TearDown]
		public void TearDown()
		{
			vm = null;
		}

		[TestCase(0, 100)]
		[TestCase(1, 200)]
		public void SetHealthTest(int wizard, int health)
		{
			byte[] bytes = new byte[] {
				(byte)Instruction.INST_LITERAL, (byte)wizard,
				(byte)Instruction.INST_LITERAL, (byte)health,
				(byte)Instruction.INST_SET_HEALTH,
			};
			vm.Interpret(bytes);

			Assert.IsTrue(vm.GetWizard(wizard).health == health);
		}

		[TestCase(0, 100)]
		[TestCase(1, 200)]
		public void SetWisdomTest(int wizard, int wisdom)
		{
			byte[] bytes = new byte[] {
				(byte)Instruction.INST_LITERAL, (byte)wizard,
				(byte)Instruction.INST_LITERAL, (byte)wisdom,
				(byte)Instruction.INST_SET_WISDOM,
			};
			vm.Interpret(bytes);

			Assert.IsTrue(vm.GetWizard(wizard).wisdom == wisdom);
		}

		[TestCase(0, 100)]
		[TestCase(1, 200)]
		public void SetAgilityTest(int wizard, int agility)
		{
			byte[] bytes = new byte[] {
				(byte)Instruction.INST_LITERAL, (byte)wizard,
				(byte)Instruction.INST_LITERAL, (byte)agility,
				(byte)Instruction.INST_SET_AGILITY,
			};
			vm.Interpret(bytes);

			Assert.IsTrue(vm.GetWizard(wizard).agility == agility);
		}

		[TestCase(1, 2)]
		[TestCase(10, 20)]
		public void AddTest(int a, int b)
		{
			byte[] bytes = new byte[] {
				(byte)Instruction.INST_LITERAL, (byte)a,
				(byte)Instruction.INST_LITERAL, (byte)b,
				(byte)Instruction.INST_ADD,
			};
			vm.Interpret(bytes);

			Assert.IsTrue(vm.GetStackTop() == a + b);
		}

		[TestCase(0, 45, 7, 11)]
		public void UseCaseTest(int wizard, int health, int wisdom, int agility)
		{
			byte[] bytes = new byte[] {
				// Init
				(byte)Instruction.INST_LITERAL, (byte)wizard,
				(byte)Instruction.INST_LITERAL, (byte)health,
				(byte)Instruction.INST_SET_HEALTH,
				(byte)Instruction.INST_LITERAL, (byte)wizard,
				(byte)Instruction.INST_LITERAL, (byte)wisdom,
				(byte)Instruction.INST_SET_WISDOM,
				(byte)Instruction.INST_LITERAL, (byte)wizard,
				(byte)Instruction.INST_LITERAL, (byte)agility,
				(byte)Instruction.INST_SET_AGILITY,

				// health = health + (wisdom + agility) / 2
				(byte)Instruction.INST_LITERAL, (byte)wizard,
				(byte)Instruction.INST_LITERAL, (byte)wizard,
				(byte)Instruction.INST_GET_HEALTH,
				(byte)Instruction.INST_LITERAL, (byte)wizard,
				(byte)Instruction.INST_GET_AGILITY,
				(byte)Instruction.INST_LITERAL, (byte)wizard,
				(byte)Instruction.INST_GET_WISDOM,
				(byte)Instruction.INST_ADD,
				(byte)Instruction.INST_LITERAL, 2,
				(byte)Instruction.INST_DIV,
				(byte)Instruction.INST_ADD,
				(byte)Instruction.INST_SET_HEALTH,
			};

			vm.Interpret(bytes);

			Assert.IsTrue(vm.GetWizard(wizard).health == (health + (wisdom + agility) / 2));
		}
	}
}
