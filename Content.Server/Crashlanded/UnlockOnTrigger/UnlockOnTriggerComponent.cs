namespace Content.Server.Crashlanded.UnlockOnTrigger;

[RegisterComponent]
public sealed class UnlockOnTriggerComponent : Component
{
    /// <summary>
    /// Whether the entity has already been unlocked.
    /// </summary>
    public bool Triggered = false;
}
