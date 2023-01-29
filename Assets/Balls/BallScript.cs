using TMPro;
using UnityEngine;

public class BallScript : MonoBehaviour
{


    public GameObject sphere;
    public GameObject button;
    public TextMeshProUGUI OutputText;
    public TMP_InputField Xinputfield;
    public TMP_InputField Yinputfield;
    public TMP_InputField Zinputfield;





    public void GoThere()
    {
        sphere.name = "Parsa's Ball";

        Transform t = sphere.GetComponent<Transform>();

        int XinputSub = int.Parse(Xinputfield.text);
        int YinputSub = int.Parse(Yinputfield.text);
        int ZinputSub = int.Parse(Zinputfield.text);



        if (XinputSub == 0 || YinputSub == 0 || ZinputSub == 0)
        {

            OutputText.text = "Please input a value in each respective Axis.";

        }

        else
        {

            t.position = new Vector3(XinputSub, YinputSub, ZinputSub);
            OutputText.text = "There We Go!";

        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
