
public class InformationSphereShower : InformationShower<Sphere>
{
    protected override void ShowInfo()
    {
        string text = "������� c���: " + CountCreateObjects + " ���������� �������� c���: " + CountActiveObjects;

        _textField.text = text;
    }
}
