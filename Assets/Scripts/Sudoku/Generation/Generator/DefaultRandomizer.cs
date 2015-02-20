﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuModel
{
    class DefaultRandomizer : IRandomizer
    {
        Random rnd = new Random(Convert.ToInt32(System.DateTime.Now.Ticks % int.MaxValue));

        public int GetInt(int max)
        {
            return rnd.Next(max);
        }

        public int GetInt(int min, int max)
        {
            return rnd.Next(min, max);
        }
    }
}
