using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runner.MenuController
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private Text _scoreText;
        [SerializeField]
        private Text _liveText;

        private float _score;

        [SerializeField]
        private List<GameObject> Panels = new List<GameObject>();

        public static Action<int> OnEnablePanel;
        public static Action<int> UpdateLives;


        private void OnEnable()
        {
            OnEnablePanel += OnActivePanel;
            UpdateLives += UpateLives;
        }

        private void OnDisable()
        {
            OnEnablePanel -= OnActivePanel;
            UpdateLives -= UpateLives;
        }

        void Update()
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            _score += 1 * Time.deltaTime;
            _scoreText.text = $"{(int)_score}";
        }

        private void UpateLives(int live) => _liveText.text = $"{live}";

        private void OnActivePanel(int index) => Panels[index].SetActive(true);

        public void OnClickRestart() => SceneManager.LoadScene(0);
    }
}
