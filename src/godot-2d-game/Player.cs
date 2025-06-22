using Godot;

public partial class Player : Area2D
{
    public int Speed { get; set; } = 2800;

    public Vector2 ScreenSize;

    public override void _Ready()
    {
        // Get the screen size
        ScreenSize = GetViewport().GetVisibleRect().Size;
    }

    public override void _Process(double delta)
    {
        // Get the input direction
        var velocity = Vector2.Zero;
        if (Input.IsActionPressed("move_right"))
        {
            velocity.X += 1;
        }
        if (Input.IsActionPressed("move_left"))
        {
            velocity.X -= 1;
        }
        if (Input.IsActionPressed("move_down"))
        {
            velocity.Y += 1;
        }
        if (Input.IsActionPressed("move_up"))
        {
            velocity.Y -= 1;
        }

        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed * (float)delta;
            animatedSprite2D.Play();
        }
        else
        {
            animatedSprite2D.Stop();
        }

        if (velocity.X != 0)
        {
            animatedSprite2D.Animation = "walk";
            animatedSprite2D.FlipV = false;
            animatedSprite2D.FlipH = velocity.X < 0;
        }
        else if (velocity.Y != 0)
        {
            animatedSprite2D.Animation = "up";
            animatedSprite2D.FlipV = velocity.Y < 0;
        }

        Position += velocity * (float)delta;
        Position = new Vector2(
           x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
           y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );

    }
}
