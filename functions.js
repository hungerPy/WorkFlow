function doThis(tbox,tp)
		{
		var s,sv,nd,typ;
		typ=window.event.type
		sv=tbox.value
		nd=sv.indexOf(".")
		s=window.event.keyCode
		//alert(s);
		/*alert(sv.length)
		alert(nd)
		alrt(sv.length-nd)*/
		//alert(typ);
		if(typ=="paste" || typ=="drop" || typ=="drag")
				return false;
	
		if(tp== "num"&& (s < 48 || s > 57)) 
			  window.event.keyCode=0
		else 
				//if(tp=="fnum" && (((s < 48 || s > 57) && (s != 46 || nd !=-1)) || (nd != -1 && sv.length-nd > 2)))
				if(tp=="fnum" && (s < 48 || s > 57) && (s != 46 || nd !=-1))// || (nd != -1 && sv.length-nd > 2)))
						window.event.keyCode=0
				else
						if (tp=="alpha" && s!=32 && (s < 65 || s > 122) || (s > 90 && s < 97))
								window.event.keyCode=0	
						else	
								if(tp=="alphaN" && s!=32 && (s < 48 || s > 122) || (s > 90 && s < 97) || (s > 57 && s < 65)  )
										window.event.keyCode=0
								else
										if(tp=="sp" && ((s > 47 && s < 58) || (s > 64 && s < 91) || (s > 96 && s < 123)))
												window.event.keyCode=0	

										else
			                                    if(tp=="fnum_y" && (s < 48 || s > 57) && (s != 46 || nd !=-1) && (s!=121 || sv.indexOf("y")!=-1))
						                            window.event.keyCode=0
			
		}
		

function validate(frm,fld1,fld2)
{
var i,k;
//var cset[]={"'","&",
for(i=0 ; i < frm.elements.length;i++)
	{
	frm.elements[i].style.backgroundColor=""
    if(frm.elements[i].type=="text" || frm.elements[i].type=="password" || frm.elements[i].type=="textarea")
    {
            for(k=0;k<frm.elements[i].value.length;k++)
			    if (frm.elements[i].value.charCodeAt(k)==39 ||frm.elements[i].value.charCodeAt(k)==34 || frm.elements[i].value.charCodeAt(k)==38)
			    {
			    alert(frm.elements[i].title.toLowerCase() + " having invalid character.")
			    frm.elements[i].focus()
				frm.elements[i].style.backgroundColor="yellow"
				return false;
			    }
	        if (frm.elements[i].accessKey=="r")
		        if(frm.elements[i].value=="")
			{		
				alert("Please enter " + frm.elements[i].title.toLowerCase() + ".")
				frm.elements[i].focus()
				frm.elements[i].style.backgroundColor="yellow"
				return false;
			}
		}
		else
			if(frm.elements[i].type=="select-one")
			{
			 if (frm.elements[i].accessKey=="r")
				if(frm.elements[i].selectedIndex==0)
				{
					//alert("Please select the Mandatory field.")
					alert("Please select " + frm.elements[i].title.toLowerCase() + ".")
					//alert(frm.elements[i].style.visibility)
					frm.elements[i].focus()
					frm.elements[i].style.backgroundColor="yellow"
					return false;
				}
			}	
			
}

	if(fld1!="" && fld2 !="")
		if (frm.elements[fld1].value != frm.elements[fld2].value)
		{
		 alert("Password missmatch!!!")
      	 frm.elements[fld1].focus()
		 frm.elements[fld1].style.backgroundColor="yellow"
		 frm.elements[fld2].focus()
		 frm.elements[fld2].style.backgroundColor="yellow"
		 return false;
		}
		return true;


}


