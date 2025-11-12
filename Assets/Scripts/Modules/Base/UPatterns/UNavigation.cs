using System;
using System.Collections.Generic;

public static class UNavigation
{
    private static Stack<Action> stack = new();
    public static void Back() => stack.Pop()?.Invoke();
    public static void Push(Action action) => stack.Push(action);
    public static void Reset() => stack.Clear();
}

public class UNavigationLocal
{
    private Stack<Action> stack = new();
    public void Back() => stack.Pop()?.Invoke();
    public void Push(Action action) => stack.Push(action);
    public void Reset() => stack.Clear();
}