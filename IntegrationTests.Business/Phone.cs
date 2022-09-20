namespace IntegrationTests.Business;

public class Phone
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public Brand Brand { get; set; } = null!;

    public override string ToString() => $"{Brand.Name} {Type}";
}