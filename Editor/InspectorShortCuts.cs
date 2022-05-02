using System.Linq;
using Shortcuts.Editor.Extensions;
using UnityEditor;
using static Shortcuts.Editor.Extensions.EditorWindowId;
using static UnityEditor.InspectorMode;

namespace Shortcuts.Editor
{
  public static class InspectorShortCuts
  {
    [MenuItem(Constants.BasePath + "Inspector/Toggle Mode ^#d")]
    private static void ToggleInspectorMode()
    {
      var editorWindow = EditorWindow.focusedWindow;
      var type = editorWindow.GetType();

      if (type.ToEditorWindowId() != Inspector)
        return;

      var inspectorModeProperty = type
        .Fields(editorWindow, x => x.AllInstance())
        .FirstOrDefault(x => x.Info.Name == "m_InspectorMode");

      var setModeMethod = type
        .Methods(x => x.AllInstance())
        .FirstOrDefault(x => x.Name == "SetMode");

      var mode = inspectorModeProperty.As<InspectorMode>();
      mode = mode == Normal
        ? Debug
        : Normal;

      setModeMethod?.Invoke(editorWindow, new object[] { mode });
      editorWindow.Repaint();
    }
  }
}