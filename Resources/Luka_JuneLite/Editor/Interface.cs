#if UNITY_EDITOR

// imports
using System.IO;
using UnityEditor;
using UnityEngine;

// Interface.cs is the actual shader GUI that is displayed..
namespace Luka.JuneLite
{

    // the material editor interface
    public class Interface : ShaderGUI
    {
        private static bool loaded = false;
        private static bool preview_loaded = false;
        private static bool is_preview = false;
        private static Config configs = null;
        private static Languages languages = null;
        private static Theme theme = null;
        private static Header header = null;
        private static Announcement announcement = null;
        private static Update update = null;
        private static Docs docs = null;
        private static SocialsMenu socials_menu = null;
        private static NoticeBox preview_notice = null;
        private static Tab tab_june_pro = null;
        private static Tab tab_rendering = null;
        private static Tab tab_audio_link = null;
        private static Tab tab_coloring = null;
        private static Tab tab_blur = null;
        private static Tab tab_distortion = null;
        private static Tab tab_border = null;
        private static Tab tab_fog = null;
        private static Tab tab_glitch = null;
        private static Tab tab_overlay = null;
        private static Tab tab_uv_manipulation = null;
        private static Tab tab_filter = null;
        private static Tab tab_zoom = null;
        private static Tab tab_sub_filter_vignette = null;
        private static Tab tab_sub_filter_color_crush = null;
        private static Tab tab_sub_filter_duotone = null;
        private static Tab tab_sub_filter_film = null;
        private static Tab tab_sub_filter_grain = null;
        private static Tab tab_sub_filter_vhs = null;
        private static Tab tab_sub_filter_gradient = null;
        private static Tab tab_sub_filter_outline = null;
        private static Tab tab_sub_filter_astral = null;
        private static Tab tab_sub_filter_neon = null;
        private static Tab tab_sub_filter_trippy = null;
        private static Tab tab_sub_uv_transformation = null;
        private static Tab tab_sub_uv_move = null;
        private static Tab tab_sub_uv_shake = null;
        private static Tab tab_sub_uv_pixelation = null;
        private static Tab tab_sub_uv_rotation = null;
        private static Tab tab_sub_uv_spherize = null;
        private static Metadata meta = null;
        private static Tab config_tab = null;
        private static Tab license_tab = null;
        private static ConfigMenu config_menu = null; 
        private static Texture2D preview_texture = null;

        #region Properties
        private MaterialProperty prop_rendering_falloff_start = null;
        private MaterialProperty prop_rendering_falloff_end = null;
        private MaterialProperty prop_rendering_oob = null;
        private MaterialProperty prop_rendering_power = null;
        private MaterialProperty prop_rendering_quality = null;
        private MaterialProperty prop_coloring_module = null;
        private MaterialProperty prop_coloring_rgb_multiply = null;
        private MaterialProperty prop_coloring_rgb_overlay = null;
        private MaterialProperty prop_coloring_rgb_overlay_transparency = null;
        private MaterialProperty prop_coloring_hsv_style = null;
        private MaterialProperty prop_coloring_hsv_h = null;
        private MaterialProperty prop_coloring_hsv_s = null;
        private MaterialProperty prop_coloring_hsv_v = null;
        private MaterialProperty prop_coloring_invert = null;
        private MaterialProperty prop_coloring_drain = null;
        private MaterialProperty prop_coloring_darkness = null;
        private MaterialProperty prop_coloring_brightness = null;
        private MaterialProperty prop_coloring_emission = null;
        private MaterialProperty prop_coloring_posterization = null;
        private MaterialProperty prop_coloring_color_grading = null;
        private MaterialProperty prop_coloring_color_grading_tone = null;
        private MaterialProperty prop_coloring_sharpness = null;
        private MaterialProperty prop_blur_module = null;
        private MaterialProperty prop_blur_style = null;
        private MaterialProperty prop_blur_power = null;
        private MaterialProperty prop_blur_radius = null;
        private MaterialProperty prop_blur_transparency = null;
        private MaterialProperty prop_blur_color = null;
        private MaterialProperty prop_blur_dithering = null;
        private MaterialProperty prop_distortion_module = null;
        private MaterialProperty prop_distortion_style = null;
        private MaterialProperty prop_distortion_power_x = null;
        private MaterialProperty prop_distortion_power_y = null;
        private MaterialProperty prop_distortion_speed_x = null;
        private MaterialProperty prop_distortion_speed_y = null;
        private MaterialProperty prop_distortion_texture = null;
        private MaterialProperty prop_distortion_texture_scale = null;
        private MaterialProperty prop_distortion_wobble = null;
        private MaterialProperty prop_distortion_wobble_power = null;
        private MaterialProperty prop_distortion_wobble_speed = null;
        private MaterialProperty prop_distortion_wobble_coverage = null;
        private MaterialProperty prop_border_module = null;
        private MaterialProperty prop_border_style = null;
        private MaterialProperty prop_border_color = null;
        private MaterialProperty prop_border_power = null;
        private MaterialProperty prop_border_soften = null;
        private MaterialProperty prop_overlay_module = null;
        private MaterialProperty prop_overlay_texture = null;
        private MaterialProperty prop_overlay_size_x = null;
        private MaterialProperty prop_overlay_size_y = null;
        private MaterialProperty prop_overlay_offset_x = null;
        private MaterialProperty prop_overlay_offset_y = null;
        private MaterialProperty prop_overlay_transparency = null;
        private MaterialProperty prop_overlay_animated = null;
        private MaterialProperty prop_overlay_frames_x = null;
        private MaterialProperty prop_overlay_frames_y = null;
        private MaterialProperty prop_overlay_frames = null;
        private MaterialProperty prop_overlay_speed = null;
        private MaterialProperty prop_overlay_scrub = null;
        private MaterialProperty prop_overlay_blendmode = null;
        private MaterialProperty prop_overlay_vr = null;
        private MaterialProperty prop_overlay_vr_preview = null;
        private MaterialProperty prop_overlay_vr_size_x = null;
        private MaterialProperty prop_overlay_vr_size_y = null;
        private MaterialProperty prop_overlay_vr_offset_x = null;
        private MaterialProperty prop_overlay_vr_offset_y = null;
        private MaterialProperty prop_uv_manipulation_module = null;
        private MaterialProperty prop_uv_manipulation_transformation_slant_tl = null;
        private MaterialProperty prop_uv_manipulation_transformation_slant_tr = null;
        private MaterialProperty prop_uv_manipulation_transformation_slant_bl = null;
        private MaterialProperty prop_uv_manipulation_transformation_slant_br = null;
        private MaterialProperty prop_uv_manipulation_transformation_flip_x = null;
        private MaterialProperty prop_uv_manipulation_transformation_flip_y = null;
        private MaterialProperty prop_uv_manipulation_transformation_stretch_x = null;
        private MaterialProperty prop_uv_manipulation_transformation_stretch_y = null;
        private MaterialProperty prop_uv_manipulation_move_x = null;
        private MaterialProperty prop_uv_manipulation_move_y = null;
        private MaterialProperty prop_uv_manipulation_shake_style = null;
        private MaterialProperty prop_uv_manipulation_shake_power_x = null;
        private MaterialProperty prop_uv_manipulation_shake_power_y = null;
        private MaterialProperty prop_uv_manipulation_shake_speed_x = null;
        private MaterialProperty prop_uv_manipulation_shake_speed_y = null;
        private MaterialProperty prop_uv_manipulation_pixelation = null;
        private MaterialProperty prop_uv_manipulation_pixelation_power = null;
        private MaterialProperty prop_uv_manipulation_rotation = null;
        private MaterialProperty prop_uv_manipulation_rotation_angle = null;
        private MaterialProperty prop_uv_manipulation_spherize = null;
        private MaterialProperty prop_uv_manipulation_spherize_power = null;
        private MaterialProperty prop_filter_module = null;
        private MaterialProperty prop_filter_vignette = null;
        private MaterialProperty prop_filter_vignette_power = null;
        private MaterialProperty prop_filter_vignette_color = null;
        private MaterialProperty prop_filter_color_crush = null;
        private MaterialProperty prop_filter_color_crush_power = null;
        private MaterialProperty prop_filter_duotone = null;
        private MaterialProperty prop_filter_duotone_transparency = null;
        private MaterialProperty prop_filter_duotone_color_one = null;
        private MaterialProperty prop_filter_duotone_color_two = null;
        private MaterialProperty prop_filter_duotone_threshold = null;
        private MaterialProperty prop_filter_film = null;
        private MaterialProperty prop_filter_film_amount = null;
        private MaterialProperty prop_filter_grain = null;
        private MaterialProperty prop_filter_grain_amount = null;
        private MaterialProperty prop_filter_grain_color = null;
        private MaterialProperty prop_filter_vhs = null;
        private MaterialProperty prop_filter_vhs_amount = null;
        private MaterialProperty prop_filter_gradient = null;
        private MaterialProperty prop_filter_gradient_lhs = null;
        private MaterialProperty prop_filter_gradient_rhs = null;
        private MaterialProperty prop_filter_gradient_transparency = null;
        private MaterialProperty prop_filter_outline = null;
        private MaterialProperty prop_filter_outline_width = null;
        private MaterialProperty prop_filter_outline_tolerance = null;
        private MaterialProperty prop_filter_outline_color = null;
        private MaterialProperty prop_filter_astral = null;
        private MaterialProperty prop_filter_astral_zoom = null;
        private MaterialProperty prop_filter_astral_zoom_transparency = null;
        private MaterialProperty prop_filter_astral_zoom_color = null;
        private MaterialProperty prop_filter_neon = null;
        private MaterialProperty prop_filter_neon_width = null;
        private MaterialProperty prop_filter_neon_transparency = null;
        private MaterialProperty prop_filter_neon_hue = null;
        private MaterialProperty prop_zoom_module = null;
        private MaterialProperty prop_zoom_power = null;
        private MaterialProperty prop_zoom_range_style = null;
        private MaterialProperty prop_zoom_range_start = null;
        private MaterialProperty prop_zoom_range_end = null;
        private MaterialProperty prop_fog_module = null;
        private MaterialProperty prop_fog_density = null;
        private MaterialProperty prop_fog_distribution = null;
        private MaterialProperty prop_fog_color = null;
        private MaterialProperty prop_fog_safe_space = null;
        private MaterialProperty prop_fog_safe_space_size = null;
        private MaterialProperty prop_audio_link_module = null;
        private MaterialProperty prop_audio_link_band = null;
        private MaterialProperty prop_audio_link_power = null;
        private MaterialProperty prop_audio_link_min = null;
        private MaterialProperty prop_audio_link_max = null;
        private MaterialProperty prop_glitch_module = null;
        private MaterialProperty prop_glitch_scale = null;
        private MaterialProperty prop_glitch_amount = null;
        private MaterialProperty prop_glitch_uvs = null;
        private MaterialProperty prop_glitch_chromatic = null;
        private MaterialProperty prop_coloring_rainbow = null;
        private MaterialProperty prop_coloring_rainbow_speed = null;
        private MaterialProperty prop_filter_trippy = null;
        private MaterialProperty prop_filter_trippy_power = null;
        private MaterialProperty prop_filter_trippy_spread = null;
        private MaterialProperty prop_filter_trippy_speed = null;
        #endregion
        
