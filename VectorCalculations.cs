using SFML.System;
using SFML.Graphics;

namespace VectorCalculations;
class Points{
    public static Vector2f SpecialPosition(uint bodySize, float angle, CircleShape body){
        float radians = DegToRad(angle);
        float xCoordinate = body.Position.X + (bodySize * MathF.Cos(radians));
        float YCoordinate = body.Position.Y - (bodySize * MathF.Sin(radians));

        return new Vector2f(xCoordinate, YCoordinate);
    }

    static float DegToRad(float degree){
        return degree * (MathF.PI / 180);
    }

    public static Vector2f MidPoint(Vector2f vector1, Vector2f vector2){
        return new Vector2f((vector1.X + vector2.X) / 2, (vector1.Y + vector2.Y) / 2);
    }

    public static Vector2f LineVector(CircleShape[] specialPoints,int randomPoint, CircleShape dot,float sizeMultiplier){
        Vector2f lineVector = specialPoints[randomPoint].Position - dot.Position;
        return lineVector * sizeMultiplier;
    }
}

class FindCenter{
    public static Vector2f Rectangle(Vector2f objectSize){
        return new Vector2f(objectSize.X / 2, objectSize.Y / 2);
    }

    public static Vector2f Circle(float radius){
        return new Vector2f(radius, radius);
    }

    public static Vector2f Window(RenderWindow window){
        return new Vector2f(window.Size.X / 2, window.Size.Y / 2);
    }
}