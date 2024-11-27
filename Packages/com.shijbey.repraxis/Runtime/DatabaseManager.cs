using UnityEngine;

namespace RePraxis
{
	/// <summary>
	/// A default implementation of a database manager for RePraxis.
	/// </summary>
	public class DatabaseManager : MonoBehaviour
	{
		private RePraxisDatabase m_Database = new RePraxisDatabase();

		public RePraxisDatabase Database => m_Database;
	}
}