        #region June Pro Preview
        private static Tab june_pro_tab_blur = null;
        private static Tab june_pro_tab_border = null;
        private static Tab june_pro_tab_chromatic = null;
        private static Tab june_pro_tab_colour_grading = null;
        private static Tab june_pro_tab_creativity = null;
        private static Tab june_pro_tab_distortion = null;
        private static Tab june_pro_tab_enhancements = null;
        private static Tab june_pro_tab_experiments = null;
        private static Tab june_pro_tab_filters = null;
        private static Tab june_pro_tab_frames = null;
        private static Tab june_pro_tab_generation = null;
        private static Tab june_pro_tab_glitch = null;
        private static Tab june_pro_tab_motion = null;
        private static Tab june_pro_tab_others = null;
        private static Tab june_pro_tab_outline = null;
        private static Tab june_pro_tab_overlay = null;
        private static Tab june_pro_tab_stylize = null;
        private static Tab june_pro_tab_special = null;
        private static Tab june_pro_tab_transition = null;
        private static Tab june_pro_tab_triplanar = null;
        private static Tab june_pro_tab_uv_manipulation = null;
        private static Tab june_pro_tab_vertex_reconstruction = null;
        private static Tab june_pro_tab_zoom = null;
        private static Tab june_pro_sub_tab_colour_grading_fine_tuning = null;
        private static Tab june_pro_sub_tab_colour_grading_colour_focus = null;
        private static Tab june_pro_sub_tab_colour_grading_colour_replacements = null;
        private static Tab june_pro_sub_tab_colour_grading_colourspace = null;
        private static Tab june_pro_sub_tab_colour_grading_lighting_adjustment = null;
        private static Tab june_pro_sub_tab_colour_grading_sharpness = null;
        private static Tab june_pro_sub_tab_colour_grading_saturation = null;
        private static Tab june_pro_sub_tab_colour_grading_stylize = null;
        private static Tab june_pro_sub_tab_colour_grading_colour_channels = null;
        private static Tab june_pro_sub_tab_colour_grading_post_processing = null;
        private static Tab june_pro_sub_tab_colour_grading_posterization = null;
        private static Tab june_pro_sub_tab_creativity_alphenglow = null;
        private static Tab june_pro_sub_tab_creativity_aquamarine = null;
        private static Tab june_pro_sub_tab_creativity_aurora = null;
        private static Tab june_pro_sub_tab_creativity_bonnibel = null;
        private static Tab june_pro_sub_tab_creativity_butterfly = null;
        private static Tab june_pro_sub_tab_creativity_candy = null;
        private static Tab june_pro_sub_tab_creativity_ecstasy = null;
        private static Tab june_pro_sub_tab_creativity_fable = null;
        private static Tab june_pro_sub_tab_creativity_lava_lamp = null;
        private static Tab june_pro_sub_tab_creativity_marceline = null;
        private static Tab june_pro_sub_tab_creativity_smokescreen = null;
        private static Tab june_pro_sub_tab_creativity_turbulence = null;
        private static Tab june_pro_sub_tab_creativity_rainbow_river = null;
        private static Tab june_pro_sub_tab_creativity_portal = null;
        private static Tab june_pro_sub_tab_creativity_tea = null;
        private static Tab june_pro_sub_tab_creativity_oil_spill = null;
        private static Tab june_pro_sub_tab_creativity_art = null;
        private static Tab june_pro_sub_tab_distortion_simple_distortion = null;
        private static Tab june_pro_sub_tab_distortion_bezier_curve = null;
        private static Tab june_pro_sub_tab_distortion_blackhole = null;
        private static Tab june_pro_sub_tab_distortion_bubbles = null;
        private static Tab june_pro_sub_tab_distortion_bumpy_glass = null;
        private static Tab june_pro_sub_tab_distortion_exaggeration = null;
        private static Tab june_pro_sub_tab_distortion_liquify = null;
        private static Tab june_pro_sub_tab_distortion_warp = null;
        private static Tab june_pro_sub_tab_distortion_wave = null;
        private static Tab june_pro_sub_tab_enhancements_anti_aliasing = null;
        private static Tab june_pro_sub_tab_enhancements_denoise = null;
        private static Tab june_pro_sub_tab_enhancements_deblur = null;
        private static Tab june_pro_sub_tab_enhancements_heavy_lines = null;
        private static Tab june_pro_sub_tab_enhancements_soft_lines = null;
        private static Tab june_pro_sub_tab_enhancements_upscale = null;
        private static Tab june_pro_sub_tab_enhancements_contrast_sharpening = null;
        private static Tab june_pro_sub_tab_enhancements_fdr = null;
        private static Tab june_pro_sub_tab_enhancements_screenspace_softshading = null;
        private static Tab june_pro_sub_tab_experiments_dolly = null;
        private static Tab june_pro_sub_tab_experiments_cloning = null;
        private static Tab june_pro_sub_tab_experiments_screen_background = null;
        private static Tab june_pro_sub_tab_experiments_depth_viewer = null;
        private static Tab june_pro_sub_tab_experiments_objectify = null;
        private static Tab june_pro_sub_tab_filters_colour_incorrection = null;
        private static Tab june_pro_sub_tab_filters_colourblind_simulation = null;
        private static Tab june_pro_sub_tab_filters_corners = null;
        private static Tab june_pro_sub_tab_filters_colour_crush = null;
        private static Tab june_pro_sub_tab_filters_colour_cycline = null;
        private static Tab june_pro_sub_tab_filters_colour_wheel = null;
        private static Tab june_pro_sub_tab_filters_crt = null;
        private static Tab june_pro_sub_tab_filters_monotone = null;
        private static Tab june_pro_sub_tab_filters_duotone = null;
        private static Tab june_pro_sub_tab_filters_tritone = null;
        private static Tab june_pro_sub_tab_filters_engraving = null;
        private static Tab june_pro_sub_tab_filters_linocut = null;
        private static Tab june_pro_sub_tab_filters_light_leak = null;
        private static Tab june_pro_sub_tab_filters_film = null;
        private static Tab june_pro_sub_tab_filters_normal_mapper = null;
        private static Tab june_pro_sub_tab_filters_chrome = null;
        private static Tab june_pro_sub_tab_filters_rainbow = null;
        private static Tab june_pro_sub_tab_filters_ramp = null;
        private static Tab june_pro_sub_tab_filters_gradient = null;
        private static Tab june_pro_sub_tab_filters_low_ink = null;
        private static Tab june_pro_sub_tab_filters_low_bitrate = null;
        private static Tab june_pro_sub_tab_filters_grain = null;
        private static Tab june_pro_sub_tab_filters_glitter = null;
        private static Tab june_pro_sub_tab_filters_moire = null;
        private static Tab june_pro_sub_tab_filters_sepia = null;
        private static Tab june_pro_sub_tab_filters_solarize = null;
        private static Tab june_pro_sub_tab_filters_specular = null;
        private static Tab june_pro_sub_tab_filters_tie_dye = null;
        private static Tab june_pro_sub_tab_filters_technicolour = null;
        private static Tab june_pro_sub_tab_filters_thermal = null;
        private static Tab june_pro_sub_tab_filters_threshold = null;
        private static Tab june_pro_sub_tab_filters_night_vision = null;
        private static Tab june_pro_sub_tab_filters_ultra_violet = null;
        private static Tab june_pro_sub_tab_filters_wall_glow = null;
        private static Tab june_pro_sub_tab_filters_vhs = null;
        private static Tab june_pro_sub_tab_filters_vignette = null;
        private static Tab june_pro_sub_tab_filters_dither = null;
        private static Tab june_pro_sub_tab_filters_fauxlate = null;
        private static Tab june_pro_sub_tab_filters_lieless = null;
        private static Tab june_pro_sub_tab_generation_lines = null;
        private static Tab june_pro_sub_tab_generation_ring_colors = null;
        private static Tab june_pro_sub_tab_generation_noise_colors = null;
        private static Tab june_pro_sub_tab_generation_sdf_colors = null;
        private static Tab june_pro_sub_tab_generation_shapes_colors = null;
        private static Tab june_pro_sub_tab_generation_shapes_uvs = null;
        private static Tab june_pro_sub_tab_generation_spiral_colors = null;
        private static Tab june_pro_sub_tab_generation_spiral_uvs = null;
        private static Tab june_pro_sub_tab_generation_hearts = null;
        private static Tab june_pro_sub_tab_glitch_simple = null;
        private static Tab june_pro_sub_tab_glitch_advanced = null;
        private static Tab june_pro_sub_tab_motion_acid = null;
        private static Tab june_pro_sub_tab_motion_blur = null;
        private static Tab june_pro_sub_tab_motion_distortion = null;
        private static Tab june_pro_sub_tab_motion_freeze = null;
        private static Tab june_pro_sub_tab_motion_rgb_freeze = null;
        private static Tab june_pro_sub_tab_motion_glitch = null;
        private static Tab june_pro_sub_tab_motion_chromatic = null;
        private static Tab june_pro_sub_tab_motion_pixel_sort = null;
        private static Tab june_pro_sub_tab_motion_trail = null;
        private static Tab june_pro_sub_tab_motion_tranquility = null;
        private static Tab june_pro_sub_tab_motion_tranceless = null;
        private static Tab june_pro_sub_tab_motion_data_mosh = null;
        private static Tab june_pro_sub_tab_motion_frame_rate = null;
        private static Tab june_pro_sub_tab_motion_fensterxd = null;
        private static Tab june_pro_sub_tab_motion_fading_projections = null;
        private static Tab june_pro_sub_tab_motion_motear = null;
        private static Tab june_pro_sub_tab_motion_lake_fill = null;
        private static Tab june_pro_sub_tab_others_astral = null;
        private static Tab june_pro_sub_tab_others_astral_rgb = null;
        private static Tab june_pro_sub_tab_others_apart = null;
        private static Tab june_pro_sub_tab_others_colour_diffusion = null;
        private static Tab june_pro_sub_tab_others_holepuncher = null;
        private static Tab june_pro_sub_tab_others_glowstick = null;
        private static Tab june_pro_sub_tab_others_grid_checkerboard = null;
        private static Tab june_pro_sub_tab_others_hallucinogen = null;
        private static Tab june_pro_sub_tab_others_lenticular_halo = null;
        private static Tab june_pro_sub_tab_others_meta_image = null;
        private static Tab june_pro_sub_tab_others_palette = null;
        private static Tab june_pro_sub_tab_others_rain_line = null;
        private static Tab june_pro_sub_tab_others_rim = null;
        private static Tab june_pro_sub_tab_others_scanline_overlay = null;
        private static Tab june_pro_sub_tab_others_stripes = null;
        private static Tab june_pro_sub_tab_others_sunbeams = null;
        private static Tab june_pro_sub_tab_others_water_reflection = null;
        private static Tab june_pro_sub_tab_others_camouflage = null;
        private static Tab june_pro_sub_tab_others_inception = null;
        private static Tab june_pro_sub_tab_others_object_detection = null;
        private static Tab june_pro_sub_tab_others_fog = null;
        private static Tab june_pro_sub_tab_others_silhouette = null;
        private static Tab june_pro_sub_tab_others_prismatic_layers = null;
        private static Tab june_pro_sub_tab_others_hexatile = null;
        private static Tab june_pro_sub_tab_others_secrets = null;
        private static Tab june_pro_sub_tab_others_divider = null;
        private static Tab june_pro_sub_tab_stylize_compression = null;
        private static Tab june_pro_sub_tab_stylize_crosshatching = null;
        private static Tab june_pro_sub_tab_stylize_crystalize = null;
        private static Tab june_pro_sub_tab_stylize_dots = null;
        private static Tab june_pro_sub_tab_stylize_emboss = null;
        private static Tab june_pro_sub_tab_stylize_impressionism = null;
        private static Tab june_pro_sub_tab_stylize_mosaic = null;
        private static Tab june_pro_sub_tab_stylize_neon_rings = null;
        private static Tab june_pro_sub_tab_stylize_oil = null;
        private static Tab june_pro_sub_tab_stylize_monitor = null;
        private static Tab june_pro_sub_tab_stylize_neon = null;
        private static Tab june_pro_sub_tab_stylize_pop_art = null;
        private static Tab june_pro_sub_tab_stylize_unicode = null;
        private static Tab june_pro_sub_tab_stylize_halftone = null;
        private static Tab june_pro_sub_tab_stylize_halftone_circles = null;
        private static Tab june_pro_sub_tab_stylize_halftone_spiral = null;
        private static Tab june_pro_sub_tab_stylize_halftone_rgb = null;
        private static Tab june_pro_sub_tab_special_bubbles = null;
        private static Tab june_pro_sub_tab_special_confetti = null;
        private static Tab june_pro_sub_tab_special_data_stream = null;
        private static Tab june_pro_sub_tab_special_lens_flare = null;
        private static Tab june_pro_sub_tab_special_hexagonal_shield = null;
        private static Tab june_pro_sub_tab_special_lightning = null;
        private static Tab june_pro_sub_tab_special_mapping = null;
        private static Tab june_pro_sub_tab_special_rain_drops = null;
        private static Tab june_pro_sub_tab_special_plexus = null;
        private static Tab june_pro_sub_tab_special_shanshuo = null;
        private static Tab june_pro_sub_tab_special_star_trail = null;
        private static Tab june_pro_sub_tab_special_spotlights = null;
        private static Tab june_pro_sub_tab_special_visualizer = null;
        private static Tab june_pro_sub_tab_special_warp_drive = null;
        private static Tab june_pro_sub_tab_uv_manipulation_bender = null;
        private static Tab june_pro_sub_tab_uv_manipulation_movement = null;
        private static Tab june_pro_sub_tab_uv_manipulation_clamp = null;
        private static Tab june_pro_sub_tab_uv_manipulation_coordinates = null;
        private static Tab june_pro_sub_tab_uv_manipulation_dither = null;
        private static Tab june_pro_sub_tab_uv_manipulation_kaleidoscope = null;
        private static Tab june_pro_sub_tab_uv_manipulation_mirror = null;
        private static Tab june_pro_sub_tab_uv_manipulation_scroll = null;
        private static Tab june_pro_sub_tab_uv_manipulation_shake_and_earthquake = null;
        private static Tab june_pro_sub_tab_uv_manipulation_slicer = null;
        private static Tab june_pro_sub_tab_uv_manipulation_melt = null;
        private static Tab june_pro_sub_tab_uv_manipulation_mirror_shatter = null;
        private static Tab june_pro_sub_tab_uv_manipulation_ring_rotation = null;
        private static Tab june_pro_sub_tab_uv_manipulation_refraction = null;
        private static Tab june_pro_sub_tab_uv_manipulation_pixelation = null;
        private static Tab june_pro_sub_tab_uv_manipulation_pixel_shifter = null;
        private static Tab june_pro_sub_tab_uv_manipulation_scanline = null;
        private static Tab june_pro_sub_tab_uv_manipulation_shuffle = null;
        private static Tab june_pro_sub_tab_uv_manipulation_skew = null;
        private static Tab june_pro_sub_tab_uv_manipulation_spherize = null;
        private static Tab june_pro_sub_tab_uv_manipulation_transformation = null;
        private static Tab june_pro_sub_tab_uv_manipulation_twisted_corridor = null;
        private static Tab june_pro_sub_tab_uv_manipulation_recursion = null;
        private static Tab june_pro_sub_tab_uv_manipulation_twod_rotation = null;
        private static Tab june_pro_sub_tab_uv_manipulation_threed_rotation = null;
        private static Tab june_pro_sub_tab_uv_manipulation_threed_pan = null;
        private static Tab june_pro_sub_tab_uv_manipulation_onlyscreens = null;
        private static Tab june_pro_sub_tab_uv_manipulation_spinterception = null;
        private static Tab june_pro_sub_tab_uv_manipulation_quadrant_zoom = null;
        private static Tab june_pro_sub_tab_uv_manipulation_swivel = null;
        private static Tab june_pro_sub_tab_uv_manipulation_distaer = null;
        private static Tab june_pro_sub_tab_uv_manipulation_thanos = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_atmosphere = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_glitter = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_shatterwave = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_threed_lighting = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_wireframe = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_wireframe_shatterwave = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_normals = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_tripful = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_hololens = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_lidar = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_corruption = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_world_wrap = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_spotlight = null;
        private static Tab june_pro_sub_tab_vertex_reconstruction_tryptamines = null;
        #endregion

        // enums for dropdowns (todo: make use languages)
        enum enumToggle { Off, On };
        enum enumAudioLink { Disabled, Bass, LowerMid, UpperMid, Treble };
        enum enumOOB { Clamp, Mirror, Repeat };
        enum enumHSV { Disabled, Multiply, Add };
        enum enumBlur { Disabled, Gaussian, Radial, Chromatic };
        enum enumDistortion { Disabled, SinCos, Wavey, Texture };
        enum enumBorder { Disabled, Horizontal, Vertical, Box };
        enum enumShake { Disabled, Bumpy, Smooth, Circular };
        enum enumBlend { Overlay, Multiply, Screen, Add, Subtract };

        // unload the interface (ex. on shader change)
        public static void unload()
        {
            loaded = false;
            preview_loaded = false;
            is_preview = false;
            configs = null;
            languages = null;
            theme = null;
            header = null;
            announcement = null;
            update = null;
            docs = null;
            socials_menu = null;
            tab_june_pro = null;
            tab_rendering = null;
            tab_audio_link = null;
            tab_coloring = null;
            tab_blur = null;
            tab_distortion = null;
            tab_border = null;
            tab_fog = null;
            tab_glitch = null;
            tab_overlay = null;
            tab_uv_manipulation = null;
            tab_filter = null;
            tab_zoom = null;
            tab_sub_filter_vignette = null;
            tab_sub_filter_color_crush = null;
            tab_sub_filter_duotone = null;
            tab_sub_filter_film = null;
            tab_sub_filter_grain = null;
            tab_sub_filter_vhs = null;
            tab_sub_filter_gradient = null;
            tab_sub_filter_outline = null;
            tab_sub_filter_astral = null;
            tab_sub_filter_neon = null;
            tab_sub_filter_trippy = null;
            tab_sub_uv_transformation = null;
            tab_sub_uv_move = null;
            tab_sub_uv_shake = null;
            tab_sub_uv_pixelation = null;
            tab_sub_uv_rotation = null;
            tab_sub_uv_spherize = null;
            meta = null;
            config_tab = null;
            license_tab = null;
            config_menu = null;
            preview_texture = null;
        }

