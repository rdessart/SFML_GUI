using GraphicalUserInterface.GUI.Abstract;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface.GUI
{
    public class Gui
    {
        protected RenderTexture _texture;
        protected Element _focusElement;
        protected Sprite _sprite;
        protected FloatRect _size;
        public FloatRect Size { get => _size; set => _size = value; }
        public Vector2f Position
        {
            get => new Vector2f(Size.Left, Size.Top);
            set
            {
                _size.Left = value.X;
                _size.Top = value.Y;
            }
        }
        public HAlignement HorizontalAligment { get; set; }
        public List<Element> Elements { get; set; }
        public Color BackgroundColor { get; set; }

        public Sprite Sprite { get => _sprite; }

        public Gui()
        {
            Elements = new List<Element>();
            BackgroundColor = Color.Transparent;
        }

        public Gui(FloatRect size) : this()
        {
            Size = size;
            _texture = new RenderTexture((uint)Size.Width, (uint)Size.Height);
        }
        public void GenerateFromArray(List<List<Element>> display, Vector2f start, Vector2f offset)
        {
            Vector2f pos = start;
            Elements = new List<Element>();
            float maxX = 0.0f;
            float maxY = 0.0f;
            foreach (List<Element> line in display)
            {
                pos.X = start.X;
                foreach (Element elem in line)
                {
                    elem.Position = pos;
                    Elements.Add(elem);
                    pos.X = elem.GlobalBound.Left + elem.GlobalBound.Width + offset.X;
                    if(pos.Y + elem.GlobalBound.Height + offset.Y > maxY)
                    {
                        maxY = pos.Y + elem.GlobalBound.Height + offset.Y;
                    }
                }
                if (pos.X > maxX)
                {
                    maxX = pos.X;
                }
                pos.Y = Elements[^1].GlobalBound.Top + Elements[^1].GlobalBound.Height + offset.Y;
            }
            _texture = new RenderTexture((uint)maxX, (uint)(maxY));
            _size.Height = (uint)maxY;
            _size.Width = (uint)maxX;
        }
        public void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if(!_size.Contains(e.X, e.Y))
            {
                return;
            }
            Vector2f mousePos = new Vector2f(e.X - _size.Left, e.Y - _size.Top);
            if (e.Button == Mouse.Button.Left)
            {
                _focusElement = null;
                Keyboard.SetVirtualKeyboardVisible(false);
                foreach (Element elem in Elements)
                {
                    if (elem.Clicked(mousePos))
                    {
                        elem.State = !elem.State;
                        _focusElement = elem;
                        if (_focusElement is TextElement telem && telem.IsEditable)
                        {
                            Keyboard.SetVirtualKeyboardVisible(true);
                        }
                        break;
                    }
                }
            }
        }
        public void OnKeyPressed(Object sender, KeyEventArgs e)
        {
            if (_focusElement == null || !(_focusElement is TextElement elem) || elem.IsEditable == false)
            {
                return;
            }
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int pressedCode = (int)e.Code;
            if (pressedCode >= (int)Keyboard.Key.A && pressedCode <= (int)Keyboard.Key.Num9)
            {
                elem.Content += alphabet[pressedCode - (int)Keyboard.Key.A];
            }
            else if (pressedCode >= (int)Keyboard.Key.Numpad0 && pressedCode <= (int)Keyboard.Key.Numpad9)
            //I WAS UNABLE TO TEST THE CODE ON MY LAPTOP (NO NUMPAD)
            {
                elem.Content += alphabet[pressedCode - (int)Keyboard.Key.Numpad0 + 26];
            }
            else
            {
                switch (e.Code)
                {
                    case Keyboard.Key.Comma:
                        elem.Content += ',';
                        break;
                    case Keyboard.Key.Period:
                        elem.Content += '.';
                        break;
                    case Keyboard.Key.Slash:
                        elem.Content += '/';
                        break;
                    case Keyboard.Key.Backslash:
                        elem.Content += '\\';
                        break;
                    case Keyboard.Key.Equal:
                        elem.Content += '=';
                        break;
                    case Keyboard.Key.Hyphen:
                        elem.Content += '-';
                        break;
                    case Keyboard.Key.Space:
                        elem.Content += ' ';
                        break;
                    case Keyboard.Key.Backspace:
                        elem.Content = elem.Content[0..^1];
                        break;
                    case Keyboard.Key.Add:
                        elem.Content += '+';
                        break;
                    case Keyboard.Key.Subtract:
                        elem.Content += '-';
                        break;
                    case Keyboard.Key.Multiply:
                        elem.Content += '*';
                        break;
                    case Keyboard.Key.Divide:
                        elem.Content += '/';
                        break;
                    default:
                        Console.WriteLine($"Un-recognized key pressed {pressedCode}");
                        break;
                }
            }
        }
        public void Draw(RenderTarget target = null)
        {
            _texture.Clear(BackgroundColor);
            foreach (Element elem in Elements)
            {
                elem.Draw(_texture);
            }
            _texture.Display();
            _sprite = new Sprite(_texture.Texture);
            _sprite.Position = Position;
            FloatRect spriteRect = _sprite.GetLocalBounds();
            switch (HorizontalAligment)
            {
                case HAlignement.Left:
                    _sprite.Origin = new Vector2f(0.0f, 0.0f);
                    break;
                case HAlignement.Right:
                    _sprite.Origin = new Vector2f(spriteRect.Width, 0.0f);
                    break;
                case HAlignement.Center:
                    _sprite.Origin = new Vector2f((spriteRect.Left + spriteRect.Width) / 2.0f, 0.0f);
                    break;
            }
            if (target != null)
            {
                target.Draw(_sprite);
            }
        }
    }
}
