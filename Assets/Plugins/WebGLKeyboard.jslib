mergeInto(LibraryManager.library, {

	OpenInputKeyboard: function (currentValue) 
	{
        if (UnityLoader.SystemInfo.mobile)
        {
            document.getElementById("fixInput").value = Pointer_stringify(currentValue);
            document.getElementById("fixInput").focus();
        }
	},
	CloseInputKeyboard: function ()
	{
        if (UnityLoader.SystemInfo.mobile)
        {
            document.getElementById("fixInput").blur();
        }
	},
	FixInputOnBlur: function ()
	{
        if (UnityLoader.SystemInfo.mobile)
        {
            SendMessage('_WebGLKeyboard', 'LoseFocus');
        }
	},
	FixInputUpdate: function () 
	{
        if (UnityLoader.SystemInfo.mobile)
        {
            SendMessage('_WebGLKeyboard', 'ReceiveInputChange', document.getElementById("fixInput").value);
        }
	},
    IsMobile: function ()
    {
        SendMessage('MobileDetector', 'DetectPlatform', UnityLoader.SystemInfo.mobile.toString());
    },
});