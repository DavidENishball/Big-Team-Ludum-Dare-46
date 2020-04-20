using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalCharacterController : MonoBehaviour
{

    public TextMeshProUGUI Terminal;

    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    float screenUpdateTime;
    float screenUpdateProgress;

    private int current_display_size;
    private const int MAX_DISPLAY_SIZE = 3;
    Queue<string> terminal = new Queue<string>(MAX_DISPLAY_SIZE);

    // Start is called before the first frame update
    void Start()
    {
        //Pick a random screenupdate time.
        screenUpdateTime = Random.Range(0.5f, 5.0f);
        screenUpdateProgress = 0.0f;
        current_display_size = 0;

        // Yeah, I know.
        terminal.Enqueue(GenerateMessage());
        terminal.Enqueue(GenerateMessage());
        terminal.Enqueue(GenerateMessage());
    }

    // Update is called once per frame
    void Update()
    {
        screenUpdateProgress += Time.deltaTime;
        if(screenUpdateProgress >= screenUpdateTime) {

            //Display a new string.
            string msg = GenerateMessage();
            UpdateDisplay(msg);

            screenUpdateProgress = 0.0f;
            screenUpdateTime = Random.Range(0.5f, 5.0f);

            Debug.Log("Terminal Size: " + terminal.Count);
        }
    }

    public void UpdateDisplay(string msg) {
        terminal.Dequeue();
        terminal.Enqueue(msg);        
        var termArray = terminal.ToArray();
        Terminal.text = termArray[0] + "\n" + termArray[1] + "\n" + termArray[2];
    }

    private string GenerateMessage() {
        char[] msg = new char[Random.Range(1, 5)];
        for(int i = 0; i < msg.Length; i++) {
            msg[i] = alphabet[Random.Range(0, alphabet.Length)];
        }
        return new string(msg);
    }
}
