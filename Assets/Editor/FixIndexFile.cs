using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

namespace WebGLKeyboard
{
    /// <summary>
    /// Edits the index.html file (the Unity default one) after the build to add the necessary elements to the fix work
    /// </summary>
    public class FixIndexFile : MonoBehaviour
    {
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.WebGL)
            {
                string text = File.ReadAllText(Path.Combine(pathToBuiltProject, "index.html"));
                //original code
                //string original = "<div class=\"webgl-content\">";
                //my code
                string original = "<body>";

                
                //original code
                //string replace = "<script>\r\n\t\tfunction FixInputOnSubmit() {\r\n\t\t\tdocument.getElementById(\"fixInput\").blur();\r\n\t\t\tevent.preventDefault();\r\n\t\t}\r\n\t</script>\r\n    <div>\r\n\t\t<form onsubmit=\"FixInputOnSubmit()\" autocomplete=\"off\" style=\"width: 0px; height: 0px; position: absolute; top: -9999px;\">\r\n\t\t\t<input type=\"text\" id=\"fixInput\" oninput=\"unityInstance.Module.ccall('FixInputUpdate', null)\" onblur=\"unityInstance.Module.ccall('FixInputOnBlur', null)\" style=\"font-size: 42px;\">\r\n\t\t</form>\r\n\t</div>\r\n\t<div class=\"webgl-content\">";
                //github fix
                //string replace = "<script>\r\n\t\tfunction FixInputOnSubmit() {\r\n\t\t\tdocument.getElementById(\"fixInput\").blur();\r\n\t\t\tevent.preventDefault();\r\n\t\t}\r\n\t</script>\r\n         \r\n\t\t<form onsubmit=\"FixInputOnSubmit()\" autocomplete=\"off\" style=\"width: 0px; height: 0px; position: absolute; top: -9999px;\">\r\n\t\t\t<input type=\"text\" id=\"fixInput\" oninput=\"unityInstance.Module.asmLibraryArg._FixInputUpdate()\"onblur=\"unityInstance.Module.asmLibraryArg._FixInputOnBlur()\"style=\"font-size: 42px;\">\r\n\t\t\r\n\t </form>   \r\n\t<div class=\"webgl-content\">";
                //my code
                string replace = original + "\r\n\t<script>\r\n\t\tfunction FixInputOnSubmit() {\r\n\t\t\tdocument.getElementById(\"fixInput\").blur();\r\n\t\t\tevent.preventDefault();\r\n\t\t}\r\n\t</script>\r\n         \r\n\t<form onsubmit=\"FixInputOnSubmit()\" autocomplete=\"off\" style=\"width: 0px; height: 0px; position: absolute; top: -9999px;\">\r\n\t\t<input type=\"text\" id=\"fixInput\" oninput=\"unityInstance.Module.asmLibraryArg._FixInputUpdate()\"onblur=\"unityInstance.Module.asmLibraryArg._FixInputOnBlur()\"style=\"font-size: 42px;\">\r\n\t</form>\r\n";


                text = text.Replace(original, replace);
                File.WriteAllText(Path.Combine(pathToBuiltProject, "index.html"), text);
            }
        }
    }
}