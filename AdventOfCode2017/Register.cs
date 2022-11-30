namespace AdventOfCode2017
{
    public class Register
    {
        public static RegisterInstructions[] ConvertInstructions(string[] instructions)
        {
            List<RegisterInstructions> registerInstructions = new List<RegisterInstructions>();
            foreach (var formattedInstructions in instructions)
            {
                registerInstructions.Add(new RegisterInstructions(formattedInstructions));
            }

            return registerInstructions.ToArray();
        }

        public static (Dictionary<string, int> register, int maxRegistryValueReached) ProcessRegister(RegisterInstructions[] instructions)
        {
            int maxRegistryValueReached = 0;
            Dictionary<string, int> register = new Dictionary<string, int>();
            foreach (var registerInstructions in instructions)
            {
                SetNewRegistryEntriesIfNeeded(register, registerInstructions);

                if (IsLogicConditionValid(register, registerInstructions))
                {
                    AdjustRegisterAmount(register, registerInstructions);
                    if (register[registerInstructions.RegisterName] > maxRegistryValueReached)
                    {
                        maxRegistryValueReached = register[registerInstructions.RegisterName];
                    }
                }
            }

            return (register, maxRegistryValueReached);
        }

        private static bool IsLogicConditionValid(Dictionary<string, int> register, RegisterInstructions registerInstructions)
        {
            switch (registerInstructions.LogicConditional)
            {
                case RegisterInstructions.Conditional.GreaterThan:
                    if (register[registerInstructions.LogicRegisterName] > registerInstructions.LogicAmount)
                    {
                        return true;
                    }
                    break;
                case RegisterInstructions.Conditional.GreaterThanOrEqual:
                    if (register[registerInstructions.LogicRegisterName] >= registerInstructions.LogicAmount)
                    {
                        return true;
                    }
                    break;
                case RegisterInstructions.Conditional.LessThan:
                    if (register[registerInstructions.LogicRegisterName] < registerInstructions.LogicAmount)
                    {
                        return true;
                    }
                    break;
                case RegisterInstructions.Conditional.LessThanOrEqual:
                    if (register[registerInstructions.LogicRegisterName] <= registerInstructions.LogicAmount)
                    {
                        return true;
                    }
                    break;
                case RegisterInstructions.Conditional.Equal:
                    if (register[registerInstructions.LogicRegisterName] == registerInstructions.LogicAmount)
                    {
                        return true;
                    }
                    break;
                case RegisterInstructions.Conditional.NotEqual:
                    if (register[registerInstructions.LogicRegisterName] != registerInstructions.LogicAmount)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }

            return false;
        }

        private static void SetNewRegistryEntriesIfNeeded(Dictionary<string, int> register, RegisterInstructions registerInstructions)
        {
            if (!register.ContainsKey(registerInstructions.RegisterName))
            {
                register.Add(registerInstructions.RegisterName, 0);
            }

            if (!register.ContainsKey(registerInstructions.LogicRegisterName))
            {
                register.Add(registerInstructions.LogicRegisterName, 0);
            }
        }

        private static void AdjustRegisterAmount(Dictionary<string, int> register, RegisterInstructions registerInstructions)
        {
            if (registerInstructions.Instruction == RegisterInstructions.Operation.Dec)
            {
                register[registerInstructions.RegisterName] -= registerInstructions.OperationAmount;
            }
            else
            {
                register[registerInstructions.RegisterName] += registerInstructions.OperationAmount;
            }
        }
    }

    public class RegisterInstructions
    {
        public enum Operation
        {
            Inc,
            Dec
        }

        public enum Conditional
        {
            GreaterThan,
            LessThan,
            GreaterThanOrEqual,
            LessThanOrEqual,
            Equal,
            NotEqual
        }

        public string RegisterName { get; }

        public Operation Instruction { get; private set; }

        public int OperationAmount { get; }

        public string LogicRegisterName { get; }

        public Conditional LogicConditional { get; private set; }

        public int LogicAmount { get; }

        public RegisterInstructions(string formattedInstuctions)
        {
            char[] delimiterChars = { ' ' };
            var items = formattedInstuctions.Split(delimiterChars).Where(s => s.Length > 0).ToArray();
            RegisterName = items[0];
            SetOperation(items[1]);
            OperationAmount = int.Parse(items[2]);
            LogicRegisterName = items[4];
            SetLogicConditional(items[5]);
            LogicAmount = int.Parse(items[6]);
        }

        private void SetOperation(string operation)
        {
            Enum.TryParse(operation, true, out Operation convertedInstruction);

            Instruction = convertedInstruction;
        }

        private void SetLogicConditional(string logicConditional)
        {
            switch (logicConditional)
            {
                case ">":
                    LogicConditional = Conditional.GreaterThan;
                    break;
                case "<":
                    LogicConditional = Conditional.LessThan;
                    break;
                case ">=":
                    LogicConditional = Conditional.GreaterThanOrEqual;
                    break;
                case "<=":
                    LogicConditional = Conditional.LessThanOrEqual;
                    break;
                case "==":
                    LogicConditional = Conditional.Equal;
                    break;
                case "!=":
                    LogicConditional = Conditional.NotEqual;
                    break;
                default:
                    throw new ArgumentException($"Unexpected logic conditional: {logicConditional}");
            }
        }
    }
}
