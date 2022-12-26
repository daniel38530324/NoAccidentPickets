using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracking : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField]
    private GameObject[] placeablePrefab;

    //[SerializeField] GameObject ui;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    private bool imageState= false;
   
    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        foreach (GameObject prefab in placeablePrefab)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, prefab.transform.rotation);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
            //ui.SetActive(true);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
            if(spawnedPrefabs["Water01"].activeInHierarchy && spawnedPrefabs["Water03"].activeInHierarchy)
            {
                imageState = true;
                spawnedPrefabs["WaterPlay_01"].SetActive(true);
                spawnedPrefabs["WaterPlay_01"].transform.position = (spawnedPrefabs["Water01"].transform.position + spawnedPrefabs["Water03"].transform.position) / 2;

                gameManager.GetScore(1);
            }
            else
            {
                imageState = false;
                spawnedPrefabs["WaterPlay_01"].SetActive(false);
            }

            if (trackedImage.trackingState == TrackingState.Limited)
            {
                spawnedPrefabs[trackedImage.referenceImage.name].SetActive(false);
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                spawnedPrefabs[trackedImage.referenceImage.name].SetActive(true);
            }
            else if (trackedImage.trackingState == TrackingState.None)
            {
                //ui.SetActive(false);
            }

        }



        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
            //ui.SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
       
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;
        Quaternion rotation = trackedImage.transform.localRotation;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.transform.localRotation = rotation;
        prefab.SetActive(true);
        
           
        

        // foreach(GameObject go in spawnedPrefabs.Values)
        // {
        //     if(go.name != name)
        //     {
        //         go.SetActive(false);
        //     }
        // }
    }
}
