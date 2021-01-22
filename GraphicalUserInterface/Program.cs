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
            boxes = new List<Element>()
            {
                new CheckBox(),
            };
            Font f = new Font(@"C:\Windows\Fonts\arial.ttf");
            Text t = new Text("Hello World", f, 25);
            RenderWindow win = new RenderWindow(new VideoMode(1000, 1000), "Hello World", Styles.Default);
            win.Closed += (o, e) => win.Close();
            win.MouseButtonPressed += Win_MouseButtonPressed;
            while (win.IsOpen)
            {
                win.DispatchEvents();
                win.Clear(Color.Black);
                foreach(CheckBox cb in boxes)
                {
                    cb.Draw(win);
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
                    //elem.Update();
                    break;
                }
            }
        }
    }
}
