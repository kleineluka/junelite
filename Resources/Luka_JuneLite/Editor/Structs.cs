#if UNITY_EDITOR
#pragma warning disable CS0414

// imports
using System.Collections.Generic;

// Structures.cs is dedicated towards data structures and classes.. (mostly for JSON)
namespace Luka.JuneLite
{

    // language file entry
    [System.Serializable]
    public class LocalisationEntry
    {
        public string key;
        public string value;
    }

    // language file support
    [System.Serializable]
    public class Localisation_JsonData
    {
        public LocalisationEntry[] entries;
    }


    // a specific scheme (text, tab)
    [System.Serializable]
    public class SchemeIndex
    {
        public string name;
        public List<string> colors;
    }

    // a collection of schemes (text, tab)
    [System.Serializable]
    public class SchemeCollection
    {
        public List<SchemeIndex> schemelist;
    }

    // interface configuration
    [System.Serializable]
    public class InterfaceConfig
    {
        public string tab_theme;
        public string text_theme;
        public string ui_size;
        public string language;
        public bool show_updates;
        public bool show_announcements;
    }

    [System.Serializable]
    public class Config_JsonData
    {
        public InterfaceConfig @interface;
    }

    // metadata stuffs
    [System.Serializable]
    public class AnnouncementData
    {
        public string Header;
        public string Message;
        public bool Active;
    }

    [System.Serializable]
    public class VersionEntry
    {
        public string Name;
        public string Version;
    }

    [System.Serializable]
    public class SocialLink
    {
        public string Name;
        public string Hover;
        public string Link;
        public SocialLink() {}
        public SocialLink (string Name, string Hover, string Link) {
            this.Name = Name;
            this.Hover = Hover;
            this.Link = Link;
        }
    }

    [System.Serializable]
    public class FullMetadata
    {
        public AnnouncementData Announcement;
        public List<VersionEntry> Versions;
        public List<SocialLink> Socials;
    }

}

#endif // UNITY_EDITOR