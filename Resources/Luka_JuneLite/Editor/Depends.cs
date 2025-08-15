#if UNITY_EDITOR

// imports
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// Depends.cs is dedicated towards helper and utility functions..
namespace Luka.JuneLite
{

    // languages
    public class Languages
    {

        // storing constant variables
        public static readonly string language_store = "Languages";
        public static readonly string language_default = "English";

        // info about the language + store the translations
        private string language;
        private Dictionary<string, string> localised;
        private Dictionary<string, string> localised_fallback;

        // constructor
        public Languages(string language)
        {
            this.language = language;
            load();
            // if the current language can't be loaded, try the default
            if (localised == null)
            {
                this.language = language_default;
                load();
            }
        }

        // load
        private void load()
        {
            try
            {
                // load the language file
                string language_path = Project.project_path + "/" + language_store + "/" + language;
                TextAsset json = Resources.Load<TextAsset>(language_path);
                Localisation_JsonData data = JsonUtility.FromJson<Localisation_JsonData>(json.text);
                localised = new Dictionary<string, string>();
                foreach (var entry in data.entries)
                {
                    localised[entry.key] = entry.value;
                }
                // also load the fallback language
                if (language != language_default)
                {
                    string fallback_path = Project.project_path + "/" + language_store + "/" + language_default;
                    TextAsset fallback_json = Resources.Load<TextAsset>(fallback_path);
                    Localisation_JsonData fallback_data = JsonUtility.FromJson<Localisation_JsonData>(fallback_json.text);
                    localised_fallback = new Dictionary<string, string>();
                    foreach (var entry in fallback_data.entries)
                    {
                        localised_fallback[entry.key] = entry.value;
                    }
                }
                else
                {
                    localised_fallback = null; // no fallback needed for default
                }
            }
            catch (Exception)
            {
                localised = null;
            }
        }

        // speak that language
        public string speak(string key, params object[] args)
        {
            if (localised != null && localised.TryGetValue(key, out string value))
            {
                return string.Format(value, args);
            }
            else if (localised_fallback != null && localised_fallback.TryGetValue(key, out string fallback_value))
            {
                return string.Format(fallback_value, args);
            }
            return key;
        }

    }

    // logging
    public class Pretty
    {

        // basic configuration
        private static readonly string log_format = "%project% @ %time% >> [%colour_start%%kind%%colour_end%] %message%";

        // log kinds
        public struct LogKind
        {
            public static readonly LogKind Info = new LogKind("Info", "#FFFFFF");
            public static readonly LogKind Warning = new LogKind("Warning", "#FFA500");
            public static readonly LogKind Error = new LogKind("Error", "#FF0000");

            public string Name { get; }
            public string HexColor { get; }

            private LogKind(string name, string hexColor)
            {
                Name = name;
                HexColor = hexColor;
            }

        }

        // format a message
        private static string format(string message, string kind)
        {
            return log_format
                .Replace("%project%", Project.project_name)
                .Replace("%time%", DateTime.Now.ToString("HH:mm:ss"))
                .Replace("%colour_start%", $"<color={LogKind.Info.HexColor}>")
                .Replace("%kind%", kind)
                .Replace("%colour_end%", "</color>")
                .Replace("%message%", message);
        }

        // log a message
        public static void print(string message, LogKind kind)
        {
            string formatted_message = format(message, kind.Name);
            switch (kind.Name)
            {
                case "Info":
                    Debug.Log(formatted_message);
                    break;
                case "Warning":
                    Debug.LogWarning(formatted_message);
                    break;
                case "Error":
                    Debug.LogError(formatted_message);
                    break;
                default:
                    Debug.Log(formatted_message);
                    break;
            }
        }

        // because i can't default logkind to null for whatever reason
        public static void print(string message)
        {
            print(message, LogKind.Info);
        }

    }

    // keep track of versions in semantic format
    public class Version
    {

        int major = 0;
        int minor = 0;
        int patch = 0;

        public Version(string version)
        {
            string[] version_split = version.Split('.');
            if (version_split.Length > 0) major = int.Parse(version_split[0]);
            if (version_split.Length > 1) minor = int.Parse(version_split[1]);
            if (version_split.Length > 2) patch = int.Parse(version_split[2]);
        }

        public bool is_newer_than(Version version)
        {
            if (major == version.major && minor == version.minor && patch == version.patch) return true; // current version is same
            if (major > version.major) return true;
            if (major == version.major && minor > version.minor) return true;
            if (major == version.major && minor == version.minor && patch > version.patch) return true;
            return false;
        }

        public string print()
        {
            return $"{major}.{minor}.{patch}";
        }

    }

    // configuration
    public class Config
    {

        // malleable properties
        public Config_JsonData json_data;
        public bool config_loaded = false;

        // constant properties
        private static readonly string config_active = "Active_Config";
        private static readonly string config_default = "Default_Config";
        private static readonly string config_storage = "Data";

        // constructor
        public Config()
        {
            populate();
        }

