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
    CircleShape[] specialPoints = new CircleShape[12];
    float[] angles = {0,30,60,90,120,150,180,210,240,270,300,330};

    int patternCount;
    int patternCountMax = 10000;
    int iterationCount = 0;

    Font vt323 = new Font("VT323-Regular.ttf");

    public enum Pattern{
        TRIANGLE,
        CIRCLE
    }


    Pattern myPattern;
    public Application(uint Width, uint Height, string Title, Pattern pattern){
        width = Width;
        height = Height;
        windowTitle = Title;
        myPattern = pattern;

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

        // Special points (circle of fifths)
        for(int i = 0; i < specialPoints.Length; i++){
            specialPoints[i] = new CircleShape(5){
            FillColor = Color.Green,
            Origin = new Vector2f(5,5),
            Position = SpecialPosition(angles[i], body)
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
            case Pattern.TRIANGLE:
                specialPosition = TrianglePattern(dot);
                break;
            case Pattern.CIRCLE:
                specialPosition = CirclePattern(dot);
                break;
        }
        

        Dot newDot = new Dot(dotSize){
                    Position = specialPosition,
                };
        
        dots.Add(newDot);

        iterationCount++;
        patternCount--;
    }

    Vector2f CirclePattern(CircleShape dot){
        Random random = new Random();
        int randomPoint = random.Next(specialPoints.Count());

        Vector2f lineVector = specialPoints[randomPoint].Position - dot.Position;
        Vector2f scaledVector = lineVector * 0.789f;

        return dot.Position + scaledVector;
    }

    Vector2f TrianglePattern(CircleShape dot){
        CircleShape[] newSpecialPoints = [specialPoints[3], specialPoints[7], specialPoints[11]];

        Random random = new Random();
        int randomPoint = random.Next(newSpecialPoints.Count());

        float midpointX = (dot.Position.X + newSpecialPoints[randomPoint].Position.X) / 2;
        float midpointY = (dot.Position.Y + newSpecialPoints[randomPoint].Position.Y) / 2;
        return new Vector2f(midpointX, midpointY);
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