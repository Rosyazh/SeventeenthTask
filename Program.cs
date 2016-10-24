using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using System.Diagnostics;

/*
Develop a console application to compute GCD (Greatest Common Divisor) from a set of more than 2 numbers using different algorithms.
Test each algorithm performance by using Stopwatch class.
*/

namespace SeventeenthTask
{
    class Program
    {
        delegate long GCDAlgorithm(long x, long y);

        private static long GCDLib(List<long> _numbers)
        {
            return Euclid.GreatestCommonDivisor(_numbers);
        }

        private static long GCD(long[] _numbers, GCDAlgorithm _alg)
        {
            int n = _numbers.Length;
            long gcd = _numbers[0];

            if (n == 0) return 0;
            for (int i = 0; i < n - 1; i++)
                gcd = _alg.Invoke(Math.Abs(gcd), Math.Abs(_numbers[i + 1]));
            return gcd;
        }

        private static long GCDRecursion(long a, long b)
        {
            if (b == 0)
                return a;
            return GCDRecursion(b, a % b);
        }

        private static long GCDDivision(long a, long b)
        {
            long t;
            while (b != 0)
            {
                t = b;
                b = a % b;
                a = t;
            }
            return a;
        }
        
        private static long GCDSubtracion(long a, long b)
        {
            while (a!=b)
                if (a > b)
                    a -= b;
                else
                    b -= a;
            return a;
        }
        
        private static long GCDBinaryRecursion(long a, long b)
        {
            if (a == b)
                return a;
            if (a == 0)
                return b;
            if (b == 0)
                return a;
            if ((~a & 1) != 0)
            {
                if ((b & 1) != 0)
                    return GCDBinaryRecursion(a >> 1, b);
                else
                    return GCDBinaryRecursion(a >> 1, b >> 1) << 1;
            }
            if ((~b & 1) != 0)
                return GCDBinaryRecursion(a, b >> 1);
            if (a > b)
                return GCDBinaryRecursion((a - b) >> 1, b);
            return GCDBinaryRecursion((b - a) >> 1, a);
        }

        private static long GCDBinaryIterative(long a, long b)
        {
            int shift;
            if (a == 0)
                return b;
            if (b == 0)
                return a;
            for (shift = 0; ((a | b) & 1) == 0; ++shift)
            {
                a >>= 1;
                b >>= 1;
            }
            while ((a & 1) == 0)
                a >>= 1;
            do
            {
                while ((b & 1) == 0)
                    b >>= 1;
                if (a > b)
                {
                    long t = b;
                    b = a;
                    a = t;
                }
                b = b - a;
            } while (b != 0);
            return a << shift;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\nEnter the number of values in the set.");
            int n = Convert.ToInt32(Console.ReadLine());
            long[] numbers = new long[n];

            Console.WriteLine("\nEnter values:");
            for (int i = 0; i < n; i++)
            {
                numbers[i] = Convert.ToInt64(Console.ReadLine());
            }           

            Stopwatch sw = new Stopwatch();

            Console.WriteLine("\nGCD by MathNet.");
            sw.Start();
            GCDLib(numbers.ToList());
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Reset();

            Console.WriteLine("\nThe recursive version of the GCD.");
            sw.Start();
            GCD(numbers, GCDRecursion);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Reset();

            Console.WriteLine("\nThe division-based version of the GCD.");
            sw.Start();
            GCD(numbers, GCDDivision);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Reset();
            
            Console.WriteLine("\nThe subtraction-based version of the GCD.");
            sw.Start();
            GCD(numbers, GCDSubtracion);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Reset();

            Console.WriteLine("\nThe recursive binary algorithm.");
            sw.Start();
            GCD(numbers, GCDBinaryRecursion);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Reset();

            Console.WriteLine("\nThe iterative binary algorithm.");
            sw.Start();
            GCD(numbers, GCDBinaryIterative);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Reset();
        }
    }
}
