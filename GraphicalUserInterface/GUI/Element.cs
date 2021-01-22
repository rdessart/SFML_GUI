using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalUserInterface.GUI
{
    public enum HAlignement
    {
        Left,
        Right,
        Center
    }
    public abstract class Element
    {
        protected FloatRect _rect;
        protected Sprite _sprite;
        protected RenderTexture _texture;
        protected static Font font = new Font(@"C:\Windows\Fonts\arial.ttf");
        protected bool _state;
        protected bool _enabled;
        protected bool _focus;
        protected Vector2f _position;
        protected Color _background;
        public string Name { get; set; }
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
        public Color Background { get => _background;
            set
            {
                _background = value;
                Update();
            } 
        }
        public Border Border { get; set; }
        public FloatRect LocalBound { 
            get => _sprite.GetLocalBounds();
        }
        public FloatRect GlobalBound
        {
            get => _sprite.GetGlobalBounds();
        }
        public Vector2f Position { 
            get => _position;
            set 
            {
                _position = value;
                if(_sprite != null)
                {
                    _sprite.Position = value;
                }
            }
        }
        public Element()
        {
            Name = "";
            _state = false;
            _enabled = false;
            _focus = false;
            Border = new Border
            {
                BorderColor = Color.White,
                BorderThickness = 2,
            };
            _texture = new RenderTexture(25, 25);
        }
        protected virtual void Update()
        {
        }
        public bool Clicked(Vector2f MousePosition)
        {
            return GlobalBound.Contains(MousePosition.X, MousePosition.Y);
        }
        public void Draw(RenderTarget target)
        {
            _sprite.Position = _position;
            target.Draw(_sprite, RenderStates.Default);
        }
    }
}