        // populate the config
        public void populate()
        {
            read();
            if (!config_loaded)
            { // basic safety check
                reset();
                read();
            }
        }

        private bool write(Config_JsonData data)
        {
            string active_config_path = Path.Combine(Project.project_path, config_storage, config_active);
            TextAsset active_config = Resources.Load<TextAsset>(active_config_path);
            string active_disk_path = Path.GetFullPath(AssetDatabase.GetAssetPath(active_config));
            string json_string = JsonUtility.ToJson(data, true);
            try
            {
                File.WriteAllText(active_disk_path, json_string);
                AssetDatabase.Refresh();
                return true;
            }
            catch (Exception e)
            {
                Pretty.print($"Failed to save configuration: {e.Message}", Pretty.LogKind.Error);
                return false;
            }
        }

        public void read()
        {
            string config_path = Project.project_path + "/" + config_storage + "/" + config_active;
            TextAsset config_text = Resources.Load<TextAsset>(config_path);
            json_data = JsonUtility.FromJson<Config_JsonData>(config_text.text);
            config_loaded = (json_data != null);
        }

        public bool save()
        {
            if (json_data != null)
            {
                if (!write(json_data)) return false;
                config_loaded = true;
                return true;
            }
            else
            {
                Pretty.print("Config data is null, cannot save.", Pretty.LogKind.Error);
                return false;
            }
        }

        public bool reset()
        {
            string default_path = Project.project_path + "/" + config_storage + "/" + config_default;
            TextAsset default_text = Resources.Load<TextAsset>(default_path);
            if (default_text != null)
            {
                json_data = JsonUtility.FromJson<Config_JsonData>(default_text.text);
                write(json_data);
                config_loaded = true;
                return true;
            }
            else
            {
                Pretty.print("Default config not found, creating a new one.", Pretty.LogKind.Error);
                json_data = new Config_JsonData();
                return false;
            }
        }

    }

    // metadata
    public class Metadata
    {

        // malleable properties
        public FullMetadata full_metadata;
        public bool metadata_loaded = false;

        // constant properties
        private static readonly string metadata_path = "https://luka.moe/api/unity/shared.json";
        private static readonly string metadata_agent = "Luka/UnityEditor/" + Project.project_name + "/" + Project.version.print();

        // constructor
        public Metadata()
        {
            fetch();
            if (!metadata_loaded)
            {
                reset();
                fallback();
            }
        }

        // fetch
        private void fetch()
        {
#if LUKA_DEVELOPER_MODE
            Pretty.print("Developer mode is active, skipping metadata fetch...", Pretty.LogKind.Info);
            full_metadata = null;
            metadata_loaded = false;
            return;
#endif
            try
            {
                using (var wc = new System.Net.WebClient())
                {
                    wc.Headers.Add("user-agent", metadata_agent);
                    string string_file = wc.DownloadString(metadata_path);
                    if (!string.IsNullOrEmpty(string_file))
                    {
                        full_metadata = JsonUtility.FromJson<FullMetadata>(string_file);
                        metadata_loaded = true;
                    }
                    else
                    {
                        Pretty.print("Failed to fetch metadata, response was empty. Defaulting to fallback..", Pretty.LogKind.Error);
                        full_metadata = null;
                        metadata_loaded = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Pretty.print($"Error fetching metadata: {ex.Message}", Pretty.LogKind.Error);
                full_metadata = null;
                metadata_loaded = false;
            }
        }

        // reset
        private void reset()
        {
            // reset the metadata
            full_metadata = new FullMetadata();
            metadata_loaded = false;
        }

        // default
        private void fallback()
        {
            full_metadata = new FullMetadata();
            full_metadata.Announcement = new AnnouncementData
            {
                Header = "",
                Message = "",
                Active = false
            };
            full_metadata.Socials = new List<SocialLink>
            {
                new SocialLink(Name: "Gumroad", Hover: "My Gumroad Shop!", Link: "https://luka.moe/go/gumroad"),
                new SocialLink(Name: "Booth", Hover: "Booth.jp Store :)", Link: "https://luka.moe/go/booth"),
                new SocialLink(Name: "Jinxxy", Hover: "My Jinxxy", Link: "https://luka.moe/go/jinxxy"),
                new SocialLink(Name: "Payhip", Hover: "My Payhip Store", Link: "https://luka.moe/go/payhip"),
                new SocialLink(Name: "Website", Hover: "My Very Own Website!", Link: "https://luka.moe/"),
                new SocialLink(Name: "Discord", Hover: "Wanna Reach Out To Me?", Link: "https://luka.moe/contact"),
                new SocialLink(Name: "Github", Hover: "My Open Source Projects <3", Link: "https://luka.moe/go/github"),
            };
            full_metadata.Versions = new List<VersionEntry>
            {
                new VersionEntry { Name = Project.project_name, Version = Project.version.print() }
            };
            metadata_loaded = true;
        }

    }

    // prefab manager
    public class Prefabulous
    {

    }

}

# endif // UNITY_EDITOR