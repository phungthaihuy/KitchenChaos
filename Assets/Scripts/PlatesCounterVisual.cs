using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plates;

    private List<GameObject> platesVisualGameObject;

    private void Awake()
    {
        platesVisualGameObject = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlatesVisual += PlatesCounter_OnPlatesVisual;
        platesCounter.OnRemovePlate += PlatesCounter_OnRemovePlate;
    }

    private void PlatesCounter_OnRemovePlate(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = platesVisualGameObject[platesVisualGameObject.Count - 1];
        platesVisualGameObject.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlatesVisual(object sender, System.EventArgs e)
    {
        
        Transform platesVisualTransform = Instantiate(plates, counterTopPoint);
        
        float platesVisualTransformY = .1f;
        platesVisualTransform.localPosition = new Vector3(0, platesVisualTransformY * platesVisualGameObject.Count, 0);
        
        platesVisualGameObject.Add(platesVisualTransform.gameObject);

    }

    private void Update()
    {
        
    }
}
