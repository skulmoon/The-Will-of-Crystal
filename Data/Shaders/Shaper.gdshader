shader_type canvas_item;
uniform float x_multiper = 1;
uniform float y_multiper = 1;
instance uniform float rotation = 1.57;

void vertex() {
    VERTEX -= vec2(0.5);

    float c = cos(rotation);
    float s = sin(rotation);
    mat2 rot = mat2(vec2(c, -s), vec2(s, c));

    // поворачиваем
    VERTEX = rot * VERTEX;

    // сплющиваем по X
    VERTEX.y *= y_multiper;

    // обратный поворот
    VERTEX = inverse(rot) * VERTEX;

    // возвращаем обратно
    VERTEX += vec2(0.5);

}

void fragment() {
	COLOR = texture(TEXTURE, UV);
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
