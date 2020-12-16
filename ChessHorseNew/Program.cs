using System;
using System.Threading;

namespace OP_laba7_2_simetria_
{
    class Program
    {
        static void ChooseColor(out ConsoleColor lightGround, out ConsoleColor darkGround)
        {
            lightGround = ConsoleColor.Black;
            darkGround = ConsoleColor.Black;
            int doing = 0;
            Console.WriteLine("Для начала тебе нужно выбрать цвет шахматной доски.");
            do
            {
                Console.WriteLine("Вот весь доступный асортимент:");
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("G зелёный");
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("B синий");
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Y жёлтый");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("R красный");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("Ваш цвет: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "G":
                        lightGround = ConsoleColor.Green;
                        darkGround = ConsoleColor.Green;
                        doing = 0;
                        break;
                    case "B":
                        lightGround = ConsoleColor.Blue;
                        darkGround = ConsoleColor.DarkBlue;
                        doing = 0;
                        break;
                    case "Y":
                        lightGround = ConsoleColor.DarkYellow;
                        darkGround = ConsoleColor.DarkYellow;
                        doing = 0;
                        break;
                    case "R":
                        lightGround = ConsoleColor.Red;
                        darkGround = ConsoleColor.DarkRed;
                        doing = 0;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Такого у меня нет, выбирай из списка.");
                        doing = 1;
                        break;
                }
            } while (doing == 1);
            Console.Clear();
        }
        static void ConsoleOutput(string[,] ChessField, string[] LettersArray, string[] NumbersArray, ConsoleColor lightGround, ConsoleColor darkGround)
        {
            for (int i = 0; i < LettersArray.Length; i++)
            {
                Console.ResetColor();
                Console.Write(LettersArray[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < ChessField.GetLength(0); i++)
            {
                Console.ResetColor();
                Console.Write(NumbersArray[i]);
                for (int j = 0; j < ChessField.GetLength(1); j++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 1)
                        {
                            Console.BackgroundColor = lightGround;
                            Console.Write(ChessField[i, j] + " ");
                        }
                        else if (j % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write(ChessField[i, j] + " ");
                        }
                    }
                    else if (i % 2 == 1)
                    {
                        if (j % 2 == 1)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write(ChessField[i, j] + " ");
                        }
                        else if (j % 2 == 0)
                        {
                            Console.BackgroundColor = darkGround;
                            Console.Write(ChessField[i, j] + " ");
                        }
                    }

                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            int n = 8;// n - длинна и ширина доски
            string[,] ChessField = new string[n, n];
            string[] LettersArray = { " ", "a", " b", " c", " d", " e", " f", " g", " h", };
            string[] NumbersArray = { "8", "7", "6", "5", "4", "3", "2", "1", };
            for (int i = 0; i < ChessField.GetLength(0); i++)                       //первое заполнение шахматной доски
            {
                for (int j = 0; j < ChessField.GetLength(1); j++)
                {
                    if (i == 0 && j == 7)
                        ChessField[i, j] = "▼";
                    else
                        ChessField[i, j] = " ";
                }
            }
            ConsoleColor lightGround, darkGround;
            ChooseColor(out lightGround, out darkGround);
            Console.WriteLine("▼ - это ваш конь");
            Console.WriteLine("¤ - это вытоптанные поля");
            ConsoleOutput(ChessField, LettersArray, NumbersArray, lightGround, darkGround);
            int LoseOrContinue = 0, LoseOrContinue2 = 0, YouWin = 0;
            do
            {
                Console.ResetColor();
                Console.WriteLine("\nКуда ходим?");
                int EndNumCoordinate, EndLettCoordinate;
                Coordinate(ChessField, out EndNumCoordinate, out EndLettCoordinate);
                Moving(ChessField, out ChessField, EndNumCoordinate, EndLettCoordinate);
                Console.Clear();
                Console.WriteLine("▼ - это ваш конь");
                Console.WriteLine("¤ - это вытоптанные поля");
                ConsoleOutput(ChessField, LettersArray, NumbersArray, lightGround, darkGround);

                LosingCheck(ChessField, EndNumCoordinate, EndLettCoordinate, out LoseOrContinue);
                LosingCheck2(ChessField, out LoseOrContinue2);
                YouWin++;
                if (LoseOrContinue == 0 || LoseOrContinue2 == 0)
                {
                    Console.WriteLine("You lose!");
                    LoseOrContinue = 0;
                }
                if (LoseOrContinue != 0 && YouWin != 63)
                    Console.WriteLine($"Score: {YouWin}");
                if (YouWin == 63)
                    Console.WriteLine("You win, but how?!");
            } while (LoseOrContinue != 0 && YouWin != 63 );


        }
        static void Coordinate(string[,] ChessField, out int EndNumCoordinate, out int EndLettCoordinate)
        {
            int condition = 0; //для цыкл do while
            EndNumCoordinate = 0;
            do
            {
                int StNumCoordinate;
                do
                {
                    try
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Координата[1-8]:");                          //находем координату по цифре
                        StNumCoordinate = Convert.ToInt32(Console.ReadLine());
                        EndNumCoordinate = Math.Abs(StNumCoordinate - 8);
                        if (StNumCoordinate < 1 || StNumCoordinate > 8)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Координата заданы некорректно, попробуй ещё раз :)");
                        }
                    }
                    catch (Exception)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Координата заданы некорректно, попробуй ещё раз :)");
                        StNumCoordinate = 0;
                    }
                } while (StNumCoordinate < 1 || StNumCoordinate > 8);
                do
                {
                    Console.ResetColor();
                    Console.Write("Координата[a-h]:");                      //находем координату по букве
                    string StLettCoordinate = Console.ReadLine();
                    string[] LettArray = { "a", "b", "c", "d", "e", "f", "g", "h", };
                    EndLettCoordinate = Array.IndexOf(LettArray, StLettCoordinate);
                    if (EndLettCoordinate == -1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Координатa заданы некорректно, попробуй ещё раз :)");
                    }
                } while (EndLettCoordinate == -1);

                int yLim = 0, xLim = 0;                                                         // y-цифры  x-буквы
                for (int i = 0; i < ChessField.GetLength(0); i++)                       //первое заполнение шахматной доски
                {
                    for (int j = 0; j < ChessField.GetLength(1); j++)
                    {
                        if (ChessField[i, j] == "▼")
                        {
                            yLim = i;
                            xLim = j;
                            //Console.WriteLine($"До перемещения конь находиться на [{i};{j}]"); ;
                        }
                    }
                }
                if (ChessField[EndNumCoordinate, EndLettCoordinate] == "¤")
                {
                    {
                        condition = 2;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("Конь уже вытоптал это поле,сюда нельзя ступить :(");
                    }
                }
                else
                    condition = 0;
                if (condition != 2)
                {
                    condition = (EndNumCoordinate - 2 == yLim && (EndLettCoordinate - 1 == xLim || EndLettCoordinate + 1 == xLim)) ? 1 : 0;  // здесь идёт проверка
                    if (condition == 0)
                        condition = (EndNumCoordinate - 1 == yLim && (EndLettCoordinate - 2 == xLim || EndLettCoordinate + 2 == xLim)) ? 1 : 0;  // возможного хода
                    if (condition == 0)
                        condition = (EndNumCoordinate + 1 == yLim && (EndLettCoordinate - 2 == xLim || EndLettCoordinate + 2 == xLim)) ? 1 : 0;  // конь ходит только 
                    if (condition == 0)
                        condition = (EndNumCoordinate + 2 == yLim && (EndLettCoordinate - 1 == xLim || EndLettCoordinate + 1 == xLim)) ? 1 : 0;  // буквой Г

                    if (condition == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("Конь так не ходит :(");
                    }
                }
            } while (condition == 0 || condition == 2);
        }
        static void Moving(string[,] StChessField, out string[,] EndChessField, int EndNumCoordinate, int EndLettCoordinate)//перезаполнениe шахматной доски
        {
            EndChessField = new string[8, 8];
            for (int i = 0; i < StChessField.GetLength(0); i++)
            {
                for (int j = 0; j < StChessField.GetLength(1); j++)
                {
                    if (i == EndNumCoordinate && j == EndLettCoordinate)
                        EndChessField[i, j] = "▼";
                    else if (StChessField[i, j] == "▼")
                        EndChessField[i, j] = "¤";
                    else if (StChessField[i, j] == "¤")
                        EndChessField[i, j] = "¤";
                    else
                        EndChessField[i, j] = " ";
                }
            }
        }
        static void Checking(string[,] ChessField, int Number, int Letter, out int[] EightVersion)
        {
            EightVersion = new int[8];
            try
            {
                if (ChessField[Number - 2, Letter - 1] == "¤")
                    EightVersion[0] = 0;
                else
                    EightVersion[0] = 1;
            }
            catch (Exception)
            {
                EightVersion[0] = 0;
            }
            try
            {
                if (ChessField[Number - 2, Letter + 1] == "¤")
                    EightVersion[1] = 0;
                else
                    EightVersion[1] = 1;

            }
            catch (Exception)
            {
                EightVersion[1] = 0;
            }
            try
            {
                if (ChessField[Number - 1, Letter - 2] == "¤")
                    EightVersion[2] = 0;
                else
                    EightVersion[2] = 1;
            }
            catch (Exception)
            {
                EightVersion[2] = 0;
            }
            try
            {
                if (ChessField[Number - 1, Letter + 2] == "¤")
                    EightVersion[3] = 0;
                else
                    EightVersion[3] = 1;
            }
            catch (Exception)
            {
                EightVersion[3] = 0;
            }
            try
            {
                if (ChessField[Number + 1, Letter - 2] == "¤")
                    EightVersion[4] = 0;
                else
                    EightVersion[4] = 1;
            }
            catch (Exception)
            {
                EightVersion[4] = 0;
            }
            try
            {
                if (ChessField[Number + 1, Letter + 2] == "¤")
                    EightVersion[5] = 0;
                else
                    EightVersion[5] = 1;
            }
            catch (Exception)
            {
                EightVersion[5] = 0;
            }
            try
            {
                if (ChessField[Number + 2, Letter - 1] == "¤")
                    EightVersion[6] = 0;
                else
                    EightVersion[6] = 1;
            }
            catch (Exception)
            {
                EightVersion[6] = 0;
            }
            try
            {
                if (ChessField[Number + 2, Letter + 1] == "¤")
                    EightVersion[7] = 0;
                else
                    EightVersion[7] = 1;
            }
            catch (Exception)
            {
                EightVersion[7] = 0;
            }
        }
        static void LosingCheck(string[,] ChessField, int EndNumCoordinate, int EndLettCoordinate, out int LoseOrContinue)
        {
            int[] EightVersion;
            Checking(ChessField, EndNumCoordinate, EndLettCoordinate, out EightVersion);
            int sumOfEight = 0;
            foreach (var item in EightVersion)
            {
                sumOfEight += item;
            }
            Console.ResetColor();
            //Console.WriteLine($"У тебя есть {sumOfEight} ходов");
            LoseOrContinue = (sumOfEight != 0) ? 1 : 0;         // 0 это проиграшь
        }
        static void LosingCheck2(string[,] ChessField, out int LoseOrContinue2)
        {
            LoseOrContinue2 = 1;
            int[,] FullVersion = new int[8, 8];
            for (int i = 0; i < ChessField.GetLength(0); i++)
            {
                for (int j = 0; j < ChessField.GetLength(1); j++)
                {
                    if (ChessField[i, j] == "¤" || ChessField[i, j] == "▼")
                        FullVersion[i, j] = 1;
                    else
                    {
                        int[] EightVersion;
                        Checking(ChessField, i, j, out EightVersion);
                        int count = 0;
                        for (int k = 0; k < EightVersion.Length; k++)
                        {
                            count += EightVersion[k];
                        }
                        FullVersion[i, j] = count;
                    }
                }
            }
            for (int i = 0; i < FullVersion.GetLength(0); i++)
            {
                for (int j = 0; j < FullVersion.GetLength(1); j++)
                {
                    if (FullVersion[i, j] == 0)
                    {
                        //Console.WriteLine($"Потерян доступ к клетке i={i} ; j={j}");
                        LoseOrContinue2 = 0;
                    }
                }
            }
        }
    }
}
