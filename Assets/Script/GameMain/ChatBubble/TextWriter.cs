using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWriter : SingletonMono_Continue<TextWriter>
{
    private List<TextWriterSingle> textWriterSingleList = new List<TextWriterSingle>();

    /// <summary>
    /// 显示TextMeshPro的
    /// </summary>
    /// <param name="textMeshPro"></param>
    /// <param name="textToWrite"></param>
    /// <param name="timePerCharacter"></param>
    /// <param name="invisibleCharacters"></param>
    /// <param name="removeWriterBeforeAdd"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public TextWriterSingle AddWriter_Static(TextMeshPro textMeshPro, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd, Action onComplete)
    {
        if (removeWriterBeforeAdd) RemoveWriter(textMeshPro);
        return AddWriter(textMeshPro, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
    }

    private TextWriterSingle AddWriter(TextMeshPro textMeshPro, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
    {
        TextWriterSingle textWriterSingle 
            = new TextWriterSingle(textMeshPro, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    /// <summary>
    /// 显示Text的
    /// </summary>
    /// <param name="uiText"></param>
    /// <param name="textToWrite"></param>
    /// <param name="timePerCharacter"></param>
    /// <param name="invisibleCharacters">是否显示空字符串填充,保持其他的文字显示正确位置</param>
    /// <param name="removeWriterBeforeAdd">添加之前删除写入器</param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public TextWriterSingle AddWriter_Static(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd, Action onComplete)
    {
        if (removeWriterBeforeAdd) RemoveWriter(uiText);
        return AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
    }

    private TextWriterSingle AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
    {
        TextWriterSingle textWriterSingle 
            = new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public void RemoveWriter_Static(Text uiText) => RemoveWriter(uiText);

    public void RemoveWriter_Static(TextMeshPro textMeshPro) => RemoveWriter(textMeshPro);

    private void RemoveWriter(Text uiText)
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            if (textWriterSingleList[i].GetUIText == uiText)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    private void RemoveWriter(TextMeshPro textMeshPro)
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            if (textWriterSingleList[i].GetTextMeshPro == textMeshPro)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    /// <summary>
    /// 更新不同文本的显示，同时显示多个对话
    /// </summary>
    private void Update()
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance)
            {
                textWriterSingleList.RemoveAt(i);
                i--;//跳过索引
            }
        }
    }

    //表示一个TextWriter实例
    public class TextWriterSingle
    {

        private Text uiText;
        private TextMeshPro textMeshPro;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;
        private Action onComplete;

        public TextWriterSingle(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            this.onComplete = onComplete;
            characterIndex = 0;
        }

        public TextWriterSingle(TextMeshPro textMeshPro, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
        {
            this.textMeshPro = textMeshPro;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            this.onComplete = onComplete;
            characterIndex = 0;
        }

        // 完成时返回true
        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)//用while是为了保证很低的帧率也能快速显示
            {
                // 显示下一个字符串
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);

                //添加这个为了显示可以看见的字符串显示在正确位置
                if (invisibleCharacters) text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";

                if (uiText != null)
                    uiText.text = text;
                else
                    textMeshPro.SetText(text);

                // 全部的字符串显示完成
                if (characterIndex >= textToWrite.Length)
                {
                    onComplete?.Invoke();
                    return true;
                }
            }
            return false;
        }

        public Text GetUIText => uiText;
        public TextMeshPro GetTextMeshPro => textMeshPro;
        public bool IsActive => characterIndex < textToWrite.Length;//是否显示完成

        public void WriteAllAndDestroy()
        {
            characterIndex = textToWrite.Length;
            onComplete?.Invoke();

            if (uiText != null)
            {
                uiText.text = textToWrite;
                Instance.RemoveWriter_Static(uiText);
            }
            else
            {
                uiText.text = textToWrite;
                Instance.RemoveWriter_Static(textMeshPro);
            }
        }
    }
}
