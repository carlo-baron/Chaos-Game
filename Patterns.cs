using SFML.Graphics;
using SFML.System;

class Patterns{

    CircleShape[] specialPoints;
    public Patterns(CircleShape[] points){
        specialPoints = points;
    }
    public Vector2f CirclePattern(CircleShape dot){
        Random random = new Random();
        int randomPoint = random.Next(specialPoints.Count());

        Vector2f scaledVector = LineVector(specialPoints, randomPoint, dot, 0.789f);

        return dot.Position + scaledVector;
    }

    public Vector2f TrianglePattern(CircleShape dot){
        //Triangle shape made by the points
        CircleShape[] triangleVertices = [specialPoints[3], specialPoints[7], specialPoints[11]];

        Random random = new Random();
        int randomPoint = random.Next(triangleVertices.Count());

        float midpointX = (dot.Position.X + triangleVertices[randomPoint].Position.X) / 2;
        float midpointY = (dot.Position.Y + triangleVertices[randomPoint].Position.Y) / 2;
        
        return new Vector2f(midpointX, midpointY);
    }

    public Vector2f HexagonPattern(CircleShape dot){
        //Hexagon shape made by the points
        CircleShape[] hexagonVertices = [specialPoints[1], specialPoints[3], specialPoints[5],
                                        specialPoints[7], specialPoints[9], specialPoints[11]];

        Random random = new Random();
        int randomPoint = random.Next(hexagonVertices.Count());

        Vector2f scaledVector = LineVector(hexagonVertices, randomPoint, dot, 0.667f);

        return dot.Position + scaledVector;
    }

    Vector2f LineVector(CircleShape[] specialPoints,int randomPoint, CircleShape dot,float sizeMultiplier){
        Vector2f lineVector = specialPoints[randomPoint].Position - dot.Position;
        return lineVector * sizeMultiplier;
    }
}