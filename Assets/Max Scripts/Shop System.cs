using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Add this for TextMeshPro

public class ShopSystem : MonoBehaviour
{
    public Button turretButtonPrefab; // Prefab of the turret button
    public GameObject shopPanel; // The panel where the shop UI will be displayed
    public GameObject turretPrefab; // The turret prefab to be placed
    public TextMeshProUGUI moneyText; // Reference to the UI text for money display (TextMeshProUGUI)
    public GameObject ghostTurretPrefab; // The ghost turret prefab for visualizing placement

    public string turretButtonText = "Turret - $50"; // Add a public field for custom button text

    private GameObject ghostTurret; // The current ghost turret

    void Start()
    {
        // Example of creating a turret button with custom text
        Button newButton = Instantiate(turretButtonPrefab, shopPanel.transform);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = turretButtonText; // Set the button text
        newButton.onClick.AddListener(OnTurretButtonClicked);
    }

    void Update()
    {
        // Update the UI to show the current money
        moneyText.text = "Money: $" + GameManager.Instance.playerMoney.ToString();

        // Update ghost turret position if it exists
        if (ghostTurret != null)
        {
            UpdateGhostTurretPosition();
        }

        // Place the turret when the second click is detected
        if (ghostTurret != null && Input.GetMouseButtonDown(0)) // Left-click to place the turret
        {
            PlaceTurret();
        }
    }

    void OnTurretButtonClicked()
    {
        // Check if the player has enough money
        if (GameManager.Instance.CanAfford(50)) 
        {
            // Deduct money for the turret
            GameManager.Instance.ChangeMoney(-50);
            // Spawn a ghost turret to show the placement
            ghostTurret = Instantiate(ghostTurretPrefab);
            ghostTurret.SetActive(true); // Make sure it's visible
            Debug.Log("Click again to place the turret.");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    void UpdateGhostTurretPosition()
    {
        RaycastHit hit;
        // Use raycasting to position the ghost turret
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                // Position the ghost turret where the raycast hits
                ghostTurret.transform.position = hit.point;
                ghostTurret.transform.rotation = Quaternion.identity; // Optional, set rotation to default
            }
        }
    }

    void PlaceTurret()
    {
        // Instantiate the actual turret at the ghost turret's position
        if (ghostTurret != null)
        {
            Instantiate(turretPrefab, ghostTurret.transform.position, Quaternion.identity);
            Destroy(ghostTurret); // Destroy the ghost turret after placing the actual turret
            ghostTurret = null; // Reset the ghost turret
            Debug.Log("Turret placed!");
        }
    }
}
