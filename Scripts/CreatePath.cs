using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
public class CreatePath : MonoBehaviour
{
    public LineRenderer myLineRenderer;
    public Transform target;
    public NavMeshAgent playerNavMesh;
    public Button searchButton;
    public TMP_InputField inputField;
    private bool searched;
    public float timer=4f;
    // Start is called before the first frame update
    void Start()
    {
        myLineRenderer = GetComponent<LineRenderer>();
        playerNavMesh = GetComponent<NavMeshAgent>();
        myLineRenderer.startWidth =1f;
        myLineRenderer.endWidth = 1f;
        searchButton.onClick.AddListener(DrawLineFinal);
        //myLineRenderer.positionCount = 0;
        

    }
    void OnGUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    IEnumerator DrawLineNew()
    {
        yield return new WaitForSeconds(0f);
        if (searched)
        {
            NavMeshPath path = new NavMeshPath();
            bool x = NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            myLineRenderer.positionCount = path.corners.Length;
            myLineRenderer.SetPositions(path.corners);
        }
        try
        {
            if (Vector3.Distance(transform.position, target.position) < 2)
            {
                searched = false;
                // myLineRenderer.positionCount = 0;
            }
        }
        catch (System.Exception)
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            StartCoroutine(DrawLineNew());
            timer = 4;
        }
        
        //DrawLine();
    }
    public void DrawLineFinal()
    {
        target = GameObject.FindGameObjectWithTag(inputField.text).transform;
        searched = true;
    }    
    public void DrawLine()
    {
        myLineRenderer.positionCount = playerNavMesh.path.corners.Length;
        myLineRenderer.SetPosition(0, transform.position);
        if(playerNavMesh.path.corners.Length<2)
        {
            return;
        }
        for(int i=0;i< playerNavMesh.path.corners.Length;i++)
        {
            Vector3 pointPosition = new Vector3(playerNavMesh.path.corners[i].x, playerNavMesh.path.corners[i].y, playerNavMesh.path.corners[i].z);
            myLineRenderer.SetPosition(i, pointPosition);
        }
    }
}
