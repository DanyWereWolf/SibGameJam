using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureGeneratorWindow : EditorWindow
{
    [MenuItem("Tools/Texture Generator")]
    public static void Start()
    {
        var window = (TextureGeneratorWindow)GetWindow(typeof(TextureGeneratorWindow), false, "Displacement Texture Generator");
        window.Init();
        window.Show();
    }

    private TextureViewController _textureVC;
    private ArcDisplacementViewController _arcVC;

    private TextureGenerator _textureGenerator;

    public void Init()
    {
        _textureVC = new TextureViewController();
        _arcVC = new ArcDisplacementViewController();

        _textureGenerator = new TextureGenerator();
    }

    private void OnGUI()
    {
        _textureVC.OnGUI();
        EditorGUILayout.Space();

        _arcVC.OnGUI();
        EditorGUILayout.Space();
        if (GUILayout.Button("Create Arc"))
            _textureGenerator.CreateArc(_textureVC.Model, _arcVC.Model);
    }
}


public class TextureGenerator
{
    public void CreateArc(TextureModel textureModel, ArcDisplacementModel arcModel)
    {
        var center = new Vector2(textureModel.Width / 2, textureModel.Height / 2);
        var texture = new Texture2D(textureModel.Width, textureModel.Height);

        var startAngle = arcModel.Arc / 2;
        var startDirection = Vector2.up;

        for (int x = 0; x < textureModel.Width; x++)
            for (int y = 0; y < textureModel.Height; y++)
            {
                var pixel = new Vector2(x, y);
                var distance = Vector2.Distance(center, pixel);
                var direction = (pixel - center).normalized;
                var right = new Vector2(direction.y, -direction.x);

                var angle = Vector2.Angle(startDirection, direction);

                var color = Color.gray;
                color.a = 0;

                var anglePart = angle / (arcModel.Arc / 2);
                var anglePower = arcModel.ArcWidth.Evaluate(anglePart);
                var arcPower = arcModel.ArcPower.Evaluate(anglePart);

                var minRadius = arcModel.Radius - arcModel.Width * anglePower / 2;
                var maxRadius = arcModel.Radius + arcModel.Width * anglePower / 2;

                if (angle < arcModel.Arc / 2 && distance > minRadius && distance < maxRadius)
                {
                    var arcWidth = distance - minRadius;
                    var crossPart = arcWidth / (arcModel.Width * anglePower);
                    var widthPower = arcModel.CrossSection.Evaluate(crossPart);

                    var power = widthPower * arcPower;

                    var directionPower = arcModel.DirectionPower.Evaluate(crossPart);

                    var rightPower = arcModel.RightPower.Evaluate(crossPart);

                    direction *= directionPower;
                    right *= rightPower;

                    var total = (direction + right) * power / 2;
                    color += new Color(total.x, total.y, 0, power);

                    color.a = power;
                }

                texture.SetPixel(x, y, color);
            }

        texture.Apply();

        SaveTextureToFile(texture, textureModel.Name);
    }

    private void SaveTextureToFile(Texture2D texture, string fileName)
    {
        var bytes = texture.EncodeToPNG();
        var file = File.Open(Application.dataPath + "/" + fileName, FileMode.OpenOrCreate);
        var binary = new BinaryWriter(file);
        binary.Write(bytes);
        file.Close();
    }
}



public class TextureModel
{
    public string Name;

    public int Width;
    public int Height;
}

public class TextureViewController
{
    public TextureModel Model;

    private int[] _sizesValues = new int[] { 16, 32, 64, 128, 256, 512, 1024, 2048 };
    private string[] _sizesNames = new string[] { "16", "32", "64", "128", "256", "512", "1024", "2048" };

    public TextureViewController()
    {
        Model = new TextureModel();

        Model.Name = "test.png";
        Model.Width = 256;
        Model.Height = 256;
    }


    public void OnGUI()
    {
        Model.Name = EditorGUILayout.TextField("Name", Model.Name);
        Model.Width = EditorGUILayout.IntPopup("Width", Model.Width, _sizesNames, _sizesValues);
        Model.Height = EditorGUILayout.IntPopup("Height", Model.Height, _sizesNames, _sizesValues);
        EditorGUILayout.Space();
    }
}

public class ArcDisplacementModel
{
    public float Radius = 0f;
    public float Width = 0f;

    public float Arc = 0f;

    public AnimationCurve ArcWidth = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 0f));
    public AnimationCurve ArcPower = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));
    public AnimationCurve CrossSection = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));
    public AnimationCurve DirectionPower = AnimationCurve.Linear(0, 1, 1, 1);
    public AnimationCurve RightPower = AnimationCurve.Linear(0, 0, 1, 0);
}

public class ArcDisplacementViewController
{
    public ArcDisplacementModel Model;

    public ArcDisplacementViewController()
    {
        Model = new ArcDisplacementModel();
    }

    public void Init()
    { }

    public void OnGUI()
    {
        Model.Radius = EditorGUILayout.FloatField("Radius", Model.Radius);
        Model.Width = EditorGUILayout.FloatField("Circle Width", Model.Width);
        Model.Arc = EditorGUILayout.FloatField("Arc", Model.Arc);
        EditorGUILayout.Space();
        Model.ArcWidth = EditorGUILayout.CurveField("Arc Width (Half)", Model.ArcWidth, Color.green, new Rect(0, 0, 1, 1));
        Model.ArcPower = EditorGUILayout.CurveField("Arc Power (Half)", Model.ArcPower, Color.green, new Rect(0, 0, 1, 1));
        Model.CrossSection = EditorGUILayout.CurveField("Cross Section", Model.CrossSection, Color.green, new Rect(0, 0, 1, 1));
        EditorGUILayout.Space();
        Model.DirectionPower = EditorGUILayout.CurveField("Direction Power", Model.DirectionPower, Color.green, new Rect(0, -1, 1, 2));
        Model.RightPower = EditorGUILayout.CurveField("Right Power", Model.RightPower, Color.green, new Rect(0, -1, 1, 2));
    }
}