using SFML.System;
using SFML.Graphics;

class VectorCalculations{
    public Vector2f SpecialPosition(uint bodySize, float angle, CircleShape body){
        float radians = DegToRad(angle);
        float xCoordinate = body.Position.X + (bodySize * MathF.Cos(radians));
        float YCoordinate = body.Position.Y - (bodySize * MathF.Sin(radians));

        return new Vector2f(xCoordinate, YCoordinate);
    }

    float DegToRad(float degree){
        return degree * (MathF.PI / 180);
    }

    public Vector2f MidPoint(Vector2f vector1, Vector2f vector2){
        return new Vector2f((vector1.X + vector2.X) / 2, (vector1.Y + vector2.Y) / 2);
    }

    public Vector2f LineVector(CircleShape[] specialPoints,int randomPoint, CircleShape dot,float sizeMultiplier){
        Vector2f lineVector = specialPoints[randomPoint].Position - dot.Position;
        return lineVector * sizeMultiplier;
    }
}