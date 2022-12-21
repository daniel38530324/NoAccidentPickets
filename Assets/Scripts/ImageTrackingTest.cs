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

    // �B�z ImageTrackingChanged �ƥ󪺨禡
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // ���o�s�W���Ϲ��C��
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // �ˬd�Ϲ��O�_�ŦX����
            if (trackedImage.referenceImage.name == "A")
            {
                // ���� aObject ����ñN���[�J trackedImages �r�夤
                GameObject a = Instantiate(aObject, trackedImage.transform.position, trackedImage.transform.rotation);
                m_TrackedImages.Add(trackedImage.referenceImage.name, a);
            }
            else if (trackedImage.referenceImage.name == "B")
            {
                // ���� bObject ����ñN���[�J trackedImages �r�夤
                GameObject b = Instantiate(bObject, trackedImage.transform.position, trackedImage.transform.rotation);
                m_TrackedImages.Add(trackedImage.referenceImage.name, b);
            }
            else if (trackedImage.referenceImage.name == "A" && m_TrackedImages.ContainsKey("B"))
            {
                // ���� cObject ����ñN������m�]�m�b "A" �M "B" ���󤧶�������
                GameObject a = m_TrackedImages["A"];
                GameObject b = m_TrackedImages["B"];
                Vector3 cPosition = (a.transform.position + b.transform.position) / 2;
                GameObject c = Instantiate(cObject, cPosition, Quaternion.identity);
            }
        }

        // ���o�������Ϲ��C��
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // �p�G trackedImages �r�夤���O���A�h�R���Ӫ���
            if (m_TrackedImages.ContainsKey(trackedImage.referenceImage.name))
            {
                GameObject trackedObject = m_TrackedImages[trackedImage.referenceImage.name];
                m_TrackedImages.Remove(trackedImage.referenceImage.name);
                Destroy(trackedObject);
            }
        }
    }

}