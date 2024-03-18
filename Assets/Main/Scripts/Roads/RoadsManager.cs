using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// Manages the roads spawn
/// </summary>
public class RoadsManager : MonoBehaviour
{
    public static List<GameObject> RoadsList = new List<GameObject>();
    [SerializeField] private List<GameObject> initRoadsList = new List<GameObject>();
    public static Road currentRoad;
    [SerializeField] private Road StartRoad;

    public static float BaseSpeed = 15f;
    public static float CurrentSpeed = BaseSpeed;

    private void Awake() {
        // Initialize the current road
        currentRoad = StartRoad;

        Character.Current.MgStarted += (sender, args) => StopMovement(1);

        // Initiialize the road list
        for (int i = 0; i < initRoadsList.Count; i++) {
            RoadsList.Add(initRoadsList[i]);
        }
        SceneManager.sceneLoaded += (arg0, mode) => StartMovement();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) CurrentSpeed++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) CurrentSpeed--; 
    }

    /// <summary>
    /// Spawn a random road next to the current road
    /// </summary>
    public static void SpawnNextRoad()
    {
        // Get a random index in the range of the RoadsList
        int index = Random.Range(0, RoadsList.Count);

        // Instantiate a road and save it in a variable
        var instance = Instantiate(RoadsList[index]);
        Road myRoad = instance.GetComponent<Road>();
        // Set the position of the road
        instance.transform.position = myRoad.GetNextPosition();

        // Update the new current road
        currentRoad = myRoad;
    }

    public static void StopMovement() {
        DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, 0f, 1f);
    }
    
    public static void StopMovement(float decelerationDuration) {
        DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, 0f, decelerationDuration);
    }

    public static void StartMovement() {
        DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, BaseSpeed, 1f);
    }
    public static void StartMovement(float accelerationDuration) {
        DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, BaseSpeed, accelerationDuration);
    }

    public static void SlowDown() {
        DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, CurrentSpeed - CurrentSpeed / 10, 1f);
    }
    public static void SlowDown(float target) {
        DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, target, 1f);
    }
    public static void SlowDown(float target, float decelerationDuration) {
        DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, target, decelerationDuration);
    }
    
    public static void SpeedUp() {
        DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, CurrentSpeed + CurrentSpeed / 10, 1f);
    }
    public static void SpeedUp(float target) {
        DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, target, 1f);
    }
    public static void SpeedUp(float target, float accelerationDuration) {
        DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, target, accelerationDuration);
    }
    
    private float MySigmoid(float x)
    {
        float result;
        float scale = 1;
        return result = 10 / (1 + Mathf.Exp((-x/2 + 2.2f)/ scale));
    }
}