using SFML.Graphics;
using SFML.Window;

abstract class Scene{
    public RenderWindow windowData;
    protected Scene(string windowName,uint width = 500, uint height = 500){
        VideoMode mode = new VideoMode(width,height);
        windowData = new RenderWindow(mode, windowName, Styles.Close);
    }

    public abstract void Functions();
    public abstract Drawable[] Shapes();
}