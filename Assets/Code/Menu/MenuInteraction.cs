using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInteraction : MonoBehaviour
{
    [Header("Configurações de Raycast")]
    public Camera mainCamera; 
    public float rayDistance = 100f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                Debug.Log("Clicou em: " + hit.collider.name);

                switch (hit.collider.name)
                {
                    case "PlayButton":
                        SceneManager.LoadScene("SampleScene");
                        break;

                    case "ConfigButton":
                        SceneManager.LoadScene("Configuracoes");
                        break;

                    case "ExitButton":
                        Application.Quit();
                        Debug.Log("Saiu do jogo (no editor não fecha)");
                        break;

                    default:
                        Debug.Log("Objeto clicado não é um botão registrado.");
                        break;
                }
            }
        }
    }
}
