using SFML.Graphics;

class Program{

    static Menu menu = new Menu();
    static void Main(){

        MainLoop(menu, menu.Shapes());
    }

    public static void MainLoop(Scene scene, params Drawable[] shapes){
        while(scene.windowData.IsOpen){
            scene.windowData.DispatchEvents();
            scene.windowData.Clear();

            scene.Functions();

            foreach(Drawable shape in shapes){
                scene.windowData.Draw(shape);
            }

            scene.windowData.Display();
        }
    }    
} 
