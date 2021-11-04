using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The play area, where the arrows spawn.")]
    private GameObject playArea;

    [SerializeField]
    [Tooltip("The prefab of the arrow.")]
    private GameObject arrowPrefab;

    [SerializeField]
    [Tooltip("The current Index of the Arrow. This is the arrow to be next played.")]
    private int arrowIx;

    private List<GameObject> _arrowObjects = new List<GameObject>();
    private const int ArrowsPerRow = 10;
    private const int RowsOfArrows = 10;

    private const int AnimFrames = 10;

    private int _arrowSize;
    
    private Arrow.EDirection CurrentArrowDirection()
    {
        return CurrentArrow().GetComponent<Arrow>().Direction;
    }

    private GameObject CurrentArrow()
    {
        return _arrowObjects[arrowIx];
    }

    
    // Start is called before the first frame update
    void Start()
    {
        CreateArrows(ArrowsPerRow * RowsOfArrows);
        _arrowSize = (int) playArea.GetComponent<GridLayoutGroup>().cellSize.x;
        SetArrowCurrent();
    }

    void CreateArrows(int numberOfArrows)
    {
        for (var i = 0; i < numberOfArrows; i++)
            _arrowObjects.Add(Instantiate(arrowPrefab, playArea.transform));
    }

    // Update is called once per frame
    void Update()
    {
        var keyInput = CurrentKeyInput();
        if (keyInput == Arrow.EDirection.None) return;
        if (gameState == GameState.Stop) return;
        if (gameState == GameState.Prepare) StartCoroutine(StartGameTimer());
        CheckKeyInput(keyInput);
        SetArrowCurrent();
        StartCoroutine(CheckArrowRow());
    }

    IEnumerator CheckArrowRow()
    {
        if (arrowIx < 2 * ArrowsPerRow) yield break;
        arrowIx -= ArrowsPerRow;
        DeleteFirstArrowRow();
        CreateArrows(ArrowsPerRow);
    }

    void DeleteFirstArrowRow()
    {
        StartCoroutine(DeleteFirstArrowRowAnim());
    }

    IEnumerator DeleteFirstArrowRowAnim()
    {
        Vector3 originalPosition = playArea.transform.localPosition;
        for (int i = 0; i < AnimFrames; i++)
        {
            playArea.transform.localPosition = 
                Vector3.Lerp(originalPosition,
                    originalPosition + new Vector3(0,_arrowSize), 
                    Mathf.Pow((float)i / AnimFrames, 0.5f));
            yield return new WaitForSeconds(Time.deltaTime / AnimFrames);    
        }

        for (int i = 0; i < ArrowsPerRow; i++) Destroy(_arrowObjects[i]);
        _arrowObjects.RemoveRange(0, ArrowsPerRow);
        playArea.transform.localPosition = originalPosition;
    }
   
    /// <summary>
    /// Moves the player back to the main menu (Scene 0)
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
