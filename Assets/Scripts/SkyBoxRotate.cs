using UnityEngine;
using System.Collections;

public class SkyBoxRotate : MonoBehaviour
{
    [Header("SkyBox Settings")]
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float initialRotation = 0f;
    [SerializeField] private float startDelay = 2f;  // 回転開始までの待機時間（秒）

    private Material instanceSkybox;
    private bool canRotate = false;

    private void Awake()
    {
        if (skyboxMaterial != null)
        {
            instanceSkybox = new Material(skyboxMaterial);
            RenderSettings.skybox = instanceSkybox;
            instanceSkybox.SetFloat("_Rotation", initialRotation);

            // 指定秒後に回転開始
            StartCoroutine(StartRotationAfterDelay());
        }
    }

    private IEnumerator StartRotationAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        canRotate = true;
    }

    private void Update()
    {
        if (!canRotate || instanceSkybox == null) return;

        float rot = instanceSkybox.GetFloat("_Rotation");
        rot += rotationSpeed * Time.deltaTime;
        instanceSkybox.SetFloat("_Rotation", rot);
    }
}
