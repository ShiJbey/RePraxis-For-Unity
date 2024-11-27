using System.Collections.Generic;
using System.Linq;
using RePraxis;
using UnityEditor;
using UnityEngine.UIElements;


[CustomEditor(typeof(DatabaseManager))]
public class DatabaseManagerDrawerUIE : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement myInspector = new VisualElement();

        ScrollView scrollView = new ScrollView(ScrollViewMode.Vertical);

        myInspector.Add(scrollView);

        DatabaseManager databaseManager = (DatabaseManager)target;

        if (databaseManager.Database != null)
        {
            List<string> sentences = GetDatabaseSentences(databaseManager.Database);
            foreach (var entry in sentences)
            {
                scrollView.Add(new Label(entry));
            }
        }

        // Return the finished Inspector UI.
        return myInspector;
    }

    public List<string> GetDatabaseSentences(RePraxisDatabase database)
    {
        List<string> sentences = new List<string>();

        var nodeStack = new Stack<INode>(database.Root.Children);

        while (nodeStack.Count > 0)
        {
            INode node = nodeStack.Pop();

            IEnumerable<INode> children = node.Children;

            if (children.Count() > 0)
            {
                // Add children to the stack
                foreach (var child in children)
                {
                    nodeStack.Push(child);
                }
            }
            else
            {
                // This is a leaf
                sentences.Add(node.GetPath());
            }
        }

        return sentences;
    }
}
