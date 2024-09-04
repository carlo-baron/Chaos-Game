using SFML.Graphics;
using SFML.System;
using SFML.Window;
using VectorCalculations;

class Button : RectangleShape{
    public Button(Vector2f size) : base(size){}

    public delegate void OnClickEventHandler(object source, EventArgs eventArgs);
    public event OnClickEventHandler? Click;

    public void OnClickBehavior(RenderWindow windowData){
        bool isClicked = Mouse.IsButtonPressed(Mouse.Button.Left);
        bool isHovered = ButtonHoverCheck(windowData);

        if(isClicked && isHovered){
            OnClick();
        }
    }

    protected virtual void OnClick(){
        if(Click != null){
            Click(this, EventArgs.Empty);
        }
    }

    float[] ButtonBounds(){
        Vector2f size = Size;
        Vector2f sizeHalfed = FindCenter.Rectangle(size);
        Vector2f position = Position;

        float left = position.X + -sizeHalfed.X;
        float right = position.X + sizeHalfed.X;
        float top = position.Y + -sizeHalfed.Y;
        float bottom = position.Y + sizeHalfed.Y;

        return [left, top, right, bottom];
    }

    private bool ButtonHoverCheck(RenderWindow window){
        //apparently, this is also a way, a much better way.
        return GetGlobalBounds().Contains(Mouse.GetPosition(window));
    }
}