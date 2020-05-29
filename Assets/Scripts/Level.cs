using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    // params
    [SerializeField] int breakableBlocks;    // serialized for debugging purposes

    // cached reference
    SceneLoader sceneLoader;

    void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void IncreaseBlockCount() {
        breakableBlocks++;
    }

    public void DecreaseBlockCount() {
        breakableBlocks--;
        if (breakableBlocks <= 0) {
            sceneLoader.LoadNextScene();
        }
    }
}
