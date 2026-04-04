shader_type canvas_item;

uniform sampler2D noise_texture : hint_default_white, filter_nearest;
uniform vec2 mask_offset = vec2(0.0);
instance uniform vec2 node_rect_size = vec2(100.0, 100.0);

varying vec2 v_node_uv;

void vertex() {
	v_node_uv = VERTEX.xy / node_rect_size;
}

void fragment() {
	vec4 base_color = COLOR;
	float mask_value;
    mask_value = texture(noise_texture, v_node_uv + mask_offset).r;
	if (COLOR.r == 1.0)
	{
		if (mask_value < 0.5) {
			mask_value = 0.5;
		}
		else if (mask_value < 0.66) {
			mask_value = 0.66;
		}
		else if (mask_value < 0.82) {
			mask_value = 0.82;
		}
		else {
			mask_value = 1.0;
		}
    	COLOR = vec4(base_color.rgb, base_color.a * mask_value);
	}
}