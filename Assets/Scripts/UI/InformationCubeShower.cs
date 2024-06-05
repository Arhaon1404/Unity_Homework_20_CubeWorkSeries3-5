
public class InformationCubeShower : InformationShower<Cube>
{
    protected override void ShowInfo()
    {
        string text = "Создано кубов: " + CountCreateObjects + " Количество активных кубов: " + CountActiveObjects;

        _textField.text = text;
    }
}
