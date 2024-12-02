using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class CSVRerderTrueEnd : MonoBehaviour
{
    [SerializeField]
    private TrueEndScenario siunario;

    //CSVファイルの読み込み
    [SerializeField]
    private List<Question> _questions = new List<Question>();
    public TextAsset csvFile;        // GUIでcsvファイルを割当
    List<string[]> csvDatas = new List<string[]>();
    [SerializeField]
    public Text text;

    public List<Question> Questions => _questions;
    void Awake()
    {

        // 格納
        var lines = csvFile.text.Replace("\r\n", "\n").Split('\n').ToList();
        //lines = csvFile.text.Replace("/", "\n").Split('\n').ToList();
        lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

        //二次元配列にして呼び出しやすように
        foreach (var line in lines)
        {
            if (line == "") { continue; }
            var question = new Question();
            var q = line.Split(',');
            question.name = q[0];
            question.bun = q[1].Replace("/", "\n");
            question.move = q[2];
            _questions.Add(question);
        }
        //textDisplay.text = csvDatas[0][0].ToString()

    }
    public Question GetQuestion()
    {
        var q = _questions[siunario.math];
        return q;

    }
}
