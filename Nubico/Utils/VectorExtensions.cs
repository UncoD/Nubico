﻿using Box2DX.Common;
using SFML.System;

namespace Nubico.Utils;

public static class VectorExtensions
{
    public static Vector2f ToVector(this Vec2 vector)
    {
        return new Vector2f(vector.X, vector.Y);
    }
    
    public static Vec2 ToVec(this Vector2f vector)
    {
        return new Vec2(vector.X, vector.Y);
    }

    public static float GetDegreesAngle(this Vector2f vector)
    {
        return (float)(System.Math.Atan2(vector.Y, vector.X) * (180 / System.Math.PI));
    }
}