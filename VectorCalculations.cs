using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace VectorCalculations;
class Points
{
    public static Vector2f SpecialPosition(uint bodySize, float angle, CircleShape body)
    {
        float radians = DegToRad(angle);
        float xCoordinate = body.Position.X + (bodySize * MathF.Cos(radians));
        float YCoordinate = body.Position.Y - (bodySize * MathF.Sin(radians));

        return new Vector2f(xCoordinate, YCoordinate);
    }

    static float DegToRad(float degree)
    {
        return degree * (MathF.PI / 180);
    }

    public static Vector2f MidPoint(Vector2f vector1, Vector2f vector2)
    {
        return new Vector2f((vector1.X + vector2.X) / 2, (vector1.Y + vector2.Y) / 2);
    }

    public static Vector2f LineVector(CircleShape[] specialPoints, int randomPoint, CircleShape dot, float sizeMultiplier)
    {
        Vector2f lineVector = specialPoints[randomPoint].Position - dot.Position;
        return lineVector * sizeMultiplier;
    }

    public static Vector2f[] RectangleVertices(RectangleShape rectangle){
        float halfedWidth = rectangle.Size.X / 2;
        float halfedheight = rectangle.Size.Y / 2;
        Vector2f rectanglePosition = rectangle.Position;

        Vector2f topLeft = rectanglePosition + new Vector2f(-halfedheight, -halfedheight);
        Vector2f topRight = rectanglePosition + new Vector2f(halfedheight, -halfedheight);
        Vector2f bottomRight = rectanglePosition + new Vector2f(halfedWidth, halfedheight);
        Vector2f bottomLeft = rectanglePosition + new Vector2f(-halfedheight, halfedheight);

        return [topLeft, topRight, bottomRight, bottomLeft];
    }
}

class FindCenter
{
    public static Vector2f Rectangle(Vector2f objectSize)
    {
        return new Vector2f(objectSize.X / 2, objectSize.Y / 2);
    }

    public static Vector2f Circle(float radius)
    {
        return new Vector2f(radius, radius);
    }

    public static Vector2f Window()
    {
        RenderWindow window = Datas.window!;
        return new Vector2f(window.Size.X / 2, window.Size.Y / 2);
    }

    public static Vector2f Text(Text text)
    {
        Vector2f center = text.GetGlobalBounds().Size / 2;
        return center + text.GetLocalBounds().Position;
    }
}

class Vectors{
    public static float Magnitude(Vector2f vector){
        return MathF.Sqrt(MathF.Pow(vector.X, 2) + MathF.Pow(vector.Y, 2));
    }

    public static Vector2f Normalize(Vector2f vector){
        float magnitude = Magnitude(vector);
        return new Vector2f(vector.X / magnitude, vector.Y / magnitude);
    }
}