using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public int displaySize;

    public GameObject collectionObject;
    public GameObject button;

    private string[] games;
    private bool quality;
    private int firstIndex;

    private static ScrollManager _instance;

    public static ScrollManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ScrollManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        for (int i = 0; i < displaySize; i++)
        {
            GameObject newButton = Instantiate(button);
            newButton.transform.SetParent(collectionObject.transform);
        }
    }

    public void InitializeScrollManager(string[] games, bool quality)
    {
        this.games = games;
        this.quality = quality;
        firstIndex = 0;
        UpdateScroll(0);
    }

    public void ScrollUp(int amount)
    {
        if (displaySize < games.Length && firstIndex - amount >= 0)
        {
            int newFirstIndex = firstIndex - amount;
            UpdateScroll(newFirstIndex);
            firstIndex = newFirstIndex;
        }
    }
    public void ScrollDown(int amount)
    {
        if (displaySize < games.Length && firstIndex + displaySize + amount <= games.Length)
        {
            int newFirstIndex = firstIndex + amount;
            UpdateScroll(newFirstIndex);
            firstIndex = newFirstIndex;
        }
    }
    private void UpdateScroll(int newFirstIndex)
    {
        for (int i = 0; i < displaySize; i++)
        {
            collectionObject.transform.GetChild(i).GetComponent<GameButtonController>().
                    InitializeMoveButton(newFirstIndex + i, quality, games[newFirstIndex + i]);
        }
    }
}
