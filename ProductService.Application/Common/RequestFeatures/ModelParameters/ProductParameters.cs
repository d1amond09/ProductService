namespace ProductService.Application.Common.RequestFeatures.ModelParameters;

public class ProductParameters : RequestParameters
{
	public double MinPrice { get; set; }
	public double MaxPrice { get; set; } = double.MaxValue;
	public bool NotValidPriceRange => MaxPrice <= MinPrice;
	public string SearchTerm { get; set; } = string.Empty;
	public ProductParameters()
	{
		OrderBy = "name";
	}
}