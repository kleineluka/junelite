#if UNITY_EDITOR

// Project.cs is dedicated towards project information and metadata.
namespace Luka.JuneLite
{

    // information about this specific project
    public class Project
    {
        public static readonly string project_name = "JuneLite";
        public static readonly string project_path = "Luka_JuneLite";
        public static Version version = new Version("3.0.0");
        public static readonly Version version_ui = new Version("1.1.0"); // just internally
        public static readonly string project_docs = "https://luka.moe/docs/junelite";
    }

}
#endif // UNITY_EDITOR