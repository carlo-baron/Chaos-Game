using SFML.Graphics;

public sealed class Datas{
    private static Datas? instance;
    public static Font vt323 = new Font("VT323-Regular.ttf");
    private Datas(){}

    public static Datas Instance{
        get {
            if(instance == null){
                instance = new Datas();
            }
            return instance;
        }
    }
}