using Content.Server.Explosion.EntitySystems;
using Content.Shared.Interaction;

namespace Content.Server.Crashlanded.OnClickTimerTrigger;

public sealed class OnClickTimerTriggerSystem : EntitySystem
{
    [Dependency] private readonly TriggerSystem _triggerSystem = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<OnClickTimerTriggerComponent, ActivateInWorldEvent>(OnActivate);
    }

    private void OnActivate(EntityUid uid, OnClickTimerTriggerComponent component, ActivateInWorldEvent args)
    {
        // if (args.Handled)
        //     return;

        _triggerSystem.HandleTimerTrigger(
            uid,
            args.User,
            component.Delay,
            component.BeepInterval,
            component.InitialBeepDelay,
            component.BeepSound,
            component.BeepParams);

        args.Handled = true;
    }
}
