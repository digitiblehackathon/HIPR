﻿using MHLab.Ethereum;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MHLab.UI
{
    public class RestartPuzzle : MonoBehaviour
    {
        public int SceneToStart;
        public GameObject HostScreen;
        public GameObject FetchingScreen;
        public Text FetchingText;
        public Button ErrorButton;

        public void Restart()
        {
            HostScreen.SetActive(false);
            FetchingScreen.SetActive(true);
            PuzzleManager.GetPuzzleHash(
                (puzzleDataSerialized) =>
                {
                    PuzzleManager.PuzzleData = JsonConvert.DeserializeObject<GetPuzzleData>(puzzleDataSerialized);
                    PuzzleManager.CurrentHash = PuzzleManager.PuzzleData.puzzleId.ToString();
                    SceneManager.LoadScene(SceneToStart);
                },
                (error) =>
                {
                    FetchingText.text = error.Message;
                    ErrorButton.gameObject.SetActive(true);
                }
            );
        }
    }
}
