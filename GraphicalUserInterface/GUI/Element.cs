using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalUserInterface.GUI
{
    public abstract class Element
    {
        protected static Font font = new Font(@"C:\Windows\Fonts\arial.ttf");
        protected bool _state;
        protected bool _enabled;
        protected bool _focus;
        public bool State 
        { 
            get => _state;
            set 
            {
                _state = value;
                Update();
            }
        }
        public bool IsEnabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                Update();
            }
        }
        public bool IsFocus
        {
            get => _focus;
            set
            {
                _focus = value;
                Update();
            }
        }
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
        protected virtual void Update()
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
