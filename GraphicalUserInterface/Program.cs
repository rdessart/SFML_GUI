using GraphicalUserInterface.GUI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace GraphicalUserInterface
{
    class Program
    {
        public static List<Element> boxes;
        static void Main(string[] args)
        {
            boxes = new List<Element>();
            boxes.Add(new Label("Checkbox : ") { Position = new Vector2f(20.0f, 20.0f), TextColor = Color.White, Background = Color.Transparent });
            boxes.Add(new CheckBox() { Name = "checkbox1",Position = new Vector2f((boxes[^1].GlobalBound.Left + boxes[^1].GlobalBound.Width), 20.0f) });
            Font f = new Font(@"C:\Windows\Fonts\arial.ttf");
            Text t = new Text("Hello World", f, 25);
            RenderWindow win = new RenderWindow(new VideoMode(1000, 1000), "Hello World", Styles.Default);
            win.Closed += (o, e) => win.Close();
            win.MouseButtonPressed += Win_MouseButtonPressed;
            while (win.IsOpen)
            {
                win.DispatchEvents();
                win.Clear(Color.Black);
                foreach(Element elem in boxes)
                {
                    elem.Draw(win);
                }
                win.Display();
            }

            Console.ReadKey();
        }

        private static void Win_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            foreach (Element elem in boxes)
            {
                if(elem.Clicked(new Vector2f(e.X, e.Y)))
                {
                    elem.State = !elem.State;
                    break;
                }
            }
        }
    }
}
