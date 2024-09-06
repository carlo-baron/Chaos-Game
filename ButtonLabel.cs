using SFML.Graphics;
using VectorCalculations;

class ButtonLabel : Text{
    public ButtonLabel(string displayedString,uint size, Button button) : base(){
        DisplayedString = displayedString;
        CharacterSize = size;
        Font = Datas.vt323;
        FillColor = Color.Black;
        Origin = FindCenter.Text(this);
        Position = button.Position;
    }
}