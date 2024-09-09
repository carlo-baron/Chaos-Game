using SFML.Graphics;
using SFML.System;
using VectorCalculations;
class Patterns
{
    RectangleShape squareBody;

    CircleShape circleBody;
    uint circleBodySize = 200;
    CircleShape[] specialPoints = new CircleShape[12];
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
            specialPoints[i] = new CircleShape(5)
            {
                FillColor = Color.Green,
                Origin = new Vector2f(5, 5),
                Position = Points.SpecialPosition(circleBodySize, angles[i], circleBody)
            };
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
            return [specialPoints[3].Position, specialPoints[7].Position, specialPoints[11].Position];
        }

        private set { }
    }

    public Vector2f[] Dodecagon
    {
        get
        {
            List<Vector2f> points = new List<Vector2f>();
            foreach (CircleShape position in specialPoints)
            {
                points.Add(position.Position);
            }

            return points.ToArray();
        }

        private set { }
    }

    public Vector2f[] HexagonVertices
    {
        get
        {
            return [specialPoints[1].Position, specialPoints[3].Position, specialPoints[5].Position,
                    specialPoints[7].Position, specialPoints[9].Position, specialPoints[11].Position];
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