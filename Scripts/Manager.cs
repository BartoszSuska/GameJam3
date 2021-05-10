using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Com.BoarShroom.GameJam3
{
    public class Manager : MonoBehaviour
    {
        public int wave = 1;
        public int chickens;
        public GameObject waveText;
        public GameObject[] spawner;
        public GameObject chickenPrefab;
        public TMP_Text chickenNumber;

        public GameObject DeadWolf;
        GameObject player;
        public GameObject EndScreen;
        public TMP_Text highscoreText;

        void Start()
        {

            StartCoroutine(StartWave());
        }

        void Update()
        {
            if(GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            chickenNumber.text = chickens.ToString();

            if(chickens == 0)
            {
                wave++;
                StartCoroutine(StartWave());
            }
        }

        IEnumerator StartWave()
        {
            chickens = wave;
            waveText.GetComponent<TMP_Text>().text = "WAVE " + wave.ToString();
            waveText.GetComponent<Animator>().SetTrigger("In");
            yield return new WaitForSeconds(3f);
            waveText.GetComponent<Animator>().SetTrigger("Out");
            for(int i = 1; i <= wave; i++)
            {
                Instantiate(chickenPrefab, spawner[Random.Range(0, spawner.Length)].transform);
            }
        }

        public void End()
        {
            if(wave > PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", wave);
            }

            highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();

            EndScreen.SetActive(true);
            Instantiate(DeadWolf, player.transform.position, player.transform.rotation);
            Destroy(player);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}
