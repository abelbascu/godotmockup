using Godot;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public partial class PlayerInput : CharacterBody2D
{

	[Export]
	private float speed;
	private AnimationPlayer playerAnimations;
	private Sprite2D playerSprite;
	private bool isAnyKeyBeingPressed;
	private Vector2 inputDirection;
	private Vector2 motion = Vector2.Zero;


	public override void _Ready()
	{
		playerAnimations = GetNode<AnimationPlayer>("AnimationPlayer2");
		playerSprite = GetNode<Sprite2D>("PlayerAnims");
		//GD.Print(playerSprite.ToString()); 
		speed = 50;
	}

	public override void _PhysicsProcess(double delta)
	{
		//PlayerIsKeyPressedInput();
		//PlayerGetVectorInput();
		PlayerIsActionPressedInput();
	}


	public void PlayerIsActionPressedInput()
	{
		// Reset motion vector
		motion = Vector2.Zero;

		// Detect input for movement
		if (Input.IsActionPressed("up"))
			motion.Y -= 1;
		if (Input.IsActionPressed("down"))
			motion.Y += 1;
		if (Input.IsActionPressed("left"))
			motion.X -= 1;
		if (Input.IsActionPressed("right"))
			motion.X += 1;

		motion = motion.Normalized() * speed;
		Velocity = motion;

		// Set animations based on the direction of movement
		if (motion.Length() > 0)
		{
			playerAnimations.Play("walk_" + GetDirectionName(motion));
		}
		else
		{
			playerAnimations.Play("idle_sideways");
		}

		// Move the player
		MoveAndSlide();

	}

	private string GetDirectionName(Vector2 dir)
	{

		GD.Print(dir.X + " " + dir.Y);

		if (dir.X < 0)
		{
			if (dir.Y > 0) return "down";
			else if (dir.Y < 0) return "up";
			else
			{
				playerSprite.FlipH = true;
				return "right"; //no 'walk_left' in Animation.Player, we just flip 'right' sprite
			}
		}

		if (dir.X > 0)
		{
			if (dir.Y > 0) return "down";
			else if (dir.Y < 0) return "up";
			else
			{
				playerSprite.FlipH = false;
				return "right";
			}
		}

		else
		{
			if (dir.Y > 0) return "down";
			else if (dir.Y < 0) return "up";
			else return "idle_sideways";
		}
	}



	//THIS METHOD IS HALF-IMPLEMENTED DO NOT USE
	private void PlayerIsKeyPressedInput()
	{

		if (Input.IsKeyPressed(Key.Left))
		{
			this.Velocity = Vector2.Left * speed;
			playerAnimations.Play("walk_right");
			//flip sprite to face left, we reuse right animaation
			playerSprite.FlipH = true;
			MoveAndSlide();
		}

		if (Input.IsKeyPressed(Key.Right))
		{
			this.Velocity = Vector2.Right * speed;
			playerAnimations.Play("walk_right");
			//if was moving left, flip sprite to right again.
			playerSprite.FlipH = false;
			MoveAndSlide();
		}

		if (Input.IsKeyPressed(Key.Up))
		{
			this.Velocity = Vector2.Up * speed;
			playerAnimations.Play("walk_up");
			MoveAndSlide();
		}

		if (Input.IsKeyPressed(Key.Down))
		{
			this.Velocity = Vector2.Down * speed;
			playerAnimations.Play("walk_down");
			MoveAndSlide();
		}

		isAnyKeyBeingPressed = Input.IsKeyPressed(Key.Right) || Input.IsKeyPressed(Key.Down) || Input.IsKeyPressed(Key.Up) || Input.IsKeyPressed(Key.Left);

		if (isAnyKeyBeingPressed == false)
		{
			playerAnimations.Play("idle_down");
			GD.Print(Velocity.ToString());
		}

		//GD.Print(isAnyKeyBeingPressed);
		//GD.Print(Input.IsKeyPressed(Key.Right) && Input.IsKeyPressed(Key.Down));

	}


	//THIS METHOD IS HALF-IMPLEMENTED DO NOT USE
	private void PlayerGetVectorInput()
	{

		inputDirection = Input.GetVector("left", "right", "up", "down");
		//GD.Print(inputDirection);
		Velocity = inputDirection * speed;
		//string inputDirectionTemp = inputDirection.ToString();
		//GD.Print(inputDirectionTemp);


		if (inputDirection.IsEqualApprox(Vector2.Down))
		{
			playerAnimations.Play("walk_down");
			MoveAndSlide();
		}

		if (inputDirection.IsEqualApprox(Vector2.Up))
		{
			playerAnimations.Play("walk_up");
			MoveAndSlide();
		}

		if (inputDirection.IsEqualApprox(Vector2.Right))
		{
			playerAnimations.Play("walk_right");
			playerSprite.FlipH = false;
			MoveAndSlide();
		}

		if (inputDirection.IsEqualApprox(Vector2.Left))
		{
			playerAnimations.Play("walk_right");
			playerSprite.FlipH = true;
			MoveAndSlide();
		}

		if (inputDirection.IsEqualApprox(Vector2.Zero))
		{
			playerAnimations.Play("idle_down");
			MoveAndSlide();
		}

	}

}
