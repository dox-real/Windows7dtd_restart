set OBJECT=WScript.CreateObject("WScript.Shell")
Set OBJECT = Nothing
WScript.sleep 50 
OBJECT.SendKeys "<PASSWORD>{ENTER}" 
WScript.sleep 20400000
OBJECT.SendKeys "say ""Server Reboot in 2 hours.""{ENTER}" 
WScript.sleep 3600000
OBJECT.SendKeys "say ""Server Reboot in 1 hour.""{ENTER}" 
WScript.sleep 3000000 
OBJECT.SendKeys "say ""Server Reboot in 30 minutes.""{ENTER}" 
WScript.sleep 1200000
OBJECT.SendKeys "say ""Server Reboot in 10 minutes.""{ENTER}" 
WScript.sleep 600000 
OBJECT.SendKeys "exit{ENTER}" 
WScript.sleep 50 

