using System;
using System.Linq;
using RePraxis;
using UnityEngine;

public class RelationshipFinder : MonoBehaviour
{
	public string characterName;
	public SampleCharacter.RelationshipType relationshipType;
	[Range( 0, 10 )]
	public int opinionThreshold;

	private DatabaseManager databaseManager;

	void Awake()
	{
		databaseManager = FindFirstObjectByType<DatabaseManager>();
	}

	public void FindRelationships()
	{
		/*
		This query bind the opinion score to the ?opinion variable and check
		if it is greater than or equal to the current threshold value. Additionally,
		the query may check for a specific relationship type if one is specified.
		*/
		var query = new DBQuery()
				.Where( $"?other.to.{characterName}.opinion!?opinion" )
				.Where( $"gte ?opinion {opinionThreshold}" );

		if ( relationshipType != SampleCharacter.RelationshipType.None )
		{
			query = query.Where( $"?other.to.{characterName}.type!{relationshipType}" );
		}

		QueryResult result = query.Run( databaseManager.Database );

		if ( result.Success )
		{
			string output = String.Join(
				", ",
				result.Bindings.Select( (entry) => (string)entry["?other"] )
			);

			Debug.Log( $"Results: {output}" );
		}
		else
		{
			Debug.Log( "Results: None" );
		}
	}
}
