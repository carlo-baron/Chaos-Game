using SFML.Graphics;
using SFML.System;
using VectorCalculations;
class Patterns
{
    RectangleShape squareBody;

    CircleShape circleBody;
    uint circleBodySize = 200;
    Vector2f[] specialPoints = new Vector2f[12];
    float[] angles = { 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330 };

    public Patterns()
    {
        squareBody = new RectangleShape(new Vector2f(400, 400));
        squareBody.Origin = FindCenter.Rectangle(squareBody.Size);
        squareBody.Position = FindCenter.Window();

        circleBody = new CircleShape(circleBodySize)
        {
            Origin = new Vector2f(circleBodySize, circleBodySize),
            Position = FindCenter.Window(),
        };

        for (int i = 0; i < specialPoints.Length; i++)
        {
            Vector2f newPointPosition = Points.SpecialPosition(circleBodySize, angles[i], circleBody);

            specialPoints[i] = newPointPosition;
        }

    }

    public Vector2f[] squareBodyPoints
    {
        get { return Points.RectangleVertices(squareBody); }
        private set { }
    }

    public Vector2f[] VicsekPoints
    {
        get
        {
            return VicsekPoints;
        }

        private set
        {
            List<Vector2f> squarePoints = squareBodyPoints.ToList();
            squarePoints.Add(squareBody.Position);
            VicsekPoints = squarePoints.ToArray();
        }
    }

    public Vector2f[] TriangleVertices
    {
        get
        {
            return [specialPoints[3], specialPoints[7], specialPoints[11]];
        }

        private set { }
    }

    public Vector2f[] Dodecagon
    {
        get
        {
            List<Vector2f> points = [.. specialPoints];

            return points.ToArray();
        }

        private set { }
    }

    public Vector2f[] HexagonVertices
    {
        get
        {
            return [specialPoints[1], specialPoints[3], specialPoints[5],
                    specialPoints[7], specialPoints[9], specialPoints[11]];
        }

        private set { }
    }

    public Vector2f DodecagonPattern(CircleShape dot)
    {
        Random random = new Random();
        int randomPoint = random.Next(specialPoints.Count());

        Vector2f scaledVector = Points.LineVector(specialPoints, randomPoint, dot, 0.789f);

        return dot.Position + scaledVector;
    }

    public Vector2f TrianglePattern(CircleShape dot)
    {
        Random random = new Random();
        int randomPoint = random.Next(TriangleVertices.Count());

        return Points.MidPoint(dot.Position, TriangleVertices[randomPoint]);
    }

    public Vector2f HexagonPattern(CircleShape dot)
    {
        // CircleShape[] hexagonVertices = [specialPoints[1], specialPoints[3], specialPoints[5],
        //                                 specialPoints[7], specialPoints[9], specialPoints[11]];

        Random random = new Random();
        int randomPoint = random.Next(HexagonVertices.Count());

        Vector2f scaledVector = Points.LineVector(HexagonVertices, randomPoint, dot, 0.667f);

        return dot.Position + scaledVector;
    }

    public Vector2f CarpetPattern(CircleShape dot){
        Random random = new Random();
        int randomPoint = random.Next(VicsekPoints.Count());

        Vector2f scaledVector = Points.LineVector(VicsekPoints, randomPoint, dot, 0.667f);

        return dot.Position + scaledVector;
    }
}