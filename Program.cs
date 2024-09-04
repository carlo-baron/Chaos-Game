using SFML.Graphics;

class Program{

    static Menu menu = new Menu();
    static void Main(){

        MainLoop(menu);
    }

    public static void MainLoop(Scene scene){
        while(scene.windowData.IsOpen){
            scene.windowData.DispatchEvents();
            scene.windowData.Clear();

            scene.Functions();

            foreach(Drawable shape in scene.Shapes()){
                scene.windowData.Draw(shape);
            }

            scene.windowData.Display();
        }
    }    
} 
