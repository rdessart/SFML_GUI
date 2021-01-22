﻿using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalUserInterface.GUI
{
    public class Label : Element
    {
        protected string _content;
        protected uint _characterSize;
        protected Text _text;
        protected Color _textColor;
        protected HAlignement _horizontalAlignement;
        protected uint? _width;
        protected uint? _height;
        public uint? Width
        {
            get => _width;
            set
            {
                _width = value;
                Update();
            }
        }
        public uint? Height
        {
            get => _height;
            set
            {
                _height = value;
                Update();
            }
        }
        public HAlignement HorizontalAlignement
        {
            get => _horizontalAlignement;
            set
            {
                _horizontalAlignement = value;
                Update();
            }
        }
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                Update();
            }
        }
        public string Content {
            get => _content;
            set
            {
                _content = value;
                Update();
            }
        }
        public uint CharacterSize
        {
            get => _characterSize;
            set
            {
                _characterSize = value;
                Update();
            }
        }
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
                    Font = font
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
