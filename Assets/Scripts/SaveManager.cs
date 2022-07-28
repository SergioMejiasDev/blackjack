using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Clase encargada de guardar y cargar las opciones y puntuaciones guardadas en el dispositivo.
/// </summary>
public static class SaveManager
{
    /// <summary>
    /// El código del idioma activo (EN o ES).
    /// </summary>
    [Header("Options")]
    public static string activeLanguage = "EN";
    /// <summary>
    /// Verdadero si el juego está muteado, falso si no lo está.
    /// </summary>
    public static bool muteVolume = false;

    /// <summary>
    /// Partidas jugadas.
    /// </summary>
    [Header("Stats")]
    public static int gamesPlayed = 0;
    /// <summary>
    /// Partidas ganadas.
    /// </summary>
    public static int gamesWon = 0;
    /// <summary>
    /// Partidas perdidas.
    /// </summary>
    public static int gamesLost = 0;
    /// <summary>
    /// Partidas empatadas.
    /// </summary>
    public static int gamesDraw = 0;

    /// <summary>
    /// Función que carga las variables guardadas en el dispositivo.
    /// </summary>
    public static void LoadOptions()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            activeLanguage = PlayerPrefs.GetString("activeLanguage", "EN");
            muteVolume = PlayerPrefs.GetInt("muteVolume", 0) == 0 ? false : true;
            gamesPlayed = PlayerPrefs.GetInt("gamesPlayed", 0);
            gamesWon = PlayerPrefs.GetInt("gamesWon", 0);
            gamesLost = PlayerPrefs.GetInt("gamesLost", 0);
            gamesDraw = PlayerPrefs.GetInt("gamesDraw", 0);

            return;
        }

        SaveData data = new SaveData();

        string path = Application.persistentDataPath + "/Save.sav";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            activeLanguage = data.activeLanguage;
            muteVolume = data.muteVolume;
            gamesPlayed = data.gamesPlayed;
            gamesWon = data.gamesWon;
            gamesLost = data.gamesLost;
            gamesDraw = data.gamesDraw;
        }
    }

    /// <summary>
    /// Función que guarda las variables en el dispositivo.
    /// </summary>
    public static void SaveOptions()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            PlayerPrefs.SetString("activeLanguage", activeLanguage);
            PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);
            PlayerPrefs.SetInt("gamesWon", gamesWon);
            PlayerPrefs.SetInt("gamesLost", gamesLost);
            PlayerPrefs.SetInt("gamesDraw", gamesDraw);

            int tempMute = muteVolume ? 1 : 0;

            PlayerPrefs.SetInt("muteVolume", tempMute);

            return;
        }

        SaveData data = new SaveData
        {
            activeLanguage = activeLanguage,
            muteVolume = muteVolume,
            gamesPlayed = gamesPlayed,
            gamesWon = gamesWon,
            gamesLost = gamesLost,
            gamesDraw = gamesDraw
        };

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Save.sav";

        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, data);

        fileStream.Close();
    }
}