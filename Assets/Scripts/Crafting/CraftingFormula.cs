using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string planetName;
    public List<string> requiredElements;
    public GameObject planetPrefab;
}

public class CraftingFormula : MonoBehaviour
{
    [Header("Planet Recipes")]
    public List<Recipe> recipes = new List<Recipe>();
    
    [Header("Planet Prefabs")]
    public GameObject mercuryPrefab;
    public GameObject venusPrefab;
    public GameObject earthPrefab;
    public GameObject marsPrefab;
    public GameObject jupiterPrefab;
    public GameObject saturnPrefab;
    public GameObject uranusPrefab;
    public GameObject neptunePrefab;
    public GameObject plutoPrefab;

    void Start()
    {
        InitializeRecipes();
    }
    
    void InitializeRecipes()
    {
        recipes.Clear();
        
        recipes.Add(new Recipe {
            planetName = "Mercury",
            requiredElements = new List<string> { "O2", "S", "H2", "He", "K" },
            planetPrefab = mercuryPrefab
        });
        
        recipes.Add(new Recipe {
            planetName = "Venus",
            requiredElements = new List<string> { "CO2", "N2" },
            planetPrefab = venusPrefab
        });
        
        recipes.Add(new Recipe {
            planetName = "Earth",
            requiredElements = new List<string> { "N2", "O2", "Ar" },
            planetPrefab = earthPrefab
        });
        
        recipes.Add(new Recipe {
            planetName = "Mars",
            requiredElements = new List<string> { "CO2", "N2", "Ar" },
            planetPrefab = marsPrefab
        });
        
        recipes.Add(new Recipe {
            planetName = "Jupiter",
            requiredElements = new List<string> { "H2", "He" },
            planetPrefab = jupiterPrefab
        });
        
        recipes.Add(new Recipe {
            planetName = "Saturn",
            requiredElements = new List<string> { "H2", "He" },
            planetPrefab = saturnPrefab
        });
        
        recipes.Add(new Recipe {
            planetName = "Uranus",
            requiredElements = new List<string> { "H2", "He", "CH4" },
            planetPrefab = uranusPrefab
        });
        
        recipes.Add(new Recipe {
            planetName = "Neptune",
            requiredElements = new List<string> { "H2", "He", "CH4" },
            planetPrefab = neptunePrefab
        });
        
        recipes.Add(new Recipe {
            planetName = "Pluto",
            requiredElements = new List<string> { "N2", "CH4", "CO" },
            planetPrefab = plutoPrefab
        });
    }
    
    public Recipe GetRecipeByElements(List<string> elements)
    {
        foreach (Recipe recipe in recipes)
        {
            if (ElementsMatch(recipe.requiredElements, elements))
            {
                return recipe;
            }
        }
        return null;
    }
    
    private bool ElementsMatch(List<string> required, List<string> provided)
    {
        if (required.Count != provided.Count)
            return false;
            
        List<string> requiredCopy = new List<string>(required);
        List<string> providedCopy = new List<string>(provided);
        
        foreach (string element in providedCopy)
        {
            if (!requiredCopy.Remove(element))
                return false;
        }
        
        return requiredCopy.Count == 0;
    }

    void Update()
    {
        
    }
}
