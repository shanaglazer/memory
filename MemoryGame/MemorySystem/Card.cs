using System.Drawing;

namespace MemorySystem
{
    public class Card
    {
        List<string> lstletters = new() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        string _cardtext = "";
        System.Drawing.Color _backcolor;
        //List<Button> lstbuttons;
        public Card()
        {

        }
        public string CardText
        {
            get => _cardtext;
            set
            {
                _cardtext = value;
            }
        }

        public bool Enabled { get; set; } = false;

        public System.Drawing.Color BackColor
        {
            get => _backcolor;
            set
            {
                _backcolor = value;
            }
        } 

        public Microsoft.Maui.Graphics.Color BackColorMaui
        {
            get => this.ConvertToMauiColor(this.BackColor);
        }

        private Microsoft.Maui.Graphics.Color ConvertToMauiColor(System.Drawing.Color systemColor)
        {
            float red = systemColor.R / 255f;
            float green = systemColor.G / 255f;
            float blue = systemColor.B / 255f;
            float alpha = systemColor.A / 255f;

            return new Microsoft.Maui.Graphics.Color(red, green, blue, alpha);
        }
    }
}
