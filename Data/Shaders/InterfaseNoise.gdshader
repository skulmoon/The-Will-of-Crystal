shader_type canvas_item;

uniform sampler2D noise_texture : hint_default_white, filter_nearest;
uniform vec4 color1 : source_color;
uniform vec4 color2 : source_color;
uniform vec4 color3 : source_color;
uniform vec4 color4 : source_color;

void fragment() {
	vec4 original_color = texture(noise_texture, UV);
	if (COLOR.g == 1.0) {
		float a = COLOR.a;
		if (original_color.r < 0.5) {
			COLOR = color1;
		}
		else if (original_color.r < 0.66) {
			COLOR = color2;
		}
		else if (original_color.r < 0.82) {
			COLOR = color3;
		}
		else {
			COLOR = color4;
		}
		COLOR.a = a;
	}
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
