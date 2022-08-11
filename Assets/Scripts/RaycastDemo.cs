using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaycastDemo : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera Camera;
    [SerializeField]
    private Scenario[] ScenarioTargets;
    [SerializeField]
    private LineRenderer LineRenderer;

    private int ScenarioIndex = 0;

    [SerializeField]
    [Range(1, 10)]
    private float RaycastDisplaySpeed = 2f;
    [SerializeField]
    [Range(0, 6)]
    private float RaycastDisplayDuration = 5f;
    [SerializeField]
    [Range(0.25f, 2f)]
    private float StartDelay = 2f;

    private void Awake()
    {
        UpdateCameraAssignments();
    }

    private void UpdateCameraAssignments()
    {
        Camera.Follow = ScenarioTargets[ScenarioIndex].CameraTarget;
        Camera.LookAt = Camera.Follow;
    }

    private IEnumerator RunScenario(bool NeedToWait)
    {
        if (NeedToWait)
        {
            UpdateCameraAssignments();

            yield return new WaitForSeconds(StartDelay);
        }

        yield return null;

        Scenario scenario = ScenarioTargets[ScenarioIndex];
        switch (scenario.Type)
        {
            case Scenario.RaycastType.Raycast:
                RunRaycastScenario(scenario);
                break;
            case Scenario.RaycastType.RaycastAll:
                RunRaycastAllScenario(scenario);
                break;
            case Scenario.RaycastType.Spherecast:
                RunSpherecastScenario(scenario);
                break;
            case Scenario.RaycastType.SpherecastAll:
                RunSpherecastAllScenario(scenario);
                break;
            case Scenario.RaycastType.Boxcast:
                RunBoxcastScenario(scenario);
                break;
            case Scenario.RaycastType.BoxcastAll:
                RunBoxcastAllScenario(scenario);
                break;
            case Scenario.RaycastType.Capsulecast:
                RunCapsulecastScenario(scenario);
                break;
            case Scenario.RaycastType.CapsulecastAll:
                RunCapsulecastAllScenario(scenario);
                break;
        }
    }

    public void RunRaycastScenario(Scenario Scenario)
    {
        LineRenderer.positionCount = 2;
        LineRenderer.startWidth = 0.01f;
        LineRenderer.endWidth = 0.01f;

        if (Physics.Raycast(
            Scenario.RaycastOrigin.position,
            Scenario.RaycastOrigin.forward,
            out RaycastHit hit,
            Scenario.RaycastLength,
            Scenario.LayerMask,
            Scenario.TriggerInteraction))
        {
            StartCoroutine(AnimateRaycast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                hit
            ));
        }
        else
        {
            StartCoroutine(AnimateRaycast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward
            ));
        }
    }

    public void RunRaycastAllScenario(Scenario Scenario)
    {
        RaycastHit[] hits = Physics.RaycastAll(
            Scenario.RaycastOrigin.position,
            Scenario.RaycastOrigin.forward,
            Scenario.RaycastLength,
            Scenario.LayerMask,
            Scenario.TriggerInteraction);

        LineRenderer.positionCount = 2;
        LineRenderer.startWidth = 0.01f;
        LineRenderer.endWidth = 0.01f;

        if (hits.Length > 0)
        {
            StartCoroutine(AnimateRaycastAll(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                hits
            ));
        }
        else
        {
            StartCoroutine(AnimateRaycast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward
            ));
        }
    }

    public void RunSpherecastScenario(Scenario Scenario)
    {
        LineRenderer.positionCount = 0;
        LineRenderer.startWidth = 0.01f;
        LineRenderer.endWidth = 0.01f;

        Scenario.AnimatedObject.localScale = new Vector3(Scenario.Radius * 2, Scenario.Radius * 2, Scenario.Radius * 2);
        if (Physics.SphereCast(
            Scenario.RaycastOrigin.position,
            Scenario.Radius,
            Scenario.RaycastOrigin.forward,
            out RaycastHit hit,
            Scenario.RaycastLength,
            Scenario.LayerMask,
            Scenario.TriggerInteraction))
        {
            StartCoroutine(AnimateXCast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                hit,
                Scenario.AnimatedObject
            ));
        }
        else
        {
            StartCoroutine(AnimateXCast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                Scenario.AnimatedObject
            ));
        }
    }


    public void RunSpherecastAllScenario(Scenario Scenario)
    {
        RaycastHit[] hits = Physics.SphereCastAll(
            Scenario.RaycastOrigin.position,
            Scenario.Radius,
            Scenario.RaycastOrigin.forward,
            Scenario.RaycastLength,
            Scenario.LayerMask,
            Scenario.TriggerInteraction);
        
        LineRenderer.positionCount = 0;
        LineRenderer.startWidth = 0.01f;
        LineRenderer.endWidth = 0.01f;

        Scenario.AnimatedObject.localScale = new Vector3(Scenario.Radius * 2, Scenario.Radius * 2, Scenario.Radius * 2);
        if (hits.Length > 0)
        {
            StartCoroutine(AnimateXCastAll(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                Scenario.AnimatedObject,
                hits
            ));
        }
        else
        {
            StartCoroutine(AnimateXCast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                Scenario.AnimatedObject
            ));
        }
    }

    public void RunBoxcastScenario(Scenario Scenario)
    {

        LineRenderer.positionCount = 0;
        LineRenderer.startWidth = 0.01f;
        LineRenderer.endWidth = 0.01f;

        Scenario.AnimatedObject.localScale = Scenario.HalfExtents * 2;

        if (Physics.BoxCast(
            Scenario.RaycastOrigin.position,
            Scenario.HalfExtents,
            Scenario.RaycastOrigin.forward,
            out RaycastHit hit,
            Scenario.RaycastOrigin.rotation,
            Scenario.RaycastLength,
            Scenario.LayerMask,
            Scenario.TriggerInteraction))
        {
            StartCoroutine(AnimateXCast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                hit,
                Scenario.AnimatedObject
            ));
        }
        else
        {
            StartCoroutine(AnimateXCast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                Scenario.AnimatedObject
            ));
        }
    }

    public void RunBoxcastAllScenario(Scenario Scenario)
    {
        RaycastHit[] hits = Physics.BoxCastAll(
            Scenario.RaycastOrigin.position,
            Scenario.HalfExtents,
            Scenario.RaycastOrigin.forward,
            Scenario.RaycastOrigin.rotation,
            Scenario.RaycastLength,
            Scenario.LayerMask,
            Scenario.TriggerInteraction);

        LineRenderer.positionCount = 0;
        LineRenderer.startWidth = 0.01f;
        LineRenderer.endWidth = 0.01f;

        Scenario.AnimatedObject.localScale = Scenario.HalfExtents * 2;

        if (hits.Length > 0)
        {
            StartCoroutine(AnimateXCastAll(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                Scenario.AnimatedObject,
                hits
            ));
        }
        else
        {
            StartCoroutine(AnimateXCast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                Scenario.AnimatedObject
            ));
        }
    }

    public void RunCapsulecastScenario(Scenario Scenario)
    {
        Vector3 sphereOffsetDirection = Scenario.AnimatedObject.up;
        LineRenderer.positionCount = 0;
        LineRenderer.startWidth = 0.01f;
        LineRenderer.endWidth = 0.01f;

        Transform sphere1 = Scenario.AnimatedObject.Find("Sphere1");
        sphere1.transform.position = Scenario.RaycastOrigin.position + sphereOffsetDirection * Scenario.Height / 2;
        sphere1.localScale = new Vector3(Scenario.Radius * 2, Scenario.Radius * 2, Scenario.Radius * 2);
        Transform sphere2 = Scenario.AnimatedObject.Find("Sphere2");
        sphere2.transform.position = Scenario.RaycastOrigin.position - sphereOffsetDirection * Scenario.Height / 2;
        sphere2.localScale = new Vector3(Scenario.Radius * 2, Scenario.Radius * 2, Scenario.Radius * 2);
        Transform cylinder = Scenario.AnimatedObject.Find("Cylinder");
        cylinder.transform.position = Scenario.RaycastOrigin.position;
        cylinder.localScale = new Vector3(Scenario.Radius * 2, Scenario.Height, Scenario.Radius * 2);
        if (Physics.CapsuleCast(
            Scenario.RaycastOrigin.position + sphereOffsetDirection * Scenario.Height / 2,
            Scenario.RaycastOrigin.position - sphereOffsetDirection * Scenario.Height / 2,
            Scenario.Radius,
            Scenario.RaycastOrigin.forward,
            out RaycastHit hit,
            Scenario.RaycastLength,
            Scenario.LayerMask,
            Scenario.TriggerInteraction))
        {
            StartCoroutine(AnimateXCast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                hit,
                Scenario.AnimatedObject
            ));
        }
        else
        {
            StartCoroutine(AnimateXCast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                Scenario.AnimatedObject
            ));
        }
    }

    public void RunCapsulecastAllScenario(Scenario Scenario)
    {
        Vector3 sphereOffsetDirection = Scenario.AnimatedObject.transform.up;

        RaycastHit[] hits = Physics.CapsuleCastAll(
            Scenario.RaycastOrigin.position + sphereOffsetDirection * Scenario.Height / 2,
            Scenario.RaycastOrigin.position - sphereOffsetDirection * Scenario.Height / 2,
            Scenario.Radius,
            Scenario.RaycastOrigin.forward,
            Scenario.RaycastLength,
            Scenario.LayerMask,
            Scenario.TriggerInteraction);
        LineRenderer.positionCount = 0;
        LineRenderer.startWidth = 0.01f;
        LineRenderer.endWidth = 0.01f;

        Transform sphere1 = Scenario.AnimatedObject.Find("Sphere1");
        sphere1.transform.position = Scenario.RaycastOrigin.position + sphereOffsetDirection * Scenario.Height / 2;
        sphere1.localScale = new Vector3(Scenario.Radius * 2, Scenario.Radius * 2, Scenario.Radius * 2);
        Transform sphere2 = Scenario.AnimatedObject.Find("Sphere2");
        sphere2.transform.position = Scenario.RaycastOrigin.position - sphereOffsetDirection * Scenario.Height / 2;
        sphere2.localScale = new Vector3(Scenario.Radius * 2, Scenario.Radius * 2, Scenario.Radius * 2);
        Transform cylinder = Scenario.AnimatedObject.Find("Cylinder");
        cylinder.transform.position = Scenario.RaycastOrigin.position;
        cylinder.localScale = new Vector3(Scenario.Radius * 2, Scenario.Height, Scenario.Radius * 2);
        if (hits.Length > 0)
        {
            StartCoroutine(AnimateXCastAll(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                Scenario.AnimatedObject,
                hits
            ));
        }
        else
        {
            StartCoroutine(AnimateXCast(
                Scenario.RaycastOrigin.position,
                Scenario.RaycastOrigin.forward,
                Scenario.AnimatedObject
            ));
        }
    }
    
    private int buttonWidth = 200;
    private int buttonHeight = 40;
    private float xButtonMargin = 20;
    private Vector2 scrollPosition = Vector2.zero;

    private void OnGUI()
    {
        RectOffset padding = GUI.skin.button.padding;
        RectOffset margin  = GUI.skin.button.margin;

        // TODO: The height calculation should be done more correctly.
        Rect viewRect = new Rect(
            0,
            0,
            buttonWidth,
            ((buttonHeight + (padding.vertical + margin.vertical)) * ScenarioTargets.Length) - buttonHeight
        );

        
        scrollPosition = GUI.BeginScrollView(
            position: new Rect(Screen.width - buttonWidth - xButtonMargin, 10, buttonWidth + xButtonMargin, Screen.height - 10),
            scrollPosition: scrollPosition,
            viewRect: viewRect,
            alwaysShowHorizontal: false,
            alwaysShowVertical: false
        );

        for (int i = 0; i < ScenarioTargets.Length; i++)
        {
            if (GUI.Button(new Rect(0, 50 * i, buttonWidth, buttonHeight), $"Run Scenario {i + 1}"))
            {
                StopAllCoroutines();
                bool updatedScenario = i != ScenarioIndex;
                ScenarioIndex = i;
                StartCoroutine(RunScenario(updatedScenario));
            }
        }
        GUI.EndScrollView();
    }

    #region Raycast
    private IEnumerator AnimateRaycast(Vector3 StartPosition, Vector3 Direction, RaycastHit Hit)
    {
        float distance = Vector3.Distance(StartPosition, Hit.point);
        float remainingDistance = distance;
        LineRenderer.positionCount = 2;
        LineRenderer.SetPosition(0, StartPosition);

        while (remainingDistance > 0)
        {
            LineRenderer.SetPosition(1, StartPosition + Direction * (distance - remainingDistance));
            remainingDistance -= RaycastDisplaySpeed * Time.deltaTime;
            yield return null;
        }

        // Handle ProBuilder vertex coloring
        Mesh mesh = Hit.collider.GetComponent<MeshFilter>().sharedMesh;
        Color[] colors = mesh.colors;

        Color[] greenColors = new Color[colors.Length];
        for (int i = 0; i < greenColors.Length; i++)
        {
            greenColors[i] = Color.green;
        }
        mesh.SetColors(greenColors);

        // Handle if we're using standard URP shader
        Color oldColor = Color.white;
        if (Hit.collider.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer) && meshRenderer.material.HasColor("_BaseColor"))
        {
            oldColor = meshRenderer.material.GetColor("_BaseColor");
            meshRenderer.material.SetColor("_BaseColor", Color.green);
        }

        yield return new WaitForSeconds(RaycastDisplayDuration);

        // Reset default standard URP shader color
        if (Hit.collider.TryGetComponent<MeshRenderer>(out meshRenderer) && meshRenderer.material.HasColor("_BaseColor"))
        {
            meshRenderer.material.SetColor("_BaseColor", oldColor);
        }

        // Reset ProBuilder vertex colors
        mesh.SetColors(colors);

        LineRenderer.positionCount = 0;
    }

    private IEnumerator AnimateRaycastAll(Vector3 StartPosition, Vector3 Direction, RaycastHit[] Hits)
    {
        float farthestDistance = Hits.Max(hit => hit.distance);
        float remainingDistance = farthestDistance;
        float distanceTraveled = 0;

        bool[] alreadyToggledObject = new bool[Hits.Length];
        Color[] oldColors = new Color[Hits.Length];
        Dictionary<int, Color[]> oldVertexColors = new Dictionary<int, Color[]>();
        for (int i = 0; i < alreadyToggledObject.Length; i++)
        {
            alreadyToggledObject[i] = false;
        }

        LineRenderer.SetPosition(0, StartPosition);
        while (remainingDistance > 0)
        {
            LineRenderer.SetPosition(1, StartPosition + Direction * (farthestDistance - remainingDistance));
            distanceTraveled += RaycastDisplaySpeed * Time.deltaTime;
            remainingDistance -= RaycastDisplaySpeed * Time.deltaTime;

            for (int i = 0; i < Hits.Length; i++)
            {
                if (!alreadyToggledObject[i] && distanceTraveled >= Hits[i].distance)
                {
                    alreadyToggledObject[i] = true;
                    // Handle ProBuilder vertex coloring
                    Mesh mesh = Hits[i].collider.GetComponent<MeshFilter>().sharedMesh;
                    oldVertexColors.Add(i, mesh.colors);

                    Color[] greenColors = new Color[mesh.colors.Length];
                    for (int j = 0; j < greenColors.Length; j++)
                    {
                        greenColors[j] = Color.green;
                    }
                    mesh.SetColors(greenColors);

                    // Handle if we're using standard URP shader
                    Color oldColor = Color.white;
                    if (Hits[i].collider.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer) && meshRenderer.material.HasColor("_BaseColor"))
                    {
                        oldColors[i] = meshRenderer.material.GetColor("_BaseColor");
                        meshRenderer.material.SetColor("_BaseColor", Color.green);
                    }
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(RaycastDisplayDuration);

        for (int i = 0; i < Hits.Length; i++)
        {
            // Reset default standard URP shader color
            if (Hits[i].collider.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer) && meshRenderer.material.HasColor("_BaseColor"))
            {
                meshRenderer.material.SetColor("_BaseColor", oldColors[i]);
            }

            // Reset ProBuilder vertex colors
            Mesh mesh = Hits[i].collider.GetComponent<MeshFilter>().sharedMesh;
            mesh.SetColors(oldVertexColors[i]);

            LineRenderer.positionCount = 0;
        }
    }

    private IEnumerator AnimateRaycast(Vector3 StartPosition, Vector3 Direction)
    {
        float distance = 15;
        float remainingDistance = distance;
        LineRenderer.SetPosition(0, StartPosition);
        while (remainingDistance > 0)
        {
            LineRenderer.SetPosition(1, StartPosition + Direction * (distance - remainingDistance));
            remainingDistance -= RaycastDisplaySpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(RaycastDisplayDuration);

        LineRenderer.positionCount = 0;
    }
    #endregion


    #region Spherecast, BoxCast, CapsuleCast
    private IEnumerator AnimateXCast(Vector3 StartPosition, Vector3 Direction, RaycastHit Hit, Transform AnimatedObject)
    {
        float distance = Vector3.Distance(StartPosition, Hit.point);
        float remainingDistance = distance;
        LineRenderer.positionCount = 0;

        while (remainingDistance > 0)
        {
            AnimatedObject.transform.position = Vector3.Lerp(StartPosition, StartPosition + Direction * distance, 1 - (remainingDistance / distance));
            remainingDistance -= RaycastDisplaySpeed * Time.deltaTime;
            yield return null;
        }

        // Handle ProBuilder vertex coloring
        Mesh mesh = Hit.collider.GetComponent<MeshFilter>().sharedMesh;
        Color[] colors = mesh.colors;

        Color[] greenColors = new Color[colors.Length];
        for (int i = 0; i < greenColors.Length; i++)
        {
            greenColors[i] = Color.green;
        }
        mesh.SetColors(greenColors);

        // Handle if we're using standard URP shader
        Color oldColor = Color.white;
        if (Hit.collider.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer) && meshRenderer.material.HasColor("_BaseColor"))
        {
            oldColor = meshRenderer.material.GetColor("_BaseColor");
            meshRenderer.material.SetColor("_BaseColor", Color.green);
        }

        yield return new WaitForSeconds(RaycastDisplayDuration);

        // Reset default standard URP shader color
        if (Hit.collider.TryGetComponent<MeshRenderer>(out meshRenderer) && meshRenderer.material.HasColor("_BaseColor"))
        {
            meshRenderer.material.SetColor("_BaseColor", oldColor);
        }

        // Reset ProBuilder vertex colors
        mesh.SetColors(colors);

        AnimatedObject.position = StartPosition;
    }

    private IEnumerator AnimateXCast(Vector3 StartPosition, Vector3 Direction, Transform AnimatedObject)
    {
        float distance = 100;
        float remainingDistance = distance;
        LineRenderer.positionCount = 0;

        while (remainingDistance > 0)
        {
            AnimatedObject.transform.position = Vector3.Lerp(StartPosition, StartPosition + Direction * distance, 1 - (remainingDistance / distance));
            remainingDistance -= RaycastDisplaySpeed * Time.deltaTime;
            yield return null;
        }


        yield return new WaitForSeconds(RaycastDisplayDuration);

        AnimatedObject.position = StartPosition;
    }

    private IEnumerator AnimateXCastAll(Vector3 StartPosition, Vector3 Direction, Transform AnimatedObject, RaycastHit[] Hits)
    {
        float farthestDistance = Hits.Max(hit => hit.distance);
        float remainingDistance = farthestDistance;
        float distanceTraveled = 0;

        bool[] alreadyToggledObject = new bool[Hits.Length];
        Color[] oldColors = new Color[Hits.Length];
        Dictionary<int, Color[]> oldVertexColors = new Dictionary<int, Color[]>();
        for (int i = 0; i < alreadyToggledObject.Length; i++)
        {
            alreadyToggledObject[i] = false;
        }

        while (remainingDistance > 0)
        {
            AnimatedObject.transform.position = Vector3.Lerp(StartPosition, StartPosition + Direction * farthestDistance, 1 - (remainingDistance / farthestDistance));
            distanceTraveled += RaycastDisplaySpeed * Time.deltaTime;
            remainingDistance -= RaycastDisplaySpeed * Time.deltaTime;

            for (int i = 0; i < Hits.Length; i++)
            {
                if (!alreadyToggledObject[i] && distanceTraveled >= Hits[i].distance)
                {
                    alreadyToggledObject[i] = true;
                    // Handle ProBuilder vertex coloring
                    Mesh mesh = Hits[i].collider.GetComponent<MeshFilter>().sharedMesh;
                    oldVertexColors.Add(i, mesh.colors);

                    Color[] greenColors = new Color[mesh.colors.Length];
                    for (int j = 0; j < greenColors.Length; j++)
                    {
                        greenColors[j] = Color.green;
                    }
                    mesh.SetColors(greenColors);

                    // Handle if we're using standard URP shader
                    Color oldColor = Color.white;
                    if (Hits[i].collider.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer) && meshRenderer.material.HasColor("_BaseColor"))
                    {
                        oldColors[i] = meshRenderer.material.GetColor("_BaseColor");
                        meshRenderer.material.SetColor("_BaseColor", Color.green);
                    }
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(RaycastDisplayDuration);

        for (int i = 0; i < Hits.Length; i++)
        {
            // Reset default standard URP shader color
            if (Hits[i].collider.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer) && meshRenderer.material.HasColor("_BaseColor"))
            {
                meshRenderer.material.SetColor("_BaseColor", oldColors[i]);
            }

            // Reset ProBuilder vertex colors
            Mesh mesh = Hits[i].collider.GetComponent<MeshFilter>().sharedMesh;
            mesh.SetColors(oldVertexColors[i]);
        }

        AnimatedObject.transform.position = StartPosition;
    }
    #endregion
}
