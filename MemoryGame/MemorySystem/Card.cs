using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MemorySystem
{
    public class Card : INotifyPropertyChanged
    {
        string _cardtext = "";
        System.Drawing.Color _backcolor;
        public event PropertyChangedEventHandler? PropertyChanged;
        public string CardText
        {
            get => _cardtext; 
            set 
            { 
                _cardtext = value; 
                InvokePropertyChanged(); 
            }//lo betucha shetzarih po aroch!
        } 

        public bool Enabled { get; set; } = false;

        public System.Drawing.Color BackColor
        {
            get => _backcolor;
            set
            {
                _backcolor = value;
                InvokePropertyChanged();
            }
        } //= Game.CardFontColor;
        public System.Drawing.Color FontColor { get; set; } = System.Drawing.Color.CornflowerBlue;

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
        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
