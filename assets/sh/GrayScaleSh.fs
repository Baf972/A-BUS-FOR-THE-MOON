// Grayscale fragment shader
#ifdef GL_ES
precision mediump float;
#endif

varying vec2 fragTexCoord;
uniform sampler2D texture0;

void main()
{
    vec4 color = texture2D(texture0, fragTexCoord);
    float gray = dot(color.rgb, vec3(0.299, 0.587, 0.114)); // Formule standard pour le niveau de gris
    gl_FragColor = vec4(vec3(gray), color.a);
}