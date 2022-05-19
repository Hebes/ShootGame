using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Tool;

public class UI_InputWindow : SingletonMono_Temp<UI_InputWindow>
{
    private Button_UI okBtn;
    private Button_UI cancelBtn;
    private TextMeshProUGUI titleText;
    private TMP_InputField inputField;


    protected override void Awake()
    {
        base.Awake();
        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
        cancelBtn = transform.Find("cancelBtn").GetComponent<Button_UI>();
        titleText = transform.Find_Child<TextMeshProUGUI>("titleText");
        inputField = transform.Find("inputField").GetComponent<TMP_InputField>();

        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            okBtn.ClickFunc();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cancelBtn.ClickFunc();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show(string titleString, string inputString, string validCharacters, int characterLimit, Action onCancel, Action<string> onOk)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

        titleText.text = titleString;

        inputField.characterLimit = characterLimit;
        inputField.onValidateInput = (string text, int charIndex, char addedChar) =>
        {
            return ValidateChar(validCharacters, addedChar);
        };

        inputField.text = inputString;
        inputField.Select();

        okBtn.ClickFunc = () =>
        {
            Hide();
            onOk(inputField.text);
        };

        cancelBtn.ClickFunc = () =>
        {
            Hide();
            onCancel();
        };
    }

    private char ValidateChar(string validCharacters, char addedChar)
    {
        if (validCharacters.IndexOf(addedChar) != -1)
        {
            // Valid 有效
            return addedChar;
        }
        else
        {
            // Invalid 无效的
            return '\0';
        }
    }

    public void Show_Static(string titleString, string inputString, string validCharacters, int characterLimit, Action onCancel, Action<string> onOk)
    {
        print("进入");
        Show(titleString, inputString, validCharacters, characterLimit, onCancel, onOk);
    }

    public void Show_Static(string titleString, int defaultInt, Action onCancel, Action<int> onOk)
    {
        Show(titleString, defaultInt.ToString(), "0123456789-", 20, onCancel,
            (string inputText) =>
            {
                // Try to Parse input string
                if (int.TryParse(inputText, out int _i))
                    onOk(_i);
                else
                    onOk(defaultInt);
            }
        );
    }
}
