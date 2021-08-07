using System;
using System.Collections.Generic;
using System.Text;

namespace ProceduralMapGenerator
{

    class Map
    {
        const int NUMBER_OF_ITERATIONS = 3;

        readonly Random psrg = new Random();

        private int[,] _map;

        private int _mapWidth;
        private int _mapHeight;

        private int _fillDensity;

        public Map (int mapW, int mapH, int d)
        {
            _mapWidth = mapW;
            _mapHeight = mapH;
            _fillDensity = d;
            _map = new int[mapW, mapH];
        }

        private int RandomBlock()
        { 
            int rand = psrg.Next(0, 100);
            return rand >= _fillDensity ? 0 : 1;
        }

        private void RandomFill()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (x == 0 || y == 0 || x == _mapWidth - 1 || y == _mapHeight - 1)
                    {
                        _map[x, y] = 1;
                    }
                    else
                    {
                        _map[x, y] = RandomBlock();
                    }
                }
            }
        }

        private bool IsWall(int x, int y)
        {
            return _map[x, y] == 1;
        }

        private int CountAdjacentWalls(int x, int y)
        {
            int wallCount = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (!(i == x && j == y))
                    {
                        if (IsWall(i, j))
                            wallCount++;
                    }
                }
            }

            return wallCount;
        }

        private int PlaceWall(int x, int y)
        {
            int adjWalls = CountAdjacentWalls(x, y);

            if (_map[x, y] == 1)
            {
                if (adjWalls >= 4)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else if (_map[x, y] == 0)
            {
                if (adjWalls >= 5)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return _map[x, y];
            }
        }
        private void CaveMap(int numberOfIterations)
        {
            int[,] temp = new int[_mapWidth, _mapHeight];
            temp = _map;

            for (int i = 0; i < numberOfIterations; i++)
            {
                for (int x = 1; x < _mapWidth - 1; x++)
                {
                    for (int y = 1; y < _mapHeight - 1; y++)
                    {
                        temp[x, y] = PlaceWall(x, y);
                    }
                }
            }

            _map = temp;
        }
        private void WriteAt(int x, int y, string s)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }

        private string Symbol(int x, int y)
        {
            return _map[x, y] == 1 ? "#" : " ";
        }

        public void Init()
        {
            RandomFill();
            CaveMap(NUMBER_OF_ITERATIONS);
        }
        public void ChangeDensity(int sign)
        {
            _fillDensity += 5 * sign;

            if (_fillDensity < 0)
                _fillDensity = 0;
            else if (_fillDensity > 100)
                _fillDensity = 100;
        }

        public void Draw()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    WriteAt(x, y, Symbol(x, y));
                }
            }
        }

        public void Reset()
        {
            Init();
        }

        public int Density
        {
            get
            {
                return _fillDensity;
            }
        }

        public string MapToString()
        {
            StringBuilder sb = new StringBuilder();
            string nextChar;

            for(int rows = 0; rows < _mapHeight; rows++)
            {
                for(int cols = 0; cols < _mapWidth; cols++)
                {
                    nextChar = Symbol(cols, rows);
                    sb.Append(nextChar);
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}
