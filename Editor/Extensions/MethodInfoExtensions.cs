// Original sources was taken from https://github.com/joshua-light/pocket.net

using System.Reflection;

namespace Shortcuts.Editor.Extensions
{
  /// <summary>
  ///     Represents extension-methods for <see cref="MethodInfo"/>.
  /// </summary>
  public static class MethodInfoExtensions
  {
    /// <summary>
    ///   Gets arguments of specified method.
    /// </summary>
    /// <param name="self"><code>this</code> object.</param>
    /// <returns>An array of <see cref="ParameterInfo"/> that represent arguments of method.</returns>
    public static ParameterInfo[] Arguments(this MethodInfo self) =>
      self.GetParameters();
    
    public static bool HasOneArgument(this MethodInfo self) =>
      self.Arguments().Length == 1;
  }
}