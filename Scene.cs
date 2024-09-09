using SFML.Graphics;
using SFML.Window;

abstract class Scene{
    public RenderWindow windowData;
    public List<Drawable> shapes = new List<Drawable>();
    protected Scene(string windowName,uint width = 500, uint height = 500){
        VideoMode mode = new VideoMode(width,height);
        windowData = new RenderWindow(mode, windowName, Styles.Close);
        Datas.window = windowData;
    }

    public abstract void Functions();
    public virtual Drawable[] Shapes(){
        return shapes.ToArray();
    }
}