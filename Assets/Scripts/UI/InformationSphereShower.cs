
public class InformationSphereShower : InformationShower<Sphere>
{
    protected override void ShowInfo()
    {
        string text = "Создано cфер: " + CountCreateObjects + " Количество активных cфер: " + CountActiveObjects;

        _textField.text = text;
    }
}
