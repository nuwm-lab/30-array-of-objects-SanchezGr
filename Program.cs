using System;

namespace Lab3
{
    class TestClass
    {
        private byte[] bigArray = new byte[50 * 1024 * 1024];
        public static bool Verbose = false;

        public TestClass()
        {
            if (Verbose) Console.WriteLine("Створено об'єкт TestClass");
        }

        ~TestClass()
        {
            if (Verbose) Console.WriteLine("Спрацював деструктор TestClass");
        }
    }

    class GeometricProgression
    {
        public double A0;
        public double Q;
        public int N;

        public GeometricProgression(double a0, double q, int n)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException(nameof(n), "Кількість членів повинна бути додатною.");

            A0 = a0;
            Q = q;
            N = n;
        }

        ~GeometricProgression()
        {
            if (TestClass.Verbose) Console.WriteLine("Знищується об'єкт геометричної прогресії");
        }

        public double LastTerm()
        {
            return A0 * Math.Pow(Q, N - 1);
        }

        public override string ToString()
        {
            return $"a0 = {A0}, q = {Q}, n = {N}, an = {LastTerm()}";
        }

        public static int GetIndexOfMaxLast(GeometricProgression[] arr)
        {
            if (arr == null || arr.Length == 0)
                throw new ArgumentException("Масив прогресій не може бути порожнім.");

            int maxIndex = 0;
            double maxLast = arr[0].LastTerm();

            for (int i = 1; i < arr.Length; i++)
            {
                double currentLast = arr[i].LastTerm();
                if (currentLast > maxLast)
                {
                    maxLast = currentLast;
                    maxIndex = i;
                }
            }

            return maxIndex;
        }
    }

    class Program
    {
        static void Main()
        {
            int m = ReadInt("Кількість прогресій: ", false);

            GeometricProgression[] gp = new GeometricProgression[m];

            for (int i = 0; i < m; i++)
            {
                Console.WriteLine($"\nПрогресія #{i + 1}");

                double a0 = ReadDouble("a0 = ");
                double q = ReadDouble("q = ");
                int n = ReadInt("n = ", false);

                gp[i] = new GeometricProgression(a0, q, n);
            }

            Console.WriteLine("\nСписок прогресій та їхніх останніх членів:");
            for (int i = 0; i < gp.Length; i++)
            {
                Console.WriteLine($"Прогресія #{i + 1}: {gp[i]}");
            }

            int maxIndex = GeometricProgression.GetIndexOfMaxLast(gp);
            Console.WriteLine($"\nНайбільший останній член у прогресії #{maxIndex + 1}: {gp[maxIndex].LastTerm()}");

            for (int i = 0; i < 10; i++)
            {
                var obj = new TestClass();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("\nКінець роботи програми.");
        }

        static int ReadInt(string prompt, bool allowZero)
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
                    : "Помилка. Введіть додатне ціле число.");
            }
        }

        static double ReadDouble(string prompt)
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

}


