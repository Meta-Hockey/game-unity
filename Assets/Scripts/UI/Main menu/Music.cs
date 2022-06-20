using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    //Источник музыки
    public AudioSource auso;
    //Включена музыка или нет
    private bool musicOn = true;


    void Awake()
    {
        //Получаем данные из сохранения
        //Пришлось переводить из int в bool, так как через PlayerPrefs bool не сохранятся
        //musicOn = Convert.ToBoolean(PlayerPrefs.GetInt("Music"));
        //Начать проигрывать музыку или нет
        if(musicOn)
            auso.Play();
        else
            auso.Stop();


        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "3")
            auso.mute = true;
        else
            auso.mute = false;
    }
 
    void Destroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Кнопка музыки
    public void OnMusic()
    {
        //Вкл или выкл музыку
        musicOn = !musicOn;
        //Сохранить переменную
        //PlayerPrefs.SetInt("Music", Convert.ToInt32(musicOn));
        //Вкл/выкл музыки
        if(musicOn)
            auso.Play();
        else
            auso.Stop();
    }
}
