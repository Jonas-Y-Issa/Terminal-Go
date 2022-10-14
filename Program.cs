using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    class Program
    {
        public static List<boardsquare> sq2 = new List<boardsquare>();
        public static int Width = 0;
        public static int inpX = Width + 1;
        public static int inpY = Width + 1;
        public static string player = "o";
        public static int empty;
        public static string enemy = "";

        static void Main(string[] args)
        {
            do
            {
                Width = int.Parse(Console.ReadLine());
            } while (Width < 5 || Width > 19);

            setupBoard();

            move();

            Console.ReadLine();
        }

        public static void setupBoard()
        {

            int x = 1;
            int y = 1;
            int co = 1;
            for (int i = 0; i < (Width * Width); i++)
            {
                sq2.Add(new boardsquare());
                sq2[i].addCoord(x, y);

                Console.Write(sq2[i].displaySquare());

                x++;

                if (x == Width + 1)
                {
                    Console.Write(co);
                    if (co == Width)
                    {
                        Console.Write("Y");
                    }
                    Console.WriteLine();
                    x = 1;
                    co++;
                    y++;
                }

            }
            for (int i = 1; i <= Width; i++)
            {
                if (i < 10 && i != Width)
                {
                    Console.Write(" " + i + " ");
                }
                else
                {
                    Console.Write(" " + i);
                    if (i == Width)
                    {
                        Console.Write("X");
                    }
                }
            }



        }
        public static void updateBoard()
        {

            int t = 1;

            foreach (boardsquare s in sq2)
            {
                if (s.getX() == Width)
                {

                    Console.Write(s.displaySquare());

                    if (t == Width)
                    { Console.WriteLine(t + "Y"); }
                    else
                    { Console.WriteLine(t); }
                    t++;

                }
                else
                {
                    Console.Write(s.displaySquare());
                }
            }
            for (int i = 1; i <= Width; i++)
            {
                if (i < 10 && i != Width)
                {
                    Console.Write(" " + i + " ");
                }
                else
                {
                    Console.Write(" " + i);
                    if (i == Width)
                    {
                        Console.Write("X");
                    }
                }
            }

        }
        public static void move()
        {
            while (inpX != 0 && inpY != 0)
            {

                Console.WriteLine();
                do
                {
                    Console.Write("X:");
                    inpX = int.Parse(Console.ReadLine());


                    Console.Write("Y:");
                    inpY = int.Parse(Console.ReadLine());
                    if (inpX > Width || inpY > Width)
                    {

                        Console.WriteLine("Invalid input");

                    }

                    empty = sq2.FindIndex(a => (a.getX() == inpX && a.getY() == inpY));

                } while ((inpX > Width || inpY > Width && inpY <= Width) || (sq2[empty].displaySquare() != " . "));

                if (inpX != 0 && inpY != 0 && inpX <= Width && inpY <= Width)
                {
                    sq2.Find(j => (j.getX() == inpX) && (j.getY() == inpY)).alterSquare(player);
                }


                checkKill();
                changeTurn();
                updateBoard();

            }
        }

        public static void changeTurn()
        {
            if (player == "o")
            {
                player = "#";
            }
            else
            {
                player = "o";
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(player + player + player + player + player + player + player);

        }

        public static void checkKill()
        {
            List<boardsquare> checkDeath = new List<boardsquare>();
            List<boardsquare> checkd = new List<boardsquare>();
            List<int> ttt = new List<int>();
            int safe = 0;


            //check for kill based on last placed stone
            if (player == "o")
            {
                enemy = "#";
            }

            else
            {
                enemy = "o";
            }

            //checkDeath.AddRange(sq2.FindAll(j => ((j.getX() >= inpX - 1) && (j.getX() <= inpX + 1)) && ((j.getY() >= inpY - 1) && (j.getY() <= inpY + 1))));
            //Check NESW and store them
            checkDeath.AddRange(sq2.FindAll(j => (
                   (j.getX() == inpX - 1 && j.getY() == inpY)
                || (j.getX() == inpX + 1 && j.getY() == inpY)
                || (j.getY() == inpY - 1 && j.getX() == inpX)
                || (j.getY() == inpY + 1 && j.getX() == inpX))));



            foreach (boardsquare d in checkDeath)
            {

                if (d.displaySquare() == (" " + enemy + " "))
                {

                    int indOf = sq2.FindIndex(a => a == d);

                    //check surroundings


                    do
                    {
                        foreach (boardsquare s in sq2)
                        {
                            if (((s.getX() == (sq2[indOf].getX() - 1) && s.getY() == sq2[indOf].getY()))
                                && ((s.displaySquare() == " . ") || (s.displaySquare() == " " + enemy + " ")))
                            {
                                //if " . " is ever returned , break loop and do not kill
                                if (s.displaySquare() == " . ")
                                {
                                    safe = 1;
                                    Console.WriteLine("Safe");
                                }
                                //else list all enemies connected to the enemy
                                else if (s.displaySquare() == " " + enemy + " " && (!checkd.Contains(sq2[indOf]) || !checkd.Contains(s)))
                                {
                                    if (!checkd.Contains(sq2[indOf]))
                                    {
                                        checkd.Add(sq2[indOf]);
                                    }

                                    if (!checkd.Contains(s))
                                    {
                                        ttt.Add(sq2.FindIndex(a => a == s));
                                    }

                                }

                            }
                            if ((s.getY() == (sq2[indOf].getY() - 1) && s.getX() == sq2[indOf].getX())
                                 && ((s.displaySquare() == " . ") || (s.displaySquare() == " " + enemy + " ")))
                            {
                                //if " . " is ever returned , break loop and do not kill
                                if (s.displaySquare() == " . ")
                                {
                                    safe = 1;
                                    Console.WriteLine("Safe");
                                }
                                //else list all enemies connected to the enemy
                                else if (s.displaySquare() == " " + enemy + " " && (!checkd.Contains(sq2[indOf]) || !checkd.Contains(s)))
                                {
                                    if (!checkd.Contains(sq2[indOf]))
                                    {
                                        checkd.Add(sq2[indOf]);
                                    }
                                    if (!checkd.Contains(s))
                                    {
                                        ttt.Add(sq2.FindIndex(a => a == s));

                                    }

                                }

                            }
                            if ((s.getX() == (sq2[indOf].getX() + 1) && s.getY() == sq2[indOf].getY())
                                 && ((s.displaySquare() == " . ") || (s.displaySquare() == " " + enemy + " ")))
                            {
                                //if " . " is ever returned , break loop and do not kill
                                if (s.displaySquare() == " . ")
                                {
                                    safe = 1;
                                    Console.WriteLine("Safe");
                                }
                                //else list all enemies connected to the enemy
                                else if (s.displaySquare() == " " + enemy + " " && (!checkd.Contains(sq2[indOf]) || !checkd.Contains(s)))
                                {
                                    if (!checkd.Contains(sq2[indOf]))
                                    {
                                        checkd.Add(sq2[indOf]);
                                    }
                                    if (!checkd.Contains(s))
                                    {
                                        ttt.Add(sq2.FindIndex(a => a == s));

                                    }

                                }

                            }
                            if ((s.getY() == (sq2[indOf].getY() + 1) && s.getX() == sq2[indOf].getX())
                                 && ((s.displaySquare() == " . ") || (s.displaySquare() == " " + enemy + " ")))
                            {
                                //if " . " is ever returned , break loop and do not kill
                                if (s.displaySquare() == " . ")
                                {
                                    safe = 1;
                                    Console.WriteLine("Safe");
                                }
                                //else list all enemies connected to the enemy
                                else if (s.displaySquare() == " " + enemy + " " && (!checkd.Contains(sq2[indOf]) || !checkd.Contains(s)))
                                {
                                    if (!checkd.Contains(sq2[indOf]))
                                    {
                                        checkd.Add(sq2[indOf]);
                                    }
                                    if (!checkd.Contains(s))
                                    {
                                        ttt.Add(sq2.FindIndex(a => a == s));
                                    }
                                }
                            }
                            if (ttt.Contains(indOf))
                            {
                                ttt.Remove(indOf);
                            }
                            if (!checkd.Contains(sq2[indOf]))
                            {
                                checkd.Add(sq2[indOf]);
                            }
                        }
                        if (ttt.Count() == 0)
                        {
                            break;
                        }
                        indOf = ttt[0];
                    }
                    while (ttt.Count > 0);
                    if (safe == 1)
                    {
                        checkd.Clear();
                    }
                    if (safe == 0)
                    {
                        foreach (boardsquare chkd in checkd)
                        {

                            sq2[sq2.IndexOf(chkd)].alterSquare(".");
                            Console.WriteLine("Kill");
                        }
                    }
                }
                safe = 0;
            }
        }
    }
}