//function validateREG(frm,fld1,fld2)
//{
//var i,k,tid;
//tid="";
//document.getElementById("tblApp").style.display="none"
//document.getElementById("tblCo").style.display="none"
//for(i=0 ; i < frm.elements.length;i++)
//	{
//	frm.elements[i].style.backgroundColor=""
//	if(frm.elements[i].type=="text" || frm.elements[i].type=="password" || frm.elements[i].type=="textarea")
//	for(k=0;k<frm.elements[i].value.length;k++)
//			if (frm.elements[i].value.charCodeAt(k)==39 ||frm.elements[i].value.charCodeAt(k)==34 || frm.elements[i].value.charCodeAt(k)==38)
//			    {
//			    alert(frm.elements[i].title.toLowerCase() + " having invalid character.")
//			    frm.elements[i].style.backgroundColor="yellow"
//				return false;
//			    }
//		
//	if (frm.elements[i].accessKey!="")
//	{
//	    if (frm.elements[i].accessKey=="A")
//	    {
//	    tid="tblApp"
//	    tdHead.innerText="Applicant Detail"
//	    }
//	    else if(frm.elements[i].accessKey=="C")
//	    {
//	    tid="tblCo"
//	    tdHead.innerText="Co-Applicant Detail"
//	    }
//	    else
//	    {
//	    tid="tblPayment"
//	    tdHead.innerText="Payment Detail"
//	    }
//	
//		if(frm.elements[i].type=="text" || frm.elements[i].type=="password" || frm.elements[i].type=="textarea")
//		{
//			if(frm.elements[i].value=="")
//			{	document.getElementById(tid).style.display=""
//			    alert("Please enter " + frm.elements[i].title.toLowerCase() + ".")
//				frm.elements[i].focus()
//				frm.elements[i].style.backgroundColor="yellow"
//				return false;
//			}
//		}
//		else
//			if(frm.elements[i].type=="select-one")
//			{
//			    if(frm.elements[i].selectedIndex==0)
//				{
//				document.getElementById(tid).style.display=""
//					alert("Please select " + frm.elements[i].title.toLowerCase() + ".")
//					frm.elements[i].focus()
//					frm.elements[i].style.backgroundColor="yellow"
//					return false;
//				}
//			}	
//		
//	}	
//}
//	
//	
//}


//function validateDP(frm,fld1,fld2)
//{
//var i,k,tid;
//tid="";
////document.getElementById("tblApp").style.display="none"
////document.getElementById("tblCo").style.display="none"
//document.getElementById("tblPayment").style.display="none"
//document.getElementById("tblPlot").style.display="none"
//document.getElementById("tblPS").style.display="none"
//for(i=0 ; i < frm.elements.length;i++)
//	{
//	frm.elements[i].style.backgroundColor=""
//	if(frm.elements[i].type=="text" || frm.elements[i].type=="password" || frm.elements[i].type=="textarea")
//	for(k=0;k<frm.elements[i].value.length;k++)
//			if (frm.elements[i].value.charCodeAt(k)==39 ||frm.elements[i].value.charCodeAt(k)==34 || frm.elements[i].value.charCodeAt(k)==38)
//			    {
//			    alert(frm.elements[i].title.toLowerCase() + " having invalid character.")
//			  	frm.elements[i].style.backgroundColor="yellow"
//				return false;
//			    }
//	
//	
//	if (frm.elements[i].accessKey!="")
//	{
////	    if (frm.elements[i].accessKey=="A")
////	    {
////	    tid="tblApp"
////	    tdHead.innerText="Applicant Detail"
////	    }
////	    else if(frm.elements[i].accessKey=="C")
////	    {
////	    tid="tblCo"
////	    tdHead.innerText="Co-Applicant Detail"
////	    }
////	    else 
//       if(frm.elements[i].accessKey=="L")
//	    {
//	    tid="tblPlot"
//	    tdHead.innerText="Plot Detail"
//	    }
//	    else if(frm.elements[i].accessKey=="S")
//	    {
//	    tid="tblPS"
//	    tdHead.innerText="Payment Schedule"
//	    }
//	    else
//	    {
//	    tid="tblPayment"
//	    tdHead.innerText="Payment Detail"
//	    }
//	
//		if(frm.elements[i].type=="text" || frm.elements[i].type=="password" || frm.elements[i].type=="textarea")
//		{
//			if(frm.elements[i].value=="")
//			{	document.getElementById(tid).style.display=""
//			    alert("Please enter " + frm.elements[i].title.toLowerCase() + ".")
//				frm.elements[i].focus()
//				frm.elements[i].style.backgroundColor="yellow"
//				return false;
//			}
//		}
//		else
//			if(frm.elements[i].type=="select-one")
//			{
//				if(frm.elements[i].selectedIndex==0)
//				{
//				document.getElementById(tid).style.display=""
//					alert("Please select " + frm.elements[i].title.toLowerCase() + ".")
//					frm.elements[i].focus()
//					frm.elements[i].style.backgroundColor="yellow"
//					return false;
//				}
//			}	
//		
//	}	
//}
//	if(frm.elements["txtCashPayment"].value=="")
//	frm.elements["txtCashPayment"].value="0"
//	if(frm.elements["txtChequeAmount"].value=="")
//	frm.elements["txtChequeAmount"].value="0"
//	if(frm.elements["txtRefNo"].value=="")
//	frm.elements["txtRefNo"].value="0"
//	if(parseFloat(frm.elements["txtRefNo"].value)==0)
//	{
//	document.getElementById("tblPayment").style.display=""
//	alert("Reference No. Missing.")
//	frm.elements["txtRefNo"].select();
//	return false;
//	}
//	if(frm.elements["txtP"].value=="")
//	frm.elements["txtP"].value="0"
//	if(parseFloat(frm.elements["txtP"].value)!= 100)
//	{
//	document.getElementById("tblPS").style.display=""
//	alert("Total Percent must be 100.")
//	frm.elements["txtP"].select();
//	return false;
//	}
//	
//	
//	
//	
//	if (parseFloat(frm.elements["txtCashPayment"].value)==0 && parseFloat(frm.elements["txtChequeAmount"].value)==0 )
//	{
//	document.getElementById("tblPayment").style.display=""
//	alert("Booking Amount Missing.")
//	frm.elements["txtCashPayment"].focus();
//	return false;
//	}
//	if(document.getElementById("txtChequeNo").value !="" || document.getElementById("txtChequeAmt").value != "")
//    {
//    document.getElementById("tblPayment").style.display=""
//    alert("Please add the cheque detail for the given cheque No. or cheque Amount.")
//    return false;
//    }
//	
//}




