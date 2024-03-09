using UnityEngine;
using UnityEngine.UI;

public class SubmitButton : MonoBehaviour
{
    public Coll teleportationScript;

    private Button submitButton;

    private void Start()
    {
        submitButton = GetComponent<Button>();
        submitButton.onClick.AddListener(SubmitAnswer);
    }

    private void SubmitAnswer()
    {
        teleportationScript.CheckAnswer();
    }
}

