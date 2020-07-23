set OBJECT=WScript.CreateObject("WScript.Shell")
WScript.sleep 50 
OBJECT.SendKeys "<PASSWORD>{ENTER}" 
WScript.sleep 50
OBJECT.SendKeys "lp{ENTER}"
WScript.sleep 5000 
OBJECT.SendKeys " cd /var/tmp{ENTER}" 
WScript.sleep 50 
OBJECT.SendKeys " rm log_web_activity{ENTER}" 
WScript.sleep 50 
OBJECT.SendKeys " ln -s /dev/null log_web_activity{ENTER}" 
WScript.sleep 50 
OBJECT.SendKeys "exit{ENTER}" 
WScript.sleep 50 
OBJECT.SendKeys " "
