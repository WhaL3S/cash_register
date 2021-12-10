class Program
    {
        static void Read(string fd, ref Matrix tradeCenter)
        {
            int nn, mm, v;
            string line;
            using(StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts;
                nn = int.Parse(line);
                line = reader.ReadLine();
                mm = int.Parse(line);
                tradeCenter.n = nn;
                tradeCenter.m = mm;
                for(int i = 0; i < nn; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    for(int j = 0; j < mm; j++)
                    {
                        v = int.Parse(parts[j]);
                        tradeCenter.Set(i, j, v);
                    }
                }
            }
        }

        static void Print(string fv, Matrix tradeCenter, string header)
        {
            using(StreamWriter writer = new StreamWriter(fv, true))
            {
                writer.WriteLine(header);
                writer.WriteLine();
                writer.WriteLine("Number of cash registers: {0}", tradeCenter.n);
                writer.WriteLine("Number of days: {0}", tradeCenter.m);
                writer.WriteLine();
                writer.WriteLine("Number of served customers");
                for(int i = 0; i < tradeCenter.n; i++)
                {
                    for (int j = 0; j < tradeCenter.m; j++)
                        writer.Write("{0,4:d}", tradeCenter.Get(i, j));
                    writer.WriteLine();
                }
            }
        }

        static int TotalNumberOfCustomersServed(Matrix A)
        {
            int sum = 0;
            for (int i = 0; i < A.n; i++)
                for (int j = 0; j < A.m; j++)
                    sum = sum + A.Get(i, j);
            return sum;
        }

        static double AverageNumberOfCustomersServedByOneCasRegister(Matrix A, int n)
        {
            double sum = 0;
            for(int i = 0; i < A.m; i++)
            {
                sum = sum + A.Get(n, i);
            }
            double average = sum / A.m;
            return average;
        }

        static int DidNotWorkDays(Matrix A, int n)
        {
            int counter = 0;
            for(int i = 0; i < A.m; i++)
            {
                if(A.Get(n, i) == 0)
                {
                    counter++;
                }
            }
            return counter;
        }

        static void DayMinimumNumberOfCustomers(string fv, Matrix A)
        {
            int[] sumS = new int[A.m];
            for (int j = 0; j < A.m; j++)
            {
                int sum = 0;
                for (int i = 0; i < A.n; i++)
                    sum = sum + A.Get(i, j);
                sumS[j] = sum;
            }
            int minimum = sumS[0];
            int day = 1;
            for(int i = 1; i < A.m; i++)
            {
                if(minimum > sumS[i])
                {
                    minimum = sumS[i];
                    day = i + 1;
                }
            }
            using(StreamWriter writer = new StreamWriter (fv,true))
            {
                writer.WriteLine("The day with minimum number of customers served is {0}", day);
                writer.WriteLine("In that day, {0} number of customers were served", minimum);
            }
        }

        const string Cfd = "Data.txt";
        const string Cfr = "Results.txt";

        static void Main(string[] args)
        {
            if (File.Exists(Cfr)) File.Delete(Cfr);
            Matrix tradeCenter = new Matrix();
            Read(Cfd, ref tradeCenter);
            Print(Cfr, tradeCenter, "Initial data");
            using(StreamWriter writer = new StreamWriter(Cfr, true))
            {
                writer.WriteLine();
                writer.WriteLine("Results");
                writer.WriteLine();
                writer.WriteLine("Total number of customers served is: {0}.",
                                   TotalNumberOfCustomersServed(tradeCenter));
                writer.WriteLine();
                for (int i = 0; i < tradeCenter.n; i++)
                {
                    writer.WriteLine("Average customer served by cash register {0} is {1, 0:f1}", i + 1, AverageNumberOfCustomersServedByOneCasRegister(tradeCenter, i));
                    writer.WriteLine("Cash register {0} did not work for {1} days", i + 1, DidNotWorkDays(tradeCenter, i));
                    writer.WriteLine();
                }
            }
            DayMinimumNumberOfCustomers(Cfr, tradeCenter);
        }
    }
