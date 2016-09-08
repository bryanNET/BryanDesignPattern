using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /// <summary>
    /// //简单工厂
    /// </summary>
    public class SimpleFactory
    {
        public static Operation GetOperation(op op, double a, double b)
        {
            switch (op)
            {
                case op.add: return new Add(a, b);
                case op.sub: return new Sub(a, b);
                case op.mul: return new Mul(a, b);
                case op.div: return new Div(a, b);
                default: return new undef(a, b);
            }
        }
    }

    public enum op
    {
        add = '+',
        sub = '-',
        mul = '*',
        div = '/'
    }

    public abstract class Operation
    {
        public double a, b;
        public Operation(double a, double b)
        {
            this.a = a;
            this.b = b;
        }
        public abstract double GetResult();
    }

    public class Add : Operation
    {
        public Add(double a, double b) : base(a, b) { }

        public override double GetResult()
        {
            return a + b;
        }
    }

    public class Sub : Operation
    {
        public Sub(double a, double b) : base(a, b) { }

        public override double GetResult()
        {
            return a - b;
        }
    }

    public class Mul : Operation
    {
        public Mul(double a, double b) : base(a, b) { }

        public override double GetResult()
        {
            return a * b;
        }
    }

    public class Div : Operation
    {
        public Div(double a, double b) : base(a, b) { }

        public override double GetResult()
        {
            try
            {
                return a / b;
            }
            catch (DivideByZeroException e)
            {
                throw e;
            }
        }
    }

    public class undef : Operation
    {
        public undef(double a, double b) : base(a, b) { }

        public override double GetResult()
        {
            throw new NotImplementedException();
        }
        
    }
}
