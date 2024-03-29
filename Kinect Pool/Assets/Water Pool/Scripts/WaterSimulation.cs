//$ cite -u https://github.com/shanecelis/water-demo -C -l mit
/* Original code[1] Copyright (c) 2018 Shane Celis[2]
   Licensed under the MIT License[3]

   [1]: https://github.com/shanecelis/water-demo
   [2]: https://github.com/shanecelis
   [3]: https://opensource.org/licenses/MIT
*/

using Assets.WaterPool.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// http://tips.hecomi.com/entry/2017/05/17/020037
public class WaterSimulation : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public CustomRenderTexture texture;
    public float dropRadius; // uv units [0, 1]
    public bool pause = false;

    public Camera main;
    public Shader shader_water;
    public Ray ray;

    private CustomRenderTextureUpdateZone[] zones = null;
    private Collider collider;
    private CustomRenderTextureUpdateZone defaultZone, normalZone, waveZone;

    void Start()
    {
        texture.Initialize();
        collider = GetComponent<Collider>();


        defaultZone = new CustomRenderTextureUpdateZone
        {
            needSwap = true,
            passIndex = 0, // integrate
            rotation = 0f,
            updateZoneCenter = new Vector2(0.5f, 0.5f),
            updateZoneSize = new Vector2(1f, 1f)
        };

        normalZone = new CustomRenderTextureUpdateZone
        {
            needSwap = true,
            passIndex = 2, // update normals
            rotation = 0f,
            updateZoneCenter = new Vector2(0.5f, 0.5f),
            updateZoneSize = new Vector2(1f, 1f)
        };

        waveZone = new CustomRenderTextureUpdateZone
        {
            needSwap = true,
            passIndex = 1, // drop
            rotation = 0f,
            // waveZone.updateZoneCenter = uv;
            updateZoneSize = new Vector2(dropRadius, dropRadius)
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) pause = !pause;

        UpdateZones();

        if (zones != null)
        {
            texture.SetUpdateZones(zones);
            zones = null;
            if (pause) texture.Update(1);
        }
        else
        {
            texture.SetUpdateZones(new CustomRenderTextureUpdateZone[] { defaultZone, defaultZone, normalZone });
        }

        if (!pause || Input.GetKeyDown(KeyCode.N)) texture.Update(1);
    }

    public void OnDrag(PointerEventData ped)
    {
        AddWave(ped);
    }

    public void OnPointerClick(PointerEventData ped)
    {
        AddWave(ped);
    }

    void AddWave(PointerEventData ped)
    {
        // https://answers.unity.com/questions/892333/find-xy-cordinates-of-click-on-uiimage-new-gui-sys.html
        Vector2 localCursor;
        var rt = GetComponent<RectTransform>();
        if (rt == null || !RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, ped.position, ped.pressEventCamera, out localCursor))
            return;

        // Vector2 uv = Rect.NormalizedToPoint(rt.rect, localCursor);
        Vector2 uv = Rect.PointToNormalized(rt.rect, localCursor);

        var leftClick = ped.button == PointerEventData.InputButton.Left;

        // Debug.Log("We got a click " + localCursor + " uv " + uv);
        // AddWave(uv, leftClick ? 2 : 3); // 1 または -1 にバッファを塗るパス);
        AddWave(uv);
    }

    public void AddWave(Vector2 uv)
    {
        waveZone.updateZoneCenter = new Vector2(uv.x, 1f - uv.y);

        if (pause)
            zones = new CustomRenderTextureUpdateZone[] { waveZone, normalZone };
        else
            zones = new CustomRenderTextureUpdateZone[] { defaultZone, defaultZone, waveZone, normalZone };

        // texture.Update(1);
    }

    void UpdateZones()
    {
        if (collider == null) return;

        bool leftClick = Input.GetMouseButton(0);
        if (!leftClick) return;

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if (Physics.Raycast(ray, out hit)
        //     && hit.transform == transform) {

        if (collider.Raycast(ray, out hit, 100f))
        {
            // Debug.Log("Clicked uv " + hit.textureCoord2);
            // AddWave(hit.textureCoord2, leftClick ? 2 : 3);
            AddWave(hit.textureCoord2);
            
        }
    }
}
