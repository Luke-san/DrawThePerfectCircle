using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;
using System.Windows;

public class CursorController
{
    [STAThread]
    static void Main()
    {
        while (true)
        {
            if (Keyboard.IsKeyDown(Key.Space))
                DrawCircle();
        }
    }

    public static void DrawCircle()
    {
        int centerX = 961;
        int centerY = 556;
        float radius = 250;

        int x = (int)(centerX + (Math.Sin(0f) * radius));
        int y = (int)(centerY + (Math.Cos(0f) * radius));
        Mouse.SetPos(x, y);

        Mouse.MouseInput(Mouse.MouseEvent.LeftDown);

        for (double radians = 0; radians < Math.PI * 2 + 0.5; radians += 0.1)
        {
            x = (int)(centerX + (Math.Sin(radians) * radius));
            y = (int)(centerY + (Math.Cos(radians) * radius));

            Mouse.SetPos(x, y);

            Thread.Sleep(1);
        }

        Mouse.MouseInput(Mouse.MouseEvent.LeftUp);
    }

    public static bool WaitForSpaceKey()
    {
        while (!Keyboard.IsKeyDown(Key.Space))
        {
            // nothing lol
        }
        return true;
    }
}


/*
 * Tried A custom equation in hope of fixing the waving issue.
 * Equation works but waving issue is still present
 * 
public class Circle
{
    public double radius;
    Vector2 centerPos;

    public Circle(double radius, double centerX, double centerY)
    {
        this.radius = radius;
        this.centerPos = new Vector2(centerX, centerY);
    }

    public Vector2 GetPosFromRads(double angle)
    {
        double PI = Math.PI;
        double polarity = -1;

        if (angle > Math.PI)
        {
            int loops = (int)Math.Floor(angle / PI);
            angle = angle - PI * loops;

            if ((loops & 1) == 1)
                polarity = 1;
        }

        double ang = Math.Sin((PI / 2) - angle) * radius;
        double x = centerPos.x + polarity * ang;
        double y = centerPos.y + polarity * Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(ang, 2));

        return new Vector2(x, y);
    }
}

public struct Vector2
{
    public double x;
    public double y;

    public Vector2(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}
*/