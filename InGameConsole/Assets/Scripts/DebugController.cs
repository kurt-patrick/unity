using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField]
    private bool showConsole = true;
    bool showHelp;
    string input;
    string lastInput;

    public static DebugCommand MoveForward;
    public static DebugCommand MoveBackward;
    public static DebugCommand SpawnXyzPoint;
    public static DebugCommand<int> MaxAssets;
    public static DebugCommand Help;

    public List<DebugCommandBase> commandList;
    private PlayerMovement playerMovement;

    public void ToggleVisibility() => showConsole = !showConsole;

    public void OnReturn()
    {

    }

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        commandList = new List<DebugCommandBase>();

        MoveForward = new DebugCommand("move_forward", "move forward", null, () =>
        {
            playerMovement.ApplyTranslation(Vector3.forward);
        });

        MoveBackward = new DebugCommand("move_back", "move backward", null, () =>
        {
            playerMovement.ApplyTranslation(-Vector3.forward);
        });

        SpawnXyzPoint = new DebugCommand("spawn_xyz", "spawn xyz", "<shape> <colour>", () =>
        {
            // add
        });

        MaxAssets = new DebugCommand<int>("assets_max", "max assets displayed", "<int>", (int value) =>
        {
            // add
        });

        Help = new DebugCommand("help", "show list of help commands", null, () =>
        {
            showHelp = true;
        });

        commandList.AddRange(new DebugCommandBase[]
        {
            MoveForward, MoveBackward, SpawnXyzPoint, MaxAssets, Help
        });

    }

    Vector2 scroll;

    /// <summary>
    /// https://www.youtube.com/watch?v=VzOEM-4A2OM
    /// </summary>
    private void OnGUI()
    {
        //if (!showConsole) return;
        float y = 0f;
        IEnumerable<DebugCommandBase> debugCommandsStartingWith = new List<DebugCommandBase>();
        if (!string.IsNullOrWhiteSpace(input))
        {
            string firstWord = GetFirstWordBeforeWhiteSpace(input);
            debugCommandsStartingWith = commandList.Where(s => s.id.StartsWith(firstWord));
        }

        if (showHelp)
        {
            DrawHelpInfo(debugCommandsStartingWith, out float helpBoxHeight);
            y += helpBoxHeight;
        }

        input = DrawInputBox(input, y);

        if (Event.current.keyCode == KeyCode.Return)
        {
            if (!string.Equals(lastInput, input))
            {
                HandleInput(input);
                input = string.Empty;

            }
            lastInput = input;
        }

    }// OnGUI()

    private void DrawHelpInfo(IEnumerable<DebugCommandBase> debugCommandsStartingWith, out float helpBoxHeight)
    {
        const float y = 0f;
        const float RowHeight = 20f;
        helpBoxHeight = Math.Min(RowHeight * commandList.Count, RowHeight * 3);
        if (debugCommandsStartingWith.Count() == 1)
        {
            helpBoxHeight = RowHeight;
        }

        List<DebugCommandBase> commandsToList = new List<DebugCommandBase>();
        if (debugCommandsStartingWith == null || debugCommandsStartingWith.Count() == 0)
        {
            commandsToList.AddRange(commandList);
        }
        else
        {
            commandsToList.AddRange(debugCommandsStartingWith);
        }

        // limit the height of the box to 3 rows
        GUI.Box(new Rect(0f, y, Screen.width, helpBoxHeight), string.Empty);

        // the height of the viewport is the total height for all rows
        Rect viewport = new Rect(0, 0, Screen.width - 30f, RowHeight * commandsToList.Count);

        // create a scroll view with max of 3 rows and a viewport that can scroll through all items
        scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, helpBoxHeight), scroll, viewport);


        for (int i = 0; i < commandsToList.Count; i++)
        {
            DebugCommandBase cmd = commandsToList[i];
            string label = $"{cmd.format} - {cmd.description}";
            Rect labelRect = new Rect(5f, RowHeight * i, viewport.width - 100f, RowHeight);
            GUI.Label(labelRect, label);
        }
        GUI.EndScrollView();

    }// DrawHelpInfo()

    private string DrawInputBox(string input, float y)
    {
        GUI.Box(new Rect(0f, y, Screen.width, 30f), string.Empty);
        GUI.backgroundColor = new Color(0f, 0f, 0f, 0f);
        return GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    private static string[] SplitSentenceOnWhiteSpace(string sentence)
    {
        string[] args = ("" + sentence).Trim().Split(
            new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
        return args;
    }

    private static string GetFirstWordBeforeWhiteSpace(string sentence)
    {
        return SplitSentenceOnWhiteSpace(sentence).First();
    }

    private void HandleInput(string input)
    {
        string[] args = SplitSentenceOnWhiteSpace(input);

        for (int i=0; i<commandList.Count; i++)
        {
            if (args.First().Equals(commandList[i].id))
            {
                if (commandList[i] is DebugCommand debugCommand)
                {
                    Debug.Log(args.First());
                    debugCommand.Invoke();

                    if (! "help".Equals(debugCommand.id))
                    {
                        //showHelp = false;
                    }

                }
                else if (commandList[i] is DebugCommand<int> debugCommandInt)
                {
                    if (args.Length >= 2)
                    {
                        if (Int32.TryParse(args[1], out int maxAssets))
                        {
                            Debug.Log($"{args.First()} {maxAssets}");
                            debugCommandInt.Invoke(maxAssets);
                        }
                    }
                }

            }
        }
    }

}
