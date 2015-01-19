using System.Collections.Generic;

namespace Networking
{

	public sealed class Operation
	{
		public static readonly Operation ADDITION = new Operation("ADDITION", InnerEnum.ADDITION, "add");
		public static readonly Operation SUBSTRACTION = new Operation("SUBSTRACTION", InnerEnum.SUBSTRACTION, "subtract");
		public static readonly Operation MULTIPLICATION = new Operation("MULTIPLICATION", InnerEnum.MULTIPLICATION, "multiply");
		public static readonly Operation DIVISION = new Operation("DIVISION", InnerEnum.DIVISION, "divide");

		private static readonly List<Operation> valueList = new List<Operation>();

		static Operation()
		{
			valueList.Add(ADDITION);
			valueList.Add(SUBSTRACTION);
			valueList.Add(MULTIPLICATION);
			valueList.Add(DIVISION);
		}

		public enum InnerEnum
		{
			ADDITION,
			SUBSTRACTION,
			MULTIPLICATION,
			DIVISION
		}

		private readonly string nameValue;
		private readonly int ordinalValue;
		private readonly InnerEnum innerEnumValue;
		private static int nextOrdinal = 0;


		private readonly string opName;

		internal Operation(string name, InnerEnum innerEnum, string opName)
		{
			this.opName = opName;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}
		public override string toString()
		{
			return opName;
		}

		public static IList<Operation> values()
		{
			return valueList;
		}

		public InnerEnum InnerEnumValue()
		{
			return innerEnumValue;
		}

		public int ordinal()
		{
			return ordinalValue;
		}

		public override string ToString()
		{
			return nameValue;
		}

		public static Operation valueOf(string name)
		{
			foreach (Operation enumInstance in Operation.values())
			{
				if (enumInstance.nameValue == name)
				{
					return enumInstance;
				}
			}
			throw new System.ArgumentException(name);
		}
	}
}