using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEntry : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI rankField;
    [SerializeField] TextMeshProUGUI emailField;
    [SerializeField] TextMeshProUGUI docField;
    [SerializeField] TextMeshProUGUI callField;
    [SerializeField] TextMeshProUGUI petsField;
    [SerializeField] TextMeshProUGUI unplugsField;

    public void SetFields(int rank, string dataFromDreamlo)
    {
        var fields = dataFromDreamlo.Replace('_', ' ').Replace("percent", "%").Split('$');

        playerName.text = fields[8];
        rankField.text = rank.ToString();
        emailField.text = fields[0] + "\n" + fields[1];
        docField.text = fields[2] + "\n" + fields[3];
        callField.text = fields[4] + "\n" + fields[5];
        petsField.text = fields[6];
        unplugsField.text = fields[7];
    }

    public void SetColor(Color color)
    {
        playerName.color = color;
        rankField.color = color;
        emailField.color = color;
        docField.color = color;
        callField.color = color;
        petsField.color = color;
        unplugsField.color = color;
    }
}
