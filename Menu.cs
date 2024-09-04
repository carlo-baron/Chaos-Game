using SFML.Graphics;
using SFML.System;
using VectorCalculations;

class Menu : Scene{
    Button startButton;
    Button quitButton;
    Text startText;
    Text closeText;
    uint textSize = 48;
    public Menu() : base(){
        Vector2f floatWindowSize = new Vector2f(windowData.Size.X, windowData.Size.Y);

        #region Shapes and Texts
        Vector2f buttonSize = new Vector2f(200,75);

        startButton = new Button(buttonSize){
            Origin = FindCenter.Rectangle(buttonSize),
            Position = new Vector2f(FindCenter.Rectangle(floatWindowSize).X, FindCenter.Rectangle(floatWindowSize).Y - 50)
        };

        startText = new Text(){
            Font = Datas.vt323,
            DisplayedString = "Start",
            CharacterSize = textSize,
            FillColor = Color.Black,
        };

        FloatRect startTextSize = startText.GetGlobalBounds();

        startText.Origin = new Vector2f(startTextSize.Width / 2, startTextSize.Height / 2 + startText.CharacterSize / 2);
        startText.Position = startButton.Position;

        quitButton = new Button(buttonSize){
            Origin = FindCenter.Rectangle(buttonSize),
            Position = new Vector2f(FindCenter.Rectangle(floatWindowSize).X, FindCenter.Rectangle(floatWindowSize).Y + 50)
        };

        closeText = new Text(){
            Font = Datas.vt323,
            DisplayedString = "Quit",
            CharacterSize = textSize,
            FillColor = Color.Black,
        };

        FloatRect closeTextSize = closeText.GetGlobalBounds();

        closeText.Origin = new Vector2f(closeTextSize.Width / 2, closeTextSize.Height / 2 + textSize/2);
        closeText.Position = quitButton.Position;
        #endregion

        #region Events
        windowData.Closed += (sender, args) => windowData.Close();
        startButton.Click += (sender, args) => StartButton();
        quitButton.Click += (sender, args) => QuitButton();
        #endregion
    }

    public override Drawable[] Shapes(){
        return [startButton, quitButton, startText, closeText];
    }

    public override void Functions(){
        startButton.OnClickBehavior(windowData);
        quitButton.OnClickBehavior(windowData);
    }

    void StartButton(){
        windowData.Close();
        PatternsOptionScene patterns = new PatternsOptionScene();
        Program.MainLoop(patterns, patterns.Shapes());
    }

    void QuitButton(){
        windowData.Close();
    }
}