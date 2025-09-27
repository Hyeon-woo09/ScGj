using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Scenes.Tile
{
    public class GameStart : MonoBehaviour
    {
        
        public void LoadScene()
        {
            SceneManager.LoadScene("MainGame"); 
        }
        
    }
}
