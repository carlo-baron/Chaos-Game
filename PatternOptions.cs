using SFML.Graphics;
using SFML.System;
using VectorCalculations;

class PatternsOptionScene : Scene{
    Button backButton;
    public PatternsOptionScene() : base(){
        Vector2f floatWindowSize = new Vector2f(windowData.Size.X, windowData.Size.Y);
        
        #region Shapes and Texts
        backButton = new Button(new Vector2f(50,50));
        #endregion

        #region Events
        windowData.Closed += (sender, args) => windowData.Close();
        backButton.Click += (sender, args) => BackButton();
        #endregion
    }
    public override void Functions()
    {
        backButton.OnClickBehavior(windowData);
    }

    public override Drawable[] Shapes()
    {
        return [backButton];
    }

    void BackButton(){
        windowData.Close();
        Menu menu = new Menu();
        Program.MainLoop(menu, menu.Shapes());
    }
}