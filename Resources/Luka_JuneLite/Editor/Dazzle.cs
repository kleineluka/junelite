#if UNITY_EDITOR
#pragma warning disable CS0414

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Dazzle.cs is my pretty GUI-building library!
namespace Luka.JuneLite
{

    // font manager
    public class Fonts
    {

        public bool cache_fonts = false; // or just use method to hot-load fonts
        public Font default_font = null;
        public Font light_font = null;
        public Font fun_font = null;

        public Fonts(bool cache_fonts = false)
        {
            this.cache_fonts = cache_fonts;
            if (cache_fonts)
            {
                default_font = font_default();
                light_font = font_light();
                fun_font = font_fun();
            }
        }

        public static Font get_font(string font_name)
        {
            string font_path = Project.project_path + "/Fonts/" + font_name;
            Font font = (Font)Resources.Load(font_path);
            if (font == null)
            {
                Pretty.print("Trying to load font, but file not found: " + font_name, Pretty.LogKind.Error);
                return null;
            }
            return font;
        }

        public static Font font_default()
        {
            return get_font("NotoSans-Regular");
        }

        public static Font font_light()
        {
            return get_font("NotoSans-Light");
        }

        public static Font font_fun()
        {
            return get_font("Nunito-Regular");
        }

    }

    // different layouts for colours in text
    public class TextSchemes
    {

        // public facing variables
        public int chosen_scheme = 0;
        public string[] scheme_colors = null;

        // potential cache
        private SchemeCollection schemes_plain = null;
        public List<string> scheme_list = new List<string>();

        // constructor for creating a list of schemes
        public TextSchemes()
        {
            load_schemes();
        }

        // constructor for getting the layout of a scheme
        public TextSchemes(int chosen_scheme, bool keep_cached = false)
        {
            load_schemes();
            // ensure it is within bounds
            if (chosen_scheme < 0 || chosen_scheme >= scheme_list.Count)
            {
                chosen_scheme = 0;
            }
            // if the chosen scheme is 0 (default), decide between dark or light based on environment
            if (chosen_scheme == 0)
            {
                this.chosen_scheme = Colours.is_dark() ? scheme_list.IndexOf("Dark") : scheme_list.IndexOf("Light");
            }
            else
            {
                this.chosen_scheme = chosen_scheme - 1;
            }
            // load colours and choose if it should be cached
            this.scheme_colors = get_colors();
            if (!keep_cached) unload_schemes();
        }

        // same thing but diff input, woot woot
        public TextSchemes(string chosen_scheme, bool keep_cached = false)
        {
            load_schemes();
            int scheme_index = scheme_list.IndexOf(chosen_scheme);
            if (scheme_index == -1) scheme_index = 0;
            if (scheme_index == 0)
            {
                this.chosen_scheme = Colours.is_dark() ? scheme_list.IndexOf("Dark") : scheme_list.IndexOf("Light");
            }
            else
            {
                this.chosen_scheme = scheme_index;
            }
            this.scheme_colors = get_colors();
            if (!keep_cached) unload_schemes();
        }

        // load the schemes from the json file
        private void load_schemes()
        {
            string schemes_path = Project.project_path + "/Data/Text_Schemes";
            TextAsset schemes_json = Resources.Load<TextAsset>(schemes_path);
            this.schemes_plain = JsonUtility.FromJson<SchemeCollection>(schemes_json.text);
            if (scheme_list.Count > 0) scheme_list.Clear();
            foreach (var scheme in schemes_plain.schemelist) scheme_list.Add(scheme.name);
        }

        // unload schemes to save on cache
        private void unload_schemes()
        {
            this.schemes_plain = null;
            this.scheme_list.Clear();
        }

        // get the colours of a scheme
        private string[] get_colors()
        {
            if (this.schemes_plain == null) load_schemes();
            return schemes_plain.schemelist[chosen_scheme].colors.ToArray();
        }

        // color text based on the given scheme
        public string texter(string text)
        {
            string[] colors = this.scheme_colors;
            if (colors.Length == 1) return "<color=" + colors[0] + ">" + text + "</color>";
            text = Regex.Replace(text, "<.*?>", string.Empty);
            string result = "";
            int index = 0;
            foreach (char c in text)
            {
                if (index > colors.Length - 1)
                {
                    index = 0; // reset weee
                }
                result += "<color=" + colors[index] + ">" + c + "</color>";
                if (c != ' ') index++;
            }
            return result;
        }

        // texting with a specific theme
        public string texter(string text, int chosen_scheme)
        {
            TextSchemes text_scheme = new TextSchemes(chosen_scheme, true);
            return text_scheme.texter(text);
        }

    }

    // different layouts for colours in tabs
    public class TabSchemes
    {

        // public facing variables
        public int chosen_scheme = 0;
        public string[] scheme_colors = null;

        // potential cache
        private SchemeCollection schemes_plain = null;
        public List<string> scheme_list = new List<string>();

        // constructor for creating a list of schemes
        public TabSchemes()
        {
            load_schemes();
        }

        // constructor for getting the layout of a scheme
        public TabSchemes(int chosen_scheme, bool keep_cached = false)
        {
            load_schemes();
            // ensure it is within bounds
            if (chosen_scheme < 0 || chosen_scheme >= scheme_list.Count)
            {
                chosen_scheme = 0;
            }
            // if the chosen scheme is 0 (default), decide between dark or light based on environment
            if (chosen_scheme == 0)
            {
                this.chosen_scheme = Colours.is_dark() ? scheme_list.IndexOf("Alternating Dark") : scheme_list.IndexOf("Alternating Light");
            }
            else
            {
                this.chosen_scheme = chosen_scheme - 1;
            }
            // load colours and choose if it should be cached
            this.scheme_colors = get_colors();
            if (!keep_cached) unload_schemes();
        }

        // same constructor but take a string instead basically
        public TabSchemes(string chosen_scheme, bool keep_cached = false)
        {
            load_schemes();
            int scheme_index = scheme_list.IndexOf(chosen_scheme);
            this.chosen_scheme = scheme_index;
            this.scheme_colors = get_colors();
            if (!keep_cached) unload_schemes();
        }

        // load the schemes from the json file
        private void load_schemes()
        {
            string schemes_path = Project.project_path + "/Data/Tab_Schemes";
            TextAsset schemes_json = Resources.Load<TextAsset>(schemes_path);
            this.schemes_plain = JsonUtility.FromJson<SchemeCollection>(schemes_json.text);
            foreach (var scheme in schemes_plain.schemelist)
            {
                scheme_list.Add(scheme.name);
            }
        }

        // unload the schemes to save on cache
        private void unload_schemes()
        {
            this.schemes_plain = null;
            this.scheme_list.Clear();
        }

