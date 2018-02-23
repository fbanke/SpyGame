using System;
namespace SpyLib
{
    public class MyClass
    {
        public MyClass()
        {
            Console.WriteLine("Hello Class!");
        }
        public int Divide(int number1, int number2)
        {
            int result;
            try
            {
                result = number1 / number2;
            }
            catch (DivideByZeroException)
            {
                result = -1;
            }
            return result;
        }
    }
}
