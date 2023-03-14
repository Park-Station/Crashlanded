namespace Content.Server.Crashlanded.AnnounceOnUse;

[RegisterComponent]

/// <summary>
/// Announces a message upon being used.
/// </summary>
public sealed class AnnounceOnUseComponent : Component
{
    /// <summary>
    /// The message to announce.
    /// </summary>
    [DataField("message")]
    public string Message = String.Empty;

    /// <summary>
    /// Whether to announce globally or on-grid.
    /// </summary>
    [DataField("global")]
    public bool Global = false;

    /// <summary>
    /// The color of the announcement.
    /// </summary>
    [DataField("color")]
    public Color Color = Color.White;

    /// <summary>
    /// Whether the announcement has already been tripped.
    /// </summary>
    public bool Triggered = false;
}
