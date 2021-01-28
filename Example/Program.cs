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
            RenderWindow win = new RenderWindow(new VideoMode(1000, 1500), "Hello World", Styles.Default);
            win.Closed += (o, e) => win.Close();
            AltitudePopup popup = new AltitudePopup(win)
            {
                Callsign = "BEL1234",
            };
            win.Clear(Color.Red);
            while (win.IsOpen)
            {
                win.DispatchEvents();
                win.Clear(Color.Black);
                popup.Draw(win);
                win.Display();
            }
            Console.ReadKey();
        }
        private static void SubmitData(object sender, EventArgs e)
        {
            Console.WriteLine("Data submited : ");
            TextInput firstname = GUI.Elements.Where( b => b.Name == "fistname").First() as TextInput;
            Console.WriteLine($"First name = {firstname.Content}");
        }
    }
}
