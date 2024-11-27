using UnityEngine;
using RePraxis;

public class LoveTriangleDetector : MonoBehaviour
{
	private DatabaseManager databaseManager;

	void Awake()
	{
		databaseManager = FindFirstObjectByType<DatabaseManager>();
	}

	public void DetectLoveTriangles()
	{
		/*
		Love triangles are detected using a query that looks for people who
		have a crush on someone and that someone does not have a crush on
		them. Additionally, this pattern must involve three characters
		(in this case ?a, ?b, and ?c) with each having a crush on the other
		in a circular fashion.
		*/
		var query = new DBQuery()
			.Where( $"?a.to.?b.type!Crush" )
			.Where( $"?b.to.?c.type!Crush" )
			.Where( $"?c.to.?a.type!Crush" )
			.Where( $"not ?b.to.?a.type!Crush" )
			.Where( $"not ?c.to.?b.type!Crush" )
			.Where( $"not ?a.to.?c.type!Crush" );

		QueryResult result = query.Run( databaseManager.Database );

		if ( result.Success )
		{
			Debug.Log( $"Love Triangles:\n{result.ToPrettyString()}" );
		}
		else
		{
			Debug.Log( "Love Triangles: None" );
		}
	}
}
