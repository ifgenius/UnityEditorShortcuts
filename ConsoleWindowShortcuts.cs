using System;
using System.Reflection;
using UnityEditor;
using static System.Reflection.BindingFlags;

namespace Shortcuts
{
  public static class ConsoleWindowShortcuts
  {
    [MenuItem("Tools/Console/Clear")]
    public static void Clear()
    {
      var assembly = Assembly.GetAssembly(typeof(Editor));
      var type = assembly.GetType("UnityEditor.LogEntries");
      var method = type.GetMethod("Clear");
      method?.Invoke(new object(), null);
    }

    [MenuItem("Tools/Console/ToggleLogs")]
    public static void ToggleLog() =>
      ToggleFlag("LogLevelLog");

    [MenuItem("Tools/Console/ToggleWarnings")]
    public static void ToggleWarning() =>
      ToggleFlag("LogLevelWarning");

    [MenuItem("Tools/Console/ToggleErrors")]
    public static void ToggleErrors() =>
      ToggleFlag("LogLevelError");

    private static void ToggleFlag(string flagName)
    {
      var assembly = Assembly.GetAssembly(typeof(Editor));
      var consoleWindowType = assembly.GetType("UnityEditor.ConsoleWindow");
      var consoleFlagsType = assembly.GetType("UnityEditor.ConsoleWindow+ConsoleFlags");

      var flagType = consoleFlagsType.GetField(flagName);
      var flag = flagType?.GetValue(consoleFlagsType);

      var consoleWindow = EditorWindow.GetWindow(consoleWindowType);
      var setFlagMethod = consoleWindowType.GetMethod("SetFlag", NonPublic | Instance | Static);
      var hasFlagMethod = consoleWindowType.GetMethod("HasFlag", NonPublic | Instance | Static);

      var hasFlag = hasFlagMethod?.Invoke(consoleWindowType, new object[] { flag });

      setFlagMethod?.Invoke(consoleWindow, new object[] { flag, !Convert.ToBoolean(hasFlag) });
    }
  }
}