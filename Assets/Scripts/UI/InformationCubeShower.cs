
public class InformationCubeShower : InformationShower<Cube>
{
    protected override void ShowInfo()
    {
        string text = "������� �����: " + CountCreateObjects + " ���������� �������� �����: " + CountActiveObjects;

        _textField.text = text;
    }
}
