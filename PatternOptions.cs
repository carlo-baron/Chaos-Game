using VectorCalculations;
using SFML.System;
using System.ComponentModel;

class PatternsOptionScene : Scene{
    Button backButton;

    Button triangle;
    Button circle;
    Button hexagon;
    Button carpet;

    Vector2f optionSize = new Vector2f(200, 75);
    public PatternsOptionScene() : base("Pattern Options"){
        Vector2f floatWindowSize = new Vector2f(windowData.Size.X, windowData.Size.Y);
        
        #region Shapes and Texts
        backButton = new Button(new Vector2f(50,50));
        backButton.Origin = new Vector2f(0,0);

        // options
        triangle = new Button(optionSize){
            Position = new Vector2f(FindCenter.Window(windowData).X, FindCenter.Window(windowData).Y - 150),
        };
        circle = new Button(optionSize){
            Position = new Vector2f(FindCenter.Window(windowData).X, FindCenter.Window(windowData).Y - 50),
        };
        hexagon = new Button(optionSize){
            Position = new Vector2f(FindCenter.Window(windowData).X, FindCenter.Window(windowData).Y + 50),
        };
        carpet = new Button(optionSize){
            Position = new Vector2f(FindCenter.Window(windowData).X, FindCenter.Window(windowData).Y + 150),
        };
        
        shapes.Add(triangle);
        shapes.Add(circle);
        shapes.Add(hexagon);
        shapes.Add(carpet);
        shapes.Add(backButton);
        #endregion

        #region Events
        windowData.Closed += (sender, args) => windowData.Close();
        backButton.Click += (sender, args) => BackButton();
        triangle.Click += (sender, args) => RunPattern(Application.PatternStates.TRIANGLE);
        circle.Click += (sender, args) => RunPattern(Application.PatternStates.CIRCLE);
        hexagon.Click += (sender, args) => RunPattern(Application.PatternStates.HEXAGON);
        carpet.Click += (sender, args) => RunPattern(Application.PatternStates.CARPET);
        #endregion
    }
    public override void Functions()
    {
        backButton.OnClickBehavior(windowData);
        triangle.OnClickBehavior(windowData);
        circle.OnClickBehavior(windowData);
        hexagon.OnClickBehavior(windowData);
        carpet.OnClickBehavior(windowData);
    }
    void BackButton(){
        windowData.Close();
        Menu menu = new Menu();
        Program.MainLoop(menu);
    }

    void RunPattern(Application.PatternStates state){
        windowData.Close();
        Application patternApp = new Application(state);
        Program.MainLoop(patternApp);
    }
}