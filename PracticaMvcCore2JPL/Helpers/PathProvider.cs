namespace PracticaMvcCore2JPL.Helpers
{
	public enum Folders { images = 0, documents = 1 }
	public class PathProvider
    {
        private IWebHostEnvironment environment;

			public PathProvider(IWebHostEnvironment environment)
			{
				this.environment = environment;
			}



			public string MapPath(string fileName, Folders folder)
			{
				string carpeta = "";
				if (folder == Folders.images)
				{
					carpeta = "images";
				}
				else if (folder == Folders.documents)
				{
					carpeta = "documents";
				}
				string path = Path.Combine(this.environment.WebRootPath
				, carpeta, fileName);
				return path;
			}
    }
}
