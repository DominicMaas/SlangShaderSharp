namespace SlangShaderSharp;

/// <summary>
///     Used to determine what kind of path is required from an input path
/// </summary>
public enum PathKind
{
    /// <summary>
    ///     Given a path, returns a simplified version of that path.
    ///     This typically means removing '..' and/or '.' from the path.
    ///     A simplified path must point to the same object as the original.
    /// </summary>
    Simplified,

    /// <summary>
    ///     Given a path, returns a 'canonical path' to the item.
    ///     This may be the operating system 'canonical path' that is the unique path to the item.
    ///
    ///     If the item exists the returned canonical path should always be usable to access the
    ///     item.
    ///
    ///     If the item the path specifies doesn't exist, the canonical path may not be returnable
    ///     or be a path simplification.
    ///     Not all file systems support canonical paths.
    /// </summary>
    Canonical,

    /// <summary>
    ///     Given a path returns a path such that it is suitable to be displayed to the user.
    ///
    ///     For example if the file system is a zip file - it might include the path to the zip
    ///     container as well as the path to the specific file.
    ///
    ///     NOTE! The display path won't necessarily work on the file system to access the item
    /// </summary>
    Display,

    /// <summary>
    ///     Get the path to the item on the *operating system* file system, if available.
    /// </summary>
    OperatingSystem,

    CountOf,
};