using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvasController : MonoBehaviour
{
   [SerializeField] private Button cancelButton;
   [SerializeField] private Animator animator;
   private NetworkRunnerController networkRunnerController;

   private void Start()
   {
      networkRunnerController = GlobalManagers.Instance.networkRunnerController;
      networkRunnerController.OnStartedRunnerController += OnStartedRunnerConnection;
      networkRunnerController.OnPlayerJoinedSucessfully += OnPlayerJoinedSucessfully;
      
      cancelButton.onClick.AddListener(networkRunnerController.ShutDownRunner);
       
      this.gameObject.SetActive(false);
   }
   
   private void OnStartedRunnerConnection()
   {
      this.gameObject.SetActive(true);
      const string CLIP_NAME = "In";
      StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, animator, CLIP_NAME));
   }

   private void OnPlayerJoinedSucessfully()
   {
      const string CLIP_NAME = "Out";
      StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, animator, CLIP_NAME, false));
   }
   
   private void OnDestroy()
   {
      networkRunnerController.OnStartedRunnerController -= OnStartedRunnerConnection;
      networkRunnerController.OnPlayerJoinedSucessfully -= OnPlayerJoinedSucessfully;
   }
}
