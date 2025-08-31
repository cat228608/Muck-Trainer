// GlobalKeyboardHook.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public sealed class GlobalKeyboardHook : IDisposable
{
    public event EventHandler<GlobalKeyboardHookEventArgs> KeyboardPressed;

    private readonly Dictionary<Keys, Action> _hookedKeys = new Dictionary<Keys, Action>();
    private IntPtr _user32LibraryHandle;
    private IntPtr _windowsHookHandle;
    private Win32.KeyboardHookProc _hookProc;

    public GlobalKeyboardHook()
    {
        _windowsHookHandle = IntPtr.Zero;
        _user32LibraryHandle = IntPtr.Zero;
        _hookProc = LowLevelKeyboardProc;

        _user32LibraryHandle = Win32.LoadLibrary("User32");
        if (_user32LibraryHandle == IntPtr.Zero)
        {
            int errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode, $"Failed to load library 'User32.dll'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
        }

        _windowsHookHandle = Win32.SetWindowsHookEx(Win32.WH_KEYBOARD_LL, _hookProc, _user32LibraryHandle, 0);
        if (_windowsHookHandle == IntPtr.Zero)
        {
            int errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode, $"Failed to adjust keyboard hooks for '{Process.GetCurrentProcess().ProcessName}'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
        }
    }

    public void HookKey(Keys key, Action action)
    {
        _hookedKeys[key] = action;
    }

    private IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            if (wParam == (IntPtr)Win32.WM_KEYDOWN || wParam == (IntPtr)Win32.WM_SYSKEYDOWN)
            {
                var keyboardHookStruct = (Win32.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32.KeyboardHookStruct));
                Keys key = (Keys)keyboardHookStruct.VirtualKeyCode;

                if (_hookedKeys.ContainsKey(key))
                {
                    _hookedKeys[key]?.Invoke();
                }

                KeyboardPressed?.Invoke(this, new GlobalKeyboardHookEventArgs(keyboardHookStruct, key));
            }
        }
        return Win32.CallNextHookEx(_windowsHookHandle, nCode, wParam, lParam);
    }

    public void Dispose()
    {
        if (_windowsHookHandle != IntPtr.Zero)
        {
            if (!Win32.UnhookWindowsHookEx(_windowsHookHandle))
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(errorCode, $"Failed to remove keyboard hooks for '{Process.GetCurrentProcess().ProcessName}'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
            }
            _windowsHookHandle = IntPtr.Zero;
            _hookProc -= LowLevelKeyboardProc;
        }

        if (_user32LibraryHandle != IntPtr.Zero)
        {
            if (!Win32.FreeLibrary(_user32LibraryHandle))
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(errorCode, $"Failed to unload library 'User32.dll'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
            }
            _user32LibraryHandle = IntPtr.Zero;
        }
    }

    ~GlobalKeyboardHook() => Dispose();
}

public class GlobalKeyboardHookEventArgs : HandledEventArgs
{
    public Win32.KeyboardHookStruct KeyboardData { get; }
    public Keys Key { get; }

    public GlobalKeyboardHookEventArgs(Win32.KeyboardHookStruct keyboardData, Keys key)
    {
        KeyboardData = keyboardData;
        Key = key;
    }
}

public static class Win32
{
    public const int WH_KEYBOARD_LL = 13;
    public const int WM_KEYDOWN = 0x100;
    public const int WM_SYSKEYDOWN = 0x104;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
    public static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool FreeLibrary(IntPtr hModule);

    public delegate IntPtr KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardHookStruct
    {
        public int VirtualKeyCode;
        public int ScanCode;
        public int Flags;
        public int Time;
        public IntPtr ExtraInfo;
    }
}