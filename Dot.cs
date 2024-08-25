using SFML.Graphics;
using SFML.System;

class Dot : CircleShape{
    public Dot(float radius) : base(radius){
        Origin = new Vector2f(radius, radius);
    }
}