        // get the colours of a scheme
        private string[] get_colors()
        {
            if (this.schemes_plain == null) load_schemes();
            string[] schemes_colours = schemes_plain.schemelist[chosen_scheme].colors.ToArray();
            for (int i = 0; i < schemes_colours.Length; i++)
            { 
                if (schemes_colours[i] == "default")
                {
                    // replace default with the gui default colour dynamically.. (to account for dark/light themes, or updates)
                    schemes_colours[i] = "#" + ColorUtility.ToHtmlStringRGB(Colours.get_background());
                }
            }
            return schemes_colours;
        }

    }

    // manage various styles used throughout the UI
    public class Styler
    {

        // public-facing read only values (like below, but used across other classes)
        public static readonly int social_icon_size = 30;
        public static readonly int social_icon_spacing = 10;
        public static readonly int social_icon_row_padding = 60;
        public static readonly double social_icon_factor_min = 1.0;
        public static readonly double social_icon_factor_max = 1.25;
        public static readonly float social_icon_highlight = 1.4f;

        // read only values for easy configuration
        private static readonly float tab_primary_height_compact = 30f;
        private static readonly int tab_primary_font_size_compact = 16;
        private static readonly Vector2 tab_primary_offset_compact = new Vector2(26f, -1f);
        private static readonly float tab_primary_height_comfy = 40f;
        private static readonly int tab_primary_font_size_comfy = 20;
        private static readonly Vector2 tab_primary_offset_comfy = new Vector2(28.0f, -2.0f);
        private static readonly float tab_primary_height_big = 50f;
        private static readonly int tab_primary_font_size_big = 24;
        private static readonly Vector2 tab_primary_big_comfy = new Vector2(30f, -3f);
        private static readonly float tab_sub_height_compact = 20f;
        private static readonly int tab_sub_font_size_compact = 12;
        private static readonly Vector2 tab_sub_offset_compact = new Vector2(20f, -2f);
        private static readonly float tab_sub_height_comfy = 28f;
        private static readonly int tab_sub_font_size_comfy = 14;
        private static readonly Vector2 tab_sub_offset_comfy = new Vector2(20f, -2f);
        private static readonly float tab_sub_height_big = 32f;
        private static readonly int tab_sub_font_size_big = 16;
        private static readonly Vector2 tab_sub_big_comfy = new Vector2(20f, -2f);

        // passed and stored
        private Fonts font_manager = null;
        private Config config_manager = null;

        // have them all as null, cache on demand, but not static (!)
        private GUIStyle style_tab_primary = null;
        private GUIStyle style_tab_sub = null;
        private GUIStyle style_tooltip_background = null;
        private GUIStyle style_tooltip_text = null;
        private GUIStyle style_header_beeg = null;
        private GUIStyle style_header_smol = null;
        private GUIStyle style_announcement_header = null;
        private GUIStyle style_announcement_body = null;
        private GUIStyle style_docs = null;

        // pass theme for fonting
        public Styler(ref Fonts font_manager, ref Config config_manager)
        {
            this.font_manager = font_manager;
            this.config_manager = config_manager;
        }

        // flush cache
        public void flush()
        {
            style_tab_primary = null;
            style_tab_sub = null;
            style_tooltip_background = null;
            style_tooltip_text = null;
            style_header_beeg = null;
            style_header_smol = null;
            style_announcement_header = null;
            style_announcement_body = null;
            style_docs = null;
        }

        // get the style for the primary tab
        public ref GUIStyle load_style_tab_primary()
        {
            if (style_tab_primary == null)
            {
                style_tab_primary = new GUIStyle("ShurikenModuleTitle");
                style_tab_primary.richText = true;
                style_tab_primary.normal.background = null;
                style_tab_primary.font = this.font_manager.default_font;
                switch (config_manager?.json_data?.@interface?.ui_size)
                {
                    case "Compact":
                        style_tab_primary.fixedHeight = tab_primary_height_compact;
                        style_tab_primary.fontSize = tab_primary_font_size_compact;
                        style_tab_primary.contentOffset = tab_primary_offset_compact;
                        break;
                    case "Big Screen":
                        style_tab_primary.fixedHeight = tab_primary_height_big;
                        style_tab_primary.fontSize = tab_primary_font_size_big;
                        style_tab_primary.contentOffset = tab_primary_big_comfy;
                        break;
                    case "Comfy":
                    default: // default if config issue
                        style_tab_primary.fixedHeight = tab_primary_height_comfy;
                        style_tab_primary.fontSize = tab_primary_font_size_comfy;
                        style_tab_primary.contentOffset = tab_primary_offset_comfy;
                        break;
                }
            }
            return ref style_tab_primary;
        }

        // get the style for a sub tab
        public ref GUIStyle load_style_tab_sub()
        {
            if (style_tab_sub == null)
            {
                style_tab_sub = new GUIStyle("ShurikenModuleTitle");
                style_tab_sub.border = new RectOffset(15, 7, 4, 4);
                style_tab_sub.richText = true;
                style_tab_sub.font = this.font_manager.default_font;
                switch (config_manager?.json_data?.@interface?.ui_size)
                {
                    case "Compact":
                        style_tab_sub.fixedHeight = tab_sub_height_compact;
                        style_tab_sub.fontSize = tab_sub_font_size_compact;
                        style_tab_sub.contentOffset = tab_sub_offset_compact;
                        break;
                    case "Big Screen":
                        style_tab_sub.fixedHeight = tab_sub_height_big;
                        style_tab_sub.fontSize = tab_sub_font_size_big;
                        style_tab_sub.contentOffset = tab_sub_big_comfy;
                        break;
                    case "Comfy":
                    default: // default if config issue
                        style_tab_sub.fixedHeight = tab_sub_height_comfy;
                        style_tab_sub.fontSize = tab_sub_font_size_comfy;
                        style_tab_sub.contentOffset = tab_sub_offset_comfy;
                        break;
                }
            }
            return ref style_tab_sub;
        }

        // get the style for a tooltip background
        public ref GUIStyle load_style_tooltip_background(ref Theme theme)
        {
            if (style_tooltip_background == null)
            {
                style_tooltip_background = new GUIStyle();
                style_tooltip_background.normal = new GUIStyleState()
                {
                    background = theme.image_manager.get_tile_background(),
                    textColor = Color.white
                };
                style_tooltip_background.padding = new RectOffset(10, 10, 5, 5);
            }
            return ref style_tooltip_background;
        }

        // get the style for tooltip texts
        public ref GUIStyle load_style_tooltip_text(ref Theme theme)
        {
            if (style_tooltip_text == null)
            {
                style_tooltip_text = new GUIStyle(GUI.skin.label);
                style_tooltip_text.font = theme.font_manager.default_font;
                style_tooltip_text.fontSize = 12;
                style_tooltip_text.fontStyle = FontStyle.Normal;
                style_tooltip_text.alignment = TextAnchor.MiddleCenter;
                style_tooltip_text.wordWrap = true;
                style_tooltip_text.richText = true;
            }
            return ref style_tooltip_text;
        }

