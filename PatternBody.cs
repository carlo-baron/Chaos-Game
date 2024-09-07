using SFML.Graphics;
using SFML.System;

class Body{
    static Patterns patterns = new Patterns();

    public static RectangleShape[] PatternBody(Application.PatternStates state){
        switch(state){
            case Application.PatternStates.TRIANGLE:
                return LineMaker(patterns.TriangleVertices);
            case Application.PatternStates.DODECAGON:
                return LineMaker(patterns.Dodecagon);
            case Application.PatternStates.HEXAGON:
                return LineMaker(patterns.HexagonVertices);
            default:
                return[];
        }
    }

    private static RectangleShape[] LineMaker(Vector2f[] vertices){
        List<RectangleShape> lines = new List<RectangleShape>();
        for (int i = 0; i < vertices.Length; i++)
        {
            Line line;
            if(i != vertices.Length - 1){
                line = new Line(vertices[i], vertices[i+1]);
            }else{
                line = new Line(vertices[i], vertices[0]);
            }
            lines.Add(line);
        }
        return lines.ToArray();
    }
}


// Vector2f rotation = specialPoints[2].Position - specialPoints[0].Position;
// float length = Vectors.Magnitude(rotation);
// float degrees = MathF.Atan2(rotation.Y, rotation.X) * (180/MathF.PI);
// Console.WriteLine(degrees);
// line1 = new RectangleShape(new Vector2f(length, 5)){
//     FillColor = Color.Blue,
//     Origin = new Vector2f(0, 5 / 2),
//     Position = specialPoints[0].Position,
//     Rotation = degrees,
// };