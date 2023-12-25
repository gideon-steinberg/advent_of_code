<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Drawing.Imaging</Namespace>
</Query>

void Main()
{
	var image = new Bitmap("C:\\dev\\day18_modified.png");
	
	var height = image.Height;
	var width = image.Width;
	
	var sum = 0;
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			var pixel = image.GetPixel(i,j);
			if (pixel.R > 0)
			{
				sum++;
			}
		}
	}
	
	sum.Dump();
}

// You can define other methods, fields, classes and namespaces here