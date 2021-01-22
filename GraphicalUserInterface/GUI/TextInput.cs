using GraphicalUserInterface.GUI.Abstract;
using SFML.Graphics;
using SFML.System;
using System;

namespace GraphicalUserInterface.GUI
{
    public class TextInput : TextElement
    {
        protected RectangleShape _rectangleShape;
        public TextInput(string placeholder = "") : base()
        {
            _editable = true;
            _content = placeholder;
            _textColor = Color.White;
            _characterSize = 15;
            _width = 150;
            _height = 20;
            _background = new Color(0xB3, 0xB3, 0xB3);
            Border = new Border() { BorderColor = new Color(0x59, 0x59, 0x59), BorderThickness = 2 };
            _rectangleShape = new RectangleShape()
            {
                Size = new Vector2f((float)_width - (Border.BorderThickness * 2.0f), (float)_height - (Border.BorderThickness * 2.0f)),
                FillColor = _background,
                OutlineColor = Border.BorderColor,
                OutlineThickness = Border.BorderThickness,
            };
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
                    _text.Position = new Vector2f(Border.BorderThickness, _texture.Size.Y / 2.0f);
                    break;
            }
            _rectangleShape.Position = new Vector2f(Border.BorderThickness, Border.BorderThickness);
            _texture.Clear(Color.Transparent);
            _texture.Draw(_rectangleShape);
            _texture.Draw(_text);
            _texture.Display();
            _sprite = new Sprite(_texture.Texture) { Position = Position };
        }

    }
}
