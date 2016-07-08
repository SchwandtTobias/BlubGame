// Based on: https://github.com/Desnoo/simple-unity-2d-map-editor

using UnityEngine;
using UnityEditor;

using System.Collections;
using System.IO;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
	Grid m_Grid;

	void OnEnable()
    {
		m_Grid = (Grid)target;
	}

	void OnSceneGUI()
    {
		int ControlID           = GUIUtility.GetControlID(FocusType.Passive);
		Event Event             = Event.current;
		Ray ScreenRay           = Camera.current.ScreenPointToRay(new Vector3(Event.mousePosition.x, -Event.mousePosition.y + Camera.current.pixelHeight));
		Vector3 ScreenRayOrigin = ScreenRay.origin;

		// Create tile
		if(Event.isMouse && Event.type == EventType.MouseDown && Event.button == 0)
        {
			GUIUtility.hotControl = ControlID;

			Event.Use();

            GameObject NewGameObject;
            GameObject SelectedPrefab = m_Grid.SelectedPrefab;

            if (SelectedPrefab != null)
            {
                Undo.IncrementCurrentGroup();

                Vector3 AlignedPosition = new Vector3(Mathf.Floor(ScreenRayOrigin.x / m_Grid.Width) * m_Grid.Width + m_Grid.Width / 2.0f, Mathf.Floor(ScreenRayOrigin.y / m_Grid.Height) * m_Grid.Height + m_Grid.Height / 2.0f, 0.0f);

                // If object is already there we do not create this tile; overwrite maybe?
                if (GetGameObjectAtPosition(AlignedPosition) != null)
                {
                    return;
                }

                // create new object with position from grid
                NewGameObject = (GameObject)PrefabUtility.InstantiatePrefab(SelectedPrefab);
                NewGameObject.transform.position = AlignedPosition;
                NewGameObject.transform.parent   = m_Grid.transform;

                Undo.RegisterCreatedObjectUndo(NewGameObject, "Create " + NewGameObject.name);
            }
        }

        // Delete a tile
		if(Event.isMouse & Event.type == EventType.MouseDown && Event.button == 1)
        {
			GUIUtility.hotControl = ControlID;

			Event.Use();

			Vector3 AlignedPosition = new Vector3(Mathf.Floor (ScreenRayOrigin.x / m_Grid.Width) * m_Grid.Width + m_Grid.Width / 2.0f, Mathf.Floor(ScreenRayOrigin.y/m_Grid.Height) * m_Grid.Height + m_Grid.Height / 2.0f, 0.0f);
			GameObject FoundGameObject = GetGameObjectAtPosition(AlignedPosition);

			if(FoundGameObject != null)
            {
				DestroyImmediate(FoundGameObject);
			}
		}	
	}

	GameObject GetGameObjectAtPosition(Vector3 _SearchPosition)
    {
		for(int IndexOfChild = 0; IndexOfChild < m_Grid.transform.childCount; ++IndexOfChild)
        {
			Transform Child = m_Grid.transform.GetChild(IndexOfChild);

			if(Child.position == _SearchPosition)
            {
				return Child.gameObject;
			}
		}

		return null;
	}
}
