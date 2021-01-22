using GraphicalUserInterface.GUI.Abstract;
using SFML.Graphics;
using SFML.System;

namespace GraphicalUserInterface.GUI
{
    public class CheckBox : Element
    {
        public CheckBox() : base()
        {
            Background = new Color(0x73, 0x73, 0x73);
            this.Update();
        }

        protected override void Update()
        {
            if(_texture == null)
            {
                return;
            }
            _texture.Clear(Color.Transparent);
            RectangleShape rectange = new RectangleShape(new Vector2f(25.0f, 25.0f) - new Vector2f(2.0f * Border.BorderThickness, 
                2.0f * Border.BorderThickness))
            {
                Position = new Vector2f(Border.BorderThickness, Border.BorderThickness),
                OutlineThickness = Border.BorderThickness,
                OutlineColor = Border.BorderColor,
                FillColor = State ? Background + new Color(0x50, 0x50, 0x50) : Background,
            };
            _texture.Draw(rectange, RenderStates.Default);
            _texture.Display();
            _sprite = new Sprite(_texture.Texture)
            {
                Position = Position
            };
            _rect = _sprite.GetLocalBounds();
        }
    }
}
