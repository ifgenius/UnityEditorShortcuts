using System.Collections.Generic;
using System.Linq;

namespace Shortcuts.Editor.Extensions
{
  public static class ListExtensions
  {
    public static T NextOf<T>(this IList<T> self, T item)
    {
      var index = self.IndexOf(item);
      return self.ElementAtOrDefault(index + 1);
    }

    public static T PreviousOf<T>(this IList<T> self, T item)
    {
      var index = self.IndexOf(item);
      return self.ElementAtOrDefault(index - 1);
    }
  }
}