using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Events;
using UnityEngine.XR;

public class ImageTrackingTest : MonoBehaviour
{
    public GameObject aObject;
    public GameObject bObject;
    public GameObject cObject;

    private ARTrackedImageManager m_TrackedImageManager;
    private Dictionary<string, GameObject> m_TrackedImages = new Dictionary<string, GameObject>();

    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // 處理 ImageTrackingChanged 事件的函式
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // 取得新增的圖像列表
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // 檢查圖像是否符合條件
            if (trackedImage.referenceImage.name == "A")
            {
                // 產生 aObject 物件並將它加入 trackedImages 字典中
                GameObject a = Instantiate(aObject, trackedImage.transform.position, trackedImage.transform.rotation);
                m_TrackedImages.Add(trackedImage.referenceImage.name, a);
            }
            else if (trackedImage.referenceImage.name == "B")
            {
                // 產生 bObject 物件並將它加入 trackedImages 字典中
                GameObject b = Instantiate(bObject, trackedImage.transform.position, trackedImage.transform.rotation);
                m_TrackedImages.Add(trackedImage.referenceImage.name, b);
            }
            else if (trackedImage.referenceImage.name == "A" && m_TrackedImages.ContainsKey("B"))
            {
                // 產生 cObject 物件並將它的位置設置在 "A" 和 "B" 物件之間的中間
                GameObject a = m_TrackedImages["A"];
                GameObject b = m_TrackedImages["B"];
                Vector3 cPosition = (a.transform.position + b.transform.position) / 2;
                GameObject c = Instantiate(cObject, cPosition, Quaternion.identity);
            }
        }

        // 取得移除的圖像列表
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // 如果 trackedImages 字典中有記錄，則刪除該物件
            if (m_TrackedImages.ContainsKey(trackedImage.referenceImage.name))
            {
                GameObject trackedObject = m_TrackedImages[trackedImage.referenceImage.name];
                m_TrackedImages.Remove(trackedImage.referenceImage.name);
                Destroy(trackedObject);
            }
        }
    }

}