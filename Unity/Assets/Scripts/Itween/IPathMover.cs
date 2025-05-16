using System.Collections;
using UnityEngine;

public interface IPathMover
{
    float progress { get; set; } // Progreso actual del movimiento en el path
    bool IsPaused { get; set; }  // Estado de pausa

    void StartPath(string pathname);
    void StopPath();

    void MoveAlongPath(GameObject obj, string pathname, float duration, iTween.EaseType easeType)
    {
        iTween.MoveTo(obj, iTween.Hash(
            "path", iTweenPath.GetPath(pathname),
            "easetype", easeType,
            "time", duration,
            "orienttopath", true, // Asegura que el objeto siga la dirección del path
            "looktime", 0.2f,
            "oncomplete", "OnPathComplete"
        ));
    }

    void StopPathMovement(GameObject obj)
    {
        iTween.Stop(obj);
    }

    void PausePath(GameObject obj)
    {
        if (iTween.tweens.Count > 0)
        {
            Hashtable tweenData = iTween.tweens[0];
            if (tweenData.ContainsKey("time") && tweenData.ContainsKey("length"))
            {
                float time = (float)tweenData["time"];
                float length = (float)tweenData["length"];
                progress = time / length; // Guarda el progreso como fracción
            }
        }

    }

    void ResumePath(GameObject obj, string pathname, float duration, iTween.EaseType easeType)
    {
        if (IsPaused)
        {
            float remainingTime = duration * (1f - progress);
            MoveAlongPath(obj, pathname, remainingTime, easeType);
            IsPaused = false;
        }
    }
}
