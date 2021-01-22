using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalUserInterface.GUI
{
    public abstract class Element
    {
        public bool State { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsFocus { get; set; }
        public Color Background { get; set; }
        public Border Border { get; set; }
        public Vector2f Position { get; set; }
        protected FloatRect _rect;
        protected Sprite _sprite;
        protected RenderTexture _text;

        public Element()
        {
            State = false;
            IsEnabled = false;
            IsFocus = false;
            Border = new Border
            {
                BorderColor = Color.White,
                BorderThickness = 2,
            };
            _text = new RenderTexture(25, 25);
        }
        public virtual void Update()
        {
        }

        public bool Clicked(Vector2f MousePosition)
        {
            return _rect.Contains(MousePosition.X, MousePosition.Y);
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(_sprite, RenderStates.Default);
        }
    }
}
