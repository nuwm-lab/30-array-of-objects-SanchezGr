using System;

namespace Lab3
{
    public class GeometricProgression
    {
        private double a0, q;
        private int n;

        public GeometricProgression(double a0, double q, int n)
        {
            this.a0 = a0;
            this.q = q;
            this.n = n;
        }

        public double LastTerm()
        {
            return a0 * Math.Pow(q, n - 1);
        }

        ~GeometricProgression()
        {
            Console.WriteLine("Об'єкт знищено");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Write("Кількість прогресій: ");
            int m = int.Parse(Console.ReadLine());
            GeometricProgression[] gp = new GeometricProgression[m];

            for (int i = 0; i < m; i++)
            {
                Console.WriteLine($"\nПрогресія #{i + 1}");
                Console.Write("a0 = ");
                double a0 = double.Parse(Console.ReadLine());
                Console.Write("q = ");
                double q = double.Parse(Console.ReadLine());
                Console.Write("n = ");
                int n = int.Parse(Console.ReadLine());
                gp[i] = new GeometricProgression(a0, q, n);
            }

            int maxIndex = 0;
            double maxValue = gp[0].LastTerm();

            for (int i = 1; i < m; i++)
            {
                double value = gp[i].LastTerm();
                if (value > maxValue)
                {
                    maxValue = value;
                    maxIndex = i;
                }
            }

            Console.WriteLine($"\nНайбільший останній член у прогресії #{maxIndex + 1}: {maxValue}");

            gp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}

