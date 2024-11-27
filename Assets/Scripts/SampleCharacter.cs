using System;
using System.Collections.Generic;
using RePraxis;
using UnityEngine;

public class SampleCharacter : MonoBehaviour
{
	public string characterName;
	public List<Relationship> relationships;

	private DatabaseManager databaseManager;

	void Awake()
	{
		databaseManager = FindFirstObjectByType<DatabaseManager>();
	}

	void Start()
	{
		databaseManager.Database.Insert( $"{characterName}" );
		UpdateRelationshipsInDB( databaseManager.Database );
		// databaseManager.Database.AddBeforeAccessListener(
		// 	$"{characterName}.to", UpdateRelationshipsInDB );
	}

	void OnEnable()
	{
		// Add the listener when the gameobject is active.
		databaseManager.Database.AddBeforeAccessListener(
			$"{characterName}.to", UpdateRelationshipsInDB );
	}

	void OnDisable()
	{
		// Remove the listener when the gameobject is not active.
		databaseManager.Database.RemoveBeforeAccessListener(
			$"{characterName}.to", UpdateRelationshipsInDB );
	}

	void UpdateRelationshipsInDB(RePraxisDatabase database)
	{
		// Remove all relationship entries under the given prefix
		database.Delete( $"{characterName}.to" );
		// Loop through all current relationships and add their info to the database
		// Example: 'Alice.to.Player.type!Friend' and 'Alice.to.Player.opinion!10'
		foreach ( var r in relationships )
		{
			string targetName = r.target.characterName;
			string relationshipType = r.relationshipType.ToString();
			database.Insert( $"{characterName}.to.{targetName}.type!{relationshipType}" );
			database.Insert( $"{characterName}.to.{targetName}.opinion!{r.opinion}" );
		}
	}

	[Serializable]
	public class Relationship
	{
		public SampleCharacter target;
		public RelationshipType relationshipType;
		[Range( 0, 10 )]
		public int opinion;
	}

	public enum RelationshipType
	{
		None,
		Acquaintance,
		Friend,
		Enemy,
		Crush,
		Lover
	}
}
