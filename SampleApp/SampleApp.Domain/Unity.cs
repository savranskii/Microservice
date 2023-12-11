#region Mocs

internal record Button();
internal record Text();
internal record Color(float r, float g, float b, float a);
internal class Material()
{
    public Color color { get; set; }
}

#endregion

internal class MenuUI
{
    public Text categoryText;

    private int _activeCategory = 0;
    private int _activeMaterial = 0;

    private MaterialCategory[] _categories =
    [
        new MaterialCategory
        {
            Name = "Classic",
            Materials =
            [
                new MaterialElement
                {
                    TexturePath = "/Images/Materials/CC001.jpg",
                    Colors =
                    [
                        new Color(1, 1, 1, 1),
                        new Color(134, 123, 112, 1),
                    ]
                }
            ]
        }
    ];

    public void NextCategory()
    {
        if (_activeCategory == _categories.Length - 1)
            _activeCategory = 0;
        else
            _activeCategory++;

        SetActiveCategory();
    }

    public void PrevCategory()
    {
        if (_activeCategory == 0)
            _activeCategory = _categories.Length - 1;
        else
            _activeCategory--;

        SetActiveCategory();
    }

    private void SetActiveCategory()
    {
        _activeMaterial = 0;
        // categoryText.text = _categories[_activeCategory].Name;
        // rerender materials
    }

    public void SelectMaterial()
    {
        _activeMaterial = 1;
    }

    public void ChangeColor(Button btn)
    {
        var index = 1;
        var color = _categories[_activeCategory].Materials[_activeMaterial].Colors[index];
        _categories[_activeCategory].Materials[_activeMaterial].Material.color = color;
    }
}

internal class MaterialCategory
{
    public string Name { get; init; } = string.Empty;
    public MaterialElement[] Materials { get; init; } = [];
}

internal class MaterialElement
{
    public Color[] Colors { get; init; } = [];
    public string TexturePath { get; init; }

    public int ActiveColor { get; set; }
    public Material Material { get; private set; }

    public MaterialElement()
    {
        ActiveColor = 0;
        // Material = Instantiate(typeof(Material));
        Material.color = Colors[0];
    }
}