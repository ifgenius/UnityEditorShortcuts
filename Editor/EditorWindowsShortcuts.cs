using System;
using System.Collections.Generic;
using System.Linq;
using Shortcuts.Editor.Extensions;
using UnityEditor;
using static Shortcuts.Editor.Extensions.EditorWindowId;

namespace Shortcuts.Editor
{
  public static class EditorWindowsShortcuts
  {
    [MenuItem(Constants.BasePath + "Window/FocusLeft &[")]
    private static void FocusLeftWindow()
    {
      var editorWindow = EditorWindow.focusedWindow;
      var window = WindowNeighbours(editorWindow)?.PreviousOf(editorWindow);
      
      if (window != null) 
        window.Focus();
    }

    [MenuItem(Constants.BasePath + "Window/FocusRight &]")]
    private static void FocusRightWindow()
    {
      var editorWindow = EditorWindow.focusedWindow;
      var window = WindowNeighbours(editorWindow)?.NextOf(editorWindow);
      
      if (window != null) 
        window.Focus();
    }

    private static List<EditorWindow> WindowNeighbours(EditorWindow editorWindow)
    {
      var type = editorWindow.GetType();

      var parentHostView = type
        .Fields(editorWindow, x => x.AllInstance())
        .FirstOrDefault(x => x.Info.Name == "m_Parent");

      var panes = parentHostView.Value.GetType()
        .Fields(of: parentHostView.Value, x => x.AllInstance())
        .FirstOrDefault(x => x.Info.Name == "m_Panes");

      return panes.As<List<EditorWindow>>();
    }
    
    [MenuItem(Constants.BasePath + "Window/Toggle Active Window Lock ^#e")]
    private static void ToggleActiveWindowLock()
    {
      var editorWindow = EditorWindow.focusedWindow;
      if (editorWindow == null)
        return;

      var type = editorWindow.GetType();
      var editorWindowId = type.ToEditorWindowId();

      switch (editorWindowId)
      {
        case Hierarchy:
          ToggleLockedHierarchy(editorWindow, type);
          break;

        case Inspector:
        case Project:
          ToggleLocked(editorWindow, type);
          break;
      }

      editorWindow.Repaint();
    }
    
    private static void ToggleLockedHierarchy(EditorWindow window, Type windowType)
    {
      var sceneHierarchyField = windowType
        .Fields(of: window, x => x.AllInstance())
        .FirstOrDefault(x => x.Info.Name == "m_SceneHierarchy");

      var scene = sceneHierarchyField.Info;
      var isLockedProperty = scene.FieldType
        .Properties(of: sceneHierarchyField.Value, x => x.AllInstance())
        .FirstOrDefault(x => x.Info.Name == "isLocked");

      var isLocked = isLockedProperty.As<bool>();
      isLockedProperty.Set(!isLocked);
    }

    private static void ToggleLocked(EditorWindow editorWindow, Type windowType)
    {
      var isLockedProperty = windowType
        .Properties(of: editorWindow, x => x.AllInstance())
        .FirstOrDefault(x => x.Info.Name == "isLocked");

      var isLocked = isLockedProperty.As<bool>();
      isLockedProperty.Set(!isLocked);
    }
  }
}