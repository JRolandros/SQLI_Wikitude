using Xamarin.Forms;

namespace SQLI_CrossAR.Utils
{
    public class PaletteDesign
    {
        public Color PrimaryColorDark { get; }
        public Color PrimaryColor { get; }
        public Color PrimaryColorLight { get; }
        public Color TextIcons { get; }
        public Color AccentColor { get; }
        public Color TextPrimary { get; }
        public Color TextSecondary { get; }
        public Color DividerColor { get; }
        public Color WhiteBackground { get; }

        public double TitleFontSize { get; }
        public double SubtitleFontSize { get; }
        public double TextLargeFontSize { get; }
        public double TextMediumFontSize { get; }
        public double TextSmallFontSize { get; }
        public double TextTinyFontSize { get; }

        public PaletteDesign(string primary, string secondary)
        {
            TextPrimary     = Color.FromHex("#212121");
            TextSecondary   = Color.FromHex("#727272");
            DividerColor    = Color.FromHex("#B6B6B6");
            WhiteBackground = Color.FromHex("#FFFFFF");

            TitleFontSize      = 20;
            SubtitleFontSize   = 18;
            TextLargeFontSize  = 16;
            TextMediumFontSize = 14;
            TextSmallFontSize  = 12;
            TextTinyFontSize   = 10;
            
            switch(primary)
            {
                case "red":
                    PrimaryColorDark    = Color.FromHex("#D32F2F");
                    PrimaryColor        = Color.FromHex("#F44336");
                    PrimaryColorLight   = Color.FromHex("#FFCDD2");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "pink":
                    PrimaryColorDark    = Color.FromHex("#C2185B");
                    PrimaryColor        = Color.FromHex("#E91E63");
                    PrimaryColorLight   = Color.FromHex("#F8BBD0");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "purple":
                    PrimaryColorDark    = Color.FromHex("#7B1FA2");
                    PrimaryColor        = Color.FromHex("#9C27B0");
                    PrimaryColorLight   = Color.FromHex("#E1BEE7");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "deeppurple":
                    PrimaryColorDark    = Color.FromHex("#512DA8");
                    PrimaryColor        = Color.FromHex("#673AB7");
                    PrimaryColorLight   = Color.FromHex("#D1C4E9");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "indigo":
                    PrimaryColorDark    = Color.FromHex("#303F9F");
                    PrimaryColor        = Color.FromHex("#3F51B5");
                    PrimaryColorLight   = Color.FromHex("#C5CAE9");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "blue":
                    PrimaryColorDark    = Color.FromHex("#1976D2");
                    PrimaryColor        = Color.FromHex("#2196F3");
                    PrimaryColorLight   = Color.FromHex("#BBDEFB");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "lightblue":
                    PrimaryColorDark    = Color.FromHex("#0288D1");
                    PrimaryColor        = Color.FromHex("#03A9F4");
                    PrimaryColorLight   = Color.FromHex("#B3E5FC");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "cyan":
                    PrimaryColorDark    = Color.FromHex("#0097A7");
                    PrimaryColor        = Color.FromHex("#00BCD4");
                    PrimaryColorLight   = Color.FromHex("#B2EBF2");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "teal":
                    PrimaryColorDark    = Color.FromHex("#00796B");
                    PrimaryColor        = Color.FromHex("#009688");
                    PrimaryColorLight   = Color.FromHex("#B2DFDB");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "green":
                    PrimaryColorDark    = Color.FromHex("#388E3C");
                    PrimaryColor        = Color.FromHex("#4CAF50");
                    PrimaryColorLight   = Color.FromHex("#C8E6C9");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "lightgreen":
                    PrimaryColorDark    = Color.FromHex("#689F38");
                    PrimaryColor        = Color.FromHex("#8BC34A");
                    PrimaryColorLight   = Color.FromHex("#DCEDC8");
                    TextIcons           = Color.FromHex("#212121");
                    break;
                case "lime":
                    PrimaryColorDark    = Color.FromHex("#AFB42B");
                    PrimaryColor        = Color.FromHex("#CDDC39");
                    PrimaryColorLight   = Color.FromHex("#F0F4C3");
                    TextIcons           = Color.FromHex("#212121");
                    break;                    
                case "yellow":
                    PrimaryColorDark    = Color.FromHex("#0288D1");
                    PrimaryColor        = Color.FromHex("#03A9F4");
                    PrimaryColorLight   = Color.FromHex("#B3E5FC");
                    TextIcons           = Color.FromHex("#212121");
                    break;
                case "amber":
                    PrimaryColorDark    = Color.FromHex("#FFA000");
                    PrimaryColor        = Color.FromHex("#FFC107");
                    PrimaryColorLight   = Color.FromHex("#FFECB3");
                    TextIcons           = Color.FromHex("#212121");
                    break;
                case "orange":
                    PrimaryColorDark    = Color.FromHex("#F57C00");
                    PrimaryColor        = Color.FromHex("#FF9800");
                    PrimaryColorLight   = Color.FromHex("#FFE0B2");
                    TextIcons           = Color.FromHex("#212121");
                    break;                    
                case "deeporange":
                    PrimaryColorDark    = Color.FromHex("#E64A19");
                    PrimaryColor        = Color.FromHex("#FF5722");
                    PrimaryColorLight   = Color.FromHex("#FFCCBC");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "brown":
                    PrimaryColorDark    = Color.FromHex("#5D4037");
                    PrimaryColor        = Color.FromHex("#795548");
                    PrimaryColorLight   = Color.FromHex("#D7CCC8");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                case "grey":
                    PrimaryColorDark    = Color.FromHex("#616161");
                    PrimaryColor        = Color.FromHex("#9E9E9E");
                    PrimaryColorLight   = Color.FromHex("#F5F5F5");
                    TextIcons           = Color.FromHex("#212121");
                    break;
                case "bluegrey":
                    PrimaryColorDark    = Color.FromHex("#455A64");
                    PrimaryColor        = Color.FromHex("#607D8B");
                    PrimaryColorLight   = Color.FromHex("#CFD8DC");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
                default:
                    PrimaryColorDark    = Color.FromHex("");
                    PrimaryColor        = Color.FromHex("");
                    PrimaryColorLight   = Color.FromHex("");
                    TextIcons           = Color.FromHex("#FFFFFF");
                    break;
            }

            switch(secondary)
            {
                case "red":                    
                    AccentColor = Color.FromHex("#FF5252");
                    break;
                case "pink":
                    AccentColor = Color.FromHex("#FF4081");
                    break;
                case "purple":
                    AccentColor = Color.FromHex("#E040FB");
                    break;
                case "deeppurple":
                    AccentColor = Color.FromHex("");
                    break;
                case "indigo":
                    AccentColor = Color.FromHex("");
                    break;
                case "blue":
                    AccentColor = Color.FromHex("");
                    break;
                case "lightblue":
                    AccentColor = Color.FromHex("");
                    break;
                case "cyan":
                    AccentColor = Color.FromHex("");
                    break;
                case "teal":
                    AccentColor = Color.FromHex("");
                    break;
                case "green":
                    AccentColor = Color.FromHex("");
                    break;
                case "lightgreen":
                    AccentColor = Color.FromHex("");
                    break;
                case "lime":
                    AccentColor = Color.FromHex("");
                    break;
                case "yellow":
                    AccentColor = Color.FromHex("");
                    break;
                case "amber":
                    AccentColor = Color.FromHex("#FFC107");
                    break;
                case "orange":
                    AccentColor = Color.FromHex("");
                    break;
                case "deeporange":
                    AccentColor = Color.FromHex("");
                    break;
                case "brown":
                    AccentColor = Color.FromHex("");
                    break;
                case "grey":
                    AccentColor = Color.FromHex("");
                    break;
                case "bluegrey":
                    AccentColor = Color.FromHex("");
                    break;
                default:
                    AccentColor = Color.FromHex("");
                    break;

            }
        }
    }
}
