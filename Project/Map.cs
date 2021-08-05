using System;
using System.Collections.Generic;
using System.Text;

namespace ProceduralMapGenerator
{
    struct Vector2DInt
    {
        int x;
        int y;

        public Vector2DInt(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }

    class Map
    {
        Random psrg = new Random();

        private int _mapWidth;
        private int _mapHeight;

        private int _fillDensity;

        public Map (int mapW, int mapH, int d)
        {
            _mapWidth = mapW;
            _mapHeight = mapH;
            _fillDensity = d;
        }
    }
}
