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
            _content = content;
            _textColor = Color.Black;
            _characterSize = 15;
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
            _texture = new RenderTexture(
                _width ?? (uint)Math.Ceiling(textSize.Width + textSize.Left),
                _height ?? (uint)Math.Ceiling(textSize.Height + textSize.Top));

            switch (HorizontalAlignement)
            {
                case HAlignement.Center:
                    _text.Origin = new Vector2f(textSize.Width / 2.0f, (textSize.Height + textSize.Top) / 2.0f);
                    _text.Position = new Vector2f(_texture.Size.X / 2.0f, _texture.Size.Y / 2.0f);
                    break;
                default:
                    _text.Origin = new Vector2f(0.0f, (textSize.Height + textSize.Top) / 2.0f);
                    _text.Position = new Vector2f(0.0f, _texture.Size.Y / 2.0f);
                    break;
            }
            _texture.Clear(Background);
            _texture.Draw(_text);
            _texture.Display();
            _sprite = new Sprite(_texture.Texture) { Position = Position };
        }
    }
}
