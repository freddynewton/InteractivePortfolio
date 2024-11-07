/// <summary>
/// Enum representing the names and indices of scenes in the project.
/// </summary>
public enum SceneName
{
    /// <summary>
    /// The initial boot scene, typically used for loading resources or initializing systems.
    /// </summary>
    BootScene = 0,

    /// <summary>
    /// The main menu scene, where players start and navigate through different game options.
    /// </summary>
    MainMenu = 1,

    /// <summary>
    /// A non-interactive scene, possibly used for displaying information or passive content.
    /// </summary>
    NonInteractiveScene = 2,

    /// <summary>
    /// An interactive scene for XR (Extended Reality) experiences, supporting player interaction.
    /// </summary>
    InteractiveXrScene = 3,
}
