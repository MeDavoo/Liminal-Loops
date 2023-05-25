using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{

    [SerializeField] private Light Light;
    [SerializeField] private LightingData Preset;

    [SerializeField, Range(0, 24)] private float TimeOfDay;


    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay = (float)TimeManager.SunRotationtTime / 100 * 1.66f;

            if (TimeOfDay == 24)
            {
                TimeOfDay = 0;
            }
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        //setting ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //Set rotation and color of light
        if (Light != null)
        {
            Light.color = Preset.DirectionalColor.Evaluate(timePercent);

            Light.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    //basic debug to find directional light just in case lol
    private void OnValidate()
    {
        if (Light != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            Light = RenderSettings.sun;
        }
        //Search for light
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    Light = light;
                    return;
                }
            }
        }
    }
}
