using UnityEngine;
public class Stove : MonoBehaviour
{
    public int cookingCount = 0;

    private AudioPlayer audioPlayer;

    void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer not found!");
        }
    }

    public void addCount()
    {
        cookingCount++;

        if (cookingCount == 1)
        {
            audioPlayer.Play();
        }

    }

    public void removeCount()
    {
        cookingCount--;

        if (cookingCount == 0)
        {
            audioPlayer.Stop();
        }
    }


}
