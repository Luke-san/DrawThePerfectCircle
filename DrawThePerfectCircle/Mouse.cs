using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;
using System.Windows;

public static class Mouse
{
    [DllImport("user32.dll")]
    private static extern void SetCursorPos(int x, int y);
    [DllImport("user32.dll")]
    private static extern uint SendInput(uint cInputs, Input[] pInput, int cbSize);
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);
    [DllImport("user32.dll")]
    private static extern IntPtr GetMessageExtraInfo();

    public static void SetPos(int x, int y)
    {
        SetCursorPos(x, y);
    }

    public static Point GetPos()
    {
        POINT lpPoint;
        GetCursorPos(out lpPoint);
        // NOTE: If you need error handling
        // bool success = GetCursorPos(out lpPoint);
        // if (!success)

        return lpPoint;
    }

    public static void MouseInput(MouseEvent mouseEvent)
    {
        Input[] input = new Input[]
        {
            new Input
            {
                inputType = (int)InputType.Mouse,
                inputUnion = new InputUnion
                {
                    mouseInput  = new MouseInput
                    {
                        dwFlags = (uint)mouseEvent,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            }
        };

        SendInput((uint)input.Length, input, Marshal.SizeOf(typeof(Input)));
    }

    // For debuging
    public static void PrintCursorPos()
    {
        Point cursorPos;
        cursorPos = GetPos();
        Console.WriteLine($"X: {cursorPos.X}, Y:{cursorPos.Y}");
    }

    [Flags]
    public enum MouseEvent
    {
        Absolute = 0x8000,
        HWheel = 0x01000,
        Move = 0x0001,
        MoveNoCoalesce = 0x2000,
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        VirtualDesk = 0x4000,
        Wheel = 0x0800,
        XDown = 0x0080,
        XUp = 0x0100
    }
}



// This below is also totally my code
[StructLayout(LayoutKind.Sequential)]
struct POINT
{
    public int X;
    public int Y;

    public static implicit operator Point(POINT point)
    {
        return new Point(point.X, point.Y);
    }
}

/*
    All below this comment was copied from the following site:
    https://www.codeproject.com/Articles/5264831/How-to-Send-Inputs-using-Csharp
*/
[StructLayout(LayoutKind.Sequential)]
public struct KeyboardInput
{
    public ushort wVk;
    public ushort wScan;
    public uint dwFlags;
    public uint time;
    public IntPtr dwExtraInfo;
}

[StructLayout(LayoutKind.Sequential)]
public struct MouseInput
{
    public int deltaX;
    public int deltaY;
    public uint mouseData;
    public uint dwFlags;
    public uint time;
    public IntPtr dwExtraInfo;
}

[StructLayout(LayoutKind.Sequential)]
public struct HardwareInput
{
    public uint uMsg;
    public ushort wParamL;
    public ushort wParamH;
}

[StructLayout(LayoutKind.Explicit)]
public struct InputUnion
{
    [FieldOffset(0)] public MouseInput mouseInput;
    [FieldOffset(0)] public KeyboardInput keyboardInput;
    [FieldOffset(0)] public HardwareInput hardwareInput;
}

public struct Input
{
    public int inputType;
    public InputUnion inputUnion;
}

[Flags]
public enum InputType
{
    Mouse = 0,
    Keyboard = 1,
    Hardware = 2
}

