using Content.Server.Explosion.EntitySystems;
using Content.Shared.Lock;

namespace Content.Server.Crashlanded.UnlockOnTrigger;

public sealed class UnlockOnTriggerSystem : EntitySystem
{
    [Dependency] private readonly LockSystem _lockSystem = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<UnlockOnTriggerComponent, TriggerEvent>(OnTrigger);
    }

    private void OnTrigger(EntityUid uid, UnlockOnTriggerComponent component, TriggerEvent args)
    {
        if (component.Triggered ||
            !EntityManager.TryGetComponent<LockComponent>(uid, out var lockComp)) return;

        _lockSystem.Unlock(uid, null, lockComp);

        component.Triggered = true;
    }
}
