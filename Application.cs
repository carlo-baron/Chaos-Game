using SFML.Graphics;
using SFML.Window;
using SFML.System;
using VectorCalculations;
using System;

class Application : Scene
{
    uint dotSize = 1;
    bool firstDot = true;
    List<Dot> dots = new List<Dot>();
    Text iteration;
    Button backButton;

    int patternCount;
    int patternCountMax = 10000;
    int iterationCount = 0;

    Patterns patterns;
    public enum PatternStates
    {
        TRIANGLE,
        DODECAGON,
        HEXAGON,
        CARPET,
    }
    PatternStates myPattern;

    public Application(PatternStates pattern) : base(pattern.ToString())
    {
        myPattern = pattern;
        patternCount = patternCountMax;

        patterns = new Patterns();

        #region Shapes and Texts

        iteration = new Text()
        {
            Font = Datas.vt323,
            DisplayedString = $"Iteration: {iterationCount}",
            CharacterSize = 24,
            Position = new Vector2f(5, 5)
        };

        backButton = new Button(new Vector2f(50, 50))
        {
            Origin = new Vector2f(0, 0),
            Position = new Vector2f(windowData.Size.X - 50, 0)
        };
        #endregion

        #region Events
        backButton.Click += (sender, args) => BackButton();
        windowData.Closed += (sender, args) => windowData.Close();
        windowData.KeyPressed += (sender, args) =>
        {
            if (args.Code == Keyboard.Key.R)
            {
                //reset
                dots.Clear();
                patternCount = patternCountMax;
                iterationCount = 0;
                iteration.DisplayedString = $"Iteration: {iterationCount}";
                firstDot = true;
            }
        };
        windowData.MouseButtonPressed += (sender, args) =>
        {
            if (args.Button == Mouse.Button.Left && firstDot)
            {
                Vector2f mousePosition = new Vector2f(Mouse.GetPosition(windowData).X, Mouse.GetPosition(windowData).Y);

                Dot dot = new Dot(dotSize)
                {
                    Position = mousePosition,
                    FillColor = Color.Blue,
                };
                dots.Add(dot);
                firstDot = false;
            }
        };
        #endregion
    }

    public override void Functions()
    {
        backButton.OnClickBehavior(windowData);
        windowData.Draw(backButton);
        foreach(RectangleShape line in Body.PatternBodyLines(myPattern)){
            windowData.Draw(line);
        }

        // display first dot, then start the algorithm
        if (dots.Count > 0)
        {
            Dot? lastDot = null;
            foreach (Dot dot in dots)
            {
                windowData.Draw(dot);
                lastDot = dot;
            }
            if (patternCount >= 0)
            {
                if (lastDot != null)
                {
                    PatternMakingBehavior(lastDot);
                    iteration.DisplayedString = $"Iteration: {iterationCount}";
                }
            }
        }
        windowData.Draw(iteration);
    }

    void PatternMakingBehavior(CircleShape dot)
    {
        Vector2f specialPosition = new Vector2f(0, 0);

        switch (myPattern)
        {
            case PatternStates.TRIANGLE:
                specialPosition = patterns.TrianglePattern(dot);
                break;
            case PatternStates.DODECAGON:
                specialPosition = patterns.DodecagonPattern(dot);
                break;
            case PatternStates.HEXAGON:
                specialPosition = patterns.HexagonPattern(dot);
                break;
            case PatternStates.CARPET:
                specialPosition = patterns.CarpetPattern(dot);
                break;
        }

        Dot newDot = new Dot(dotSize)
        {
            Position = specialPosition,
        };

        dots.Add(newDot);

        iterationCount++;
        patternCount--;
    }

    void BackButton(){
        windowData.Close();
        PatternsOptionScene patternsOption = new PatternsOptionScene();
        Program.MainLoop(patternsOption);
    }
}