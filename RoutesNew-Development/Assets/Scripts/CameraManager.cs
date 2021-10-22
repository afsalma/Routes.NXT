using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeCameraNamespace;
using Firebase.Storage;
using System.IO;
using System;

public class CameraManager : MonoBehaviour
{
	[SerializeField] GameObject UploadDelayPanel;
	[SerializeField] bool uploaded;
	public static CameraManager manager;
	string imagePath;
	FirebaseStorage storage;
	StorageReference storage_ref;
	Texture2D imageTexture;
	//public Texture2D newTexture;
	String documentName;
	//WebCamTexture webCamTexture;
	private void Awake()
    {
		if (manager == null)
		{
			manager = this;
		}
	
	}
	byte[] _bytes;

	public void TakePicture(string docName)
	{
		documentName = docName;
		int maxSize=1;
		NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				imagePath = path;
				// Create a Texture2D from the captured image
			//	imageTexture = new Texture2D(545, 545);
			//	imageTexture.
			    imageTexture = NativeCamera.LoadImageAtPath(path, maxSize);
				
				if (imageTexture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}

				 _bytes = File.ReadAllBytes(path);
				if(_bytes==null)
				{
					Debug.Log("Image not read from " + path);
					return;
				}

				StoreImage();
				/*
				var duplicate = duplicateTexture(imageTexture);
				byte[] itemBGBytes = duplicate.EncodeToPNG();
				File.WriteAllBytes(Application.persistentDataPath + "/Background.png", itemBGBytes);
				StoreImage();
				/*
				// Assign texture to a temporary quad and destroy it after 5 seconds
				GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
				quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
				quad.transform.forward = Camera.main.transform.forward;
				quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

				Material material = quad.GetComponent<Renderer>().material;
				if (!material.shader.isSupported) // happens when Standard shader is not included in the build
					material.shader = Shader.Find("Legacy Shaders/Diffuse");

				material.mainTexture = texture;

				Destroy(quad, 5f);
				*/
				// If a procedural texture is not destroyed manually, 
				// it will only be freed after a scene change
				//	Destroy(texture, 5f);
			}
		}, maxSize);
	
		/*
		if (imageTexture != null)
		{

			///	Sprite itemBGSprite = Resources.Load<Sprite>("_Defaults/Item Images/_Background");
			//	Texture2D itemBGTex = itemBGSprite.texture;
			/*
			Texture2D texture = new Texture2D(Screen.width, Screen.height);
			texture.filterMode = FilterMode.Point;
			texture.wrapMode = TextureWrapMode.Clamp;
			texture.SetPixels(imageTexture);
			texture.Apply();
			
			var duplicate = duplicateTexture(imageTexture);
			byte[] itemBGBytes = duplicate.EncodeToPNG();
			File.WriteAllBytes(Application.persistentDataPath + "/Background.png", itemBGBytes);
			StoreImage();
		}
	*/
		//	return imagePath;
	}

	string local_file;
	public void StoreImage()
	{
		UploadDelayPanel.SetActive(true);
		local_file = Application.persistentDataPath + "/Background.png";
		//byte[] _bytes = imageTexture.EncodeToPNG();
		File.WriteAllBytes(local_file, _bytes);
		Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + local_file);

		//	local_file= Uri.
		StartCoroutine(DelayGenerator());
		
	}


	IEnumerator DelayGenerator()
	{
		yield return new WaitForSeconds(2f);
		Debug.Log("Storeimage called");
		storage = FirebaseStorage.DefaultInstance;
		storage_ref = storage.GetReferenceFromUrl("gs://unityroutes.appspot.com/");

		// File located on disk

		//string local_file =  imagePath;
		// Create a reference to the file you want to upload
		StorageReference image_ref = storage_ref.Child(DataHandlerCustom.dataHandler.date).Child("username").Child(documentName+".jpeg");
		StreamReader stream = new StreamReader(local_file);
		//	Stream stream = new FileStream(local_file, FileMode.Open);

		// Upload the file to the path
		
		  object p =  image_ref.PutStreamAsync(stream.BaseStream).ContinueWith((task) =>
		{
			if (task.IsFaulted || task.IsCanceled)
			{
				Debug.Log(task.Exception.ToString());

			}
			else
			{
				// Metadata contains file metadata such as size, content-type, and download URL.
				StorageMetadata metadata = task.Result;
				string md5Hash = metadata.Md5Hash;
				Debug.Log("Finished uploading...");
				uploaded = true;
				Debug.Log("md5 hash = " + md5Hash);
				stream.Close();
				
			}
			
		});

		yield return new WaitForSeconds(4f);
	//	if(uploaded)
		UploadDelayPanel.SetActive(false);
		



	}
	Texture2D duplicateTexture(Texture2D source)
	{
		RenderTexture renderTex = RenderTexture.GetTemporary(
					source.width,
					source.height,
					0,
					RenderTextureFormat.Default,
					RenderTextureReadWrite.Linear);

		Graphics.Blit(source, renderTex);
		RenderTexture previous = RenderTexture.active;
		RenderTexture.active = renderTex;
		Texture2D readableText = new Texture2D(source.width, source.height);
		readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
		readableText.Apply();
		RenderTexture.active = previous;
		RenderTexture.ReleaseTemporary(renderTex);
		return readableText;
	}

}

