using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GraphicalUserInterface
{
    public class Display
    {
        protected RenderTexture _texture;
        protected Sprite _sprite;
        protected RenderTarget _parent;
        protected Window _window;
        protected Vector2f _position;
        protected Vector2f _size;
        public RenderTarget Parent
        {
            get => _parent;
            set => _parent = value;
        }
        public bool MouseOver(int x, int y)
        {
            return _sprite.GetGlobalBounds().Contains(x, y);
        }
    }
}
