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
        public static Element focusedElement;
        static void Main(string[] args)
        {
            boxes = new List<Element>();
            boxes.Add(new Label("Checkbox : ") {Position = new Vector2f(20.0f, 20.0f), TextColor = Color.White, Background = Color.Transparent });
            boxes.Add(new CheckBox() {Name = "checkbox1",Position = new Vector2f((boxes[^1].GlobalBound.Left + boxes[^1].GlobalBound.Width), 20.0f) });
            boxes.Add(new Label("Name : ") {Position = new Vector2f(20.0f, boxes[^1].GlobalBound.Top + boxes[^1].GlobalBound.Height + 10.0f), TextColor = Color.White, Background = Color.Transparent });
            boxes.Add(new TextInput() {Name = "fistname",Position = new Vector2f((boxes[^1].GlobalBound.Left + boxes[^1].GlobalBound.Width), boxes[^1].Position.Y) });

            RenderWindow win = new RenderWindow(new VideoMode(1000, 1000), "Hello World", Styles.Default);
            win.Closed += (o, e) => win.Close();
            win.MouseButtonPressed += Win_MouseButtonPressed;
            win.KeyPressed += Win_KeyPressed;
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

        private static void Win_KeyPressed(object sender, KeyEventArgs e)
        {
            if(focusedElement == null || !(focusedElement is TextElement elem) || elem.IsEditable == false)
            {
                return;
            }
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int pressedCode = (int)e.Code;
            if(pressedCode >= (int)Keyboard.Key.A && pressedCode <= (int)Keyboard.Key.Num9)
            {
                Console.WriteLine($"Key is {alphabet[pressedCode - (int)Keyboard.Key.A]}");
                elem.Content += alphabet[pressedCode - (int)Keyboard.Key.A];
            }
            else if(pressedCode >= (int)Keyboard.Key.Numpad0 && pressedCode <= (int)Keyboard.Key.Numpad9)
            //I WAS UNABLE TO TEST THE CODE ON MY LAPTOP (NO NUMPAD)
            {
                Console.WriteLine($"Key is {alphabet[pressedCode - (int)Keyboard.Key.Numpad0 + 26]}");
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

        private static void Win_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            focusedElement = null;
            Keyboard.SetVirtualKeyboardVisible(false);
            foreach (Element elem in boxes)
            {
                if (elem.Clicked(new Vector2f(e.X, e.Y)))
                {
                    elem.State = !elem.State;
                    focusedElement = elem;
                    if (focusedElement is TextElement telem && telem.IsEditable)
                    {
                        Keyboard.SetVirtualKeyboardVisible(true);
                    }
                    break;
                }
            }
        }
    }
}
