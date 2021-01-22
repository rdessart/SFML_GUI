using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface.GUI
{
    public abstract class TextElement : Element
    {
        protected static Font font = new Font(@"C:\Windows\Fonts\arial.ttf");

        protected string _content;
        protected uint _characterSize;
        protected Text _text;
        protected Color _textColor;
        protected HAlignement _horizontalAlignement;
        protected uint? _width;
        protected uint? _height;
       
        public TextElement() : base() { }

        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                Update();
            }
        }
        public string Content
        {
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

        public HAlignement HorizontalAlignement
        {
            get => _horizontalAlignement;
            set
            {
                _horizontalAlignement = value;
                Update();
            }
        }
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
    }
}
