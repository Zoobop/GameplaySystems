using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    using DialogueSystem;

    public class DSSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private DSGraphView graphView;
        private Texture2D indentationIcon;

        public void Initialize(DSGraphView dsGraphView)
        {
            graphView = dsGraphView;

            indentationIcon = new Texture2D(1, 1);
            indentationIcon.SetPixel(0, 0, Color.clear);
            indentationIcon.Apply();
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Elements")),
                new SearchTreeGroupEntry(new GUIContent("Dialogue Nodes"), 1),
                new SearchTreeEntry(new GUIContent("Single Choice", indentationIcon))
                {
                    userData = DialogueType.SingleChoice,
                    level = 2
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice", indentationIcon))
                {
                    userData = DialogueType.MultipleChoice,
                    level = 2
                },
                new SearchTreeGroupEntry(new GUIContent("Dialogue Groups"), 1),
                new SearchTreeEntry(new GUIContent("Single Group", indentationIcon))
                {
                    userData = new Group(),
                    level = 2
                }
            };

            return searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            Vector2 localMousePosition = graphView.GetLocalMousePosition(context.screenMousePosition, true);

            switch (SearchTreeEntry.userData)
            {
                case DialogueType.SingleChoice:
                {
                    DSSingleChoiceNode singleChoiceNode = (DSSingleChoiceNode) graphView.CreateNode("DialogueName", DialogueType.SingleChoice, localMousePosition);

                    graphView.AddElement(singleChoiceNode);

                    return true;
                }

                case DialogueType.MultipleChoice:
                {
                    DSMultipleChoiceNode multipleChoiceNode = (DSMultipleChoiceNode) graphView.CreateNode("DialogueName", DialogueType.MultipleChoice, localMousePosition);

                    graphView.AddElement(multipleChoiceNode);

                    return true;
                }

                case Group _:
                {
                    graphView.CreateGroup("DialogueGroup", localMousePosition);

                    return true;
                }

                default:
                {
                    return false;
                }
            }
        }
    }
}