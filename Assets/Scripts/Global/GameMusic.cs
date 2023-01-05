using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] AudioSource _menuSong;
    [SerializeField] AudioSource _gameplaySong;
    bool isGameplaySongEnabled;
    bool isMenuScene;

    // Start is called before the first frame update
    void Awake()
    {
        SceneLoader.NewScene += Both;
        SceneLoader.NewScene += PlayMenuSong;
        AutoPause.GamePaused += SongPause;
        AutoPause.GameUnpaused += SongUnpause;
    }

    void Start()
    {
        PlayMenuSong();
    }

    private void OnDestroy()
    {
        SceneLoader.NewScene -= Both;
        SceneLoader.NewScene -= PlayMenuSong;
        AutoPause.GamePaused -= SongPause;
        AutoPause.GameUnpaused -= SongUnpause;
    }

    void PlayMenuSong()
    {
        isMenuScene = true;

        if (_gameplaySong.isPlaying) _gameplaySong.Stop();

        if (!_menuSong.isPlaying) _menuSong.Play();
    }

    public void PlayGamplaySong()
    {
        isMenuScene = false;

        if (_menuSong.isPlaying) _menuSong.Stop();

        if (!_gameplaySong.isPlaying) _gameplaySong.Play();

        isGameplaySongEnabled = true;
    }

    public void TogglePauseGameplaySong()
    {
        if (_gameplaySong.isPlaying) _gameplaySong.Pause();
        else _gameplaySong.UnPause();

        isGameplaySongEnabled = !isGameplaySongEnabled;
    }

    void SongPause()
    {
        if (isMenuScene) _menuSong.Pause();
        else if (isGameplaySongEnabled) _gameplaySong.Pause();
    }

    void SongUnpause()
    {
        if (isMenuScene) _menuSong.UnPause();
        else if (isGameplaySongEnabled) _gameplaySong.UnPause();
    }

    public void TogglePauseMenuSong()
    {
        if (_menuSong.isPlaying) _menuSong.Pause();
        else _menuSong.UnPause();
    }

    public void Left()
    {
        if (_gameplaySong.isPlaying) _gameplaySong.panStereo = -0.8f;
    }

    public void Both()
    {
        if (_gameplaySong.isPlaying) _gameplaySong.panStereo = 0f;
    }
}