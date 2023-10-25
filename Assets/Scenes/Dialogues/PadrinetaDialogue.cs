using Godot;
using System;
using DialogueManagerRuntime;

public partial class EscenaDialogoDePrueba : Node2D
{
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var dialogue = GD.Load<Resource>("res://Assets/Dialogues/PadrinetaDialogue.dialogue");

		if (Input.IsActionPressed("action"))
		{
			DialogueManager.ShowExampleDialogueBalloon(dialogue, "start");
		}
	}


}