        // get the style for header beeg text
        public ref GUIStyle load_style_header_beeg(ref Theme theme)
        {
            if (style_header_beeg == null)
            {
                style_header_beeg = new GUIStyle(GUI.skin.label);
                style_header_beeg.font = theme.font_manager.fun_font;
                style_header_beeg.fontSize = 24;
                style_header_beeg.alignment = TextAnchor.MiddleCenter;
                style_header_beeg.richText = true;
                style_header_beeg.normal.textColor = Colours.get_foreground();
            }
            return ref style_header_beeg;
        }

        // get the style for a smol header
        public ref GUIStyle load_style_header_smol(ref Theme theme)
        {
            if (style_header_smol == null)
            {
                style_header_smol = new GUIStyle(GUI.skin.label);
                style_header_smol.font = theme.font_manager.fun_font;
                style_header_smol.fontSize = 12;
                style_header_smol.alignment = TextAnchor.MiddleCenter;
                style_header_smol.richText = true;
                style_header_smol.normal.textColor = Colours.get_foreground();
            }
            return ref style_header_smol;
        }

        // get the style for an announcement header
        public ref GUIStyle load_style_announcement_header(ref Theme theme)
        {
            if (style_announcement_header == null)
            {
                style_announcement_header = new GUIStyle(GUI.skin.label);
                style_announcement_header.font = theme.font_manager.fun_font;
                style_announcement_header.fontSize = 16;
                style_announcement_header.alignment = TextAnchor.MiddleCenter;
                style_announcement_header.richText = true;
                style_announcement_header.wordWrap = true;
                style_announcement_header.normal.textColor = Colours.get_foreground();
                style_announcement_header.padding = new RectOffset(10, 10, 25, 5);
            }
            return ref style_announcement_header;
        }

        // get a style for an announement body
        public ref GUIStyle load_style_announcement_body(ref Theme theme)
        {
            if (style_announcement_body == null)
            {
                style_announcement_body = new GUIStyle(GUI.skin.label);
                style_announcement_body.font = theme.font_manager.fun_font;
                style_announcement_body.fontSize = 12;
                style_announcement_body.alignment = TextAnchor.MiddleCenter;
                style_announcement_body.richText = true;
                style_announcement_body.wordWrap = true;
                style_announcement_body.normal.textColor = Colours.get_foreground();
                style_announcement_body.padding = new RectOffset(10, 10, 5, 25);
            }
            return ref style_announcement_body;
        }

        // get a style for a docs
        public ref GUIStyle load_style_docs(ref Theme theme)
        {
            if (style_docs == null)
            {
                style_docs = new GUIStyle(GUI.skin.label);
                style_docs.font = theme.font_manager.fun_font;
                style_docs.fontSize = 12;
                style_docs.alignment = TextAnchor.MiddleCenter;
                style_docs.richText = true;
                style_docs.wordWrap = true;
                style_docs.normal.textColor = Colours.get_foreground();
                style_docs.padding = new RectOffset(10, 10, 5, 5);
            }
            return ref style_docs;
        }

    }

    // reusable images
    public class Images
    {

        // on-demand images
        private Texture2D tile_background = null;
        private Texture2D tile_hover_overlay = null;
        private Texture2D tile_window_controls = null;

        // empty constructor
        public Images() { }

        // clear cache
        public void flush()
        {
            tile_background = null;
            tile_hover_overlay = null;
            tile_window_controls = null;
        }

        // get the background image for a tile
        public ref Texture2D get_tile_background()
        {
            string tile_background_path = Project.project_path + "/Images/Tile_Background";
            if (tile_background == null) tile_background = (Texture2D)Resources.Load(tile_background_path);
            return ref tile_background;
        }

        // get the hover overlay for a tile
        public ref Texture2D get_tile_hover_overlay()
        {
            string tile_hover_overlay_path = Project.project_path + "/Images/Tile_Hover_Overlay";
            if (tile_hover_overlay == null) tile_hover_overlay = (Texture2D)Resources.Load(tile_hover_overlay_path);
            return ref tile_hover_overlay;
        }

        // get the hover overlay for a tile
        public ref Texture2D get_tile_window_controls()
        {
            string tile_window_controls_path = Project.project_path + "/Images/Tile_Window_Controls";
            if (tile_window_controls == null) tile_window_controls = (Texture2D)Resources.Load(tile_window_controls_path);
            return ref tile_window_controls;
        }

        // helper functions for getting various types of images
        public static Texture2D get_social_icon(SocialLink item)
        {
            string images_path = Project.project_path + "/Images/Social_" + item.Name;
            return (Texture2D)Resources.Load(images_path);
        }

    }

    // what the user's current setup looks like..
    public class Theme
    {

        // managers for various ui-related features
        public Fonts font_manager = null;
        public TextSchemes text_manager = null;
        public TabSchemes tab_manager = null;
        public Styler styler_manager = null;
        public Images image_manager = null;
        public Config config_manager = null;
        public Languages language_manager = null;
        public Metadata metadata_manager = null;

        // constructor (w/ default theme)
        public Theme(ref Config config_manager, ref Languages language_manager, ref Metadata metadata_manager)
        {
            this.config_manager = config_manager;
            this.language_manager = language_manager;
            this.metadata_manager = metadata_manager;
            init_default();
        }

        // default theme for no config (i guess for testing..)
        private void init_default()
        {
            font_manager = new Fonts();
            text_manager = new TextSchemes(config_manager.json_data.@interface.text_theme, true);
            tab_manager = new TabSchemes(config_manager.json_data.@interface.tab_theme, true);
            styler_manager = new Styler(ref font_manager, ref config_manager);
            image_manager = new Images();
        }

    }

    // tabs, sub tabs, everything bout em
    public class Tab
    {

        // read only values for easy configuration
        private static readonly float tab_primary_arrow_x_compact = 10f;
        private static readonly float tab_primary_arrow_y_compact = 15f;
        private static readonly float tab_primary_arrow_x_comfy = 10f;
        private static readonly float tab_primary_arrow_y_comfy = 20f;
        private static readonly float tab_primary_arrow_x_big = 10f;
        private static readonly float tab_primary_arrow_y_big = 25f;
        private static readonly float tab_sub_arrow_x_compact = 5f;
        private static readonly float tab_sub_arrow_y_compact = -7f;
        private static readonly float tab_sub_arrow_x_comfy = 5f;
        private static readonly float tab_sub_arrow_y_comfy = -2f;
        private static readonly float tab_sub_arrow_x_big = 5f;
        private static readonly float tab_sub_arrow_y_big = -1f;

