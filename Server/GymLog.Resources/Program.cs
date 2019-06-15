using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace GymLog.Resources
{
    class Program
    {
        static void Main(string[] args)
        {
            var pl = Resource.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true);
            var en = Resource.ResourceManager.GetResourceSet(CultureInfo.GetCultureInfo("en-US"), true, true);

            var rootPath = GoUp(Environment.CurrentDirectory, 5);
            var clientTranslationsPath = $@"{rootPath}\Client\src\assets\i18n\";

            Save(pl, clientTranslationsPath, nameof(pl));
            Save(en, clientTranslationsPath, nameof(en));
        }

        private static void Save(IEnumerable resourceSet, string clientTranslationsPath, string suffix)
        {
            var json = JsonString(resourceSet);
            File.WriteAllText($"{clientTranslationsPath}{suffix}.json", json);
        }

        private static string JsonString(IEnumerable resourceSet)
        {
            var dictionary = resourceSet.Cast<DictionaryEntry>().ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
            return JsonConvert.SerializeObject(dictionary, Formatting.Indented);
        }

        private static string GoUp(string path, int levels)
        {
            if (levels == 0)
                return path;

            var temp = Directory.GetParent(path).ToString();

            return GoUp(temp, levels - 1);
        }
    }
}
