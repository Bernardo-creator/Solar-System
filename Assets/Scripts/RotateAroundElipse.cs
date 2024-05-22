using UnityEngine;

public class RotateAroundEllipse : MonoBehaviour {

    [Tooltip("This is the object that the script's game object will rotate around")]
    public Transform target; // o objeto que o script vai rotacionar em volta

    [Tooltip("Rotation speed in degrees per second")]
    public float speed = 30f; // Velocidade de rotação

    [Tooltip("Excentricidade que vai de 0(circulo) a 1 para ficar mais alongada")]
    public float excentricidade = 0.5f; 

    private Vector3 center; 
    private float A; // Semi eixo maior
    private float B; // Semi eixo menor

    void Start() {
        if (target == null) {
            target = this.gameObject.transform;
            Debug.Log("RotateAroundEllipse target not specified. Defaulting to this GameObject");
        }

        center = target.position;

        // Calcular a distância do semi-eixo maior com base na distância entre o objeto e o centro
        A = Vector3.Distance(transform.position, center);

        // Calcular o semi-eixo menor com base na excentricidade
        B = A * Mathf.Sqrt(1 - excentricidade * excentricidade);
    }

    void Update() {
        // Calcular o ângulo com base no tempo
        center = target.position;
        float angle = Time.time * speed / 10;

        // Equação paramétrica da elipse para calcular a trajetória
        float newX = center.x + A * Mathf.Cos(angle);
        float newZ = center.z + B * Mathf.Sin(angle);

        transform.position = new Vector3(newX, transform.position.y, newZ);

        transform.LookAt(center);
    }
}