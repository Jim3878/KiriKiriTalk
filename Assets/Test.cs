using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yeh.Parser;
namespace KirikiriTalk
{
    public class Test : MonoBehaviour
    {

        KirikiriController manager;
        // Use this for initialization
        void Start()
        {
            Debug.Log(Color.red.ToHex());


            string text = @"[hides][wait=0.5][setChara,name=""Leading"",file=""chara"",from=""LeftLeft"",to=""Left"",duration=0.5f][wait=0.5f][show]
[UserName][username][USERNAME]UserName在用55555555\\加上雙引號()[waitClick]即可";
            manager = KirikiriController.Instance();


            manager.AddParser(
                new AddCharaCommand(),
                new ShowTalkDialog(),
                new HideTalkDialog(),
                new WaitSeconds(),
                new HideTalkDialogASAP(),
                new WaitClick()
                );

            manager.SetDialogString(text.Replace("\n",""));
            manager.SetCommand(new TextHandlerReplacHandler(new ReplaceParser("UserName", "主人公")));
            manager.delayPerChar = 0.1f;
            //manager.SetCommand(new ZoomInOnStartCommand());
            manager.Play();

            Yeh.InputEvent.onMouseUp += OnMouseDown;
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseDown(object sender, Yeh.MouseClickEvnetArgs e)
        {
            Debug.Log(e.button);
        }
    }
}