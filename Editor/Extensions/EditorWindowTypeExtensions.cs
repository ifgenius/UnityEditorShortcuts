using System;

namespace Shortcuts.Editor.Extensions
{
  internal static class EditorWindowTypeExtensions
  {
    internal static EditorWindowId ToEditorWindowId(this Type self)
    {
      switch (self.Name)
      {
        case "InspectorWindow":
          return EditorWindowId.Inspector;
        
        case "SceneHierarchyWindow":
          return EditorWindowId.Hierarchy;
        
        case "ProjectBrowser":
          return EditorWindowId.Project;
        
        default:
          return EditorWindowId.Unknown;
      }
    }
  }
}