        // load (/reload) the interface (ex. on language change)
        public void load(ref Material targetMat)
        {
            configs = new Config();
            languages = new Languages(configs.json_data.@interface.language);
            meta = new Metadata();
            theme = new Theme(ref configs, ref languages, ref meta);
            config_tab = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 13, languages.speak("tab_config"));
            license_tab = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 15, languages.speak("tab_license"));
            config_menu = new ConfigMenu(ref theme, ref languages, ref configs, ref config_tab);
            header = new Header(ref theme);
            announcement = new Announcement(ref theme);
            update = new Update(ref theme);
            docs = new Docs(ref theme);
            socials_menu = new SocialsMenu(ref theme);
            tab_rendering = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 1, languages.speak("tab_rendering"));
            tab_audio_link = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 2, languages.speak("tab_audio_link"));
            tab_blur = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 3, languages.speak("tab_blur"));
            tab_border = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 4, languages.speak("tab_border"));
            tab_coloring = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 5, languages.speak("tab_coloring"));
            tab_distortion = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 6, languages.speak("tab_distortion"));
            tab_filter = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 7, languages.speak("tab_filter"));
            tab_fog = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 8, languages.speak("tab_fog"));
            tab_glitch = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 9, languages.speak("tab_glitch"));
            tab_overlay = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 10, languages.speak("tab_overlay"));
            tab_uv_manipulation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 11, languages.speak("tab_uv_manipulation"));
            tab_zoom = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 12, languages.speak("tab_zoom"));
            tab_sub_filter_vignette = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("tab_sub_filter_vignette"));
            tab_sub_filter_color_crush = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("tab_sub_filter_color_crush"));
            tab_sub_filter_duotone = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("tab_sub_filter_duotone"));
            tab_sub_filter_film = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("tab_sub_filter_film"));
            tab_sub_filter_grain = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("tab_sub_filter_grain"));
            tab_sub_filter_vhs = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("tab_sub_filter_vhs"));
            tab_sub_filter_gradient = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("tab_sub_filter_gradient"));
            tab_sub_filter_outline = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("tab_sub_filter_outline"));
            tab_sub_filter_astral = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("tab_sub_filter_astral"));
            tab_sub_filter_neon = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 9, languages.speak("tab_sub_filter_neon"));
            tab_sub_filter_trippy = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 10, languages.speak("tab_sub_filter_trippy"));
            tab_sub_uv_transformation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("tab_sub_uv_transformation"));
            tab_sub_uv_move = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("tab_sub_uv_move"));
            tab_sub_uv_shake = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("tab_sub_uv_shake"));
            tab_sub_uv_pixelation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("tab_sub_uv_pixelation"));
            tab_sub_uv_rotation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("tab_sub_uv_rotation"));
            tab_sub_uv_spherize = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("tab_sub_uv_spherize")); 
            tab_june_pro = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 14, languages.speak("tab_june_pro"));
            preview_texture = Resources.Load(Project.project_path + "/Images/Preview_Banner", typeof(Texture2D)) as Texture2D;
            loaded = true;
        }

        // unload the june pro interface
        private void unload_preview()
        {
            june_pro_tab_blur = null;
            june_pro_tab_border = null;
            june_pro_tab_chromatic = null;
            june_pro_tab_colour_grading = null;
            june_pro_tab_creativity = null;
            june_pro_tab_distortion = null;
            june_pro_tab_enhancements = null;
            june_pro_tab_experiments = null;
            june_pro_tab_filters = null;
            june_pro_tab_frames = null;
            june_pro_tab_generation = null;
            june_pro_tab_glitch = null;
            june_pro_tab_motion = null;
            june_pro_tab_others = null;
            june_pro_tab_outline = null;
            june_pro_tab_overlay = null;
            june_pro_tab_stylize = null;
            june_pro_tab_special = null;
            june_pro_tab_transition = null;
            june_pro_tab_triplanar = null;
            june_pro_tab_uv_manipulation = null;
            june_pro_tab_vertex_reconstruction = null;
            june_pro_tab_zoom = null;
            june_pro_sub_tab_colour_grading_fine_tuning = null;
            june_pro_sub_tab_colour_grading_colour_focus = null;
            june_pro_sub_tab_colour_grading_colour_replacements = null;
            june_pro_sub_tab_colour_grading_colourspace = null;
            june_pro_sub_tab_colour_grading_lighting_adjustment = null;
            june_pro_sub_tab_colour_grading_sharpness = null;
            june_pro_sub_tab_colour_grading_saturation = null;
            june_pro_sub_tab_colour_grading_stylize = null;
            june_pro_sub_tab_colour_grading_colour_channels = null;
            june_pro_sub_tab_colour_grading_post_processing = null;
            june_pro_sub_tab_colour_grading_posterization = null;
            june_pro_sub_tab_creativity_alphenglow = null;
            june_pro_sub_tab_creativity_aquamarine = null;
            june_pro_sub_tab_creativity_aurora = null;
            june_pro_sub_tab_creativity_bonnibel = null;
            june_pro_sub_tab_creativity_butterfly = null;
            june_pro_sub_tab_creativity_candy = null;
            june_pro_sub_tab_creativity_ecstasy = null;
            june_pro_sub_tab_creativity_fable = null;
            june_pro_sub_tab_creativity_lava_lamp = null;
            june_pro_sub_tab_creativity_marceline = null;
            june_pro_sub_tab_creativity_smokescreen = null;
            june_pro_sub_tab_creativity_turbulence = null;
            june_pro_sub_tab_creativity_rainbow_river = null;
            june_pro_sub_tab_creativity_portal = null;
            june_pro_sub_tab_creativity_tea = null;
            june_pro_sub_tab_creativity_oil_spill = null;
            june_pro_sub_tab_creativity_art = null;
            june_pro_sub_tab_distortion_simple_distortion = null;
            june_pro_sub_tab_distortion_bezier_curve = null;
            june_pro_sub_tab_distortion_blackhole = null;
            june_pro_sub_tab_distortion_bubbles = null;
            june_pro_sub_tab_distortion_bumpy_glass = null;
            june_pro_sub_tab_distortion_exaggeration = null;
            june_pro_sub_tab_distortion_liquify = null;
            june_pro_sub_tab_distortion_warp = null;
            june_pro_sub_tab_distortion_wave = null;
            june_pro_sub_tab_enhancements_anti_aliasing = null;
            june_pro_sub_tab_enhancements_denoise = null;
            june_pro_sub_tab_enhancements_deblur = null;
            june_pro_sub_tab_enhancements_heavy_lines = null;
            june_pro_sub_tab_enhancements_soft_lines = null;
            june_pro_sub_tab_enhancements_upscale = null;
            june_pro_sub_tab_enhancements_contrast_sharpening = null;
            june_pro_sub_tab_enhancements_fdr = null;
            june_pro_sub_tab_enhancements_screenspace_softshading = null;
            june_pro_sub_tab_experiments_dolly = null;
            june_pro_sub_tab_experiments_cloning = null;
            june_pro_sub_tab_experiments_screen_background = null;
            june_pro_sub_tab_experiments_depth_viewer = null;
            june_pro_sub_tab_experiments_objectify = null;
            june_pro_sub_tab_filters_colour_incorrection = null;
            june_pro_sub_tab_filters_colourblind_simulation = null;
            june_pro_sub_tab_filters_corners = null;
            june_pro_sub_tab_filters_colour_crush = null;
            june_pro_sub_tab_filters_colour_cycline = null;
            june_pro_sub_tab_filters_colour_wheel = null;
            june_pro_sub_tab_filters_crt = null;
            june_pro_sub_tab_filters_monotone = null;
            june_pro_sub_tab_filters_duotone = null;
            june_pro_sub_tab_filters_tritone = null;
            june_pro_sub_tab_filters_engraving = null;
            june_pro_sub_tab_filters_linocut = null;
            june_pro_sub_tab_filters_light_leak = null;
            june_pro_sub_tab_filters_film = null;
            june_pro_sub_tab_filters_normal_mapper = null;
            june_pro_sub_tab_filters_chrome = null;
            june_pro_sub_tab_filters_rainbow = null;
            june_pro_sub_tab_filters_ramp = null;
            june_pro_sub_tab_filters_gradient = null;
            june_pro_sub_tab_filters_low_ink = null;
            june_pro_sub_tab_filters_low_bitrate = null;
            june_pro_sub_tab_filters_grain = null;
            june_pro_sub_tab_filters_glitter = null;
            june_pro_sub_tab_filters_moire = null;
            june_pro_sub_tab_filters_sepia = null;
            june_pro_sub_tab_filters_solarize = null;
            june_pro_sub_tab_filters_specular = null;
            june_pro_sub_tab_filters_tie_dye = null;
            june_pro_sub_tab_filters_technicolour = null;
            june_pro_sub_tab_filters_thermal = null;
            june_pro_sub_tab_filters_threshold = null;
            june_pro_sub_tab_filters_night_vision = null;
            june_pro_sub_tab_filters_ultra_violet = null;
            june_pro_sub_tab_filters_wall_glow = null;
            june_pro_sub_tab_filters_vhs = null;
            june_pro_sub_tab_filters_vignette = null;
            june_pro_sub_tab_filters_dither = null;
            june_pro_sub_tab_filters_fauxlate = null;
            june_pro_sub_tab_filters_lieless = null;
            june_pro_sub_tab_generation_lines = null;
            june_pro_sub_tab_generation_ring_colors = null;
            june_pro_sub_tab_generation_noise_colors = null;
            june_pro_sub_tab_generation_sdf_colors = null;
            june_pro_sub_tab_generation_shapes_colors = null;
            june_pro_sub_tab_generation_shapes_uvs = null;
            june_pro_sub_tab_generation_spiral_colors = null;
            june_pro_sub_tab_generation_spiral_uvs = null;
            june_pro_sub_tab_generation_hearts = null;
            june_pro_sub_tab_glitch_simple = null;
            june_pro_sub_tab_glitch_advanced = null;
            june_pro_sub_tab_motion_acid = null;
            june_pro_sub_tab_motion_blur = null;
            june_pro_sub_tab_motion_distortion = null;
            june_pro_sub_tab_motion_freeze = null;
            june_pro_sub_tab_motion_rgb_freeze = null;
            june_pro_sub_tab_motion_glitch = null;
            june_pro_sub_tab_motion_chromatic = null;
            june_pro_sub_tab_motion_pixel_sort = null;
            june_pro_sub_tab_motion_trail = null;
            june_pro_sub_tab_motion_tranquility = null;
            june_pro_sub_tab_motion_tranceless = null;
            june_pro_sub_tab_motion_data_mosh = null;
            june_pro_sub_tab_motion_frame_rate = null;
            june_pro_sub_tab_motion_fensterxd = null;
            june_pro_sub_tab_motion_fading_projections = null;
            june_pro_sub_tab_motion_motear = null;
            june_pro_sub_tab_motion_lake_fill = null;
            june_pro_sub_tab_others_astral = null;
            june_pro_sub_tab_others_astral_rgb = null;
            june_pro_sub_tab_others_apart = null;
            june_pro_sub_tab_others_colour_diffusion = null;
            june_pro_sub_tab_others_holepuncher = null;
            june_pro_sub_tab_others_glowstick = null;
            june_pro_sub_tab_others_grid_checkerboard = null;
            june_pro_sub_tab_others_hallucinogen = null;
            june_pro_sub_tab_others_lenticular_halo = null;
            june_pro_sub_tab_others_meta_image = null;
            june_pro_sub_tab_others_palette = null;
            june_pro_sub_tab_others_rain_line = null;
            june_pro_sub_tab_others_rim = null;
            june_pro_sub_tab_others_scanline_overlay = null;
            june_pro_sub_tab_others_stripes = null;
            june_pro_sub_tab_others_sunbeams = null;
            june_pro_sub_tab_others_water_reflection = null;
            june_pro_sub_tab_others_camouflage = null;
            june_pro_sub_tab_others_inception = null;
            june_pro_sub_tab_others_object_detection = null;
            june_pro_sub_tab_others_fog = null;
            june_pro_sub_tab_others_silhouette = null;
            june_pro_sub_tab_others_prismatic_layers = null;
            june_pro_sub_tab_others_hexatile = null;
            june_pro_sub_tab_others_secrets = null;
            june_pro_sub_tab_others_divider = null;
            june_pro_sub_tab_stylize_compression = null;
            june_pro_sub_tab_stylize_crosshatching = null;
            june_pro_sub_tab_stylize_crystalize = null;
            june_pro_sub_tab_stylize_dots = null;
            june_pro_sub_tab_stylize_emboss = null;
            june_pro_sub_tab_stylize_impressionism = null;
            june_pro_sub_tab_stylize_mosaic = null;
            june_pro_sub_tab_stylize_neon_rings = null;
            june_pro_sub_tab_stylize_oil = null;
            june_pro_sub_tab_stylize_monitor = null;
            june_pro_sub_tab_stylize_neon = null;
            june_pro_sub_tab_stylize_pop_art = null;
            june_pro_sub_tab_stylize_unicode = null;
            june_pro_sub_tab_stylize_halftone = null;
            june_pro_sub_tab_stylize_halftone_circles = null;
            june_pro_sub_tab_stylize_halftone_spiral = null;
            june_pro_sub_tab_stylize_halftone_rgb = null;
            june_pro_sub_tab_special_bubbles = null;
            june_pro_sub_tab_special_confetti = null;
            june_pro_sub_tab_special_data_stream = null;
            june_pro_sub_tab_special_lens_flare = null;
            june_pro_sub_tab_special_hexagonal_shield = null;
            june_pro_sub_tab_special_lightning = null;
            june_pro_sub_tab_special_mapping = null;
            june_pro_sub_tab_special_rain_drops = null;
            june_pro_sub_tab_special_plexus = null;
            june_pro_sub_tab_special_shanshuo = null;
            june_pro_sub_tab_special_star_trail = null;
            june_pro_sub_tab_special_spotlights = null;
            june_pro_sub_tab_special_visualizer = null;
            june_pro_sub_tab_special_warp_drive = null;
            june_pro_sub_tab_uv_manipulation_bender = null;
            june_pro_sub_tab_uv_manipulation_movement = null;
            june_pro_sub_tab_uv_manipulation_clamp = null;
            june_pro_sub_tab_uv_manipulation_coordinates = null;
            june_pro_sub_tab_uv_manipulation_dither = null;
            june_pro_sub_tab_uv_manipulation_kaleidoscope = null;
            june_pro_sub_tab_uv_manipulation_mirror = null;
            june_pro_sub_tab_uv_manipulation_scroll = null;
            june_pro_sub_tab_uv_manipulation_shake_and_earthquake = null;
            june_pro_sub_tab_uv_manipulation_slicer = null;
            june_pro_sub_tab_uv_manipulation_melt = null;
            june_pro_sub_tab_uv_manipulation_mirror_shatter = null;
            june_pro_sub_tab_uv_manipulation_ring_rotation = null;
            june_pro_sub_tab_uv_manipulation_refraction = null;
            june_pro_sub_tab_uv_manipulation_pixelation = null;
            june_pro_sub_tab_uv_manipulation_pixel_shifter = null;
            june_pro_sub_tab_uv_manipulation_scanline = null;
            june_pro_sub_tab_uv_manipulation_shuffle = null;
            june_pro_sub_tab_uv_manipulation_skew = null;
            june_pro_sub_tab_uv_manipulation_spherize = null;
            june_pro_sub_tab_uv_manipulation_transformation = null;
            june_pro_sub_tab_uv_manipulation_twisted_corridor = null;
            june_pro_sub_tab_uv_manipulation_recursion = null;
            june_pro_sub_tab_uv_manipulation_twod_rotation = null;
            june_pro_sub_tab_uv_manipulation_threed_rotation = null;
            june_pro_sub_tab_uv_manipulation_threed_pan = null;
            june_pro_sub_tab_uv_manipulation_onlyscreens = null;
            june_pro_sub_tab_uv_manipulation_spinterception = null;
            june_pro_sub_tab_uv_manipulation_quadrant_zoom = null;
            june_pro_sub_tab_uv_manipulation_swivel = null;
            june_pro_sub_tab_uv_manipulation_distaer = null;
            june_pro_sub_tab_uv_manipulation_thanos = null;
            june_pro_sub_tab_vertex_reconstruction_atmosphere = null;
            june_pro_sub_tab_vertex_reconstruction_glitter = null;
            june_pro_sub_tab_vertex_reconstruction_shatterwave = null;
            june_pro_sub_tab_vertex_reconstruction_threed_lighting = null;
            june_pro_sub_tab_vertex_reconstruction_wireframe = null;
            june_pro_sub_tab_vertex_reconstruction_wireframe_shatterwave = null;
            june_pro_sub_tab_vertex_reconstruction_normals = null;
            june_pro_sub_tab_vertex_reconstruction_tripful = null;
            june_pro_sub_tab_vertex_reconstruction_hololens = null;
            june_pro_sub_tab_vertex_reconstruction_lidar = null;
            june_pro_sub_tab_vertex_reconstruction_corruption = null;
            june_pro_sub_tab_vertex_reconstruction_world_wrap = null;
            june_pro_sub_tab_vertex_reconstruction_spotlight = null;
            june_pro_sub_tab_vertex_reconstruction_tryptamines = null;
            preview_notice = null;
        }

        // load the june pro interface 
        private void load_preview(ref Material targetMat) 
        {
            june_pro_tab_blur = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 0, languages.speak("june_pro_tab_blur"));
            june_pro_tab_border = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 1, languages.speak("june_pro_tab_border"));
            june_pro_tab_chromatic = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 2, languages.speak("june_pro_tab_chromatic"));
            june_pro_tab_colour_grading = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 3, languages.speak("june_pro_tab_colour_grading"));
            june_pro_tab_creativity = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 4, languages.speak("june_pro_tab_creativity"));
            june_pro_tab_distortion = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 5, languages.speak("june_pro_tab_distortion"));
            june_pro_tab_enhancements = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 6, languages.speak("june_pro_tab_enhancements"));
            june_pro_tab_experiments = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 7, languages.speak("june_pro_tab_experiments"));
            june_pro_tab_filters = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 8, languages.speak("june_pro_tab_filters"));
            june_pro_tab_frames = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 9, languages.speak("june_pro_tab_frames"));
            june_pro_tab_generation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 10, languages.speak("june_pro_tab_generation"));
            june_pro_tab_glitch = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 11, languages.speak("june_pro_tab_glitch"));
            june_pro_tab_motion = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 12, languages.speak("june_pro_tab_motion"));
            june_pro_tab_others = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 13, languages.speak("june_pro_tab_others"));
            june_pro_tab_outline = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 14, languages.speak("june_pro_tab_outline"));
            june_pro_tab_overlay = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 15, languages.speak("june_pro_tab_overlay"));
            june_pro_tab_stylize = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 16, languages.speak("june_pro_tab_stylize"));
            june_pro_tab_special = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 17, languages.speak("june_pro_tab_special"));
            june_pro_tab_transition = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 18, languages.speak("june_pro_tab_transition"));
            june_pro_tab_triplanar = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 19, languages.speak("june_pro_tab_triplanar"));
            june_pro_tab_uv_manipulation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 20, languages.speak("june_pro_tab_uv_manipulation"));
            june_pro_tab_vertex_reconstruction = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 21, languages.speak("june_pro_tab_vertex_reconstruction"));
            june_pro_tab_zoom = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Primary, 22, languages.speak("june_pro_tab_zoom"));
            june_pro_sub_tab_colour_grading_fine_tuning = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_colour_grading_fine_tuning"));
            june_pro_sub_tab_colour_grading_colour_focus = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_colour_grading_colour_focus"));
            june_pro_sub_tab_colour_grading_colour_replacements = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_colour_grading_colour_replacements"));
            june_pro_sub_tab_colour_grading_colourspace = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_colour_grading_colourspace"));
            june_pro_sub_tab_colour_grading_lighting_adjustment = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_colour_grading_lighting_adjustment"));
            june_pro_sub_tab_colour_grading_sharpness = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_colour_grading_sharpness"));
            june_pro_sub_tab_colour_grading_saturation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_colour_grading_saturation"));
            june_pro_sub_tab_colour_grading_stylize = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_colour_grading_stylize"));
            june_pro_sub_tab_colour_grading_colour_channels = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_colour_grading_colour_channels"));
            june_pro_sub_tab_colour_grading_post_processing = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 9, languages.speak("june_pro_sub_tab_colour_grading_post-processing"));
            june_pro_sub_tab_colour_grading_posterization = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 10, languages.speak("june_pro_sub_tab_colour_grading_posterization"));
            june_pro_sub_tab_creativity_alphenglow = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_creativity_alphenglow"));
            june_pro_sub_tab_creativity_aquamarine = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_creativity_aquamarine"));
            june_pro_sub_tab_creativity_aurora = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_creativity_aurora"));
            june_pro_sub_tab_creativity_bonnibel = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_creativity_bonnibel"));
            june_pro_sub_tab_creativity_butterfly = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_creativity_butterfly"));
            june_pro_sub_tab_creativity_candy = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_creativity_candy"));
            june_pro_sub_tab_creativity_ecstasy = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_creativity_ecstasy"));
            june_pro_sub_tab_creativity_fable = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_creativity_fable"));
            june_pro_sub_tab_creativity_lava_lamp = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_creativity_lava_lamp"));
            june_pro_sub_tab_creativity_marceline = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 9, languages.speak("june_pro_sub_tab_creativity_marceline"));
            june_pro_sub_tab_creativity_smokescreen = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 10, languages.speak("june_pro_sub_tab_creativity_smokescreen"));
            june_pro_sub_tab_creativity_turbulence = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 11, languages.speak("june_pro_sub_tab_creativity_turbulence"));
            june_pro_sub_tab_creativity_rainbow_river = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 12, languages.speak("june_pro_sub_tab_creativity_rainbow_river"));
            june_pro_sub_tab_creativity_portal = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 13, languages.speak("june_pro_sub_tab_creativity_portal"));
            june_pro_sub_tab_creativity_tea = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 14, languages.speak("june_pro_sub_tab_creativity_tea"));
            june_pro_sub_tab_creativity_oil_spill = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 15, languages.speak("june_pro_sub_tab_creativity_oil_spill"));
            june_pro_sub_tab_creativity_art = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 16, languages.speak("june_pro_sub_tab_creativity_art"));
            june_pro_sub_tab_distortion_simple_distortion = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_distortion_simple_distortion"));
            june_pro_sub_tab_distortion_bezier_curve = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_distortion_bezier_curve"));
            june_pro_sub_tab_distortion_blackhole = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_distortion_blackhole"));
            june_pro_sub_tab_distortion_bubbles = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_distortion_bubbles"));
            june_pro_sub_tab_distortion_bumpy_glass = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_distortion_bumpy_glass"));
            june_pro_sub_tab_distortion_exaggeration = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_distortion_exaggeration"));
            june_pro_sub_tab_distortion_liquify = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_distortion_liquify"));
            june_pro_sub_tab_distortion_warp = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_distortion_warp"));
            june_pro_sub_tab_distortion_wave = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_distortion_wave"));
            june_pro_sub_tab_enhancements_anti_aliasing = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_enhancements_anti-aliasing"));
            june_pro_sub_tab_enhancements_denoise = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_enhancements_denoise"));
            june_pro_sub_tab_enhancements_deblur = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_enhancements_deblur"));
            june_pro_sub_tab_enhancements_heavy_lines = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_enhancements_heavy_lines"));
            june_pro_sub_tab_enhancements_soft_lines = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_enhancements_soft_lines"));
            june_pro_sub_tab_enhancements_upscale = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_enhancements_upscale"));
            june_pro_sub_tab_enhancements_contrast_sharpening = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_enhancements_contrast_sharpening"));
            june_pro_sub_tab_enhancements_fdr = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_enhancements_fdr"));
            june_pro_sub_tab_enhancements_screenspace_softshading = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_enhancements_screenspace_softshading"));
            june_pro_sub_tab_experiments_dolly = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_experiments_dolly"));
            june_pro_sub_tab_experiments_cloning = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_experiments_cloning"));
            june_pro_sub_tab_experiments_screen_background = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_experiments_screen_background"));
            june_pro_sub_tab_experiments_depth_viewer = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_experiments_depth_viewer"));
            june_pro_sub_tab_experiments_objectify = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_experiments_objectify"));
            june_pro_sub_tab_filters_colour_incorrection = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_filters_colour_incorrection"));
            june_pro_sub_tab_filters_colourblind_simulation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_filters_colourblind_simulation"));
            june_pro_sub_tab_filters_corners = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_filters_corners"));
            june_pro_sub_tab_filters_colour_crush = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_filters_colour_crush"));
            june_pro_sub_tab_filters_colour_cycline = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_filters_colour_cycline"));
            june_pro_sub_tab_filters_colour_wheel = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_filters_colour_wheel"));
            june_pro_sub_tab_filters_crt = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_filters_crt"));
            june_pro_sub_tab_filters_monotone = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_filters_monotone"));
            june_pro_sub_tab_filters_duotone = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_filters_duotone"));
            june_pro_sub_tab_filters_tritone = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 9, languages.speak("june_pro_sub_tab_filters_tritone"));
            june_pro_sub_tab_filters_engraving = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 10, languages.speak("june_pro_sub_tab_filters_engraving"));
            june_pro_sub_tab_filters_linocut = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 11, languages.speak("june_pro_sub_tab_filters_linocut"));
            june_pro_sub_tab_filters_light_leak = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 12, languages.speak("june_pro_sub_tab_filters_light_leak"));
            june_pro_sub_tab_filters_film = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 13, languages.speak("june_pro_sub_tab_filters_film"));
            june_pro_sub_tab_filters_normal_mapper = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 14, languages.speak("june_pro_sub_tab_filters_normal_mapper"));
            june_pro_sub_tab_filters_chrome = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 15, languages.speak("june_pro_sub_tab_filters_chrome"));
            june_pro_sub_tab_filters_rainbow = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 16, languages.speak("june_pro_sub_tab_filters_rainbow"));
            june_pro_sub_tab_filters_ramp = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 17, languages.speak("june_pro_sub_tab_filters_ramp"));
            june_pro_sub_tab_filters_gradient = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 18, languages.speak("june_pro_sub_tab_filters_gradient"));
            june_pro_sub_tab_filters_low_ink = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 19, languages.speak("june_pro_sub_tab_filters_low_ink"));
            june_pro_sub_tab_filters_low_bitrate = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 20, languages.speak("june_pro_sub_tab_filters_low_bitrate"));
            june_pro_sub_tab_filters_grain = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 21, languages.speak("june_pro_sub_tab_filters_grain"));
            june_pro_sub_tab_filters_glitter = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 22, languages.speak("june_pro_sub_tab_filters_glitter"));
            june_pro_sub_tab_filters_moire = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 23, languages.speak("june_pro_sub_tab_filters_moire"));
            june_pro_sub_tab_filters_sepia = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 24, languages.speak("june_pro_sub_tab_filters_sepia"));
            june_pro_sub_tab_filters_solarize = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 25, languages.speak("june_pro_sub_tab_filters_solarize"));
            june_pro_sub_tab_filters_specular = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 26, languages.speak("june_pro_sub_tab_filters_specular"));
            june_pro_sub_tab_filters_tie_dye = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 27, languages.speak("june_pro_sub_tab_filters_tie_dye"));
            june_pro_sub_tab_filters_technicolour = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 28, languages.speak("june_pro_sub_tab_filters_technicolour"));
            june_pro_sub_tab_filters_thermal = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 29, languages.speak("june_pro_sub_tab_filters_thermal"));
            june_pro_sub_tab_filters_threshold = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 30, languages.speak("june_pro_sub_tab_filters_threshold"));
            june_pro_sub_tab_filters_night_vision = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 31, languages.speak("june_pro_sub_tab_filters_night_vision"));
            june_pro_sub_tab_filters_ultra_violet = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 32, languages.speak("june_pro_sub_tab_filters_ultra_violet"));
            june_pro_sub_tab_filters_wall_glow = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 33, languages.speak("june_pro_sub_tab_filters_wall_glow"));
            june_pro_sub_tab_filters_vhs = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 34, languages.speak("june_pro_sub_tab_filters_vhs"));
            june_pro_sub_tab_filters_vignette = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 35, languages.speak("june_pro_sub_tab_filters_vignette"));
            june_pro_sub_tab_filters_dither = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 36, languages.speak("june_pro_sub_tab_filters_dither"));
            june_pro_sub_tab_filters_fauxlate = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 37, languages.speak("june_pro_sub_tab_filters_fauxlate"));
            june_pro_sub_tab_filters_lieless = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 38, languages.speak("june_pro_sub_tab_filters_lieless"));
            june_pro_sub_tab_generation_lines = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_generation_lines"));
            june_pro_sub_tab_generation_ring_colors = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_generation_ring_colors"));
            june_pro_sub_tab_generation_noise_colors = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_generation_noise_colors"));
            june_pro_sub_tab_generation_sdf_colors = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_generation_sdf_colors"));
            june_pro_sub_tab_generation_shapes_colors = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_generation_shapes_colors"));
            june_pro_sub_tab_generation_shapes_uvs = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_generation_shapes_uvs"));
            june_pro_sub_tab_generation_spiral_colors = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_generation_spiral_colors"));
            june_pro_sub_tab_generation_spiral_uvs = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_generation_spiral_uvs"));
            june_pro_sub_tab_generation_hearts = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_generation_hearts"));
            june_pro_sub_tab_glitch_simple = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_glitch_simple"));
            june_pro_sub_tab_glitch_advanced = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_glitch_advanced"));
            june_pro_sub_tab_motion_acid = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_motion_acid"));
            june_pro_sub_tab_motion_blur = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_motion_blur"));
            june_pro_sub_tab_motion_distortion = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_motion_distortion"));
            june_pro_sub_tab_motion_freeze = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_motion_freeze"));
            june_pro_sub_tab_motion_rgb_freeze = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_motion_rgb_freeze"));
            june_pro_sub_tab_motion_glitch = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_motion_glitch"));
            june_pro_sub_tab_motion_chromatic = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_motion_chromatic"));
            june_pro_sub_tab_motion_pixel_sort = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_motion_pixel_sort"));
            june_pro_sub_tab_motion_trail = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_motion_trail"));
            june_pro_sub_tab_motion_tranquility = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 9, languages.speak("june_pro_sub_tab_motion_tranquility"));
            june_pro_sub_tab_motion_tranceless = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 10, languages.speak("june_pro_sub_tab_motion_tranceless"));
            june_pro_sub_tab_motion_data_mosh = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 11, languages.speak("june_pro_sub_tab_motion_data_mosh"));
            june_pro_sub_tab_motion_frame_rate = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 12, languages.speak("june_pro_sub_tab_motion_frame_rate"));
            june_pro_sub_tab_motion_fensterxd = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 13, languages.speak("june_pro_sub_tab_motion_fensterxd"));
            june_pro_sub_tab_motion_fading_projections = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 14, languages.speak("june_pro_sub_tab_motion_fading_projections"));
            june_pro_sub_tab_motion_motear = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 15, languages.speak("june_pro_sub_tab_motion_motear"));
            june_pro_sub_tab_motion_lake_fill = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 16, languages.speak("june_pro_sub_tab_motion_lake_fill"));
            june_pro_sub_tab_others_astral = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_others_astral"));
            june_pro_sub_tab_others_astral_rgb = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_others_astral_rgb"));
            june_pro_sub_tab_others_apart = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_others_apart"));
            june_pro_sub_tab_others_colour_diffusion = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_others_colour_diffusion"));
            june_pro_sub_tab_others_holepuncher = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_others_holepuncher"));
            june_pro_sub_tab_others_glowstick = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_others_glowstick"));
            june_pro_sub_tab_others_grid_checkerboard = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_others_grid_checkerboard"));
            june_pro_sub_tab_others_hallucinogen = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_others_hallucinogen"));
            june_pro_sub_tab_others_lenticular_halo = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_others_lenticular_halo"));
            june_pro_sub_tab_others_meta_image = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 9, languages.speak("june_pro_sub_tab_others_meta_image"));
            june_pro_sub_tab_others_palette = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 10, languages.speak("june_pro_sub_tab_others_palette"));
            june_pro_sub_tab_others_rain_line = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 11, languages.speak("june_pro_sub_tab_others_rain_line"));
            june_pro_sub_tab_others_rim = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 12, languages.speak("june_pro_sub_tab_others_rim"));
            june_pro_sub_tab_others_scanline_overlay = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 13, languages.speak("june_pro_sub_tab_others_scanline_overlay"));
            june_pro_sub_tab_others_stripes = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 14, languages.speak("june_pro_sub_tab_others_stripes"));
            june_pro_sub_tab_others_sunbeams = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 15, languages.speak("june_pro_sub_tab_others_sunbeams"));
            june_pro_sub_tab_others_water_reflection = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 16, languages.speak("june_pro_sub_tab_others_water_reflection"));
            june_pro_sub_tab_others_camouflage = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 17, languages.speak("june_pro_sub_tab_others_camouflage"));
            june_pro_sub_tab_others_inception = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 18, languages.speak("june_pro_sub_tab_others_inception"));
            june_pro_sub_tab_others_object_detection = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 19, languages.speak("june_pro_sub_tab_others_object_detection"));
            june_pro_sub_tab_others_fog = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 20, languages.speak("june_pro_sub_tab_others_fog"));
            june_pro_sub_tab_others_silhouette = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 21, languages.speak("june_pro_sub_tab_others_silhouette"));
            june_pro_sub_tab_others_prismatic_layers = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 22, languages.speak("june_pro_sub_tab_others_prismatic_layers"));
            june_pro_sub_tab_others_hexatile = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 23, languages.speak("june_pro_sub_tab_others_hexatile"));
            june_pro_sub_tab_others_secrets = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 24, languages.speak("june_pro_sub_tab_others_secrets"));
            june_pro_sub_tab_others_divider = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 25, languages.speak("june_pro_sub_tab_others_divider"));
            june_pro_sub_tab_stylize_compression = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_stylize_compression"));
            june_pro_sub_tab_stylize_crosshatching = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_stylize_crosshatching"));
            june_pro_sub_tab_stylize_crystalize = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_stylize_crystalize"));
            june_pro_sub_tab_stylize_dots = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_stylize_dots"));
            june_pro_sub_tab_stylize_emboss = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_stylize_emboss"));
            june_pro_sub_tab_stylize_impressionism = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_stylize_impressionism"));
            june_pro_sub_tab_stylize_mosaic = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_stylize_mosaic"));
            june_pro_sub_tab_stylize_neon_rings = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_stylize_neon_rings"));
            june_pro_sub_tab_stylize_oil = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_stylize_oil"));
            june_pro_sub_tab_stylize_monitor = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 9, languages.speak("june_pro_sub_tab_stylize_monitor"));
            june_pro_sub_tab_stylize_neon = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 10, languages.speak("june_pro_sub_tab_stylize_neon"));
            june_pro_sub_tab_stylize_pop_art = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 11, languages.speak("june_pro_sub_tab_stylize_pop_art"));
            june_pro_sub_tab_stylize_unicode = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 12, languages.speak("june_pro_sub_tab_stylize_unicode"));
            june_pro_sub_tab_stylize_halftone = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 13, languages.speak("june_pro_sub_tab_stylize_halftone"));
            june_pro_sub_tab_stylize_halftone_circles = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 14, languages.speak("june_pro_sub_tab_stylize_halftone_circles"));
            june_pro_sub_tab_stylize_halftone_spiral = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 15, languages.speak("june_pro_sub_tab_stylize_halftone_spiral"));
            june_pro_sub_tab_stylize_halftone_rgb = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 16, languages.speak("june_pro_sub_tab_stylize_halftone_rgb"));
            june_pro_sub_tab_special_bubbles = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_special_bubbles"));
            june_pro_sub_tab_special_confetti = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_special_confetti"));
            june_pro_sub_tab_special_data_stream = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_special_data_stream"));
            june_pro_sub_tab_special_lens_flare = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_special_lens_flare"));
            june_pro_sub_tab_special_hexagonal_shield = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_special_hexagonal_shield"));
            june_pro_sub_tab_special_lightning = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_special_lightning"));
            june_pro_sub_tab_special_mapping = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_special_mapping"));
            june_pro_sub_tab_special_rain_drops = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_special_rain_drops"));
            june_pro_sub_tab_special_plexus = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_special_plexus"));
            june_pro_sub_tab_special_shanshuo = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 9, languages.speak("june_pro_sub_tab_special_shanshuo"));
            june_pro_sub_tab_special_star_trail = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 10, languages.speak("june_pro_sub_tab_special_star_trail"));
            june_pro_sub_tab_special_spotlights = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 11, languages.speak("june_pro_sub_tab_special_spotlights"));
            june_pro_sub_tab_special_visualizer = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 12, languages.speak("june_pro_sub_tab_special_visualizer"));
            june_pro_sub_tab_special_warp_drive = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 13, languages.speak("june_pro_sub_tab_special_warp_drive"));
            june_pro_sub_tab_uv_manipulation_bender = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_uv_manipulation_bender"));
            june_pro_sub_tab_uv_manipulation_movement = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_uv_manipulation_movement"));
            june_pro_sub_tab_uv_manipulation_clamp = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_uv_manipulation_clamp"));
            june_pro_sub_tab_uv_manipulation_coordinates = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_uv_manipulation_coordinates"));
            june_pro_sub_tab_uv_manipulation_dither = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_uv_manipulation_dither"));
            june_pro_sub_tab_uv_manipulation_kaleidoscope = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_uv_manipulation_kaleidoscope"));
            june_pro_sub_tab_uv_manipulation_mirror = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_uv_manipulation_mirror"));
            june_pro_sub_tab_uv_manipulation_scroll = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_uv_manipulation_scroll"));
            june_pro_sub_tab_uv_manipulation_shake_and_earthquake = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_uv_manipulation_shake_(and_earthquake)"));
            june_pro_sub_tab_uv_manipulation_slicer = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 9, languages.speak("june_pro_sub_tab_uv_manipulation_slicer"));
            june_pro_sub_tab_uv_manipulation_melt = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 10, languages.speak("june_pro_sub_tab_uv_manipulation_melt"));
            june_pro_sub_tab_uv_manipulation_mirror_shatter = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 11, languages.speak("june_pro_sub_tab_uv_manipulation_mirror_shatter"));
            june_pro_sub_tab_uv_manipulation_ring_rotation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 12, languages.speak("june_pro_sub_tab_uv_manipulation_ring_rotation"));
            june_pro_sub_tab_uv_manipulation_refraction = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 13, languages.speak("june_pro_sub_tab_uv_manipulation_refraction"));
            june_pro_sub_tab_uv_manipulation_pixelation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 14, languages.speak("june_pro_sub_tab_uv_manipulation_pixelation"));
            june_pro_sub_tab_uv_manipulation_pixel_shifter = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 15, languages.speak("june_pro_sub_tab_uv_manipulation_pixel_shifter"));
            june_pro_sub_tab_uv_manipulation_scanline = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 16, languages.speak("june_pro_sub_tab_uv_manipulation_scanline"));
            june_pro_sub_tab_uv_manipulation_shuffle = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 17, languages.speak("june_pro_sub_tab_uv_manipulation_shuffle"));
            june_pro_sub_tab_uv_manipulation_skew = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 18, languages.speak("june_pro_sub_tab_uv_manipulation_skew"));
            june_pro_sub_tab_uv_manipulation_spherize = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 19, languages.speak("june_pro_sub_tab_uv_manipulation_spherize"));
            june_pro_sub_tab_uv_manipulation_transformation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 20, languages.speak("june_pro_sub_tab_uv_manipulation_transformation"));
            june_pro_sub_tab_uv_manipulation_twisted_corridor = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 21, languages.speak("june_pro_sub_tab_uv_manipulation_twisted_corridor"));
            june_pro_sub_tab_uv_manipulation_recursion = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 22, languages.speak("june_pro_sub_tab_uv_manipulation_recursion"));
            june_pro_sub_tab_uv_manipulation_twod_rotation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 23, languages.speak("june_pro_sub_tab_uv_manipulation_twod_rotation"));
            june_pro_sub_tab_uv_manipulation_threed_rotation = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 24, languages.speak("june_pro_sub_tab_uv_manipulation_threed_rotation"));
            june_pro_sub_tab_uv_manipulation_threed_pan = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 25, languages.speak("june_pro_sub_tab_uv_manipulation_threed_pan"));
            june_pro_sub_tab_uv_manipulation_onlyscreens = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 26, languages.speak("june_pro_sub_tab_uv_manipulation_onlyscreens"));
            june_pro_sub_tab_uv_manipulation_spinterception = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 27, languages.speak("june_pro_sub_tab_uv_manipulation_spinterception"));
            june_pro_sub_tab_uv_manipulation_quadrant_zoom = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 28, languages.speak("june_pro_sub_tab_uv_manipulation_quadrant_zoom"));
            june_pro_sub_tab_uv_manipulation_swivel = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 29, languages.speak("june_pro_sub_tab_uv_manipulation_swivel"));
            june_pro_sub_tab_uv_manipulation_distaer = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 30, languages.speak("june_pro_sub_tab_uv_manipulation_distaer"));
            june_pro_sub_tab_uv_manipulation_thanos = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 31, languages.speak("june_pro_sub_tab_uv_manipulation_thanos"));
            june_pro_sub_tab_vertex_reconstruction_atmosphere = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 0, languages.speak("june_pro_sub_tab_vertex_reconstruction_atmosphere"));
            june_pro_sub_tab_vertex_reconstruction_glitter = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 1, languages.speak("june_pro_sub_tab_vertex_reconstruction_glitter"));
            june_pro_sub_tab_vertex_reconstruction_shatterwave = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 2, languages.speak("june_pro_sub_tab_vertex_reconstruction_shatterwave"));
            june_pro_sub_tab_vertex_reconstruction_threed_lighting = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 3, languages.speak("june_pro_sub_tab_vertex_reconstruction_threed_lighting"));
            june_pro_sub_tab_vertex_reconstruction_wireframe = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 4, languages.speak("june_pro_sub_tab_vertex_reconstruction_wireframe"));
            june_pro_sub_tab_vertex_reconstruction_wireframe_shatterwave = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 5, languages.speak("june_pro_sub_tab_vertex_reconstruction_wireframe_shatterwave"));
            june_pro_sub_tab_vertex_reconstruction_normals = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 6, languages.speak("june_pro_sub_tab_vertex_reconstruction_normals"));
            june_pro_sub_tab_vertex_reconstruction_tripful = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 7, languages.speak("june_pro_sub_tab_vertex_reconstruction_tripful"));
            june_pro_sub_tab_vertex_reconstruction_hololens = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 8, languages.speak("june_pro_sub_tab_vertex_reconstruction_hololens"));
            june_pro_sub_tab_vertex_reconstruction_lidar = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 9, languages.speak("june_pro_sub_tab_vertex_reconstruction_lidar"));
            june_pro_sub_tab_vertex_reconstruction_corruption = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 10, languages.speak("june_pro_sub_tab_vertex_reconstruction_corruption"));
            june_pro_sub_tab_vertex_reconstruction_world_wrap = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 11, languages.speak("june_pro_sub_tab_vertex_reconstruction_world_wrap"));
            june_pro_sub_tab_vertex_reconstruction_spotlight = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 12, languages.speak("june_pro_sub_tab_vertex_reconstruction_spotlight"));
            june_pro_sub_tab_vertex_reconstruction_tryptamines = new Tab(ref targetMat, ref theme, (int)Tab.tab_sizes.Sub, 13, languages.speak("june_pro_sub_tab_vertex_reconstruction_tryptamines"));
            preview_notice = new NoticeBox(ref theme, "june_pro_preview_notice");
            preview_loaded = true;
        }

        // switch to preview mode
        private void preview_on(ref Material targetMat)
        {
            if (targetMat == null) return;
            load_preview(ref targetMat);
            preview_loaded = true;
            is_preview = true;
        }

        // switch to normal mode
        private void preview_off(ref Material targetMat)
        {
            if (targetMat == null) return;
            unload_preview();
            preview_loaded = false;
            is_preview = false;
        }

        // per-shader ui here
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Material targetMat = materialEditor.target as Material;
            // load global
            if (!loaded) load(ref targetMat);
            EditorGUI.BeginChangeCheck();
            header.draw();
            update.draw();
            // load june pro preview (if using)
            if (is_preview) {
                if (!preview_loaded) load_preview(ref targetMat);
                OnGUIPreview(materialEditor, properties, ref targetMat);
                return;
            }
            // rendering tab
            tab_rendering.draw();
            if (tab_rendering.is_expanded)
            {
                Components.start_foldout();
                prop_rendering_power = ShaderGUI.FindProperty("_LiteRenderingPower", properties);
                prop_rendering_falloff_start = ShaderGUI.FindProperty("_LiteRenderingFalloffStart", properties);
                prop_rendering_falloff_end = ShaderGUI.FindProperty("_LiteRenderingFalloffEnd", properties);
                prop_rendering_oob = ShaderGUI.FindProperty("_LiteRenderingOOB", properties);
                materialEditor.ShaderProperty(prop_rendering_power, languages.speak("prop_rendering_shader_power"));
                materialEditor.ShaderProperty(prop_rendering_falloff_start, languages.speak("prop_rendering_falloff_start"));
                materialEditor.ShaderProperty(prop_rendering_falloff_end, languages.speak("prop_rendering_falloff_end"));
                prop_rendering_oob.floatValue = (float)(enumOOB)EditorGUILayout.EnumPopup(languages.speak("prop_rendering_out_of_bounds_style"), (enumOOB)prop_rendering_oob.floatValue);
                Components.end_foldout();
            }
            // audio link tab
            tab_audio_link.draw();
            if (tab_audio_link.is_expanded)
            {
                Components.start_foldout();
                prop_audio_link_module = ShaderGUI.FindProperty("_LiteAudioLinkModule", properties);
                prop_audio_link_band = ShaderGUI.FindProperty("_LiteAudioLinkBand", properties);
                prop_audio_link_power = ShaderGUI.FindProperty("_LiteAudioLinkPower", properties);
                prop_audio_link_min = ShaderGUI.FindProperty("_LiteAudioLinkMin", properties);
                prop_audio_link_max = ShaderGUI.FindProperty("_LiteAudioLinkMax", properties);
                materialEditor.ShaderProperty(prop_audio_link_module, languages.speak("prop_audio_link_enable"));
                Components.start_dynamic_disable(prop_audio_link_module.floatValue == (float)enumToggle.Off);
                prop_audio_link_band.floatValue = (float)(enumAudioLink)EditorGUILayout.EnumPopup(languages.speak("prop_audio_link_band"), (enumAudioLink)prop_audio_link_band.floatValue);
                materialEditor.ShaderProperty(prop_audio_link_power, languages.speak("prop_audio_link_power"));
                materialEditor.ShaderProperty(prop_audio_link_min, languages.speak("prop_audio_link_min"));
                materialEditor.ShaderProperty(prop_audio_link_max, languages.speak("prop_audio_link_max"));
                Components.end_dynamic_disable(prop_audio_link_module.floatValue == (float)enumToggle.Off);
                Components.end_foldout();
            }
            // blur tab
            tab_blur.draw();
            if (tab_blur.is_expanded)
            {
                Components.start_foldout();
                prop_blur_module = ShaderGUI.FindProperty("_LiteBlurModule", properties);
                prop_blur_style = ShaderGUI.FindProperty("_LiteBlurStyle", properties);
                prop_blur_power = ShaderGUI.FindProperty("_LiteBlurPower", properties);
                prop_blur_radius = ShaderGUI.FindProperty("_LiteBlurRadius", properties);
                prop_blur_dithering = ShaderGUI.FindProperty("_LiteBlurDithering", properties);
                prop_blur_transparency = ShaderGUI.FindProperty("_LiteBlurTransparency", properties);
                prop_blur_color = ShaderGUI.FindProperty("_LiteBlurColor", properties);
                prop_rendering_quality = ShaderGUI.FindProperty("_LiteRenderingQuality", properties);
                materialEditor.ShaderProperty(prop_blur_module, languages.speak("prop_blur_enable"));
                materialEditor.ShaderProperty(prop_rendering_quality, languages.speak("prop_blur_high_quality"));
                Components.start_dynamic_disable(prop_blur_module.floatValue == (float)enumToggle.Off);
                prop_blur_style.floatValue = (float)(enumBlur)EditorGUILayout.EnumPopup(languages.speak("prop_blur_style"), (enumBlur)prop_blur_style.floatValue);
                materialEditor.ShaderProperty(prop_blur_power, languages.speak("prop_blur_power"));
                materialEditor.ShaderProperty(prop_blur_dithering, languages.speak("prop_blur_dithering"));
                materialEditor.ShaderProperty(prop_blur_radius, languages.speak("prop_blur_radius"));
                materialEditor.ShaderProperty(prop_blur_transparency, languages.speak("prop_blur_transparency"));
                materialEditor.ShaderProperty(prop_blur_color, languages.speak("prop_blur_color"));
                Components.end_dynamic_disable(prop_blur_module.floatValue == (float)enumToggle.Off);
                Components.end_foldout();
            }
            // border tab
            tab_border.draw();
            if (tab_border.is_expanded)
            {
                Components.start_foldout();
                prop_border_module = ShaderGUI.FindProperty("_LiteBorderModule", properties);
                prop_border_style = ShaderGUI.FindProperty("_LiteBorderStyle", properties);
                prop_border_color = ShaderGUI.FindProperty("_LiteBorderColor", properties);
                prop_border_power = ShaderGUI.FindProperty("_LiteBorderPower", properties);
                prop_border_soften = ShaderGUI.FindProperty("_LiteBorderSoften", properties);
                materialEditor.ShaderProperty(prop_border_module, languages.speak("prop_border_enable"));
                Components.start_dynamic_disable(prop_border_module.floatValue == (float)enumToggle.Off);
                prop_border_style.floatValue = (float)(enumBorder)EditorGUILayout.EnumPopup(languages.speak("prop_border_style"), (enumBorder)prop_border_style.floatValue);
                materialEditor.ShaderProperty(prop_border_color, languages.speak("prop_border_color"));
                materialEditor.ShaderProperty(prop_border_power, languages.speak("prop_border_power"));
                materialEditor.ShaderProperty(prop_border_soften, languages.speak("prop_border_soften"));
                Components.end_dynamic_disable(prop_border_module.floatValue == (float)enumToggle.Off);
                Components.end_foldout();
            }
            // colouring tab
            tab_coloring.draw();
            if (tab_coloring.is_expanded)
            {
                Components.start_foldout();
                prop_coloring_module = ShaderGUI.FindProperty("_LiteColoringModule", properties);
                prop_coloring_rgb_multiply = ShaderGUI.FindProperty("_LiteColoringRGBMultiply", properties);
                prop_coloring_rgb_overlay = ShaderGUI.FindProperty("_LiteColoringRGBOverlay", properties);
                prop_coloring_rgb_overlay_transparency = ShaderGUI.FindProperty("_LiteColoringRGBOverlayTransparency", properties);
                prop_coloring_hsv_style = ShaderGUI.FindProperty("_LiteColoringHSVStyle", properties);
                prop_coloring_hsv_h = ShaderGUI.FindProperty("_LiteColoringHSVh", properties);
                prop_coloring_hsv_s = ShaderGUI.FindProperty("_LiteColoringHSVs", properties);
                prop_coloring_hsv_v = ShaderGUI.FindProperty("_LiteColoringHSVv", properties);
                prop_coloring_invert = ShaderGUI.FindProperty("_LiteColoringInvert", properties);
                prop_coloring_drain = ShaderGUI.FindProperty("_LiteColoringDrain", properties);
                prop_coloring_darkness = ShaderGUI.FindProperty("_LiteColoringDarkness", properties);
                prop_coloring_brightness = ShaderGUI.FindProperty("_LiteColoringBrightness", properties);
                prop_coloring_emission = ShaderGUI.FindProperty("_LiteColoringEmission", properties);
                prop_coloring_posterization = ShaderGUI.FindProperty("_LiteColoringPosterization", properties);
                prop_coloring_color_grading = ShaderGUI.FindProperty("_LiteColoringColorGrading", properties);
                prop_coloring_color_grading_tone = ShaderGUI.FindProperty("_LiteColoringColorGradingTone", properties);
                prop_coloring_sharpness = ShaderGUI.FindProperty("_LiteColoringSharpness", properties);
                prop_coloring_rainbow = ShaderGUI.FindProperty("_LiteColoringRainbow", properties);
                prop_coloring_rainbow_speed = ShaderGUI.FindProperty("_LiteColoringRainbowSpeed", properties);
                materialEditor.ShaderProperty(prop_coloring_module, languages.speak("prop_coloring_enable"));
                Components.start_dynamic_disable(prop_coloring_module.floatValue == (float)enumToggle.Off);
                materialEditor.ShaderProperty(prop_coloring_rgb_multiply, languages.speak("prop_coloring_rgb_multiply"));
                materialEditor.ShaderProperty(prop_coloring_rgb_overlay_transparency, languages.speak("prop_coloring_rgb_overlay_transparency"));
                Components.start_dynamic_disable(prop_coloring_rgb_overlay_transparency.floatValue == 0f);
                materialEditor.ShaderProperty(prop_coloring_rgb_overlay, languages.speak("prop_coloring_rgb_overlay"));
                Components.end_dynamic_disable(prop_coloring_rgb_overlay_transparency.floatValue == 0f);
                prop_coloring_hsv_style.floatValue = (float)(enumHSV)EditorGUILayout.EnumPopup(languages.speak("prop_coloring_hsv_style"), (enumHSV)prop_coloring_hsv_style.floatValue);
                Components.start_dynamic_disable(prop_coloring_hsv_style.floatValue == (float)enumHSV.Disabled);
                materialEditor.ShaderProperty(prop_coloring_hsv_h, languages.speak("prop_coloring_hsv_hue"));
                materialEditor.ShaderProperty(prop_coloring_hsv_s, languages.speak("prop_coloring_hsv_saturation"));
                materialEditor.ShaderProperty(prop_coloring_hsv_v, languages.speak("prop_coloring_hsv_value"));
                Components.end_dynamic_disable(prop_coloring_hsv_style.floatValue == (float)enumHSV.Disabled);
                materialEditor.ShaderProperty(prop_coloring_rainbow, languages.speak("prop_rainbow_saturation"));
                Components.start_dynamic_disable(prop_coloring_rainbow.floatValue == (float)enumToggle.Off);
                materialEditor.ShaderProperty(prop_coloring_rainbow_speed, languages.speak("prop_rainbow_speed"));
                Components.end_dynamic_disable(prop_coloring_rainbow.floatValue == (float)enumToggle.Off);
                materialEditor.ShaderProperty(prop_coloring_invert, languages.speak("prop_coloring_invert"));
                materialEditor.ShaderProperty(prop_coloring_drain, languages.speak("prop_coloring_color_drain"));
                materialEditor.ShaderProperty(prop_coloring_darkness, languages.speak("prop_coloring_darkness"));
                materialEditor.ShaderProperty(prop_coloring_brightness, languages.speak("prop_coloring_brightness"));
                materialEditor.ShaderProperty(prop_coloring_emission, languages.speak("prop_coloring_emission"));
                materialEditor.ShaderProperty(prop_coloring_posterization, languages.speak("prop_coloring_posterization"));
                materialEditor.ShaderProperty(prop_coloring_sharpness, languages.speak("prop_coloring_sharpness"));
                materialEditor.ShaderProperty(prop_coloring_color_grading, languages.speak("prop_coloring_color_grading"));
                Components.start_dynamic_disable(prop_coloring_color_grading.floatValue == 0f);
                materialEditor.ShaderProperty(prop_coloring_color_grading_tone, languages.speak("prop_coloring_color_grading_tone"));
                Components.end_dynamic_disable(prop_coloring_color_grading.floatValue == 0f);
                Components.end_dynamic_disable(prop_coloring_module.floatValue == (float)enumToggle.Off);
                Components.end_foldout();
            }
            // distortion tab
            tab_distortion.draw();
            if (tab_distortion.is_expanded)
            {
               Components.start_foldout();
                prop_distortion_module = ShaderGUI.FindProperty("_LiteDistortionModule", properties);
                prop_distortion_style = ShaderGUI.FindProperty("_LiteDistortionStyle", properties);
                prop_distortion_power_x = ShaderGUI.FindProperty("_LiteDistortionPowerX", properties);
                prop_distortion_power_y = ShaderGUI.FindProperty("_LiteDistortionPowerY", properties);
                prop_distortion_speed_x = ShaderGUI.FindProperty("_LiteDistortionSpeedX", properties);
                prop_distortion_speed_y = ShaderGUI.FindProperty("_LiteDistortionSpeedY", properties);
                prop_distortion_texture = ShaderGUI.FindProperty("_LiteDistortionTexture", properties);
                prop_distortion_texture_scale = ShaderGUI.FindProperty("_LiteDistortionTextureScale", properties);
                prop_distortion_wobble = ShaderGUI.FindProperty("_LiteDistortionWobble", properties);
                prop_distortion_wobble_power = ShaderGUI.FindProperty("_LiteDistortionWobblePower", properties);
                prop_distortion_wobble_speed = ShaderGUI.FindProperty("_LiteDistortionWobbleSpeed", properties);
                prop_distortion_wobble_coverage = ShaderGUI.FindProperty("_LiteDistortionWobbleCoverage", properties);
                materialEditor.ShaderProperty(prop_distortion_module, languages.speak("prop_distortion_enable"));
                Components.start_dynamic_disable(prop_distortion_module.floatValue == (float)enumToggle.Off);
                prop_distortion_style.floatValue = (float)(enumDistortion)EditorGUILayout.EnumPopup(languages.speak("prop_distortion_style"), (enumDistortion)prop_distortion_style.floatValue);
                materialEditor.ShaderProperty(prop_distortion_power_x, languages.speak("prop_distortion_power_x"));
                materialEditor.ShaderProperty(prop_distortion_power_y, languages.speak("prop_distortion_power_y"));
                materialEditor.ShaderProperty(prop_distortion_speed_x, languages.speak("prop_distortion_speed_x"));
                materialEditor.ShaderProperty(prop_distortion_speed_y, languages.speak("prop_distortion_speed_y"));
                Components.start_dynamic_disable(prop_distortion_style.floatValue != (float)enumDistortion.Texture);
                materialEditor.ShaderProperty(prop_distortion_texture, languages.speak("prop_distortion_texture"));
                materialEditor.ShaderProperty(prop_distortion_texture_scale, languages.speak("prop_distortion_texture_scale"));
                Components.end_dynamic_disable(prop_distortion_style.floatValue != (float)enumDistortion.Texture);
                prop_distortion_wobble.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_distortion_add_in_wobble"), (enumToggle)prop_distortion_wobble.floatValue);
                if (prop_distortion_wobble.floatValue == (float)enumToggle.On) 
                {
                    materialEditor.ShaderProperty(prop_distortion_wobble_power, languages.speak("prop_distortion_wobble_power"));
                    materialEditor.ShaderProperty(prop_distortion_wobble_speed, languages.speak("prop_distortion_wobble_speed"));
                    materialEditor.ShaderProperty(prop_distortion_wobble_coverage, languages.speak("prop_distortion_wobble_coverage"));
                }
                Components.end_dynamic_disable(prop_distortion_module.floatValue == (float)enumToggle.Off);
                Components.end_foldout();
            }
            // filter tab
            tab_filter.draw();
            if (tab_filter.is_expanded)
            {
                Components.start_foldout();
                prop_filter_module = ShaderGUI.FindProperty("_LiteFilterModule", properties);
                materialEditor.ShaderProperty(prop_filter_module, languages.speak("prop_filter_enable"));
                // vignette
                tab_sub_filter_vignette.draw();
                if (tab_sub_filter_vignette.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_vignette = ShaderGUI.FindProperty("_LiteFilterVignette", properties);
                    prop_filter_vignette_power = ShaderGUI.FindProperty("_LiteFilterVignettePower", properties);
                    prop_filter_vignette_color = ShaderGUI.FindProperty("_LiteFilterVignetteColor", properties);
                    prop_filter_vignette.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_vignette_enable"), (enumToggle)prop_filter_vignette.floatValue);
                    Components.start_dynamic_disable(prop_filter_vignette.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_vignette_power, languages.speak("prop_vignette_power"));
                    materialEditor.ShaderProperty(prop_filter_vignette_color, languages.speak("prop_vignette_color"));
                    Components.end_dynamic_disable(prop_filter_vignette.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                // color crush
                tab_sub_filter_color_crush.draw();
                if (tab_sub_filter_color_crush.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_color_crush = ShaderGUI.FindProperty("_LiteFilterColorCrush", properties);
                    prop_filter_color_crush_power = ShaderGUI.FindProperty("_LiteFilterColorCrushPower", properties);
                    prop_filter_color_crush.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_color_crush_enable"), (enumToggle)prop_filter_color_crush.floatValue);
                    Components.start_dynamic_disable(prop_filter_color_crush.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_color_crush_power, languages.speak("prop_color_crush_power"));
                    Components.end_dynamic_disable(prop_filter_color_crush.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                // duotone
                tab_sub_filter_duotone.draw();
                if (tab_sub_filter_duotone.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_duotone = ShaderGUI.FindProperty("_LiteFilterDuotone", properties);
                    prop_filter_duotone_transparency = ShaderGUI.FindProperty("_LiteFilterDuotoneTransparency", properties);
                    prop_filter_duotone_color_one = ShaderGUI.FindProperty("_LiteFilterDuotoneColorOne", properties);
                    prop_filter_duotone_color_two = ShaderGUI.FindProperty("_LiteFilterDuotoneColorTwo", properties);
                    prop_filter_duotone_threshold = ShaderGUI.FindProperty("_LiteFilterDuotoneThreshold", properties);
                    prop_filter_duotone.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_duotone_enable"), (enumToggle)prop_filter_duotone.floatValue);
                    Components.start_dynamic_disable(prop_filter_duotone.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_duotone_transparency, languages.speak("prop_duotone_transparency"));
                    materialEditor.ShaderProperty(prop_filter_duotone_color_one, languages.speak("prop_duotone_color_one"));
                    materialEditor.ShaderProperty(prop_filter_duotone_color_two, languages.speak("prop_duotone_color_two"));
                    materialEditor.ShaderProperty(prop_filter_duotone_threshold, languages.speak("prop_duotone_threshold"));
                    Components.end_dynamic_disable(prop_filter_duotone.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                // film
                tab_sub_filter_film.draw();
                if (tab_sub_filter_film.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_film = ShaderGUI.FindProperty("_LiteFilterFilm", properties);
                    prop_filter_film_amount = ShaderGUI.FindProperty("_LiteFilterFilmAmount", properties);
                    prop_filter_film.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_film_enable"), (enumToggle)prop_filter_film.floatValue);
                    Components.start_dynamic_disable(prop_filter_film.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_film_amount, languages.speak("prop_film_amount"));
                    Components.end_dynamic_disable(prop_filter_film.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                // grain
                tab_sub_filter_grain.draw();
                if (tab_sub_filter_grain.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_grain = ShaderGUI.FindProperty("_LiteFilterGrain", properties);
                    prop_filter_grain_amount = ShaderGUI.FindProperty("_LiteFilterGrainAmount", properties);
                    prop_filter_grain_color = ShaderGUI.FindProperty("_LiteFilterGrainColor", properties);
                    prop_filter_grain.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_grain_enable"), (enumToggle)prop_filter_grain.floatValue);
                    Components.start_dynamic_disable(prop_filter_grain.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_grain_amount, languages.speak("prop_grain_amount"));
                    materialEditor.ShaderProperty(prop_filter_grain_color, languages.speak("prop_grain_color"));
                    Components.end_dynamic_disable(prop_filter_grain.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                // vhs
                tab_sub_filter_vhs.draw();
                if (tab_sub_filter_vhs.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_vhs = ShaderGUI.FindProperty("_LiteFilterVHS", properties);
                    prop_filter_vhs_amount = ShaderGUI.FindProperty("_LiteFilterVHSAmount", properties);
                    prop_filter_vhs.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_vhs_enable"), (enumToggle)prop_filter_vhs.floatValue);
                    Components.start_dynamic_disable(prop_filter_vhs.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_vhs_amount, languages.speak("prop_vhs_amount"));
                    Components.end_dynamic_disable(prop_filter_vhs.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                // gradient
                tab_sub_filter_gradient.draw();
                if (tab_sub_filter_gradient.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_gradient = ShaderGUI.FindProperty("_LiteFilterGradient", properties);
                    prop_filter_gradient_lhs = ShaderGUI.FindProperty("_LiteFilterGradientLHS", properties);
                    prop_filter_gradient_rhs = ShaderGUI.FindProperty("_LiteFilterGradientRHS", properties);
                    prop_filter_gradient_transparency = ShaderGUI.FindProperty("_LiteFilterGradientTransparency", properties);
                    prop_filter_gradient.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_gradient_enable"), (enumToggle)prop_filter_gradient.floatValue);
                    Components.start_dynamic_disable(prop_filter_gradient.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_gradient_lhs, languages.speak("prop_gradient_lhs"));
                    materialEditor.ShaderProperty(prop_filter_gradient_rhs, languages.speak("prop_gradient_rhs"));
                    materialEditor.ShaderProperty(prop_filter_gradient_transparency, languages.speak("prop_gradient_transparency"));
                    Components.end_dynamic_disable(prop_filter_gradient.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                // outline
                tab_sub_filter_outline.draw();
                if (tab_sub_filter_outline.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_outline = ShaderGUI.FindProperty("_LiteFilterOutline", properties);
                    prop_filter_outline_width = ShaderGUI.FindProperty("_LiteFilterOutlineWidth", properties);
                    prop_filter_outline_tolerance = ShaderGUI.FindProperty("_LiteFilterOutlineTolerance", properties);
                    prop_filter_outline_color = ShaderGUI.FindProperty("_LiteFilterOutlineColor", properties);
                    prop_filter_outline.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_outline_enable"), (enumToggle)prop_filter_outline.floatValue);
                    Components.start_dynamic_disable(prop_filter_outline.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_outline_width, languages.speak("prop_outline_width"));
                    materialEditor.ShaderProperty(prop_filter_outline_tolerance, languages.speak("prop_outline_tolerance"));
                    materialEditor.ShaderProperty(prop_filter_outline_color, languages.speak("prop_outline_color"));
                    Components.end_dynamic_disable(prop_filter_outline.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                // astral
                tab_sub_filter_astral.draw();
                if (tab_sub_filter_astral.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_astral = ShaderGUI.FindProperty("_LiteFilterAstral", properties);
                    prop_filter_astral_zoom = ShaderGUI.FindProperty("_LiteFilterAstralZoom", properties);
                    prop_filter_astral_zoom_transparency = ShaderGUI.FindProperty("_LiteFilterAstralTransparency", properties);
                    prop_filter_astral_zoom_color = ShaderGUI.FindProperty("_LiteFilterAstralColor", properties);
                    prop_filter_astral.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_astral_enable"), (enumToggle)prop_filter_astral.floatValue);
                    Components.start_dynamic_disable(prop_filter_astral.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_astral_zoom, languages.speak("prop_astral_zoom"));
                    materialEditor.ShaderProperty(prop_filter_astral_zoom_transparency, languages.speak("prop_astral_transparency"));
                    materialEditor.ShaderProperty(prop_filter_astral_zoom_color, languages.speak("prop_astral_color"));
                    Components.end_dynamic_disable(prop_filter_astral.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                // neon
                tab_sub_filter_neon.draw();
                if (tab_sub_filter_neon.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_neon = ShaderGUI.FindProperty("_LiteFilterNeon", properties);
                    prop_filter_neon_width = ShaderGUI.FindProperty("_LiteFilterNeonWidth", properties);
                    prop_filter_neon_transparency = ShaderGUI.FindProperty("_LiteFilterNeonTransparency", properties);
                    prop_filter_neon_hue = ShaderGUI.FindProperty("_LiteFilterNeonHue", properties);
                    prop_filter_neon.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_neon_enable"), (enumToggle)prop_filter_neon.floatValue);
                    Components.start_dynamic_disable(prop_filter_neon.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_neon_width, languages.speak("prop_neon_width"));
                    materialEditor.ShaderProperty(prop_filter_neon_transparency, languages.speak("prop_neon_transparency"));
                    materialEditor.ShaderProperty(prop_filter_neon_hue, languages.speak("prop_neon_hue"));
                    Components.end_dynamic_disable(prop_filter_neon.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                // trippy
                tab_sub_filter_trippy.draw();
                if (tab_sub_filter_trippy.is_expanded)
                {
                    Components.start_foldout();
                    prop_filter_trippy = ShaderGUI.FindProperty("_LiteFilterTrippy", properties);
                    prop_filter_trippy_power = ShaderGUI.FindProperty("_LiteFilterTrippyPower", properties);
                    prop_filter_trippy_spread = ShaderGUI.FindProperty("_LiteFilterTrippySpread", properties);
                    prop_filter_trippy_speed = ShaderGUI.FindProperty("_LiteFilterTrippySpeed", properties);
                    prop_filter_trippy.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_trippy_enable"), (enumToggle)prop_filter_trippy.floatValue);
                    Components.start_dynamic_disable(prop_filter_trippy.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_filter_trippy_power, languages.speak("prop_trippy_power"));
                    materialEditor.ShaderProperty(prop_filter_trippy_spread, languages.speak("prop_trippy_spread"));
                    materialEditor.ShaderProperty(prop_filter_trippy_speed, languages.speak("prop_trippy_speed"));
                    Components.end_dynamic_disable(prop_filter_trippy.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                Components.end_foldout();
            }
            // fog tab
            tab_fog.draw();
            if (tab_fog.is_expanded)
            {
                Components.start_foldout();
                prop_fog_module = ShaderGUI.FindProperty("_LiteFogModule", properties);
                prop_fog_density = ShaderGUI.FindProperty("_LiteFogDensity", properties);
                prop_fog_distribution = ShaderGUI.FindProperty("_LiteFogDistribution", properties);
                prop_fog_color = ShaderGUI.FindProperty("_LiteFogColor", properties);
                prop_fog_safe_space = ShaderGUI.FindProperty("_LiteFogSafespace", properties);
                prop_fog_safe_space_size = ShaderGUI.FindProperty("_LiteFogSafespaceSize", properties);
                materialEditor.ShaderProperty(prop_fog_module, languages.speak("prop_fog_enable"));
                Components.start_dynamic_disable(prop_fog_module.floatValue == (float)enumToggle.Off);
                materialEditor.ShaderProperty(prop_fog_density, languages.speak("prop_fog_density"));
                materialEditor.ShaderProperty(prop_fog_distribution, languages.speak("prop_fog_distribution"));
                materialEditor.ShaderProperty(prop_fog_color, languages.speak("prop_fog_color"));
                prop_fog_safe_space.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_fog_safe_space"), (enumToggle)prop_fog_safe_space.floatValue);
                Components.start_dynamic_disable(prop_fog_safe_space.floatValue == (float)enumToggle.Off);
                materialEditor.ShaderProperty(prop_fog_safe_space_size, languages.speak("prop_fog_safe_space_size"));
                Components.end_dynamic_disable(prop_fog_safe_space.floatValue == (float)enumToggle.Off);
                EditorGUILayout.HelpBox(languages.speak("prop_ui_depth"), MessageType.Info);
                Components.end_dynamic_disable(prop_fog_module.floatValue == (float)enumToggle.Off);
                Components.end_foldout();
            }
            // glitch tab
            tab_glitch.draw();
            if (tab_glitch.is_expanded)
            {
                Components.start_foldout();
                prop_glitch_module = ShaderGUI.FindProperty("_LiteGlitchModule", properties);
                prop_glitch_scale = ShaderGUI.FindProperty("_LiteGlitchScale", properties);
                prop_glitch_amount = ShaderGUI.FindProperty("_LiteGlitchAmount", properties);
                prop_glitch_uvs = ShaderGUI.FindProperty("_LiteGlitchUVs", properties);
                prop_glitch_chromatic = ShaderGUI.FindProperty("_LiteGlitchChromatic", properties);
                materialEditor.ShaderProperty(prop_glitch_module, languages.speak("prop_glitch_enable"));
                Components.start_dynamic_disable(prop_glitch_module.floatValue == (float)enumToggle.Off);
                materialEditor.ShaderProperty(prop_glitch_scale, languages.speak("prop_glitch_scale"));
                materialEditor.ShaderProperty(prop_glitch_amount, languages.speak("prop_glitch_amount"));
                materialEditor.ShaderProperty(prop_glitch_uvs, languages.speak("prop_glitch_uvs"));
                materialEditor.ShaderProperty(prop_glitch_chromatic, languages.speak("prop_glitch_chromatic"));
                Components.end_dynamic_disable(prop_glitch_module.floatValue == (float)enumToggle.Off);
                Components.end_foldout();
            }
            // overlay tab
            tab_overlay.draw();
            if (tab_overlay.is_expanded)
            {
                Components.start_foldout();
                prop_overlay_module = ShaderGUI.FindProperty("_LiteOverlayModule", properties);
                prop_overlay_texture = ShaderGUI.FindProperty("_LiteOverlayTexture", properties);
                prop_overlay_transparency = ShaderGUI.FindProperty("_LiteOverlayTransparency", properties);
                prop_overlay_size_x = ShaderGUI.FindProperty("_LiteOverlaySizeX", properties);
                prop_overlay_size_y = ShaderGUI.FindProperty("_LiteOverlaySizeY", properties);
                prop_overlay_offset_x = ShaderGUI.FindProperty("_LiteOverlayOffsetX", properties);
                prop_overlay_offset_y = ShaderGUI.FindProperty("_LiteOverlayOffsetY", properties);
                prop_overlay_animated = ShaderGUI.FindProperty("_LiteOverlayAnimated", properties);
                prop_overlay_frames_x = ShaderGUI.FindProperty("_LiteOverlayFramesX", properties);
                prop_overlay_frames_y = ShaderGUI.FindProperty("_LiteOverlayFramesY", properties);
                prop_overlay_frames = ShaderGUI.FindProperty("_LiteOverlayFrames", properties); 
                prop_overlay_speed = ShaderGUI.FindProperty("_LiteOverlaySpeed", properties);
                prop_overlay_scrub = ShaderGUI.FindProperty("_LiteOverlayScrub", properties);
                prop_overlay_blendmode = ShaderGUI.FindProperty("_LiteOverlayBlendMode", properties);
                prop_overlay_vr = ShaderGUI.FindProperty("_LiteOverlayVR", properties);
                prop_overlay_vr_preview = ShaderGUI.FindProperty("_LiteOverlayVRPreview", properties);
                prop_overlay_vr_size_x = ShaderGUI.FindProperty("_LiteOverlayVRSizeX", properties);
                prop_overlay_vr_size_y = ShaderGUI.FindProperty("_LiteOverlayVRSizeY", properties);
                prop_overlay_vr_offset_x = ShaderGUI.FindProperty("_LiteOverlayVROffsetX", properties);
                prop_overlay_vr_offset_y = ShaderGUI.FindProperty("_LiteOverlayVROffsetY", properties);
                materialEditor.ShaderProperty(prop_overlay_module, languages.speak("prop_overlay_enable"));
                Components.start_dynamic_disable(prop_overlay_module.floatValue == (float)enumToggle.Off);
                materialEditor.ShaderProperty(prop_overlay_texture, languages.speak("prop_overlay_texture"));
                prop_overlay_blendmode.floatValue = (float)(enumBlend)EditorGUILayout.EnumPopup(languages.speak("prop_overlay_blendmode"), (enumBlend)prop_overlay_blendmode.floatValue);
                materialEditor.ShaderProperty(prop_overlay_transparency, languages.speak("prop_overlay_transparency"));
                materialEditor.ShaderProperty(prop_overlay_size_x, languages.speak("prop_overlay_size_x"));
                materialEditor.ShaderProperty(prop_overlay_size_y, languages.speak("prop_overlay_size_y"));
                materialEditor.ShaderProperty(prop_overlay_offset_x, languages.speak("prop_overlay_offset_x"));
                materialEditor.ShaderProperty(prop_overlay_offset_y, languages.speak("prop_overlay_offset_y"));
                prop_overlay_animated.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_overlay_animated"), (enumToggle)prop_overlay_animated.floatValue);
                if (prop_overlay_animated.floatValue == (float)enumToggle.On)
                {
                    materialEditor.ShaderProperty(prop_overlay_frames_x, languages.speak("prop_overlay_frames_x"));
                    materialEditor.ShaderProperty(prop_overlay_frames_y, languages.speak("prop_overlay_frames_y"));
                    materialEditor.ShaderProperty(prop_overlay_frames, languages.speak("prop_overlay_frames"));
                    materialEditor.ShaderProperty(prop_overlay_speed, languages.speak("prop_overlay_speed"));
                    materialEditor.ShaderProperty(prop_overlay_scrub, languages.speak("prop_overlay_scrub"));
                }
                Components.end_dynamic_disable(prop_overlay_module.floatValue == (float)enumToggle.Off);
                prop_overlay_vr.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_overlay_vr"), (enumToggle)prop_overlay_vr.floatValue);
                if (prop_overlay_vr.floatValue == (float)enumToggle.On) 
                {
                    prop_overlay_vr_preview.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_overlay_vr_preview"), (enumToggle)prop_overlay_vr_preview.floatValue);
                    materialEditor.ShaderProperty(prop_overlay_vr_size_x, languages.speak("prop_overlay_vr_size_x"));
                    materialEditor.ShaderProperty(prop_overlay_vr_size_y, languages.speak("prop_overlay_vr_size_y"));
                    materialEditor.ShaderProperty(prop_overlay_vr_offset_x, languages.speak("prop_overlay_vr_offset_x"));
                    materialEditor.ShaderProperty(prop_overlay_vr_offset_y, languages.speak("prop_overlay_vr_offset_y"));
                }
                Components.end_foldout();
            }
            // uv manipulation tab
            tab_uv_manipulation.draw();
            if (tab_uv_manipulation.is_expanded)
            {
                Components.start_foldout();
                prop_uv_manipulation_module = ShaderGUI.FindProperty("_LiteUVManipulationModule", properties);
                materialEditor.ShaderProperty(prop_uv_manipulation_module, languages.speak("prop_uv_manipulation_enable"));
                tab_sub_uv_transformation.draw();
                if (tab_sub_uv_transformation.is_expanded)
                {
                    Components.start_foldout();
                    prop_uv_manipulation_transformation_slant_tl = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantTL", properties);
                    prop_uv_manipulation_transformation_slant_tr = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantTR", properties);
                    prop_uv_manipulation_transformation_slant_bl = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantBL", properties);
                    prop_uv_manipulation_transformation_slant_br = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantBR", properties);
                    prop_uv_manipulation_transformation_flip_x = ShaderGUI.FindProperty("_LiteUVManipulationTransformationFlipX", properties);
                    prop_uv_manipulation_transformation_flip_y = ShaderGUI.FindProperty("_LiteUVManipulationTransformationFlipY", properties);
                    prop_uv_manipulation_transformation_stretch_x = ShaderGUI.FindProperty("_LiteUVManipulationTransformationStretchX", properties);
                    prop_uv_manipulation_transformation_stretch_y = ShaderGUI.FindProperty("_LiteUVManipulationTransformationStretchY", properties);
                    materialEditor.ShaderProperty(prop_uv_manipulation_transformation_slant_tl, languages.speak("prop_transformation_slant_top_left"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_transformation_slant_tr, languages.speak("prop_transformation_slant_top_right"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_transformation_slant_bl, languages.speak("prop_transformation_slant_bottom_left"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_transformation_slant_br, languages.speak("prop_transformation_slant_bottom_right"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_transformation_flip_x, languages.speak("prop_transformation_flip_x"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_transformation_flip_y, languages.speak("prop_transformation_flip_y"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_transformation_stretch_x, languages.speak("prop_transformation_stretch_x"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_transformation_stretch_y, languages.speak("prop_transformation_stretch_y"));
                    Components.end_foldout();
                }
                tab_sub_uv_move.draw();
                if (tab_sub_uv_move.is_expanded)
                {
                    Components.start_foldout();
                    prop_uv_manipulation_move_x = ShaderGUI.FindProperty("_LiteUVManipulationMoveX", properties);
                    prop_uv_manipulation_move_y = ShaderGUI.FindProperty("_LiteUVManipulationMoveY", properties);
                    materialEditor.ShaderProperty(prop_uv_manipulation_move_x, languages.speak("prop_movement_move_x"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_move_y, languages.speak("prop_movement_move_y"));
                    Components.end_foldout();
                }
                tab_sub_uv_shake.draw();
                if (tab_sub_uv_shake.is_expanded)
                {
                    Components.start_foldout();
                    prop_uv_manipulation_shake_style = ShaderGUI.FindProperty("_LiteUVManipulationShakeStyle", properties);
                    prop_uv_manipulation_shake_power_x = ShaderGUI.FindProperty("_LiteUVManipulationShakePowerX", properties);
                    prop_uv_manipulation_shake_power_y = ShaderGUI.FindProperty("_LiteUVManipulationShakePowerY", properties);
                    prop_uv_manipulation_shake_speed_x = ShaderGUI.FindProperty("_LiteUVManipulationShakeSpeedX", properties);
                    prop_uv_manipulation_shake_speed_y = ShaderGUI.FindProperty("_LiteUVManipulationShakeSpeedY", properties);
                    prop_uv_manipulation_shake_style.floatValue = (float)(enumShake)EditorGUILayout.EnumPopup(languages.speak("prop_shake_style"), (enumShake)prop_uv_manipulation_shake_style.floatValue);
                    Components.start_dynamic_disable(prop_uv_manipulation_shake_style.floatValue == (float)enumShake.Disabled);
                    materialEditor.ShaderProperty(prop_uv_manipulation_shake_power_x, languages.speak("prop_shake_power_x"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_shake_power_y, languages.speak("prop_shake_power_y"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_shake_speed_x, languages.speak("prop_shake_speed_x"));
                    materialEditor.ShaderProperty(prop_uv_manipulation_shake_speed_y, languages.speak("prop_shake_speed_y"));
                    Components.end_dynamic_disable(prop_uv_manipulation_shake_style.floatValue == (float)enumShake.Disabled);
                    Components.end_foldout();
                }
                tab_sub_uv_pixelation.draw();
                if (tab_sub_uv_pixelation.is_expanded)
                {
                    Components.start_foldout();
                    prop_uv_manipulation_pixelation = ShaderGUI.FindProperty("_LiteUVManipulationPixelation", properties);
                    prop_uv_manipulation_pixelation_power = ShaderGUI.FindProperty("_LiteUVManipulationPixelationPower", properties);
                    prop_uv_manipulation_pixelation.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_pixelation_enable"), (enumToggle)prop_uv_manipulation_pixelation.floatValue);
                    Components.start_dynamic_disable(prop_uv_manipulation_pixelation.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_uv_manipulation_pixelation_power, languages.speak("prop_pixelation_power"));
                    Components.end_dynamic_disable(prop_uv_manipulation_pixelation.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                tab_sub_uv_rotation.draw();
                if (tab_sub_uv_rotation.is_expanded)
                {
                    Components.start_foldout();
                    prop_uv_manipulation_rotation = ShaderGUI.FindProperty("_LiteUVManipulationRotation", properties);
                    prop_uv_manipulation_rotation_angle = ShaderGUI.FindProperty("_LiteUVManipulationRotationAngle", properties);
                    prop_uv_manipulation_rotation.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_rotation_enable"), (enumToggle)prop_uv_manipulation_rotation.floatValue);
                    Components.start_dynamic_disable(prop_uv_manipulation_rotation.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_uv_manipulation_rotation_angle, languages.speak("prop_rotation_angle"));
                    Components.end_dynamic_disable(prop_uv_manipulation_rotation.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                tab_sub_uv_spherize.draw();
                if (tab_sub_uv_spherize.is_expanded)
                {
                    Components.start_foldout();
                    prop_uv_manipulation_spherize = ShaderGUI.FindProperty("_LiteUVManipulationSpherize", properties);
                    prop_uv_manipulation_spherize_power = ShaderGUI.FindProperty("_LiteUVManipulationSpherizePower", properties);
                    prop_uv_manipulation_spherize.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_spherize_enable"), (enumToggle)prop_uv_manipulation_spherize.floatValue);
                    Components.start_dynamic_disable(prop_uv_manipulation_spherize.floatValue == (float)enumToggle.Off);
                    materialEditor.ShaderProperty(prop_uv_manipulation_spherize_power, languages.speak("prop_spherize_power"));
                    Components.end_dynamic_disable(prop_uv_manipulation_spherize.floatValue == (float)enumToggle.Off);
                    Components.end_foldout();
                }
                Components.end_foldout();
            }
            // zoom tab
            tab_zoom.draw();
            if (tab_zoom.is_expanded)
            {
                Components.start_foldout();
                prop_zoom_module = ShaderGUI.FindProperty("_LiteZoomModule", properties);
                prop_zoom_power = ShaderGUI.FindProperty("_LiteZoomPower", properties);
                prop_zoom_range_style = ShaderGUI.FindProperty("_LiteZoomRangeStyle", properties);
                prop_zoom_range_start = ShaderGUI.FindProperty("_LiteZoomRangeStart", properties);
                prop_zoom_range_end = ShaderGUI.FindProperty("_LiteZoomRangeEnd", properties);
                materialEditor.ShaderProperty(prop_zoom_module, languages.speak("prop_zoom_enable"));
                Components.start_dynamic_disable(prop_zoom_module.floatValue == (float)enumToggle.Off);
                materialEditor.ShaderProperty(prop_zoom_power, languages.speak("prop_zoom_power"));
                prop_zoom_range_style.floatValue = (float)(enumToggle)EditorGUILayout.EnumPopup(languages.speak("prop_zoom_custom_range"), (enumToggle)prop_zoom_range_style.floatValue);
                Components.start_dynamic_disable(prop_zoom_range_style.floatValue == (float)enumToggle.Off);
                materialEditor.ShaderProperty(prop_zoom_range_start, languages.speak("prop_zoom_range_start"));
                materialEditor.ShaderProperty(prop_zoom_range_end, languages.speak("prop_zoom_range_end"));
                Components.end_dynamic_disable(prop_zoom_range_style.floatValue == (float)enumToggle.Off);
                Components.end_dynamic_disable(prop_zoom_module.floatValue == (float)enumToggle.Off);
                Components.end_foldout();
            }
            config_menu.draw();
            // june pro preview tab
            tab_june_pro.draw();
            if (tab_june_pro.is_expanded) 
            {
                Components.start_foldout();
                enumToggle previewToggle = is_preview ? enumToggle.On : enumToggle.Off;
                previewToggle = (enumToggle)EditorGUILayout.EnumPopup(languages.speak("june_pro_preview_toggle"), previewToggle);
                bool newPreview = (previewToggle == enumToggle.On);
                if (newPreview != is_preview)
                {
                    if (newPreview)
                    {
                        preview_on(ref targetMat);
                    }
                    else
                    {
                        preview_off(ref targetMat);
                    }
                }
                is_preview = newPreview;
                // buy june pro button
                if (GUILayout.Button(languages.speak("june_pro_buy_button"), GUILayout.Height(30)))
                {
                    Application.OpenURL("https://luka.moe/go/junelitebuy");
                }
                // learn more about june pro button
                if (GUILayout.Button(languages.speak("june_pro_learn_button"), GUILayout.Height(30)))
                {
                    Application.OpenURL("https://luka.moe/go/junelitelearn");
                }
                // open the june pro docs
                if (GUILayout.Button(languages.speak("june_pro_docs_button"), GUILayout.Height(30)))
                {
                    Application.OpenURL("https://luka.moe/go/junelitedocs");
                }
                // june pro coupon
                EditorGUILayout.HelpBox(languages.speak("june_pro_coupon"), MessageType.Info);
                // june pro preview
                EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetRect(0, int.MaxValue, ((int)EditorGUIUtility.currentViewWidth / 3), 10), preview_texture, null, ScaleMode.ScaleToFit);
                Components.end_foldout();
            }
            // license tab
            license_tab.draw();
            if (license_tab.is_expanded)
            {
                Components.start_foldout();
                // a button that uses button_show_license and highlights the license asset in Unity
                if (GUILayout.Button(languages.speak("button_show_license"), GUILayout.Height(30)))
                {
                    string license_path = Project.project_path + "/Licenses/June Lite";
                    TextAsset license_asset = Resources.Load<TextAsset>(license_path);
                    if (license_asset != null)
                    {
                        EditorGUIUtility.PingObject(license_asset);
                    }
                }
                Components.end_foldout();
            }
            announcement.draw();
            docs.draw();
            socials_menu.draw();
            EditorGUI.EndChangeCheck();
        }

        // gui for the june pro preview
        public void OnGUIPreview(MaterialEditor materialEditor, MaterialProperty[] properties, ref Material targetMat)
        {
            preview_notice.draw();
            june_pro_tab_blur.draw();
            if (june_pro_tab_blur.is_expanded)
            {
                Components.start_foldout();
                EditorGUILayout.HelpBox(languages.speak("june_pro_styles_blur"), MessageType.Info);
                Components.end_foldout();
            }
            june_pro_tab_border.draw();
            if (june_pro_tab_border.is_expanded)
            {
                Components.start_foldout();
                EditorGUILayout.HelpBox(languages.speak("june_pro_styles_border"), MessageType.Info);
                Components.end_foldout();
            }
            june_pro_tab_chromatic.draw();
            if (june_pro_tab_chromatic.is_expanded)
            {
                Components.start_foldout();
                EditorGUILayout.HelpBox(languages.speak("june_pro_styles_chromatic"), MessageType.Info);
                Components.end_foldout();
            }
            june_pro_tab_colour_grading.draw();
            if (june_pro_tab_colour_grading.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_colour_grading_fine_tuning.draw();
                june_pro_sub_tab_colour_grading_colour_focus.draw();
                june_pro_sub_tab_colour_grading_colour_replacements.draw();
                june_pro_sub_tab_colour_grading_colourspace.draw();
                june_pro_sub_tab_colour_grading_lighting_adjustment.draw();
                june_pro_sub_tab_colour_grading_sharpness.draw();
                june_pro_sub_tab_colour_grading_saturation.draw();
                june_pro_sub_tab_colour_grading_stylize.draw();
                june_pro_sub_tab_colour_grading_colour_channels.draw();
                june_pro_sub_tab_colour_grading_post_processing.draw();
                june_pro_sub_tab_colour_grading_posterization.draw();
                Components.end_foldout();
            }
            june_pro_tab_creativity.draw();
            if (june_pro_tab_creativity.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_creativity_alphenglow.draw();
                june_pro_sub_tab_creativity_aquamarine.draw();
                june_pro_sub_tab_creativity_aurora.draw();
                june_pro_sub_tab_creativity_bonnibel.draw();
                june_pro_sub_tab_creativity_butterfly.draw();
                june_pro_sub_tab_creativity_candy.draw();
                june_pro_sub_tab_creativity_ecstasy.draw();
                june_pro_sub_tab_creativity_fable.draw();
                june_pro_sub_tab_creativity_lava_lamp.draw();
                june_pro_sub_tab_creativity_marceline.draw();
                june_pro_sub_tab_creativity_smokescreen.draw();
                june_pro_sub_tab_creativity_turbulence.draw();
                june_pro_sub_tab_creativity_rainbow_river.draw();
                june_pro_sub_tab_creativity_portal.draw();
                june_pro_sub_tab_creativity_tea.draw();
                june_pro_sub_tab_creativity_oil_spill.draw();
                june_pro_sub_tab_creativity_art.draw();
                Components.end_foldout();
            }
            june_pro_tab_distortion.draw();
            if (june_pro_tab_distortion.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_distortion_simple_distortion.draw();
                june_pro_sub_tab_distortion_bezier_curve.draw();
                june_pro_sub_tab_distortion_blackhole.draw();
                june_pro_sub_tab_distortion_bubbles.draw();
                june_pro_sub_tab_distortion_bumpy_glass.draw();
                june_pro_sub_tab_distortion_exaggeration.draw();
                june_pro_sub_tab_distortion_liquify.draw();
                june_pro_sub_tab_distortion_warp.draw();
                june_pro_sub_tab_distortion_wave.draw();
                Components.end_foldout();
            }
            june_pro_tab_enhancements.draw();
            if (june_pro_tab_enhancements.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_enhancements_anti_aliasing.draw();
                june_pro_sub_tab_enhancements_denoise.draw();
                june_pro_sub_tab_enhancements_deblur.draw();
                june_pro_sub_tab_enhancements_heavy_lines.draw();
                june_pro_sub_tab_enhancements_soft_lines.draw();
                june_pro_sub_tab_enhancements_upscale.draw();
                june_pro_sub_tab_enhancements_contrast_sharpening.draw();
                june_pro_sub_tab_enhancements_fdr.draw();
                june_pro_sub_tab_enhancements_screenspace_softshading.draw();
                Components.end_foldout();
            }
            june_pro_tab_experiments.draw();
            if (june_pro_tab_experiments.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_experiments_dolly.draw();
                june_pro_sub_tab_experiments_cloning.draw();
                june_pro_sub_tab_experiments_screen_background.draw();
                june_pro_sub_tab_experiments_depth_viewer.draw();
                june_pro_sub_tab_experiments_objectify.draw();
                Components.end_foldout();
            }
            june_pro_tab_filters.draw();
            if (june_pro_tab_filters.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_filters_colour_incorrection.draw();
                june_pro_sub_tab_filters_colourblind_simulation.draw();
                june_pro_sub_tab_filters_corners.draw();
                june_pro_sub_tab_filters_colour_crush.draw();
                june_pro_sub_tab_filters_colour_cycline.draw();
                june_pro_sub_tab_filters_colour_wheel.draw();
                june_pro_sub_tab_filters_crt.draw();
                june_pro_sub_tab_filters_monotone.draw();
                june_pro_sub_tab_filters_duotone.draw();
                june_pro_sub_tab_filters_tritone.draw();
                june_pro_sub_tab_filters_engraving.draw();
                june_pro_sub_tab_filters_linocut.draw();
                june_pro_sub_tab_filters_light_leak.draw();
                june_pro_sub_tab_filters_film.draw();
                june_pro_sub_tab_filters_normal_mapper.draw();
                june_pro_sub_tab_filters_chrome.draw();
                june_pro_sub_tab_filters_rainbow.draw();
                june_pro_sub_tab_filters_ramp.draw();
                june_pro_sub_tab_filters_gradient.draw();
                june_pro_sub_tab_filters_low_ink.draw();
                june_pro_sub_tab_filters_low_bitrate.draw();
                june_pro_sub_tab_filters_grain.draw();
                june_pro_sub_tab_filters_glitter.draw();
                june_pro_sub_tab_filters_moire.draw();
                june_pro_sub_tab_filters_sepia.draw();
                june_pro_sub_tab_filters_solarize.draw();
                june_pro_sub_tab_filters_specular.draw();
                june_pro_sub_tab_filters_tie_dye.draw();
                june_pro_sub_tab_filters_technicolour.draw();
                june_pro_sub_tab_filters_thermal.draw();
                june_pro_sub_tab_filters_threshold.draw();
                june_pro_sub_tab_filters_night_vision.draw();
                june_pro_sub_tab_filters_ultra_violet.draw();
                june_pro_sub_tab_filters_wall_glow.draw();
                june_pro_sub_tab_filters_vhs.draw();
                june_pro_sub_tab_filters_vignette.draw();
                june_pro_sub_tab_filters_dither.draw();
                june_pro_sub_tab_filters_fauxlate.draw();
                june_pro_sub_tab_filters_lieless.draw();
                Components.end_foldout();
            }
            june_pro_tab_frames.draw();
            if (june_pro_tab_frames.is_expanded)
            {
                Components.start_foldout();
                EditorGUILayout.HelpBox(languages.speak("june_pro_styles_frames"), MessageType.Info);
                Components.end_foldout();
            }
            june_pro_tab_generation.draw();
            if (june_pro_tab_generation.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_generation_lines.draw();
                june_pro_sub_tab_generation_ring_colors.draw();
                june_pro_sub_tab_generation_noise_colors.draw();
                june_pro_sub_tab_generation_sdf_colors.draw();
                june_pro_sub_tab_generation_shapes_colors.draw();
                june_pro_sub_tab_generation_shapes_uvs.draw();
                june_pro_sub_tab_generation_spiral_colors.draw();
                june_pro_sub_tab_generation_spiral_uvs.draw();
                june_pro_sub_tab_generation_hearts.draw();
                Components.end_foldout();
            }
            june_pro_tab_glitch.draw();
            if (june_pro_tab_glitch.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_glitch_simple.draw();
                june_pro_sub_tab_glitch_advanced.draw();
                Components.end_foldout();
            }
            june_pro_tab_motion.draw();
            if (june_pro_tab_motion.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_motion_acid.draw();
                june_pro_sub_tab_motion_blur.draw();
                june_pro_sub_tab_motion_distortion.draw();
                june_pro_sub_tab_motion_freeze.draw();
                june_pro_sub_tab_motion_rgb_freeze.draw();
                june_pro_sub_tab_motion_glitch.draw();
                june_pro_sub_tab_motion_chromatic.draw();
                june_pro_sub_tab_motion_pixel_sort.draw();
                june_pro_sub_tab_motion_trail.draw();
                june_pro_sub_tab_motion_tranquility.draw();
                june_pro_sub_tab_motion_tranceless.draw();
                june_pro_sub_tab_motion_data_mosh.draw();
                june_pro_sub_tab_motion_frame_rate.draw();
                june_pro_sub_tab_motion_fensterxd.draw();
                june_pro_sub_tab_motion_fading_projections.draw();
                june_pro_sub_tab_motion_motear.draw();
                june_pro_sub_tab_motion_lake_fill.draw();
                Components.end_foldout();
            }
            june_pro_tab_others.draw();
            if (june_pro_tab_others.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_others_astral.draw();
                june_pro_sub_tab_others_astral_rgb.draw();
                june_pro_sub_tab_others_apart.draw();
                june_pro_sub_tab_others_colour_diffusion.draw();
                june_pro_sub_tab_others_holepuncher.draw();
                june_pro_sub_tab_others_glowstick.draw();
                june_pro_sub_tab_others_grid_checkerboard.draw();
                june_pro_sub_tab_others_hallucinogen.draw();
                june_pro_sub_tab_others_lenticular_halo.draw();
                june_pro_sub_tab_others_meta_image.draw();
                june_pro_sub_tab_others_palette.draw();
                june_pro_sub_tab_others_rain_line.draw();
                june_pro_sub_tab_others_rim.draw();
                june_pro_sub_tab_others_scanline_overlay.draw();
                june_pro_sub_tab_others_stripes.draw();
                june_pro_sub_tab_others_sunbeams.draw();
                june_pro_sub_tab_others_water_reflection.draw();
                june_pro_sub_tab_others_camouflage.draw();
                june_pro_sub_tab_others_inception.draw();
                june_pro_sub_tab_others_object_detection.draw();
                june_pro_sub_tab_others_fog.draw();
                june_pro_sub_tab_others_silhouette.draw();
                june_pro_sub_tab_others_prismatic_layers.draw();
                june_pro_sub_tab_others_hexatile.draw();
                june_pro_sub_tab_others_secrets.draw();
                june_pro_sub_tab_others_divider.draw();
                Components.end_foldout();
            }
            june_pro_tab_outline.draw();
            if (june_pro_tab_outline.is_expanded)
            {
                Components.start_foldout();
                EditorGUILayout.HelpBox(languages.speak("june_pro_styles_outline"), MessageType.Info);
                Components.end_foldout();
            }
            june_pro_tab_overlay.draw();
            if (june_pro_tab_overlay.is_expanded)
            {
                Components.start_foldout();
                EditorGUILayout.HelpBox(languages.speak("june_pro_styles_overlay"), MessageType.Info);
                Components.end_foldout();
            }
            june_pro_tab_stylize.draw();
            if (june_pro_tab_stylize.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_stylize_compression.draw();
                june_pro_sub_tab_stylize_crosshatching.draw();
                june_pro_sub_tab_stylize_crystalize.draw();
                june_pro_sub_tab_stylize_dots.draw();
                june_pro_sub_tab_stylize_emboss.draw();
                june_pro_sub_tab_stylize_impressionism.draw();
                june_pro_sub_tab_stylize_mosaic.draw();
                june_pro_sub_tab_stylize_neon_rings.draw();
                june_pro_sub_tab_stylize_oil.draw();
                june_pro_sub_tab_stylize_monitor.draw();
                june_pro_sub_tab_stylize_neon.draw();
                june_pro_sub_tab_stylize_pop_art.draw();
                june_pro_sub_tab_stylize_unicode.draw();
                june_pro_sub_tab_stylize_halftone.draw();
                june_pro_sub_tab_stylize_halftone_circles.draw();
                june_pro_sub_tab_stylize_halftone_spiral.draw();
                june_pro_sub_tab_stylize_halftone_rgb.draw();
                Components.end_foldout();
            }
            june_pro_tab_special.draw();
            if (june_pro_tab_special.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_special_bubbles.draw();
                june_pro_sub_tab_special_confetti.draw();
                june_pro_sub_tab_special_data_stream.draw();
                june_pro_sub_tab_special_lens_flare.draw();
                june_pro_sub_tab_special_hexagonal_shield.draw();
                june_pro_sub_tab_special_lightning.draw();
                june_pro_sub_tab_special_mapping.draw();
                june_pro_sub_tab_special_rain_drops.draw();
                june_pro_sub_tab_special_plexus.draw();
                june_pro_sub_tab_special_shanshuo.draw();
                june_pro_sub_tab_special_star_trail.draw();
                june_pro_sub_tab_special_spotlights.draw();
                june_pro_sub_tab_special_visualizer.draw();
                june_pro_sub_tab_special_warp_drive.draw();
                Components.end_foldout();
            }
            june_pro_tab_transition.draw();
            if (june_pro_tab_transition.is_expanded)
            {
                Components.start_foldout();
                EditorGUILayout.HelpBox(languages.speak("june_pro_styles_transition"), MessageType.Info);
                Components.end_foldout();
            }
            june_pro_tab_triplanar.draw();
            if (june_pro_tab_triplanar.is_expanded)
            {
                Components.start_foldout();
                EditorGUILayout.HelpBox(languages.speak("june_pro_styles_triplanar"), MessageType.Info);
                Components.end_foldout();
            }
            june_pro_tab_uv_manipulation.draw();
            if (june_pro_tab_uv_manipulation.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_uv_manipulation_bender.draw();
                june_pro_sub_tab_uv_manipulation_movement.draw();
                june_pro_sub_tab_uv_manipulation_clamp.draw();
                june_pro_sub_tab_uv_manipulation_coordinates.draw();
                june_pro_sub_tab_uv_manipulation_dither.draw();
                june_pro_sub_tab_uv_manipulation_kaleidoscope.draw();
                june_pro_sub_tab_uv_manipulation_mirror.draw();
                june_pro_sub_tab_uv_manipulation_scroll.draw();
                june_pro_sub_tab_uv_manipulation_shake_and_earthquake.draw();
                june_pro_sub_tab_uv_manipulation_slicer.draw();
                june_pro_sub_tab_uv_manipulation_melt.draw();
                june_pro_sub_tab_uv_manipulation_mirror_shatter.draw();
                june_pro_sub_tab_uv_manipulation_ring_rotation.draw();
                june_pro_sub_tab_uv_manipulation_refraction.draw();
                june_pro_sub_tab_uv_manipulation_pixelation.draw();
                june_pro_sub_tab_uv_manipulation_pixel_shifter.draw();
                june_pro_sub_tab_uv_manipulation_scanline.draw();
                june_pro_sub_tab_uv_manipulation_shuffle.draw();
                june_pro_sub_tab_uv_manipulation_skew.draw();
                june_pro_sub_tab_uv_manipulation_spherize.draw();
                june_pro_sub_tab_uv_manipulation_transformation.draw();
                june_pro_sub_tab_uv_manipulation_twisted_corridor.draw();
                june_pro_sub_tab_uv_manipulation_recursion.draw();
                june_pro_sub_tab_uv_manipulation_twod_rotation.draw();
                june_pro_sub_tab_uv_manipulation_threed_rotation.draw();
                june_pro_sub_tab_uv_manipulation_threed_pan.draw();
                june_pro_sub_tab_uv_manipulation_onlyscreens.draw();
                june_pro_sub_tab_uv_manipulation_spinterception.draw();
                june_pro_sub_tab_uv_manipulation_quadrant_zoom.draw();
                june_pro_sub_tab_uv_manipulation_swivel.draw();
                june_pro_sub_tab_uv_manipulation_distaer.draw();
                june_pro_sub_tab_uv_manipulation_thanos.draw();
                Components.end_foldout();
            }
            june_pro_tab_vertex_reconstruction.draw();
            if (june_pro_tab_vertex_reconstruction.is_expanded)
            {
                Components.start_foldout();
                june_pro_sub_tab_vertex_reconstruction_atmosphere.draw();
                june_pro_sub_tab_vertex_reconstruction_glitter.draw();
                june_pro_sub_tab_vertex_reconstruction_shatterwave.draw();
                june_pro_sub_tab_vertex_reconstruction_threed_lighting.draw();
                june_pro_sub_tab_vertex_reconstruction_wireframe.draw();
                june_pro_sub_tab_vertex_reconstruction_wireframe_shatterwave.draw();
                june_pro_sub_tab_vertex_reconstruction_normals.draw();
                june_pro_sub_tab_vertex_reconstruction_tripful.draw();
                june_pro_sub_tab_vertex_reconstruction_hololens.draw();
                june_pro_sub_tab_vertex_reconstruction_lidar.draw();
                june_pro_sub_tab_vertex_reconstruction_corruption.draw();
                june_pro_sub_tab_vertex_reconstruction_world_wrap.draw();
                june_pro_sub_tab_vertex_reconstruction_spotlight.draw();
                june_pro_sub_tab_vertex_reconstruction_tryptamines.draw();
                Components.end_foldout();
            }
            june_pro_tab_zoom.draw();
            if (june_pro_tab_zoom.is_expanded)
            {
                Components.start_foldout();
                EditorGUILayout.HelpBox(languages.speak("june_pro_styles_zoom"), MessageType.Info);
                Components.end_foldout();
            }
            config_menu.draw();
            // june pro preview tab
            tab_june_pro.draw();
            if (tab_june_pro.is_expanded) 
            {
                Components.start_foldout();
                enumToggle previewToggle = is_preview ? enumToggle.On : enumToggle.Off;
                previewToggle = (enumToggle)EditorGUILayout.EnumPopup(languages.speak("june_pro_preview_toggle"), previewToggle);
                bool newPreview = (previewToggle == enumToggle.On);
                if (newPreview != is_preview)
                {
                    if (newPreview)
                    {
                        preview_on(ref targetMat);
                    }
                    else
                    {
                        preview_off(ref targetMat);
                    }
                }
                is_preview = newPreview;
                // buy june pro button
                if (GUILayout.Button(languages.speak("june_pro_buy_button"), GUILayout.Height(30)))
                {
                    Application.OpenURL("https://luka.moe/go/junelitebuy");
                }
                // learn more about june pro button
                if (GUILayout.Button(languages.speak("june_pro_learn_button"), GUILayout.Height(30)))
                {
                    Application.OpenURL("https://luka.moe/go/junelitelearn");
                }
                // open the june pro docs
                if (GUILayout.Button(languages.speak("june_pro_docs_button"), GUILayout.Height(30)))
                {
                    Application.OpenURL("https://luka.moe/go/junelitedocs");
                }
                // june pro coupon
                EditorGUILayout.HelpBox(languages.speak("june_pro_coupon"), MessageType.Info);
                // june pro preview
                EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetRect(0, int.MaxValue, ((int)EditorGUIUtility.currentViewWidth / 3), 10), preview_texture, null, ScaleMode.ScaleToFit);
                Components.end_foldout();
            }
            announcement.draw();
            docs.draw();
            socials_menu.draw();
            EditorGUI.EndChangeCheck();
        }

    }

}
#endif // UNITY_EDITOR