//function validateC(frm,tb)
//{
//var i,k,tid;
//tid="tbl" + tb;
////document.getElementById("tblApp").style.display="none"
//document.getElementById(tid).style.display="none"
//document.getElementById("tblPD").style.display="none"
//document.getElementById("tblPS").style.display="none"
//for(i=0 ; i < frm.elements.length;i++)
//	{
//	frm.elements[i].style.backgroundColor=""
//	if(frm.elements[i].type=="text" || frm.elements[i].type=="password" || frm.elements[i].type=="textarea")
//	for(k=0;k<frm.elements[i].value.length;k++)
//			if (frm.elements[i].value.charCodeAt(k)==39 ||frm.elements[i].value.charCodeAt(k)==34 || frm.elements[i].value.charCodeAt(k)==38)
//			    {
//			    document.getElementById(tid).style.display=""
//			    alert(frm.elements[i].title.toLowerCase() + " having invalid character.")
//			    frm.elements[i].focus()
//				frm.elements[i].style.backgroundColor="yellow"
//				return false;
//			    }
//	
//	
//	if (frm.elements[i].accessKey!="")
//	{
//	    if (frm.elements[i].accessKey=="T")
//	    {
//	    //tid=tid
//	    tdHead.innerText= tb + " Detail"
//	    }
//	    else if(frm.elements[i].accessKey=="D")
//	    {
//	    tid="tblPD"
//	    tdHead.innerText="Payment Detail"
//	    }
////	    else if(frm.elements[i].accessKey=="S")
////	    {
////	    tid="tblPS"
////	    tdHead.innerText="Payment Schedule"
////	    }
//	    else
//	    {
//	    tid="tblPS"
//	    tdHead.innerText="Payment Schedule"
//	    }
//	
//		if(frm.elements[i].type=="text" || frm.elements[i].type=="password" || frm.elements[i].type=="textarea")
//		{
//			if(frm.elements[i].value=="")
//			{	document.getElementById(tid).style.display=""
//			    alert("Please enter " + frm.elements[i].title.toLowerCase() + ".")
//				frm.elements[i].focus()
//				frm.elements[i].style.backgroundColor="yellow"
//				return false;
//			}
//		}
//		else
//			if(frm.elements[i].type=="select-one")
//			{
//				if(frm.elements[i].selectedIndex==0)
//				{
//				document.getElementById(tid).style.display=""
//					alert("Please select " + frm.elements[i].title.toLowerCase() + ".")
//					frm.elements[i].focus()
//					frm.elements[i].style.backgroundColor="yellow"
//					return false;
//				}
//			}	
//		
//	}	
//	
//}
//	
//	return true;	
//}








