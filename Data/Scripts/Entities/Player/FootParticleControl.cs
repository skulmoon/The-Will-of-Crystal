using Godot;
using System;

public partial class FootParticleControl : Node2D
{
    private Timer _emitingTimer;
    private Timer _disablingTimer; 

    [Export] public GpuParticles2D[] Particles { get; set; }

    public override void _Ready()
    {
        _emitingTimer = new Timer()
        {
            OneShot = true,
            Autostart = false,
        };
        _disablingTimer = new Timer()
        {
            OneShot = true,
            Autostart = false,
        };
        AddChild(_emitingTimer);
        AddChild(_disablingTimer);
        _emitingTimer.Timeout += () => Particles[1].Emitting = true;
        _disablingTimer.Timeout += () => Particles[1].Emitting = false;
        GetParent().GetParent<Player>().ChangedDirection += OnChangedDirection;
        base._Ready();
    }

    public void Emit()
    {
        Particles[0].Emitting = true;
        _emitingTimer.Start((Particles[0].Lifetime / (Particles[0].Amount * Particles[0].AmountRatio)) / 2);
    }

    public void Disabling()
    {
        Particles[0].Emitting = false;
        _disablingTimer.Start((Particles[0].Lifetime / (Particles[0].Amount * Particles[0].AmountRatio)) / 2);
    }

    public void OnChangedDirection(Vector2 direction)
    {
        if (direction != Vector2.Zero)
        {
            Emit();
            Rotation = direction.Angle() + Mathf.DegToRad(90);
            foreach (var particle in Particles)
                particle.SetInstanceShaderParameter("rotation", Rotation);
        }
        else
            Disabling();
        GD.Print(Particles[0].Emitting);
    }
}
