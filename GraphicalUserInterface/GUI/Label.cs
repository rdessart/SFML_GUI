using GraphicalUserInterface.GUI.Abstract;
using SFML.Graphics;
using SFML.System;
using System;

namespace GraphicalUserInterface.GUI
{
    public class Label : TextElement
    {
        public Label(string content) : base()
        {
            _state = false;
            _content = content;
            _textColor = Color.Black;
            _characterSize = 15;
            _border = null;
            Update();
        }
        protected override void Update()
        {
            if (_text == null)
            {
                _text = new Text()
                {
                    Font = font,
                };
            }
            _text.CharacterSize = _characterSize;
            _text.FillColor = _textColor;
            _text.DisplayedString = _content;
            FloatRect textSize = _text.GetLocalBounds();
            if ((!_width.HasValue || _width.Value <= 0) && ((uint)Math.Ceiling(textSize.Width + textSize.Left) <= 0
                || !_height.HasValue || _height.Value <= 0) && (uint)Math.Ceiling(textSize.Height + textSize.Top) <= 0)
            {
                return;
            }

            int borderThickness = Border != null ? (int)Border.BorderThickness : 0;

            _texture = new RenderTexture(
                _width ?? (uint)Math.Ceiling(textSize.Width + textSize.Left + 2),
                _height ?? (uint)Math.Ceiling(textSize.Height + textSize.Top + 2));
            _texture.Clear(_state ? Background : Color.Transparent);
            if (_border != null)
            {
                RectangleShape border = new RectangleShape(new Vector2f((float)(_texture.Size.X - borderThickness * 2.0f), (float)(_texture.Size.Y - (borderThickness * 2.0f))))
                {
                    OutlineColor = _border.BorderColor,
                    OutlineThickness = _border.BorderThickness,
                    FillColor = Color.Transparent
                };
                _texture.Draw(border);
            }
            switch (HorizontalAlignment)
            {
                case HAlignement.Center:
                    _text.Origin = new Vector2f(textSize.Width / 2.0f, (textSize.Height + textSize.Top) / 2.0f);
                    _text.Position = new Vector2f(_texture.Size.X / 2.0f, (_texture.Size.Y / 2.0f) - (borderThickness * 2.0f));
                    break;
                default:
                    _text.Origin = new Vector2f(0.0f, (textSize.Height + textSize.Top) / 2.0f);
                    _text.Position = new Vector2f(borderThickness, (_texture.Size.Y / 2.0f) - (borderThickness * 2.0f));
                    break;
            }
            _texture.Draw(_text);
            _texture.Display();
            _sprite = new Sprite(_texture.Texture) { Position = Position };
        }
    }
}
