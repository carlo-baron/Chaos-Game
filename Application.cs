using SFML.Graphics;
using SFML.Window;
using SFML.System;
using VectorCalculations;

class Application : Scene
{
    CircleShape body;
    uint bodySize = 200;
    float bodyThickness = 5;
    uint dotSize = 1;
    bool firstDot = true;
    List<Dot> dots = new List<Dot>();

    CircleShape[] specialPoints = new CircleShape[12];
    float[] angles = { 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330 };

    Text iteration;

    int patternCount;
    int patternCountMax = 10000;
    int iterationCount = 0;

    Patterns patterns;
    public enum PatternStates
    {
        TRIANGLE,
        CIRCLE,
        HEXAGON,
        CARPET,
    }
    PatternStates myPattern;

    public Application(PatternStates pattern) : base(pattern.ToString())
    {
        myPattern = pattern;
        patternCount = patternCountMax;

        patterns = new Patterns(specialPoints);

        #region Shapes and Texts
        body = new CircleShape(bodySize)
        {
            OutlineThickness = bodyThickness,
            FillColor = Color.Black,
            Origin = new Vector2f(bodySize, bodySize),
            Position = new Vector2f(windowData.Size.X / 2, windowData.Size.Y / 2),
        };

        // Special points (circle of fifths)
        for (int i = 0; i < specialPoints.Length; i++)
        {
            specialPoints[i] = new CircleShape(5)
            {
                FillColor = Color.Green,
                Origin = new Vector2f(5, 5),
                Position = Points.SpecialPosition(bodySize, angles[i], body)
            };
        }

        iteration = new Text()
        {
            Font = Datas.vt323,
            DisplayedString = $"Iteration: {iterationCount}",
            CharacterSize = 24,
            Position = new Vector2f(5, 5)
        };
        #endregion

        #region Events
        windowData.Closed += (sender, args) => windowData.Close();
        windowData.KeyPressed += (sender, args) =>
        {
            if (args.Code == Keyboard.Key.R)
            {
                //reset
                dots.Clear();
                patternCount = patternCountMax;
                iterationCount = 0;
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
        windowData.Draw(body);
        foreach (CircleShape points in specialPoints)
        {
            windowData.Draw(points);
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

    public override Drawable[] Shapes()
    {
        return [];
    }

    void PatternMakingBehavior(CircleShape dot)
    {
        Vector2f specialPosition = new Vector2f(0, 0);

        switch (myPattern)
        {
            case PatternStates.TRIANGLE:
                specialPosition = patterns.TrianglePattern(dot);
                break;
            case PatternStates.CIRCLE:
                specialPosition = patterns.CirclePattern(dot);
                break;
            case PatternStates.HEXAGON:
                specialPosition = patterns.HexagonPattern(dot);
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

}