using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalUserInterface.GUI
{
    public class CheckBox : Element
    {
        public CheckBox() : base()
        {
            Background = new Color(0x73, 0x73, 0x73);
            this.Update();
        }

        public override void Update()
        {
            _text.Clear(Color.Transparent);
            RectangleShape rectange = new RectangleShape(
                new Vector2f(25.0f, 25.0f)
                - new Vector2f(2.0f * Border.BorderThickness, 2.0f * Border.BorderThickness))
            {
                Position = new Vector2f(Border.BorderThickness, Border.BorderThickness),
                OutlineThickness = Border.BorderThickness,
                OutlineColor = Border.BorderColor,
                FillColor = State ? Background + new Color(0x50, 0x50, 0x50) : Background,
            };
            _text.Draw(rectange, RenderStates.Default);
            _text.Display();
            _sprite = new Sprite(_text.Texture)
            {
                Position = Position
            };
            _rect = _sprite.GetLocalBounds();
        }
    }
}
