using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public Text countText;
	public Text winText;

    private Rigidbody rb;
	private int count;
    private int count1;
    public GameObject prefab;
    public SceneFader fader;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText();
		winText.text = "";
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText();
            GameObject[] getCount = GameObject.FindGameObjectsWithTag("Pick Up");
            count1 = getCount.Length;
        }
        if (count1 < 2 && count < 10)
        {
            Invoke("SpawnObject", 0.0f);
        }
    }

    void SpawnObject()
    {
        Vector3 position = new Vector3(Random.Range(-9.0f, 9.0f), 1.0f, Random.Range(-10.0f, 9.0f));
        GameObject prefabClone = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        prefabClone.tag = "Pick Up";
    }

    void SetCountText()
	{
		countText.text = "Score: " + count.ToString();

		if (count >= 10)
        {
			winText.text = "You Win!";
            Scene sceneLoaded = SceneManager.GetActiveScene();
            PlayerPrefs.SetInt("levelReached", sceneLoaded.buildIndex + 1);
            Invoke("LevelUp", 4.0f);
        }
	}

    void LevelUp()
    {
        fader.FadeTo("LevelSelector");
        winText.text = "";
        countText.text = "";
    }

}
