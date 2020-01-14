using System;

public class CommandManager
{
    private static CommandManager commandManager = new CommandManager();
    private static bool _canControl;
    private CommandManager()
    {
        _canControl = true;
    }
    public static CommandManager getInstance()
    {
        return commandManager;
    }
    public static void ChangeControl(bool canControl)
    {
        _canControl = canControl;
    }
    public static bool getCanControl()
    {
        return _canControl;
    }
}
