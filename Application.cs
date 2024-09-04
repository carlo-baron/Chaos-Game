using SFML.Graphics;
using SFML.Window;
using SFML.System;
using VectorCalculations;

class Application{
    RenderWindow window;
    uint bodySize = 200;
    float bodyThickness = 5;
    uint dotSize = 1;
    
    uint width;
    uint height;
    string windowTitle;

    bool firstDot = true;
    List<Dot> dots = new List<Dot>();
    CircleShape[] specialPoints = new CircleShape[12];
    float[] angles = {0,30,60,90,120,150,180,210,240,270,300,330};

    int patternCount;
    int patternCountMax = 10000;
    int iterationCount = 0;

    Font vt323 = new Font("VT323-Regular.ttf");

    Patterns patterns;
    public enum PatternStates{
        TRIANGLE,
        CIRCLE,
        HEXAGON,
        CARPET,
    }

    PatternStates myPattern;
    public Application(uint Width, uint Height, string Title, PatternStates pattern){
        width = Width;
        height = Height;
        windowTitle = Title;
        myPattern = pattern;

        VideoMode mode = new VideoMode(width, height);
        window = new RenderWindow(mode, windowTitle, Styles.Close);
        patternCount = patternCountMax;

        patterns = new Patterns(specialPoints);

        #region Events
        window.Closed += (sender, args) => window.Close();
        window.KeyPressed += (sender, args) => {
            if(args.Code == Keyboard.Key.R){
                //reset
                dots.Clear();
                patternCount = patternCountMax;
                iterationCount = 0;
                firstDot = true;
            }
        };
        window.MouseButtonPressed += (sender, args) => {
            if(args.Button == Mouse.Button.Left && firstDot){
                // call method
                Vector2f mousePosition = new Vector2f(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y);

                Dot dot = new Dot(dotSize){
                    Position = mousePosition,
                    FillColor = Color.Blue,
                };
                dots.Add(dot);
                firstDot = false;
            }
        };
        #endregion

        #region Shapes
        CircleShape body = new CircleShape(bodySize){
            OutlineThickness = bodyThickness,
            FillColor = Color.Black,
            Origin = new Vector2f(bodySize, bodySize),
            Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2),
        };

        // Special points (circle of fifths)
        for(int i = 0; i < specialPoints.Length; i++){
            specialPoints[i] = new CircleShape(5){
            FillColor = Color.Green,
            Origin = new Vector2f(5,5),
            Position = Points.SpecialPosition(bodySize, angles[i], body)
            };
        }
        #endregion

        #region Text
        Text iteration = new Text(){
            Font = vt323,
            DisplayedString = $"Iteration: {iterationCount}",
            CharacterSize = 24,
            Position = new Vector2f(5,5)
        };
        #endregion

        MainLoop(body, iteration);
    }

    void MainLoop(CircleShape circle, Text text){
        while(window.IsOpen){
            window.Clear();
            window.DispatchEvents();

            window.Draw(circle);
            foreach(CircleShape points in specialPoints){
                window.Draw(points);
            }

            // display first dot, then start the algorithm
            if(dots.Count > 0){
                Dot? lastDot = null;
                foreach(Dot dot in dots){
                    window.Draw(dot);
                    lastDot = dot;
                }
                if(patternCount >= 0){
                    if(lastDot != null){
                        PatternMaking(lastDot);
                        text.DisplayedString = $"Iteration: {iterationCount}";
                    }
                }
            }
            
            window.Draw(text);
            window.Display();
        }
    }

    void PatternMaking(CircleShape dot){
        Vector2f specialPosition = new Vector2f(0,0);
        
        switch(myPattern){
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

        Dot newDot = new Dot(dotSize){
                    Position = specialPosition,
                };
        
        dots.Add(newDot);

        iterationCount++;
        patternCount--;
    }
}