
public class InformationBlowingSphereShower : InformationShower<BlowingSphere>
{
    protected override void ShowInfo()
    {
        string text = "������� c���: " + CountCreateObjects + " ���������� �������� c���: " + CountActiveObjects;

        _textField.text = text;
    }
}
