using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandBase
{
    public string id { get; set; }
    public string description { get; set; }
    public string format { get; set; }

    public DebugCommandBase(string id, string description, string format)
    {
        this.id = id;
        this.description = description;
        if (string.IsNullOrWhiteSpace(format))
        {
            this.format = ("" + id);
        }
        else
        {
            this.format = id + " " + format.Trim();
        }
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action command;
    public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        this.command.Invoke();
    }
}

public class DebugCommand<T> : DebugCommandBase
{
    private Action<T> command;
    public DebugCommand(string id, string description, string format, Action<T> command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke(T value)
    {
        this.command.Invoke(value);
    }
}
