/*
*   @author Kyuzen
*/

using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// File class.
/// </summary>
public class FileClass : MonoBehaviour{

	/// <summary>
	/// Save the specified prefKey and serializableObject.
	/// </summary>
	/// <param name="prefKey">Preference key.</param>
	/// <param name="serializableObject">Serializable object.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static bool Save<T>( string prefKey, T serializableObject ){
		MemoryStream memoryStream = new MemoryStream();
#if UNITY_IPHONE || UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
#endif
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize( memoryStream, serializableObject );
		
		string tmp = System.Convert.ToBase64String( memoryStream.ToArray() );
		try {
			PlayerPrefs.SetString ( prefKey, tmp );
		} catch( PlayerPrefsException ) {
			return false;
		}
		return true;
	}

	/// <summary>
	/// Load the specified prefKey.
	/// </summary>
	/// <param name="prefKey">Preference key.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T Load<T>( string prefKey ){
		if (!PlayerPrefs.HasKey(prefKey)) return default(T);
#if UNITY_IPHONE || UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
#endif
		BinaryFormatter bf = new BinaryFormatter();
		string serializedData = PlayerPrefs.GetString( prefKey );
		
		MemoryStream dataStream = new MemoryStream( System.Convert.FromBase64String(serializedData) );
		T deserializedObject = (T)bf.Deserialize( dataStream );
		
		return deserializedObject;
	}

}