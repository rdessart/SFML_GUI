using GraphicalUserInterface;
using GraphicalUserInterface.GUI;
using GraphicalUserInterface.GUI.Abstract;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example
{
    class Program
    {
        public static Gui GUI;
        static void Main(string[] args)
        {
            RenderWindow win = new RenderWindow(new VideoMode(1000, 1000), "Hello World", Styles.Default);
            win.Closed += (o, e) => win.Close();
            //GUI = CreateGUI(null);
            Popup popup = new Popup(win)
            {
                Caption = "Test Popup",
            };
            popup.GenerateFromArray(GenerateGrid(), new Vector2f(20.0f, 10.0f));
            
            win.Clear(Color.Red);
            while (win.IsOpen)
            {
                win.DispatchEvents();
                win.Clear(Color.Black);
                popup.Update();
                popup.Draw(win);
                win.Display();
            }
            Console.ReadKey();
        }
        private static Gui CreateGUI(RenderWindow window)
        {
            Gui gui = new Gui(new FloatRect(new Vector2f(50.0f, 50.0f), new Vector2f(400.0f, 400.0f)))
            {
                Window = window,
            };
            gui.GenerateFromArray(GenerateGrid(), offset: new Vector2f(20.0f, 10.0f), start: new Vector2f(20.0f, 20.0f));
            return gui;
        }
        private static List<List<Element>> GenerateGrid()
        {
            return new List<List<Element>>()
            {
                new List<Element>()
                {
                    new Label("Checkbox : ")
                    {
                        TextColor = Color.White,
                        Background = Color.Transparent
                    },
                    new CheckBox()
                    {
                        Name = "checkbox1",
                    }
                },
                new List<Element>()
                {
                    new Label("Name : ")
                    {
                        TextColor = Color.White,
                        Background = Color.Transparent
                    },
                    new TextInput()
                    {
                        HorizontalAlignement = HAlignement.Center,
                        Name = "fistname"
                    }
                },
                new List<Element>()
                {
                    new Button("SUBMIT")
                    {
                        HorizontalAlignement=HAlignement.Center,
                        Name = "submitBut",
                    }
                }
            };
        }
        private static void SubmitData(object sender, EventArgs e)
        {
            Console.WriteLine("Data submited : ");
            TextInput firstname = GUI.Elements.Where( b => b.Name == "fistname").First() as TextInput;
            Console.WriteLine($"First name = {firstname.Content}");
        }
    }
}
