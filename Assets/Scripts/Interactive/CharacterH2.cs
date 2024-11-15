using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色对话互动
/// </summary>
[RequireComponent(typeof(DialogueController))]
public class CharacterH2 : Interactive
{
    private DialogueController dialogueController;

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }

    public override void EmptyClick()
    {
        if (isDone)
        {
            dialogueController.ShowDialogueFinish();
        }else
        {
            dialogueController.ShowDialogueEmpty();
        }
    }

    protected override void OnClickAction()
    {
        dialogueController.ShowDialogueFinish();
    }
}

