using GraphicalUserInterface.GUI;
using GraphicalUserInterface.GUI.Abstract;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public class Popup
    {
        protected Element _focusedElement;
        protected static Font font = new Font(@"C:\Windows\Fonts\arial.ttf");
        protected Sprite _sprite;
        protected RenderTexture _texture;
        protected Vector2f _size;
        protected Vector2f _position;
        protected Window _win;
        protected RenderTarget _parent;
        protected Text _caption;
        protected RectangleShape _rect;
        public Vector2f Size { get => _size; set => _size = value;}
        public Vector2f Position { get => _position; set => value = _position; }
        public List<Element> Elements { get; set; }
        public string Caption { get; set; }
        public string ClearedValue { get; set; }
        public Popup(Window win, RenderTarget parent = null)
        {
            _win = win;
            _parent = parent;
        }
        public Popup (RenderWindow win)
        {
            _win = win;
            _win.MouseButtonPressed += OnMouseButtonPressed;
            _win.KeyPressed += OnKeyPressed;
            _parent = win;
        }
        public void Update()
        {
            _texture.Clear(Color.Blue);
            DrawGUI();
        }
        private void GenerateTitleBar()
        {
            _caption = new Text()
            {
                Font = font,
                FillColor = Color.Black,
                DisplayedString = Caption,
                CharacterSize = 20,
            };
            FloatRect textRect = _caption.GetGlobalBounds();
            _caption.Origin = new Vector2f((textRect.Left + textRect.Width) / 2.0f, 0.0f);
            _caption.Position = new Vector2f(Size.X / 2.0f, 0.0f);
            _rect = new RectangleShape(new Vector2f((Size.X - 4.0f), 30.0f))
            {
                FillColor = new Color(0x60, 0x60, 0x60),
                OutlineColor = Color.Black,
                OutlineThickness = 2,
                Position = new Vector2f(0.0f, 0.0f)
            };
            Size = new Vector2f((textRect.Left + textRect.Width) + (2.0f * _rect.OutlineThickness), Size.Y);
        }
        public void GenerateFromArray(List<List<Element>> display, Vector2f offset)
        {
            Vector2f start = new Vector2f(Size.X / 2.0f, 10.0f);
            Vector2f pos = start;
            Elements = new List<Element>();
            float maxX = 150.0f;
            float maxY = 0.0f;
            foreach (List<Element> line in display)
            {
                pos.X = start.X;
                foreach (Element elem in line)
                {
                    elem.Position = pos;
                    Elements.Add(elem);
                    pos.X = elem.GlobalBound.Left + elem.GlobalBound.Width + offset.X;
                    if (pos.Y + elem.GlobalBound.Height + offset.Y > maxY)
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
            if(Size != new Vector2f(maxX, maxY))
            {
                Size = new Vector2f(maxX, maxY);
                if (_texture != null)
                {
                    _texture.Dispose();
                }
                _texture = new RenderTexture((uint)Size.X, (uint)Size.Y);
                GenerateFromArray(display, offset);
            }
        }
        public void Draw(RenderTarget t)
        {
            _texture.Display();
            _sprite = new Sprite(_texture.Texture)
            {
                Position = Position
            };
            _sprite.Draw(t, RenderStates.Default);
        }
        protected void DrawGUI()
        {
            foreach (Element elem in Elements)
            {
                elem.Draw(_texture);
            }
        }
        private void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            var oldFocus = _focusedElement;
            Vector2f localCoord = new Vector2f(e.X - Position.X, e.Y - Position.Y);
            _focusedElement = Elements.Where(e => e.Clicked(localCoord) == true).FirstOrDefault();
            if (_focusedElement != null)
            {
                _focusedElement.State = true;
                if(!string.IsNullOrWhiteSpace(ClearedValue))
                {
                    var elem = Elements.Where(el => el.Name == ClearedValue).First();
                    elem.State = false;
                }
                ClearedValue = _focusedElement.Name;
                Console.WriteLine($"{_focusedElement.Name} is clicked");
            }
            Update();
        }
        public void OnKeyPressed(Object sender, KeyEventArgs e)
        {
            if (_focusedElement == null || !(_focusedElement is TextElement elem) || elem.IsEditable == false)
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
            Update();
        }
    }
}
