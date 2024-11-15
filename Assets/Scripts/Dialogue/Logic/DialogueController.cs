using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对话控制器
/// </summary>
public class DialogueController : MonoBehaviour
{
    public DialogueData_SO dialogueEmpty;
    public DialogueData_SO dialogueFinish;

    private Stack<string> dialogueEmptyStack;
    private Stack<string> dialogueFinishStack;

    private bool isTalking; //是否正在在说话

    private void Awake()
    {
        FillDialogueStack();
    }

    private void FillDialogueStack()
    {
        dialogueEmptyStack = new Stack<string>();
        dialogueFinishStack = new Stack<string>();

        for (int i = dialogueEmpty.dialogueList.Count - 1; i >= 0; i--)
        {
            dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
        }
        for (int i = dialogueFinish.dialogueList.Count - 1; i >= 0; i--)
        {
            dialogueFinishStack.Push(dialogueFinish.dialogueList[i]);
        }
    }

    public void ShowDialogueEmpty()
    {
        if (!isTalking)
            StartCoroutine(DialogueRoutine(dialogueEmptyStack));
    }

    public void ShowDialogueFinish()
    {
        if (!isTalking)
            StartCoroutine(DialogueRoutine(dialogueFinishStack));
    }

    private IEnumerator DialogueRoutine(Stack<string> stack)
    {
        isTalking = true;
        if(stack.TryPop(out string result)) //出栈
        {
            EventHandler.CallShowDialogueEvent(result);
            yield return null;
            isTalking=false;
            EventHandler.CallGameStateChangeEvent(E_GameState.Pause);
        }
        else
        {
            //无话可说时
            EventHandler.CallShowDialogueEvent(string.Empty);
            FillDialogueStack();    //重新填充
            isTalking = false;
            EventHandler.CallGameStateChangeEvent(E_GameState.GamePlay);
        }
    }

}
