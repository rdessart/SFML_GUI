using GraphicalUserInterface.GUI;
using GraphicalUserInterface.GUI.Abstract;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicalUserInterface
{
    class Program
    {
        public static List<Element> boxes;
        //public static
        public static Element focusedElement;
        static void Main(string[] args)
        {
            Vector2f offset = new Vector2f(20.0f, 10.0f);
            Vector2f startPos = new Vector2f(20.0f, 20.0f);
            List<List<Element>> display = new List<List<Element>>()
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
            Gui gui = new Gui(new FloatRect(new Vector2f(50.0f, 50.0f), new Vector2f(400.0f, 400.0f)));
            gui.GenerateFromArray(display, startPos, offset);
            #region OLD
            //boxes = new List<Element>();
            //boxes.Add(new Label("Checkbox : ") 
            //{
            //    Position = new Vector2f(20.0f, 20.0f),
            //    TextColor = Color.White,
            //    Background = Color.Transparent 
            //});
            //boxes.Add(new CheckBox() 
            //{
            //    Name = "checkbox1",
            //    Position = new Vector2f((boxes[^1].GlobalBound.Left + boxes[^1].GlobalBound.Width),
            //    20.0f)
            //});
            //boxes.Add(new Label("Name : ")
            //{
            //    Position = new Vector2f(20.0f, boxes[^1].GlobalBound.Top + boxes[^1].GlobalBound.Height + 10.0f),
            //    TextColor = Color.White,
            //    Background = Color.Transparent
            //});
            //boxes.Add(new TextInput()
            //{ 
            //    HorizontalAlignement = HAlignement.Center,
            //    Name = "fistname",
            //    Position = new Vector2f((boxes[^1].GlobalBound.Left + boxes[^1].GlobalBound.Width), 
            //    boxes[^1].Position.Y) 
            //});
            //boxes.Add(new Button("SUBMIT") 
            //{
            //    HorizontalAlignement=HAlignement.Center,
            //    Name = "submitBut",
            //    Position = new Vector2f(20.0f, boxes[^1].GlobalBound.Top + boxes[^1].GlobalBound.Height + 10.0f) 
            //});
            //boxes[^1].StatusChanged += SubmitData;
            #endregion
            
            RenderWindow win = new RenderWindow(new VideoMode(1000, 1000), "Hello World", Styles.Default);
            win.Closed += (o, e) => win.Close();
            win.MouseButtonPressed += gui.OnMouseButtonPressed;
            win.KeyPressed += gui.OnKeyPressed;
            while (win.IsOpen)
            {
                win.DispatchEvents();
                win.Clear(Color.Black);
                gui.Draw(win);
                win.Display();
            }
            Console.ReadKey();
        }

        private static void SubmitData(object sender, EventArgs e)
        {
            Console.WriteLine("Data submited : ");
            TextInput firstname = boxes.Where( b => b.Name == "fistname").First() as TextInput;
            Console.WriteLine($"First name = {firstname.Content}");
        }
    }
}
