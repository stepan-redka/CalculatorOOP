using System.Globalization;
namespace ClassLibrary
{
    public class MyCalculator
    {
        private readonly Dictionary<string, IOperation> _operations;

        public MyCalculator()
        {
            //collection for accessing ops imediately 
            _operations = new Dictionary<string, IOperation>
            {
                { "+", new Addition() },
                { "-", new Subtraction() },
                { "*", new Multiplication() },
                { "/", new Division() }
            };
        }

        public double Calculate(double a, string op, double b)
        {
            return _operations[op].DoOperation(a, b);
        }
    }



public class ExpressionEvaluator
{
    private readonly MyCalculator _calculator;

    public ExpressionEvaluator(MyCalculator calculator)
    {
        _calculator = calculator;
    }

    public double Evaluate(string expr)
    {
        var values = new Stack<double>();
        var ops = new Stack<string>();

        int i = 0;

        while (i < expr.Length)
        {
            char c = expr[i];

            // ignore whitespace
            if (char.IsWhiteSpace(c))
            {
                i++;
                continue;
            }

            // number
            if (char.IsDigit(c) || c == '.')
            {
                string number = "";
                while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == '.'))
                {
                    number += expr[i];
                    i++;
                }

                if (!double.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                    throw new Exception($"Invalid number: {number}");

                values.Push(value);
                continue;
            }

            // opening parenthesis
            if (c == '(')
            {
                ops.Push("(");
                i++;
                continue;
            }

            // closing parenthesis
            if (c == ')')
            {
                if (!ops.Contains("("))
                    throw new Exception("Mismatched parentheses.");

                while (ops.Peek() != "(")
                {
                    ApplyOperationSafely(values, ops.Pop());
                }

                ops.Pop(); // remove '('
                i++;
                continue;
            }

            // operator (+ - * /)
            if (IsOperator(c))
            {
                string op = c.ToString();

                // apply operators of higher or equal precedence (skip "(")
                while (ops.Count > 0 && ops.Peek() != "(" && Precedence(ops.Peek()) >= Precedence(op))
                {
                    ApplyOperationSafely(values, ops.Pop());
                }

                ops.Push(op);
                i++;
                continue;
            }

            throw new Exception($"Unexpected character: {c}");
        }

        // apply remaining ops
        while (ops.Count > 0)
        {
            if (ops.Peek() == "(")
                throw new Exception("Mismatched parentheses.");

            ApplyOperationSafely(values, ops.Pop());
        }

        if (values.Count != 1)
            throw new Exception("Invalid expression.");

        return values.Pop();
    }

    private void ApplyOperationSafely(Stack<double> values, string op)
    {
        if (values.Count < 2)
            throw new Exception("Invalid expression: not enough operands.");

        double b = values.Pop();
        double a = values.Pop();

        values.Push(_calculator.Calculate(a, op, b));
    }

    private bool IsOperator(char c) => "+-*/".Contains(c);

    private int Precedence(string op) => (op == "+" || op == "-") ? 1 : 2;
}

}

