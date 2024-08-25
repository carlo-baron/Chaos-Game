using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Application{
    RenderWindow window;
    uint bodySize = 200;
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
            OutlineThickness = 5f,
            FillColor = Color.Black,
            Origin = new Vector2f(bodySize, bodySize),
            Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2),
        };
        #endregion

        MainLoop(body);
    }

    void MainLoop(CircleShape circle){
        while(window.IsOpen){
            window.Clear();
            window.DispatchEvents();

            window.Draw(circle);
            foreach(Dot dot in dots){
                window.Draw(dot);
            }

            window.Display();
        }
    }
}