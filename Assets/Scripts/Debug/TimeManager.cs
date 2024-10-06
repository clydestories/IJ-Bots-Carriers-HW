using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _scale = 1;

    private void Update()
    {
        Time.timeScale = _scale;
    }
}
