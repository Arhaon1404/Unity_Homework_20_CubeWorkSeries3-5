
public class InformationBlowingSphereShower : InformationShower<BlowingSphere>
{
    protected override void ShowInfo()
    {
        string text = "Создано cфер: " + CountCreateObjects + " Количество активных cфер: " + CountActiveObjects;

        _textField.text = text;
    }
}
