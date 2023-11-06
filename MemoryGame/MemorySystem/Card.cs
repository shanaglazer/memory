using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MemorySystem
{
    public class Card : INotifyPropertyChanged
    {
        bool _enabled = false;
        string _cardtext = "";
        System.Drawing.Color _fontcolor = System.Drawing.Color.CornflowerBlue;
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

        public bool Enabled { get=>_enabled; set { _enabled = value; InvokePropertyChanged(); } }

        public System.Drawing.Color BackColor
        {
            get => _backcolor;
            set
            {
                _backcolor = value;
                InvokePropertyChanged();
                InvokePropertyChanged("BackColorMaui");
            }
        } //= Game.CardFontColor;
        public System.Drawing.Color FontColor { get=>_fontcolor; set { _fontcolor = value; InvokePropertyChanged(); InvokePropertyChanged("BackColorMaui"); } }

        public Microsoft.Maui.Graphics.Color BackColorMaui
        {
            get => this.ConvertToMauiColor(this.BackColor);
        }

        public Microsoft.Maui.Graphics.Color FontColorMaui
        {
            get => this.ConvertToMauiColor(this.FontColor);
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
