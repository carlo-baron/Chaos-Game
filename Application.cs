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
    public Application(uint Width, uint Height, string Title){
        width = Width;
        height = Height;
        windowTitle = Title;

        VideoMode mode = new VideoMode(width, height);
        window = new RenderWindow(mode, windowTitle);


        #region Events
        window.Closed += (sender, args) => window.Close();
        window.KeyPressed += (sender, args) => {
            if(args.Code == Keyboard.Key.R){
                //do something
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

        CircleShape dot = new CircleShape(dotSize){
            Origin = new Vector2f(dotSize, dotSize),
            Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2),

        };
        #endregion

        MainLoop(body, dot);
    }

    void MainLoop(CircleShape circle, CircleShape dot){
        while(window.IsOpen){
            window.Clear();
            window.DispatchEvents();

            window.Draw(circle);
            window.Draw(dot);

            window.Display();
        }
    }
}