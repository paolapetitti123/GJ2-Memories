using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryVisible : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject inventoryTitle;


    private void Start()
    {
        Animator invSlots = inventoryUI.GetComponent<Animator>();
        Animator invTitle = inventoryTitle.GetComponent<Animator>();

        invSlots.Play("invSlots-idle");
        invTitle.Play("Inventory-visible-idle");
    }


    public void InvInteraction()
    {
        Animator invSlots = inventoryUI.GetComponent<Animator>();
        Animator invTitle = inventoryTitle.GetComponent<Animator>();

        if (inventoryUI.activeInHierarchy)
        {
            invSlots.Play("invSlots-close");
            invTitle.Play("Inventory-close");
            inventoryTitle.GetComponent<Button>().enabled = false;
            StartCoroutine(InventoryInActive());

        }
        else if (!inventoryUI.activeInHierarchy)
        {
            StartCoroutine(InventoryActive());
            invSlots.Play("invSlots-open");
            invTitle.Play("Inventory-open");

        }
    }

    private IEnumerator InventoryInActive()
    {
        yield return new WaitForSeconds(1f);
        inventoryTitle.GetComponent<Button>().enabled = true;
        inventoryUI.SetActive(false);
    }

    private IEnumerator InventoryActive()
    {
        inventoryUI.SetActive(true);
        yield return null;
       
    }
}
