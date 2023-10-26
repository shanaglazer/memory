using System.Drawing;

namespace MemorySystem
{
    public class Card
    {
        string _cardtext = "";
        System.Drawing.Color _backcolor;

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
        } //= Game.CardFontColor;

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
