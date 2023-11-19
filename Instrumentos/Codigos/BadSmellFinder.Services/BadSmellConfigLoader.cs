using BadSmellFinder.Domain.Entities;
using System.Text.Json;
using Util;

namespace BadSmellFinder.Services;

public class BadSmellConfigLoader
{
	string BasePath = "./";
	string BaseExtension = ".bs.config";
	string GetFile(string name) => $"{BasePath}{name}{BaseExtension}";

	public CodeConfig? ReadConfigPath(string configName)
	{
		if (!File.Exists(configName)) return null;

		var file = File.ReadAllText(configName);

		if (file == null) return null;
		return JsonSerializer.Deserialize<CodeConfig>(file);
	}

	public CodeConfig? ReadConfig(string configName)
		=> ReadConfigPath(GetFile(configName));


	public void WriteConfig(CodeConfig config)
	{
		var converter = JsonSerializer.Serialize(config);
		File.WriteAllText(GetFile(config.Name), converter);
	}

	public IEnumerable<CodeConfig> All()
	{
		var configs = new List<CodeConfig>();
		var files = new FileReader($"*{BaseExtension}", recursively: false).Read(BasePath);
		foreach (var file in files)
		{
			var readedFile = ReadConfigPath(file.Key);

			if(readedFile == null) continue;

			configs.Add(readedFile);
		}
		return configs;
	}

	public CodeConfig? TryAddConfig(CodeConfig? config)
	{
		if (config == null || !config.IsValid()) return null;

		WriteConfig(config);

		return config;
	}
}