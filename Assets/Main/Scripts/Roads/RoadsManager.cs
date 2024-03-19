using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEditor.Rendering;
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
    private static float SpeedIncr= 1;
    public static float BaseSpeed = 15f;
    public static float CurrentSpeed = BaseSpeed;
    private static TweenerCore<float, float, FloatOptions> isSpeeding;
    private static float maxSpeed = 100;
    private static int lastindex;

    private void Awake() {
        // Initialize the current road
        currentRoad = StartRoad;

        // Initiialize the road list
        for (int i = 0; i < initRoadsList.Count; i++) {
            RoadsList.Add(initRoadsList[i]);
        }
        SceneManager.sceneLoaded += (arg0, mode) => StartMovement();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) CurrentSpeed++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) CurrentSpeed--;
    }

    private void Start() {
        SpeedUp();
        Character.Current.MgStarted += (sender, args) => StopMovement(1);
    }

    /// <summary>
    /// Spawn a random road next to the current road
    /// </summary>
    public static void SpawnNextRoad() {
        // Get a random index in the range of the RoadsList
        int index = Random.Range(0, RoadsList.Count);
        if (lastindex != index)
        {
            Debug.Log(RoadsList.Count);
            Debug.Log(index);

            // Instantiate a road and save it in a variable
            var instance = Instantiate(RoadsList[index]);
            Road myRoad = instance.GetComponent<Road>();
            // Set the position of the road
            instance.transform.position = myRoad.GetNextPosition();

            // Update the new current road
            currentRoad = myRoad;
            lastindex = index;
        }
        else if (lastindex == index)
            SpawnNextRoad();
    }
    public static void StopMovement()
    {
        isSpeeding?.Kill();
        isSpeeding = DOTween.To(() => CurrentSpeed, f => CurrentSpeed = f, 0f, 1f);
    }
    
    public static void StopMovement(float decelerationDuration) {
        isSpeeding?.Kill();
        isSpeeding = DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, 0f, decelerationDuration);
    }

    public static void StartMovement() {
        isSpeeding?.Kill();
        isSpeeding = DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, BaseSpeed, 1f);
    }
    public static void StartMovement(float accelerationDuration) {
        isSpeeding?.Kill();
        isSpeeding = DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, BaseSpeed, accelerationDuration);
    }

    public static void SlowDown() {
        isSpeeding?.Kill();
        isSpeeding = DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, CurrentSpeed - CurrentSpeed / 10, 10f);
    }
    public static void SlowDown(float target) {
        isSpeeding?.Kill();
        isSpeeding = DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, target, 1f);
        isSpeeding.onComplete += () => SpeedUp();
    }
    public static void SlowDown(float target, float decelerationDuration) {
        isSpeeding?.Kill();
        isSpeeding = DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, target, decelerationDuration);
        isSpeeding.onComplete += () => SpeedUp();
    }
    
    public static void SpeedUp() {
        isSpeeding?.Kill();
        if (CurrentSpeed <= maxSpeed)
        {
            isSpeeding = DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, CurrentSpeed + SpeedIncr, 1f);
            Debug.Log("PlayerSpeed = " + CurrentSpeed);
            isSpeeding.onComplete += () => SpeedUp();
        }
    }
    
    public static void SpeedUp(float target) {
        isSpeeding?.Kill();
        if (CurrentSpeed <= maxSpeed)
        {
            isSpeeding = DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, target, 1f);
            isSpeeding.onComplete += () => SpeedUp(target);
        }
    }
    public static void SpeedUp(float target, float accelerationDuration) {
        isSpeeding?.Kill();
        if (CurrentSpeed <= maxSpeed)
        {
            isSpeeding = DOTween.To(() => CurrentSpeed,f => CurrentSpeed = f, target, accelerationDuration);
            isSpeeding.onComplete += () => SpeedUp(target, accelerationDuration);
        }
    }
    
}