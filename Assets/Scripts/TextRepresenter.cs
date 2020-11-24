using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextRepresenter : MonoBehaviour
{
	[SerializeField] private Color textColor = Color.white;
    private Text field;

	private void Awake()
	{
		field = GetComponent<Text>();
	}

	public void RepresentUpdate(string text)
	{
		if (field)
		{
			field.text = text;
			field.color = textColor;
		}
	}
}