        // there are different kinds of tabs..
        public enum tab_sizes
        {
            Primary, Sub
        };

        // and there are various (performance) ratings each tab can have
        public enum tab_ratings
        {
            Default, Bad, Medium, Good
        };

        // properties about tab
        private Material material = null;
        private Theme theme = null;
        private int tab_size = (int)tab_sizes.Primary;
        private int tab_index = -1;
        private string tab_label = null;

        // tab state
        public bool is_expanded = false;
        public bool is_active = false;

        // constructor for a tab
        public Tab(ref Material material, ref Theme theme, int tab_size, int tab_index, string tab_label)
        {
            this.theme = theme;
            this.material = material;
            this.tab_size = tab_size;
            this.tab_index = tab_index;
            this.tab_label = tab_label;
        }

        // wrapper to draw the tab
        public void draw()
        {
            string tab_text = this.theme.text_manager.texter(this.tab_label);
            switch (tab_size)
            {
                case (int)tab_sizes.Primary:
                    draw_primary_tab(tab_text);
                    break;
                case (int)tab_sizes.Sub:
                    draw_sub_tab(tab_text);
                    break;
            }
        }

        // helper to draw the background texture of a tab
        private Texture2D make_background(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        // foldout part of the primary tab
        private void draw_primary_tab(string tab_text)
        {
            GUILayout.BeginHorizontal();
            GUIStyle primary_style = this.theme.styler_manager.load_style_tab_primary();
            Rect rect = GUILayoutUtility.GetRect(5.0f, primary_style.fixedHeight, primary_style, GUILayout.ExpandWidth(true));
            //rect.y += 2.5f;
            Color cache_col = GUI.color;
            // set custom background color
            Color tab_background_color = Colours.get_background();
            if (this.theme.tab_manager.scheme_colors != null)
            {
                string[] current_scheme_colors = this.theme.tab_manager.scheme_colors;
                if (current_scheme_colors.Length > 0)
                {
                    string color_hex;
                    if (current_scheme_colors.Length == 1)
                    {
                        color_hex = current_scheme_colors[0];
                    }
                    else
                    {
                        int color_index = this.tab_index >= 0 ? this.tab_index % current_scheme_colors.Length : 0;
                        color_hex = current_scheme_colors[color_index];
                    }
                    if (ColorUtility.TryParseHtmlString(color_hex, out Color parsed_color))
                    {
                        tab_background_color = parsed_color;
                    }
                    else
                    {
                        Pretty.print("Failed to parse color hex: " + color_hex, Pretty.LogKind.Error);
                    }
                }
            }
            GUI.color = tab_background_color;
            // draw the background
            GUI.DrawTexture(rect, theme.image_manager.get_tile_background(), ScaleMode.StretchToFill);
            // reset GUI.color
            GUI.color = cache_col;
            // draw the box text
            GUI.Box(rect, tab_text, primary_style);
            Event currentEvent = Event.current;
            // calculate arrow position based on tab size
            float tab_primary_arrow_x;
            float tab_primary_arrow_y;
            switch (theme.config_manager?.json_data?.@interface?.ui_size)
            {
                case "Compact":
                    tab_primary_arrow_x = tab_primary_arrow_x_compact;
                    tab_primary_arrow_y = tab_primary_arrow_y_compact;
                    break;
                case "Big Screen":
                    tab_primary_arrow_x = tab_primary_arrow_x_big;
                    tab_primary_arrow_y = tab_primary_arrow_y_big;
                    break;
                case "Comfy":
                default: // default if config issue
                    tab_primary_arrow_x = tab_primary_arrow_x_comfy;
                    tab_primary_arrow_y = tab_primary_arrow_y_comfy;
                    break;
            }
            Rect arrowRect = new Rect(rect.x + tab_primary_arrow_x, rect.y + tab_primary_arrow_y, 100.0f, 0.0f);
            if (currentEvent.type == EventType.Repaint)
            {
                EditorStyles.foldout.Draw(arrowRect, false, false, is_expanded, false);
            }
            if (currentEvent.type == EventType.MouseDown && rect.Contains(currentEvent.mousePosition))
            {
                is_expanded = !is_expanded;
                currentEvent.Use();
            }
            GUILayout.Space(20f);
            GUILayout.EndHorizontal();
        }

        // draw the sub tab
        private void draw_sub_tab(string tab_text)
        {
            GUILayout.BeginHorizontal();
            GUIStyle sub_style = this.theme.styler_manager.load_style_tab_sub();
            Rect rect = GUILayoutUtility.GetRect(16f, sub_style.fixedHeight, sub_style);
            rect.position = new Vector2(rect.position.x, rect.position.y);
            rect.y += 2.5f;
            GUI.Box(rect, tab_text, sub_style);
            Event currentEvent = Event.current;
            // calculate arrow position based on tab size
            float tab_sub_arrow_x;
            float tab_sub_arrow_y;
            switch (theme.config_manager?.json_data?.@interface?.ui_size)
            {
                case "Compact":
                    tab_sub_arrow_x = tab_sub_arrow_x_compact;
                    tab_sub_arrow_y = tab_sub_arrow_y_compact;
                    break;
                case "Big Screen":
                    tab_sub_arrow_x = tab_sub_arrow_x_big;
                    tab_sub_arrow_y = tab_sub_arrow_y_big;
                    break;
                case "Comfy":
                default: // default if config issue
                    tab_sub_arrow_x = tab_sub_arrow_x_comfy;
                    tab_sub_arrow_y = tab_sub_arrow_y_comfy;
                    break;
            }
            Rect arrowRect = new Rect(rect.x + tab_sub_arrow_x, rect.y + tab_sub_arrow_y, 28f, 28f);
            if (currentEvent.type == EventType.Repaint)
            {
                EditorStyles.foldout.Draw(arrowRect, false, false, is_expanded, false);
            }
            if (currentEvent.type == EventType.MouseDown && rect.Contains(currentEvent.mousePosition))
            {
                is_expanded = !is_expanded;
                currentEvent.Use();
            }
            GUILayout.EndHorizontal();
        }

    }

    // tile! tile tile tile!
    public class Tile
    {

        // properties about the tile
        public SocialLink item = null; // public for sorting
        private Theme theme = null;
        private Texture2D tile_icon = null;

        // constructor for a tile
        public Tile(SocialLink item, ref Theme theme)
        {
            this.item = item;
            this.theme = theme;
            this.tile_icon = Images.get_social_icon(item);
        }

