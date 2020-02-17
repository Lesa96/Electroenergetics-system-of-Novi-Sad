using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ2
{
    public class BFSHelper
    {
        struct direction
        {
            public int x;
            public int y;
        };

        static direction[] directions = {
                                       new direction(){x = 1, y = 0},
                                       new direction(){x = 0, y = 1},
                                       new direction(){x = -1, y = 0},
                                       new direction(){x = 0, y = -1}
        };
        public static int[,] visitedmap = ResetVisitedMap();

        public static Position BFS(Position present, string[,] map, string lookingfor, bool resetMap)
        {
            Position[] poses = new Position[] { present };
            bool isFound = false;

            while (!isFound)
            {
                foreach (Position pos in poses)
                {
                    if (map[pos.Row, pos.Col].Equals(lookingfor))
                    {
                        if (resetMap)
                            visitedmap = ResetVisitedMap();
                        return pos;
                    }
                }
                poses = CellsNear(poses, map);
            }

            return present;
        }

        public static Position[] CellsNear(Position pos, string[,] matrix)
        {
            List<Position> result = new List<Position>();
            foreach (direction dir in directions)
            {
                int rowCalculated = pos.Row + dir.x;
                int colCalculated = pos.Col + dir.y;
                if (rowCalculated >= 0 &&
                        colCalculated >= 0 &&
                        rowCalculated < matrix.GetLength(0) &&
                        colCalculated < matrix.GetLength(1) &&
                        !Visited(rowCalculated, colCalculated))
                {
                    visitedmap[rowCalculated, colCalculated] = 1;
                    Position posPath = new Position();
                    posPath.Col = colCalculated;
                    posPath.Row = rowCalculated;
                    posPath.Parent = pos;
                    result.Add(posPath);
                }
            }
            return result.ToArray();
        }

        public static Position[] CellsNear(Position[] poses, string[,] matrix)
        {
            List<Position> result = new List<Position>();
            foreach (Position pos in poses)
            {
                result.AddRange(CellsNear(pos, matrix));
            }
            return result.ToArray();
        }

        public static bool Visited(int row, int col)
        {
            return visitedmap[row, col] == 1;
        }

        public static int[,] ResetVisitedMap()
        {
            int[,] returnArray = new int[151, 151];

            for (int i = 0; i < 151; i++)
            {
                for (int k = 0; k < 151; k++)
                {
                    returnArray[i, k] = 0;
                }
            }

            return returnArray;
        }
    }
}
