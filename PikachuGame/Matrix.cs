using System;
using System.Collections.Generic;
using System.Drawing;

namespace PikachuGame
{
    public class Matrix
    {
        public static List<int> numList = new List<int>();
        static List<TwoPoint> lineList = new List<TwoPoint>();
        static List<Point> pointList = new List<Point>();
        static Random random = new Random();
        public static int[,] arr = new int[10, 17];

        /// <summary>
        /// i,x la dong
        /// j,y la cot
        /// </summary>
        public Matrix()
        {
            
        }

        public static void InitMatrix(int level)
        {
            //ngoai cung ma tran -1
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (i == 0 || j == 0 || i == 9 || j == 16)
                        arr[i, j] = -1;
                }
            }
            ResetNumList(level);
            RandomMatrix();
        }
        public static void ResetNumList(int level)
        {
            if (level <= 3)
            {
                int img = 0;
                int index = 0;
                for (int i = 1; i < 9; i++)
                {
                    for (int j = 1; j < 16; j++)
                        if (index < 7)
                        {
                            arr[i, j] = img;
                            index++;
                        }
                        else
                        {
                            arr[i, j] = img;
                            index = 0;
                            img++;
                        }

                }
            }
            else if (level <= 6)
            {
                int img = 0;
                int index = 0;
                for (int i = 1; i < 9; i++)
                {
                    for (int j = 1; j < 16; j++)
                        if (index < 5)
                        {
                            arr[i, j] = img;
                            index++;
                        }
                    else
                        {
                            arr[i, j] = img;
                            index=0;
                            img++;
                        }
                        
                }
            }
            else
            {
                int img = 0;
                int index = 0;
                for (int i = 1; i < 9; i++)
                {
                    for (int j = 1; j < 16; j++)
                        if (index < 3)
                        {
                            arr[i, j] = img;
                            index++;
                        }
                        else
                        {
                            arr[i, j] = img;
                            index = 0;
                            img++;
                        }

                }
            }
            
        }
        public static void RandomMatrix()
        {
            numList.Clear();
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    numList.Add(arr[i, j]);
                }

            }

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    int next = random.Next(numList.Count);
                    arr[i, j] = numList[next];
                    numList.RemoveAt(next);
                }

            }

        }
        public static TwoPoint CheckAvailablePath()
        {
            pointList.Clear();
            TwoPoint result = new TwoPoint(new Point(0, 0), new Point(0, 0));
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    if (arr[i, j] != -1)
                        pointList.Add(new Point(i, j));
                }

            }

            for (int i = 0; i < pointList.Count - 1; i++)
            {
                for (int j = i + 1; j < pointList.Count; j++)
                {
                    if (arr[pointList[i].X, pointList[i].Y] == arr[pointList[j].X, pointList[j].Y])
                    {
                        findpath(pointList[i], pointList[j]);
                        if (lineList.Count > 0)
                        {
                            result.p1 = pointList[i];
                            result.p2 = pointList[j];
                            return result;
                        }                      
                                
                    }
                   
                }
            }
            return result;
        }
        public static bool CheckVictory()
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    if (arr[i, j] != -1)
                        return false;
                }

            }
            return true;
        }
        public static int ChangeArrayLevel2(int row, int col)
        {
            
            for (int i = row; i < 9; i++)
            {
               
                arr[i, col] = arr[i + 1, col];
                if (arr[i+1, col] == -1)
                {
                    arr[i, col] = -1;
                    return i;
                }
                   

            }
            return row;
        }
        public static int ChangeArrayLevel3(int row, int col)
        {

            for (int i = col; i < 16; i++)
            {

                arr[row, i] = arr[row, i+1];
                if (arr[row, i + 1] == -1)
                {
                    arr[row, i]=-1;
                    return i;
                }


            }
            return col;
        }
        public static void RefreshMatrix()
        {
            numList.Clear();
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    if (arr[i, j] != -1)
                        numList.Add(arr[i, j]);
                }
            }


            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    if (arr[i, j] != -1)
                    {
                        int next = random.Next(numList.Count);
                        arr[i, j] = numList[next];
                        numList.RemoveAt(next);
                    }
                }
            }

        }
       

      

        public static bool checkLineCol(int r1, int r2, int c)
        {
            int min, max;
            if (r1 < r2)
            {
                min = r1;
                max = r2;
            }
            else
            {
                min = r2;
                max = r1;
            }
            for (int i = min; i <= max; i++)
            {
                if (arr[i, c] != -1)
                    return false;
            }
            return true;
        }

        public static bool checkLineRow(int c1, int c2, int r)
        {
            int min, max;
            if (c1 < c2)
            {
                min = c1;
                max = c2;
            }
            else
            {
                min = c2;
                max = c1;
            }
            for (int i = min; i <= max; i++)
            {
                if (arr[r, i] != -1)
                    return false;
            }
            return true;
        }


        /// <summary>
       //tra ve cot
        public static int check3LineX(Point p1, Point p2)
        {
            Point min, max;
            if (p1.Y < p2.Y)
            {
                min = p1;
                max = p2;
            }
            else
            {
                min = p2;
                max = p1;
            }
            //Z
            for (int i = min.Y + 1; i < max.Y; i++)
            {
                if (checkLineRow(min.Y + 1, i, min.X) && checkLineCol(min.X, max.X, i) && checkLineRow(i, max.Y - 1, max.X))
                {
                    return i;
                }
            }

            //U phai

            for (int i = max.Y + 1; i < 17; i++)
            {
                if (checkLineRow(min.Y + 1, i, min.X) && checkLineCol(min.X, max.X, i) && checkLineRow(max.Y + 1, i, max.X))
                {
                    return i;
                }
            }

            //U trai

            for (int i = min.Y - 1; i > -1; i--)
            {
                if (checkLineRow(min.Y - 1, i, min.X) && checkLineCol(min.X, max.X, i) && checkLineRow(max.Y - 1, i, max.X))
                {
                    return i;
                }
            }








            return -1;
        }

        //tra ve dog
        public static  int check3LineY(Point p1, Point p2)
        {
            Point min, max;
            if (p1.X < p2.X)
            {
                min = p1;
                max = p2;
            }
            else
            {
                min = p2;
                max = p1;
            }
            //Z
            for (int i = min.X + 1; i < max.X; i++)
            {
                if (checkLineCol(min.X + 1, i, min.Y) && checkLineRow(min.Y, max.Y, i) && checkLineCol(i, max.X - 1, max.Y))
                {
                    return i;
                }
            }

            //u duoi

            for (int i = max.X + 1; i < 10; i++)
            {
                if (checkLineCol(min.X + 1, i, min.Y) && checkLineRow(min.Y, max.Y, i) && checkLineCol(max.X + 1, i, max.Y))
                {
                    return i;
                }
            }
            //u tren

            for (int i = min.X - 1; i > -1; i--)
            {
                if (checkLineCol(min.X - 1, i, min.Y) && checkLineRow(min.Y, max.Y, i) && checkLineCol(max.X - 1, i, max.Y))
                {
                    return i;
                }
            }



            return -1;
        }
        //min max theo y
        public static int checkRowL(Point min, Point max)
        {
            if (min.X < max.X)
            {
                if (checkLineRow(min.Y + 1, max.Y, min.X) && checkLineCol(min.X, max.X - 1, max.Y))
                    return 0;
            }
            else
            {
                if (checkLineRow(min.Y + 1, max.Y, min.X) && checkLineCol(min.X, max.X + 1, max.Y))
                    return 0;
            }
            return -1;
        }
        //min max theo y
        public static int checkColL(Point min, Point max)
        {
            if (min.X < max.X)
            {
                if (checkLineCol(min.X + 1, max.X, min.Y) && checkLineRow(min.Y, max.Y - 1, max.X))
                    return 0;
            }
            else
            {
                if (checkLineCol(min.X - 1, max.X, min.Y) && checkLineRow(min.Y, max.Y - 1, max.X))
                    return 0;
            }
            return -1;
        }
        public static Point findMinY(Point p1, Point p2)
        {

            if (p1.Y < p2.Y)
            {
                return p1;
            }
            else
            {
                return p2;
            }
        }
        public static Point findMaxY(Point p1, Point p2)
        {

            if (p1.Y > p2.Y)
            {
                return p1;
            }
            else
            {
                return p2;
            }
        }

        public static Point findMinX(Point p1, Point p2)
        {

            if (p1.X < p2.X)
            {
                return p1;
            }
            else
            {
                return p2;
            }
        }
        public static Point findMaxX(Point p1, Point p2)
        {

            if (p1.X > p2.X)
            {
                return p1;
            }
            else
            {
                return p2;
            }
        }
        public static List<TwoPoint> findpath(Point p1, Point p2)
        {
            lineList.Clear();
            //checkLineRow
            if (p1.X == p2.X)
            {

                Point min, max;
                min = findMinY(p1, p2);
                max = findMaxY(p1, p2);

                if (checkLineRow(min.Y + 1, max.Y - 1, p1.X) || max.Y - min.Y == 1)
                {
                    lineList.Add(new TwoPoint(p1, p2));
                    return lineList;
                }
            }
            //checkLineCol
            if (p1.Y == p2.Y)
            {
                Point min, max;
                min = findMinX(p1, p2);
                max = findMaxX(p1, p2);
                if (checkLineCol(min.X + 1, max.X - 1, p1.Y) || max.X - min.X == 1)
                {
                    lineList.Add(new TwoPoint(p1, p2));
                    return lineList;
                }
            }



            int tmp;



            Point maxy, miny;
            miny = findMinY(p1, p2);
            maxy = findMaxY(p1, p2);
            if ((tmp = checkRowL(miny, maxy)) != -1)
            {
                lineList.Add(new TwoPoint(miny, new Point(miny.X, maxy.Y)));
                lineList.Add(new TwoPoint(new Point(miny.X, maxy.Y), maxy));
                return lineList;
            }
            if ((tmp = checkColL(miny, maxy)) != -1)
            {
                lineList.Add(new TwoPoint(miny, new Point(maxy.X, miny.Y)));
                lineList.Add(new TwoPoint(new Point(maxy.X, miny.Y), maxy));

                return lineList;
            }

            //check in rectangle with x

            if ((tmp = check3LineX(p1, p2)) != -1)
            {
                lineList.Add(new TwoPoint(p1, new Point(p1.X, tmp)));
                lineList.Add(new TwoPoint(new Point(p1.X, tmp), new Point(p2.X, tmp)));
                lineList.Add(new TwoPoint(new Point(p2.X, tmp), p2));
                return lineList;
            }

            // check in rectangle with y

            if ((tmp = check3LineY(p1, p2)) != -1)
            {
                lineList.Add(new TwoPoint(p1, new Point(tmp, p1.Y)));
                lineList.Add(new TwoPoint(new Point(tmp, p1.Y), new Point(tmp, p2.Y)));
                lineList.Add(new TwoPoint(new Point(tmp, p2.Y), p2));
                return lineList;
            }


            return lineList;
        }
    }
}
