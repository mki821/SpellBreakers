using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class NicknameInputField : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TMP_InputField>().onValidateInput += Validate;
    }

    private char Validate(string text, int charIndex, char addedChar)
    {
        if (char.IsLetterOrDigit(addedChar) || (addedChar >= '\uAC00' && addedChar <= '\uD7AF'))
        {
            return addedChar;
        }

        return '\0';
    }
}
