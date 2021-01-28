using GraphicalUserInterface.GUI;
using GraphicalUserInterface.GUI.Abstract;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalUserInterface
{
    public class AltitudePopup : Popup
    {
        private string _callsign;
        public string Callsign
        {
            get => _callsign;
            set
            {
                _callsign = value;
                if (Elements == null)
                    return;
                Element selElem = Elements.Where(el => el.Name == "Callsign").First();
                if(selElem != null && selElem is Label lab)
                {
                    lab.Content = _callsign;
                }
                Update();
            }
        }
        public AltitudePopup(RenderWindow win) : base(win)
        {
           
            CreateGUI();
        }
        public AltitudePopup(Window win, RenderTarget target = null): base(win, target)
        {
            CreateGUI();
        }

        private void CreateGUI()
        {
            Callsign = "|";
            var temp = new List<List<Element>>()
            {
                new List<Element>()
                {
                    new Label(Callsign)
                    {
                        Name = "Callsign",
                        TextColor = Color.White,
                        Background = Color.Transparent,
                        HorizontalAlignment = HAlignement.Center,
                    },
                },
                new List<Element>()
                {
                    new Label("CFL")
                    {
                        Name="CFL_LABEL",
                        TextColor = Color.White,
                        Background = Color.Transparent,
                        HorizontalAlignment = HAlignement.Center,
                    },
                },
            };
            int alt = 410;
            while (alt >= 10)
            {
                string display;
                if (alt < 45)
                {
                    display = $"A{alt:00}";
                }
                else
                {
                    display = $"{alt:000}";
                }
                temp.Add(new List<Element>()
                {
                    new Label(display)
                    {
                        TextColor = Color.Black,
                        Background = Color.White,
                        HorizontalAlignment = HAlignement.Center,
                        Name = alt.ToString("000"),
                    }
                });
                if (alt < 45)
                {
                    alt -= 5;
                }
                else
                {
                    alt -= 10;
                }
            }
            temp.Add(new List<Element>()
                {
                    new Button("CA")
                    {
                        Name="ClearedApproach",
                        TextColor = Color.Black,
                        Background = new Color(0xB3, 0xB3, 0xB3),
                        HorizontalAlignment = HAlignement.Center,
                    },
                });
            temp.Add(new List<Element>()
                {
                    new Button("VA")
                    {
                        Name="ClearedVisual",
                        TextColor = Color.Black,
                        Background = new Color(0xB3, 0xB3, 0xB3),
                        HorizontalAlignment = HAlignement.Center,
                    },
                });
            temp.Add(new List<Element>()
                {
                    new Button("LDN")
                    {
                        Name="ClearedToLand",
                        TextColor = Color.Black,
                        Background = new Color(0xB3, 0xB3, 0xB3),
                        HorizontalAlignment = HAlignement.Center,
                    },
                });

            GenerateFromArray(temp, new Vector2f(10.0f, 10.0f));
            Update();
        }
    }
}