        // draw the tile
        public void draw(Rect background_rectangle, int icon_index, int tiles_count, int icons_per_row)
        {
            // icon spacing
            int icon_size = Components.casted_clamp(Components.height_offset(), Styler.social_icon_size, Styler.social_icon_factor_min, Styler.social_icon_factor_max);
            float rowSpacing = Styler.social_icon_spacing;
            // base the y position on how many rows there are
            int totalRows = Mathf.CeilToInt((float)tiles_count / icons_per_row);
            float totalHeight = totalRows * (icon_size + rowSpacing) - rowSpacing;
            float startY = background_rectangle.y + (background_rectangle.height - totalHeight) / 2;
            int row = icon_index / icons_per_row;
            int column = icon_index % icons_per_row;
            // center the row
            int currentRowIcons = Mathf.Min(icons_per_row, tiles_count - row * icons_per_row);
            float availableWidth = background_rectangle.width;
            float rowWidth = currentRowIcons * (icon_size + rowSpacing) - rowSpacing;
            float rowStartX = background_rectangle.x + (availableWidth - rowWidth) / 2;
            // build the icon
            Rect icon_rectangle = new Rect(
                rowStartX + column * (icon_size + rowSpacing),
                startY + row * (icon_size + rowSpacing),
                icon_size,
                icon_size
            );
            // hover effect
            if (icon_rectangle.Contains(Event.current.mousePosition))
            {
                // hover glow
                Rect icon_rec_highlight = icon_rectangle;
                float adjust_highlight_x = icon_rec_highlight.width * Styler.social_icon_highlight;
                float adjust_highlight_y = icon_rec_highlight.height * Styler.social_icon_highlight;
                icon_rec_highlight.width = adjust_highlight_x;
                icon_rec_highlight.height = adjust_highlight_y;
                icon_rec_highlight.x -= (adjust_highlight_x - icon_rectangle.width) / 2f;
                icon_rec_highlight.y -= (adjust_highlight_y - icon_rectangle.height) / 2f;
                GUI.DrawTexture(icon_rec_highlight, theme.image_manager.get_tile_hover_overlay(), ScaleMode.StretchToFill);
                // tooltip (scale with characters and scale y pos with rows)
                Vector2 mousePosition = Event.current.mousePosition;
                float tooltipWidth = 80f + (item.Hover.Length * 5f);
                float tooltipHeight = 30f;
                Rect tooltip_rectangle = new Rect(
                    mousePosition.x - tooltipWidth / 2,
                    icon_rectangle.y - tooltipHeight - 5f,
                    tooltipWidth,
                    tooltipHeight
                );
                Color cache_col = GUI.color;
                GUI.color = Colours.get_background();
                GUI.Box(tooltip_rectangle, "", theme.styler_manager.load_style_tooltip_background(ref theme));
                GUI.color = cache_col;
                GUI.Label(tooltip_rectangle, theme.text_manager.texter(item.Hover, 5), theme.styler_manager.load_style_tooltip_text(ref theme));
                // on click of the icon, open the link
                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                {
                    Application.OpenURL(item.Link);
                }
            }
            // draw the icon
            GUI.DrawTexture(icon_rectangle, tile_icon, ScaleMode.ScaleToFit);
        }
        
    }

    // row of tiles
    public class Row
    {

        // properties about the row
        private List<Tile> tiles = new List<Tile>();
        private Theme theme = null;
        private List<SocialLink> socials = null;

        // to be filled as it is constructed
        private Rect background_rectangle;

        // constructor for a row
        public Row(ref List<SocialLink> socials, ref Theme theme) {
            foreach (SocialLink item in socials) {
                tiles.Add(new Tile(item, ref theme));
            }
            this.theme = theme;
        }

        // draw the row
        public void draw() {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            int icon_size = Components.casted_clamp(Components.height_offset(), Styler.social_icon_size, Styler.social_icon_factor_min, Styler.social_icon_factor_max);
            // calculate the number of icons per row
            float availableWidth = EditorGUIUtility.currentViewWidth - Styler.social_icon_row_padding; 
            int icons_per_row = Mathf.Max(1, Mathf.FloorToInt((availableWidth + Styler.social_icon_spacing - 30) / (icon_size + Styler.social_icon_spacing)));
            // calculate rows
            int totalRows = Mathf.CeilToInt((float)tiles.Count / icons_per_row);
            float rowHeight = icon_size * 1.25f;
            float totalHeight = 60f + ((totalRows - 1.0f) * rowHeight * 1.1f);
            // adjust background height based on the number of rows
            background_rectangle = EditorGUILayout.GetControlRect(false, totalHeight); 
            Color cache_col = GUI.color;
            GUI.color = Colours.get_background();
            GUI.DrawTexture(new Rect(background_rectangle.x, background_rectangle.y, background_rectangle.width, background_rectangle.height), theme.image_manager.get_tile_background(), ScaleMode.StretchToFill);
            GUI.color = cache_col;
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].draw(background_rectangle, i, tiles.Count, icons_per_row);
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(20f); // match unity's default shader padding.. sigh.
            EditorGUILayout.EndHorizontal();
        }

    }

    // create a scrollable area
    public class Scrollable {

        // properties about the scrollable area
        private float scrollbar_height = 300f; // suggestion?

        // keep track of the scrolling
        private static Vector2 scrollbar_position = Vector2.zero;

        // constructor
        public Scrollable(int scrollbar_height) {
            this.scrollbar_height = scrollbar_height;
        }

        // start the scrollable area
        public void open() {
            scrollbar_position = EditorGUILayout.BeginScrollView(scrollbar_position, GUILayout.Height(scrollbar_height));
        }

        // end the scrollable area
        public void close() {
            EditorGUILayout.EndScrollView();
        }

    }

    // colour manager for dark/light mode
    public class Colours
    {

        public static bool is_dark()
        {
            return EditorGUIUtility.isProSkin;
        }

        public static string texture_selector(string tex_name)
        {
            return tex_name + (is_dark() ? "_Dark" : "_Light");
        }

        public static Color get_background()
        { // a tile on a background
            if (is_dark()) return new Color(0.133f, 0.133f, 0.133f);
            return new Color(0.925f, 0.925f, 0.925f);
        }

        public static Color get_foreground()
        { // text on a tile
            if (is_dark()) return new Color(0.913f, 0.913f, 0.913f);
            return new Color(0.152f, 0.152f, 0.152f);
        }

    }

    // various plug and play components
    public class Components
    {

        // ---- type line
        public static void draw_divider()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

        // start foldout
        public static void start_foldout()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical("GroupBox");
        }

        // end foldout
        public static void end_foldout(bool extra_padding = true)
        {
            EditorGUILayout.EndVertical();
            if (extra_padding) GUILayout.Space(15f);
            EditorGUILayout.EndHorizontal();
        }

        // start group box
        public static void start_group()
        {
            EditorGUILayout.BeginVertical("GroupBox");
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
        }

