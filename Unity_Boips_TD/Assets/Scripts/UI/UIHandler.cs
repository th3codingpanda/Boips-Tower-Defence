using TMPro;
using UnityEngine;

public class UIHandler : MonoSingleton<UIHandler>
{
    public void ChangeUIText(TextMeshProUGUI TextToChange, string Text)
    {
        TextToChange.text = Text;
    }
}
