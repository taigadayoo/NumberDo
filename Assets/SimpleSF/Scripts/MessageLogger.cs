using ScenarioFlow;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MessageLogger : IReflectable
{
    [CommandMethod("log message")]
    public void LogMessage(string message)
    {
        Debug.Log(message);

        //// 特定のメッセージに基づいてシーン遷移
        //if (message.Contains("end"))
        //{
        //    string sceneName = message.Split(':')[1].Trim();
        //    SceneManager.LoadScene(sceneName);
        //}
    }

    [CommandMethod("log end")]
    public void LogEnd(bool isEnd)
    {
        Debug.Log(isEnd ? true : false);
        SceneManager.LoadScene(isEnd ? "Scenario 2" : "");
        ChangeScene("Scenario 2");
    }

    [CommandMethod("change scene")]
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}