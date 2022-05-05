using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
  public InputField nameInputField;
  public Slider ageSlider;
  public Dropdown classDropdown;

  string filePath;

  void Awake()
  {
    filePath = Path.Combine(Application.dataPath, "playerData.dat");
  }

  public void SavePlayerData()
  {
    PlayerData playerData = new PlayerData();
    playerData.playerName = nameInputField.text;
    playerData.playerAge = ageSlider.value;
    playerData.playerClass = classDropdown.value;

    Stream stream = new FileStream(filePath, FileMode.Create);
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    binaryFormatter.Serialize(stream, playerData);
    stream.Close();
  }

  public void LoadPlayerData()
  {
    if (File.Exists(filePath))
    {
      Stream stream = new FileStream(filePath, FileMode.Open);
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      PlayerData data = (PlayerData)binaryFormatter.Deserialize(stream);
      stream.Close();

      nameInputField.text = data.playerName;
      ageSlider.value = data.playerAge;
      classDropdown.value = data.playerClass;
    }
  }
}
