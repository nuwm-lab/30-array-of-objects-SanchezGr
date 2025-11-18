using System;

namespace Lab3
{
    public class GeometricProgression
    {
        private readonly double _a0;
        private readonly double _q;
        private readonly int _n;

        public double A0 => _a0;
        public double Q  => _q;
        public int N     => _n;

        public GeometricProgression(double a0, double q, int n)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException(nameof(n), "n must be positive.");

            _a0 = a0;
            _q  = q;
            _n  = n;
        }

        public double LastTerm()
        {
            return _a0 * Math.Pow(_q, _n - 1);
        }

        public bool IsLastTermLargest()
        {
            if (_n <= 1) return true;

            double current = _a0;
            double max = _a0;

            for (int i = 1; i < _n; i++)
            {
                current *= _q;
                if (current > max)
                    max = current;
            }

            const double eps = 1e-9;
            return Math.Abs(current - max) <= eps;
        }
    }

    class Program
    {
        static void Main()
        {
            int m = ReadInt("Кількість прогресій: ", allowZero: false);

            if (m <= 0)
            {
                Console.WriteLine("Немає прогресій для обробки.");
                return;
            }

            var gp = new GeometricProgression[m];

            for (int i = 0; i < m; i++)
            {
                Console.WriteLine($"\nПрогресія #{i + 1}");

                double a0 = ReadDouble("a0 = ");
                double q  = ReadDouble("q = ");
                int n     = ReadInt("n = ", allowZero: false);

                gp[i] = new GeometricProgression(a0, q, n);
            }

            int maxIndex = -1;
            double maxValue = 0.0;

            for (int i = 0; i < m; i++)
            {
                double value = gp[i].LastTerm();

                if (double.IsNaN(value) || double.IsInfinity(value))
                {
                    Console.WriteLine($"Прогресія #{i + 1}: останній член некоректний (NaN/Infinity), пропускаємо.");
                    continue;
                }

                if (maxIndex == -1 || value > maxValue)
                {
                    maxValue = value;
                    maxIndex = i;
                }
            }

            if (maxIndex == -1)
            {
                Console.WriteLine("\nНемає жодної прогресії з коректним останнім членом.");
                return;
            }

            Console.WriteLine($"\nНайбільший останній член у прогресії #{maxIndex + 1}: {maxValue}");

            for (int i = 0; i < m; i++)
            {
                bool lastIsMax = gp[i].IsLastTermLargest();
                Console.WriteLine($"Прогресія #{i + 1}: останній член {(lastIsMax ? "є" : "не є")} найбільшим у цій прогресії.");
            }
        }

        private static int ReadInt(string prompt, bool allowZero)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value > 0 || (allowZero && value == 0))
                        return value;
                }

                Console.WriteLine(allowZero
                    ? "Помилка. Введіть число 0 або більше."
                    : "Помилка. Введіть ціле число.");
            }
        }

        private static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;

                Console.WriteLine("Помилка. Введіть число.");
            }
        }
    }
}

