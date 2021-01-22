using System;
using System.Collections.Generic;
using SFML.Graphics;
using System.Text;

namespace GraphicalUserInterface.GUI
{
    public class Border
    {
        public Color BorderColor { get; set; }
        public uint BorderThickness { get; set; }

        public Border()
        {
            BorderThickness = 1;
            BorderColor = Color.Black;
        }
    }
}
