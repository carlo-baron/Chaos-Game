using SFML.Graphics;
using SFML.Window;
using SFML.System;

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

    int patternCount;
    int patternCountMax = 10000;
    int iterationCount = 0;

    Font vt323 = new Font("VT323-Regular.ttf");

    public Application(uint Width, uint Height, string Title){
        width = Width;
        height = Height;
        windowTitle = Title;

        VideoMode mode = new VideoMode(width, height);
        window = new RenderWindow(mode, windowTitle, Styles.Close);
        patternCount = patternCountMax;


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

        CircleShape specialPoint1 = new CircleShape(5){
            FillColor = Color.Green,
            Origin = new Vector2f(5,5),
            Position = SpecialPosition(90, body)
        };

        CircleShape specialPoint2 = new CircleShape(5){
            FillColor = Color.Green,
            Origin = new Vector2f(5,5),
            Position = SpecialPosition(225, body)
        };

        CircleShape specialPoint3 = new CircleShape(5){
            FillColor = Color.Green,
            Origin = new Vector2f(5,5),
            Position = SpecialPosition(315, body)
        };
        #endregion

        #region Text
        Text iteration = new Text(){
            Font = vt323,
            DisplayedString = $"Iteration: {iterationCount}",
            CharacterSize = 24,
            Position = new Vector2f(5,5)
        };
        #endregion

        MainLoop(body, iteration,[specialPoint1, specialPoint2, specialPoint3]);
    }

    void MainLoop(CircleShape circle, Text text, CircleShape[] specialPoints){
        while(window.IsOpen){
            window.Clear();
            window.DispatchEvents();

            window.Draw(circle);
            foreach(CircleShape points in specialPoints){
                window.Draw(points);
            }
            
            if(dots.Count > 0){
                Dot? lastDot = null;
                foreach(Dot dot in dots){
                    window.Draw(dot);
                    lastDot = dot;
                }
                if(patternCount >= 0){
                    if(lastDot != null){
                        PatternMaking(lastDot, specialPoints);
                        text.DisplayedString = $"Iteration: {iterationCount}";
                    }
                }
            }
            
            window.Draw(text);
            window.Display();
        }
    }

    void PatternMaking(CircleShape dot, CircleShape[] specialPoints){
        Random random = new Random();
        int randomPoint = random.Next(specialPoints.Count());

        float midpointX = (dot.Position.X + specialPoints[randomPoint].Position.X) / 2;
        float midpointY = (dot.Position.Y + specialPoints[randomPoint].Position.Y) / 2;
        Vector2f midpoint = new Vector2f(midpointX, midpointY);

        Dot newDot = new Dot(dotSize){
                    Position = midpoint,
                    FillColor = Color.Blue,
                };
        
        dots.Add(newDot);

        iterationCount++;
        patternCount--;
    }

    Vector2f SpecialPosition(float angle, CircleShape body){
        float radians = DegToRad(angle);
        float xCoordinate = body.Position.X + (bodySize * MathF.Cos(radians));
        float YCoordinate = body.Position.Y - (bodySize * MathF.Sin(radians));

        return new Vector2f(xCoordinate, YCoordinate);
    }

    float DegToRad(float degree){
        return degree * (MathF.PI / 180);
    }
}