using System.Collections.Generic;
using UnityEngine;
 
public class WeightAndScale : MonoBehaviour
{
    // Define the available materials and their corresponding masses in KG per meter squared.
    public enum MaterialType
    {
        WoodBalsa,
        CastIron,
        Copper,
        Steel,
        Titanium,
        Gold,
        Silver,
        Brick,
        StoneGranite,
        Male,
        Female,
        Water,
        Concrete,
        Sand,
        Rubber,
        Tire,
        Diamond,
        Beyblade
    }
 
    // Dictionary to store material-to-mass conversion values.
    private Dictionary<MaterialType, float> materialMasses = new Dictionary<MaterialType, float>
{
    { MaterialType.WoodBalsa, 100f },
    { MaterialType.CastIron, 7300f },
    { MaterialType.Copper, 8930f },
    { MaterialType.Steel, 7850f },
    { MaterialType.Titanium, 4500f },
    { MaterialType.Gold, 19300f },
    { MaterialType.Silver, 10500f },
    { MaterialType.Brick, 1800f },
    { MaterialType.StoneGranite, 2700f },
    { MaterialType.Male, 47f },
    { MaterialType.Female, 42f },
    { MaterialType.Water, 1000f },
    { MaterialType.Concrete, 2500f },
    { MaterialType.Sand, 1680f },
    { MaterialType.Rubber, 1217f },
    { MaterialType.Tire, 15f },
    { MaterialType.Diamond, 17.5f },
    { MaterialType.Beyblade, 0.0079f }
};
 
    // Public variable to select the material from the Unity editor.
    public MaterialType selectedMaterial;
 
    // Reference to the Rigidbody component of the game object.
    private Rigidbody rb;
 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateMassAndScale();
        //SetCustomProperties();
    }
 
    public void Update()
    {
        UpdateMassAndScale();
    }
 
    // Method to update the mass and scale based on the selected material.
    public void UpdateMassAndScale()
    {
        if (materialMasses.TryGetValue(selectedMaterial, out float referenceMass))
        {
            // Get the current scale of the game object.
            Vector3 currentScale = transform.localScale;
 
            // Calculate the mass based on the selected material's reference mass and the current scale.
            float mass = referenceMass * currentScale.x * currentScale.y * currentScale.z;
 
            // Set the mass of the Rigidbody component.
            rb.mass = mass;
        }
        else
        {
            Debug.LogWarning("Selected material not found in the materialMasses dictionary!");
        }
    }
 
    // Method to be called when custom properties are changed in the Unity editor.
    public void OnCustomPropertiesChanged()
    {
        //  SetCustomProperties();
        UpdateMassAndScale();
    }
}