using SFML.Graphics;
using SFML.System;
using VectorCalculations;

class Menu : Scene{
    Button startButton;
    Button quitButton;
    Text startText;
    Text closeText;
    uint textSize = 48;
    public Menu() : base("Menu"){
        Vector2f floatWindowSize = new Vector2f(windowData.Size.X, windowData.Size.Y);

        #region Shapes and Texts
        Vector2f buttonSize = new Vector2f(200,75);

        startButton = new Button(buttonSize){
            Position = new Vector2f(FindCenter.Window(windowData).X, FindCenter.Window(windowData).Y - 50),
        };
        shapes.Add(startButton);

        startText = new Text(){
            Font = Datas.vt323,
            DisplayedString = "Start",
            CharacterSize = textSize,
            FillColor = Color.Black,
        };
        startText.Origin = FindCenter.Text(startText);
        startText.Position = startButton.Position;

        shapes.Add(startText);

        quitButton = new Button(buttonSize){
            Position = new Vector2f(FindCenter.Window(windowData).X, FindCenter.Window(windowData).Y + 50),
        };
        shapes.Add(quitButton);

        closeText = new Text(){
            Font = Datas.vt323,
            DisplayedString = "Quit",
            CharacterSize = textSize,
            FillColor = Color.Black,
        };
        closeText.Origin = FindCenter.Text(closeText);
        closeText.Position = quitButton.Position;

        shapes.Add(closeText);
        #endregion

        #region Events
        windowData.Closed += (sender, args) => windowData.Close();
        startButton.Click += (sender, args) => StartButton();
        quitButton.Click += (sender, args) => QuitButton();
        #endregion
    }
    public override void Functions(){
        startButton.OnClickBehavior(windowData);
        quitButton.OnClickBehavior(windowData);
    }

    void StartButton(){
        windowData.Close();
        PatternsOptionScene patterns = new PatternsOptionScene();
        Program.MainLoop(patterns);
    }

    void QuitButton(){
        windowData.Close();
    }
}