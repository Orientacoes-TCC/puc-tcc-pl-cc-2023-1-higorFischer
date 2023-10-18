namespace Util;

public class FileReader
{
	IDictionary<string, string> Files = new Dictionary<string, string>();

	public IDictionary<string, string> Read(string path)
	{
		ReadDirectory(path);
		return Files;
	}

	void ReadFile(string file)
		=> Files.Add(file, File.ReadAllText(file));

	void ReadFile(string[] files)
	{
		foreach (var file in files)
		{
			FileAttributes attr = File.GetAttributes(file);
			if (attr.HasFlag(FileAttributes.Directory))
				ReadFile(Directory.GetFiles(file));
			else
				ReadFile(file);
		}
	}

	void ReadDirectory(string[] paths)
	{
		foreach(var path in paths) ReadDirectory(path);
	}

	void ReadDirectory(string path)
	{
		FileAttributes attr = File.GetAttributes(path);

		if (attr.HasFlag(FileAttributes.Directory))
		{
			ReadDirectory(Directory.GetDirectories(path));
			ReadFile(Directory.GetFiles(path, "*.cs"));
		}
		else
			ReadFile(path);
	}
}