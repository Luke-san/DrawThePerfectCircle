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
        if (WaitForSpaceKey())
            DrawCircle();
    }

    [DllImport("user32.dll")]
    public static extern void SetCursorPos(int x, int y);

    public static void DrawCircle()
    {
        int centerX = 960;
        int centerY = 557;
        float radius = 200;

        for (float radians = 0f; radians < Math.PI * 2 + 0.5f; radians += 0.05f)
        {
            int x = (int)(centerX + (Math.Sin(radians) * radius));
            int y = (int)(centerY + (Math.Cos(radians) * radius));

            SetCursorPos(x, y);

            Thread.Sleep(1);
        }
    }

    public static bool WaitForSpaceKey()
    {
        while (!Keyboard.IsKeyDown(Key.Space))
        {
            // nothing lol
        }
        return true;
    }

    // For debuging
    public static void PrintCursorPos()
    {
        Point cursorPos;
        cursorPos = GetCursorPosition();
       Console.WriteLine($"X: {cursorPos.X}, Y:{cursorPos.Y}");
    }

    // totally my code
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT lpPoint);

    public static Point GetCursorPosition()
    {
        POINT lpPoint;
        GetCursorPos(out lpPoint);
        // NOTE: If you need error handling
        // bool success = GetCursorPos(out lpPoint);
        // if (!success)

        return lpPoint;
    }


}

// This below is also totally my code
[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int X;
    public int Y;

    public static implicit operator Point(POINT point)
    {
        return new Point(point.X, point.Y);
    }
}

