
using Content.Server.Chat.Systems;
using Content.Shared.Interaction;
using Content.Shared.Interaction.Events;

namespace Content.Server.Crashlanded.AnnounceOnUse;

public sealed class AnnounceOnUseSystem : EntitySystem
{
    [Dependency] private readonly ChatSystem _chatSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AnnounceOnUseComponent, ActivateInWorldEvent>(OnActivate);
    }

    private void OnActivate(EntityUid uid, AnnounceOnUseComponent component, ActivateInWorldEvent args)
    {
        if (component.Triggered) return;

        var message = component.Message;
        var color = component.Color;
        var global = component.Global;

        if (global)
        {
            _chatSystem.DispatchGlobalAnnouncement(message, colorOverride: color);
        }
        else
        {
            _chatSystem.DispatchStationAnnouncement(EntityManager.GetComponent<TransformComponent>(uid).ParentUid,
                message, colorOverride: color);
        }

        component.Triggered = true;
    }
}