        // end group box
        public static void end_group()
        {
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        // start dynamic disable 
        public static void start_dynamic_disable(bool disable_conditional)
        {
            EditorGUI.BeginDisabledGroup(disable_conditional == true);
        }

        // end dynamic disable
        public static void end_dynamic_disable(bool disable_conditional)
        {
            if (disable_conditional) EditorGUI.EndDisabledGroup();
        }

        // cull text given a certain length
        public static string culled_text(string text, int max_length)
        {
            if (text.Length <= max_length) return text;
            return text.Substring(0, max_length - 3) + "...";
        }

        // clamp a value between two values (double -> int)
        public static int casted_clamp(int initial, int size, double min, double max)
        {
            int lower_end = (int)((double)size * min);
            int upper_end = (int)((double)size * max);
            return Mathf.Clamp(initial, lower_end, upper_end);
        }

        // dynamically update height offset because it only captures the static width
        public static int height_offset()
        {
            return Components.casted_clamp((int)(Screen.width / 6.0), 100, 0.75, 1.25) - 50; // 25 px * 2
        }

        // start a module toggle area (basically, a box with centered options in it)
        public static void start_feature_box()
        {
            EditorGUILayout.BeginVertical("GroupBox");
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
        }

        // end a module toggle area
        public static void end_feature_box()
        {
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

    }

    // plug and play header
    public class Header
    {

        // values needed..
        private Theme theme = null;

        // to be filled in
        private Rect background_rectangle;

        // constructor
        public Header(ref Theme theme)
        {
            this.theme = theme;
        }

        // draw the header
        public void draw()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            background_rectangle = EditorGUILayout.GetControlRect(false, 75);
            Color cache_col = GUI.color;
            GUI.color = Colours.get_background();
            GUI.DrawTexture(new Rect(background_rectangle.x, background_rectangle.y, background_rectangle.width, background_rectangle.height), theme.image_manager.get_tile_background(), ScaleMode.StretchToFill);
            GUI.color = cache_col;
            string proj_name = theme.text_manager.texter(Project.project_name);
            string proj_version = theme.text_manager.texter("Version " + Project.version.print());
            Vector2 name_size = theme.styler_manager.load_style_header_beeg(ref theme).CalcSize(new GUIContent(proj_name));
            Vector2 version_size = theme.styler_manager.load_style_header_smol(ref theme).CalcSize(new GUIContent(proj_version));
            float header_text_height = name_size.y + version_size.y + 5f;
            float vertical_start = background_rectangle.y + (background_rectangle.height - header_text_height) / 2f;
            Rect projectNameRect = new Rect(
                background_rectangle.x,
                vertical_start,
                background_rectangle.width,
                name_size.y
            );
            GUI.Label(projectNameRect, proj_name, theme.styler_manager.load_style_header_beeg(ref theme));
            Rect projectVersionRect = new Rect(
                    background_rectangle.x,
                    vertical_start + name_size.y + 5f,
                    background_rectangle.width,
                    version_size.y
                );
            GUI.Label(projectVersionRect, proj_version, theme.styler_manager.load_style_header_smol(ref theme));
            EditorGUILayout.EndVertical();
            GUILayout.Space(20f);
            EditorGUILayout.EndHorizontal();
        }

    }

    // plug and play announcement tile
    public class Announcement
    {

        // values needed..
        private Theme theme = null;

        // to be filled in
        private Rect background_rectangle;
        private bool dismissed_notif = false;

        // constructor
        public Announcement(ref Theme theme)
        {
            this.theme = theme;
        }

        // draw the announcement
        public void draw()
        {
            if (this == null) return; // before we fetch and trying to draw
            if (!theme.config_manager.json_data.@interface.show_announcements || dismissed_notif) return; // user doesn't want
            if (theme.metadata_manager.full_metadata.Announcement == null) return; // shouldn't happen, but just in case
            if (!theme.metadata_manager.full_metadata.Announcement.Active) return; // announcement is not active
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            // temp
            string header = theme.text_manager.texter(this.theme.metadata_manager.full_metadata.Announcement.Header);
            string body = theme.text_manager.texter(this.theme.metadata_manager.full_metadata.Announcement.Message);
            // styles
            GUIStyle headerStyle = theme.styler_manager.load_style_announcement_header(ref theme);
            GUIStyle bodyStyle = theme.styler_manager.load_style_announcement_body(ref theme);
            headerStyle.wordWrap = true;
            bodyStyle.wordWrap = true;
            // dimensions and sizing
            float spacing = 4f;
            float contentWidth = EditorGUIUtility.currentViewWidth - 40f;
            float headerHeight = theme.styler_manager.load_style_announcement_header(ref theme).CalcHeight(new GUIContent(header), contentWidth);
            float bodyHeight = theme.styler_manager.load_style_announcement_body(ref theme).CalcHeight(new GUIContent(body), contentWidth);
            float totalContentHeight = headerHeight + spacing + spacing + bodyHeight;
            float backgroundHeight = totalContentHeight;
            // background
            background_rectangle = EditorGUILayout.GetControlRect(false, backgroundHeight - 4f);
            Color cache_col = GUI.color;
            GUI.color = Colours.get_background();
            GUI.DrawTexture(
                new Rect(background_rectangle.x, background_rectangle.y - 2f, background_rectangle.width, backgroundHeight),
                theme.image_manager.get_tile_background(),
                ScaleMode.StretchToFill
            );
            GUI.color = cache_col;
            // content positions
            float cursorY = background_rectangle.y;
            float startX = background_rectangle.x;
            float width = background_rectangle.width;
            Rect clickableRect = new Rect(startX, background_rectangle.y, width, totalContentHeight);
            // header
            Rect headerRect = new Rect(startX, cursorY, width, headerHeight);
            GUI.Label(headerRect, header, theme.styler_manager.load_style_announcement_header(ref theme));
            cursorY += headerHeight + spacing;
            // body
            Rect bodyRect = new Rect(startX, cursorY, width, bodyHeight);
            GUI.Label(bodyRect, body, theme.styler_manager.load_style_announcement_body(ref theme));
            // window controls + close on click
            float iconSize = 56f;
            Rect iconRect = new Rect(
                background_rectangle.xMax - iconSize - 10f,
                background_rectangle.y - 10f,
                iconSize,
                iconSize
            );
            GUI.DrawTexture(iconRect, theme.image_manager.get_tile_window_controls(), ScaleMode.ScaleToFit, true);
            if (GUI.Button(clickableRect, GUIContent.none, GUIStyle.none))
            {
                dismissed_notif = true;
            }
            EditorGUIUtility.AddCursorRect(background_rectangle, MouseCursor.Link);
            EditorGUILayout.EndVertical();
            GUILayout.Space(20f);
            EditorGUILayout.EndHorizontal();
        }

    }

