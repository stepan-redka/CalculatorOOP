namespace ClassLibrary
{
    public interface IOperation
    {
        public double DoOperation(double a, double b);
    }

    public class Addition : IOperation
    {
        public double DoOperation(double a, double b) =>  a + b;
    }

    public class Multiplication :  IOperation
    {
        public double DoOperation(double a, double b) =>  a * b;
    }

    public class Division :  IOperation
    {
        public double DoOperation(double a, double b) =>  a / b;
    }

    public class Subtraction :  IOperation
    {
        public double DoOperation(double a, double b) =>  a - b;
    }
}

