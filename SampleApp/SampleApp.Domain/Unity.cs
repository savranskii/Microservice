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
    private MaterialCategory[] _categories =
    [
        new MaterialCategory
        {
            Name = "Classic",
            Materials = new[]
            {
                new MaterialElement
                {
                    TexturePath = "/Images/Materials/CC001.jpg",
                    Colors = new[]
                    {
                        new Color(1, 1, 1, 1),
                        new Color(134, 123, 112, 1),
                    }
                }
            }
        }
    ];

#region Category

    public Text categoryLabel;
    private int _activeCategory = 0;

    // Next button handler
    public void NextCategory()
    {
        if (_activeCategory == _categories.Length - 1)
            _activeCategory = 0;
        else
            _activeCategory++;
        RenderActiveCategory();
    }

    // Prev button handler
    public void PrevCategory()
    {
        if (_activeCategory == 0)
            _activeCategory = _categories.Length - 1;
        else
            _activeCategory--;
        RenderActiveCategory();
    }

    // Render list of materials
    private void RenderActiveCategory()
    {

    }

#endregion
}

internal class MaterialCategory
{
    public string Name { get; init; } = string.Empty;
    public MaterialElement[] Materials { get; set; } = Array.Empty<MaterialElement>();
}

internal class MaterialElement
{
    public string TexturePath { get; init; } = string.Empty;
    public Color[] Colors { get; init; } = Array.Empty<Color>();
    public int ActiveColor { get; private set; }

    public void NextColor()
    {
        if (ActiveColor == Colors.Length - 1)
            ActiveColor = 0;
        else
            ActiveColor++;
    }

    public void PrevColor()
    {
        if (ActiveColor == 0)
            ActiveColor = Colors.Length - 1;
        else
            ActiveColor--;
    }
}
