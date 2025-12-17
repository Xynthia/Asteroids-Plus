using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private static HighScoreManager instance;
    public static HighScoreManager Instance { get { return instance; } }

    public float highScore;

    private float highScoreDeathStar;
    private float highScoreInOrbit;
    private float highScoreHittingTheAtmosphere;

    private void Awake()
    {
        // setup singleton
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public void setHighScore(float finalScore)
    {
        if (AudioManager.Instance.bgAudio == AudioManager.Instance.deathStar.song)
        {
            if (finalScore >  highScoreDeathStar)
            {
                highScoreDeathStar = finalScore;
                highScore = highScoreDeathStar;
            }
        }

        if (AudioManager.Instance.bgAudio == AudioManager.Instance.inOrbit.song)
        {
            if (finalScore > highScoreInOrbit)
            {
                highScoreInOrbit = finalScore;
                highScore = highScoreInOrbit;
            }
        }

        if (AudioManager.Instance.bgAudio == AudioManager.Instance.hittingTheAtmosphere.song)
        {
            if (finalScore > highScoreHittingTheAtmosphere)
            {
                highScoreHittingTheAtmosphere = finalScore;
                highScore = highScoreHittingTheAtmosphere;

            }
        }
    }

}
