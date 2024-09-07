using SFML.Graphics;
using SFML.System;
using VectorCalculations;

class Line : RectangleShape{
    public Line(Vector2f point1, Vector2f point2) : base(){
        Vector2f rotation = point2 - point1;
        float length = Vectors.Magnitude(rotation);
        float degrees = MathF.Atan2(rotation.Y, rotation.X) * (180/MathF.PI);

        Size = new Vector2f(length, 2.5f);
        FillColor = Color.Green;
        Origin = new Vector2f(0, 2.5f / 2);
        Rotation = degrees;
        Position = point1;
    }
}