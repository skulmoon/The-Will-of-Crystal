shader_type canvas_item;

uniform sampler2D noise_texture : hint_default_white, filter_nearest;
uniform float dark_power = 0.6;
uniform float dark_scale = 2.0;
uniform float noise_scale = 0.0;
uniform vec2 cursor_position = vec2(0, 0);

void fragment() {
	vec4 original_color = texture(noise_texture, UV);
	vec2 uv = (UV + cursor_position - 0.5) * 2.0;
	float dist = length(uv);
	COLOR.q = (dist - 1.0 + dark_power * dark_scale + original_color.r * noise_scale);
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