//function validateDF(frm,fld1,fld2)
//{
//var i,k,tid;
//tid="";
////document.getElementById("tblApp").style.display="none"
////document.getElementById("tblCo").style.display="none"
//document.getElementById("tblPayment").style.display="none"
//document.getElementById("tblFlat").style.display="none"
//document.getElementById("tblPS").style.display="none"
//for(i=0 ; i < frm.elements.length;i++)
//	{
//	frm.elements[i].style.backgroundColor=""
//	if(frm.elements[i].type=="text" || frm.elements[i].type=="password" || frm.elements[i].type=="textarea")
//	for(k=0;k<frm.elements[i].value.length;k++)
//			if (frm.elements[i].value.charCodeAt(k)==39 ||frm.elements[i].value.charCodeAt(k)==34 || frm.elements[i].value.charCodeAt(k)==38)
//			    {
//			    document.getElementById(tid).style.display=""
//			    alert(frm.elements[i].title.toLowerCase() + " having invalid character.")
//			    frm.elements[i].focus()
//				frm.elements[i].style.backgroundColor="yellow"
//				return false;
//			    }
//	
//	
//	if (frm.elements[i].accessKey!="")
//	{
////	    if (frm.elements[i].accessKey=="A")
////	    {
////	    tid="tblApp"
////	    tdHead.innerText="Applicant Detail"
////	    }
////	    else if(frm.elements[i].accessKey=="C")
////	    {
////	    tid="tblCo"
////	    tdHead.innerText="Co-Applicant Detail"
////	    }
////	    else 
//        if(frm.elements[i].accessKey=="L")
//	    {
//	    tid="tblFlat"
//	    tdHead.innerText="Flat Allotment"
//	    }
//	    else if(frm.elements[i].accessKey=="S")
//	    {
//	    tid="tblPS"
//	    tdHead.innerText="Payment Schedule"
//	    }
//	    else
//	    {
//	    tid="tblPayment"
//	    tdHead.innerText="Payment Detail"
//	    }
//	
//		if(frm.elements[i].type=="text" || frm.elements[i].type=="password" || frm.elements[i].type=="textarea")
//		{
//			if(frm.elements[i].value=="")
//			{	document.getElementById(tid).style.display=""
//			    alert("Please enter " + frm.elements[i].title.toLowerCase() + ".")
//				frm.elements[i].focus()
//				frm.elements[i].style.backgroundColor="yellow"
//				return false;
//			}
//		}
//		else
//			if(frm.elements[i].type=="select-one")
//			{
//				if(frm.elements[i].selectedIndex==0)
//				{
//				document.getElementById(tid).style.display=""
//					alert("Please select " + frm.elements[i].title.toLowerCase() + ".")
//					frm.elements[i].focus()
//					frm.elements[i].style.backgroundColor="yellow"
//					return false;
//				}
//			}	
//		
//	}	
//}
//	
//	if(frm.elements["txtCashPayment"].value=="")
//	frm.elements["txtCashPayment"].value="0"
//	if(frm.elements["txtChequeAmount"].value=="")
//	frm.elements["txtChequeAmount"].value="0"
//	
//	if(frm.elements["txtRefNo"].value=="")
//	frm.elements["txtRefNo"].value="0"
//	if(parseFloat(frm.elements["txtRefNo"].value)==0)
//	{
//	document.getElementById("tblPayment").style.display=""
//	alert("Reference No. Missing.")
//	frm.elements["txtRefNo"].select();
//	return false;
//	}
//	if(frm.elements["txtP"].value=="")
//	frm.elements["txtP"].value="0"
//	if(parseFloat(frm.elements["txtP"].value)!= 100)
//	{
//	document.getElementById("tblPS").style.display=""
//	alert("Total Percent must be 100.")
//	frm.elements["txtP"].select();
//	return false;
//	}
//	
//	if (parseFloat(frm.elements["txtCashPayment"].value)==0 && parseFloat(frm.elements["txtChequeAmount"].value)==0 )
//	{
//	document.getElementById("tblPayment").style.display=""
//	alert("Booking Amount Missing.")
//	frm.elements["txtCashPayment"].focus();
//	return false;
//	}
//	if(document.getElementById("txtChequeNo").value !="" || document.getElementById("txtChequeAmt").value != "")
//    {
//    document.getElementById("tblPayment").style.display=""
//    alert("Please add the cheque detail for the given cheque No. or cheque Amount.")
//    return false;
//    }
//		
//}

