using UnityEngine;

// Static helper — call from any scene to save, load, or clear player progress.
// Extend this class when new stats are added to Cache.
public static class SaveManager
{
    private const string KEY_HAS_SAVE  = "hasSave";
    private const string KEY_FOOD      = "food";
    private const string KEY_MERCURY   = "mercury";

    // Returns true if a save slot exists
    public static bool HasSave()
    {
        return PlayerPrefs.GetInt(KEY_HAS_SAVE, 0) == 1;
    }

    // Writes current Cache values to PlayerPrefs
    public static void SaveGame(int food, int mercury)
    {
        PlayerPrefs.SetInt(KEY_HAS_SAVE, 1);
        PlayerPrefs.SetInt(KEY_FOOD,     food);
        PlayerPrefs.SetInt(KEY_MERCURY,  mercury);
        PlayerPrefs.Save();
    }

    // Reads saved food value (returns 0 if no save)
    public static int GetFood()
    {
        return PlayerPrefs.GetInt(KEY_FOOD, 0);
    }

    // Reads saved mercury value (returns 0 if no save)
    public static int GetMercury()
    {
        return PlayerPrefs.GetInt(KEY_MERCURY, 0);
    }

    // Wipes all saved data — used on New Beginning and Confirm Restart
    public static void ClearSave()
    {
        PlayerPrefs.DeleteKey(KEY_HAS_SAVE);
        PlayerPrefs.DeleteKey(KEY_FOOD);
        PlayerPrefs.DeleteKey(KEY_MERCURY);
        PlayerPrefs.Save();
    }
}
