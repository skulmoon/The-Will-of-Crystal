
using Godot;
using System;
using static Godot.TextServer;

public partial class Player : NPC
{
    private PlayerInteractionArea _interactionArea;
    private float _stamina = 0;
    private Vector2 _currentDirection;
    private float _currentSpeedMultiper;
    private Timer _walkTimer;

    public AnimatedSprite2D Sprite { get; private set; }
    public ShardManager Shard { get; private set; }
    public Camera2D Camera { get; private set; }
    public HitBox HitBox { get; private set; }
    public float Stamina 
    { 
        get => _stamina;
        set
        {
            _stamina = value;
            ChangedPower?.Invoke(_stamina);
        }
    }
    [Export] public float MaxStamina { get; set; } = 100;
    [Export] public int PlayerSpeed { get; set; } = 7000;
    [Export] public float Acceleration { get; set; } = 2;

    public event Action<float> ChangedPower;
    public event Action<Vector2> ChangedDirection;
    public event Action<float> ChangedSpeedMultiper;

    public override void _Ready()
    {
        _walkTimer = new Timer()
        {
            Autostart = false,
            OneShot = false,
            WaitTime = 0.5f
        };
        AddChild(_walkTimer);
        _walkTimer.Timeout += () => Global.Music.PlaySound("GolemRottenGrassWalk.ogg", 0.2f);
        ChangedDirection += OnChangedDirection;
        ChangedSpeedMultiper += OnChangedSpeedMultiper;
        Global.CutSceneManager.StartedCutScene += OnStartedCutScene;
        Sprite = GetNode<AnimatedSprite2D>("Sprite2D");
        _interactionArea = GetNode<PlayerInteractionArea>("PlayerInteractionArea");
        HitBox = GetNode<HitBox>("HitBox");
        Camera = GetNode<Camera2D>("Camera");
        Shard = new ShardManager(this);
        AddChild(Shard);
        Stamina = Global.Settings.SaveData.Stamina;
        HitBox.Health = Global.Settings.SaveData.Health;
        Global.SceneObjects.Player = this;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!Global.Settings.CutScene)
            Move(delta);
    }

    private void Move(double delta)
    {
        float speedMultiper = 1;
        Vector2 direction = new Vector2(Input.GetAxis("left", "right"), Input.GetAxis("up", "down")).Normalized();
        if (Input.IsActionPressed("acceleration") && Stamina - (float)delta > 0 && direction != Vector2.Zero)
        {
            speedMultiper *= Acceleration;
            Stamina -= (float)delta * 40;
        }
        else if (!Input.IsActionPressed("acceleration") && Stamina < MaxStamina)
            Stamina += (float)delta * 60;
        Velocity = direction * PlayerSpeed * speedMultiper * (float)delta;
        MoveAndSlide();
        if (_currentDirection != direction)
        {
            ChangedDirection?.Invoke(direction);
            _currentDirection = direction;
        }
        if (_currentSpeedMultiper != speedMultiper)
        {
            ChangedSpeedMultiper?.Invoke(speedMultiper);
            _currentSpeedMultiper = speedMultiper;
        }
    }  

    public void OnStartedCutScene() =>
        ChangedDirection.Invoke(Vector2.Zero);

    public void OnChangedSpeedMultiper(float speedMultiper) =>
        _walkTimer.WaitTime = 0.5f / speedMultiper;

    public void OnChangedDirection(Vector2 direction)
    {
        if (direction != Vector2.Zero)
        {
            if (_walkTimer.TimeLeft == 0)
            {
                Global.Music.PlaySound("GolemRottenGrassWalk.ogg", 0.2f);
                _walkTimer.Start();
            }
        }
        else
            _walkTimer.Stop();
    }

    public override void _ExitTree()
    {
        ChangedSpeedMultiper -= OnChangedSpeedMultiper;
        Global.CutSceneManager.StartedCutScene -= OnStartedCutScene;
        ChangedDirection -= OnChangedDirection;
        base._ExitTree();
    }
}