    // plug and play update tile
    public class Update
    {

        // values needed..
        private Theme theme = null;

        // to be filled in
        private Rect background_rectangle;
        private bool has_update = false;
        private bool dismissed_notif = false;

        // constructor
        public Update(ref Theme theme)
        {
            this.theme = theme;
            check_update();
        }

        // compare the current version with the latest version
        private void check_update()
        {
            VersionEntry versionEntry = theme.metadata_manager.full_metadata.Versions.Find(x => x.Name == Project.project_name);
            if (versionEntry == null)
            {
                Pretty.print("Could not find version in metadata for project.. " + Project.project_name, Pretty.LogKind.Error);
                has_update = false;
                return;
            }
            if (string.IsNullOrEmpty(versionEntry.Version))
            {
                Pretty.print("Version string is empty for project.. " + Project.project_name, Pretty.LogKind.Error);
                has_update = false;
                return;
            }
            try
            {
                Version latestVersion = new Version(versionEntry.Version);
                has_update = !Project.version.is_newer_than(latestVersion);
            }
            catch (Exception e)
            {
                Pretty.print("Failed to parse version string for project.. " + Project.project_name + " - " + e.Message, Pretty.LogKind.Error);
                has_update = false;
            }
        }

        // draw the update message
        public void draw()
        {
            if (dismissed_notif || !theme.config_manager.json_data.@interface.show_updates) return; // user wants to silence updates
            if (!has_update) return; // no update available
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            // temp
            string header = theme.text_manager.texter("<i>" + this.theme.language_manager.speak("update_available") + "</i>");
            string body = theme.text_manager.texter(this.theme.language_manager.speak("update_description"));
            // styles
            GUIStyle headerStyle = theme.styler_manager.load_style_announcement_header(ref theme);
            GUIStyle bodyStyle = theme.styler_manager.load_style_announcement_body(ref theme);
            headerStyle.wordWrap = true;
            bodyStyle.wordWrap = true;
            // dimensions and sizing
            float spacing = 4f;
            float contentWidth = EditorGUIUtility.currentViewWidth - 40f;
            float headerHeight = theme.styler_manager.load_style_announcement_header(ref theme).CalcHeight(new GUIContent(header), contentWidth);
            float bodyHeight = theme.styler_manager.load_style_announcement_body(ref theme).CalcHeight(new GUIContent(body), contentWidth);
            float totalContentHeight = headerHeight + spacing + spacing + bodyHeight;
            float backgroundHeight = totalContentHeight;
            // background
            background_rectangle = EditorGUILayout.GetControlRect(false, backgroundHeight - 4f);
            Color cache_col = GUI.color;
            GUI.color = Colours.get_background();
            GUI.DrawTexture(
                new Rect(background_rectangle.x, background_rectangle.y - 2f, background_rectangle.width, backgroundHeight),
                theme.image_manager.get_tile_background(),
                ScaleMode.StretchToFill
            );
            GUI.color = cache_col;
            // content positions
            float cursorY = background_rectangle.y;
            Rect clickableRect = new Rect(background_rectangle.x, background_rectangle.y, background_rectangle.width, totalContentHeight);
            // header
            Rect headerRect = new Rect(background_rectangle.x, cursorY, background_rectangle.width, headerHeight);
            GUI.Label(headerRect, header, theme.styler_manager.load_style_announcement_header(ref theme));
            cursorY += headerHeight + spacing;
            // body
            Rect bodyRect = new Rect(background_rectangle.x, cursorY, background_rectangle.width, bodyHeight);
            GUI.Label(bodyRect, body, theme.styler_manager.load_style_announcement_body(ref theme));
            // window controls + close on click
            float iconSize = 56f;
            Rect iconRect = new Rect(
                background_rectangle.xMax - iconSize - 10f,
                background_rectangle.y - 10f,
                iconSize,
                iconSize
            );
            GUI.DrawTexture(iconRect, theme.image_manager.get_tile_window_controls(), ScaleMode.ScaleToFit, true);
            if (GUI.Button(clickableRect, GUIContent.none, GUIStyle.none))
            {
                dismissed_notif = true;
            }
            EditorGUIUtility.AddCursorRect(background_rectangle, MouseCursor.Link);
            EditorGUILayout.EndVertical();
            GUILayout.Space(20f);
            EditorGUILayout.EndHorizontal();
        }

    }

    // plug and play config menu
    public class ConfigMenu
    {

        // passed stuff..
        private Theme theme;
        private Languages languages;
        private Config config;
        private Tab tab;

        // basic constructor
        public ConfigMenu(ref Theme theme, ref Languages languages, ref Config config, ref Tab tab)
        {
            this.theme = theme;
            this.languages = languages;
            this.config = config;
            this.tab = tab;
        }

