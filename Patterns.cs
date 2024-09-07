using SFML.Graphics;
using SFML.System;
using VectorCalculations;
class Patterns{
    CircleShape[] specialPoints;
    
    public Patterns(){
        specialPoints = Application.specialPoints;
    }

    public Vector2f[] TriangleVertices {
        get {
            return [specialPoints[3].Position, specialPoints[7].Position, specialPoints[11].Position];
        }

        private set {}
    }

    public Vector2f[] Dodecagon {
        get {
            List<Vector2f> points = new List<Vector2f>();
            foreach(CircleShape position in specialPoints){
                points.Add(position.Position);
            }

            return points.ToArray();
        }

        private set {}
    }

    public Vector2f[] HexagonVertices {
        get {
            return [specialPoints[1].Position, specialPoints[3].Position, specialPoints[5].Position,
                    specialPoints[7].Position, specialPoints[9].Position, specialPoints[11].Position];
        }

        private set {}
    }


    public Vector2f DodecagonPattern(CircleShape dot){
        Random random = new Random();
        int randomPoint = random.Next(specialPoints.Count());

        Vector2f scaledVector = Points.LineVector(specialPoints, randomPoint, dot, 0.789f);

        return dot.Position + scaledVector;
    }

    public Vector2f TrianglePattern(CircleShape dot){
        Random random = new Random();
        int randomPoint = random.Next(TriangleVertices.Count());

        return Points.MidPoint(dot.Position, TriangleVertices[randomPoint]);
    }

    public Vector2f HexagonPattern(CircleShape dot){
        CircleShape[] hexagonVertices = [specialPoints[1], specialPoints[3], specialPoints[5],
                                        specialPoints[7], specialPoints[9], specialPoints[11]];

        Random random = new Random();
        int randomPoint = random.Next(hexagonVertices.Count());

        Vector2f scaledVector = Points.LineVector(hexagonVertices, randomPoint, dot, 0.667f);

        return dot.Position + scaledVector;
    }

    // public Vector2f CarpetPattern(CircleShape dot){

    // }
}