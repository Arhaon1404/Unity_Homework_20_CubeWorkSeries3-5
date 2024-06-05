using UnityEngine;
using TMPro;

public abstract class InformationShower<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected TMP_Text _textField;
    [SerializeField] private Spawner<T> _spawner;

    protected int CountActiveObjects;
    protected int CountCreateObjects;

    private void OnEnable()
    {
        _spawner.ObjectCreated += IncreaseCreateObjects;
        _spawner.ObjectIsActived += CalculateActiveObjects;
    }

    private void OnDisable()
    {
        _spawner.ObjectCreated -= IncreaseCreateObjects;
        _spawner.ObjectIsActived -= CalculateActiveObjects;
    }

    protected virtual void ShowInfo() { }

    private void IncreaseCreateObjects()
    {
        CountCreateObjects++;

        ShowInfo();
    }

    private void CalculateActiveObjects(bool isActive)
    {
        if (isActive)
        {
            CountActiveObjects++;
        }
        else
        {
            CountActiveObjects--;
        }

        ShowInfo();
    }
}
