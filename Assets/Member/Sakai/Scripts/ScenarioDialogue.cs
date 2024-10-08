using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScenarioDialogue : MonoBehaviour
{
    public Text dialogueText; // 会話内容を表示するTextオブジェクト
    public Dialogue currentDialogue; // 現在表示中の会話データ
    private int currentLineIndex; // 現在の会話行のインデックス

    SceneManagement sceneManagement;

    SampleSoundManager soundManager;
    void Start()
    {
        soundManager = FindObjectOfType<SampleSoundManager>();
        gameObject.SetActive(true); // 初期状態で非表示に設定
      sceneManagement =   FindObjectOfType<SceneManagement>();
        if(soundManager != null)
        {
            soundManager.PlayBgm(BgmType.BGM2);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // 会話開始時に表示
        DisplayLine();
    }
  
    void Update()
    {
        // タップを検知し、次の会話行に進む
        if (Input.GetMouseButtonDown(0)) // 左クリックまたはタップ
        {
            NextLine();
        }
     
    }

    public void NextLine()
    {
     
            DisplayLine();
            currentLineIndex++;
     
    }
    public void DisplayLine()
    {
        if (currentLineIndex < currentDialogue.lines.Count)
        {
            var line = currentDialogue.lines[currentLineIndex];
            dialogueText.text = line.dialogueText; // 会話内容のみ表示
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        sceneManagement.OnGame();
    }

}