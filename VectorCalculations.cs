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
}