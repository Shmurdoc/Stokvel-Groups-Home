function success() {
    if (document.getElementById("accounttype").value =="Select Gender" || document.getElementById("depositamount").value === "") {
        document.getElementById('buttondepo').disabled = true;
    } else {
        document.getElementById('buttondepo').disabled = false;
    }
}


