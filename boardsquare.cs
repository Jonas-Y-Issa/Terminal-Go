using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public class boardsquare
    {
        string square = ".";
        int[] Coord = new int[2];

        public void addCoord(int x, int y)
        {
            Coord[0] = x;
            Coord[1] = y;
        }
        public int getX()
        {
            return Coord[0];
        }
        public int getY()
        {
            return Coord[1];
        }
        public void alterSquare(string replace)
        {
            square = replace;
        }
        public string displaySquare()
        {
            return " " + square + " ";
        }
        
    }

 

}
