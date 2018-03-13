using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KirikiriTalk.Parser;
namespace KirikiriTalk
{
    public class Test : MonoBehaviour
    {

        KirikiriController manager;
        // Use this for initialization
        void Start()
        {
            Debug.Log(Color.red.ToHex());

            
            string text = @"[hides][wait=0.5][wait=0.5f][show][setChara, name=""Leading"", file=""chara"", from=""Left_Left"", to=""Left"", duration=0.5f]
[UserName][username][USERNAME]UserNaddddddddddddddddddddddddddddddddddddddddme在用55555555\\加上雙引號()[waitClick][clear]即可[waitClick]";
            manager = KirikiriController.Instance();


            manager.AddParser(
                new AddChara(),
                new ShowTalkDialog(),
                new HideTalkDialog(),
                new WaitSeconds(),
                new HideTalkDialogASAP(),
                new WaitClick(),
                new ClearDialog()
                );

            manager.SetDialogString(text.Replace("\n", ""));
            manager.SetCommand(new TextHandlerReplacHandler(new ReplaceParser("UserName", "主人公")));
            manager.SetCommand(new ShowDialogImmediatelyCommand());
            manager.charDelay = 0.1f;
            //manager.SetCommand(new ZoomInOnStartCommand());
            manager.Play();

            InputInvoker.instance.mouseEvent += OnMouseDown;
            InputInvoker.instance.keyEvent += OnMouseDown;

        }

        private void OnMouseDown(InputInvoker.clickTypeEnum clickType, KeyCode code)
        {
            //Debug.Log(string.Format("clickTpe={0} , keycode={1}", clickType, code));
        }
    }
}