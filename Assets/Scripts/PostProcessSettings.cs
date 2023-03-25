using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class PostProcessSettings : MonoBehaviour
{
    PostProcessVolume volume;
    bool isActive = true;
    public GameObject sphere;
    public GameObject wristmenu;
    
    public Slider slNoise;
    public Slider slStripes;
    public Slider slPixels;
    public Slider slDepth;
    public Slider slTime;
    
    void Awake() {
        volume = GetComponentInParent<PostProcessVolume>();
        AutostereogramNoise autostereogramNoise;
        Depth depth;
        
        volume.profile.TryGetSettings(out autostereogramNoise);
        volume.profile.TryGetSettings(out depth);
        
        isActive = !isActive;
        sphere.active = !isActive;
        wristmenu.active = !isActive;
        autostereogramNoise.active = isActive;
        depth.active = isActive;
    }
    
    

    internal void Reveal()
    {
        AutostereogramNoise autostereogramNoise;
        Depth depth;
        
        volume.profile.TryGetSettings(out autostereogramNoise);
        volume.profile.TryGetSettings(out depth);
        isActive = !isActive;
        
        
        autostereogramNoise.useNoise.value = (int)slNoise.value;
        autostereogramNoise.strips.value = (int)slStripes.value;
        autostereogramNoise.pixelsPerStrip.value = (int)slPixels.value;
        autostereogramNoise.depthFactor.value = slDepth.value;
        autostereogramNoise.timeFactor.value = slTime.value;
        
        sphere.active = !isActive;
        wristmenu.active = !isActive;
        autostereogramNoise.active = isActive;
        depth.active = isActive;
    }

}
