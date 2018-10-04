using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public DialogObject dialog;

    private bool isTriggered = false;

    public void TriggerDialog()
    {
        if (!isTriggered)
        {
            FindObjectOfType<DialogManager>().StartDialog(dialog);
            isTriggered = true;
        }
    }
}