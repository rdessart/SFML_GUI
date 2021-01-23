using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalUserInterface.GUI.Abstract
{
    public enum HAlignement
    {
        Left,
        Right,
        Center
    }
    public abstract class Element
    {
        #region EVENT
        public event EventHandler StatusChanged;
        #endregion
        #region PROTECTED ATTRIBUTES
        protected FloatRect _rect;
        protected Sprite _sprite;
        protected RenderTexture _texture;
        protected bool _state;
        protected bool _enabled;
        protected bool _focus;
        protected Vector2f _position;
        protected Color _background;
        protected string _name;
        #endregion
        #region PUBLIC ATTRIBUTES
        public float Scale { get; set; }
        public string Name { 
            get => _name;
            set {
                _name = value;
                }
        }
        public bool State 
        { 
            get => _state;
            set 
            {
                _state = value;
                OnStatusChanged();
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
            get
            {
                if (_sprite is null)
                {
                    return new FloatRect(_position.X, _position.Y, 0.0f, 0.0f);
                }
                return _sprite.GetLocalBounds();
            }
        }
        public FloatRect GlobalBound
        {
            get 
            {
                if (_sprite is null)
                {
                    return new FloatRect(_position.X, _position.Y, 0.0f, 0.0f);
                }
                return _sprite.GetGlobalBounds();
            }
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
            Scale = 1.0f;
            //Name = "";
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
        #endregion
        #region PROTECTED METHODS
        protected void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
        protected abstract void Update();
        #endregion
        #region PUBLIC METHODS
        public bool Clicked(Vector2f MousePosition)
        {
            return GlobalBound.Contains(MousePosition.X, MousePosition.Y);
        }
        public void Draw(RenderTarget target)
        {
            if (_sprite != null)
            {
                _sprite.Position = _position;
                _sprite.Scale = new Vector2f(Scale, Scale);
                target.Draw(_sprite, RenderStates.Default);
            }
        }
        #endregion
    }
}
