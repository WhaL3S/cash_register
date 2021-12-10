class Matrix
    {
        const int CMaxRows = 10;
        const int CmaxColumns = 100;
        private int[,] A;
        public int n { get; set; }
        public int m { get; set; }

        public Matrix()
        {
            n = 0;
            m = 0;
            A = new int[CMaxRows, CMaxRows];
        }
        public void Set(int i, int j, int v)
        {
            A[i, j] = v;
        }
        public int Get(int i, int j)
        {
            return A[i, j];
        }
    }
