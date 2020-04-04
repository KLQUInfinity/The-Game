using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private GameObject chestTresure;
    [SerializeField] private Sprite openedChest;
    [SerializeField] private float disableDelay;

    private Animator myAnim;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    public void ToggleChest(bool enabled)
    {
        myAnim.SetBool("IsDissolve", enabled);
    }

    private void OnMouseDown()
    {
        GetComponent<Collider2D>().enabled = false;

        GetComponent<SpriteRenderer>().sprite = openedChest;
        GetComponent<SpriteRenderer>().material.SetTexture("_MainTextrue", openedChest.texture);

        if (chestTresure)
        {
            GameObject go = Instantiate(chestTresure, transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.SetParent(null);
        }

        StartCoroutine(DisableChest());
    }

    IEnumerator DisableChest()
    {
        yield return new WaitForSeconds(disableDelay);
        ToggleChest(false);
    }
}
