﻿namespace ProductService.Application.Common.RequestFeatures;

public class MetaData
{
	public int CurrentPage { get; set; }
	public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
	public int PageSize { get; set; }
	public int TotalCount { get; set; }
	public bool HasPrevious => CurrentPage > 1;
	public bool HasNext => CurrentPage < TotalPages;
}
