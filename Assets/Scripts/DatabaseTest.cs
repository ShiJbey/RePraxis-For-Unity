using UnityEngine;
using RePraxis;

public class DatabaseTest : MonoBehaviour
{
    void Start()
    {
        DatabaseManager manager = FindFirstObjectByType<DatabaseManager>();

        manager.Database.Insert("ashley.dislikes.mike");

        Debug.Log(
            manager.Database.Assert("ashley.dislikes.mike")
        );
    }
}
