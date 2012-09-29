using System;
using System.IO;
using System.Xml.Serialization;

public sealed class ResourceManager
{
	#region Singletone
	private static ResourceManager _instance;
	
	public static ResourceManager Instance {
		get {
			if (_instance == null) {
				_instance = new ResourceManager ();
			}
			
			return _instance;
		}
			
	}

	private ResourceManager ()
	{
	}
	#endregion
	
	private string storagePath;
	
	void Awake ()
	{
		_instance = this;
	}
	
	public void SetStoragePath (string storagePath)
	{
		this.storagePath = storagePath;
	}
	
	public object Load (string fileName, Type type)
	{
		object retObj = null;
		var serializer = new XmlSerializer (type);
		FileStream stream;
		
		if (!File.Exists (storagePath + fileName)) {
			Save (fileName, Activator.CreateInstance (type), type);
		}
		
		try {
			stream = new FileStream (storagePath + fileName, FileMode.Open);
			using (stream) {
				retObj = serializer.Deserialize (stream);
			}
			stream.Close ();
		} catch (FileNotFoundException) {
		}
		
		return retObj;
	}
	
	/// <summary>
	/// Writes the serialized object into XML file..
	/// </summary>
	/// <param name='fileName'>
	/// XML file name, to store serialized object.
	/// </param>
	/// <param name='data'>
	/// Object for serialization.
	/// </param>
	/// <param name='type'>
	/// Type of object.
	/// </param>
	public void Save (string fileName, object data, Type type)
	{
		if (!Directory.Exists (storagePath)) {
			Directory.CreateDirectory (storagePath);
		}
		var serializer = new XmlSerializer (type);
		var stream = new FileStream (storagePath + fileName, FileMode.Create);
		serializer.Serialize (stream, data);
		stream.Close ();
	}
}