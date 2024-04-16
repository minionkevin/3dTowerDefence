using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
        private static UIManager instance = new UIManager();

        public static UIManager Instance => instance;

        private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

        private Transform canvasRect;

        private UIManager()
        {
                var canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
                canvasRect = canvas.transform;
                GameObject.DontDestroyOnLoad(canvas);
        }

        public T ShowPanel<T>()where T:BasePanel
        {
                var panelName = typeof(T).Name;
                if (panelDic.ContainsKey(panelName))
                {
                        panelDic[panelName].TransitionIn();
                        return panelDic[panelName] as T;
                }
                var newPanel = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName), canvasRect, false);
                var panelScript = newPanel.GetComponent<T>();
                panelDic.Add(panelName,panelScript);
                panelScript.TransitionIn();
                
                return panelScript;
        }
        
        public void HidePanel<T>(bool hasFade = true) where T:BasePanel
        {
                var panelName = typeof(T).Name;
                if (panelDic.ContainsKey(panelName))
                {
                        if (hasFade)
                        {
                                panelDic[panelName].TransitionOut();
                        }
                        GameObject.Destroy(panelDic[panelName].gameObject);
                        panelDic.Remove(panelName);     
                }
        }

        public T GetPanel<T>() where T : BasePanel
        {
                var panelName = typeof(T).Name;
                if (panelDic.ContainsKey(panelName)) return panelDic[panelName] as T;
                return null;
        }
}
