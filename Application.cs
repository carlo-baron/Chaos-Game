using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Application{
    RenderWindow window;
    uint bodySize = 200;
    float bodyThickness = 5;
    uint dotSize = 5;
    
    
    uint width;
    uint height;
    string windowTitle;

    bool firstDot = true;
    List<Dot> dots = new List<Dot>();
    public Application(uint Width, uint Height, string Title){
        width = Width;
        height = Height;
        windowTitle = Title;

        VideoMode mode = new VideoMode(width, height);
        window = new RenderWindow(mode, windowTitle, Styles.Close);


        #region Events
        window.Closed += (sender, args) => window.Close();
        window.KeyPressed += (sender, args) => {
            if(args.Code == Keyboard.Key.R){
                //reset
                dots.Clear();
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

        MainLoop(body, [specialPoint1, specialPoint2, specialPoint3]);
    }

    void MainLoop(CircleShape circle, CircleShape[] specialPoints){
        while(window.IsOpen){
            window.Clear();
            window.DispatchEvents();

            window.Draw(circle);
            foreach(CircleShape points in specialPoints){
                window.Draw(points);
            }
            
            foreach(Dot dot in dots){
                window.Draw(dot);
            }

            window.Display();
        }
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