function resetIt(frm)
{
for(i=0 ; i < frm.elements.length;i++)
	{
	    
		if(frm.elements[i].type=="text" || frm.elements[i].type=="password" || frm.elements[i].type=="textarea")
		    {
	    	    frm.elements[i].value=""
	    	    frm.elements[i].style.backgroundColor=""
	    	}
		else
			if(frm.elements[i].type=="select-one")
			    {
			        frm.elements[i].style.backgroundColor=""
			        switch(frm.elements[i].title.toLowerCase())
			        {
			        case "state":
			        case "city":
			                    removeOptions(frm.elements[i]);
			                    break;
			        default : 
			                    frm.elements[i].selectedIndex=0;
			        }
			        
			    }
      
      }
}


//function getDate1(n,d1,m1,y1)
//{
//try{
//var dn,mn,yn,di=0;
//if (document.getElementById(d1)!=null)
//{
// dn = document.getElementById(d1)
// mn = document.getElementById(m1)
// yn = document.getElementById(y1)
// }
// else
// {
// dn=d1
// mn=m1
// yn=y1
// }
//  if (isDate(dn.selectedIndex +1,mn.selectedIndex +1,parseInt(yn.value)))
//    di=dn.selectedIndex
//  
// 
//var d,m,y,i;
//var myele;
//y=yn.value;
//m=mn.selectedIndex +1;
//d=dn.value;
//for (i=dn.length; i>=0;i--) 
//dn.remove(i);
//if(m==2) 
//	if (y%400==0 || (y % 4==0 && y % 100!=0))
//		for( i=1 ;i<=29;i++)
//		{
//		myele = document.createElement("option") 
//        myele.setAttribute('value',i)
//        var txt = document.createTextNode(i)
//    	myele.appendChild(txt)
//    	dn.appendChild(myele)		
//     	}
//	else
//	for( i=1 ;i<=28;i++)
//		{
//		myele = document.createElement("option") 
//        myele.setAttribute('value',i)
//        var txt = document.createTextNode(i)
//    	myele.appendChild(txt)
//    	dn.appendChild(myele)		
//    	}
//else 
//	if (m==4 || m==6 || m==9 || m==11)
//		for( i=1 ;i<=30;i++)
//		{
//		myele = document.createElement("option") 
//        myele.setAttribute('value',i)
//        var txt = document.createTextNode(i)
//     	myele.appendChild(txt)
//     	dn.appendChild(myele)
//		}
//	else
//		for( i=1 ;i<=31;i++)
//		{
//		myele = document.createElement("option") 
//    	myele.setAttribute('value',i)
//     	var txt = document.createTextNode(i)
//     	myele.appendChild(txt)
//     	dn.appendChild(myele)
//		}

//}catch(e){}
//}


function getDate(n,d1,m1,y1)
{
try{
var dn,mn,yn,di=0;
if (document.getElementById(d1)!=null)
{
 dn = document.getElementById(d1)
 mn = document.getElementById(m1)
 yn = document.getElementById(y1)
 }
 else
 {
 dn=d1
 mn=m1
 yn=y1
 }
  if (isDate(dn.selectedIndex +1,mn.selectedIndex +1,parseInt(yn.value)))
    di=dn.selectedIndex
 dn.selectedIndex=di
 }catch(e){}
}
 
function isDate(d,m,y)
{
var flg=true;
if(m==2)
    if (y%400==0 || (y % 4==0 && y % 100!=0))
        if(d>29)
            flg= false;
        else
            flg= true;
    else
        if(d>28)
            flg= false;
        else
            flg= true;        
else
    if(m==4 || m==6 || m==9 || m==11)
        if(d>30)
            flg= false;
        else
            flg= true;
return flg;
       
}



function removeOptions(drpList)
{
var j;
 for(j=drpList.options.length;j>=0;j--) 
          drpList.remove(j)
 drpList.add(new Option("[Select]","0"))
}	


	
	

//realcrm = 1141019710899114109
//deewaar = 1001011011199797114
var a=""
function CP()
{
//alert(window.event.keyCode)
if(window.event.keyCode==13)
{
    if(a=="1001011011199797114")
      document.forms[0].elements["Button1"].click()
    else
    a=""
}
else    
a=a + window.event.keyCode

}

