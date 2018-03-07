using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KirikiriTalk {
    public class CharaBoard : MonoBehaviour {

        KirikiriController ctrl;
        [SerializeField]
        Image image;
        static string prefabsPath = "CharaBoard";


        public static CharaBoard Instance(string charaName, Sprite sprite,KirikiriController ctrl)
        {
            GameObject prefabs = Resources.Load<GameObject>(prefabsPath);
            prefabs = GameObject.Instantiate<GameObject>(prefabs, ctrl.CharaLayer);
            
            return prefabs.GetComponent<CharaBoard>().Initialized(charaName,sprite, ctrl);
        }

        public CharaBoard Initialized(string charaName, Sprite sprite,KirikiriController ctrl)
        {
            this.ctrl = ctrl;
            gameObject.name = charaName;
            SetImage(sprite);
            return this;
        }

        public CharaBoard SetImage(Sprite sprite)
        {
            image.sprite = sprite;
            image.SetNativeSize();
            return this;
        }

        public CharaBoard SetPosition(CharaPosition position)
        {
            transform.position = ctrl.GetCharaPosition(position).position;
            return this;
        }

        public CharaBoard SetPosition(Vector3 position)
        {
            transform.position = position;
            return this;
        }

        public CharaBoard SetScale(Vector3 scale)
        {
            transform.localScale = scale;
            return this;
        }

        public CharaBoard SetColor(Color color)
        {
            image.color = color;
            return this;
        }
        
    }
}