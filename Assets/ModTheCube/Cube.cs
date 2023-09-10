using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    float randomXdir;
    float randomYdir;
    float randomZdir;

    void Start()
    {
        Material material = Renderer.material;
        
        material.color = new Color(0.5f, 1.0f, 0.3f, 0.4f);

        randomXdir = Random.Range(1, 90);
        randomYdir = Random.Range(1, 90);
        randomZdir = Random.Range(1, 90);
    }
    
    void Update()
    {
        //Rotar
        transform.Rotate(randomXdir * Time.deltaTime * 3, randomYdir * Time.deltaTime * 3, randomZdir * Time.deltaTime * 3);

        //Cambiar tamano
        Vector3 vec = new Vector3(Mathf.Sin(Time.time), Mathf.Sin(Time.time), Mathf.Sin(Time.time));
        transform.localScale = vec;

        //Cambiar color
        float h, s, v;
        Color.RGBToHSV(Renderer.material.color, out h, out s, out v);
        Renderer.material.color = Color.HSVToRGB(h + Time.deltaTime * .25f, s, v);
    }
}
