using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageHandler : MonoBehaviour
{
    [SerializeField] GameObject[] _pages;

    // Start is called before the first frame update
    void Start()
    {
        CloseAllPages();

        if (GameSession.IsFirstGame) GoToPage(1);
        else GoToPage(_pages.Length);
    }

    void CloseAllPages()
    {
        foreach (var page in _pages)
        {
            page.SetActive(false);
        }
    }

    public void GoToPage(int pageNumber)
    {
        CloseAllPages();

        _pages[pageNumber - 1].SetActive(true);
    }
}
