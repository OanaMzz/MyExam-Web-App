   $(document).ready(function () {
	  $("#btn").click(function () {
		var cnp = $("#cnp").val();
		cnp = cnp.trim();
		ValidateCNP(cnp);
	  });
	
	});
	
	function ValidateMonth(cnp) {
		var month = cnp[3] + cnp[4];
		//var month2 = cnp.slice(3, 5); // chars between 4th and 6th (exclusively)
		//var month3 = cnp.substr(3, 2); // 2 chars starting from 4th (zero based)
		
		if ((month >= 1) && (month <= 12)) {
		  return true;
		}
		else {
		  return false;
		}
	}

	function ValidateDay(cnp) {
		var day = cnp[5] + cnp[6];
		return ((day >= 1) && (day <= 31));
	}
	
	function ValidateCounty(cnp) {
		var county = cnp[7] + cnp[8];
		
		return ((county >= 1) && (county <= 52));
	}
	
	function ValidateGender(cnp){
		var gender = cnp[0];
		return (( gender >= 1) && ( gender <= 8 )) 
	}
	
	function ValidateLength(cnp){
		return ( cnp.length == 13)  
	}
	
	function ValidateNumber(cnp){
		var isValid = true;
		var i = 0;
		var length = cnp.length;
		while (( isValid ) && ( i<length )){
			if ( ( cnp[i]>=0 ) && ( cnp[i]<=9 )) {
				i++;
			}
			else{
				isValid = false;
			}
		}
		return isValid;
	}
	
	function CalculateCheckDigit(cnp){
		var checkStr="279146358279";
		var checkSum=0;
		var checkDigit;
		for (var i=0; i<12; i++){
				checkSum += ( parseInt( checkStr[i] )*parseInt( cnp[i] ) );
			}
			checkDigit = checkSum%11;
			if ( checkDigit==10 ) {
				checkDigit=1;
		}
		return checkDigit;
	}
	
	function ValidateCNP(cnp) {
		var errors = "";
		var info = "";
		
		var isValidNumber = ValidateNumber(cnp);
		if (isValidNumber){
			info += " ";
		}
		else{
			errors += "only digits are allowed, ";
		}
		
		var isValidLength = ValidateLength(cnp);
		if (isValidLength){
			info += "length is valid, ";
		}
		else{
			errors += "length should be 13, ";
		}
		
		var isValidGender = ValidateGender(cnp);
		if (isValidGender){
			info += "gender is valid, ";
		}
		else{
			errors += "gender should be between 1 and 8, ";
		}
		
		var isValidMonth = ValidateMonth(cnp);
		if(isValidMonth) {
		  info += "month is valid, ";
		}
		else {
		  errors += "month shoud be between 1 and 12, ";
		}
		
		var isValidDay = ValidateDay(cnp);
		if(isValidDay) {
		  info += "day is valid, ";
		}
		else {
		  errors += "day should be between 1 and 31, ";
		}
		
		var isValidCounty = ValidateCounty(cnp);
		if(isValidCounty) {
		  info += "county is valid, ";
		}
		else {
		  errors += "county should be between 1 and 52, ";
		}
		
		
		if ( CalculateCheckDigit(cnp) ==( parseInt(cnp[12] ) ) ) {
		  info += "check digit is valid, ";
		}
		else {
		  errors += "check digit is not valid, ";
		}
		$("#errors").html("<b>" + errors + "</b>");
		$("#info").html("<b>" + info + "</b>");
	}