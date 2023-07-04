using System;

class Program
{
    static void Main()
    {
        Sudoku sudoku = new Sudoku();
        sudoku.Initialize();
        sudoku.Solve();
        sudoku.DisplaySolution();

        Console.ReadLine();
    }
}

class Sudoku
{
    private int[,] board;
    private const int Size = 9;

    public void Initialize()
    {
        board = new int[Size, Size];

        Console.WriteLine("enter the sodoku number: ");
        for (int i = 0; i < Size; i++)
        {
            Console.WriteLine($"radif {i + 1}:");
            string rowInput = Console.ReadLine();

            for (int j = 0; j < Size; j++)
            {
                if (rowInput[j] == ' ')
                    board[i, j] = 0;
                else
                    board[i, j] = int.Parse(rowInput[j].ToString());
            }
        }
    }

    public bool Solve()
    {
        int row, col;

        if (!FindUnassignedLocation(out row, out col))
            return true; // همه خانه‌ها پر شده و مسئله حل شده است

        for (int num = 1; num <= 9; num++)
        {
            if (IsSafe(row, col, num))
            {
                board[row, col] = num;

                if (Solve())
                    return true;

                board[row, col] = 0; // اگر حل نشود، مقدار را صفر می‌کنیم و مجدداً تلاش می‌کنیم
            }
        }

        return false; // هیچ عددی قابل قرار دادن نیست و مسئله حل نشده است
    }

    private bool FindUnassignedLocation(out int row, out int col)
    {
        for (row = 0; row < Size; row++)
        {
            for (col = 0; col < Size; col++)
            {
                if (board[row, col] == 0)
                    return true; // یک خانه خالی پیدا شد
            }
        }

        row = -1;
        col = -1;
        return false; // هیچ خانه خالی پیدا نشد
    }

    private bool IsSafe(int row, int col, int num)
    {
        // بررسی سطر
        for (int i = 0; i < Size; i++)
        {
            if (board[row, i] == num)
                return false; // عدد تکراری در سطر
        }

        // بررسی ستون
        for (int i = 0; i < Size; i++)
        {
            if (board[i, col] == num)
                return false; // عدد تکراری در ستون
        }

        // بررسی بلوک 3x3
        int blockRowStart = row - row % 3;
        int blockColStart = col - col % 3;

        for (int i = blockRowStart; i < blockRowStart + 3; i++)
        {
            for (int j = blockColStart; j < blockColStart + 3; j++)
            {
                if (board[i, j] == num)
                    return false; // عدد تکراری در بلوک 3x3
            }
        }

        return true; // عدد مناسب است
    }

    public void DisplaySolution()
    {
        Console.WriteLine("javab :");
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Console.Write(board[i, j] + " ");
            }

            Console.WriteLine();
        }
    }
}