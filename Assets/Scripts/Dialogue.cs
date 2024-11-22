using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] speechText;
    public string actorName;

    private DialogueScript dc;
    private bool onRadius;
    private bool isDialogueActive = false;

    public LayerMask playerLayer;
    public float radius;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dc = FindObjectOfType<DialogueScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && onRadius && !isDialogueActive)
        {
            StartDialogue();
        }
    }

    private void FixedUpdate()
    {
        Interact();
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        dc.Speech(speechText, actorName);
        Debug.Log("Diálogo iniciado com " + actorName);
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        Debug.Log("Diálogo com " + actorName + " encerrado.");
    }

    public void Interact()
    {
        Vector3 point1 = transform.position + Vector3.up * radius;
        Vector3 point2 = transform.position - Vector3.up * radius;

        Collider[] hits = Physics.OverlapCapsule(point1, point2, radius);

        if (hits.Length > 0)
        {
            if (!onRadius)
            {
                Debug.Log("Jogador entrou na interação.");
            }
            onRadius = true;
        }
        else
        {
            if (isDialogueActive)
            {
                Debug.Log("Jogador saiu da interação.");
            }
            onRadius = false;
        }
    }
}
