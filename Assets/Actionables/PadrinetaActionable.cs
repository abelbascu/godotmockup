using Godot;
using System;
using DialogueManagerRuntime;

public partial class PadrinetaActionable : Area2D
{

	[Export]
	public Resource dialogue;
	[Export]
	public string dialogueStart = "start";

	public void DialogueTrigger() 
	{
		DialogueManager.ShowExampleDialogueBalloon(dialogue, "start");
	}
	
}