        // draw
        public void draw()
        {
            tab.draw();
            if (tab.is_expanded)
            {
                Components.start_foldout();
                // tab themes
                string[] tabThemeNames = theme.tab_manager.scheme_list.ToArray();
                int currentTabThemeIndex = Array.IndexOf(tabThemeNames, config.json_data.@interface.tab_theme);
                if (currentTabThemeIndex < 0) currentTabThemeIndex = 0;
                int newTabThemeIndex = EditorGUILayout.Popup(languages.speak("config_tab_theme"), currentTabThemeIndex, tabThemeNames);
                if (newTabThemeIndex != currentTabThemeIndex)
                {
                    config.json_data.@interface.tab_theme = tabThemeNames[newTabThemeIndex];
                    theme.tab_manager = new TabSchemes(config.json_data.@interface.tab_theme, true);
                }
                // text theme
                    string[] textThemeNames = theme.text_manager.scheme_list.ToArray();
                int currentTextThemeIndex = Array.IndexOf(textThemeNames, config.json_data.@interface.text_theme);
                if (currentTextThemeIndex < 0) currentTextThemeIndex = 0;
                int newTextThemeIndex = EditorGUILayout.Popup(languages.speak("config_text_theme"), currentTextThemeIndex, textThemeNames);
                if (newTextThemeIndex != currentTextThemeIndex)
                {
                    config.json_data.@interface.text_theme = textThemeNames[newTextThemeIndex];
                    theme.text_manager = new TextSchemes(config.json_data.@interface.text_theme, true);
                }
                // ui size
                string[] uiSizeNames = new string[] { "Compact", "Comfy", "Big Screen" };
                int currentUiSizeIndex = Array.IndexOf(uiSizeNames, config.json_data.@interface.ui_size);
                if (currentUiSizeIndex < 0) currentUiSizeIndex = 0;
                int newUiSizeIndex = EditorGUILayout.Popup(languages.speak("config_ui_size"), currentUiSizeIndex, uiSizeNames);
                if (newUiSizeIndex != currentUiSizeIndex)
                {
                    config.json_data.@interface.ui_size = uiSizeNames[newUiSizeIndex];
                    theme.styler_manager.flush();
                }
                // language
                string[] languageNames = new string[] { "English", "German", "Japanese", "French", "Chinese", "Spanish", "Korean", "Russian", "Cat" };
                int currentLanguageIndex = Array.IndexOf(languageNames, config.json_data.@interface.language);
                if (currentLanguageIndex < 0) currentLanguageIndex = 0;
                int newLanguageIndex = EditorGUILayout.Popup(languages.speak("config_language"), currentLanguageIndex, languageNames);
                if (newLanguageIndex != currentLanguageIndex)
                {
                    config.json_data.@interface.language = languageNames[newLanguageIndex];
                    this.languages = new Languages(config.json_data.@interface.language);
                }
                // toggle update toasts
                string[] toggleOptions = new string[] { languages.speak("config_toggle_enabled"), languages.speak("config_toggle_disabled") };
                bool showUpdates = config.json_data.@interface.show_updates;
                int currentUpdateIndex = showUpdates ? 0 : 1;
                int newUpdateIndex = EditorGUILayout.Popup(languages.speak("config_toggle_updates"), currentUpdateIndex, toggleOptions);
                if (newUpdateIndex != currentUpdateIndex)
                {
                    config.json_data.@interface.show_updates = newUpdateIndex == 0;
                }
                // toggle announcement toasts
                bool showAnnouncements = config.json_data.@interface.show_announcements;
                int currentAnnouncementIndex = showAnnouncements ? 0 : 1;
                int newAnnouncementIndex = EditorGUILayout.Popup(languages.speak("config_toggle_announcements"), currentAnnouncementIndex, toggleOptions);
                if (newAnnouncementIndex != currentAnnouncementIndex)
                {
                    config.json_data.@interface.show_announcements = newAnnouncementIndex == 0;
                }
                // save / cancel button row
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(languages.speak("config_save")))
                {
                    if (config.save())
                    {
                        theme.styler_manager.flush();
                        Interface.unload();
                        EditorUtility.DisplayDialog(Project.project_name, languages.speak("config_save_success"), languages.speak("dialog_okay"));
                    }
                    else
                    {
                        EditorUtility.DisplayDialog(Project.project_name, languages.speak("config_save_error"), languages.speak("dialog_okay"));
                    }
                }
                if (GUILayout.Button(languages.speak("config_reset")))
                {
                    if (config.reset())
                    {
                        theme.styler_manager.flush();
                        Interface.unload();
                        EditorUtility.DisplayDialog(Project.project_name, languages.speak("config_reset_success"), languages.speak("dialog_okay"));
                    }
                    else
                    {
                        EditorUtility.DisplayDialog(Project.project_name, languages.speak("config_reset_error"), languages.speak("dialog_okay"));
                    }
                }
                EditorGUILayout.EndHorizontal();
                Components.end_foldout();
            }
        }

    }

    // plug and play documentation link
    public class Docs
    {

        // values needed..
        private Theme theme = null;

        // to be filled in
        private Rect background_rectangle;

        // constructor
        public Docs(ref Theme theme)
        {
            this.theme = theme;
        }

        // draw the update message
        public void draw()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            string body = theme.text_manager.texter("<i>" + theme.language_manager.speak("open_docs") + "</i>");
            float spacing = 26f;
            float contentWidth = EditorGUIUtility.currentViewWidth - 40f;
            float bodyHeight = theme.styler_manager.load_style_docs(ref theme).CalcHeight(new GUIContent(body), contentWidth);
            float totalContentHeight = spacing + bodyHeight;
            float backgroundHeight = totalContentHeight;
            Rect background_rectangle = EditorGUILayout.GetControlRect(false, backgroundHeight - 4f);
            Color cache_col = GUI.color;
            GUI.color = Colours.get_background();
            GUI.DrawTexture(
                new Rect(background_rectangle.x, background_rectangle.y - 2f, background_rectangle.width, backgroundHeight),
                theme.image_manager.get_tile_background(),
                ScaleMode.StretchToFill
            );
            GUI.color = cache_col;
            if (GUI.Button(background_rectangle, body, theme.styler_manager.load_style_docs(ref theme)))
            {
                Application.OpenURL(Project.project_docs);
            }
            EditorGUIUtility.AddCursorRect(background_rectangle, MouseCursor.Link);
            EditorGUILayout.EndVertical();
            GUILayout.Space(20f);
            EditorGUILayout.EndHorizontal();
        }

    }

    // plug and play notice box
    public class NoticeBox
    {

        // values needed..
        private Theme theme = null;
        private string text_key = "";

        // to be filled in
        private Rect background_rectangle;

        // constructor
        public NoticeBox(ref Theme theme, string text_key)
        {
            this.theme = theme;
            this.text_key = text_key;
        }

        // draw the notice
        public void draw()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            string notice_text = theme.text_manager.texter("<i>" + theme.language_manager.speak(text_key) + "</i>");
            GUIContent content = new GUIContent(notice_text);
            float spacing = 26f;
            float contentWidth = EditorGUIUtility.currentViewWidth - 40f;
            GUIStyle style = theme.styler_manager.load_style_docs(ref theme);
            float bodyHeight = style.CalcHeight(content, contentWidth);
            float totalContentHeight = spacing + bodyHeight;
            float backgroundHeight = totalContentHeight;
            Rect background_rectangle = EditorGUILayout.GetControlRect(false, backgroundHeight - 4f);
            Color cache_col = GUI.color;
            GUI.color = Colours.get_background();
            GUI.DrawTexture(
                new Rect(background_rectangle.x, background_rectangle.y - 2f, background_rectangle.width, backgroundHeight),
                theme.image_manager.get_tile_background(),
                ScaleMode.StretchToFill
            );
            GUI.color = cache_col;
            GUI.Label(background_rectangle, content, style);
            EditorGUILayout.EndVertical();
            GUILayout.Space(20f);
            EditorGUILayout.EndHorizontal();
        }

    }

    // plug and play social menu
    public class SocialsMenu
    {

        // values needed..
        private Theme theme = null;

        // to be filled in
        private Row row = null;

        // constructor
        public SocialsMenu(ref Theme theme)
        {
            this.theme = theme;
            populate();
        }

        // draw the socials menu
        public void draw()
        {
            row.draw();
        }

        // populate the tiles
        private void populate()
        {
            if (this.theme.metadata_manager != null)
            {
                row = new Row(ref theme.metadata_manager.full_metadata.Socials, ref theme);
            }
            else
            {
                Pretty.print("Could not draw socials, as metadata doesn't exist..", Pretty.LogKind.Error);
            }
        }

    }

}

#endif // UNITY_EDITOR